using System;
using System.Collections.Generic;

namespace Slate
{
    public interface IViewModelMapper
    {
        void Register<TView, TViewModel>(ReuseOption reuse);
        (Type vmType, ReuseOption reuse)? GetViewModel(Type viewType);
    }

    public class ViewModelMapper : IViewModelMapper
    {
        private readonly Dictionary<Type, (Type vmType, ReuseOption reuse)> _map = new Dictionary<Type, (Type vmType, ReuseOption reuse)> ();
        public void Register<TView, TViewModel>(ReuseOption reuse)
        {
            _map[typeof (TView)] = (typeof (TViewModel), reuse);
        }
        public (Type vmType, ReuseOption reuse)? GetViewModel(Type viewType)
        {
            return _map.TryGetValue (viewType, out var tuple) ? tuple : (ValueTuple<Type, ReuseOption>?)null;
        }
    }
}
