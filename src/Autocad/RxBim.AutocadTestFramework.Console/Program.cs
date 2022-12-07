// See https://aka.ms/new-console-template for more information

using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RxBim.AutocadTestFramework;
using RxBim.AutocadTestFramework.Console;
using RxBim.Example.Autocad.IntegrationTests;
using RxBim.ScriptUtils.Autocad;
using RxBim.ScriptUtils.Autocad.Extensions;

Console.WriteLine("Hello, World!");

CreateWebHostBuilder(args).Build().RunAsync();
var runner = new AutocadScriptRunner
{
    UseConsole = true
};
runner.Run(builder => builder
    .SetStartMode(false)
    .SetSecureLoad(false)
    .NetLoadCommand(typeof(AutocadTestFrameworkTestCommand).Assembly.Location)
    .AddCommand("AutocadTestFrameworkTestCommand")
    .AddCommand(typeof(Tests).Assembly.Location)
    .SetSecureLoad(true));

/// <summary>
/// blah blah
/// </summary>
/// <param name="args"></param>
IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseUrls("https://localhost:5000", "http://localhost:5001");