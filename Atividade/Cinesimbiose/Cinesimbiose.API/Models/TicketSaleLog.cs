using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinesimbiose.API.Models;
public class TicketSaleLog
{
    [Key] public int IdLog { get; set; }
    public int IdTicket { get; set; }
    [Required, Column(TypeName = "decimal(10, 2)")] public decimal SalePrice { get; set; }
    public DateTime SaleDateTime { get; set; }
    public int? IdSession { get; set; }
    public int? IdCustomer { get; set; }
    [ForeignKey("IdTicket")] public virtual Ticket Ticket { get; set; }
    [ForeignKey("IdSession")] public virtual Session? Session { get; set; }
    [ForeignKey("IdCustomer")] public virtual Customer? Customer { get; set; }
}