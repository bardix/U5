using EsSettimanaleU5S3.DataModel;

namespace EsSettimanaleU5S3.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
