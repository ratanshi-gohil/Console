using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using EllieMae.Encompass.Client;

namespace DataExchangeApp
{
	/// <summary>
	/// Summary description for DataExchangeForm.
	/// </summary>
	public class DataExchangeForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtSendTo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataExchangeForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSend = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtSendTo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtFirstName
			// 
			this.txtFirstName.Location = new System.Drawing.Point(96, 26);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(186, 20);
			this.txtFirstName.TabIndex = 0;
			this.txtFirstName.Text = "John";
			// 
			// txtPhone
			// 
			this.txtPhone.Location = new System.Drawing.Point(96, 70);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(186, 20);
			this.txtPhone.TabIndex = 1;
			this.txtPhone.Text = "555-555-5555";
			// 
			// txtLastName
			// 
			this.txtLastName.Location = new System.Drawing.Point(96, 48);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(186, 20);
			this.txtLastName.TabIndex = 2;
			this.txtLastName.Text = "Homeowner";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "First Name:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 18);
			this.label2.TabIndex = 4;
			this.label2.Text = "Last Name:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 18);
			this.label3.TabIndex = 5;
			this.label3.Text = "Phone #:";
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(206, 138);
			this.btnSend.Name = "btnSend";
			this.btnSend.TabIndex = 6;
			this.btnSend.Text = "Send";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 18);
			this.label4.TabIndex = 8;
			this.label4.Text = "Send to:";
			// 
			// txtSendTo
			// 
			this.txtSendTo.Location = new System.Drawing.Point(96, 102);
			this.txtSendTo.Name = "txtSendTo";
			this.txtSendTo.Size = new System.Drawing.Size(186, 20);
			this.txtSendTo.TabIndex = 7;
			this.txtSendTo.Text = "admin";
			// 
			// DataExchangeForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(308, 180);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtSendTo);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtLastName);
			this.Controls.Add(this.txtPhone);
			this.Controls.Add(this.txtFirstName);
			this.Name = "DataExchangeForm";
			this.Text = "Data Exchange Test App";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			new EllieMae.Encompass.Runtime.RuntimeServices().Initialize();
			Application.Run(new DataExchangeForm());
		}

		// Transmits the data to the Encompass application using the Data Exchange mechanism
		private void btnSend_Click(object sender, System.EventArgs e)
		{
			// Connect to the Encompass Server -- you need to put your server address, user ID and password
			// into the call to Start() below.
			using (Session s = new Session())
			{
				s.Start("localhost", "admin", "password");

				// Build the data to post from the entered values
				string data = txtFirstName.Text + "," + txtLastName.Text + "," + txtPhone.Text;

				// Post the data to all users logged in with the user ID specified in the "Send To" field
				int sessionCount = s.DataExchange.PostDataToUser(txtSendTo.Text, data);

				if (sessionCount == 0)
					MessageBox.Show(this, "No users with the specified ID are logged into the server");
				else
					MessageBox.Show(this, "The message was posted to " + sessionCount + " sessions");
			}
		}
	}
}
