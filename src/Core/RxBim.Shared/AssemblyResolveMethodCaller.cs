﻿namespace RxBim.Shared
{
    using Di;
    using Microsoft.Extensions.DependencyInjection;

    /// <inheritdoc />
    public class AssemblyResolveMethodCaller : MethodCallerDecorator<PluginResult>
    {
        private readonly AssemblyResolver _resolver;

        /// <inheritdoc />
        public AssemblyResolveMethodCaller(IMethodCaller<PluginResult> decorated, AssemblyResolver resolver)
            : base(decorated)
        {
            _resolver = resolver;
        }

        /// <inheritdoc />
        public override PluginResult InvokeMethod(IServiceCollection services, string methodName)
        {
            var result = Decorated.InvokeMethod(services, methodName);

            if (methodName == Constants.ShutdownMethodName)
            {
                _resolver.Dispose();
            }

            return result;
        }
    }
}