using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Tipo di email non valido")]
        public required string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string? Password { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
