namespace RxBim.AutocadTestFramework.Services
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using NUnit.Framework.Api;
    using NUnit.Framework.Interfaces;

    /// <inheritdoc />
    public class TestAssemblyRunner : ITestAssemblyRunner
    {
        /// <inheritdoc />
        public ITest LoadedTest { get; }

        /// <inheritdoc />
        public ITestResult Result { get; }

        /// <inheritdoc />
        public bool IsTestLoaded { get; }

        /// <inheritdoc />
        public bool IsTestRunning { get; }

        /// <inheritdoc />
        public bool IsTestComplete { get; }

        /// <inheritdoc />
        public ITest Load(string assemblyName, IDictionary<string, object> settings)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ITest Load(Assembly assembly, IDictionary<string, object> settings)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int CountTestCases(ITestFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ITest ExploreTests(ITestFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ITestResult Run(ITestListener listener, ITestFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RunAsync(ITestListener listener, ITestFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool WaitForCompletion(int timeout)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void StopRun(bool force)
        {
            throw new NotImplementedException();
        }
    }
}