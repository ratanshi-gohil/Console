using System;
using System.Windows.Forms;
using EllieMae.Encompass.ComponentModel;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;

namespace LoanMonitorPlugin
{
	/// <summary>
	/// Summary description for LoanMonitorPlugin.
	/// </summary>
	[Plugin]
	public class LoanMonitorPlugin
	{
		// Display the window
		private MonitorWindow currentMonitor = null;

		// The public constructor for the plugin. All plugins must have a public, parameterless
		// constructor. In the constructor you should subscribe to the events you wish to
		// handle within Encompass.
		public LoanMonitorPlugin()
		{
			EncompassApplication.LoanOpened += new EventHandler(Application_LoanOpened);
			EncompassApplication.LoanClosing += new EventHandler(Application_LoanClosing);
		}

		// Event handler for when a loan is opened
		private void Application_LoanOpened(object sender, EventArgs e)
		{
			this.currentMonitor = new MonitorWindow();
			this.currentMonitor.Show();
		}

		private void Application_LoanClosing(object sender, EventArgs e)
		{
			if (this.currentMonitor != null)
			{
				this.currentMonitor.Close();
				this.currentMonitor = null;
			}
		}
	}
}
