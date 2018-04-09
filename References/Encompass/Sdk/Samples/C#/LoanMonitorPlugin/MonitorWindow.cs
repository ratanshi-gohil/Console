using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Automation;

namespace LoanMonitorPlugin
{
	/// <summary>
	/// Summary description for MonitorWindow.
	/// </summary>
	public class MonitorWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView lvwChanges;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MonitorWindow()
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
			this.lvwChanges = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lvwChanges
			// 
			this.lvwChanges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.columnHeader1,
																						 this.columnHeader2,
																						 this.columnHeader3});
			this.lvwChanges.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwChanges.GridLines = true;
			this.lvwChanges.Location = new System.Drawing.Point(0, 0);
			this.lvwChanges.Name = "lvwChanges";
			this.lvwChanges.Size = new System.Drawing.Size(532, 316);
			this.lvwChanges.TabIndex = 0;
			this.lvwChanges.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Field";
			this.columnHeader1.Width = 93;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Prior Value";
			this.columnHeader2.Width = 184;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "New Value";
			this.columnHeader3.Width = 207;
			// 
			// MonitorWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(532, 316);
			this.Controls.Add(this.lvwChanges);
			this.Name = "MonitorWindow";
			this.Text = "MonitorWindow";
			this.TopMost = true;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MonitorWindow_Closing);
			this.Load += new System.EventHandler(this.MonitorWindow_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void MonitorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Disconnect events
			EncompassApplication.CurrentLoan.FieldChange -= new FieldChangeEventHandler(CurrentLoan_FieldChange);
		}

		private void MonitorWindow_Load(object sender, System.EventArgs e)
		{
			// Listen for events
			EncompassApplication.CurrentLoan.FieldChange += new FieldChangeEventHandler(CurrentLoan_FieldChange);
		}

		// Watch for changes to fields
		private void CurrentLoan_FieldChange(object source, FieldChangeEventArgs e)
		{
			ListViewItem item = new ListViewItem(new string[] { e.FieldID, e.PriorValue, e.NewValue });
			lvwChanges.Items.Add(item);
			item.EnsureVisible();
		}
	}
}
