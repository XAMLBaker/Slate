using DryIoc;
using System;
using System.Linq;

namespace Slate
{
    public static partial class RegisterProvider
    {
        private static Register _register;
        private static IContainer _container;
        public static void SetRegister(Register register)
        {
            _register = register;
        }

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static void SetWindow<T>()
        {
            _register.RegisterMap["SlateFrameworkWindow"] = typeof(T);
        }
    }
    public static partial class RegisterProvider
    {
        public static Type GetDefineNestedLayout => _register.InitialLayout;
        public static bool HasPartialKeyMatch(string url) => _register.RegisterMap.Keys.Any (x => x.Contains (url));
        public static bool IsUrlRegistered(string url) => _register.RegisterMap.ContainsKey (url);
        public static Type GetType(string _nameSpace) => _register.RegisterMap[_nameSpace];
        public static void AddRegister(string key, Type type) => _register.RegisterMap[key] = type;

        public static IContainer Container => _container;
        public static object Window => _container.Resolve (_register.RegisterMap["SlateFrameworkWindow"]);
        public static object Get<T>() => _container.Resolve<T> ();
    }
}
