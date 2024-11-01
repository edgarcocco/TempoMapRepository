using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TempoMapRepository.Models.Domain;
using TempoMapRepository.Models.Identity;

namespace TempoMapRepository.Data.Context
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public DbSet<Map> Maps { get; set; } = null!;
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Maps)
                .WithOne(e => e.User)
                .HasForeignKey("e.UserId")
                .IsRequired(false);

            modelBuilder.Entity<Map>()
                .HasMany(e => e.Datasets)
                .WithOne(e => e.Map)
                .HasForeignKey("e.MapId");
        }
    }
}
