namespace Entities.Models.DTO
{
    public class VideogameDTO
    {
        
        public string Title { get; set; }
        public int GenreId { get; set; }

        public string Developer { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CoverVideogame { get; set; }
        public DateTime ReleaseDate { get; set; }
       
    }
}
