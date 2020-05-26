using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebSCADA.DAL.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("login")]
        public string Login { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("roleId")]
        public string RoleId { get; set; }
    }
}
