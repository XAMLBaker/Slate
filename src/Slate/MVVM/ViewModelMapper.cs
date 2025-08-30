using System;
using System.Collections.Generic;

namespace Slate
{
    public interface IViewModelMapper
    {
        void Register<TView, TViewModel>();
        Type GetViewModel(Type viewType);
    }

    public class ViewModelMapper : IViewModelMapper
    {
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type> ();
        public void Register<TView, TViewModel>()
        {
            _map[typeof (TView)] = typeof (TViewModel);
        }
        public Type GetViewModel(Type viewType)
        {
            return _map.TryGetValue (viewType, out var tuple) ? tuple : null;
        }
    }
}
