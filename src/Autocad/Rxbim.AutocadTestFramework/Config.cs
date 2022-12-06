namespace RxBim.AutocadTestFramework
{
    using Di;
    using NUnit.Framework.Interfaces;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container.AddSingleton<ITestListener, TestListener>();
        }
    }
}