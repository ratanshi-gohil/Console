using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EllieMae.Encompass.Client;

namespace EllieMae.Encompass.SDK.Samples
{
	/// <summary>
	/// Summary description for LoginForm.
	/// </summary>
	public class LoginForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox serverBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.TextBox passwordBox;
		private System.Windows.Forms.TextBox loginNameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LoginForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.serverBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.passwordBox = new System.Windows.Forms.TextBox();
			this.loginNameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// serverBox
			// 
			this.serverBox.Location = new System.Drawing.Point(118, 74);
			this.serverBox.MaxLength = 150;
			this.serverBox.Name = "serverBox";
			this.serverBox.Size = new System.Drawing.Size(124, 21);
			this.serverBox.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(18, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 23);
			this.label3.TabIndex = 21;
			this.label3.Text = "Server:";
			// 
			// cancelBtn
			// 
			this.cancelBtn.BackColor = System.Drawing.SystemColors.Control;
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(166, 110);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(76, 23);
			this.cancelBtn.TabIndex = 4;
			this.cancelBtn.Text = "&Cancel";
			this.cancelBtn.UseVisualStyleBackColor = false;
			// 
			// okBtn
			// 
			this.okBtn.BackColor = System.Drawing.SystemColors.Control;
			this.okBtn.Location = new System.Drawing.Point(102, 110);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(60, 23);
			this.okBtn.TabIndex = 3;
			this.okBtn.Text = "&OK";
			this.okBtn.UseVisualStyleBackColor = false;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// passwordBox
			// 
			this.passwordBox.Location = new System.Drawing.Point(118, 46);
			this.passwordBox.Name = "passwordBox";
			this.passwordBox.PasswordChar = '*';
			this.passwordBox.Size = new System.Drawing.Size(124, 20);
			this.passwordBox.TabIndex = 1;
			// 
			// loginNameBox
			// 
			this.loginNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.loginNameBox.Location = new System.Drawing.Point(118, 14);
			this.loginNameBox.Name = "loginNameBox";
			this.loginNameBox.Size = new System.Drawing.Size(124, 20);
			this.loginNameBox.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(18, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 15;
			this.label2.Text = "Password:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(18, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 14;
			this.label1.Text = "Login Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LoginForm
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(258, 142);
			this.Controls.Add(this.serverBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.passwordBox);
			this.Controls.Add(this.loginNameBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginForm";
			this.Text = "Server Login";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			// Collect the values entered
			string server = this.serverBox.Text;
			string loginName = this.loginNameBox.Text;
			string password = this.passwordBox.Text;

			try
			{
				// Start the session
				Session s = new Session();

				if (server == "")
					s.StartOffline(loginName, password);
				else
					s.Start(server, loginName, password);

				Globals.Session = s;
				this.DialogResult = DialogResult.OK;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Login error: " + ex, "Viewer App", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
