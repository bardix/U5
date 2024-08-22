using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Drawer
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Locker Locker { get; set; }
        public List<DrawerProduct> Product { get; set; } = [];
    }
}
