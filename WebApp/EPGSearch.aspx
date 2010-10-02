<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EPGSearch.aspx.cs" Inherits="EPGSearch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>MediaPortal WebServices - EPG search</title>
		<link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
		<link rel="stylesheet" type="text/css" href="stylesheets/epgsearch.css" />
	</head>
	<body>
		<div id="main">
			<form id="form1" runat="server">
				<h1>EPG Search</h1>
				Title: 
				<asp:TextBox ID="edTitle" runat="server"></asp:TextBox>
				<asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" />
				<p>Use the percentage sign as wildcard character e.g.</p>
				<p>winter% &raquo; searches for all titles that start with winter</p>
				<p>%winter% &raquo; searches for all titles that contain the word winter</p>
				<p style="margin-bottom: 20px;">The search is NOT case sensitive</p>
				<asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" GridLines="None" DataKeyNames="idProgram" onrowcommand="grid_RowCommand">
					<RowStyle BackColor="#eeeeee" />
					<Columns>
						<asp:BoundField HeaderText="Time" DataField="time" HtmlEncode="False"></asp:BoundField>
						<asp:BoundField DataField="genre" HeaderText="Genre"></asp:BoundField>
						<asp:ImageField DataImageUrlField="logo"><ControlStyle Height="50px" Width="50px" /></asp:ImageField>
						<asp:BoundField DataField="channel" HeaderText="Channel"></asp:BoundField>
						<asp:ButtonField ButtonType="Image" CommandName="record" HeaderText="Action" ImageUrl="~/pics/rec.gif" Text="record"></asp:ButtonField>
						<asp:BoundField DataField="program" HeaderText="Program" HtmlEncode="False"><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
					</Columns>
					<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
					<AlternatingRowStyle BackColor="#ffffff" ForeColor="#284775" />
				</asp:GridView>
			</form>
		</div>
	</body>
</html>