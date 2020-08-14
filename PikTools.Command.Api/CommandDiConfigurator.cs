﻿namespace PikTools.Command.Api
{
    using Autodesk.Revit.UI;
    using Di;

    /// <summary>
    /// Конфигуратор зависимостей комманды
    /// </summary>
    public class CommandDiConfigurator : DiConfigurator
    {
        private readonly object _commandObject;
        private readonly ExternalCommandData _commandData;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="commandObject">Объект комманды</param>
        /// <param name="commandData">ExternalCommandData</param>
        public CommandDiConfigurator(object commandObject, ExternalCommandData commandData)
        {
            _commandObject = commandObject;
            _commandData = commandData;
        }

        /// <inheritdoc />
        protected override void ConfigureBaseRevitDependencies()
        {
            Container.RegisterInstance(_commandData);
            Container.RegisterInstance(_commandData.Application);
            Container.RegisterInstance(_commandData.Application.Application);
            Container.RegisterInstance(_commandData.Application.ActiveUIDocument);
            Container.RegisterInstance(_commandData.Application.ActiveUIDocument.Document);
            Container.Register<IMethodCaller<CommandResult>>(() => new MethodCaller<CommandResult>(_commandObject));
        }
    }
}