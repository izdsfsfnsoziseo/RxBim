namespace RxBim.AutocadTestFramework.Console
{
    using Abstractions;
    using CoreWCF;
    using CoreWCF.Channels;
    using CoreWCF.Configuration;
    using CoreWCF.Description;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Services;

    /// <summary>
    /// run
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Warning));
            services.AddServiceModelServices();
            services.AddServiceModelMetadata();
            services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<Service>();
                serviceBuilder.AddServiceEndpoint<Service, IService>(
                    new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service.svc");
                var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
                serviceMetadataBehavior.HttpsGetEnabled = true;
            });
        }
    }
}