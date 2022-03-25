﻿namespace RxBim.Transactions.Extensions
{
    using Abstractions;
    using Castle.DynamicProxy;
    using Di;
    using Di.Exceptions;

    /// <summary>
    /// Extensions for <see cref="IContainer"/>
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Tries add transaction proxy functionality
        /// </summary>
        /// <param name="container">DI container</param>
        public static IContainer TrySetupProxy(this IContainer container)
        {
            if (container is ITransactionProxyProvider proxyProvider)
            {
                proxyProvider.SetupContainer();
            }
            else
            {
                throw new RegistrationException("Current container configuration doesn't " +
                                                "implement transaction proxy functionality!");
            }

            return container.AddTransactionInterceptor();
        }

        /// <summary>
        /// Adds transaction interceptor into DI container
        /// </summary>
        /// <param name="container">DI container</param>
        private static IContainer AddTransactionInterceptor(this IContainer container)
        {
            return container.AddTransient<IInterceptor, TransactionInterceptor>();
        }
    }
}