using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Slate
{
    public class ContainerRegistry : IContainerRegistry
    {
        private readonly IServiceCollection _services;

        public ContainerRegistry()
        {
            _services = new ServiceCollection ();
            // Lazy-load the ConfigurationManager, so it isn't created if it is never used.
            // Don't capture the 'this' variable in AddSingleton, so MauiAppBuilder can be GC'd.
            var configuration = new Lazy<ConfigurationManager> (() => new ConfigurationManager ());
            _services.AddSingleton<IConfiguration> (sp => configuration.Value);
        }
        public ContainerRegistry(IServiceCollection services)
        {
            this._services = services;
            // Lazy-load the ConfigurationManager, so it isn't created if it is never used.
            // Don't capture the 'this' variable in AddSingleton, so MauiAppBuilder can be GC'd.
            var configuration = new Lazy<ConfigurationManager> (() => new ConfigurationManager ());
            _services.AddSingleton<IConfiguration> (sp => configuration.Value);
        }

        public IContainerRegistry RegisterWindow<T>()
        {
            Type type = typeof (T);
            string _key = type.FullName;

            RegisterProvider.AddRegister (_key, type);
            return this;
        }

        public IContainerRegistry RegisterWindow<T>(Func<T> window)
        {
            string _key = window.GetType ().FullName;

            RegisterProvider.AddRegister (_key, window.GetType ());
            return this;
        }

        public IContainerRegistry RegisterComponent<T>()
        {
            Type type = typeof (T);
            string _key = type.FullName;

            RegisterProvider.AddRegister (_key, type);

            return this;
        }

        public IContainerRegistry RegisterComponent<T>(Func<T> component)
        {
            string _key = component.GetType ().FullName;

            RegisterProvider.AddRegister (_key, component.GetType ());
            return this;
        }

        public IServiceCollection Services => this._services;
    }
}
