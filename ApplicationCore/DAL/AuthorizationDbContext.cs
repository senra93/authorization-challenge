using Authorization.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DAL
{
    public class AuthorizationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Card> Cards { get; set; }

        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
            .HasIndex(p => p.Id);

            modelBuilder.Entity<Card>()
            .HasIndex(p => p.Number)
            .IsUnique();

            modelBuilder.Entity<Transaction>()
            .HasIndex(p => p.CardId);

            modelBuilder.Entity<User>()
            .HasIndex(p => p.Id);

            modelBuilder.Entity<User>()
            .HasIndex(p => p.Email)
            .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
