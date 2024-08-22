using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Sell
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Owner? Owner { get; set; }

        [Required]
        public Product? Product { get; set; }

        [Column(TypeName = "char(10)")]
        public string? NumberOfRecipe {  get; set; }

        [Required]  
        public DateTime DateSell { get; set; }
    }
}
