using System.ComponentModel.DataAnnotations;
namespace Cinesimbiose.API.Models;
public class CinemaGroup
{
    [Key] public int IdGroup { get; set; }
    [Required, StringLength(100)] public string GroupName { get; set; }
    [StringLength(100)] public string? GroupContact { get; set; }
    public virtual ICollection<Cinema> Cinemas { get; set; } = new List<Cinema>();
}