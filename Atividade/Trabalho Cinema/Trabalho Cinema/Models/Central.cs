using System.ComponentModel.DataAnnotations;

namespace Trabalho_Cinema.Models
{
    public class Central
    {
        [Key]

        public int CentralID { get; set; }
        public string? CentralName { get; set; }
        public int CentralPhoneNumber { get; set; }
        public string? CentralContact { get; set; }
        public string? CentralAddress {  get; set; }
    }
}
