namespace EsSettimanaleU5S3.DataModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public int DeliveryTime { get; set; }

        public ICollection<IngredientProduct> IngredientProducts { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
