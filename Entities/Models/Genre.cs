using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }
        [Required]

        public List<Videogame> Videogames { get; set; }
    }
}
