using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using MediaPortal.TvServer.WebServices;
using MediaPortal.TvServer.WebServices.Classes;


public partial class Default : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["authenticated"] == null)
      Response.Redirect("Login.aspx");
    if (!IsPostBack)
    {
      ServiceInterface server = new ServiceInterface();
      SupportedMPFunctions f=server.GetSupportedMPFunctions();
      btnMovie.Visible = f.myVideos;
      btnMusic.Visible = f.myMusic;
      btnPictures.Visible = f.myPictures;
      btnTvSeries.Visible = f.myTvSeries;
      btnMovingPictures.Visible = f.movingPictures;
    }
  }

  protected void CreateStreamBatch(string queryString)
  {
    string player = Utils.GetClientPlayerPath();
    string str = "\"" + player + "\"";
    string rtspProfile = "idProfile="+(cbRecordingProfiles.Items.Count - 1).ToString();
    if (queryString.Contains("idRecording") && queryString.Contains(rtspProfile))
    {
      ServiceInterface server = new ServiceInterface();
      string idRecording = queryString.Substring(0, queryString.IndexOf('&'));
      idRecording = idRecording.Remove(0, 12);
      str += " " + server.GetRecordingURL(Int32.Parse(idRecording));
    }
    else
      str += " \"" + Utils.GetStreamURL() + "/Streamer.aspx?" + queryString + "\"";
    Response.Clear();
    Response.AddHeader("Content-Disposition", "attachment;filename=StartStreaming.bat; charset=ASCII");
    Response.ContentType = "application/bat";
    Response.Write(str);
    Response.End();
  }
  protected void StartPlayer(string queryString, string mediafile)
  {
    int playerType = Utils.GetPlayerType();
    if (playerType == 0)
      CreateStreamBatch(queryString);
    else
      RegisterStartupScript("firefoxvlc", "<script>window.open('VLCFirefox.htm?url=" + queryString.Replace('&',',') + "&media=" + Server.HtmlEncode(mediafile) + "');</script>");
  }
  protected void LoadStreamingProfiles(DropDownList cb)
  {
    List<EncoderConfig> cfgs = Utils.LoadConfig();
    cb.Items.Clear();
    int idx = 0;
    foreach (EncoderConfig cfg in cfgs)
    {
      cb.Items.Add(new ListItem(cfg.displayName, idx.ToString()));
      idx++;
    }
    cb.SelectedIndex = 0;
  }

  #region Tab-Button handler
  protected void btnTv_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 0;
    RefreshTv();
  }
  protected void btnRadio_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 1;
    RefreshRadio();
  }
  protected void btnRecordings_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 2;
    RefreshRecordings();
  }
  protected void btnSchedules_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 3;
    RefreshSchedules();
  }
  protected void btnMovie_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 4;
    RefreshMovies();
  }
  protected void btnMusic_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 5;
    RefreshMusicTracks();
  }
  protected void btnPictures_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 6;
    RefreshPictures();
  }
  protected void btnTvSeries_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 7;
    RefreshTvSeries();
  }
  protected void btnMovingPictures_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 8;
    RefreshMovingPictures();
  }
  #endregion

  #region TV
  protected void RefreshTv()
  {
    ServiceInterface server = new ServiceInterface();
    if (cbTvGroups.Items.Count == 0)
    {
      List<WebChannelGroup> tvgroups = server.GetTvChannelGroups();
      int i = 0;
      int index = -1;
      int allIndex = -1;
      foreach (WebChannelGroup g in tvgroups)
      {
        if (g.name == "All Channels" && allIndex == -1)
          allIndex = i;
        if (g.name != "All Channels" && index == -1)
          index = i;
        cbTvGroups.Items.Add(new ListItem(g.name, g.idGroup.ToString()));
        i++;
      }
      if (index != -1)
        cbTvGroups.SelectedIndex = index;
      else
        cbTvGroups.SelectedIndex = allIndex;
    }

    List<WebMiniEPG> epgs = server.GetTvMiniEPGForGroup(Int32.Parse(cbTvGroups.SelectedValue));
    DataTable dt = new DataTable();
    dt.Columns.Add("channel", typeof(string));
    dt.Columns.Add("now_next", typeof(string));
    dt.Columns.Add("idChannel", typeof(int));
    foreach (WebMiniEPG epg in epgs)
    {
      DataRow row = dt.NewRow();
      row["channel"] = epg.name;
      row["now_next"] = epg.epgNow.startTime.ToShortTimeString() + " - " + epg.epgNow.endTime.ToShortTimeString() + ": " + epg.epgNow.title + "<br/>" + epg.epgNext.startTime.ToShortTimeString() + " - " + epg.epgNext.endTime.ToShortTimeString() + ": " + epg.epgNext.title;
      row["idChannel"] = epg.idChannel;
      dt.Rows.Add(row);
    }
    gridTv.DataSource = dt;
    gridTv.DataBind();
    LoadStreamingProfiles(cbTvProfiles);
  }
  protected void gridTv_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    if (e.CommandName == "play")
    {
      int rowIndex = Int32.Parse((string)e.CommandArgument);
      int idx = (int)gridTv.DataKeys[rowIndex].Value;
      StartPlayer("idChannel=" + idx.ToString() + "&idProfile=" + cbTvProfiles.SelectedIndex, gridTv.Rows[rowIndex].Cells[0].Text);
    }
  }
  protected void cbTvGroups_SelectedIndexChanged(object sender, EventArgs e)
  {
    RefreshTv();
  }
  protected void btnTvRSS_Click(object sender, ImageClickEventArgs e)
  {
    string url = Utils.GetStreamURL() + "/RSSGenerator.aspx?idTvGroup=" + cbTvGroups.SelectedValue + "&idProfile=" + cbTvProfiles.SelectedIndex.ToString() + "&groupname=" + cbTvGroups.SelectedItem.Text;
    RegisterStartupScript("rss", "<script>window.open('" + url + "');</script>");
  }
  #endregion

  #region Radio
  protected void RefreshRadio()
  {
    ServiceInterface server = new ServiceInterface();
    if (cbRadioGroups.Items.Count == 0)
    {
      List<WebChannelGroup> radiogroups = server.GetRadioChannelGroups();
      int i = 0;
      int index = -1;
      int allIndex = -1;
      foreach (WebChannelGroup g in radiogroups)
      {
        if (g.name == "All Channels" && allIndex != -1)
          allIndex = i;
        if (g.name != "All Channels" && index != -1)
          index = i;
        cbRadioGroups.Items.Add(new ListItem(g.name, g.idGroup.ToString()));
        i++;
      }
      if (index != -1)
        cbRadioGroups.SelectedIndex = index;
      else
        cbRadioGroups.SelectedIndex = allIndex;
    }
    List<WebMiniEPG> epgs = server.GetRadioMiniEPGForGroup(Int32.Parse(cbRadioGroups.SelectedValue));
    DataTable dt = new DataTable();
    dt.Columns.Add("channel", typeof(string));
    dt.Columns.Add("now_next", typeof(string));
    dt.Columns.Add("idChannel", typeof(int));
    foreach (WebMiniEPG epg in epgs)
    {
      DataRow row = dt.NewRow();
      row["channel"] = epg.name;
      row["now_next"] = epg.epgNow.startTime.ToShortTimeString() + " - " + epg.epgNow.endTime.ToShortTimeString() + ": " + epg.epgNow.title + "<br/>" + epg.epgNext.startTime.ToShortTimeString() + " - " + epg.epgNext.endTime.ToShortTimeString() + ": " + epg.epgNext.title;
      row["idChannel"] = epg.idChannel;
      dt.Rows.Add(row);
    }
    gridRadio.DataSource = dt;
    gridRadio.DataBind();
    LoadStreamingProfiles(cbRadioProfiles);
  }
  protected void gridRadio_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    if (e.CommandName == "play")
    {
      int rowIndex=Int32.Parse((string)e.CommandArgument);
      int idx = (int)gridRadio.DataKeys[rowIndex].Value;
      StartPlayer("idChannel=" + idx.ToString() + "&idProfile=" + cbRadioProfiles.SelectedIndex, gridRadio.Rows[rowIndex].Cells[0].Text);
    }
  }
  protected void cbRadioGroups_SelectedIndexChanged(object sender, EventArgs e)
  {
    RefreshRadio();
  }
  protected void btnRadioRSS_Click(object sender, ImageClickEventArgs e)
  {
    string url = Utils.GetStreamURL() + "/RSSGenerator.aspx?idRadioGroup=" + cbRadioGroups.SelectedValue + "&idProfile=" + cbTvProfiles.SelectedIndex.ToString() + "&groupname=" + cbRadioGroups.SelectedItem.Text;
    RegisterStartupScript("rss", "<script>window.open('" + url + "');</script>");
  }
  #endregion

  #region Recordings
  protected void RefreshRecordings()
  {
    ServiceInterface server = new ServiceInterface();
    List<WebRecording> recs = server.GetAllRecordings();
    DataTable dt = new DataTable();
    dt.Columns.Add("time", typeof(string));
    dt.Columns.Add("channel", typeof(string));
    dt.Columns.Add("genre", typeof(string));
    dt.Columns.Add("program", typeof(string));
    dt.Columns.Add("idRecording", typeof(int));
    foreach (WebRecording rec in recs)
    {
      DataRow row = dt.NewRow();
      row["time"] = rec.startTime.ToString() + "-" + rec.endTime.ToShortTimeString();
      row["channel"] = rec.channelName;
      row["genre"] = rec.genre;
      row["program"] = "<b>" + rec.title + "</b><br/>" + rec.description;
      row["idRecording"] = rec.idRecording;
      dt.Rows.Add(row);
    }
    gridRecordings.DataSource = dt;
    gridRecordings.DataBind();
    LoadStreamingProfiles(cbRecordingProfiles);
    cbRecordingProfiles.Items.Add(new ListItem("[TvServer RTSP]"));
  }
  protected void gridRecordings_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    if (e.CommandName != "play")
      return;
    int rowIndex = Int32.Parse((string)e.CommandArgument);
    int idx = (int)gridRecordings.DataKeys[rowIndex].Value;
    StartPlayer("idRecording=" + idx.ToString() + "&idProfile=" + cbRecordingProfiles.SelectedIndex, gridRecordings.Rows[rowIndex].Cells[4].Text);
  }
  protected void btnRecordingsRSS_Click(object sender, ImageClickEventArgs e)
  {
    string url = Utils.GetStreamURL() + "/RSSGenerator.aspx?recordings=yes&idProfile=" + cbRecordingProfiles.SelectedIndex.ToString();
    RegisterStartupScript("rss", "<script>window.open('" + url + "');</script>");
  }
  #endregion

  #region Schedules
  protected void RefreshSchedules()
  {
    ServiceInterface server = new ServiceInterface();
    List<WebSchedule> scheds = server.GetAllSchedules();
    DataTable dt = new DataTable();
    dt.Columns.Add("startTime", typeof(string));
    dt.Columns.Add("endTime", typeof(string));
    dt.Columns.Add("channel", typeof(string));
    dt.Columns.Add("title", typeof(string));
    dt.Columns.Add("type", typeof(string));
    dt.Columns.Add("idSchedule", typeof(int));
    foreach (WebSchedule s in scheds)
    {
      DataRow row = dt.NewRow();
      row["startTime"] = s.startTime.ToString();
      row["endTime"] = s.endTime.ToString();
      row["channel"] = s.channelName;
      row["title"] = s.programName;
      row["type"] = s.scheduleTypeStr;
      row["idSchedule"] = s.idSchedule;
      dt.Rows.Add(row);
    }
    gridSchedules.DataSource = dt;
    gridSchedules.DataBind();
  }
  protected void gridSchedules_RowEditing(object sender, GridViewEditEventArgs e)
  {
    int idx = (int)gridSchedules.DataKeys[e.NewEditIndex].Value;
    e.Cancel = true;
    RegisterStartupScript("editschedule", "<script>window.open('ScheduleEditor.aspx?idSchedule=" + idx.ToString() + "');</script>");
  }
  protected void gridSchedules_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
    int idx = (int)gridSchedules.DataKeys[e.RowIndex].Value;
    ServiceInterface server = new ServiceInterface();
    server.DeleteSchedule(idx);
    e.Cancel = true;
    RefreshSchedules();
  }
  protected void btnRefresh_Click(object sender, EventArgs e)
  {
    RefreshSchedules();
  }
  #endregion

  #region Movies
  protected void RefreshMovies()
  {
    ServiceInterface server = new ServiceInterface();
    List<WebMovie> movies = server.GetAllMovies();
    DataTable dt = new DataTable();
    dt.Columns.Add("genre", typeof(string));
    dt.Columns.Add("file", typeof(string));
    dt.Columns.Add("title", typeof(string));
    dt.Columns.Add("plot", typeof(string));
    dt.Columns.Add("idMovie", typeof(int));
    foreach (WebMovie m in movies)
    {
      DataRow row = dt.NewRow();
      row["genre"] = m.genre;
      row["file"] = System.IO.Path.GetFileName(m.file);
      row["title"] = m.title;
      row["plot"] = m.plot;
      row["idMovie"] = m.idMovie;
      dt.Rows.Add(row);
    }
    gridMovies.DataSource = dt;
    gridMovies.DataBind();
    LoadStreamingProfiles(cbMovieProfiles);
  }
  protected void gridMovies_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    int rowIndex = Int32.Parse((string)e.CommandArgument);
    int idx = (int)gridMovies.DataKeys[rowIndex].Value;
    StartPlayer("idMovie=" + idx.ToString() + "&idProfile=" + cbMovieProfiles.SelectedIndex, gridMovies.Rows[rowIndex].Cells[2].Text);
  }
  protected void btnMovieRSS_Click(object sender, ImageClickEventArgs e)
  {
    string url = Utils.GetStreamURL() + "/RSSGenerator.aspx?movies=yes&idProfile=" + cbMovieProfiles.SelectedIndex.ToString();
    RegisterStartupScript("rss", "<script>window.open('" + url + "');</script>");
  }
  #endregion

  #region Music
  protected void RefreshMusicTracks()
  {
    ServiceInterface server = new ServiceInterface();
    List<WebMusicTrack> tracks = server.GetAllMusicTracks();
    DataTable dt = new DataTable();
    dt.Columns.Add("album", typeof(string));
    dt.Columns.Add("artist", typeof(string));
    dt.Columns.Add("trackno", typeof(int));
    dt.Columns.Add("title", typeof(string));
    dt.Columns.Add("idTrack", typeof(int));
    foreach (WebMusicTrack t in tracks)
    {
      DataRow row = dt.NewRow();
      row["album"] = t.album;
      row["artist"] = t.artist;
      row["trackno"] = t.trackno;
      row["title"] = t.title;
      row["idTrack"] = t.idTrack;
      dt.Rows.Add(row);
    }
    gridMusic.DataSource = dt;
    gridMusic.DataBind();
    LoadStreamingProfiles(cbMusicProfiles);
  }
  protected void gridMusic_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    int rowIndex = Int32.Parse((string)e.CommandArgument);
    int idx = (int)gridMusic.DataKeys[rowIndex].Value;
    StartPlayer("idMusicTrack=" + idx.ToString() + "&idProfile=" + cbMusicProfiles.SelectedIndex, gridMusic.Rows[rowIndex].Cells[4].Text);
  }
  protected void btnMusicRSS_Click(object sender, ImageClickEventArgs e)
  {
    string url = Utils.GetStreamURL() + "/RSSGenerator.aspx?music=yes&idProfile=" + cbMusicProfiles.SelectedIndex.ToString();
    RegisterStartupScript("rss", "<script>window.open('" + url + "');</script>");
  }
  #endregion

  #region Pictures
  protected void RefreshPictures()
  {
    ServiceInterface server = new ServiceInterface();
    List<string> p = server.GetAllPicturePaths();
    cbPicturePath.Items.Clear();
    foreach (string s in p)
      cbPicturePath.Items.Add(s);
    cbPicturePath.SelectedIndex = 0;
  }
  protected void btnShowPictures_Click(object sender, EventArgs e)
  {
    ServiceInterface server = new ServiceInterface();
    List<WebPicture> pics = server.GetAllPicturesByPath(cbPicturePath.SelectedValue);
    int width; int height;
    Utils.GetThumbDimensions(out width, out height);
    foreach (WebPicture pic in pics)
    {
      Image thumb = new Image();
      thumb.ImageUrl = Utils.GetStreamURL()+"/PictureStreamer.aspx?thumb=" + Server.UrlEncode(pic.file);
      thumb.ImageAlign = ImageAlign.Top;
      thumb.AlternateText = System.IO.Path.GetFileNameWithoutExtension(pic.file) + Environment.NewLine + pic.taken.ToString();
      thumb.BorderStyle = BorderStyle.Outset;
      thumb.BorderWidth = new Unit(1);
      
      thumb.Width = new Unit(width);
      thumb.Height = new Unit(height);
      thumb.Attributes.Add("hspace", "4");
      thumb.Attributes.Add("vspace", "4");
      thumb.Attributes.Add("onclick", "window.open('"+Utils.GetStreamURL()+"/PictureViewer.aspx?picture=" + Server.UrlEncode(pic.file) + "');");
      thumb.Style.Add("cursor", "hand");
      picBox.Controls.Add(thumb);
    }
  }
  #endregion

  #region TvSeries
  protected void RefreshTvSeries()
  {
    ServiceInterface server = new ServiceInterface();
    List<WebSeries> series = server.GetAllTvSeries();
    DataTable dt = new DataTable();
    dt.Columns.Add("compositeId", typeof(string));
    dt.Columns.Add("series", typeof(string));
    dt.Columns.Add("episode", typeof(string));
    foreach (WebSeries s in series)
    {
      DataRow row = dt.NewRow();
      row["compositeId"] = s.compositeId;
      row["series"] = s.seriesName;
      row["episode"] = "<b>S" + s.season.ToString() + "E" + s.episode.ToString() + " " + s.episodeName + "</b><br/>" + s.episodePlot;
      dt.Rows.Add(row);
    }
    gridTvSeries.DataSource = dt;
    gridTvSeries.DataBind();
    LoadStreamingProfiles(cbTvSeriesProfiles);
  }
  protected void gridTvSeries_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    int rowIndex = Int32.Parse((string)e.CommandArgument);
    StartPlayer("idTvSeries=" + gridTvSeries.DataKeys[rowIndex].Value + "&idProfile=" + cbTvSeriesProfiles.SelectedIndex, gridTvSeries.Rows[rowIndex].Cells[2].Text);
  }
  protected void btnSeriesRSS_Click(object sender, ImageClickEventArgs e)
  {
    string url = Utils.GetStreamURL() + "/RSSGenerator.aspx?tvseries=yes&idProfile=" + cbTvSeriesProfiles.SelectedIndex.ToString();
    RegisterStartupScript("rss", "<script>window.open('" + url + "');</script>");
  }
  #endregion

  #region MovingPictures
  protected void RefreshMovingPictures()
  {
    ServiceInterface server = new ServiceInterface();
    List<WebMovingPicture> pics = server.GetAllMovingPictures();
    DataTable dt = new DataTable();
    dt.Columns.Add("id", typeof(int));
    dt.Columns.Add("genre", typeof(string));
    dt.Columns.Add("parentalRating", typeof(string));
    dt.Columns.Add("movie", typeof(string));
    foreach (WebMovingPicture p in pics)
    {
      DataRow row = dt.NewRow();
      row["id"] = p.id;
      row["genre"] = p.genre;
      row["parentalRating"] = p.parentalRating;
      string s = "<b>" + p.title;
      if (p.year > 0)
        s += " (" + p.year.ToString() + ")";
      s += "</b><br/>" + p.plot;
      row["movie"] = s;
      dt.Rows.Add(row);
    }
    gridMovingPictures.DataSource = dt;
    gridMovingPictures.DataBind();
    LoadStreamingProfiles(cbMovingPicturesProfiles);
  }
  protected void gridMovingPictures_RowCommand(object sender, GridViewCommandEventArgs e)
  {
    int rowIndex = Int32.Parse((string)e.CommandArgument);
    int idx = (int)gridMovingPictures.DataKeys[rowIndex].Value;
    StartPlayer("idMovingPicture=" + idx.ToString() + "&idProfile=" + cbMovingPicturesProfiles.SelectedIndex, gridMovingPictures.Rows[rowIndex].Cells[3].Text);
  }
  protected void btnMovingPicturesRSS_Click(object sender, ImageClickEventArgs e)
  {
    string url = Utils.GetStreamURL() + "/RSSGenerator.aspx?movingpictures=yes&idProfile=" + cbMovingPicturesProfiles.SelectedIndex.ToString();
    RegisterStartupScript("rss", "<script>window.open('" + url + "');</script>");
  }
  #endregion

}
