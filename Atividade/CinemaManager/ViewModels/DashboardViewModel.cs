namespace CinemaManager.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalMovies { get; set; }
        public int ActiveSessions { get; set; }
        public decimal SalesToday { get; set; }
        public int TicketsSoldToday { get; set; }
    }
}