using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class VisitViewModel
    {

        [StringLength(50)]
        [Required(ErrorMessage = "Inserisci il tipo di esame svolto.")]
        public required string TypeOfExam { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Inserisci il tipo di cura che l'animale dovrà seguire.")]
        public required string TypeOfCure { get; set; }
    }
}
