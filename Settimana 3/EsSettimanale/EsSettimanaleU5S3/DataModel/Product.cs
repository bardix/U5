using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EsSettimanaleU5S3.DataModel
{
    public class Product
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int DeliveryTime { get; set; }

        public ICollection<IngredientProduct> IngredientProducts { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
