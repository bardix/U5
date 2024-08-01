using System.ComponentModel.DataAnnotations;

namespace EsSettimanaleU5S3.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
