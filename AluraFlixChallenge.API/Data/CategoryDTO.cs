using System.ComponentModel.DataAnnotations;

namespace AluraFlixChallenge.API.Data
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "The ID Field is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Color Field is required")]
        public string Color { get; set; }
    }
}
