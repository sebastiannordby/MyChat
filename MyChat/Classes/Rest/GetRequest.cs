using MyChat.Classes.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace MyChat.Classes.Rest
{
    public static class GetRequest
    {
        public static string Send(string url)
        {
            try
            {
                var client = new HttpClient(RestService.ByPassSSLHandler()).ConfigureAuthentication();
                var result = client.GetAsync(url).Result;
                var stringResult = result.Content?.ReadAsStringAsync().Result;

                return stringResult;
            }
            catch (Exception)
            {
                throw new APIException("Seems like API is out of service. Please contact a system administrator.");
            }
        }

        public static RequestResult<T> SendGetResult<T>(string url)
        {
            var resultString = Send(url);

            if (!string.IsNullOrWhiteSpace(resultString))
            {
                return JsonConvert.DeserializeObject<RequestResult<T>>(resultString);
            }

            return null;
        }
    }
}
