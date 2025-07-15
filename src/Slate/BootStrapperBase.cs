using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Slate.Navigation;
using System;

namespace Slate
{
    public class BootStrapperBase
    {
        protected IServiceCollection Services { get; set; }
        protected readonly IModuleCatalog _moduleCatalog;
        protected readonly Register _register;
        protected readonly ViewModelMapper _viewModelMapper;
        protected readonly IComponentRegistry _componentRegistry;
        public BootStrapperBase()
        {
            this._moduleCatalog = new ModuleCatalog ();
            this._viewModelMapper = new ViewModelMapper ();
            this._componentRegistry = new ComponentRegistry ();
            _register = new Register ();
            RegisterProvider.SetRegister (this._register);
        }

        public BootStrapperBase StartLayout<T>()
        {
            if (_register.InitialLayout != null)
                throw new InvalidOperationException ("초기 Layout은 이미 설정되었습니다.");
            _register.InitialLayout = typeof (T);

            Type type = typeof (T);
            string _key = type.FullName;

            RegisterProvider.AddRegister (_key, type);
            return this;
        }
        protected virtual void RegisterComponent(IComponentRegistry component) { }
        protected virtual void Register(IServiceCollection services) { }
        protected virtual void ViewModelMapper(IViewModelMapper modelMapper) { }
        protected virtual void Initialize(IServiceProvider serviceProvider) { }
        public virtual void Run()
        {

        }

        protected virtual IContainer CreateContainer()
        {
            var container = new Container ();
            container.WithDependencyInjectionAdapter (this.Services);

            foreach (var module in this._moduleCatalog.GetModules ())
            {
                module.Initialize (container);
            }
            Initialize (container);
            container.BuildServiceProvider ();
            return container;
        }

        public BootStrapperBase AddModule<T>()
        {
            this._moduleCatalog.AddModule<T> ();
            return this;
        }

        protected virtual void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IViewModelMapper> (_viewModelMapper);
            foreach (var module in this._moduleCatalog.GetModules ())
            {
                module.Register (services);
                module.RegisterComponent (this._componentRegistry);
            }

            this.Register (services);
            RegisterComponent (this._componentRegistry);


            foreach (var register in this._register.RegisterMap)
            {
                services.TryAddSingleton (register.Value);
            }
        }

        protected void ViewModelMapperLoad()
        {
            this.ViewModelMapper (this._viewModelMapper);

            foreach (var module in this._moduleCatalog.GetModules ())
            {
                module.ViewModelMapper (_viewModelMapper);
            }
        }
    }
}
