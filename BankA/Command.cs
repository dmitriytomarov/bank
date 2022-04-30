using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankA
{
    public class Command : ICommand
    {
        private Action<object> _command;
        private Predicate<object> _canExecute;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter) { return this._canExecute == null || this._canExecute(parameter); }
        //public bool CanExecute(object parameter) { return parameter == null ? false : this._canExecute(parameter); }
        public void Execute(object parameter)
        {
            _command(parameter);
        }
        public Command(Action<object> command, Predicate<object> canExecute = null)
        {
            this._command = command;
            this._canExecute = canExecute;
        }


    }
}
