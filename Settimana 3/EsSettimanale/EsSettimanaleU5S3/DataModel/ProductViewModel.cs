using EsSettimanaleU5S3.DataModel;
using System.ComponentModel.DataAnnotations;

namespace EsSettimanaleU5S3.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }

        [Required]
        public int DeliveryTime { get; set; }

        public List<int> IngredientIds { get; set; }

        // Aggiungi una proprietà per la lista degli ingredienti
        public List<Ingredient> Ingredients { get; set; }
    }
}
