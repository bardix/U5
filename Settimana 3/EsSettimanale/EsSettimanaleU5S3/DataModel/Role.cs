using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EsSettimanaleU5S3.DataModel
{
    public class Role
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
    }

}
