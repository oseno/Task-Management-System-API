using OsenoTaskManagementSystem.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OsenoTaskManagementSystem.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<Models.Task> _taskCollection;
        private readonly MongoDBService _mongoDBService;
        private readonly IHubContext<TasksHub> _hubContext;
        public TaskService(MongoDBService mongoDBService, IHubContext<TasksHub> hubContext)
        {
            _mongoDBService = mongoDBService;
            _taskCollection = mongoDBService.GetTaskCollection();
            _hubContext = hubContext;
        }
        public async Task<List<Models.Task>> GetAllTasksAsync()
        {
            return await _taskCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async System.Threading.Tasks.Task CreateTaskAsync(Models.Task task)
        {
            await _taskCollection.InsertOneAsync(task);
            await _hubContext.Clients.All.SendAsync("TaskCreated", task);
            return;
        }
        public async System.Threading.Tasks.Task UpdateTaskAsync(Models.Task updatedTask)
        {
            FilterDefinition<Models.Task> filter = Builders<Models.Task>.Filter.Eq("Id", updatedTask.Id);
            await _taskCollection.ReplaceOneAsync(filter, updatedTask);
            await _hubContext.Clients.All.SendAsync("TaskUpdated", updatedTask);
            return;
        }
        public async System.Threading.Tasks.Task DeleteTaskAsync(string id)
        {
            FilterDefinition<Models.Task> filter = Builders<Models.Task>.Filter.Eq("Id", id);
            await _taskCollection.DeleteOneAsync(filter);
            await _hubContext.Clients.All.SendAsync("TaskDeleted", id);
            return;
        }
    }
}
