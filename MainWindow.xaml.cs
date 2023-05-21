using CatanRollTracker.Models;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CatanRollTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Arrays are oversized on purpose for easier understanding
		private ProgressBar[] progressBars = new ProgressBar[13];
		private Label[] labels = new Label[13];

		public MainWindow()
		{
			loadSettings();

			for (int i = 0; i < App.rolls.Length; i++)
			{
				App.rolls[i] = new IntegerProperty(0);
			}

			WindowState = WindowState.Maximized;
			InitializeComponent();
			DataContext = this;

			progressBars[2] = roll2sPB;
			progressBars[3] = roll3sPB;
			progressBars[4] = roll4sPB;
			progressBars[5] = roll5sPB;
			progressBars[6] = roll6sPB;
			progressBars[7] = roll7sPB;
			progressBars[8] = roll8sPB;
			progressBars[9] = roll9sPB;
			progressBars[10] = roll10sPB;
			progressBars[11] = roll11sPB;
			progressBars[12] = roll12sPB;

			labels[2] = roll2sLbl;
			labels[3] = roll3sLbl;
			labels[4] = roll4sLbl;
			labels[5] = roll5sLbl;
			labels[6] = roll6sLbl;
			labels[7] = roll7sLbl;
			labels[8] = roll8sLbl;
			labels[9] = roll9sLbl;
			labels[10] = roll10sLbl;
			labels[11] = roll11sLbl;
			labels[12] = roll12sLbl;

			// Bindings

			for(int i = 2; i < 13; i++)
			{
				Binding lblBinding = new Binding("Value");
				lblBinding.Source = App.rolls[i];
				labels[i].SetBinding(Label.ContentProperty, lblBinding);
				progressBars[i].SetBinding(ProgressBar.ValueProperty, lblBinding);
			}

			// Keyboard event handling

			KeyDown += MainWindow_KeyDown;
		}

		private async void loadSettings()
		{
			if (!File.Exists(App.settingsFileName))
			{
				App.settings = new Settings();
				serializeObject(App.settingsFileName, App.settings);
			}else
			{
				App.settings = await deserializeObject<Settings>(App.settingsFileName);
			}
			bindSettings();
		}
		
		private void bindSettings()
		{
			autoSaveMI.IsChecked = App.settings.AutoSaveOnExit;
			//Binding autoSaveMIBinding = new Binding("AutoSaveOnExit");
			//autoSaveMIBinding.Mode = BindingMode.TwoWay;
			//autoSaveMIBinding.Source = App.settings;
			//autoSaveMI.SetBinding(MenuItem.IsCheckedProperty, autoSaveMIBinding);
		}

		private int getMaxRollCount()
		{
			int maxRollCount = 0;

			for(int i = 2; i < App.rolls.Length; i++)
			{
				if (App.rolls[i].Value > maxRollCount)
				{
					maxRollCount = App.rolls[i].Value;
				}
			}

			return maxRollCount;
		}

		private void setPBMaximums(int max)
		{
			for (int i = 2; i < App.rolls.Length; i++)
			{
				progressBars[i].Maximum = max;
			}
		}

		private void roll(int roll)
		{
			//MessageBox.Show(roll.ToString());
			int expectedRollCount = App.rolls[roll].Value + 1;
			if(expectedRollCount > getMaxRollCount())
			{
				setPBMaximums(expectedRollCount);
			}
			App.rolls[roll].Value++;
		}

		public static void showError(string message)
		{
			MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		private static async void serializeObject(string pathWithFileName, object result)
		{
			try
			{
				App.serializeObjectCore(pathWithFileName, result);
			}catch(Exception ex)
			{
				showError(ex.Message);
			}
		}

		private static async Task<T> deserializeObject<T>(string path)
		{
			try
			{
				using FileStream openStream = File.OpenRead(path);
				T? obj =
					await JsonSerializer.DeserializeAsync<T>(openStream);
				return obj;
			}
			catch (Exception ex)
			{
				showError(ex.Message);
			}
			return default(T);
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			//MessageBox.Show(e.Key.ToString());
			switch(e.Key)
			{
				case Key.D2:
				case Key.NumPad2:
					roll(2);
					return;
				case Key.D3:
				case Key.NumPad3:
					roll(3);
					return;
				case Key.D4:
				case Key.NumPad4:
					roll(4);
					return;
				case Key.D5:
				case Key.NumPad5:
					roll(5);
					return;
				case Key.D6:
				case Key.NumPad6:
					roll(6);
					return;
				case Key.D7:
				case Key.NumPad7:
					roll(7);
					return;
				case Key.D8:
				case Key.NumPad8:
					roll(8);
					return;
				case Key.D9:
				case Key.NumPad9:
					roll(9);
					return;
				case Key.Oem3:
				case Key.Divide:
					roll(10);
					return;
				case Key.OemQuestion:
				case Key.Multiply:
					roll(11);
					return;
				case Key.OemPlus:
				case Key.Subtract:
					roll(12);
					return;
			}
		}

		private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (!Directory.Exists(App.resultsDir))
			{
				try
				{
					Directory.CreateDirectory(App.resultsDir);
				}catch (Exception ex)
				{
					showError(ex.Message);
				}
			}
			serializeObject(App.resultsDir + App.pathSeparator + App.getDefaultFileName(), App.createResult());
		}

		private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = App.getDefaultFileName();
			if(saveFileDialog.ShowDialog() == true)
			{
				serializeObject(saveFileDialog.FileName, App.createResult());
			}
		}

		private void autoSaveMI_Checked(object sender, RoutedEventArgs e)
		{
			if(App.settings != null)
			{
				App.settings.AutoSaveOnExit = true;
			}
		}

		private void autoSaveMI_Unchecked(object sender, RoutedEventArgs e)
		{
			if (App.settings != null)
			{
				App.settings.AutoSaveOnExit = false;
			}
		}
	}
}
