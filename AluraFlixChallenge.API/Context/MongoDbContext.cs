using AluraFlixChallenge.API.Entities;
using MongoDB.Driver;



namespace AluraFlixChallenge.API.Context
{
    public class MongoDbContext
    {
        public static string ConnectionString { get; set; }
        public static string Database { get; set; }
        private IMongoDatabase _mongoDatabase { get; }

        public MongoDbContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromConnectionString(ConnectionString);
                MongoClient mongoClient = new(settings);

                _mongoDatabase = mongoClient.GetDatabase(Database);
            }
            catch(Exception ex)
            {
                throw new Exception($"Couldn't connect to server: {ex.Message}");
            }
        }

        public IMongoCollection<Video> Videos 
        {
            get
            {
                return _mongoDatabase.GetCollection<Video>("Video");
            }
        }

    }
}
