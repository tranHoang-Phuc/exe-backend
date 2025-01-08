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
        }
    }
}
