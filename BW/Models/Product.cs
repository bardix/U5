using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string TypeOfProduct { get; set; }

        [StringLength(50)]
        public string TypeOfUse { get; set; }

        public Company? Company { get; set; } 

      //  public List<Sell> Sells { get; set; } = new List<Sell>();

        public List<DrawerProduct> Drawer { get; set; }
    }
}
