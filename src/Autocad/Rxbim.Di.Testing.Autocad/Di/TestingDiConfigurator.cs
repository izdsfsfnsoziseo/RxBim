﻿namespace RxBim.Di.Testing.Autocad.Di
{
    using Autodesk.AutoCAD.ApplicationServices.Core;
    using RxBim.Di;
    using Shared;

    /// <inheritdoc />
    public class TestingDiConfigurator : DiConfigurator<ITestConfiguration>
    {
        /// <inheritdoc />
        public TestingDiConfigurator()
        {
        }

        /// <inheritdoc />
        protected override void ConfigureBaseDependencies()
        {
            Container
                .AddInstance(Application.DocumentManager.MdiActiveDocument)
                .AddInstance(Application.DocumentManager.MdiActiveDocument.Database)
                .AddInstance(Application.DocumentManager.MdiActiveDocument.Editor);
        }
    }
}