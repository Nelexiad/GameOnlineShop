
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        // Chiave esterna per UserId
        public string UserId { get; set; }
        [Required]

        public bool isDeleted { get; set; } = false;

        
      

    }
}
