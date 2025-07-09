namespace Slate
{
    public class BootStrapperBase
    {
        protected SlateFluent _fluent;
        public BootStrapperBase()
        {

        }

        public BootStrapperBase Window<T>()
        {
            this._fluent.Window<T> ();
            return this;
        }

        public BootStrapperBase StartLayout<T>()
        {
            this._fluent.StartLayout<T> ();
            return this;
        }

        protected virtual void Register(IContainerRegistry containerRegistry) { }
        protected virtual void ModuleContext(IModuleCatalog moduleCatalog) { }
        protected virtual void ViewModelMapper(IViewModelMapper modelMapper) { }

        public virtual void Run()
        {
            this.ModuleContext (this._fluent.ModuleCatalog);
            this.Register (this._fluent.ContainerRegistry);
            this.ViewModelMapper (this._fluent.ViewModelMapper);
            _fluent.Run ();
        }
    }
}
