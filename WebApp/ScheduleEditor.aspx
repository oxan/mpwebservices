<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleEditor.aspx.cs" Inherits="ScheduleEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <h3><asp:Label ID="lHeading" runat="server" Text="New Schedule"></asp:Label></h3>
      <hr style="border: solid 1px black" />
      Channel: 
      <asp:DropDownList ID="cbChannelType" runat="server" AutoPostBack="True" 
        onselectedindexchanged="cbChannelType_SelectedIndexChanged">
        <asp:ListItem>Tv</asp:ListItem>
        <asp:ListItem>Radio</asp:ListItem>
      </asp:DropDownList>
&nbsp;<asp:DropDownList ID="cbGroup" runat="server" AutoPostBack="True" 
        onselectedindexchanged="cbGroup_SelectedIndexChanged"></asp:DropDownList>&nbsp;<asp:DropDownList ID="cbChannel" runat="server"></asp:DropDownList>&nbsp;<asp:CustomValidator 
        ID="CustomValidator3" runat="server" ControlToValidate="cbChannel" 
        ErrorMessage="* Please select a channel" 
        onservervalidate="CustomValidator3_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
      <br />
      <table border="0">
        <tr>
          <td>Start: </td><td><asp:TextBox ID="edStart" runat="server"></asp:TextBox>
          <asp:CustomValidator ID="CustomValidator1" runat="server" 
            ControlToValidate="edStart" 
            ErrorMessage="* Please enter a valid date time string" 
            onservervalidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
          </td>
        </tr>
        <tr>
          <td>End: </td><td><asp:TextBox ID="edEnd" runat="server"></asp:TextBox>
          <asp:CustomValidator ID="CustomValidator2" runat="server" 
            ControlToValidate="edEnd" 
            ErrorMessage="* Please enter a valid date time string" 
            onservervalidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
          </td>
        </tr>        
        <tr>
          <td>Title: </td><td><asp:TextBox ID="edTitle" runat="server" Width="280px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="edTitle" ErrorMessage="* Please enter a title"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td>Type: </td><td><asp:DropDownList ID="cbScheduleType" runat="server">
          <asp:ListItem Value="0">Once</asp:ListItem>
          <asp:ListItem Value="1">Daily</asp:ListItem>
          <asp:ListItem Value="2">Weekly</asp:ListItem>
          <asp:ListItem Value="3">Every time on this channel</asp:ListItem>
          <asp:ListItem Value="4">Every time on every channel</asp:ListItem>
          <asp:ListItem Value="5">Weekends</asp:ListItem>
          <asp:ListItem Value="6">Working days</asp:ListItem>
          </asp:DropDownList></td>
        </tr>        
      </table>
    </div>
    <br />
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
    </form>
</body>
</html>
