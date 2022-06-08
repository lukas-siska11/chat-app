using ChatApp.Entities;
using ChatApp.Repositories;
using ChatApp.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace ChatApp.Extensions;

public static class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            return mongoClient.GetDatabase(mongoDbSettings.Database);
        });

        return services;
    }

    public static IServiceCollection AddUserMongoRepository(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository>(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return new UserMongoDbRepository(database, "Users");
        });

        return services;
    }
    
    public static IServiceCollection AddMessageMongoRepository(this IServiceCollection services)
    {
        services.AddSingleton<IMessageRepository>(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return new MessageMongoDbRepository(database, "Messages");
        });

        return services;
    }
    
    public static IServiceCollection AddChatRoomMongoRepository(this IServiceCollection services)
    {
        services.AddSingleton<IChatRoomRepository>(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return new ChatRoomMongoDbRepository(database, "ChatRooms");
        });

        return services;
    }
}