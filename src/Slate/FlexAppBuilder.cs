using Microsoft.Extensions.DependencyInjection;

namespace Slate
{
    internal class SlateAppBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IContainerRegistry _containerRegistry;
        private readonly IModuleCatalog _moduleCatalog;
        public SlateAppBuilder()
        {
            this._containerRegistry = new ContainerRegistry ();
            this._moduleCatalog = new ModuleCatalog ();
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
