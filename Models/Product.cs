namespace Dashboard.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Brand { get; set; }

        public int Price { get; set;}

        public int Quantity { get; set; }

        public DateTime PurchaseDate { get; set; } 

        public DateTime PurchaseTime { get; set; } 
    }
    
}