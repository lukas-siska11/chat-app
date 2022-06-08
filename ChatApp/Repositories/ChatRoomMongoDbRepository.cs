using ChatApp.Entities;
using MongoDB.Driver;

namespace ChatApp.Repositories;

public class ChatRoomMongoDbRepository : MongoDbRepository<ChatRoom>, IChatRoomRepository
{
    public ChatRoomMongoDbRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
}