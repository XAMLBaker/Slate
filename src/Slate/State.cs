using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Slate
{
    public class State<T>: INotifyPropertyChanged
    {
        private T _value;
        private Func<T, T, Task<bool>> _beforeChangeAsync;
        private Func<T, T, Task> _afterChangeAsync;
        public async void Set(T newValue)
        {
            if (EqualityComparer<T>.Default.Equals (_value, newValue))
                return;

            if (_beforeChangeAsync != null)
            {
                bool allowChange = await _beforeChangeAsync (_value, newValue);
                if (!allowChange)
                    return;
            }
            var oldValue = _value;  // 여기서 저장
            _value = newValue;
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (nameof (Value)));
            if (_afterChangeAsync != null)
                await _afterChangeAsync (oldValue, newValue);
        }

        public T Value
        {
            get => _value;
            set => Set (value); // 강제로 async 경로로 이동
        }

        public void OnBeforeChangeAsync(Func<T, T, Task<bool>> handler)
            => _beforeChangeAsync = handler;
        public void OnAfterChangeAsync(Func<T, T, Task> handler)
            => _afterChangeAsync = handler;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));

        public State(T initialValue)
        {
            _value = initialValue;
        }

    }
}
