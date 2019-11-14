using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyChat.Classes;
using MyChat.DTO;
using MyChat.ViewModels.Account;

namespace MyChat.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(LoginViewModel model)
        {
            var isValidLogin = RestService.User.IsValidLogin(model.Username, model.Password);

            if (isValidLogin)
            {
                var user = RestService.User.GetByUsername(model.Username);
                var token = TokenIssuer.IssueToken(user);

                HttpContext.Session.SetString("JWToken", token);

                return Ok(true);
            }

            return BadRequest();    
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] UserDto userDto)
        {
            var result = RestService.User.AddUser(userDto);

            if (result.IsSucceeded)
            {
                return Ok(true);
            }

            return BadRequest(result.Message);
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("JWToken");

            return RedirectToAction("Index", "Home");
        }
    }
}