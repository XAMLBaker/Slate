using DryIoc;
using System;

namespace FlexMVVM
{
    public static class DryIocExtensions
    {
        public static void UseInstance(this IContainer container, Type serviceType, object instance)
        {
            // 이미 등록된 서비스가 있으면 무시하거나 덮어쓰기 로직 필요하면 추가 가능
            if (!container.IsRegistered (serviceType))
                container.RegisterInstance (serviceType, instance);
        }
    }
}
