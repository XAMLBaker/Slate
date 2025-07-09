using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Slate
{
    public class SlateFluent
    {
        private readonly IContainerRegistry _containerRegistry;
        private readonly IModuleCatalog _moduleCatalog;
        private readonly Register _register;
        private readonly ViewModelMapper _viewModelMapper;

        public IModuleCatalog ModuleCatalog => this._moduleCatalog;
        public IServiceCollection Services => this._containerRegistry.Services;
        public IContainerRegistry ContainerRegistry => this._containerRegistry;

        public IViewModelMapper ViewModelMapper => this._viewModelMapper;

        public SlateFluent()
        {
            this._containerRegistry = new ContainerRegistry ();
            this._moduleCatalog = new ModuleCatalog ();
            this._viewModelMapper = new ViewModelMapper ();
            this.Services.AddSingleton<IViewModelMapper> (_viewModelMapper);

            _register = new Register ();
            RegisterProvider.SetRegister (this._register);
        }


        public void Window<T>()
        {
            _register.RegisterMap["SlateFrameworkWindow"] = typeof (T);
        }

        public void StartLayout<T>()
        {
            if (_register.InitialLayout != null)
                throw new InvalidOperationException ("초기 Layout은 이미 설정되었습니다.");
            _register.InitialLayout = typeof (T);
        }

        public virtual void Run()
        {

        }
        protected void Init()
        {
            if (this._register.InitialLayout == null)
                throw new InvalidOperationException (
                    "초기 Layout이 설정되지 않았습니다. Slate.StartWithLayout<T>()를 Render() 안에서 반드시 호출하세요."
                );

            IContainer container = this.CreateContainer ();
            RegisterProvider.SetContainer (container);
        }

        private IContainer CreateContainer()
        {
            foreach (var module in this._moduleCatalog.GetModules ())
            {
                module.Register (this._containerRegistry);
            }

            foreach (var register in this._register.RegisterMap)
            {
                this.Services.TryAddSingleton (register.Value);
            }
            var container = new Container ();
            container.WithDependencyInjectionAdapter (this.Services);

            foreach (var module in this.ModuleCatalog.GetModules ())
            {
                module.Initialize (container);
            }
            foreach (var module in this.ModuleCatalog.GetModules ())
            {
                module.ViewModelMapper (_viewModelMapper);
            }
            container.BuildServiceProvider ();
            return container;
        }
    }
}
