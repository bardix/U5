using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BW2_Team6.Models
{
    public class Animal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required DateTime DateRegister { get; set; } = DateTime.Now;

        [Required]
        [StringLength(30)]
        public required string Name { get; set; }

        [Required]
        [StringLength(30)]
        public required string Type { get; set; }

        [Required]
        [StringLength(30)]
        public required string Fur {  get; set; }

        public DateOnly DateBirth { get; set; }

        [Column(TypeName = "char(15)")]
        public string? Microchip { get; set; }
        public Owner? Owner { get; set; }

    }
}
