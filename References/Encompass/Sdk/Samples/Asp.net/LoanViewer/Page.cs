using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EllieMae.Encompass.BusinessObjects;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Collections;

namespace EllieMae.Encompass.SDK.Samples
{
	/// <summary>
	/// Summary description for page.
	/// </summary>
	public class Page :System.Web.UI.Page
	{
		private EllieMae.Encompass.Client.Session session  = null;


		protected  EllieMae.Encompass.Client.Session LoginSession
		{
			get
			{
				if (session == null)
					if (Session["session"] == null)
						Server.Transfer("Login.aspx");
					else
						session =  (EllieMae.Encompass.Client.Session)Session["session"];
				return session;
			}
		}
	}
}
