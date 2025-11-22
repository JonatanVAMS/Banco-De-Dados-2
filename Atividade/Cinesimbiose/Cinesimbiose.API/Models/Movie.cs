using System.ComponentModel.DataAnnotations;
namespace Cinesimbiose.API.Models;
public class Movie
{
    [Key] public int IdMovie { get; set; }
    [Required, StringLength(255)] public string Title { get; set; }
    public string? Description { get; set; }
    public int? DurationMinutes { get; set; }
    [StringLength(10)] public string? Rating { get; set; }
    [StringLength(50)] public string? OriginalLanguage { get; set; }
    [StringLength(50)] public string? DubbedLanguage { get; set; }
    [StringLength(100)] public string? Genre { get; set; }
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    public virtual ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}