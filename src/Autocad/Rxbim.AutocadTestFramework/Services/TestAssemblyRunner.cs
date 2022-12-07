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

/*/// <summary>
        /// asdasd
        /// </summary>
        public void DoSomething()
        {
            // Create a temporary application domain to load the assembly.
            var tempDomain = AppDomain.CreateDomain("temp_Domain");

            // найти тесты в сборке
            // NOTE: We use reflection only load here so that we don't have to resolve all binaries
            // This is an assumption by Dynamo tests which reference assemblies that can be resolved 
            // at runtime inside Revit.
            var assembly = Assembly.ReflectionOnlyLoadFrom(AssemplyPath);
            foreach (var fixtureType in assembly.GetTypes()
                         .Where(ft => ft.GetCustomAttributes()
                             .OfType<TestFixtureAttribute>()
                             .Any()))
            {
                foreach (var test in fixtureType.GetMethods())
                {
                    if (!test.GetCustomAttributes().OfType<TestAttribute>().Any())
                    {
                        continue;
                    }

                    string absoluteDrawingPath;
                    var drawingPath = test.GetCustomAttributes(typeof(TestDrawingAttribute))
                        .Cast<TestDrawingAttribute>()
                        .FirstOrDefault()?.Path;

                    if (drawingPath is null)
                    {
                        throw new Exception("TestDrawingAttribute error");
                    }

                    if (Path.IsPathRooted(drawingPath))
                    {
                        absoluteDrawingPath = drawingPath;
                    }
                    else
                    {
                        if (WorkingDirectory == null)
                        {
                            // If the working directory is not specified.
                            // Add the relative path to the assembly's path.
                            absoluteDrawingPath = Path.GetFullPath(
                                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
                                    drawingPath));
                        }
                        else
                        {
                            absoluteDrawingPath = Path.GetFullPath(Path.Combine(WorkingDirectory, drawingPath));
                        }
                    }
                }
            }

            AppDomain.Unload(tempDomain);
        }*/