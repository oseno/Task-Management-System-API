using OsenoTaskManagementSystem.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using System.Text;

namespace OsenoTaskManagementSystem.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly MongoDBService _mongoDBService;
        public UserService(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
            _userCollection = mongoDBService.GetUserCollection();
        }
        public async System.Threading.Tasks.Task RegisterAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return;
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _userCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
