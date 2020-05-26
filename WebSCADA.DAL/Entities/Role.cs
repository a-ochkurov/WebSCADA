using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebSCADA.DAL.Entities
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("role_name")]
        public string RoleName { get; set; }
    }
}
