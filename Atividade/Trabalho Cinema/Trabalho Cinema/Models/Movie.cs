namespace Trabalho_Cinema.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int IndicativeClassification { get; set; }
        public string? OriginalLanguage { get; set; }
        public string? Dub { get; set; }
        public string? Gender { get; set; }

    }
}
