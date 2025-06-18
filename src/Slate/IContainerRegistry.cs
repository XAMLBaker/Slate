using Microsoft.Extensions.DependencyInjection;
using System;

namespace Slate
{
    public interface IContainerRegistry
    {
        IContainerRegistry RegisterWindow<T>();
        IContainerRegistry RegisterWindow<T>(Func<T> window);
        IContainerRegistry RegisterComponent<T>();
        IContainerRegistry RegisterComponent<T>(Func<T> component);
        IServiceCollection Services { get; }
    }
}
