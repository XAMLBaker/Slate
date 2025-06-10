using DryIoc;
using System;
using System.Linq;

namespace FlexMVVM
{
    public static class RegisterProvider
    {
        private static Register _register;
        private static IContainer _container;
        private static IServiceProvider _serviceProvider;
        public static void SetRegister(Register register)
        {
            _register = register;
        }

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static IContainer Container => _container;
        public static IServiceProvider ServiceProvider => _serviceProvider;

        public static Type GetWindow() => _register.RegisterMap["FlexFrameworkWindow"];
        public static object Window => _container.Resolve (GetWindow());
        public static Type GetDefineNestedLayout() => _register.NestedLayout;
        public static object Get<T>() => _container.Resolve<T> ();
        public static bool HasPartialKeyMatch(string url) => _register.RegisterMap.Keys.Any (x => x.Contains (url));

        public static bool IsUrlRegistered(string url) => _register.RegisterMap.ContainsKey (url);

        public static Type GetType(string _nameSpace) => _register.RegisterMap[_nameSpace];

        public static void AddRegister(string key, Type type)=> _register.RegisterMap[key] = type;
    }
}
