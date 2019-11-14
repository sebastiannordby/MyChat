using MyChat.Classes.Rest;
using Microsoft.AspNetCore.Mvc;
using MyChat.Classes;

namespace MyChat.Controllers
{
    [Route("Rest")]
    public class RestController : Controller
    {
        [HttpGet("Get")]
        public IActionResult Get([FromQuery] string endpoint)
        {
            return Ok(GetRequest.Send($"{SystemVariables.RestAPI}/{endpoint}"));
        }

        [HttpPost("Post")]
        public IActionResult Post([FromQuery] string endpoint, [FromBody] object data)
        {
            return Ok(PostRequest.Send($"{SystemVariables.RestAPI}/{endpoint}", data));
        }
    }
}