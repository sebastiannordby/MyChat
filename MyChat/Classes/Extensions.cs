using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyChat.Classes
{
    public static class Extensions
    {
        public static string GetHostURI(this HttpRequest request)
        {
            if (request != null)
            {
                return string.Format("{0}://{1}", request.Scheme, request.Host);
            }

            return null;
        }

        public static HttpClient ConfigureAuthentication(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add("x-api-key", SystemVariables.APIKey);

            return client;
        }
    }
}
