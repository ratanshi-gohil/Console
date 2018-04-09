using System;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using EllieMae.Encompass.Client;

namespace EllieMae.Encompass.SDK.Samples
{
	// The globals will provide a place to hold the Session object for the current application
	public class Globals
	{
		private static Session session = null;

		// Current Session object
		public static Session Session
		{
			get { return session; }
			
			set 
			{ 
				session = value;

				// Attach the event handlers to catch messages and disconnects
				session.MessageArrived += new ServerMessageEventHandler(sessionMessageArrived);
				session.Disconnected += new DisconnectedEventHandler(sessionDisconnected);
			}
		}

		// The event handler for an incoming asynchronous message
		private static void sessionMessageArrived(object sender, ServerMessageEventArgs e)
		{
			MessageBox.Show(MainForm.Instance, "A message has arrived from " + e.Source + ":\r\n\r\n" + e.Text);
		}

		// The event handler for unexpected disconnects from the server
		private static void sessionDisconnected(object sender, DisconnectedEventArgs e)
		{
			try
			{
				MessageBox.Show(MainForm.Instance, "The session has been disconnected for the following reason: " + e.Reason + ".");
			}
			catch {}
		}
	}
}
