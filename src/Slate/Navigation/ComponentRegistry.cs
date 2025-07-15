using System;

namespace Slate.Navigation
{
    public interface IComponentRegistry
    {
        IComponentRegistry RegisterComponent<T>();
        IComponentRegistry RegisterComponent<T>(Func<T> component);
    } 

    public class ComponentRegistry : IComponentRegistry
    {
        public IComponentRegistry RegisterComponent<T>()
        {
            Type type = typeof (T);
            string _key = type.FullName;

            RegisterProvider.AddRegister (_key, type);

            return this;
        }

        public IComponentRegistry RegisterComponent<T>(Func<T> component)
        {
            string _key = component.GetType ().FullName;

            RegisterProvider.AddRegister (_key, component.GetType ());
            return this;
        }
    }
}
