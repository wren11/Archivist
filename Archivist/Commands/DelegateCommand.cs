using System;
using System.Windows.Input;

namespace Archivist.Commands
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action onExecute, Func<bool> onCanExecute = null)
        : base(x => onExecute(), o => onCanExecute?.Invoke() ?? true) { }
    }

    public class DelegateCommand<T> : ICommand
    {
        readonly Action<T> onExecute;
        readonly Predicate<T> onCanExecute;

        bool isExecuting;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<T> onExecute, Predicate<T> onCanExecute = null)
        {
            if (onExecute == null)
                throw new ArgumentNullException(nameof(onExecute));

            this.onExecute = onExecute;
            this.onCanExecute = onCanExecute;
        }

        #region ICommand Methods
        public void NotifyCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(T parameter)
        {
            return onCanExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(T parameter)
        {
            onExecute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return !isExecuting && CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            isExecuting = true;
            try
            {
                NotifyCanExecuteChanged();
                Execute((T)parameter);
            }
            finally
            {
                isExecuting = false;
                NotifyCanExecuteChanged();
            }
        }
        #endregion
    }
}
