using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyChat.Domain.Common;
using MyChat.Infrastructure.Common;

namespace MyChat.Rest.Installers
{
    public static class EmailOptionsInstaller
    {
        public static void ConfigureEmailOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var emailOptions = new EmailOptions() 
            { 
                SmptServer = configuration["EmailOptions.SmptServer"],
                UserName = configuration["EmailOptions.UserName"],
                Password = configuration["EmailOptions.Password"],
                EmailAddress = configuration["EmailOptions.EmailAddress"]
            };

            services.AddSingleton<IEmailOptions>(emailOptions);
        }
    }
}
