// See https://aka.ms/new-console-template for more information

using System;
using RxBim.ScriptUtils.Autocad;
using RxBim.ScriptUtils.Autocad.Extensions;

Console.WriteLine("Hello, World!");

var runner = new AutocadScriptRunner();
runner.Run(builder => builder
    .SetStartMode(false)
    .SetSecureLoad(false)
    .NetLoadCommand(typeof(RxBim.AutocadTestFramework.AutocadTestFrameworkTestCommand).Assembly.Location)
    .AddCommand("AutocadTestFrameworkTestCommand")
    .AddCommand(typeof(RxBim.Example.Autocad.IntegrationTests.Tests).Assembly.Location)
    .SetSecureLoad(true));