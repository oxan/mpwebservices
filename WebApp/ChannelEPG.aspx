<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChannelEPG.aspx.cs" Inherits="ChannelEPG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>MediaPortal WebServices--Channel EPG</title>
		<link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
		<link rel="stylesheet" type="text/css" href="stylesheets/channelepg.css" />
	</head>
	<body onload="window.location.hash='currentdatetime';">
		<div id="main">
			<form id="form1" runat="server">
				<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
				<hr style="border: solid 1px black" />
				<asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="idProgram" onrowcommand="grid_RowCommand">
					<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
					<Columns>
						<asp:BoundField HeaderText="Time" DataField="time" HtmlEncode="False" ><ItemStyle VerticalAlign="Top" /></asp:BoundField>
						<asp:BoundField DataField="genre" HeaderText="Genre"><ItemStyle VerticalAlign="Top" /></asp:BoundField>
						<asp:ButtonField ButtonType="Image" CommandName="record" HeaderText="Action" ImageUrl="~/pics/rec.gif" Text="record" ><ItemStyle VerticalAlign="Top" /></asp:ButtonField>
						<asp:BoundField DataField="program" HeaderText="Program" HtmlEncode="False"><HeaderStyle HorizontalAlign="Left" /><ItemStyle VerticalAlign="Top" /></asp:BoundField>
					</Columns>
					<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
					<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
					<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
					<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
					<EditRowStyle BackColor="#999999" />
					<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
				</asp:GridView>
			</form>
		</div>
	</body>
</html>