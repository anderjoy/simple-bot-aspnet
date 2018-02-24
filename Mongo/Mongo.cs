using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBot.Mongo
{
    public class FiapMongo
    {
        private static MongoClient _client;
        private static IMongoDatabase _db;
        IMongoCollection<BsonDocument> _history;

        public FiapMongo()
        {
            _client = new MongoClient();

            _db = _client.GetDatabase("Bot");

            _history = _db.GetCollection<BsonDocument>("History");
        }

        public IEnumerable<BsonDocument> FindAll()
        {
            return _db.GetCollection<BsonDocument>("History").Find(new FilterDefinitionBuilder<BsonDocument>().Empty).ToList();
        }

        public void InsertMessage(Message message)
        {
            var _message = new BsonDocument()
            {
                {"User", message.User },
                {"Message,", message.Text }
            };

            _history.InsertOne(_message);
        }
    }
}