<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MediaPortal WebServices</title>
</head>
<body style="background: /pics/Background.png">
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btnTv" runat="server" Text="Tv" 
        onclick="btnTv_Click" Width="70px" />
&nbsp;&nbsp;
      <asp:Button ID="btnRadio" runat="server" Text="Radio" 
        onclick="btnRadio_Click" Width="70px" />
    &nbsp;&nbsp;<asp:Button ID="btnRecordings" runat="server" 
        onclick="btnRecordings_Click" Text="Recordings" 
        Width="70px" />
&nbsp;&nbsp;
      <asp:Button ID="btnSchedules" runat="server" Text="Schedules" 
        Width="70px" onclick="btnSchedules_Click" />
&nbsp;&nbsp;
      <asp:Button ID="btnMovie" runat="server" onclick="btnMovie_Click" Text="Movies" 
        Width="70px" />
&nbsp;&nbsp;
      <asp:Button ID="btnMusic" runat="server" onclick="btnMusic_Click" Text="Music" 
        Width="70px" />
&nbsp;
      <asp:Button ID="btnPictures" runat="server" onclick="btnPictures_Click" Text="Pictures" 
        Width="70px" />
      &nbsp;
      <asp:Button ID="btnTvSeries" runat="server" Text="TvSeries" 
        Width="70px" onclick="btnTvSeries_Click" />
      &nbsp;
      <asp:Button ID="btnMovingPictures" runat="server" Text="Moving Pictures" 
        Width="100px" onclick="btnMovingPictures_Click" />
      &nbsp;<a href="TvServerStatus.aspx" target="_blank">Tv Server Status</a>
      <asp:Button ID="btnLogoff" runat="server" onclick="btnLogoff_Click" 
        Text="Logoff" />
      

    </div>
    <hr style="border: double 3px black"/>
    <asp:MultiView ID="MultiView1" runat="server">
      <asp:View ID="vTv" runat="server">
        <h3>TV</h3>Group:
        <asp:DropDownList ID="cbTvGroups" runat="server" AutoPostBack="True" 
          onselectedindexchanged="cbTvGroups_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;Streaming Profile:
        <asp:DropDownList ID="cbTvProfiles" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnTvRSS" runat="server" AlternateText="RSS Feed" 
          ImageUrl="~/pics/rss-icon.gif" onclick="btnTvRSS_Click" />  &nbsp;
        <input onclick="window.open('EPGSearch.aspx');" type="button" 
          value="Search EPG" />
        &nbsp;&nbsp; <hr style="border: 1px solid #000000" />
        <asp:GridView ID="gridTv" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" ForeColor="#333333" GridLines="None" 
          DataKeyNames="idChannel" onrowcommand="gridTv_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:ImageField DataImageUrlField="logo">
              <ControlStyle Height="50px" Width="50px" />
            </asp:ImageField>
            <asp:HyperLinkField DataNavigateUrlFields="idChannel,channel" 
              DataNavigateUrlFormatString="ChannelEPG.aspx?idChannel={0}&amp;amp;channelName={1}" 
              DataTextField="channel" HeaderText="Channel" 
              NavigateUrl="ChannelEPG.aspx?idChannel={0}&amp;amp;channelName={1}" 
              Target="_blank">
              <ItemStyle VerticalAlign="Top" />
            </asp:HyperLinkField>
            <asp:ButtonField ButtonType="Image" ImageUrl="~\pics\play_enabled.gif" 
              Text="play" CommandName="play" >
              <ItemStyle VerticalAlign="Top" />
            </asp:ButtonField>
            <asp:BoundField DataField="now_next" HeaderText="Now/Next" HtmlEncode="False">
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

        
      </asp:View>
      <asp:View ID="vRadio" runat="server">
        <h3>Radio</h3>Group:
        <asp:DropDownList ID="cbRadioGroups" runat="server" AutoPostBack="True" 
          onselectedindexchanged="cbRadioGroups_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;Streaming Profile:
        <asp:DropDownList ID="cbRadioProfiles" runat="server">
        </asp:DropDownList>
        &nbsp;
        <asp:ImageButton ID="btnRadioRSS" runat="server" AlternateText="RSS Feed" 
          ImageUrl="~/pics/rss-icon.gif" onclick="btnRadioRSS_Click" />
        <hr style="border: 1px solid #000000" />
        <asp:GridView ID="gridRadio" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" ForeColor="#333333" GridLines="None" 
          DataKeyNames="idChannel" onrowcommand="gridRadio_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:ImageField DataImageUrlField="logo">
              <ControlStyle Height="50px" Width="50px" />
            </asp:ImageField>
            <asp:HyperLinkField DataNavigateUrlFields="idChannel,channel" 
              DataNavigateUrlFormatString="ChannelEPG.aspx?idChannel={0}&amp;channelName={1}" 
              DataTextField="channel" HeaderText="Channel" 
              NavigateUrl="ChannelEPG.aspx?idChannel={0}&amp;channelName={1}" 
              Target="_blank">
              <ItemStyle VerticalAlign="Top" />
            </asp:HyperLinkField>
            <asp:ButtonField ButtonType="Image" CommandName="play" 
              ImageUrl="~\pics\play_enabled.gif" Text="play" />
            <asp:BoundField DataField="now_next" HeaderText="Now/Next" HtmlEncode="False">
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
      </asp:View>
      <asp:View ID="vRecordings" runat="server">
        <h3>Recordings</h3>
        Streaming Profile:
        <asp:DropDownList ID="cbRecordingProfiles" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;<asp:ImageButton ID="btnRecordingsRSS" runat="server" 
          AlternateText="RSS Feed" ImageUrl="~/pics/rss-icon.gif" 
          onclick="btnRecordingsRSS_Click" />
        <br />
        WHERE title STARTS WITH&nbsp;<asp:TextBox ID="edRecTitle" runat="server"></asp:TextBox>
        &nbsp;&nbsp;<asp:Button ID="btnSearchRecordings" runat="server" 
          onclick="btnSearchRecordings_Click" Text="Search" />
        &nbsp;<hr style="border: 1px solid #000000" />
        &nbsp;<asp:GridView ID="gridRecordings" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" ForeColor="#333333" GridLines="None" 
          DataKeyNames="idRecording" onrowcommand="gridRecordings_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:BoundField DataField="time" HeaderText="Time">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="channel" HeaderText="Channel">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="genre" HeaderText="Genre">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" CommandName="play" 
              ImageUrl="~\pics\play_enabled.gif" Text="play">
              <ItemStyle VerticalAlign="Top" />
            </asp:ButtonField>
            <asp:BoundField HeaderText="Description" DataField="program" 
              HtmlEncode="False" />
          </Columns>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#999999" />
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </asp:View>
      <asp:View ID="vSchedules" runat="server">
      <h3>Schedules</h3>
      <hr style="border: 1px solid #000000" />
      <input type="button" value="Add schedule" onclick="window.open('ScheduleEditor.aspx');" />  &nbsp;  
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" 
          onclick="btnRefresh_Click" />
      
      <asp:GridView ID="gridSchedules" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" ForeColor="#333333" GridLines="None" 
          DataKeyNames="idSchedule" onrowdeleting="gridSchedules_RowDeleting" 
          onrowediting="gridSchedules_RowEditing">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:BoundField DataField="startTime" HeaderText="Start">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="endTime" HeaderText="End">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/pics/btnDelete.gif" 
              EditImageUrl="~/pics/btnEdit.gif" ShowCancelButton="False" 
              ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="channel" HeaderText="Channel">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Title" DataField="title" 
              HtmlEncode="False" />
            <asp:BoundField DataField="type" HeaderText="Type" />
          </Columns>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#999999" />
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </asp:View>
      <asp:View ID="vMovies" runat="server">
        <h3>Movies</h3>
        Streaming Profile:
        <asp:DropDownList ID="cbMovieProfiles" runat="server">
        </asp:DropDownList>
        &nbsp;
        <asp:ImageButton ID="btnMovieRSS" runat="server" AlternateText="RSS Feed" 
          ImageUrl="~/pics/rss-icon.gif" onclick="btnMovieRSS_Click" />
        <br />
        WHERE title STARTS WITH
        <asp:TextBox ID="edMovieTitle" runat="server"></asp:TextBox>
        &nbsp;&nbsp;<asp:Button ID="btnSearchMovie" runat="server" onclick="btnSearchMovie_Click" 
          Text="Search" />
        &nbsp;<hr style="border: 1px solid #000000" />
        <asp:GridView ID="gridMovies" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" DataKeyNames="idMovie" ForeColor="#333333" GridLines="None" 
          onrowcommand="gridMovies_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:BoundField DataField="genre" HeaderText="Genre">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" CommandName="play" 
              ImageUrl="~/pics/play_enabled.gif" Text="play">
              <ItemStyle VerticalAlign="Top" />
            </asp:ButtonField>
            <asp:BoundField DataField="file" HeaderText="File">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="title" HeaderText="Title" HtmlEncode="False">
              <ItemStyle VerticalAlign="Top" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="plot" HeaderText="Plot">
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
      </asp:View>
      <asp:View ID="vMusic" runat="server">
        <h3>Music</h3>
        Streaming Profile:
        <asp:DropDownList ID="cbMusicProfiles" runat="server">
        </asp:DropDownList>
        &nbsp;
        <asp:ImageButton ID="btnMusicRSS" runat="server" AlternateText="RSS Feed" 
          ImageUrl="~/pics/rss-icon.gif" onclick="btnMusicRSS_Click" />
        <br />
        WHERE album STARTS WITH
        <asp:TextBox ID="edMusicAlbum" runat="server"></asp:TextBox>
        &nbsp;<br />
        AND artist STARTS WITH
        <asp:TextBox ID="edMusicArtist" runat="server"></asp:TextBox>
        &nbsp;<br />
        AND title STARTS WITH
        <asp:TextBox ID="edMusicTitle" runat="server"></asp:TextBox>
        &nbsp;&nbsp;<asp:Button ID="btnSearchMusic" runat="server" Text="Search" 
          onclick="btnSearchMusic_Click" />
        &nbsp;
        <asp:Button ID="btnMusicM3U" runat="server" onclick="btnMusicM3U_Click" 
          Text="Get .m3u of selected tracks" />
        &nbsp;<hr style="border: 1px solid #000000" />
        <asp:GridView ID="gridMusic" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="idTrack" 
          onrowcommand="gridMusic_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:BoundField HeaderText="Album" DataField="album" />
            <asp:BoundField HeaderText="Artist" DataField="artist" />
            <asp:BoundField HeaderText="TrackNo" DataField="trackno" />
            <asp:TemplateField>
              <ItemTemplate>
                <asp:CheckBox ID="cbPlayList" runat="server" />
                <asp:HiddenField ID="hfDuration" runat="server" 
                  Value='<%# Bind("duration") %>' />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Image" CommandName="play" 
              ImageUrl="~/pics/play_enabled.gif" Text="play" />
            <asp:BoundField HeaderText="Title" DataField="title" />
            <asp:BoundField DataField="durationStr" HeaderText="Duration" />
          </Columns>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#999999" />
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </asp:View>
      <asp:View ID="vPicures" runat="server">
      <h3>Pictures</h3>
      Path: <asp:DropDownList ID="cbPicturePath" runat="server"></asp:DropDownList>   &nbsp;   
        <asp:Button ID="btnShowPictures" runat="server" Text="Show" 
          onclick="btnShowPictures_Click" />
      <hr style="border: 1px solid #000000" />
        <asp:PlaceHolder ID="picBox" runat="server"></asp:PlaceHolder>
      </asp:View>
      <asp:View ID="vTvSeries" runat="server">
        <h3>
          TvSeries</h3>
        Streaming Profile:
        <asp:DropDownList ID="cbTvSeriesProfiles" runat="server">
        </asp:DropDownList>
        &nbsp;
        <asp:ImageButton ID="btnSeriesRSS" runat="server" AlternateText="RSS Feed" 
          ImageUrl="~/pics/rss-icon.gif" onclick="btnSeriesRSS_Click" />
        <br />
        WHERE series STARTS WITH
        <asp:TextBox ID="edSeries" runat="server"></asp:TextBox>
        <br />
        AND episode STARTS WITH
        <asp:TextBox ID="edEpisode" runat="server"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnSearchTvSeries" runat="server" 
          onclick="btnSearchTvSeries_Click" Text="Search" />
        &nbsp;<hr style="border: 1px solid #000000" />
        <asp:GridView ID="gridTvSeries" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" DataKeyNames="compositeId" ForeColor="#333333" GridLines="None" 
          onrowcommand="gridTvSeries_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:BoundField DataField="series" HeaderText="Series" >
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" CommandName="play" 
              ImageUrl="~/pics/play_enabled.gif" Text="play" >
              <ItemStyle VerticalAlign="Top" />
            </asp:ButtonField>
            <asp:BoundField DataField="episode" HeaderText="Episode" HtmlEncode="False" />
          </Columns>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#999999" />
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </asp:View>
            <asp:View ID="vMovingPictures" runat="server">
        <h3>
          Moving Pictures</h3>
        Streaming Profile:
        <asp:DropDownList ID="cbMovingPicturesProfiles" runat="server">
        </asp:DropDownList>
              &nbsp;
              <asp:ImageButton ID="btnMovingPicturesRSS" runat="server" 
                AlternateText="RSS Feed" ImageUrl="~/pics/rss-icon.gif" 
                onclick="btnMovingPicturesRSS_Click" />
              <br />
              WHERE title STARTS WITH
              <asp:TextBox ID="edMovingTitle" runat="server"></asp:TextBox>
              &nbsp;
              <asp:Button ID="btnMovingSearch" runat="server" onclick="btnMovingSearch_Click" 
                Text="Search" />
              &nbsp;<hr style="border: 1px solid #000000" />
        <asp:GridView ID="gridMovingPictures" runat="server" AutoGenerateColumns="False" 
          CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" 
          onrowcommand="gridMovingPictures_RowCommand">
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <Columns>
            <asp:BoundField DataField="genre" HeaderText="Genre" >
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:BoundField DataField="parentalRating" HeaderText="Parental Rating">
              <ItemStyle VerticalAlign="Top" />
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" CommandName="play" 
              ImageUrl="~/pics/play_enabled.gif" Text="play" >
              <ItemStyle VerticalAlign="Top" />
            </asp:ButtonField>
            <asp:BoundField DataField="movie" HeaderText="Movie" HtmlEncode="False" />
          </Columns>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#999999" />
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </asp:View>
    </asp:MultiView>
    </form>
</body>
</html>
