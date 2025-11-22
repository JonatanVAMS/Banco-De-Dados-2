using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaManager.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public int SessionId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [Required]
        [Display(Name = "Assento")]
        public int SeatNumber { get; set; }

        [Display(Name = "Data da Compra")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Nome do Cliente")]
        public string? CustomerName { get; set; } 

        [Display(Name = "CPF")]
        public string? CustomerCPF { get; set; }
        

        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; } = null!;

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}