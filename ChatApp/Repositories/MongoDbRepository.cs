using System.Linq.Expressions;
using ChatApp.Entities;
using MongoDB.Driver;

namespace ChatApp.Repositories;

public class MongoDbRepository<T> : IRepository<T> where T : IEntity
{
    protected readonly IMongoCollection<T> DbCollection;
    protected readonly FilterDefinitionBuilder<T> FilterBuilder = Builders<T>.Filter;

    public MongoDbRepository(IMongoDatabase database, string collectionName)
    {
        DbCollection = database.GetCollection<T>(collectionName);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await DbCollection.Find(FilterBuilder.Empty).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await DbCollection.Find(filter).ToListAsync();
    }

    public async Task<T?> GetAsync(Guid id)
    {
        var filter = FilterBuilder.Eq(item => item.Id, id);
        return await DbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await DbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await DbCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        var filter = FilterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
        await DbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        var filter = FilterBuilder.Eq(entity => entity.Id, id);
        await DbCollection.DeleteOneAsync(filter);
    }
}