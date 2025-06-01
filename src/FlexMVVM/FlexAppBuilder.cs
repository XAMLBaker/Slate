using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlexMVVM
{
    public sealed class FlexAppBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IContainerRegistry _containerRegistry;
        private readonly IModuleCatalog _moduleCatalog;
        public FlexAppBuilder()
        {
        }

        public FlexAppBuilder(IContainerRegistry containerRegistry, IModuleCatalog moduleCatalog)
        {
            this._containerRegistry = containerRegistry;
            this._moduleCatalog = moduleCatalog;
        }

        public IContainerRegistry ContainerRegistry => this._containerRegistry;
        public IServiceCollection Services => this._containerRegistry.Services;

        public void ModuleRegister()
        {
            foreach (var module in this._moduleCatalog.GetModules())
            {
                module.Register (this._containerRegistry);
            }
        }

        public void AddModule<T>()
        {
            this._moduleCatalog.AddModule<T> ();
        }

        public void ModuleInitialize(IServiceProvider serviceProvider)
        {
            foreach (var module in this._moduleCatalog.GetModules())
            {
                module.Initialize (serviceProvider);
            }
        }

        public IModuleCatalog ModuleCatalog() => this._moduleCatalog;
    }
}
