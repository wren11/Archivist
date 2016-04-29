using System;
using System.Windows.Input;

using Archivist.Commands;

namespace Archivist.ViewModels
{
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        public event EventHandler CloseRequested;

        public ICommand CloseCommand => new DelegateCommand(OnCloseRequested);

        protected WorkspaceViewModel() { }

        protected virtual void OnCloseRequested()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
