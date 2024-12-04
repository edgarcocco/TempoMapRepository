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
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            modelBuilder.Entity<Map>()
                .HasMany(e => e.Datasets)
                .WithOne(e => e.Map)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("e.MapId")
               ;
            modelBuilder.Entity<Map>()
                .Navigation(e => e.User)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

        }
        public DbSet<TempoMapRepository.Models.Domain.MapDataset> MapDataset { get; set; } = default!;
    }
}
