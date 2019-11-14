using MyChat.DTO;
using Microsoft.AspNetCore.Mvc;
using MyChat.Domain.Services.Interfaces;

namespace MyChat.Rest.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public IActionResult Add(UserDto user)
            => ResultTry(() => _service.Add(user), "Successfully added user");

        [HttpGet("GetUserByUserName/{username}")]
        public IActionResult GetUserByUsername(string username)
            => Result(_service.GetUserByUserName(username), "Successfully executed GetUserByUserName.");

        [HttpGet("IsUserNameAvailable/{username}")]
        public IActionResult IsUserNameAvailable(string username)
            =>  Result(_service.IsUserNameAvailable(username), "Successfully executed IsUserNameAvailable.");

        [HttpGet("IsValidLogin/{username}/{password}")]
        public IActionResult IsValidLogin(string username, string password)
            => Result(_service.IsValidLogin(username, password), "Successfully executed IsValidLogin.");

        [HttpGet("GetUsersByKeyword/{keyword}")]
        public IActionResult GetUsersByKeyword(string keyword)
            => Result(_service.GetUsersByKeyword(keyword), "Successfully retrieved users by keyword.");
    }
}
