using DryIoc;
using System;
using System.Linq;

namespace FlexMVVM
{
    public static class ViewModelFactory
    {
        public static object ResolveOrCreate(Type vmType, ReuseOption reuse)
        {
            var container = RegisterProvider.Container;

            if (reuse == ReuseOption.Singleton)
            {
                if (!container.IsRegistered (vmType))
                {
                    var ctor = vmType.GetConstructors ().FirstOrDefault (c => c.GetParameters ().Length == 0);
                    if (ctor != null)
                    {
                        var instance = Activator.CreateInstance (vmType);
                        container.UseInstance (vmType, instance);
                    }
                    else
                    {
                        throw new InvalidOperationException ($"'{vmType.Name}'은(는) 등록되어 있지 않으며 기본 생성자도 없습니다.");
                    }
                }

                return container.Resolve (vmType);
            }
            else
            {
                // Transient으로 자동 등록 후 Resolve
                if (!container.IsRegistered (vmType))
                    container.Register (vmType); // 기본 생성자 기반
                return container.Resolve (vmType);
            }
        }

        public static T ResolveOrCreate<T>(ReuseOption reuse)
            => (T)ResolveOrCreate (typeof (T), reuse);
    }

}
