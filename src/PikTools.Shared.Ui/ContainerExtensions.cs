﻿namespace PikTools.Shared.Ui
{
    using Abstractions;
    using Di;
    using PikTools.Shared.Abstractions;
    using Services;
    using ViewModels;

    /// <summary>
    /// Расширения для контейнера
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Добавляет сервисы UI в контейнер
        /// </summary>
        /// <param name="container">контейнер</param>
        public static void AddUi(this IContainer container)
        {
            container.AddSingleton<IUIDispatcher, UIDispatcher>()
                .AddSingleton<IExternalDialogs, ExternalDialogsService>()
                .AddSingleton<INotificationService, NotificationService>()
                .AddSingleton<IAboutBox, AboutDialogService>();
            container.AddInstance<INotificationViewModel>(new NotificationViewModel());
        }
    }
}