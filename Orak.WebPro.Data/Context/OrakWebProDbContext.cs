using Microsoft.EntityFrameworkCore;
using Orak.WebPro.Data.Entities;

namespace Orak.WebPro.Data.Context
{
    public class OrakWebProDbContext : DbContext
    {
        public OrakWebProDbContext(DbContextOptions<OrakWebProDbContext> options)
            : base(options)
        {
        }

        public DbSet<Website> Websites => Set<Website>();
        public DbSet<WebsiteDomain> WebsiteDomains => Set<WebsiteDomain>();
        public DbSet<WebsiteSetting> WebsiteSettings => Set<WebsiteSetting>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Website>(entity =>
            {
                entity.HasIndex(x => x.Slug).IsUnique();

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Slug)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<WebsiteDomain>(entity =>
            {
                entity.HasIndex(x => x.DomainName).IsUnique();

                entity.Property(x => x.DomainName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(x => x.Website)
                    .WithMany(x => x.Domains)
                    .HasForeignKey(x => x.WebsiteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WebsiteSetting>(entity =>
            {
                entity.HasIndex(x => x.WebsiteId).IsUnique();

                entity.HasOne(x => x.Website)
                    .WithOne(x => x.Settings)
                    .HasForeignKey<WebsiteSetting>(x => x.WebsiteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}