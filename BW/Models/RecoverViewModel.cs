namespace BW2_Team6.Models
{
    public class RecoverViewModel
    {
        public string? ExistingImagePath { get; set; }
        public IFormFile Image { get; set; }

        public bool IsActive { get; set; }
    }
}
