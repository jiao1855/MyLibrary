using System.Windows;
using System.Windows.Forms;

namespace HSBC.INS.Utils.Wpf
{
    /// <summary>
    /// Interaction logic for OpenFileTextBox.xaml
    /// </summary>
    public partial class OpenFileTextBox : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty OpenFileNameProperty;
        public static readonly DependencyProperty OpenFileFilterProperty;

        static OpenFileTextBox()
        {
            OpenFileNameProperty = DependencyProperty.Register("OpenFileName", typeof(string), typeof(OpenFileTextBox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
            OpenFileFilterProperty = DependencyProperty.Register("OpenFileFilter", typeof(string), typeof(OpenFileTextBox));
        }

        public string OpenFileName
        {
            get { return (string)GetValue(OpenFileNameProperty); }
            set { SetValue(OpenFileNameProperty, value); }
        }

        public string OpenFileFilter
        {
            get { return (string)GetValue(OpenFileFilterProperty); }
            set { SetValue(OpenFileFilterProperty, value); }
        }

        private OpenFileDialog openFileDialog;

        public OpenFileDialog OpenFileDialog
        {
            get
            {
                if (openFileDialog == null)
                {
                    openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = OpenFileFilter;
                }
                return openFileDialog;
            }
        }

        public OpenFileTextBox()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OpenFileName))
            {
                OpenFileDialog.FileName = OpenFileName;
            }
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetValue(OpenFileNameProperty, OpenFileDialog.FileName);
            }
        }
    }
}