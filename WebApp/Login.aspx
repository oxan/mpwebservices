<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>MediaPortal WebServices - Login</title>
		<link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
		<link rel="stylesheet" type="text/css" href="stylesheets/login.css" />
	</head>
	<body>
		<div id="main">
			<form id="form1" runat="server">
				<h1>MediaPortal WebServices Login</h1>
				<table>
					<tr>
						<td>Username:</td>
						<td><asp:TextBox ID="edUid" runat="server"></asp:TextBox></td>
					</tr>
					<tr>
						<td>Password:</td>
						<td><asp:TextBox ID="edPwd" runat="server" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td></td>
						<td style="text-align:right;"><asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" Text="Login" /></td>
					</tr>
				</table>
				<asp:Label ID="lError" runat="server" Text="Username or password incorrrect" Visible="False"></asp:Label>	
			</form>
		</div>
	</body>
</html>