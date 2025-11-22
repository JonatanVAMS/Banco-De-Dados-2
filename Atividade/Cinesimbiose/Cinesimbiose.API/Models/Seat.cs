using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
namespace Cinesimbiose.API.Models;
public class Seat
{
    [Key] public int IdSeat { get; set; }
    [Required, StringLength(10)] public string SeatNumber { get; set; }
    [StringLength(20)] public string? SeatType { get; set; }
    public int IdTheater { get; set; }
    [ForeignKey("IdTheater")] public virtual Theater Theater { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}