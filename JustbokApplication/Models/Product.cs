namespace JustbokApplication.Models
{
    public class Product 
    {
        public int ProductId { get; set; }
        public string BrandName { get; set;}
        public string ProductName { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int LowStockQuantity { get; set; }
        public bool ForSale { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }

    }
}
