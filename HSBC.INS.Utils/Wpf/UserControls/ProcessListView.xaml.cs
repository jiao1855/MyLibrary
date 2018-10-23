using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HSBC.INS.Utils.Wpf
{
    /// <summary>
    /// Interaction logic for ProcessListView.xaml
    /// </summary>
    public partial class ProcessListView : UserControl
    {
        public static DependencyProperty ProcessModelsProperty;

        public IList<Message> ProcessModels
        {
            get { return (IList<Message>)GetValue(ProcessModelsProperty); }
            set { SetValue(ProcessModelsProperty, value); }
        }

        public ProcessListView()
        {
            InitializeComponent();
        }

        static ProcessListView()
        {
            ProcessModelsProperty = DependencyProperty.Register("ProcessModels", typeof(IList<Message>), typeof(ProcessListView));
            CommandManager.RegisterClassCommandBinding(typeof(ProcessListView), new CommandBinding(ApplicationCommands.Copy, CopyCommand_Executed, CopyCommand_CanExecuted));
        }

        private static void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
                Clipboard.SetDataObject(e.Parameter.ToString());
        }

        private static void CopyCommand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (e.Parameter != null && e.Parameter.ToString().Trim() != string.Empty);
        }
    }
}