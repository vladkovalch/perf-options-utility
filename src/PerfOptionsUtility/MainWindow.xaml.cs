using MahApps.Metro.Controls;
using PerfOptionsUtility.Enums;
using PerfOptionsUtility.Managers;
using PerfOptionsUtility.Service;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PerfOptionsUtility
{
    public partial class MainWindow : MetroWindow
    {
        private PerfOptionsRegistryHelper registryHelper;

        public MainWindow()
        {
            InitializeDependencies();

            InitializeComponent();

            perfOptionsComboBox.ItemsSource = Enum.GetValues(typeof(PerfOptions))
                .Cast<PerfOptions>()
                .Select(x => new
                {
                    Value = x,
                    Text = x.ToString().Replace("_", " ")
                });

            perfOptionsComboBox.DisplayMemberPath = "Text";
            perfOptionsComboBox.SelectedValuePath = "Value";
            perfOptionsComboBox.SelectedValue = PerfOptions.High;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GenerateRegFile();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                GenerateRegFile();
            }
        }

        private void InitializeDependencies()
        {
            // TODO: Refactor this method to use DI instead of creating instances directly
            var defaultFileManager = new FileManager();

            registryHelper = new PerfOptionsRegistryHelper(defaultFileManager);
        }

        private void GenerateRegFile()
        {
            // TODO: Localize dialog messages used throughout

            if (!ValidateFileNameTextBox())
            {
                return;
            }

            try
            {
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileName = fileNameTextBox.Text;
                var perfOptions = (PerfOptions)perfOptionsComboBox.SelectedValue;

                var operationResult = registryHelper.GenerateRegistryFileForPerfOptions(filePath, fileName, perfOptions);

                ShowInfoMessageDialog($"The registry file has been created successfully and saved on your Desktop.");
            }
            catch (ArgumentException)
            {
                ShowErrorMessageDialog("File name is invalid. Please try again.");
            }
            catch (System.IO.PathTooLongException)
            {
                ShowErrorMessageDialog("File name is too long. Please try again.");
            }
            catch (NotSupportedException)
            {
                ShowErrorMessageDialog("File name is not supported. Please try again.");
            }
            catch (Exception)
            {
                ShowErrorMessageDialog("Something went wrong. Please try again.");
            }
        }

        private bool ValidateFileNameTextBox()
        {
            var fileName = fileNameTextBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(fileName) || !fileName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                ShowErrorMessageDialog("Enter a valid file name including the extension.");

                return false;
            }

            return true;
        }

        private void ShowErrorMessageDialog(string message)
        {
            ErrorMessageWindow errorMessage = new ErrorMessageWindow(message)
            {
                Left = this.Left + this.Width / 4,
                Top = this.Top + this.Height / 2
            };

            errorMessage.ShowDialog();
        }

        private void ShowInfoMessageDialog(string message)
        {
            InfoMessageWindow infoMessage = new InfoMessageWindow(message)
            {
                Left = this.Left + this.Width / 4,
                Top = this.Top + this.Height / 2
            };

            infoMessage.ShowDialog();
        }
    }
}
