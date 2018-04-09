using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EllieMae.Encompass.Automation;

namespace DefaultScreenPlugin
{
	/// <summary>
	/// Summary description for WelcomeForm.
	/// </summary>
	public class WelcomeForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.ComboBox cboScreens;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WelcomeForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Load the user's name
			lblName.Text = EncompassApplication.CurrentUser.ToString();

			// Load the screen combo box with the list of screens and select the first one.
			cboScreens.DataSource = Enum.GetValues(typeof(EncompassScreen));
			cboScreens.SelectedIndex = 0;
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
			this.label1 = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.cboScreens = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Welcome, ";
			// 
			// lblName
			// 
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblName.Location = new System.Drawing.Point(87, 28);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(214, 18);
			this.lblName.TabIndex = 1;
			// 
			// cboScreens
			// 
			this.cboScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboScreens.Location = new System.Drawing.Point(30, 79);
			this.cboScreens.Name = "cboScreens";
			this.cboScreens.Size = new System.Drawing.Size(272, 21);
			this.cboScreens.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(30, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(272, 18);
			this.label2.TabIndex = 3;
			this.label2.Text = "Seelct your new Home screen:";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(228, 122);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// WelcomeForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(332, 162);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cboScreens);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WelcomeForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Welcome!";
			this.ResumeLayout(false);

		}
		#endregion

		// Save the value to the registry
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			PluginSettings.DefaultScreen = (EncompassScreen) cboScreens.SelectedValue;
			this.DialogResult = DialogResult.OK;
		}
	}
}
