using MahApps.Metro.Controls;
using System.Windows.Input;

namespace PerfOptionsUtility
{
    public partial class InfoMessageWindow : MetroWindow
    {
        public string InfoMessage { get; set; }

        public InfoMessageWindow(string infoMessage)
        {
            InfoMessage = infoMessage;
            InitializeComponent();

            DataContext = this;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CloseDialog();
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CloseDialog();
        }

        private void CloseDialog()
        {
            DialogResult = true;
            Close();
        }
    }
}
