using CatanRollTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CatanRollTracker
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public const string resultsDir = "results";
		public const string pathSeparator = "/";
		public const string resultFileExtension = ".txt";
		public const string settingsFileName = "settings.ini";
		public static Settings? settings;
		public static IntegerProperty[] rolls = new IntegerProperty[13];


		private void Application_Exit(object sender, ExitEventArgs e)
		{
			serializeObjectNoGUI(settingsFileName, settings);
			if (settings.AutoSaveOnExit)
			{
				if (!Directory.Exists(App.resultsDir))
				{
					try
					{
						Directory.CreateDirectory(App.resultsDir);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
				serializeObjectNoGUI(resultsDir + pathSeparator + getDefaultFileName() + resultFileExtension, createResult());
			}
		}

		public static async void serializeObjectCore(string pathWithFileName, object result)
		{
			// based on https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-8-0
			using FileStream saveFileStream = File.Create(pathWithFileName);
			await JsonSerializer.SerializeAsync(saveFileStream, result);
			await saveFileStream.DisposeAsync();
		}

		public static void serializeObjectNoGUI(string pathWithFileName, object result)
		{
			try
			{
				serializeObjectCore(pathWithFileName, result);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public static string getDefaultFileName()
		{
			return "Game_on_" + DateTime.Now.ToString().Replace(" ", "_").Replace(":", "") + resultFileExtension;
		}

		public static Result createResult()
		{
			return new Result()
			{
				roll2s = rolls[2].Value,
				roll3s = rolls[3].Value,
				roll4s = rolls[4].Value,
				roll5s = rolls[5].Value,
				roll6s = rolls[6].Value,
				roll7s = rolls[7].Value,
				roll8s = rolls[8].Value,
				roll9s = rolls[9].Value,
				roll10s = rolls[10].Value,
				roll11s = rolls[11].Value,
				roll12s = rolls[12].Value
			};
		}
	}
}
