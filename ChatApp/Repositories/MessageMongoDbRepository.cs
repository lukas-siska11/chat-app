using System.Linq.Expressions;
using ChatApp.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ChatApp.Repositories;

public class MessageMongoDbRepository : MongoDbRepository<Message>, IMessageRepository
{
    public MessageMongoDbRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
    
    public new async Task<IReadOnlyCollection<Message>> GetAllAsync(Expression<Func<Message, bool>> filter)
    {
        var bsonData = await DbCollection.Aggregate()
            .Match(filter)
            .Lookup("Users", "UserId", "_id", "User")
            .Unwind("User", new AggregateUnwindOptions<Message>() { PreserveNullAndEmptyArrays = true })
            .ToListAsync();

        return BsonSerializer.Deserialize<List<Message>>(bsonData.ToJson());
    }

    public new async Task<Message?> GetAsync(Guid id)
    {
        var collection = await GetAllAsync(message => message.Id == id);
        return collection.FirstOrDefault();
    }
}