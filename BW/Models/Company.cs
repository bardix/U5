using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [Column(TypeName = "char(10)")]
        public string? PhoneCompany {  get; set; }

        [StringLength(80)]
        public string? Address { get; set; }

    }
}
