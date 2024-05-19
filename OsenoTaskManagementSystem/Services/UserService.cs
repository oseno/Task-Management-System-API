using OsenoTaskManagementSystem.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;

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
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }
        public string ComputeHash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
