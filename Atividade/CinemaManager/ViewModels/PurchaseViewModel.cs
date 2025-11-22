using CinemaManager.Models;
using System.ComponentModel.DataAnnotations; 

namespace CinemaManager.ViewModels
{
    public class PurchaseViewModel
    {
        public Session Session { get; set; } = null!;
        public List<int> OccupiedSeats { get; set; } = new List<int>();

        
        [Display(Name = "Nome do Cliente (Opcional)")]
        public string? CustomerName { get; set; }

        [Display(Name = "CPF (Opcional)")]
        public string? CustomerCPF { get; set; }

        [Display(Name = "Forma de Pagamento")]
        public string PaymentMethod { get; set; } = "Cartão de Crédito"; // Padrão
       
    }
}