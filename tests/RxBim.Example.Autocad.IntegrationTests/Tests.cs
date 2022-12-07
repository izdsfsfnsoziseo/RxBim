namespace RxBim.Example.Autocad.IntegrationTests;

using System;
using System.Reflection;
using AutocadTestFramework;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Di;
using Di.Testing.Autocad.Di;
using FluentAssertions;
using NUnit.Framework;

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
        throw new Exception(nameof(FailureTest));
    }

    [Test]
    [TestDrawing("./drawing.rvt")]
    public void ConsoleTest()
    {
        Console.WriteLine("Any text");
    }

    [Test]
    [TestDrawing("./drawing.rvt")]
    public void DrawCircleTest()
    {
        var acCurDb = _container.GetService<Database>();
        using var acTrans = acCurDb.TransactionManager.StartTransaction();
        var acBlkTbl = (BlockTable)acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead);
        var acBlkTblRec = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
        var acCircle = new Circle();
        acCircle.SetDatabaseDefaults();
        acCircle.Center = new Point3d();
        const int radius = 5;
        acCircle.Radius = 5;
        acBlkTblRec.AppendEntity(acCircle);
        acTrans.AddNewlyCreatedDBObject(acCircle, true);
        acTrans.Commit();
        acCircle.Area.Should().BeApproximately(Math.PI * radius * radius, 1e-3, "wrong calculation of the area of the circle");
    }
}