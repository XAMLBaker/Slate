using System;
using System.Collections.Generic;

namespace FlexMVVM.Modularity
{
    public interface IModuleCatalog
    {
        List<IModule> GetModules();
        IModuleCatalog AddModule<T>();
    }
}
