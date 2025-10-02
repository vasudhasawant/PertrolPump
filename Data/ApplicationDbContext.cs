using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetrolPumpLog.Models;

namespace PetrolPumpLog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DispensingRecord> DispensingRecords { get; set; }
        public DbSet<LoginUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DispensingRecord>()
                .Property(r => r.QuantityLiters)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<DispensingRecord>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }

    }
}
