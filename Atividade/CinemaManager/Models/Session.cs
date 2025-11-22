using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace CinemaManager.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Filme")]
        public int MovieId { get; set; }

        [Required]
        [Display(Name = "Sala")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "O horário é obrigatório")]
        [Display(Name = "Horário da Sessão")]
        public DateTime ScheduledTime { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preço")]
        public decimal Price { get; set; }

        [Display(Name = "Assentos Disponíveis")]
        public int AvailableSeats { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}