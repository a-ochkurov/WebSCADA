using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebSCADA.DAL.Entities
{
    public class Schema
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("data")]
        public string Data { get; set; }
    }
}
