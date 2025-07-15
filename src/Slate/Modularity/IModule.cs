using Microsoft.Extensions.DependencyInjection;
using Slate.Navigation;
using System;

namespace Slate
{
    public interface IModule
    {
        void RegisterComponent(IComponentRegistry componentRegistry);
        void Register(IServiceCollection services);
        void Initialize(IServiceProvider containerProvider);
        void ViewModelMapper(IViewModelMapper modelMapper);
    }
}
