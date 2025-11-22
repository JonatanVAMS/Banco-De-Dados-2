using Cinesimbiose.API.Models;
using Microsoft.EntityFrameworkCore;
using System;  DateTime

namespace Cinesimbiose.API.Data
{
    public class CinesimbioseContext : DbContext
    {
        public CinesimbioseContext(DbContextOptions<CinesimbioseContext> options)
            : base(options)
        {
        }

        ///Composição/Agregação. O Context "tem" coleções de objetos.
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<CinemaGroup> CinemaGroups { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaDistributor> CinemaDistributors { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketSaleLog> TicketSaleLogs { get; set; }
        public DbSet<SessionDetails> SessionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Definindo os Relacionamentos entre os objetos.
            modelBuilder.Entity<CinemaDistributor>()
                .HasKey(cd => new { cd.IdCinema, cd.IdDistributor });

            modelBuilder.Entity<MovieActor>()
                .HasKey(fa => new { fa.IdMovie, fa.IdActor });

            modelBuilder.Entity<Seat>()
                .HasIndex(p => new { p.SeatNumber, p.IdTheater })
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.CustomerEmail)
                .IsUnique();

            modelBuilder.Entity<Ticket>()
                .HasIndex(i => new { i.IdSession, i.IdSeat })
                .IsUnique();

            modelBuilder.Entity<TicketSaleLog>()
                .Property(l => l.SaleDateTime)
                .HasDefaultValueSql("GETDATE()"); // Sintaxe SQL Server

            modelBuilder.Entity<SessionDetails>(eb =>
            {
                eb.ToView("V_SessionDetails");
                eb.HasKey(v => v.IdSession);
            });

            // --- Data Seeding (Criação de "Instâncias" de objetos) ---
            modelBuilder.Entity<Distributor>().HasData(
                new Distributor { IdDistributor = 1, DistributorName = "CINESIMBIOSE", ContactPhone = "XX-XXXX-XXXX", ContactEmail = "contato@cinesimbiose.com.br", Address = "Rua da Distribuição, 100, Cidade Global" },
                new Distributor { IdDistributor = 2, DistributorName = "FILMES FANTASTICOS", ContactPhone = "55-11-98765-4321", ContactEmail = "contato@filmesfantasticos.com", Address = "Av. Cinema, 500, SAO PAULO" }
            );
            modelBuilder.Entity<CinemaGroup>().HasData(
                new CinemaGroup { IdGroup = 1, GroupName = "REDE ESTELAR", GroupContact = "redestelar@contato.com.br" },
                new CinemaGroup { IdGroup = 2, GroupName = "CINEMAS POP", GroupContact = "cinemaspop@contato.com.br" }
            );
            modelBuilder.Entity<Customer>().HasData(
                new Customer { IdCustomer = 1, CustomerName = "MARIA SILVA", CustomerEmail = "maria.silva@email.com" },
                new Customer { IdCustomer = 2, CustomerName = "JOAO SOUZA", CustomerEmail = "joao.souza@email.com" },
                new Customer { IdCustomer = 3, CustomerName = "ANA PAULA", CustomerEmail = "ana.paula@email.com" }
            );
            modelBuilder.Entity<Movie>().HasData(
                new Movie { IdMovie = 1, Title = "A JORNADA DO HEROI", Description = "UMA EPICA AVENTURA SOBRE UM JOVEM EM BUSCA DE SUA ORIGEM.", DurationMinutes = 180, Rating = "LIVRE", OriginalLanguage = "INGLES", DubbedLanguage = "PORTUGUES", Genre = "AVENTURA" },
                new Movie { IdMovie = 2, Title = "O MISTERIO DA ILHA", Description = "UM SUSPENSE INTRIGANTE QUE SE PASSA EM UMA ILHA ISOLADA.", DurationMinutes = 110, Rating = "14 ANOS", OriginalLanguage = "PORTUGUES", DubbedLanguage = null, Genre = "SUSPENSE" },
                new Movie { IdMovie = 3, Title = "COMEDIA ROMANTICA PERFEITA", Description = "UM FILME LEVE E DIVERTIDO SOBRE AMOR E AMIZADE.", DurationMinutes = 95, Rating = "12 ANOS", OriginalLanguage = "INGLES", DubbedLanguage = "PORTUGUES", Genre = "COMEDIA ROMANTICA" },
                new Movie { IdMovie = 4, Title = "DOCUMENTARIO DA NATUREZA", Description = "EXPLORE A VIDA SELVAGEM EM SEUS HABITATS NATURAIS.", DurationMinutes = 60, Rating = "LIVRE", OriginalLanguage = "PORTUGUES", DubbedLanguage = "INGLES", Genre = "DOCUMENTARIO" }
            );
            modelBuilder.Entity<Actor>().HasData(
                new Actor { IdActor = 1, ActorName = "PEDRO ALVARES", BirthDate = new DateTime(1980, 3, 10), Nationality = "BRASILEIRA" },
                new Actor { IdActor = 2, ActorName = "SOFIA COSTA", BirthDate = new DateTime(1992, 7, 25), Nationality = "PORTUGUESA" },
                new Actor { IdActor = 3, ActorName = "LUCAS FERNANDES", BirthDate = new DateTime(1975, 11, 1), Nationality = "BRASILEIRA" },
                new Actor { IdActor = 4, ActorName = "ISABELA LIMA", BirthDate = new DateTime(1995, 1, 18), Nationality = "AMERICANA" }
            );
            modelBuilder.Entity<Cinema>().HasData(
                new Cinema { IdCinema = 1, FantasyName = "CINE ESTRELA SUL", Address = "RUA DAS PALMEIRAS, 100, BAIRRO JARDIM", IdGroup = 1 },
                new Cinema { IdCinema = 2, FantasyName = "MEGA CINE CENTRO", Address = "AV. PRINCIPAL, 50, CENTRO", IdGroup = 1 },
                new Cinema { IdCinema = 3, FantasyName = "CINE KIDS MANIA", Address = "SHOPPING DA CRIANCA, LOJA 10, CIDADE NOVA", IdGroup = 2 }
            );
            modelBuilder.Entity<CinemaDistributor>().HasData(
                new CinemaDistributor { IdCinema = 1, IdDistributor = 1 },
                new CinemaDistributor { IdCinema = 1, IdDistributor = 2 },
                new CinemaDistributor { IdCinema = 2, IdDistributor = 1 },
                new CinemaDistributor { IdCinema = 3, IdDistributor = 2 }
            );
            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { IdMovie = 1, IdActor = 1 },
                new MovieActor { IdMovie = 1, IdActor = 2 },
                new MovieActor { IdMovie = 2, IdActor = 3 },
                new MovieActor { IdMovie = 3, IdActor = 1 },
                new MovieActor { IdMovie = 3, IdActor = 4 }
            );
            modelBuilder.Entity<Theater>().HasData(
                new Theater { IdTheater = 1, TheaterNumber = "SALA 1", Capacity = 150, IdCinema = 1 },
                new Theater { IdTheater = 2, TheaterNumber = "SALA VIP", Capacity = 50, IdCinema = 1 },
                new Theater { IdTheater = 3, TheaterNumber = "SALA MASTER", Capacity = 200, IdCinema = 2 },
                new Theater { IdTheater = 4, TheaterNumber = "SALA INFANTIL", Capacity = 80, IdCinema = 3 }
            );
            modelBuilder.Entity<Session>().HasData(
                new Session { IdSession = 1, StartTime = DateTime.Parse("2025-06-25 10:00:00"), EndTime = DateTime.Parse("2025-06-25 13:00:00"), TicketPrice = 35.00m, DisplayLanguage = "PORTUGUES", DisplayType = "3D", IdMovie = 1, IdTheater = 1, SessionStatus = "ENCERRADA" },
                new Session { IdSession = 2, StartTime = DateTime.Parse("2025-06-25 14:00:00"), EndTime = DateTime.Parse("2025-06-25 15:50:00"), TicketPrice = 30.00m, DisplayLanguage = "PORTUGUES", DisplayType = "2D", IdMovie = 2, IdTheater = 1, SessionStatus = "AGENDADA" },
                new Session { IdSession = 3, StartTime = DateTime.Parse("2025-06-25 19:30:00"), EndTime = DateTime.Parse("2025-06-25 21:05:00"), TicketPrice = 40.00m, DisplayLanguage = "INGLES", DisplayType = "IMAX", IdMovie = 3, IdTheater = 2, SessionStatus = "AGENDADA" },
                new Session { IdSession = 4, StartTime = DateTime.Parse("2025-06-26 10:30:00"), EndTime = DateTime.Parse("2025-06-26 11:30:00"), TicketPrice = 25.00m, DisplayLanguage = "PORTUGUES", DisplayType = "2D", IdMovie = 4, IdTheater = 4, SessionStatus = "AGENDADA" }
            );
            modelBuilder.Entity<Seat>().HasData(
                new Seat { IdSeat = 1, SeatNumber = "A1", SeatType = "NORMAL", IdTheater = 1 }, new Seat { IdSeat = 2, SeatNumber = "A2", SeatType = "NORMAL", IdTheater = 1 }, new Seat { IdSeat = 3, SeatNumber = "A3", SeatType = "NORMAL", IdTheater = 1 },
                new Seat { IdSeat = 4, SeatNumber = "B1", SeatType = "NORMAL", IdTheater = 1 }, new Seat { IdSeat = 5, SeatNumber = "B2", SeatType = "NORMAL", IdTheater = 1 }, new Seat { IdSeat = 6, SeatNumber = "B3", SeatType = "NORMAL", IdTheater = 1 },
                new Seat { IdSeat = 7, SeatNumber = "C1", SeatType = "NORMAL", IdTheater = 1 }, new Seat { IdSeat = 8, SeatNumber = "V1", SeatType = "VIP", IdTheater = 2 }, new Seat { IdSeat = 9, SeatNumber = "V2", SeatType = "VIP", IdTheater = 2 },
                new Seat { IdSeat = 10, SeatNumber = "V3", SeatType = "VIP", IdTheater = 2 }, new Seat { IdSeat = 11, SeatNumber = "M1", SeatType = "NORMAL", IdTheater = 3 }, new Seat { IdSeat = 12, SeatNumber = "M2", SeatType = "NORMAL", IdTheater = 3 }
            );
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { IdTicket = 1, FinalPrice = 35.00m, TicketStatus = "VENDIDO", TicketType = "INTEIRA", IdSession = 1, IdSeat = 1, IdCustomer = 1 },
                new Ticket { IdTicket = 2, FinalPrice = 20.00m, TicketStatus = "VENDIDO", TicketType = "MEIA-ENTRADA", IdSession = 3, IdSeat = 8, IdCustomer = 2 },
                new Ticket { IdTicket = 3, FinalPrice = 35.00m, TicketStatus = "RESERVADO", TicketType = "INTEIRA", IdSession = 1, IdSeat = 2, IdCustomer = 3 }
            );
        }
    }
}