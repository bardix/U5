using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Recover
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required Animal Animal { get; set; }

        public required DateTime DateRecover { get; set; }

        public string? Image {  get; set; }

        public bool IsActive { get; set; }
    }
}
