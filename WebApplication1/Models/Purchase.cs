namespace WebApplication1.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        // Chiave esterna per UserId (string)
        public string UserId { get; set; }

        // Data dell'acquisto
        public DateTime PurchaseDate { get; set; }

        // Totale dell'acquisto
        public decimal TotalAmount { get; set; }


        public ICollection<CartItem> CartItems { get; set; }
    }
}
