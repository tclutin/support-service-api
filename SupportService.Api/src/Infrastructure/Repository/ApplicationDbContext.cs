using Microsoft.EntityFrameworkCore;
using SupportService.Api.src.Entities;

namespace SupportService.Api.src.Infrastructure.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<Ticket>()
               .HasMany(t => t.Messages)
               .WithOne(m => m.Ticket)
               .HasForeignKey(m => m.TicketId);
        }
    }
}
