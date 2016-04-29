using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Archivist.Common;

namespace Archivist.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, IDisposable
    {
        bool isDisposed;

        string displayName;
        bool throwOnInvalidPropertyName;

        public virtual string DisplayName
        {
            get { return displayName; }
            set
            {
                CheckIfDisposed();
                SetProperty(ref displayName, value);
            }
        }

        protected bool ThrowOnInvalidPropertyName
        {
            get { return throwOnInvalidPropertyName; }
            set { SetProperty(ref throwOnInvalidPropertyName, value); }
        }

        protected ViewModelBase() { }

        #region IDisposable Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
                return;

            if (isDisposing)
            {
                // Dispose of managed resources
            }

            // Dispose of unmanaged resources

            isDisposed = true;
        }

        [DebuggerStepThrough]
        void CheckIfDisposed()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }
        #endregion

        #region INotifyPropertyChanged Methods
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            VerifyPropertyName(propertyName);
            base.OnPropertyChanged(propertyName);
        }
        #endregion

        #region INotifyPropertyChanging Methods
        protected override void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            VerifyPropertyName(propertyName);
            base.OnPropertyChanging(propertyName);
        }
        #endregion

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var message = $"{GetType().Name} does not have property {propertyName}.";

                if (ThrowOnInvalidPropertyName)
                    throw new Exception(message);

                Debug.Fail(message);
            }
        }
    }
}
