using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TuiToot.Server.Api.Cores;
using TuiToot.Server.Api.Exceptions;
using TuiToot.Server.Api.Services;
using TuiToot.Server.Api.Services.IServices;
using TuiToot.Server.Infrastructure.EfCore.DataAccess;
using TuiToot.Server.Infrastructure.EfCore.Models;
using TuiToot.Server.Infrastructure.EfCore.Repository;

namespace TuiToot.Server.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("ApiSetting:JwtOptions"));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();
            
            builder.Services.AddHttpContextAccessor();
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register services
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IInvalidTokenRepository, InvalidTokenRepository>();
            builder.Services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();
            builder.Services.AddScoped<IDeliveryAddressService, DeliveryAddressService>();

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

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
