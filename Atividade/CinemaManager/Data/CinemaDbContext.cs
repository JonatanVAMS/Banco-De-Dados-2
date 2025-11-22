using CinemaManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Data
{
    public class CinemaDbContext : IdentityDbContext<ApplicationUser>
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 


           
            builder.Entity<Movie>()
                .HasMany(m => m.Sessions)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict); // Não deixa deletar filme com sessão

            // N Sessions
            builder.Entity<Room>()
                .HasMany(r => r.Sessions)
                .WithOne(s => s.Room)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // Não deixa deletar sala com sessão

            // N Tickets
            builder.Entity<Session>()
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Session)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Cascade); // Deleta ingressos se a sessão for deletada

            // N Ticket
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.ApplicationUser)
                .HasForeignKey(t => t.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict); // Não deixa deletar usuário com ingresso
        }
    }
}