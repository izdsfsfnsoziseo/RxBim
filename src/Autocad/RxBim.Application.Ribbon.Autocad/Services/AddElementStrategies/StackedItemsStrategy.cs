﻿namespace RxBim.Application.Ribbon.Services.AddElementStrategies
{
    using System.Linq;
    using Autodesk.Windows;
    using ConfigurationBuilders;
    using Shared.Abstractions;

    /// <summary>
    /// Implementation of <see cref="IAddElementStrategy"/> for stacked items.
    /// </summary>
    public class StackedItemsStrategy : ElementStrategyBase<StackedItems>
    {
        private readonly IStrategyFactory<IAddElementStrategy> _strategyFactory;
        private readonly IPanelService _panelService;

        /// <inheritdoc />
        public StackedItemsStrategy(IStrategyFactory<IAddElementStrategy> strategyFactory, IPanelService panelService)
        {
            _strategyFactory = strategyFactory;
            _panelService = panelService;
        }

        /// <inheritdoc />
        protected override void CreateAndAddElement(RibbonPanel ribbonPanel, StackedItems stackedItems)
        {
            var stackSize = stackedItems.StackedButtons.Count;
            var stackedItemsRow = new RibbonRowPanel();
            var small = stackSize == StackedItemsBuilder.MaxStackSize;

            var strategies = _strategyFactory.GetStrategies().ToList();

            _panelService.AddItem(ribbonPanel, stackedItemsRow);

            for (var i = 0; i < stackSize; i++)
            {
                if (i > 0)
                    stackedItemsRow.Items.Add(new RibbonRowBreak());

                var buttonConfig = stackedItems.StackedButtons[i];

                var addElementStrategy = strategies.FirstOrDefault(x => x.IsApplicable(buttonConfig));
                if (addElementStrategy is null)
                    continue;

                var element = (RibbonItem)addElementStrategy.CreateElementForStack(buttonConfig, small);
                stackedItemsRow.Items.Add(element);
            }
        }

        /// <inheritdoc />
        protected override RibbonItem CreateElementForStack(StackedItems elementConfig, RibbonItemSize size)
        {
            return CantBeStackedStub(elementConfig);
        }
    }
}