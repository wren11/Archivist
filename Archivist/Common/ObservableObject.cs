using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Archivist.Common
{
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected ObservableObject() { }
        
        protected virtual bool SetProperty<T>(ref T field, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null,
            Action<T> onChanging = null)
        {
            // If the current value and new value are the same, do nothing
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            // Notify that the property is about to change
            onChanging?.Invoke(value);
            OnPropertyChanging(propertyName);

            field = value;

            // Notify that the property has changed
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        #region INotifyPropertyChanged Methods
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region INotifyPropertyChanging Methods
        public void NotifyPropertyChanging([CallerMemberName] string propertyName = "")
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            OnPropertyChanging(propertyName);
        }

        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
        #endregion
    }
}
