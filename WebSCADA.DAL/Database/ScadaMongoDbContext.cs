using MongoDB.Driver;
using WebSCADA.DAL.Entities;

namespace WebSCADA.DAL.Database
{
    public class ScadaMongoDbContext
    {
        private readonly IMongoDatabase _db;

        public ScadaMongoDbContext(string ConnectionString, string DatabaseName)
        {
            var client = new MongoClient(ConnectionString);
            _db = client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<Schema> Schema => _db.GetCollection<Schema>("schema");

        public IMongoCollection<Log> Log => _db.GetCollection<Log>("logs");

        public IMongoCollection<User> User => _db.GetCollection<User>("users");

        public IMongoCollection<Role> Role => _db.GetCollection<Role>("roles");
    }
}
