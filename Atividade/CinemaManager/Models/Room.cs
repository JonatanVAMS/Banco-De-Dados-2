using System.ComponentModel.DataAnnotations;

namespace CinemaManager.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da sala é obrigatório")]
        [Display(Name = "Nome da Sala")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A quantidade de assentos é obrigatória")]
        [Range(10, 200, ErrorMessage = "A sala deve ter entre 10 e 200 assentos")]
        [Display(Name = "Nº de Assentos")]
        public int SeatCount { get; set; }

        [Required(ErrorMessage = "O tipo da sala é obrigatório")]
        [Display(Name = "Tipo")]
        public string Type { get; set; } 


        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}