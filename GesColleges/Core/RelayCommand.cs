using System;
using System.Windows.Input;

namespace GesColleges.Core
{
    class RelayCommand : ICommand
    {
        private Action<Object> _execute;
        private Func<Object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<Object> execute, Func<Object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object paramater)
        {
            return _canExecute == null || _canExecute(paramater);
        }

        public void Execute(object paramater)
        {
            _execute(paramater);
        }
    }
}
