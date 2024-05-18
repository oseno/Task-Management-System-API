using OsenoTaskManagementSystem.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;

namespace OsenoTaskManagementSystem.Services;

public class MongoDBService
{

    private readonly IMongoCollection<Models.Task> _taskCollection;
    private readonly IMongoCollection<User> _userCollection;
    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _taskCollection = database.GetCollection<Models.Task>(mongoDBSettings.Value.CollectionName);
        _userCollection = database.GetCollection<User>(mongoDBSettings.Value.UserCollection);
    }

    //login and register methods
    public async Task<List<Models.Task>> GetAsync()
    {
        return await _taskCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async System.Threading.Tasks.Task CreateAsync(Models.Task task)
    {
        await _taskCollection.InsertOneAsync(task);
        return;
    }
    public async System.Threading.Tasks.Task AddToTasksAsync(string id, string movieId)
    {
        FilterDefinition<Models.Task> filter = Builders<Models.Task>.Filter.Eq("Id", id);
        UpdateDefinition<Models.Task> update = Builders<Models.Task>.Update.AddToSet<string>("movieIds", movieId);
        await _taskCollection.UpdateOneAsync(filter, update);
        return;
    }
    public async System.Threading.Tasks.Task DeleteAsync(string id)
    {
        FilterDefinition<Models.Task> filter = Builders<Models.Task>.Filter.Eq("Id", id);
        await _taskCollection.DeleteOneAsync(filter);
        return;
    }

}