using System.ComponentModel.DataAnnotations;
namespace Cinesimbiose.API.Models;
public class Distributor
{
    [Key] public int IdDistributor { get; set; }
    [Required, StringLength(100)] public string DistributorName { get; set; }
    [StringLength(20)] public string? ContactPhone { get; set; }
    [StringLength(100)] public string? ContactEmail { get; set; }
    [StringLength(255)] public string? Address { get; set; }
    public virtual ICollection<CinemaDistributor> CinemaDistributors { get; set; } = new List<CinemaDistributor>();
}