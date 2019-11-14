using Microsoft.AspNetCore.Authentication;

// Credits: Josef Ottosson
// https://josefottosson.se/asp-net-core-protect-your-api-with-api-keys/

namespace MyChat.Rest.Authentication
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public static readonly string DefaultScheme = "API Key";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
