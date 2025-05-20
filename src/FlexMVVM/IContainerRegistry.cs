using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlexMVVM
{
    public interface IContainerRegistry
    {
        IContainerRegistry RegisterWindow<T>();
        IContainerRegistry RegisterWindow<T>(Func<T> window);
        IContainerRegistry RegisterLayout<T>();
        IContainerRegistry RegisterLayout<T>(Func<T> layout);
        IContainerRegistry RegisterComponent<T>();
        IContainerRegistry RegisterComponent<T>(Func<T> component);
        IServiceCollection Services { get; }
    }
}
