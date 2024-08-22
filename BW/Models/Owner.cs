using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Owner
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30)]
        public required string FirstName { get; set; }
        
        [StringLength(50)]
        public required string LastName { get; set; }

        [Column(TypeName = "char(10)")]
        public string? NumberPhone { get; set; }

        [EmailAddress]
        [StringLength(80)]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "char(16)")]
        public required string FiscalCode { get; set; }

        public List<Animal> Animals { get; set; } = new List<Animal>();

    //    public List<Sell> Sells { get; set; } = new List<Sell>();


    }
}
