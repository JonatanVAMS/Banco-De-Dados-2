using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinesimbiose.API.Models;
public class Actor
{
    [Key] public int IdActor { get; set; }
    [Required, StringLength(100)] public string ActorName { get; set; }
    [Column(TypeName = "date")] public DateTime? BirthDate { get; set; }
    [StringLength(50)] public string? Nationality { get; set; }
    public virtual ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}