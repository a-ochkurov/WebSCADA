using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebSCADA.DAL.Entities
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }
    }
}
