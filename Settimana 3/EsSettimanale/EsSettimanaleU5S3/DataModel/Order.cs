using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsSettimanaleU5S3.DataModel
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(70)]
        public string ShippingAddress { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
