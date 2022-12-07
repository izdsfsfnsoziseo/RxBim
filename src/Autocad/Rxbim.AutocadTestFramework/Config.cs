namespace RxBim.AutocadTestFramework
{
    using Di;
    using JetBrains.Annotations;
    using NUnit.Framework.Interfaces;
    using Service;
    using Services;

    /// <inheritdoc />
    [UsedImplicitly]
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container.AddSingleton<ITestListener, TestListener>();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;
            container.AddInstance((IService)new ServiceClient(ServiceClient.EndpointConfiguration.BasicHttpBinding_IService, "https://localhost:5000/Service.svc"));
        }
    }
}