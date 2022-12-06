namespace RxBim.AutocadTestFramework.Services
{
    using System.Collections.Generic;
    using System.Reflection;
    using NUnit.Framework.Api;
    using NUnit.Framework.Interfaces;

    /// <inheritdoc />
    public class MyTestAssemblyBuilder : ITestAssemblyBuilder
    {
        /// <inheritdoc />
        public ITest Build(Assembly assembly, IDictionary<string, object> options)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ITest Build(string assemblyName, IDictionary<string, object> options)
        {
            throw new System.NotImplementedException();
        }
    }
}