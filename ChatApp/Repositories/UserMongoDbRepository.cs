using ChatApp.Entities;
using MongoDB.Driver;

namespace ChatApp.Repositories;

public class UserMongoDbRepository : MongoDbRepository<User>, IUserRepository
{
    public UserMongoDbRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
}