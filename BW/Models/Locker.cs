using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Locker
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "char(10)")]
        public required string NumberLocker {  get; set; }
       // public List<Drawer> Drawer { get; set; } = [];
    }
}
