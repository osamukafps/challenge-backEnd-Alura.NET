namespace AluraFlixChallenge.API.Entities
{
    public class Category
    {
        public long Id { get; set; }

        private string title;
        private string color;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Color
        {
            get
            {
                return title;
            }
            set
            {
                color = value;
            }
        }
    }
}
