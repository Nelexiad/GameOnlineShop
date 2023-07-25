namespace WebApplication1.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // Chiave esterna per il carrello (ShoppingCartId)
        public int ShoppingCartId { get; set; }

        // Proprietà di navigazione per il carrello
        public ShoppingCart ShoppingCart { get; set; }

        // Chiave esterna per l'ID del videogioco
        public int VideoGameId { get; set; }

        // Proprietà di navigazione per il videogioco
        public VideoGame VideoGame { get; set; }

        public int? PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
    }
}
