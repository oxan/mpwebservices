<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleEditor.aspx.cs" Inherits="ScheduleEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>MediaPortal WebServices - Schedule Editor</title>
		<link rel="stylesheet" type="text/css" href="stylesheets/reset.css" />
		<link rel="stylesheet" type="text/css" href="stylesheets/schedules.css" />
	</head>
	<body>
		<div id="main">
			<form id="form1" runat="server">
				<h1><asp:Label ID="lHeading" runat="server" Text="New Schedule"></asp:Label></h1>
				<table>
					<tr>
						<td>Channel:</td>
						<td>
							<asp:DropDownList ID="cbChannelType" runat="server" AutoPostBack="True" onselectedindexchanged="cbChannelType_SelectedIndexChanged">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem>Tv</asp:ListItem>
								<asp:ListItem>Radio</asp:ListItem>
							</asp:DropDownList>
							<asp:DropDownList ID="cbGroup" runat="server" AutoPostBack="True" onselectedindexchanged="cbGroup_SelectedIndexChanged"></asp:DropDownList>
							<asp:DropDownList ID="cbChannel" runat="server" onselectedindexchanged="cbChannel_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> 
							<asp:Label ID="lChanError" runat="server" ForeColor="Red" Text="* Please select a channel" Visible="False"></asp:Label>
						</td>
					</tr>
					<!--
					<tr>
						<td>Selected Channel:</td>
						<td><asp:Label ID="lChannel" runat="server" Font-Bold="True"></asp:Label><asp:HiddenField ID="hfIdChannel" runat="server" /></td>
					</tr>				
					-->
					<tr>
						<td>Start:</td>
						<td>
							<asp:TextBox ID="edStart" runat="server"></asp:TextBox>
							<asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="edStart" ErrorMessage="* Please enter a valid date time string" onservervalidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
						</td>
					</tr>
					<tr>
						<td>End:</td>
						<td>
							<asp:TextBox ID="edEnd" runat="server"></asp:TextBox>
							<asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="edEnd" ErrorMessage="* Please enter a valid date time string" onservervalidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
						</td>
					</tr>        
					<tr>
						<td>Title:</td>
						<td>
							<asp:TextBox ID="edTitle" runat="server" Width="280px"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="edTitle" ErrorMessage="* Please enter a title"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td>Type:</td>
						<td>
							<asp:DropDownList ID="cbScheduleType" runat="server">
								<asp:ListItem Value="0">Once</asp:ListItem>
								<asp:ListItem Value="1">Daily</asp:ListItem>
								<asp:ListItem Value="2">Weekly</asp:ListItem>
								<asp:ListItem Value="3">Every time on this channel</asp:ListItem>
								<asp:ListItem Value="4">Every time on every channel</asp:ListItem>
								<asp:ListItem Value="5">Weekends</asp:ListItem>
								<asp:ListItem Value="6">Working days</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td></td>
						<td><asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" /></td>
				</table>				
			</form>
		</div>
	</body>
</html>
