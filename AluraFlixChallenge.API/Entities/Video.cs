using MongoDB.Bson.Serialization.Attributes;

namespace AluraFlixChallenge.API.Entities
{

    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class Video
    {
        [BsonElement("id")]
        public long Id { get; set; }

        private string title;
        private string description;
        private string url;

        [BsonElement("title")]
        public string Title 
        { 
            get { return title; } 
            set { title = value; } 
        }

        [BsonElement("description")]
        public string Description 
        {
            get { return description; } 
            set { description = value; }
        }

        [BsonElement("url")]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
    }
}
