﻿namespace RxBim.Application.Ribbon.Autocad.Services.AddElementStrategies
{
    using Abstractions;
    using Autodesk.Windows;
    using Models;
    using Models.Configurations;
    using Ribbon.Abstractions;
    using Ribbon.Abstractions.ConfigurationBuilders;

    /// <summary>
    /// Implementation of <see cref="IAddElementStrategy"/> for separator.
    /// </summary>
    public class SeparatorStrategy : ElementStrategyBase<PanelLayoutElement>
    {
        private readonly IPanelService _panelService;

        /// <inheritdoc />
        public SeparatorStrategy(IPanelService panelService)
        {
            _panelService = panelService;
        }

        /// <inheritdoc />
        public override bool IsApplicable(IRibbonPanelElement config)
        {
            return base.IsApplicable(config) &&
                   ((PanelLayoutElement)config).LayoutElementType == PanelLayoutElementType.Separator;
        }

        /// <inheritdoc />
        protected override void CreateAndAddElement(
            RibbonTab ribbonTab,
            RibbonPanel ribbonPanel,
            PanelLayoutElement elementConfig)
        {
            _panelService.AddSeparator(ribbonPanel);
        }

        /// <inheritdoc />
        protected override RibbonItem CreateElementForStack(PanelLayoutElement elementConfig, RibbonItemSize size)
        {
            return CantBeStackedStub(elementConfig);
        }
    }
}