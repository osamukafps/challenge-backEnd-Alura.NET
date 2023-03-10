using System.ComponentModel.DataAnnotations;

namespace AluraFlixChallenge.API.Data
{
    public class VideoDTO
    {
        [Required(ErrorMessage = "The ID field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The field Id has to be bigger than zero")]
        public long Id { get; set; }

        [Required(ErrorMessage = "The Title field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Url field is required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "The Url field is required")]
        public string Description { get; set; }
    }
}
