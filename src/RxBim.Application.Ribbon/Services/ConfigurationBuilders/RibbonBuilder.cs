﻿namespace RxBim.Application.Ribbon.Services.ConfigurationBuilders
{
    using Abstractions.ConfigurationBuilders;
    using Microsoft.Extensions.Configuration;
    using Models.Configurations;

    /// <summary>
    /// Represents a ribbon builder.
    /// </summary>
    public class RibbonBuilder : IRibbonBuilder
    {
        /// <summary>
        /// Building ribbon.
        /// </summary>
        public Ribbon Ribbon { get; } = new();

        /// <inheritdoc />
        public ITabBuilder AddTab(string title)
        {
            return AddTabInternal(title);
        }

        /// <inheritdoc />
        public IRibbonBuilder SetAddVersionToCommandTooltip(bool enable)
        {
            Ribbon.AddVersionToCommandTooltip = enable;
            return this;
        }

        /// <inheritdoc />
        public IRibbonBuilder SetCommandTooltipVersionHeader(string prefix)
        {
            Ribbon.CommandTooltipVersionHeader = prefix;
            return this;
        }

        /// <summary>
        /// Loads a ribbon menu from configuration.
        /// </summary>
        /// <param name="config">Configuration.</param>
        internal void LoadFromConfig(IConfiguration config)
        {
            SetProperties(config.GetSection(nameof(Ribbon)));

            var tabs = config.GetSection(nameof(Ribbon)).GetSection(nameof(Ribbon.Tabs));
            if (!tabs.Exists())
                return;

            foreach (var tabSection in tabs.GetChildren())
            {
                if (!tabSection.Exists())
                    continue;
                var tabBuilder = AddTabInternal(tabSection.GetSection(nameof(Tab.Name)).Value);
                tabBuilder.LoadFromConfig(tabSection);
            }
        }

        private void SetProperties(IConfiguration config)
        {
            var versionSection = config.GetSection(nameof(Ribbon.AddVersionToCommandTooltip));
            if (versionSection.Exists())
            {
                Ribbon.AddVersionToCommandTooltip = versionSection.Get<bool>();
            }

            var headerSection = config.GetSection(nameof(Ribbon.CommandTooltipVersionHeader));
            if (headerSection.Exists())
            {
                Ribbon.CommandTooltipVersionHeader = headerSection.Value;
            }
        }

        private TabBuilder AddTabInternal(string tabTitle)
        {
            var builder = new TabBuilder(tabTitle, this);
            Ribbon.Tabs.Add(builder.BuildingTab);
            return builder;
        }
    }
}