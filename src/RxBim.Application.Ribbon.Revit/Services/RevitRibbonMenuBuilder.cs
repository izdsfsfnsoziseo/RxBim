﻿namespace RxBim.Application.Ribbon.Revit.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Controls;
    using Abstractions;
    using Autodesk.Private.Windows;
    using Autodesk.Revit.UI;
    using Autodesk.Windows;
    using GalaSoft.MvvmLight.CommandWpf;
    using Models.Configurations;
    using Ribbon.Services;
    using UIFramework;
    using Button = Models.Configurations.Button;
    using RibbonButton = Autodesk.Windows.RibbonButton;
    using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
    using TaskDialog = Autodesk.Revit.UI.TaskDialog;

    /// <summary>
    /// Implementation of <see cref="IRibbonMenuBuilder"/> for Revit
    /// </summary>
    public class RevitRibbonMenuBuilder : RibbonMenuBuilderBase<string, RibbonPanel>
    {
        private readonly UIControlledApplication _application;

        /// <inheritdoc />
        public RevitRibbonMenuBuilder(UIControlledApplication application, Assembly menuAssembly)
            : base(menuAssembly)
        {
            _application = application;
        }

        /// <inheritdoc />
        protected override bool CheckRibbonCondition()
        {
            return RevitRibbonControl.RibbonControl != null;
        }

        /// <inheritdoc />
        protected override string GetOrCreateTab(string tabName)
        {
            var existsTab =
                RevitRibbonControl.RibbonControl.Tabs.FirstOrDefault(t =>
                    t.Title.Equals(tabName, StringComparison.OrdinalIgnoreCase));
            if (existsTab != null)
            {
                return existsTab.Title;
            }

            _application.CreateRibbonTab(tabName);
            return tabName;
        }

        /// <inheritdoc />
        protected override RibbonPanel GetOrCreatePanel(string tabName, string panelName)
        {
            var existsPanel = _application.GetRibbonPanels(tabName)
                .FirstOrDefault(x => x.Title.Equals(panelName, StringComparison.OrdinalIgnoreCase));

            var panel = existsPanel ?? _application.CreateRibbonPanel(tabName, panelName);
            panel.Title = panelName;

            return panel;
        }

        /// <inheritdoc />
        protected override void CreateAboutButton(string tabName, RibbonPanel panel, AboutButton aboutButtonConfig)
        {
            CheckButtonName(aboutButtonConfig);
            var button = new RibbonButton
            {
                Name = aboutButtonConfig.Name,
                Image = GetIconImage(aboutButtonConfig.SmallImage),
                LargeImage = GetIconImage(aboutButtonConfig.LargeImage),
                GroupLocation = RibbonItemGroupLocation.Middle,
                IsToolTipEnabled = true,
                ShowImage = true,
                ShowText = true,
                Text = aboutButtonConfig.Text,
                ToolTip = aboutButtonConfig.ToolTip,
                Size = RibbonItemSize.Large,
                Orientation = Orientation.Vertical,
                CommandHandler = new RelayCommand(() =>
                    {
                        if (!TryShowAboutWindow(aboutButtonConfig.Content))
                            TaskDialog.Show(aboutButtonConfig.Name, aboutButtonConfig.Content.ToString());
                    },
                    true)
            };

            ComponentManager.Ribbon?
                .Tabs.FirstOrDefault(x => x.Title.Equals(tabName, StringComparison.OrdinalIgnoreCase))
                ?
                .Panels.FirstOrDefault(x => x.Source.Title.Equals(panel.Name))
                ?
                .Source.Items.Add(button);
        }

        /// <inheritdoc />
        protected override void CreateCommandButton(RibbonPanel panel, CommandButton cmdButtonConfig)
        {
            var pushButtonData = CreateCommandButtonData(cmdButtonConfig);
            panel.AddItem(pushButtonData);
        }

        /// <inheritdoc />
        protected override void CreatePullDownButton(RibbonPanel panel, PullDownButton pullDownButtonConfig)
        {
            CheckButtonName(pullDownButtonConfig);
            var pulldownButtonData = new PulldownButtonData(
                pullDownButtonConfig.Name,
                pullDownButtonConfig.Text ?? pullDownButtonConfig.Name);
            SetButtonProperties(pulldownButtonData, pullDownButtonConfig);
            SetTooltip(pulldownButtonData, pullDownButtonConfig.ToolTip);
            var pulldownButton = (PulldownButton)panel.AddItem(pulldownButtonData);

            foreach (var cmdButtonConfig in pullDownButtonConfig.CommandButtonsList)
            {
                var pushButtonData = CreateCommandButtonData(cmdButtonConfig);
                pulldownButton.AddPushButton(pushButtonData);
            }
        }

        /// <inheritdoc />
        protected override void AddSeparator(RibbonPanel panel)
        {
            panel.AddSeparator();
        }

        /// <inheritdoc />
        protected override void AddSlideOut(RibbonPanel panel)
        {
            panel.AddSlideOut();
        }

        /// <inheritdoc />
        protected override void CreateStackedItems(RibbonPanel panel, StackedItems stackedItems)
        {
            if (!stackedItems.StackedButtons.All(x => x is CommandButton))
                throw new InvalidOperationException("The stack can only contain command buttons!");

            var button1 = CreateCommandButtonData((CommandButton)stackedItems.StackedButtons[0]);
            var button2 = CreateCommandButtonData((CommandButton)stackedItems.StackedButtons[1]);

            switch (stackedItems.StackedButtons.Count)
            {
                case 2:
                    panel.AddStackedItems(button1, button2);
                    break;
                case 3:
                    var button3 = CreateCommandButtonData((CommandButton)stackedItems.StackedButtons[2]);
                    panel.AddStackedItems(button1, button2, button3);
                    break;
                default:
                    throw new InvalidOperationException("The stack size can only be 2 or 3!");
            }
        }

        private void SetButtonProperties(ButtonData buttonData, Button buttonConfig)
        {
            if (buttonConfig.Description != null)
                buttonData.LongDescription = buttonConfig.Description;
            if (buttonConfig.HelpUrl != null)
                buttonData.SetContextualHelp(new ContextualHelp(ContextualHelpType.Url, buttonConfig.HelpUrl));
            buttonData.Image = GetIconImage(buttonConfig.SmallImage);
            buttonData.LargeImage = GetIconImage(buttonConfig.LargeImage);
        }

        private void SetTooltip(RibbonItemData buttonData, string tooltip)
        {
            if (tooltip != null)
                buttonData.ToolTip = tooltip;
        }

        private PushButtonData CreateCommandButtonData(CommandButton cmdButtonConfig)
        {
            CheckButtonName(cmdButtonConfig);
            if (string.IsNullOrWhiteSpace(cmdButtonConfig.CommandType))
                throw new ArgumentException($"Command type not found! Button: {cmdButtonConfig.Name}");
            var cmdType = GetCommandType(cmdButtonConfig.CommandType!);
            var className = cmdType.FullName;
            var assemblyLocation = cmdType.Assembly.Location;
            var pushButtonData =
                new PushButtonData(
                    cmdButtonConfig.Name,
                    cmdButtonConfig.Text ?? cmdButtonConfig.Name,
                    assemblyLocation,
                    className)
                {
                    AvailabilityClassName = className
                };
            SetButtonProperties(pushButtonData, cmdButtonConfig);
            SetTooltip(pushButtonData, GetTooltipContent(cmdButtonConfig, cmdType));
            return pushButtonData;
        }

        private void CheckButtonName(Button buttonConfig)
        {
            if (string.IsNullOrWhiteSpace(buttonConfig.Name))
                throw new ArgumentException($"Button name not found!");
        }
    }
}