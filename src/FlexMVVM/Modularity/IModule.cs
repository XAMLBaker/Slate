using System;

namespace FlexMVVM
{
    public interface IModule
    {
        void Register(IContainerRegistry containerRegistry);
        void Initialize(IServiceProvider containerProvider);
    }
}
