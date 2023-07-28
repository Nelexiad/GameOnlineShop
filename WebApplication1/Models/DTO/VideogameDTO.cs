namespace WebApplication1.Models.MappedModel
{
    public class VideogameDTO
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CoverVideogame { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
