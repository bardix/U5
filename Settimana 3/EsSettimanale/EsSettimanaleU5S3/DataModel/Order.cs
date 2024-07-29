namespace EsSettimanaleU5S3.DataModel
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        public bool IsCompleted { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
