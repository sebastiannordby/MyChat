using MyChat.Infrastructure.Models;

namespace MyChat.Infrastructure.Repositories.Interfaces
{
    public interface IApiKeyRepository
    {
        void Add(ApiKey apiKey);
        ApiKey GetByKey(string apiKey);
    }
}
