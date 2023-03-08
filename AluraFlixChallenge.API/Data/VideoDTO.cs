using System.ComponentModel.DataAnnotations;

namespace AluraFlixChallenge.API.Data
{
    public class VideoDTO
    {
        [Required(ErrorMessage = "The ID field is required")]
        public long Id 
        {
            get
            {
                return Id;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("id field must be bigger than zero");

                Id = value;
            }
        }

        [Required(ErrorMessage = "The Title field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Url field is required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "The Url field is required")]
        public string Description { get; set; }
    }
}
