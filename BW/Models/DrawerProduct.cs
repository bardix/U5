namespace BW2_Team6.Models
{
    public class DrawerProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int DrawerId { get; set; }
        public Drawer Drawer { get; set; }
    }
}
