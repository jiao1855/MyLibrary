using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HSBC.INS.Utils.Wpf
{
    public class BindableBase : INotifyPropertyChanged
    {
        protected void SetProperty<T>(ref T prop, T value, [CallerMemberName] string callerName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                prop = value;
                OnPropertyChanged(callerName);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
