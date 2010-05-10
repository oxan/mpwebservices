﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TvServerStatus.aspx.cs" Inherits="TvServerStatus" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>MediaPortal WebServices - Serverstatus</title>
		<link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
		<link rel="stylesheet" type="text/css" href="stylesheets/serverstatus.css" />
		<meta http-equiv="refresh" content="5" />
		<script language="javascript">
			function KickSession(idCard,idChannel,username,actionType)
			{
				document.getElementById("hfIdCard").value=idCard;
				document.getElementById("hfIdChannel").value=idChannel;
				document.getElementById("hfUsername").value=username;
				document.getElementById("hfAction").value=actionType;
				document.form1.submit();
			}
		</script>
	</head>
	<body>
		<div id="main">
			<form id="form1" runat="server">
				<h1>Serverstatus</h1>
				<table>
					<tr>
						<td><b>Machinename/Model:</b></td>
						<td><asp:Label ID="lMachinename" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<td><b>Operation system:</b></td>
						<td><asp:Label ID="lOS" runat="server" Text="Label"></asp:Label></td>
					</tr>          
					<tr>
						<td><b>CPU usage:</b></td>
						<td><asp:Label ID="lCPU" runat="server" Text="Label"></asp:Label></td>
					</tr>          
					<tr>
						<td><b>Memory used:</b></td>
						<td><asp:Label ID="lMemory" runat="server" Text="Label"></asp:Label></td>
					</tr>          
					<tr>
						<td><b>Recording folders:</b></td>
						<td><asp:Label ID="lSpace" runat="server" Text="Label"></asp:Label></td>
					</tr>          
				</table>
				<asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
					<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
					<Columns>
						<asp:BoundField HeaderText="CardName" DataField="name" >
							<ItemStyle VerticalAlign="Top" />
						</asp:BoundField>
						<asp:BoundField DataField="type" HeaderText="CardType" HtmlEncode="False">
							<HeaderStyle HorizontalAlign="Left" />
							<ItemStyle VerticalAlign="Top" />
						</asp:BoundField>
						<asp:BoundField DataField="state" HeaderText="Status" />
						<asp:BoundField DataField="channel" HeaderText="Channel" />
						<asp:BoundField DataField="user" HeaderText="User" />
						<asp:BoundField DataField="action" HtmlEncode="False" />
					</Columns>
					<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
					<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
					<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
					<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
					<EditRowStyle BackColor="#999999" />
					<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
				</asp:GridView>
				<asp:HiddenField ID="hfIdCard" runat="server" />
				<asp:HiddenField ID="hfIdChannel" runat="server" />
				<asp:HiddenField ID="hfUsername" runat="server" />
				<asp:HiddenField ID="hfAction" runat="server" />
			</form>
		</div>
	</body>
</html>