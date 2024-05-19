using OsenoTaskManagementSystem.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;

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
    public IMongoCollection<Models.Task> GetTaskCollection()
    {
        return _taskCollection;
    }
    public IMongoCollection<User> GetUserCollection()
    {
        return _userCollection;
    }
    
}