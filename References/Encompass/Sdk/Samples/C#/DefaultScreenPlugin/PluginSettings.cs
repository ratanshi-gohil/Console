using System;
using Microsoft.Win32;
using EllieMae.Encompass.Automation;

namespace DefaultScreenPlugin
{
	/// <summary>
	/// Summary description for PluginSettings.
	/// </summary>
	public class PluginSettings
	{
		// Static/const data
		private const string registryRoot = "Software\\Ellie Mae\\PluginSample";

		// This class is not meant to be instantiated
		private PluginSettings() {}

		// Gets the user's default screen
		public static EncompassScreen DefaultScreen
		{
			get 
			{ 
				try { return (EncompassScreen) Enum.Parse(typeof(EncompassScreen), getValue("Default Screen"), true); }
				catch { return EncompassScreen.Unknown; }
			}

			set  { setValue("Default Screen", value.ToString()); }
		}

		// Reads a value from the registry
		private static string getValue(string name)
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryRoot))
				if (key != null)
					return key.GetValue(name) + "";

			return "";
		}

		// Writes a value to the registry
		private static void setValue(string name, string value)
		{
			using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryRoot))
				key.SetValue(name, value);
		}
	}
}
