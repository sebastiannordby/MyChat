using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyChat.Domain.Common.UnitOfWork;
using MyChat.Domain.Services;
using MyChat.Domain.Services.Interfaces;
using MyChat.Infrastructure.Common;
using MyChat.Infrastructure.Repositories.Interfaces;
using MyChat.Infrastructure.Repositories.SQL;
using MyChat.Infrastructure.UnitOfWork;
using MyChat.Rest.Authentication;
using MyChat.Rest.Installers;
using StructureMap;

namespace MyChat.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["connectionString"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(options => { });

            services.AddStructureMap();
            services.AddControllers();
            services.AddSingleton<IApiKeyRepository>(new SQLApiKeyRepository(connectionString));
            services.AddSingleton(new UnitOfWorkOptions(connectionString));
            services.AddScoped(typeof(IUnitOfWork), typeof(SQLUnitOfWork));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IDomainEventDispatcher), typeof(DomainEventDispatcher));
            services.ConfigureStructureMap(); // StructureMapInstaller.cs
            services.ConfigureEmailOptions(Configuration); // EmailOptionsInstaller.cs
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
