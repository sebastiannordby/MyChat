using Microsoft.Extensions.DependencyInjection;
using MyChat.Domain.Common;
using StructureMap;

namespace MyChat.Rest.Installers
{
    public static class StructureMapInstaller
    {
        public static void ConfigureStructureMap(this IServiceCollection services)
        {
            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup));
                    _.AssemblyContainingType(typeof(Entity));
                    _.Assembly("MyChat.Infrastructure");
                    _.WithDefaultConventions();
                    _.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                });

                config.Populate(services);
            });

            services.AddSingleton<IContainer>(container);
        }
    }
}
