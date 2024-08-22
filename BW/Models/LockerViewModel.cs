using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BW2_Team6.Models
{
    public class LockerViewModel
    {
        public int? Id { get; set; }

        [Required]
        public required string NumberLocker { get; set; }
        public List<Drawer> Drawer { get; set; } = [];
    }
}
