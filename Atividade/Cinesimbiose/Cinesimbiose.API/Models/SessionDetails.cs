using System.ComponentModel.DataAnnotations;
namespace Cinesimbiose.API.Models;
public class SessionDetails
{
    [Key] public int IdSession { get; set; }
    public DateTime StartTime { get; set; }
    public decimal TicketPrice { get; set; }
    public string? DisplayLanguage { get; set; }
    public string? DisplayType { get; set; }
    public string SessionStatus { get; set; }
    public string MovieTitle { get; set; }
    public string? Rating { get; set; }
    public string TheaterNumber { get; set; }
    public string CinemaName { get; set; }
}