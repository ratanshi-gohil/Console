using System;
using System.Windows.Forms;
using EllieMae.Encompass.ComponentModel;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;

namespace DefaultScreenPlugin
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Plugin]
	public class DefaultScreenPlugin
	{
		// The public constructor for the plugin. All plugins must have a public, parameterless
		// constructor. In the constructor you should subscribe to the events you wish to
		// handle within Encompass.
		public DefaultScreenPlugin()
		{
			EncompassApplication.Login += new EventHandler(Application_Login);
		}

		// Event handler for when the application is ready for user input
		private void Application_Login(object sender, EventArgs e)
		{
			if (PluginSettings.DefaultScreen == EncompassScreen.Unknown)
				using (WelcomeForm frm = new WelcomeForm())
					frm.ShowDialog(System.Windows.Forms.Form.ActiveForm);

			// Make the selected screen the current screen
			if (PluginSettings.DefaultScreen != EncompassScreen.Unknown)
				EncompassApplication.Screens[PluginSettings.DefaultScreen].MakeCurrent();
		}
	}
}
