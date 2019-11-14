using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyChat.Rest.Controllers
{
    public class DefaultContoller : ControllerBase
    {
        [HttpGet("BuildNumber")]
        public string BuildNumber()
        {
            return "MyChat API V0.1.1";
        }
    }
}
