﻿namespace RxBim.Application.Ribbon.Autocad.Services.AddElementStrategies
{
    using System;
    using Autodesk.Windows;
    using Ribbon.Abstractions;
    using Ribbon.Abstractions.ConfigurationBuilders;

    /// <summary>
    /// Basic implementation of <see cref="IAddElementStrategy"/> for AutoCAD menu item.
    /// </summary>
    public abstract class ElementStrategyBase<TElement> : IAddElementStrategy
        where TElement : IRibbonPanelElement
    {
        /// <inheritdoc />
        public virtual bool IsApplicable(IRibbonPanelElement config)
        {
            return config is TElement;
        }

        /// <inheritdoc />
        public void CreateAndAddElement(object tab, object panel, IRibbonPanelElement config)
        {
            if (tab is not RibbonTab ribbonTab || panel is not RibbonPanel ribbonPanel ||
                config is not TElement elementConfig)
                return;

            CreateAndAddElement(ribbonTab, ribbonPanel, elementConfig);
        }

        /// <inheritdoc />
        public object CreateElementForStack(IRibbonPanelElement config, bool small = false)
        {
            if (config is not TElement elementConfig)
                throw new InvalidOperationException($"Invalid config type: {config.GetType().FullName}");
            var size = small ? RibbonItemSize.Standard : RibbonItemSize.Large;

            return CreateElementForStack(elementConfig, size);
        }

        /// <summary>
        /// Creates and adds to ribbon an element.
        /// </summary>
        /// <param name="ribbonTab">Ribbon tab.</param>
        /// <param name="ribbonPanel">Ribbon panel.</param>
        /// <param name="elementConfig">Ribbon item configuration.</param>
        protected abstract void CreateAndAddElement(
            RibbonTab ribbonTab,
            RibbonPanel ribbonPanel,
            TElement elementConfig);

        /// <summary>
        /// Creates and returns an element for a stack.
        /// </summary>
        /// <param name="elementConfig">Ribbon item configuration.</param>
        /// <param name="size">Item size.</param>
        protected abstract RibbonItem CreateElementForStack(TElement elementConfig, RibbonItemSize size);

        /// <summary>
        /// Stub for CreateElementForStack, if element can't be stacked.
        /// </summary>
        /// <param name="elementConfig">Ribbon item configuration.</param>
        protected RibbonItem CantBeStackedStub(TElement elementConfig)
        {
            throw new InvalidOperationException($"Can't be stacked: {elementConfig.GetType().FullName}");
        }
    }
}