using System.ComponentModel.DataAnnotations.Schema;
namespace Cinesimbiose.API.Models;
public class MovieActor
{
    public int IdMovie { get; set; }
    public int IdActor { get; set; }
    [ForeignKey("IdMovie")] public virtual Movie Movie { get; set; }
    [ForeignKey("IdActor")] public virtual Actor Actor { get; set; }
}