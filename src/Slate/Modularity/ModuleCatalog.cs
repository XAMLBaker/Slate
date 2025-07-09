using System;
using System.Collections.Generic;

namespace Slate
{
    public class ModuleCatalog : IModuleCatalog
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

        public List<IModule> GetModules() => this._modules;
    }
}
