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
	/// Summary description for LoanDetail.
	/// </summary>
	public partial class LoanDetail : EllieMae.Encompass.SDK.Samples.Page
	{
		private string loanId = string.Empty;
		private string folderId = string.Empty;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Gets loan guid from session
			loanId = Request.QueryString["selLoan"];
			folderId = Request.QueryString["selFolder"];

			if (!IsPostBack && loanId != null) 
			{
				generateDataTable();
			}

		}

		//Adds a new row to table
		private void addRow(string fieldName, string fieldId,bool editable, Loan loan)
		{
			TableRow row = new TableRow();

			//add fieldId cell
			TableCell idCell = new TableCell();
			idCell.Text = fieldId;
			row.Cells.Add(idCell);			

			//add fieldName cell
			TableCell nameCell = new TableCell();
			nameCell.Text = fieldName;
			row.Cells.Add(nameCell);

			//add value cell
			TableCell valueCell = new TableCell();
			TextBox valueBox = new TextBox();
			valueBox.Text = loan.Fields[fieldId].FormattedValue;
			valueBox.ID = fieldId;
			valueBox.Width = 200;
			valueCell.Controls.Add(valueBox);

			if (!editable) valueBox.Enabled =false;

			row.Cells.Add(valueCell);

			loanTable.Rows.Add(row);
		}

		//Generates table 
		private void generateDataTable()
		{
			//Gets loan from guid
			Loan loan = LoginSession.Loans.Open(loanId);
			labLoanName.Text = loan.LoanName;

			addRow("Borrower First Name","36",true,loan);
			addRow("Borrower Last Name","37",true,loan);
			addRow("Street","11",true,loan);
			addRow("City","12",true,loan);
			addRow("County","13",true,loan);
			addRow("State","14",true,loan);
			addRow("Loan To Value","353",false,loan);
			addRow("Loan Amount","1109",true,loan);
			addRow("Interest Rate","3",true,loan);
			addRow("Term","4",true,loan);
			addRow("Monthly Payment","5",false,loan);	

			loan.Close();
		}

		//save fields to loan
		private void saveFields(string fieldId, Loan loan)
		{
			string fieldValue = Request.Form[fieldId];
			loan.Fields[fieldId].Value = fieldValue;
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//open loan
			Loan loan = LoginSession.Loans.Open(loanId);

			try
			{
				// update value of certain loan
				loan.Lock();
				saveFields("36",loan);
				saveFields("37",loan);
				saveFields("11",loan);
				saveFields("12",loan);
				saveFields("13",loan);
				saveFields("14",loan);
				saveFields("1109",loan);
				saveFields("3",loan);
				saveFields("4",loan);
				loan.Commit();
			}
			finally
			{
				loan.Unlock();
				loan.Close();
				generateDataTable();
			}		
		}

		protected void butBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Pipeline.aspx?folderName=" + folderId);
		}

	}
}
