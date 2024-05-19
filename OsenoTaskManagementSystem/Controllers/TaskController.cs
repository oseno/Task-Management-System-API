using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OsenoTaskManagementSystem.Models;
using OsenoTaskManagementSystem.Services;

namespace OsenoTaskManagementSystem.Controllers
{
    [Authorize]
    [Controller]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Models.Task>> GetAll()
        {
            return await _taskService.GetAllTasksAsync();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var task = GetAll().Result.Where(x => x.Id == id).FirstOrDefault();
            if(task == null) { return NotFound(); }
            return Ok(task);
        }

        [HttpPost]
        [Route("CreateTask")]
        public async Task<IActionResult> CreateTask(AddTaskModel newTask)
        {
            if ((!string.IsNullOrEmpty(newTask.Title)) && !string.IsNullOrEmpty(newTask.Description))
            {
                if (newTask.Title.Length > 50 || newTask.Title.Any(char.IsSymbol))
                {
                    return BadRequest("Task Title cannot contain contain symbols and cannot be greater than 50 characters");
                }
                if (newTask.Description.Length > 200 || newTask.Description.Any(char.IsSymbol))
                {
                    return BadRequest("Task Description cannot contain contain symbols and cannot be greater than 200 characters");
                }
            }
            else
            {
                return BadRequest("Task title or description cannot be empty");
            }

            Models.Task task = new Models.Task
            {
                Title = newTask.Title.Trim(),
                Description = newTask.Description.Trim(),
                DateCreated = DateTime.Now,
                IsCompleted = false
            };
            await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
        }

        [HttpPost]
        [Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask(UpdateTaskModel updatedTask)
        {
            var task = GetAll().Result.Where(x => x.Id == updatedTask.Id).FirstOrDefault();
            if (task == null)
            {
                return BadRequest("Task does not exist.");
            }
            if (!string.IsNullOrEmpty(updatedTask.Title))
            {
                if (updatedTask.Title.Length > 50 || updatedTask.Title.Any(char.IsSymbol))
                {
                    return BadRequest("Task Title cannot contain contain symbols and cannot be greater than 50 characters");
                }
                task.Title = updatedTask.Title.Trim();
            }
            if (!string.IsNullOrEmpty(updatedTask.Description))
            {
                if (updatedTask.Description.Length > 200 || updatedTask.Description.Any(char.IsSymbol))
                {
                    return BadRequest("Task Description cannot contain contain symbols and cannot be greater than 200 characters");
                }
                task.Description = updatedTask.Description.Trim();
            }
            
            task.IsCompleted = updatedTask.IsCompleted;

            await _taskService.UpdateTaskAsync(task);
            return Ok(task);
        }

        [HttpPost]
        [Route("DeleteTask")]
        public async Task<IActionResult> Delete(string id)
        {
            await _taskService.DeleteTaskAsync(id);
            return Ok("Task deleted succesfully.");
        }

    }
}
