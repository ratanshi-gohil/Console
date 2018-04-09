<%@ Page language="c#" Inherits="EllieMae.Encompass.SDK.Samples.Pipeline" Codebehind="Pipeline.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Pipelines</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="default.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		function gotoDetail(id)
		{
			var form,loanFolder,loanId;
			
			loanFolder = document.getElementById("selFolder");
			loanId = document.getElementById("selLoan");
			form = document.getElementById("seeDetail");
			
			loanFolder.value = document.getElementById("lstFolders").value;
			loanId.value = id;
			form.submit();
		}
		-->
		</script>
	</HEAD>
	<body>
		<p align="center">Pipeline</p>
		<form id="Piplines" method="post" runat="server">
			<TABLE cellSpacing="1" cellPadding="1" width="600" align="center" border="0" >
				<TR>
					<TD >Loan Folders:<asp:dropdownlist id="lstFolders" runat="server" AutoPostBack="True" onselectedindexchanged="lstFolders_SelectedIndexChanged"></asp:dropdownlist></TD>
				</TR>
			</table>
			<TABLE id="mainBody" cellSpacing="1" cellPadding="1" width="600" align="center" border="0" style="border:1px solid #000000">				
				<TR>
					<TD >
						<asp:DataGrid id="lvwLoanData" CellPadding="3" AutoGenerateColumns="false" BorderWidth="0" CellSpacing="1"
							runat="server" Width="100%">
							<HeaderStyle BackColor="#00aaaa"></HeaderStyle>
							<SelectedItemStyle BackColor="#ffffcc"></SelectedItemStyle>
							<Columns>
								<asp:TemplateColumn>
									<HeaderTemplate>
										Last Name
									</HeaderTemplate>
									<ItemTemplate>
										<a href="#" onclick="gotoDetail('<%# DataBinder.Eval(Container.DataItem, "Guid") %>')">
											<%# DataBinder.Eval(Container.DataItem, "Last Name") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										First Name
									</HeaderTemplate>
									<ItemTemplate>
										<a href="#" onclick="gotoDetail('<%# DataBinder.Eval(Container.DataItem, "Guid") %>')">
											<%# DataBinder.Eval(Container.DataItem, "First Name") %>
										</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										Loan Number
									</HeaderTemplate>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Loan Number") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										Milestone
									</HeaderTemplate>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Milestone") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</TD>
				</TR>
			</TABLE>
		</form>
		<form id="seeDetail" method="get" action="LoanDetail.aspx">
			<input type="hidden" id="selFolder" name="selFolder" /> <input type="hidden" id="selLoan" name="selLoan" />
		</form>
	</body>
</HTML>
