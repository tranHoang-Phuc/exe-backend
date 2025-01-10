using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuiToot.Server.Infrastructure.EfCore.Models;

namespace TuiToot.Server.Infrastructure.EfCore.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<InvalidToken> InvalidTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Phone).IsRequired();
                entity.Property(e => e.CreatedTime)
                    .HasDefaultValueSql("GETDATE()").IsRequired();
                entity.Property(e => e.UpdatedTime)
                    .HasDefaultValueSql("GETDATE()").IsRequired();
            });
            builder.Entity<InvalidToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); 
                entity.Property(e => e.Token)
                      .IsRequired();
            });

            builder.Entity<DeliveryAddress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.ProvinceId)
                        .IsRequired();
                entity.Property(e => e.DistrictId)
                        .IsRequired();
                entity.Property(e => e.WardId)
                        .IsRequired();
                entity.Property(e => e.DetailAddress)
                        .IsRequired();
                entity.HasOne<ApplicationUser>(a => a.ApplicationUser)
                        .WithMany(d => d.DeliveryAddresses)
                        .HasForeignKey(d => d.ApplicationUserId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BagType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                        .IsRequired();
                entity.Property(e => e.Url)
                        .IsRequired();
                entity.Property(e => e.Description);
                entity.HasMany<Product>(e => e.Products)
                        .WithOne(p => p.BagType)
                        .HasForeignKey(p => p.BagTypeId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.Url)
                        .IsRequired();
                entity.Property(e => e.BagTypeId)
                        .IsRequired();
                entity.Property(e => e.CreatedTime)
                        .HasDefaultValueSql("GETDATE()").IsRequired();
                entity.Property(e => e.OrderId);
                entity.HasOne<Order>(p => p.Order)
                        .WithMany(o => o.Products)
                        .HasForeignKey(p => p.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<BagType>(p => p.BagType)
                        .WithMany(b => b.Products)
                        .HasForeignKey(p => p.BagTypeId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.ApplicationUserId)
                        .IsRequired();
                entity.Property(e => e.DeliveryAddressId)
                        .IsRequired();
                entity.Property(e => e.OrderStatus)
                        .IsRequired();
                entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("GETDATE()").IsRequired();
                entity.Property(e => e.UpdatedAt)
                        .HasDefaultValueSql("GETDATE()").IsRequired();
                entity.HasOne<ApplicationUser>(o => o.ApplicationUser)
                    .WithMany(a => a.Orders)
                    .HasForeignKey(o => o.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<DeliveryAddress>(o => o.DeliveryAddress)
                      .WithMany(d => d.Orders)
                      .HasForeignKey(o => o.DeliveryAddressId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany<Product>(o => o.Products)
                      .WithOne(p => p.Order)
                      .HasForeignKey(p => p.OrderId);
            });
        }
    }
}
