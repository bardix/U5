using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class Visit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required Animal Animal { get; set; }

        public required  DateTime DateVisit {  get; set; }

        [StringLength(50)]
        public required string TypeOfExam {  get; set; }

        [StringLength(100)]
        public required string TypeOfCure { get; set; }
    }
}
