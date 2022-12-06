namespace RxBim.Example.Autocad.IntegrationTests;

using System;
using System.Reflection;
using System.Threading;
using Autodesk.AutoCAD.EditorInput;
using Di;
using Di.Testing.Autocad;
using Di.Testing.Autocad.Di;
using NUnit.Framework;
using Exception = Autodesk.AutoCAD.BoundaryRepresentation.Exception;

[TestFixture]
public class Tests
{
    private IContainer _container = null!;

    [SetUp]
    public void Setup()
    {
        var testingDiConfigurator = new TestingDiConfigurator();
        testingDiConfigurator.Configure(Assembly.GetExecutingAssembly());
        _container = testingDiConfigurator.Container;
    }

    [Test]
    [TestDrawing("./drawing.rvt")]
    public void FirstTest()
    {
    }

    [Test]
    [TestDrawing("./drawing.rvt")]
    public void FailureTest()
    {
        throw new Exception();
    }

    [Test]
    [TestDrawing("./drawing.rvt")]
    public void ConsoleTest()
    {
        Console.WriteLine("Any text");
    }

    [Test]
    [TestDrawing("./drawing.rvt")]
    public void EditorWriteMessage()
    {
    }
}