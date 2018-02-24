using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace SimpleBot.Repositorio
{
    public class UserMongo : IUserRepo
    {
        private static MongoClient _client;
        private static IMongoDatabase _db;
        IMongoCollection<BsonDocument> _history;
        IMongoCollection<UserProfile> _profile;

        public UserMongo()
        {
            _client = new MongoClient();

            _db = _client.GetDatabase("Bot");

            _history = _db.GetCollection<BsonDocument>("History");

            _profile = _db.GetCollection<UserProfile>("Profile");
        }

        public void InsertMessage(Message message)
        {
            var _message = new BsonDocument()
            {
                {"Id", message.Id },
                {"User", message.User },
                {"Message,", message.Text }
            };

            _history.InsertOne(_message);
        }

        public UserProfile FindProfile(string Id)
        {
            UserProfile _user = _db.GetCollection<UserProfile>("Profile").Find(new FilterDefinitionBuilder<UserProfile>().Eq(x => x.Id, Id)).FirstOrDefault();

            if (_user == null)
            {
                _user = new UserProfile()
                {
                    Id = Id,
                    Visitas = 0
                };

                _profile.InsertOne(_user);
            }

            return _user;
        }

        public void UpdateProfile(string id, UserProfile profile)
        {
            var filter = Builders<UserProfile>.Filter.Eq(c => c.Id, id);
            var updateDefinition = Builders<UserProfile>.Update.Set(x => x.Visitas, profile.Visitas);

            _profile.UpdateOne(filter, updateDefinition);
        }
    }
}