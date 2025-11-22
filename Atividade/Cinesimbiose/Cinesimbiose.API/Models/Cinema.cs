using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinesimbiose.API.Models;
public class Cinema
{
    [Key] public int IdCinema { get; set; }
    [Required, StringLength(100)] public string FantasyName { get; set; }
    [StringLength(255)] public string? Address { get; set; }
    public int IdGroup { get; set; }
    [ForeignKey("IdGroup")] public virtual CinemaGroup CinemaGroup { get; set; }
    public virtual ICollection<Theater> Theaters { get; set; } = new List<Theater>();
    public virtual ICollection<CinemaDistributor> CinemaDistributors { get; set; } = new List<CinemaDistributor>();
}