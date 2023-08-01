using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Videogame")]
    public class Videogame
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]

        public int GenreId { get; set; }
        public string? Developer { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        
        public double Price { get; set; }

        public string? CoverVideogame { get; set; }
        public DateTime ReleaseDate { get; set; } 

        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }

        public List<CartDetail> CartDetail { get; set; }
        [NotMapped]
        public string GenreName { get; set; }   

        
    }
}
