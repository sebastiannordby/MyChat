using Newtonsoft.Json;
using System;
using System.Text;
using System.Net.Http;
using MyChat.Classes.Exceptions;

namespace MyChat.Classes.Rest
{
    public static class PostRequest
    {
        public static string Send(string url, object data)
        {
            try
            {
                var client = new HttpClient(RestService.ByPassSSLHandler()).ConfigureAuthentication();
                var jsonObject = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                var stringResult = result.Content?.ReadAsStringAsync().Result;

                return stringResult;
            }
            catch (Exception)
            {
                throw new APIException("Seems like API is out of service. Please contact a system administrator.");
            }
        }

        public static RequestResult<T> SendPostResult<T>(string url, object data)
        {
            var resultString = Send(url, data);

            if (!string.IsNullOrWhiteSpace(resultString))
            {
                return JsonConvert.DeserializeObject<RequestResult<T>>(resultString);
            }

            return null;
        }
    }
}
