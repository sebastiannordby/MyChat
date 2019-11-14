using Microsoft.AspNetCore.Authentication;
using System;

// Credits: Josef Ottosson
// https://josefottosson.se/asp-net-core-protect-your-api-with-api-keys/

namespace MyChat.Rest.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKeySupport(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthenticationOptions> options)
        {
            return authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, options);
        }
    }
}
