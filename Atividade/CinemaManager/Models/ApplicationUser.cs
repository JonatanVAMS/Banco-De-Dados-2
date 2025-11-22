using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace CinemaManager.Models
{
    // Estendendo o IdentityUser padrão
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Nome Completo")] // Frontend 
        public string FullName { get; set; } // Backend 

      
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}