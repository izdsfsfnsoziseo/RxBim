﻿namespace RxBim.Application.Ribbon.ElementFromConfigStrategies
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The strategy for getting a <see cref="CommandButton"/> from a configuration section.
    /// </summary>
    public class CommandButtonStrategy : SimpleElementStrategyBase<CommandButton>
    {
        /// <inheritdoc />
        public override bool IsApplicable(IConfigurationSection elementSection)
        {
            return elementSection.GetSection(nameof(CommandButton.CommandType)).Exists();
        }
    }
}