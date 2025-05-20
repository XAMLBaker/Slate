using System;

namespace FlexMVVM.Modularity
{
    public interface IModule
    {
        void Register(IContainerRegistry containerRegistry);
        void Initialize(IServiceProvider containerProvider);
    }
}
