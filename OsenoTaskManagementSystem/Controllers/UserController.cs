using Microsoft.AspNetCore.Mvc;
using OsenoTaskManagementSystem.Models;
using OsenoTaskManagementSystem.Services;

namespace OsenoTaskManagementSystem.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public UserController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        //register and login, thats all
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User task)
        {
            
        }

        
    }
}
