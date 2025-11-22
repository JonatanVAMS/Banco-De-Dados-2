namespace Trabalho_Cinema.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TicketPrice { get; set; }
        public string? SessionLanguage { get; set; }
        public string? SessionType { get; set; }

    }
}
        