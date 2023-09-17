namespace Dashboard.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Brand { get; set; }

        public int Price { get; set;}

        public int Quantity { get; set; }

        public DateOnly PurchaseDate { get; set; } 

        public TimeOnly PurchaseTime { get; set; } 

        public DateOnly EditDate { get; set; } 

        public TimeOnly EditTime { get; set; } 
    }
    
}