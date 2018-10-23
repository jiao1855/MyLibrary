using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HSBC.INS.Utils.Wpf
{
    public class ReplyCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public ReplyCommand(Action<object> execute)
            : this(execute, null)
        {

        }

        public ReplyCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
