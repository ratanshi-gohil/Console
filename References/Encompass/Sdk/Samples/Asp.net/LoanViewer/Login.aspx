<%@ Page language="c#" Inherits="EllieMae.Encompass.SDK.Samples.Login" Codebehind="Login.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Login</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="default.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<p align="center">Server Login</p>
		<form method="post" runat="server">
			<TABLE id="Table1" cellSpacing="3" cellPadding="3" width="340" border="0" align="center" style="border:1px solid #000000">
				<TR>
					<TD>Login Name:</TD>
					<TD>
						<asp:TextBox id="loginNameBox" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>Password:</TD>
					<TD>
						<asp:TextBox id="passwordBox" TextMode="Password" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>Server:</TD>
					<TD>
						<asp:TextBox id="serverBox" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD colspan="2" align="center">
						<asp:Button id="but_ok" runat="server" Text="Login" onclick="but_ok_Click"></asp:Button>&nbsp;&nbsp;</TD>
				</TR>
			</TABLE>
			<asp:Label id="messageBox" runat="server" Width="176px" ForeColor="Red"></asp:Label>			
		</form>
	</body>
</HTML>
