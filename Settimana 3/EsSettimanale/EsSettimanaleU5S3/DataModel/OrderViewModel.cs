using EsSettimanaleU5S3.DataModel;
using System.ComponentModel.DataAnnotations;

namespace EsSettimanaleU5S3.Models
{
    public class OrderViewModel
    {
        [Required]
        [StringLength(70)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [StringLength(255)]
        [Display(Name = "Additional Notes")]
        public string Notes { get; set; }

        public List<OrderItem> OrderItems { get; set; } // Facoltativo, per mostrare il riepilogo del carrello
    }
}
