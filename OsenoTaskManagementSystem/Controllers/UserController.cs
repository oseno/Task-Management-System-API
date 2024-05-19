using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OsenoTaskManagementSystem.Models;
using OsenoTaskManagementSystem.Services;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace OsenoTaskManagementSystem.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<User>> GetAll()
        {
            return await _userService.GetAllUsersAsync();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(AddUserModel newUser)
        {
            //validate username and password
            if ((!string.IsNullOrEmpty(newUser.Username)) && !string.IsNullOrEmpty(newUser.Password))
            {
                if(newUser.Username.Length < 3 || newUser.Username.Length > 20 || newUser.Username.Any(char.IsWhiteSpace) || newUser.Username.Any(char.IsSymbol)) 
                {
                    return BadRequest("Username cannot contain spaces or symbols and cannot" +
                        " be less than 3 or greater than 20 characters");
                }
                if (newUser.Password.Length < 6 || newUser.Password.Length > 20 || newUser.Password.Contains(newUser.Username) || !newUser.Password.Any(char.IsDigit) || !newUser.Password.Any(char.IsLower) || !newUser.Password.Any(char.IsUpper))
                {
                    return BadRequest("Password must have an uppercase letter, a lowercase letter, a number," +
                        "cannot be less than 3 or greater than 20 characters and cannot contain username.");
                }
                //hash password
                newUser.Password = new AuthService().ComputeHash(newUser.Password);
            }
            else
            {
                return BadRequest("Username or password cannot be empty");
            }

            User user = new User
            {
                Username = newUser.Username,
                Password= newUser.Password,
                DateCreated= DateTime.Now
            };
            await _userService.RegisterAsync(user);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(AddUserModel existingUser)
        {
            var user = await _userService.GetUserByUserNameAsync(existingUser.Username);
            if (user != null)
            {
                //confirm password
                var pwd = new AuthService().ComputeHash(existingUser.Password);
                if(pwd != user.Password)
                {
                    return BadRequest("Wrong Password.");
                }
                return Ok("Login Successful");
            }
            else
            {
                // Login failed
                return Unauthorized();
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("GenerateToken")]
        public IActionResult GenerateToken([FromBody] UserTokenModel request)
        {
            //confirm that user exists
            var user = _userService.GetUserByIdAsync(request.UserId);
            if (user == null) 
            { 
                return BadRequest("User does not exist"); 
            }
            //create token for existing users
            var result = new AuthService().GenerateToken(request);
            return Content(result);
        }

    }
}
