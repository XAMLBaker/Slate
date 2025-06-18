using Microsoft.Extensions.DependencyInjection;
using System;

namespace Slate
{
    public sealed class SlateAppBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IContainerRegistry _containerRegistry;
        private readonly IModuleCatalog _moduleCatalog;
        public SlateAppBuilder()
        {
        }

        public SlateAppBuilder(IContainerRegistry containerRegistry, IModuleCatalog moduleCatalog)
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

        public IModuleCatalog ModuleCatalog() => this._moduleCatalog;
    }
}
