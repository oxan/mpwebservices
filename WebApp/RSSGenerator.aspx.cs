using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using MediaPortal.TvServer.WebServices;
using MediaPortal.TvServer.WebServices.Classes;

public partial class RSSGenerator : System.Web.UI.Page
{
  string rss="";

  protected void AddHeader(string title,string description)
  {
    title = Server.HtmlEncode(title);
    description = Server.HtmlEncode(description);
    rss = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + "<rss version=\"2.0\">" + Environment.NewLine;
    rss += "  <channel>" + Environment.NewLine;
    rss += "    <title>" + title + "</title>" + Environment.NewLine;
    rss += "    <description>" + description + "</description>" + Environment.NewLine;
    rss += "    <link>" + Server.HtmlEncode(Utils.GetStreamURL() +"/RSSGenerator.aspx?" + Request.QueryString.ToString()) + "</link>" + Environment.NewLine;

  }
  protected void AddFooter()
  {
    rss += "  </channel>" + Environment.NewLine;
    rss += "</rss>";
  }
  protected void AddItem(string title, string description,string link)
  {
    title = Server.HtmlEncode(title);
    description = Server.HtmlEncode(description);
    link = Server.HtmlEncode(link);
    rss += "    <item>" + Environment.NewLine;
    rss += "      <title>" + title + "</title>" + Environment.NewLine;
    rss += "      <description>" + description + "</description>" + Environment.NewLine;
    rss += "      <link>" + link + "</link>" + Environment.NewLine;
    rss += "    </item>" + Environment.NewLine;
  }
  protected void Page_Load(object sender, EventArgs e)
  {
    string streamURL=Utils.GetStreamURL();
    ServiceInterface server = new ServiceInterface();
    if (Request.QueryString["idTvGroup"] != null)
    {
      AddHeader("Tv MiniEPG for [" + Request.QueryString["groupName"] + "]","");
      List<WebMiniEPG> channels=server.GetTvMiniEPGForGroup(Int32.Parse(Request.QueryString["idTvGroup"]));
      foreach (WebMiniEPG chan in channels)
        AddItem(chan.name, chan.epgNow.startTime.ToString() + "-" + chan.epgNow.endTime.ToString() + " " + chan.epgNow.title, streamURL + "/Streamer.aspx?idChannel=" + chan.idChannel.ToString() + "&idProfile=" + Request.QueryString["idProfile"]);
    }
    else if (Request.QueryString["idRadioGroup"] != null)
    {
      AddHeader("Radio MiniEPG for [" + Request.QueryString["groupName"] + "]","");
      List<WebMiniEPG> channels = server.GetRadioMiniEPGForGroup(Int32.Parse(Request.QueryString["idRadioGroup"]));
      foreach (WebMiniEPG chan in channels)
        AddItem(chan.name, chan.epgNow.startTime.ToString() + "-" + chan.epgNow.endTime.ToString() + " " + chan.epgNow.title, streamURL + "/Streamer.aspx?idChannel=" + chan.idChannel.ToString() + "&idProfile=" + Request.QueryString["idProfile"]);
    }
    else if (Request.QueryString["recordings"] != null)
    {
      AddHeader("Recordings","");
      List<WebRecording> recs = server.GetAllRecordings();
      foreach (WebRecording rec in recs)
        AddItem(rec.title,rec.description,streamURL + "/Streamer.aspx?idRecording=" + rec.idRecording.ToString() + "&idProfile=" + Request.QueryString["idProfile"]);
    }
    else if (Request.QueryString["movies"] != null)
    {
      AddHeader("Movies","");
      List<WebMovie> movies=server.GetAllMovies();
      foreach (WebMovie m in movies)
        AddItem(m.title,m.plot,streamURL + "/Streamer.aspx?idMovie=" + m.idMovie.ToString() + "&idProfile=" + Request.QueryString["idProfile"]);
    }
    else if (Request.QueryString["music"] != null)
    {
      AddHeader("Music","");
      List<WebMusicTrack> tracks=server.GetAllMusicTracks();
      foreach (WebMusicTrack track in tracks)
        AddItem(track.album,track.title,streamURL + "/Streamer.aspx?idMusicTrack=" + track.idTrack.ToString() + "&idProfile=" + Request.QueryString["idProfile"]);
    }
    else if (Request.QueryString["tvseries"] != null)
    {
      AddHeader("TV Series","");
      List<WebSeries> series=server.GetAllTvSeries();
      foreach (WebSeries s in series)
        AddItem(s.seriesName,s.episodeName,streamURL + "/Streamer.aspx?idTvSeries=" + s.compositeId + "&idProfile=" + Request.QueryString["idProfile"]);
    }
    else if (Request.QueryString["movingpictures"] != null)
    {
      AddHeader("Moving Pictures","");
      List<WebMovingPicture> mpics=server.GetAllMovingPictures();
      foreach (WebMovingPicture m in mpics)
        AddItem(m.title,m.plot,streamURL + "/Streamer.aspx?idMovingPicture=" + m.id.ToString() + "&idProfile=" + Request.QueryString["idProfile"]);
    }
    AddFooter();
    Response.Clear();
    Response.ContentType = "application/rss+xml";
    Response.Write(rss);
    Response.End();
  }
}
