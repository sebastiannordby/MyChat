using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.DTO.Common
{
    public class RequestResult
    {
        public bool IsSucceeded { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
