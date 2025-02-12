    using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Filter;
using TuiToot.Server.Api.Services;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;
using TuiToot.Server.Infrastructure.EfCore.Repository;

namespace TuiToot.Server.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("ApiSetting:JwtOptions"));
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("Cloudinary")
                );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();
            
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
               options =>
               {
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["ApiSetting:JwtOptions:Issuer"],
                       ValidAudience = builder.Configuration["ApiSetting:JwtOptions:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                           .GetBytes(builder.Configuration["ApiSetting:JwtOptions:Secret"]))
                   };

                   options.Events = new JwtBearerEvents
                   {
                       OnTokenValidated = async context =>
                       {
                           var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
                           var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                           if (!string.IsNullOrEmpty(token))
                           {
                               var invalidToken = await unitOfWork.InvalidTokenRepository
                                   .FindAsync(t => t.Token == token);

                               if (invalidToken.Count() > 0)
                               {
                                   throw new AppException(ErrorCode.Unauthorized);
                               }
                           }
                       },
                       OnAuthenticationFailed = async context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               throw new AppException(ErrorCode.Unauthorized);
                           }
                       },

                   };
               });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("USER", policy => policy.RequireRole("USER"));
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToteBag", Version = "v1" });
                c.OperationFilter<AddFileUploadSupportFilter>();
            });

            // Register services
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IInvalidTokenRepository, InvalidTokenRepository>();
            builder.Services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();
            builder.Services.AddScoped<IDeliveryAddressService, DeliveryAddressService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IBagTypeRepository, BagTypeRepository>();
            builder.Services.AddScoped<ICloudaryService, CloudaryService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IAvaliblreProductRepository, AvalibleProductRepository>();
            builder.Services.AddScoped<IAvalibleProductService, AvalibleProductService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<IBagTypeService, BagTypeService>();

            builder.Services.AddSingleton(provider =>
            {
                var settings = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                return new CloudinaryDotNet.Cloudinary(new Account(
                    settings.CloudName,
                    settings.ApiKey,
                    settings.ApiSecret
                ));
            });
            var app = builder.Build();
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandler = app.Services.GetRequiredService<ILogger<GlobalExceptionHandler>>();
                    var handler = new GlobalExceptionHandler(exceptionHandler);
                    await handler.InvokeAsync(context);
                });
            });

            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll"); 


            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await AddDefault(app);
            app.Run();

        }

        private static async Task AddDefault(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!roleManager.Roles.Any())
                    {
                        await roleManager.CreateAsync(new IdentityRole("USER"));
                        await roleManager.CreateAsync(new IdentityRole("ADMIN"));
                    }

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    if (!userManager.Users.Any())
                    {
                        var adminUser = new ApplicationUser
                        {
                            Name = "Super Admin",
                            UserName = "phucthhe172242@fpt.edu.vn",
                            Email = "phucthhe172242@fpt.edu.vn",
                            Phone = "0123456789",
                            Address = "Ha Noi",
                            NormalizedEmail = "phucthhe172242@fpt.edu.vn".ToUpper()
                        };

                        var createResult = await userManager.CreateAsync(adminUser, "@Happy3115");
                        // Account super  Admin:
                        // phucthhe172242@fpt.edu.vn 
                        // @Happy3115
                        if (createResult.Succeeded)
                        {
                            var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "ADMIN");
                            
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    throw new AppException(ErrorCode.UncategorizedException);
                }
            }
        }
    }
}
