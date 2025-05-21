using System;
using System.ComponentModel;

namespace FlexMVVM
{
    public class State<T>: INotifyPropertyChanged
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (!Equals (_value, value))
                {
                    _value = value;
                    OnPropertyChanged (nameof(Value));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));

        public State(T initialValue)
        {
            _value = initialValue;
        }

    }
}
