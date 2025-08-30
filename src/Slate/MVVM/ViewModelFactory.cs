using DryIoc;
using System;

namespace Slate
{
    public static class ViewModelFactory
    {
        public static object ResolveOrCreate(Type vmType)
        {
            var container = RegisterProvider.Container;

            return container.Resolve (vmType);
        }
    }
}
