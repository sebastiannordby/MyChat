using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyChat.Controllers
{
    public class ChatRoomController : BaseController
    {
        public IActionResult Room(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                ViewBag.RoomName = name;

                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}