using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyChat.DTO.Common;
using System;

namespace MyChat.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaseController : Controller
    {
        public IActionResult Result(object data, string message = "Request succeessful.")
        {
            return Ok(new RequestResult() { 
                Data = data,
                Message = message,
                IsSucceeded = true
            });
        }

        public IActionResult ResultTry(Func<object> execute, string message = "Request succeessful.")
        {

            try
            {
                var data = execute();

                return Ok(new RequestResult()
                {
                    Data = data,
                    Message = message,
                    IsSucceeded = true
                });
            }
            catch(Exception e)
            {
                return BadRequest(new RequestResult()
                {
                    Data = null,
                    Message = e.Message,
                    IsSucceeded = false
                });
            }
        }
    }
}