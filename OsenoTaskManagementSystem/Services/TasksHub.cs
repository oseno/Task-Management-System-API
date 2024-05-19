using Microsoft.AspNetCore.SignalR;

namespace OsenoTaskManagementSystem.Services
{
    public class TasksHub : Hub
    {
        public async Task TaskCreated(Task task)
        {
            await Clients.All.SendAsync("TaskCreated", task);
        }
        public async Task TaskUpdated(Task task)
        {
            await Clients.All.SendAsync("TaskUpdated", task);
        }
        public async Task TaskDeleted(string id)
        {
            await Clients.All.SendAsync("TaskDeleted", id);
        }
    }
}
