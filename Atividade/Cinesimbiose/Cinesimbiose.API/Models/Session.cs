using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
namespace Cinesimbiose.API.Models;
public class Session
{
    [Key] public int IdSession { get; set; }
    [Required] public DateTime StartTime { get; set; }
    [Required] public DateTime EndTime { get; set; }
    [Required, Column(TypeName = "decimal(10, 2)")] public decimal TicketPrice { get; set; }
    [StringLength(50)] public string? DisplayLanguage { get; set; }
    [StringLength(50)] public string? DisplayType { get; set; }
    public int IdMovie { get; set; }
    public int IdTheater { get; set; }
    [StringLength(20)] public string SessionStatus { get; set; } = "AGENDADA";
    [ForeignKey("IdMovie")] public virtual Movie Movie { get; set; }
    [ForeignKey("IdTheater")] public virtual Theater Theater { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}