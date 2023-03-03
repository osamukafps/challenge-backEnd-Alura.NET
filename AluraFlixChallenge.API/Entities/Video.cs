namespace AluraFlixChallenge.API.Entities
{
    public class Video
    {
        public long Id { get; set; }

        private string title;
        private string description;
        private string url;
        

        public string Title 
        { 
            get { return title; } 
            set { title = value; } 
        }

        public string Description 
        {
            get { return description; } 
            set { description = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
    }
}
