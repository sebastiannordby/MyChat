using System;
using System.Net.Http;
using MyChat.Classes.Rest;
using MyChat.DTO;

namespace MyChat.Classes
{
    public class RestService
    {
        public class User
        {
            public static RequestResult<UserDto> AddUser(UserDto user)
            {
                return PostRequest.SendPostResult<UserDto>(
                    $"{SystemVariables.RestAPI}/api/Users/Add", user);
            }

            public static bool IsValidLogin(string username, string password)
            {
                var requestResult = 
                    GetRequest.SendGetResult<bool>($"{SystemVariables.RestAPI}/api/Users/IsValidLogin/{username}/{password}");

                return requestResult.IsSucceeded ? requestResult.Data : false;
            }

            internal static UserDto GetByUsername(string username)
            {
                var requestResult =
                    GetRequest.SendGetResult<UserDto>($"{SystemVariables.RestAPI}/api/Users/GetUserByUserName/{username}");

                return requestResult?.Data;
            }
        }

        public static HttpClientHandler ByPassSSLHandler()
        {
            HttpClientHandler clientHandler = new HttpClientHandler(); // Bypass SSL
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // Bypass SSL

            return clientHandler;
        }
    }

    public class RequestResult<T>
    {
        public bool IsSucceeded { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
