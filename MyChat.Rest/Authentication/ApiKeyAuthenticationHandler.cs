using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyChat.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

// Credits: Josef Ottosson
// https://josefottosson.se/asp-net-core-protect-your-api-with-api-keys/
// Note: Some changes done.

namespace MyChat.Rest.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly string ApiKeyHeaderName = "X-Api-Key";
        private readonly IApiKeyRepository _repository;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, 
            UrlEncoder encoder, ISystemClock clock, IApiKeyRepository repository)  : base(options, logger, encoder, clock)
        {
            _repository = repository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
            {
                return await Fail("No API Key Provided.");
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return await Fail("API Key In Header Is Empty.");
            }

            var existingApiKey = _repository.GetByKey(providedApiKey);

            if (existingApiKey != null && existingApiKey.IsValid())
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingApiKey.Owner)
                };

                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return await Fail("Invalid API Key Provided.");
        }

        protected async Task<AuthenticateResult> Fail(string message)
        {
            Context.Response.StatusCode = 401;
            await Context.Response.WriteAsync(message);
            return AuthenticateResult.Fail("Invalid API Key Provided.");
        }
    }
}
