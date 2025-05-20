using System;
using System.Collections.Generic;

namespace FlexMVVM.Modularity
{
    public interface IModuleCatalog
    {
        List<IModule> GetModules();
        IModuleCatalog AddModule<T>();
        IModuleCatalog AddModules(List<IModule> modules);
    }

    public class ModuleCatalog<T> : IModuleCatalog where T : IModule
    {
        private List<IModule> _modules;

        public ModuleCatalog()
        {
            _modules = new List<IModule>();
        }

        public IModuleCatalog AddModule<T>()
        {
            var module = (IModule)Activator.CreateInstance (typeof (T));

            _modules.Add (module);

            return this;
        }

        public IModuleCatalog AddModules(List<IModule> modules)
        {
            if (modules.Count == 0)
                return this;

            this._modules.AddRange (modules);

            return this;
        }

        public List<IModule> GetModules() => this._modules;
    }
}
