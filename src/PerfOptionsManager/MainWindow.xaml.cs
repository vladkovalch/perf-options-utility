using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PerfOptionsManager
{
	public partial class MainWindow : MetroWindow
	{
		PerfOptionsMngr perfOptionsMngr;

		public MainWindow()
		{
			perfOptionsMngr = new PerfOptionsMngr();
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

		private void GenerateRegFile()
		{
			if (string.IsNullOrEmpty(fileNameTextBox.Text))
			{
				ShowErrorMessageDialog("Enter file name");
			}
			else if (fileNameTextBox.Text.Length > 260)
			{
				ShowErrorMessageDialog("File name is too long. Please try again");
			}
			else if (Path.GetExtension(fileNameTextBox.Text) == ".exe")
			{
				try
				{
					string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
					string fileName = fileNameTextBox.Text;
					PerfOptions perfOptions = (PerfOptions)perfOptionsComboBox.SelectedValue;

					;
					ShowInfoMessageDialog($@"{perfOptionsMngr.ChangePerfOptions(filePath, fileName, perfOptions)}"+
						" file successfully created!");
				}
				catch (ArgumentException)
				{
					ShowErrorMessageDialog("File name is not valid. Please try again");
				}
				catch (System.IO.PathTooLongException)
				{
					ShowErrorMessageDialog("File name is too long. Please try again");
				}
				catch (NotSupportedException)
				{
					ShowErrorMessageDialog("File name is not supported. Please try again");
				}
				catch (Exception)
				{
					ShowErrorMessageDialog("Something went wrong. Please try again");
				}
			}
			else
			{
				ShowErrorMessageDialog("Enter file extension (.exe)");
			}
		}

		private void ShowErrorMessageDialog(string message)
		{
			ErrorMessageWindow errorMessage = new ErrorMessageWindow(message);
			errorMessage.Left = this.Left + this.Width / 4;
			errorMessage.Top = this.Top + this.Height / 2;
			errorMessage.ShowDialog();
		}

		private void ShowInfoMessageDialog(string message)
		{
			InfoMessageWindow infoMessage = new InfoMessageWindow(message);
			infoMessage.Left = this.Left + this.Width / 4;
			infoMessage.Top = this.Top + this.Height / 2;
			infoMessage.ShowDialog();
		}
	}
}
