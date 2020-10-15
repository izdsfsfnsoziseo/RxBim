﻿using PikTools.Shared.Ui.Abstractions;
using PikTools.Shared.Ui.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace PikTools.WpfStyles.Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly INotificationService _notificationService;

        public MainWindow()
        {
            var config = new DiConfig();
            config.Configure(GetType().Assembly);
            _notificationService = config.Container.GetInstance<INotificationService>();
            
            InitializeComponent();

            var table = new List<TableRowData> 
            {
                new TableRowData { Name = "Ползователь 1", Role = "Роль 1", Access = "Права 1"},
                new TableRowData { Name = "Ползователь 2", Role = "Роль 2", Access = "Права 2"},
                new TableRowData { Name = "Ползователь 3", Role = "Роль 3", Access = "Права 3"},
                new TableRowData { Name = "Ползователь 4", Role = "Роль 4", Access = "Права 4"},
                new TableRowData { Name = "Ползователь 5", Role = "Роль 5", Access = "Права 5"}
            };

            dg.ItemsSource = table;
        }

        private void EditableTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2
                && sender is EditableTextBlock tb)
                tb.IsInEditMode = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _notificationService.ShowMessage("Заголовок окна", "Текст сообщения. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста."
                + " Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста."
                + " Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста."
                + " Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста."
                + " Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста."
                + " Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста."
                + " Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста."
                + " Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много текста.");
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            _notificationService.ShowMessage("Ошибка!", "Извините, произошла ошибка :(", NotificationType.Error);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            _notificationService.ShowMessage("Выполнено", "Все успешно выполнено!", NotificationType.Success);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            _notificationService.ShowConfirmMessage("Вопрос", "Вы уверены в своем решении?");
        }
    }
}
