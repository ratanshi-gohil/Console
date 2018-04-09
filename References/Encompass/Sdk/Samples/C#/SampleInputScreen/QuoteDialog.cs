using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using EllieMae.Encompass.BusinessObjects.Loans;

namespace SampleInputScreen
{
	/// <summary>
	/// Summary description for QuoteDialog.
	/// </summary>
	public class QuoteDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblAddr;
		private System.Windows.Forms.Label lblCityStateZip;
		private System.Windows.Forms.Label lblAmount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblAmort;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblTerm;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblRate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtAmount;
		private System.Windows.Forms.Label label9;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblInsurance;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		private Loan currentLoan = null;

		public QuoteDialog(Loan loan)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Init the form
			this.currentLoan = loan;
			lblName.Text = loan.Fields["37"].Value + ", " + loan.Fields["36"].Value;
			lblAddr.Text = loan.Fields["11"].Value + "";
			lblCityStateZip.Text = loan.Fields["12"].Value + ", " + loan.Fields["14"].Value + " " + loan.Fields["15"].Value;
			lblAmount.Text = loan.Fields["1109"].Value + "";
			lblAmort.Text = loan.Fields["608"].Value + "";
			lblTerm.Text = loan.Fields["4"].Value + "";
			lblRate.Text = loan.Fields["3"].Value + "";
			txtAmount.Text = loan.Fields["1109"].Value + "";
			recalcInsurance();
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
			this.lblAddr = new System.Windows.Forms.Label();
			this.lblCityStateZip = new System.Windows.Forms.Label();
			this.lblAmount = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblAmort = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblTerm = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblRate = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtAmount = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.lblInsurance = new System.Windows.Forms.Label();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Borrower:";
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(24, 41);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(244, 16);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "(Last, First)";
			// 
			// lblAddr
			// 
			this.lblAddr.Location = new System.Drawing.Point(24, 57);
			this.lblAddr.Name = "lblAddr";
			this.lblAddr.Size = new System.Drawing.Size(244, 16);
			this.lblAddr.TabIndex = 2;
			this.lblAddr.Text = "(Street Address)";
			// 
			// lblCityStateZip
			// 
			this.lblCityStateZip.Location = new System.Drawing.Point(24, 73);
			this.lblCityStateZip.Name = "lblCityStateZip";
			this.lblCityStateZip.Size = new System.Drawing.Size(244, 16);
			this.lblCityStateZip.TabIndex = 3;
			this.lblCityStateZip.Text = "(City, State, Zip)";
			// 
			// lblAmount
			// 
			this.lblAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAmount.Location = new System.Drawing.Point(96, 128);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(172, 17);
			this.lblAmount.TabIndex = 5;
			this.lblAmount.Text = "(Amount)";
			this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(24, 108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "Loan Information:";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(24, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "Amount:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 146);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 17);
			this.label2.TabIndex = 8;
			this.label2.Text = "Amortization:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblAmort
			// 
			this.lblAmort.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAmort.Location = new System.Drawing.Point(96, 146);
			this.lblAmort.Name = "lblAmort";
			this.lblAmort.Size = new System.Drawing.Size(172, 17);
			this.lblAmort.TabIndex = 7;
			this.lblAmort.Text = "(Amortization)";
			this.lblAmort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(24, 164);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 17);
			this.label5.TabIndex = 10;
			this.label5.Text = "Term:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTerm
			// 
			this.lblTerm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblTerm.Location = new System.Drawing.Point(96, 164);
			this.lblTerm.Name = "lblTerm";
			this.lblTerm.Size = new System.Drawing.Size(172, 17);
			this.lblTerm.TabIndex = 9;
			this.lblTerm.Text = "(Term)";
			this.lblTerm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(24, 182);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 17);
			this.label6.TabIndex = 12;
			this.label6.Text = "Rate:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRate
			// 
			this.lblRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblRate.Location = new System.Drawing.Point(96, 182);
			this.lblRate.Name = "lblRate";
			this.lblRate.Size = new System.Drawing.Size(172, 17);
			this.lblRate.TabIndex = 11;
			this.lblRate.Text = "(Rate)";
			this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(24, 219);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(144, 16);
			this.label7.TabIndex = 13;
			this.label7.Text = "Insurance Information:";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(24, 241);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 17);
			this.label8.TabIndex = 14;
			this.label8.Text = "Amount:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtAmount
			// 
			this.txtAmount.Location = new System.Drawing.Point(96, 240);
			this.txtAmount.Name = "txtAmount";
			this.txtAmount.Size = new System.Drawing.Size(172, 20);
			this.txtAmount.TabIndex = 15;
			this.txtAmount.Text = "(Amount)";
			this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(24, 261);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(70, 17);
			this.label9.TabIndex = 16;
			this.label9.Text = "Monthly Pmt:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblInsurance
			// 
			this.lblInsurance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblInsurance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInsurance.Location = new System.Drawing.Point(96, 261);
			this.lblInsurance.Name = "lblInsurance";
			this.lblInsurance.Size = new System.Drawing.Size(172, 17);
			this.lblInsurance.TabIndex = 17;
			this.lblInsurance.Text = "(Amount)";
			this.lblInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnApply
			// 
			this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnApply.Location = new System.Drawing.Point(194, 308);
			this.btnApply.Name = "btnApply";
			this.btnApply.TabIndex = 18;
			this.btnApply.Text = "&Apply";
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(114, 308);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "&Cancel";
			// 
			// QuoteDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(294, 346);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.lblInsurance);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.txtAmount);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.lblRate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lblTerm);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblAmort);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblAmount);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblCityStateZip);
			this.Controls.Add(this.lblAddr);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "QuoteDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "QuoteDialog";
			this.ResumeLayout(false);

		}
		#endregion

		// Recaclulates the insurance cost
		private void recalcInsurance()
		{
			try
			{
				Decimal amt = Decimal.Parse(txtAmount.Text, System.Globalization.NumberStyles.Any, null);
				amt = amt / 360;
				lblInsurance.Text = "$" + amt.ToString("#,##0.00");
			}
			catch
			{
				lblInsurance.Text = "";
			}
		}

		// Applies the insurance to the account
		private void btnApply_Click(object sender, System.EventArgs e)
		{
			this.currentLoan.Fields["CX.INSURANCE"].Value = lblInsurance.Text;
		}

		private void txtAmount_Leave(object sender, System.EventArgs e)
		{
			recalcInsurance();
		}
	}
}
