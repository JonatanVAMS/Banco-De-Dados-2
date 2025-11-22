using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
namespace Cinesimbiose.API.Models;
public class Customer
{
    [Key] public int IdCustomer { get; set; }
    [Required, StringLength(100)] public string CustomerName { get; set; }
    [Required, StringLength(100)] public string CustomerEmail { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<TicketSaleLog> SaleLogs { get; set; } = new List<TicketSaleLog>();
}