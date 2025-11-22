using System.ComponentModel.DataAnnotations;

namespace CinemaManager.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Título")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [Display(Name = "Descrição")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "A duração é obrigatória")]
        [Range(30, 300, ErrorMessage = "A duração deve ser entre 30 e 300 minutos")]
        [Display(Name = "Duração (min)")]
        public int DurationMinutes { get; set; } 

        [Required(ErrorMessage = "A classificação é obrigatória")]
        [Display(Name = "Classificação")]
        public string Rating { get; set; } = null!; 

        [Required(ErrorMessage = "O gênero é obrigatório")]
        [Display(Name = "Gênero")]
        public string Genre { get; set; } = null!;

        [Display(Name = "URL do Cartaz")]
      
        public string PosterUrl { get; set; } = null!;
        // Propriedade de navegação
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}