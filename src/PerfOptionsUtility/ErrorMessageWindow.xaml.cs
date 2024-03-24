using MahApps.Metro.Controls;
using System.Windows.Input;

namespace PerfOptionsUtility
{
    public partial class ErrorMessageWindow : MetroWindow
    {
        public string ErrorMessage { get; set; }

        public ErrorMessageWindow(string errorMessage)
        {
            ErrorMessage = errorMessage;
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
