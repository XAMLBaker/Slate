using System;
using System.Collections.Generic;

namespace FlexMVVM
{
    public interface IModuleCatalog
    {
        List<IModule> GetModules();
        IModuleCatalog AddModule<T>();
    }
}
