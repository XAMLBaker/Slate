using System.Collections.Generic;

namespace Slate
{
    public interface IModuleCatalog
    {
        List<IModule> GetModules();
        IModuleCatalog AddModule<T>();
    }
}
