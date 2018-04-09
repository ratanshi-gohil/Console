using System;
using System.Windows.Forms;
using EllieMae.Encompass.ComponentModel;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.Client;
using EllieMae.Encompass.BusinessObjects.Loans;

namespace DataExchangePlugin
{
	/// <summary>
	/// Summary description for DataExchangePlugin.
	/// </summary>
	[Plugin]
	public class DataExchangePlugin
	{
		// A helper delegate class
		private delegate void DataExchangeDelegate(object data);

		// The public constructor for the plugin. All plugins must have a public, parameterless
		// constructor. In the constructor you should subscribe to the events you wish to
		// handle within Encompass.
		public DataExchangePlugin()
		{
			EncompassApplication.Session.DataExchange.DataReceived += new DataExchangeEventHandler(Session_DataExchange);
		}

		// Catch the data exchange event to get the data from the external app. This event is likely called
		// on a thread other than the UI thread since it occurs asynchronously. To ensure proper behavior of the UI
		// we must marshal the call back to the UI thread using the EncompassApplication.Screens.Invoke() method.
		private void Session_DataExchange(object sender, DataExchangeEventArgs e)
		{
			// Push this onto the UI thread since the DataExchange will be called asynchronously from the server
			if (EncompassApplication.Screens.InvokeRequired)
				EncompassApplication.Screens.Invoke(new DataExchangeEventHandler(Session_DataExchange), new object[] { sender, e });
			else
				executeDataExchange(e.Data);
		}


		// This private method performs the actual data exchange processing. In this example we make the assumption that the data
		// being received is a string containing a comma-delimited list of FirstName,LastName,PhoneNumber.
		// We then prompt the user to create a new loan. If they agree, we must first ensure that a loan isn't already in the
		// editor and, if it is, force it to be closed (allowing the user to save). A new loan is then opened up in
		// the loan editor and data recevied thru the exchange is populated into it.
		private void executeDataExchange(object data)
		{
			// Parse the payload by splitting it at the commas
			string[] dataItems = data.ToString().Split(',');

			// Display a message -- if the user selects "No", we simply abort the process
			DialogResult res = MessageBox.Show(EncompassApplication.Screens, "A call has been received from " + 
				dataItems[0] + " " + dataItems[1] + ". Start a new loan?", 
				"DataExchangePlugin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (res == DialogResult.No) return;

			// Close the current loan screen, if open -- this is a requirement before we attempt to open
			// a new loan in the editor.
			LoansScreen screen = (LoansScreen) EncompassApplication.Screens[EncompassScreen.Loans];

			if (EncompassApplication.CurrentLoan != null)
				screen.Close(false);
		
			// Start a new loan in the My Pipeline folder
			Loan newLoan = screen.OpenNew(EncompassApplication.Session.Loans.Folders["My Pipeline"], null);
			newLoan.Fields["36"].Value = dataItems[0];   // First Name
			newLoan.Fields["37"].Value = dataItems[1];   // Last Name
			newLoan.Fields["66"].Value = dataItems[2];   // Home Phone

			// Refresh the current input form to reflect the values set here
			if (screen.CurrentForm != null)
				screen.CurrentForm.Refresh(false);
		}
	}
}
