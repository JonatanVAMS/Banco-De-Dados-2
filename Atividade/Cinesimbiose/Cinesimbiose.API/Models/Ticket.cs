using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinesimbiose.API.Models;
public class Ticket
{
    [Key] public int IdTicket { get; set; }
    [Required, Column(TypeName = "decimal(10, 2)")] public decimal FinalPrice { get; set; }
    [Required, StringLength(20)] public string TicketStatus { get; set; }
    [Required, StringLength(20)] public string TicketType { get; set; }
    public int IdSession { get; set; }
    public int IdSeat { get; set; }
    public int IdCustomer { get; set; }
    [ForeignKey("IdSession")] public virtual Session Session { get; set; }
    [ForeignKey("IdSeat")] public virtual Seat Seat { get; set; }
    [ForeignKey("IdCustomer")] public virtual Customer Customer { get; set; }
    public virtual ICollection<TicketSaleLog> SaleLogs { get; set; } = new List<TicketSaleLog>();
}