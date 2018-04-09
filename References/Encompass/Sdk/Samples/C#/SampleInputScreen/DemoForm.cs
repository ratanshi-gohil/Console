using System;
using EllieMae.Encompass.Forms;

namespace SampleInputScreen
{
	/// <summary>
	/// The DemoForm is a CodeBase Assembly for a Custom Form in Encompass.
	/// </summary>
	public class DemoForm : Form
	{
		// Instance variables
		private Button btnQuote = null;

		// Override the method to create the controls
		public override void CreateControls()
		{
			this.btnQuote = (Button) FindControl("btnQuote");
			this.btnQuote.Click += new EventHandler(btnQuote_Click);
		}

		// Invoked when the Export button is clicked
		private void btnQuote_Click(object sender, EventArgs e)
		{
			using (QuoteDialog frm = new QuoteDialog(this.Loan))
				frm.ShowDialog(System.Windows.Forms.Form.ActiveForm);
		}
	}
}
