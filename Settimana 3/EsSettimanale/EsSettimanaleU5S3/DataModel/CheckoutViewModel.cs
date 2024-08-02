using EsSettimanaleU5S3.DataModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EsSettimanaleU5S3.Models
{
    public class CheckoutViewModel
    {
        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string Notes { get; set; }

        public List<CartItem> CartItems { get; set; } // Corretto
    }
}
