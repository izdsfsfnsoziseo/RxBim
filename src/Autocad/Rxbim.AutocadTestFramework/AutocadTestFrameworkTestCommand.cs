namespace RxBim.AutocadTestFramework
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using Autodesk.AutoCAD.EditorInput;
    using Command.Autocad;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using NUnit.Framework.Api;
    using NUnit.Framework.Interfaces;
    using NUnit.Framework.Internal;
    using Shared;

    /// <inheritdoc />
    public class AutocadTestFrameworkTestCommand : RxBimCommand
    {
        private ITestListener _testListener = null!;
        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="editor"><see cref="Editor"/> instance.</param>
        /// <param name="testListener"><see cref="ITestListener"/></param>
        [RxBimCommandClass("AutocadTestFrameworkTestCommand")]
        [PublicAPI]
        public PluginResult ExecuteCommand(Editor editor, ITestListener testListener)
        {
            _testListener = testListener;
            try
            {
                Debugger.Launch();
                var assembly = GetTestAssemblyPath(editor);
                var workingDirectory = new FileInfo(assembly).Directory!.FullName;
                var result = RunTests(assembly);
                SaveResults(result, workingDirectory + "\\results.json");
                return PluginResult.Succeeded;
            }
            catch (Exception e)
            {
                return PluginResult.Failed;
            }
        }

        private static void SaveResults(ITestResult result, string resultsPath)
        {
            var output = JsonConvert.SerializeObject(result, settings: new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) => { args.ErrorContext.Handled = true; }
            });
            File.WriteAllText(resultsPath, output);
        }

        private static string GetTestAssemblyPath(Editor editor)
        {
            var pStrOpts = new PromptStringOptions("\nEnter assembly path: ")
            {
                AllowSpaces = false,
            };
            var pStrRes = editor.GetString(pStrOpts);
            return pStrRes.StringResult;
        }

        private ITestResult RunTests(string assemblyPath)
        {
            var assemblyBuilder = new DefaultTestAssemblyBuilder();
            var runner = new NUnitTestAssemblyRunner(assemblyBuilder);
            runner.Load(assemblyPath, new Dictionary<string, object>());
            var result = runner.Run(_testListener, TestFilter.Empty);
            return result;
        }
    }
}