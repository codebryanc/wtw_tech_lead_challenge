using Microsoft.EntityFrameworkCore;

using Entity;

namespace DAL.Base.Data
{
    public class ApplicationDbContext : DbContext
    {
        // [Constructor]
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // [Properties]
        public DbSet<Requests> Requests { get; set; }
        public DbSet<RequestTypes> RequestTypes { get; set; }
        public DbSet<RequestStatusEntity> Resources { get; set; }

        // [Methods]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for Requests entity
            modelBuilder.Entity<Requests>(entity =>
            {
                entity.HasKey(e => e.reqId);
                entity.Property(e => e.reqId).IsRequired();
                entity.Property(e => e.rtyId).IsRequired();
                entity.Property(e => e.resId).IsRequired();
                entity.Property(e => e.createdAt).IsRequired();
                entity.Property(e => e.data).IsRequired();
            });

            // Configuration for RequestTypes entity
            modelBuilder.Entity<RequestTypes>(entity =>
            {
                entity.HasKey(e => e.rtyId);
                entity.Property(e => e.rtyId).IsRequired();
                entity.Property(e => e.name).HasMaxLength(255);
            });

            // Configuration for Resources entity
            modelBuilder.Entity<RequestStatusEntity>(entity =>
            {
                entity.HasKey(e => e.resId);
                entity.Property(e => e.resId).IsRequired();
                entity.Property(e => e.name).HasMaxLength(255);
            });
        }

    }
}