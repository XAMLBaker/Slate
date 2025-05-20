using FlexMVVM.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

        public void AddModules(List<IModule> modules)
        {
            this._moduleCatalog.AddModules (modules);
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
