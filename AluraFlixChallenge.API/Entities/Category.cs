using MongoDB.Bson.Serialization.Attributes;

namespace AluraFlixChallenge.API.Entities
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class Category
    {
        [BsonElement("id")]
        public long Id { get; set; }

        private string title;
        private string color;

        [BsonElement("title")]
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

        [BsonElement("color")]
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
    }
}
