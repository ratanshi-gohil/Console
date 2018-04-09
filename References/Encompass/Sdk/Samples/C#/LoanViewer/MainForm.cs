using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EllieMae.Encompass.BusinessObjects;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Collections;
using EllieMae.Encompass.Licensing;

namespace EllieMae.Encompass.SDK.Samples
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		// Instance data for the form
		private Loan currentLoan = null;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox lstFolders;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lstLoans;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView lvwLoanData;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label lblFieldId;
		private System.Windows.Forms.Label lblFieldName;
		private System.Windows.Forms.TextBox txtFieldValue;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		public static MainForm Instance = null;

		// The Entry point for the application
		public static void Main()
		{
			new EllieMae.Encompass.Runtime.RuntimeServices().Initialize();
			execute();
		}

		private static void execute()
		{
			// Force the user to log in
			LoginForm frm = new LoginForm();
			DialogResult res = frm.ShowDialog();

			if (res == DialogResult.Cancel)
				return;

			MainForm.Instance = new MainForm();
			Application.Run(MainForm.Instance);
			Globals.Session.End();
		}

		// Constructor
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Init the form by loading the list of loan folders from the server
			populateLoanFolderList();

			// Set the state of the loan info portion of the form to disabled
			clearCurrentLoan();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lstFolders = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lstLoans = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lvwLoanData = new System.Windows.Forms.ListView();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblFieldId = new System.Windows.Forms.Label();
			this.lblFieldName = new System.Windows.Forms.Label();
			this.txtFieldValue = new System.Windows.Forms.TextBox();
			this.btnCommit = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lstFolders
			// 
			this.lstFolders.Location = new System.Drawing.Point(12, 39);
			this.lstFolders.Name = "lstFolders";
			this.lstFolders.Size = new System.Drawing.Size(160, 147);
			this.lstFolders.TabIndex = 0;
			this.lstFolders.SelectedIndexChanged += new System.EventHandler(this.lstFolders_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Loan Folders:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 194);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Loans:";
			// 
			// lstLoans
			// 
			this.lstLoans.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lstLoans.Location = new System.Drawing.Point(12, 210);
			this.lstLoans.Name = "lstLoans";
			this.lstLoans.Size = new System.Drawing.Size(160, 173);
			this.lstLoans.TabIndex = 2;
			this.lstLoans.SelectedIndexChanged += new System.EventHandler(this.lstLoans_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(184, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(153, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Selected Loan Summary Info:";
			// 
			// lvwLoanData
			// 
			this.lvwLoanData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwLoanData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.columnHeader1,
																						  this.columnHeader2,
																						  this.columnHeader3});
			this.lvwLoanData.FullRowSelect = true;
			this.lvwLoanData.GridLines = true;
			this.lvwLoanData.HideSelection = false;
			this.lvwLoanData.Location = new System.Drawing.Point(184, 39);
			this.lvwLoanData.MultiSelect = false;
			this.lvwLoanData.Name = "lvwLoanData";
			this.lvwLoanData.Size = new System.Drawing.Size(386, 163);
			this.lvwLoanData.TabIndex = 5;
			this.lvwLoanData.View = System.Windows.Forms.View.Details;
			this.lvwLoanData.SelectedIndexChanged += new System.EventHandler(this.lvwLoanData_SelectedIndexChanged);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Field";
			this.columnHeader2.Width = 108;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Value";
			this.columnHeader3.Width = 147;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(184, 214);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "Field ID:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(184, 237);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Field Name:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(184, 261);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 16);
			this.label6.TabIndex = 8;
			this.label6.Text = "Value:";
			// 
			// lblFieldId
			// 
			this.lblFieldId.Location = new System.Drawing.Point(264, 214);
			this.lblFieldId.Name = "lblFieldId";
			this.lblFieldId.Size = new System.Drawing.Size(242, 16);
			this.lblFieldId.TabIndex = 9;
			// 
			// lblFieldName
			// 
			this.lblFieldName.Location = new System.Drawing.Point(264, 237);
			this.lblFieldName.Name = "lblFieldName";
			this.lblFieldName.Size = new System.Drawing.Size(242, 16);
			this.lblFieldName.TabIndex = 10;
			// 
			// txtFieldValue
			// 
			this.txtFieldValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFieldValue.Location = new System.Drawing.Point(264, 258);
			this.txtFieldValue.Name = "txtFieldValue";
			this.txtFieldValue.Size = new System.Drawing.Size(306, 20);
			this.txtFieldValue.TabIndex = 11;
			this.txtFieldValue.Text = "";
			// 
			// btnCommit
			// 
			this.btnCommit.Location = new System.Drawing.Point(184, 290);
			this.btnCommit.Name = "btnCommit";
			this.btnCommit.TabIndex = 12;
			this.btnCommit.Text = "Commit";
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(268, 290);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.TabIndex = 13;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Field ID";
			this.columnHeader1.Width = 48;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(580, 396);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.btnCommit);
			this.Controls.Add(this.txtFieldValue);
			this.Controls.Add(this.lblFieldName);
			this.Controls.Add(this.lblFieldId);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lvwLoanData);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lstLoans);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstFolders);
			this.Name = "MainForm";
			this.Text = "Encompass Loan Viewer";
			this.ResumeLayout(false);

		}
		#endregion

		// Retrieves the list of loan folders from the server and lists them in the Listbox
		private void populateLoanFolderList()
		{
			// Clear the current list
			lstFolders.Items.Clear();

			// Get the list from the server
			foreach (LoanFolder folder in Globals.Session.Loans.Folders)
				lstFolders.Items.Add(folder);
		}

		// When the user selects a folder, we will list all of the folder's loan in the 
		// Loans listbox
		private void lstFolders_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Clear the current loan
			clearCurrentLoan();

			// Load the loan list
			populateLoanList((LoanFolder) lstFolders.SelectedItem);
		}

		// Populates the loan list with the contents of a folder
		private void populateLoanList(LoanFolder parentFolder)
		{
			// Clear the current items from the list
			lstLoans.Items.Clear();

			// Get the contents of the folder
			LoanIdentityList loans = parentFolder.GetContents();

			// Load the list with the identities of the loans
			foreach (LoanIdentity id in loans)
				lstLoans.Items.Add(id);
		}

		// When a user selects one of the loans, we need to display the loan in the
		// body of the form.
		private void lstLoans_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Clear the current loan, if any
			clearCurrentLoan();
		
			// Fetch the selected loan identity
			LoanIdentity id = (LoanIdentity) lstLoans.SelectedItem;

			// Retrieve the loan from the server
			Loan loan = Globals.Session.Loans.Open(id.Guid);

			// Load this loan into the form
			setCurrentLoan(loan);
		}

		// Clears the form of the current loan
		private void clearCurrentLoan()
		{
			// Clear the listview and other fields
			lvwLoanData.Items.Clear();
			lblFieldId.Text = "";
			lblFieldName.Text = "";
			txtFieldValue.Text = "";
			
			// Disable the elements
			lvwLoanData.Enabled = false;
			txtFieldValue.Enabled = false;
			btnCommit.Enabled = false;
			btnRefresh.Enabled = false;

			// Clear the current loan
			this.currentLoan = null;
		}

		// Sets the current loan and loads its data into the form
		private void setCurrentLoan(Loan loan)
		{
			// Set the current loan value for future use
			this.currentLoan = loan;

			// Load the listview with the loan's data
			populateCurrentLoanIntoListView();

			// Enable the controls
			lvwLoanData.Enabled = true;
			txtFieldValue.Enabled = true;
			btnCommit.Enabled = true;
			btnRefresh.Enabled = true;
		}

		// Loads the current loan's data into the listview
		private void populateCurrentLoanIntoListView()
		{
			// Clear the listview rows
			lvwLoanData.SelectedItems.Clear();
			lvwLoanData.Items.Clear();

			// Add a set of rows to the listview from the loan
			addLoanFieldToListView("364", "Loan Number");
			addLoanFieldToListView("11", "Property Address");
			addLoanFieldToListView("12", "Property City");
			addLoanFieldToListView("13", "Property County");
			addLoanFieldToListView("14", "Property State");
			addLoanFieldToListView("136", "Purchase Price");
			addLoanFieldToListView("4", "Term");
			addLoanFieldToListView("3", "Interest Rate");
			addLoanFieldToListView("1335", "Down Payment");
		}

		// Adds a field to the load data view for the current loan
		private void addLoanFieldToListView(string fieldId, string fieldDesc)
		{
			ListViewItem item = new ListViewItem(new string[] { fieldId, 
																  fieldDesc, 
																  this.currentLoan.Fields[fieldId].FormattedValue } );

			lvwLoanData.Items.Add(item);
		}

		// When a listview row is selected, move that row's data into the fields
		private void lvwLoanData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Save any changes made to the current field
			saveCurrentField();

			if (lvwLoanData.SelectedItems.Count > 0)
				loadCurrentField(lvwLoanData.SelectedItems[0]);
		}

		// Saves any changes to the current field back to the loan and updates the listview
		private void saveCurrentField()
		{
			// If no field was loaded, bail out immediately
			if (lblFieldId.Text == "")
				return;

			// Load the field values onto the page
			this.currentLoan.Fields[lblFieldId.Text].Value = txtFieldValue.Text;

		}

		// Loads the selected field into the editable form elements
		private void loadCurrentField(ListViewItem item)
		{
			lblFieldId.Text = item.SubItems[0].Text;
			lblFieldName.Text = item.SubItems[1].Text;
			txtFieldValue.Text = item.SubItems[2].Text;
		}

		// Locates an item in the listview by field Id
		private ListViewItem findListViewItem(string fieldId)
		{
			for (int i = 0; i < lvwLoanData.Items.Count; i++)
				if (lvwLoanData.Items[i].SubItems[0].Text == fieldId)
					return lvwLoanData.Items[i];

			return null;
		}

		// When the Commit button is pressed, all pending changes are saved back to the server
		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			// Save the current field
			saveCurrentField();

			try
			{
				// Lock the loan and then commit the changes
				this.currentLoan.Lock();
				this.currentLoan.Commit();
				this.currentLoan.Unlock();

				// Notify the user
				MessageBox.Show(this, "The current loan has been saved.");

                // Update the listview item for this field
                ListViewItem item = findListViewItem(lblFieldId.Text);

                if (item != null)
                    item.SubItems[2].Text = this.currentLoan.Fields[lblFieldId.Text].FormattedValue;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Error saving loan: " + ex.Message);
			}
		}

		// The refresh button retrieves the loan data from the server and throws out
		// any changes made to this point.
		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			// Refresh the loan
			this.currentLoan.Refresh();
		
			// Re-populate the listview
			populateCurrentLoanIntoListView();
		}
	}
}
