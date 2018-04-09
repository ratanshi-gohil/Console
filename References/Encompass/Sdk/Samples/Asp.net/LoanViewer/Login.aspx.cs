using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EllieMae.Encompass.Client;

namespace EllieMae.Encompass.SDK.Samples
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class Login : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		//submit login
		protected void but_ok_Click(object sender, System.EventArgs e)
		{
			// Collect the values entered
			string server = this.serverBox.Text;
			string loginName = this.loginNameBox.Text;
			string password = this.passwordBox.Text;

			try
			{
				// Start the session
				EllieMae.Encompass.Client.Session s = new EllieMae.Encompass.Client.Session();

				if (server == "")
					s.StartOffline(loginName, password);
				else
					s.Start(server, loginName, password);

				//save session
				Session["session"] = s;
				//save login info
				System.Web.Security.FormsAuthentication.SetAuthCookie(loginName,false);
				//redirect to pipline page
				Server.Transfer("Pipeline.aspx");

			}
			catch (Exception ex)
			{
				messageBox.Text = "Login error: " + ex;
			}		
		}
	}
}
