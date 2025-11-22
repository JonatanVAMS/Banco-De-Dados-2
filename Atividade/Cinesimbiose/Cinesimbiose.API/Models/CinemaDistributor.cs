using System.ComponentModel.DataAnnotations.Schema;
namespace Cinesimbiose.API.Models;
public class CinemaDistributor
{
    public int IdCinema { get; set; }
    public int IdDistributor { get; set; }
    [ForeignKey("IdCinema")] public virtual Cinema Cinema { get; set; }
    [ForeignKey("IdDistributor")] public virtual Distributor Distributor { get; set; }
}