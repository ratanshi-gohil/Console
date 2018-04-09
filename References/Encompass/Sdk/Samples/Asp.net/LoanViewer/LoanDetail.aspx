<%@ Page language="c#" Inherits="EllieMae.Encompass.SDK.Samples.LoanDetail" Codebehind="LoanDetail.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LoanDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="default.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<p align="center"><asp:Label id="labLoanName" runat="server"></asp:Label></p>
		<form id="loanDetail" method="post" runat="server">
			<table cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
				<tr>
					<td><asp:table id="loanTable" runat="server" Width="100%" BorderWidth="1" BorderColor="black">
							<asp:TableRow BackColor="#00aaaa">
								<asp:TableCell>Field Id</asp:TableCell>
								<asp:TableCell>Field Name</asp:TableCell>
								<asp:TableCell>Field Value</asp:TableCell>
							</asp:TableRow>
						</asp:table></td>
				</tr>
				<tr>
					<td align="center"><asp:button id="butSave" runat="server" Text="Save" onclick="Button1_Click"></asp:button>&nbsp;&nbsp;
						<asp:Button id="butBack" runat="server" Text="Back" onclick="butBack_Click"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
