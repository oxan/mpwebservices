<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EPGSearch.aspx.cs" Inherits="EPGSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MediaPortal WebServices--EPG search</title>
</head>
<body>
    <form id="form1" runat="server">
    <h3>EPG Search</h3>
    Title: 
    <asp:TextBox ID="edTitle" runat="server" Width="301px"></asp:TextBox>  
    &nbsp;&nbsp;  
    <asp:Button ID="btnSearch" runat="server" Text="Search" 
      onclick="btnSearch_Click" />
    <br />
    <i>Use the percentage sign as wildcard character e.g.<br />
    winter% =&gt; searches for all titles that start with winter<br />
    %winter% =&gt; searches for all titles that contain the word winter<br />
    The search is NOT case sensitive </i>
    <hr style="border: solid 1px black" />
    
        <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" ForeColor="#333333" GridLines="None" 
      DataKeyNames="idProgram" onrowcommand="grid_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:BoundField HeaderText="Time" DataField="time" HtmlEncode="False" >
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="genre" HeaderText="Genre">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:ImageField DataImageUrlField="logo">
              <ControlStyle Height="50px" Width="50px" />
              <ItemStyle VerticalAlign="Top" />
            </asp:ImageField>
            <asp:BoundField DataField="channel" HeaderText="Channel">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" CommandName="record" HeaderText="Action" 
              ImageUrl="~/pics/rec.gif" Text="record" >
              <ItemStyle VerticalAlign="Top" />
            </asp:ButtonField>
            <asp:BoundField DataField="program" HeaderText="Program" HtmlEncode="False">
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
          </Columns>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#999999" />
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </form>
</body>
</html>
