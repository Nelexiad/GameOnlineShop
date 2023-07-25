using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        // Chiave esterna per UserId
        public string UserId { get; set; }



        

        // Proprietà di navigazione per gli elementi del carrello
        public IdentityUser User { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
}
