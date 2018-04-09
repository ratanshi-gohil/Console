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
using EllieMae.Encompass.BusinessObjects;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Collections;

namespace EllieMae.Encompass.SDK.Samples
{
	/// <summary>
	/// Summary description for Pipelines.
	/// </summary>
	public partial class Pipeline : EllieMae.Encompass.SDK.Samples.Page
	{
		protected System.Web.UI.WebControls.DropDownList lstFolders;
		protected System.Web.UI.WebControls.DataGrid lvwLoanData;
		
		protected void Page_Load(object sender, System.EventArgs e) 
		{
			if (!IsPostBack) 
			{
				string folderName = Request.QueryString["folderName"] ;
				populateLoanFolderList(folderName);
				populateLoanList(LoginSession.Loans.Folders[lstFolders.SelectedValue]);

			}
		}

		// Populates the loan list with the contents of a folder
		private void populateLoanFolderList(string folderName)
		{
			//build data source 
			ArrayList folders = new ArrayList();

			// Load the list with the identities of the loans
			foreach (LoanFolder folder in LoginSession.Loans.Folders)
				folders.Add(folder.Name);
			
			//Sort folders
			folders.Sort();


			// Clear the current items from the list
			lstFolders.Items.Clear();

			lstFolders.DataSource = folders;
			lstFolders.DataBind();

			if (folderName != null) setFolder(folderName);

		}

		private void setFolder(string folderName)
		{
			// Load the list with the identities of the loans
			foreach (ListItem folder in lstFolders.Items)			
				if (folder.Text.Equals(folderName))
					folder.Selected = true;
				else
					folder.Selected = false;
		}

		// Populates the loan list with the contents of a folder
		private void populateLoanList(LoanFolder parentFolder)
		{
			// Get the contents of the folder
			LoanIdentityList loans = parentFolder.GetContents();

			StringList fieldIds = new StringList();
			fieldIds.Add("36");          // Customer Last Name
			fieldIds.Add("37");          // Customer First Name
			fieldIds.Add("364");          // Loan Number
			fieldIds.Add("MS.STATUS");          // current milestone


			//Create a data table of loans
			DataTable dataTable = new DataTable();
			DataRow dataRow;
 			dataTable.Columns.Add(new DataColumn("Guid", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Last Name", typeof(string)));
			dataTable.Columns.Add(new DataColumn("First Name", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Loan Number", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Milestone", typeof(string)));



			// Load the list with the identities of the loans
			foreach (LoanIdentity id in loans)
			{
				dataRow = dataTable.NewRow();
				StringList fieldValues = LoginSession.Loans.SelectFields(id.Guid, fieldIds);
				dataRow[0] = id.Guid;
				dataRow[1] = fieldValues[0];
				dataRow[2] = fieldValues[1];
				dataRow[3] = fieldValues[2];
				dataRow[4] = fieldValues[3];
				dataTable.Rows.Add(dataRow);
			}

			DataView view = new DataView(dataTable);
			view.Sort = "Last Name";
			lvwLoanData.DataSource = view;
			lvwLoanData.DataBind();
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

		protected void lstFolders_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			populateLoanList(LoginSession.Loans.Folders[lstFolders.SelectedValue]);
		}

	}
}
