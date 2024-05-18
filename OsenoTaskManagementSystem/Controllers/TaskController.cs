using Microsoft.AspNetCore.Mvc;
using OsenoTaskManagementSystem.Models;
using OsenoTaskManagementSystem.Services;

namespace OsenoTaskManagementSystem.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //do my validations here, nowhere else lol
        private readonly MongoDBService _mongoDBService;

        public TaskController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Models.Task>> GetAll()
        {
            return await _mongoDBService.GetAsync();
        }
        [HttpGet]
        public async Task<Models.Task> GetById(int id)
        {
            var task = GetAll().Result.Where(x => x.Id == id).FirstOrDefault();
            return task;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Task task)
        {
            await _mongoDBService.CreateAsync(task);
            return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddTask(string id, [FromBody] string movieId)
        {
            await _mongoDBService.AddToTasksAsync(id, movieId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }
}
