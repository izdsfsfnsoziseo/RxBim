/*namespace Rxbim.AutocadTestFramework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using NUnit.Engine;
    using NUnit.Framework;
    using NUnit.Framework.Api;
    using NUnit.Framework.Interfaces;
    using NUnit.Framework.Internal;
    using RxBim.Di.Testing.Autocad;
    using TestFilter = NUnit.Framework.Internal.TestFilter;

    /// <summary>
    /// sadas
    /// </summary>
    public class MyTestRunner
    {
        /// <summary>
        /// as
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// asdasd
        /// </summary>
        public string AssemplyPath { get; set; }

        /// <summary>
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
        }

        /// <summary>
        /// asdasdas
        /// </summary>
        /// <param name="assemblyPath"></param>
        public ITestResult MscSAdas(string assemblyPath)
        {
            var w = new DefaultTestAssemblyBuilder();
            var runner = new NUnitTestAssemblyRunner(w);
            runner.Load(assemblyPath, new Dictionary<string, object>());
            var result = runner.Run(TestListener.NULL, TestFilter.Empty);
            return result;
        }
    }
}*/