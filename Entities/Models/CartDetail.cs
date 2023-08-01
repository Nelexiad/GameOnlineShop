using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace Entities.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int VideogameId { get; set; }
        [Required]

        public int Quantity { get; set; }   
        public Videogame Videogame { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

    }
}
