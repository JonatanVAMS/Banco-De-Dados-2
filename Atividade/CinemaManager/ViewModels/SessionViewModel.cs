using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CinemaManager.ViewModels
{
    public class SessionViewModel
    {
     
        public int Id { get; set; }

        [Required(ErrorMessage = "O filme é obrigatório.")]
        [Display(Name = "Filme")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "A sala é obrigatória.")]
        [Display(Name = "Sala")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "O horário é obrigatório.")]
        [Display(Name = "Horário da Sessão")]
        public DateTime ScheduledTime { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Price { get; set; }

        // Listas para os dropdowns
        public SelectList? MoviesList { get; set; }
        public SelectList? RoomsList { get; set; }
    }
}