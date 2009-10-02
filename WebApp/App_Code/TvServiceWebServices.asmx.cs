using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using MediaPortal.TvServer.WebServices.Classes;
using TvDatabase;
using System.Xml;
using Gentle.Common;
using Gentle.Framework;
using TvControl;
using System.IO;

using System.Data.SQLite;


namespace MediaPortal.TvServer.WebServices
{
  /// <summary>
  /// Summary description for Service1
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [ToolboxItem(false)]
  public class ServiceInterface : System.Web.Services.WebService
  {
    private static bool isDBConnected=false;
    public string gentleConfig;
    public string connStr;

    #region Utils
    public bool ConnectToDatabase()
    {
      if (isDBConnected)
        return true;
      
      string provider = "";
      RemoteControl.HostName = Utils.GetTvServerHostFromConfig();
      try
      {
        RemoteControl.Instance.GetDatabaseConnectionString(out connStr, out provider);
        Gentle.Framework.ProviderFactory.SetDefaultProviderConnectionString(connStr);
      }
      catch (Exception)
      {
        return false;
      }
      isDBConnected=true;
      return true;
    }
    private string GetDateTimeString()
    {
      string provider = Gentle.Framework.ProviderFactory.GetDefaultProvider().Name.ToLower();
      if (provider == "mysql") return "yyyy-MM-dd HH:mm:ss";
      return "yyyyMMdd HH:mm:ss";
    }
    private string SafeStr(SQLiteDataReader reader, int idx)
    {
      if (reader.IsDBNull(idx))
        return "";
      else
        return reader.GetString(idx);
    }
    #endregion

    #region Channels
    [WebMethod]
    public List<WebChannel> GetAllChannels()
    {
      List<WebChannel> channels = new List<WebChannel>();
      if (!ConnectToDatabase())
        return channels;
      IList<Channel> dbChannels = Channel.ListAll();
      foreach (Channel ch in dbChannels)
        channels.Add(new WebChannel(ch));
      return channels;
    }
    [WebMethod]
    public WebChannel GetChannel(int idChannel)
    {
      if (!ConnectToDatabase())
        return new WebChannel();
      return new WebChannel(Channel.Retrieve(idChannel));
    }
    [WebMethod]
    public List<WebChannel> GetChannelsInTvGroup(int idGroup)
    {
      List<WebChannel> channels = new List<WebChannel>();
      if (!ConnectToDatabase())
        return channels;
      ChannelGroup group = null;
      try
      {
        group = ChannelGroup.Retrieve(idGroup);
      }
      catch (Exception)
      {
      }
      if (group == null)
        return channels;
      IList<GroupMap> maps = group.ReferringGroupMap();
      foreach (GroupMap map in maps)
        channels.Add(new WebChannel(map.ReferencedChannel()));
      return channels;
    }
    [WebMethod]
    public List<WebChannel> GetChannelsInRadioGroup(int idGroup)
    {
      List<WebChannel> channels = new List<WebChannel>();
      if (!ConnectToDatabase())
        return channels;
      RadioChannelGroup group = null;
      try
      {
        group = RadioChannelGroup.Retrieve(idGroup);
      }
      catch (Exception)
      {
      }
      if (group == null)
        return channels;
      IList<RadioGroupMap> maps = group.ReferringRadioGroupMap();
      foreach (RadioGroupMap map in maps)
        channels.Add(new WebChannel(map.ReferencedChannel()));
      return channels;
    }
    #endregion

    #region Tv
    [WebMethod]
    public List<WebChannelGroup> GetTvChannelGroups()
    {
      List<WebChannelGroup> ret = new List<WebChannelGroup>();
      if (!ConnectToDatabase())
        return ret;
      IList<ChannelGroup> groups=ChannelGroup.ListAll();
      foreach (ChannelGroup group in groups)
        ret.Add(new WebChannelGroup(group.IdGroup,group.GroupName));
      return ret;
    }
    [WebMethod]
    public List<WebMiniEPG> GetTvMiniEPGForGroup(int idGroup)
    {
      List<WebMiniEPG> ret = new List<WebMiniEPG>();
      if (!ConnectToDatabase())
        return ret;
      ChannelGroup group = null;
      try
      {
        group = ChannelGroup.Retrieve(idGroup);
      }
      catch (Exception)
      {
      }
      if (group == null)
        return ret;
      
      IList<GroupMap> maps = group.ReferringGroupMap();
      foreach (GroupMap map in maps)
        ret.Add(new WebMiniEPG(map.ReferencedChannel()));
      return ret;
    }
    #endregion

    #region Radio
    [WebMethod]
    public List<WebChannelGroup> GetRadioChannelGroups()
    {
      List<WebChannelGroup> ret = new List<WebChannelGroup>();
      if (!ConnectToDatabase())
        return ret;
      IList<RadioChannelGroup> groups = RadioChannelGroup.ListAll();
      foreach (RadioChannelGroup group in groups)
        ret.Add(new WebChannelGroup(group.IdGroup, group.GroupName));
      return ret;
    }
    [WebMethod]
    public List<WebMiniEPG> GetRadioMiniEPGForGroup(int idGroup)
    {
      List<WebMiniEPG> ret = new List<WebMiniEPG>();
      if (!ConnectToDatabase())
        return ret;
      RadioChannelGroup group = null;
      try
      {
        group = RadioChannelGroup.Retrieve(idGroup);
      }
      catch (Exception)
      {
      }
      if (group == null)
        return ret;

      IList<RadioGroupMap> maps = group.ReferringRadioGroupMap();
      foreach (RadioGroupMap map in maps)
        ret.Add(new WebMiniEPG(map.ReferencedChannel()));
      return ret;
    }
    #endregion

    #region EPG
    [WebMethod]
    public List<WebProgram> GetTodayEPGForChannel(int idChannel)
    {
      IFormatProvider mmddFormat = new System.Globalization.CultureInfo(String.Empty, false);
      List<WebProgram> infos = new List<WebProgram>();
      if (!ConnectToDatabase())
        return infos;
      SqlBuilder sb = new SqlBuilder(Gentle.Framework.StatementType.Select, typeof(Program));
      sb.AddConstraint(Operator.Equals, "idChannel", idChannel);
      DateTime thisMorning = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
      sb.AddConstraint(String.Format("startTime>='{0}'", thisMorning.ToString(GetDateTimeString(), mmddFormat)));
      sb.AddOrderByField(true, "startTime");
      SqlStatement stmt = sb.GetStatement(true);
      IList programs = ObjectFactory.GetCollection(typeof(Program), stmt.Execute());
      if (programs != null && programs.Count > 0)
      {
        foreach (Program prog in programs)
          infos.Add(new WebProgram(prog));
      }
      return infos;
    }
    [WebMethod]
    public List<WebProgram> GetEPGForChannel(string idChannel,string startTime,string endTime)
    {
      List<WebProgram> infos = new List<WebProgram>();
      if (!ConnectToDatabase())
        return infos;
      DateTime dtStart; DateTime dtEnd;
      if (!DateTime.TryParse(startTime,out dtStart))
        return infos;
      if (!DateTime.TryParse(endTime, out dtEnd))
        return infos;
      IFormatProvider mmddFormat = new System.Globalization.CultureInfo(String.Empty, false);
      try
      {
        SqlBuilder sb = new SqlBuilder(Gentle.Framework.StatementType.Select, typeof(Program));
        sb.AddConstraint(Operator.Equals, "idChannel", Int32.Parse(idChannel));
        sb.AddConstraint(String.Format("startTime>='{0}'", dtStart.ToString(GetDateTimeString(), mmddFormat)));
        sb.AddConstraint(String.Format("endTime<='{0}'", dtEnd.ToString(GetDateTimeString(), mmddFormat)));
        sb.AddOrderByField(true, "startTime");
        SqlStatement stmt = sb.GetStatement(true);
        IList programs = ObjectFactory.GetCollection(typeof(Program), stmt.Execute());
        if (programs != null && programs.Count > 0)
        {
          foreach (Program prog in programs)
            infos.Add(new WebProgram(prog));
        }
      }
      catch (Exception)
      {
      }
      return infos;
    }
    #endregion

    #region Schedules
    [WebMethod]
    public List<WebSchedule> GetAllSchedules()
    {
      List<WebSchedule> schedInfos = new List<WebSchedule>();
      if (!ConnectToDatabase())
        return schedInfos;
      IList<Schedule> schedules = Schedule.ListAll();
      foreach (Schedule schedule in schedules)
        schedInfos.Add(new WebSchedule(schedule));
      return schedInfos;
    }
    [WebMethod]
    public WebSchedule GetSchedule(int idSchedule)
    {
      if (!ConnectToDatabase())
        return new WebSchedule();
      return new WebSchedule(Schedule.Retrieve(idSchedule));
    }
    [WebMethod]
    public bool AddSchedule(int idChannel,string programName,DateTime startTime,DateTime endTime,int scheduleType)
    {
      if (!ConnectToDatabase())
        return false;
      Schedule sched = new Schedule(idChannel, programName, startTime, endTime);
      TvBusinessLayer layer = new TvBusinessLayer();
      sched.PreRecordInterval = Int32.Parse(layer.GetSetting("preRecordInterval", "5").Value);
      sched.PostRecordInterval = Int32.Parse(layer.GetSetting("postRecordInterval", "5").Value);
      sched.ScheduleType = scheduleType;
      sched.Persist();
      RemoteControl.Instance.OnNewSchedule();
      return true;
    }
    [WebMethod]
    public bool DeleteSchedule(int idSchedule)
    {
      if (!ConnectToDatabase())
        return false;
      try
      {
        Schedule sched = Schedule.Retrieve(idSchedule);
        sched.Remove();
        RemoteControl.Instance.OnNewSchedule();
      }
      catch (Exception)
      {
        return false;
      }
      return true;
    }
    #endregion

    #region Recordings
    [WebMethod]
    public List<WebRecording> GetAllRecordings()
    {
       List<WebRecording> recInfos = new List<WebRecording>();
       if (!ConnectToDatabase())
         return recInfos;
      IList<Recording> recordings = Recording.ListAll();
      foreach (Recording rec in recordings)
        recInfos.Add(new WebRecording(rec));
      return recInfos;
    }
    [WebMethod]
    public WebRecording GetRecording(int idRecording)
    {
      WebRecording rec = new WebRecording();
      if (!ConnectToDatabase())
        return rec;
      try
      {
        Recording recording = Recording.Retrieve(idRecording);
        rec = new WebRecording(recording);
      }
      catch (Exception)
      {
      }
      return rec;
    }
    [WebMethod]
    public string GetRecordingURL(int idRecording)
    {
      if (!ConnectToDatabase())
        return "";
      TvControl.TvServer server = new TvControl.TvServer();
      return server.GetStreamUrlForFileName(idRecording);
    }
    [WebMethod]
    public bool DeleteRecording(int idRecording)
    {
      if (!ConnectToDatabase())
        return false;
      return RemoteControl.Instance.DeleteRecording(idRecording);
    }
    #endregion

    #region TvServer Info functions
    [WebMethod]
    public WebReceptionDetails GetReceptionDetails()
    {
      WebReceptionDetails details = new WebReceptionDetails();
      if (!ConnectToDatabase())
        return details;
      VirtualCard vcard;
      try
      {
        vcard = new VirtualCard(new User("gemx",true), RemoteControl.HostName);
      }
      catch (Exception)
      {
        return details;
      }
      details.signalLevel = vcard.SignalLevel;
      details.signalQuality = vcard.SignalQuality;
      return details;
    }
    [WebMethod]
    public List<WebTvServerStatus> GetTvServerStatus()
    {
      List<WebTvServerStatus> states = new List<WebTvServerStatus>();
      if (!ConnectToDatabase())
        return states;
      VirtualCard vcard;
      try
      {
        IList<Card> cards = Card.ListAll();
        foreach (Card card in cards)
        {
          User user = new User();
          User[] usersForCard = null;
          user.CardId = card.IdCard;
          try
          {
            usersForCard = RemoteControl.Instance.GetUsersForCard(card.IdCard);
          }
          catch (Exception)
          {
          }
          if (usersForCard == null)
          {
            WebTvServerStatus state = new WebTvServerStatus();
            vcard = new VirtualCard(user, RemoteControl.HostName);
            string tmp = "idle";
            state.status = (int)CardStatus.Idle;
            if (vcard.IsScanning)
            {
              tmp = "Scanning";
              state.status = (int)CardStatus.Scanning;
            }
            if (vcard.IsGrabbingEpg)
            {
              tmp = "Grabbing EPG";
              state.status = (int)CardStatus.Grabbing_EPG;
            }
            state.idCard = card.IdCard;
            state.cardName = vcard.Name;
            state.cardType = (int)vcard.Type;
            state.cardTypeStr = vcard.Type.ToString();
            state.statusStr = tmp;
            state.channel = "";
            state.userName = "";
            states.Add(state);
            continue;
          }
          if (usersForCard.Length == 0)
          {
            WebTvServerStatus state = new WebTvServerStatus();
            vcard = new VirtualCard(user, RemoteControl.HostName);
            string tmp = "idle";
            state.status = (int)CardStatus.Idle;
            if (vcard.IsScanning)
            {
              tmp = "Scanning";
              state.status = (int)CardStatus.Scanning;
            }
            if (vcard.IsGrabbingEpg)
            {
              tmp = "Grabbing EPG";
              state.status = (int)CardStatus.Grabbing_EPG;
            }
            state.idCard = card.IdCard;
            state.cardName = vcard.Name;
            state.cardType = (int)vcard.Type;
            state.cardTypeStr = vcard.Type.ToString();
            state.statusStr = tmp;
            state.channel = "";
            state.userName = "";
            states.Add(state);
            continue;
          }
          for (int i = 0; i < usersForCard.Length; ++i)
          {
            WebTvServerStatus state = new WebTvServerStatus();
            string tmp = "idle";
            state.status = (int)CardStatus.Idle;
            vcard = new VirtualCard(usersForCard[i], RemoteControl.HostName);
            if (vcard.IsTimeShifting)
            {
              tmp = "Timeshifting";
              state.status = (int)CardStatus.TimeShifting;
            }
            else
              if (vcard.IsRecording)
              {
                tmp = "Recording";
                state.status = (int)CardStatus.Recording;
              }
              else
                if (vcard.IsScanning)
                {
                  tmp = "Scanning";
                  state.status = (int)CardStatus.Scanning;
                }
                else
                  if (vcard.IsGrabbingEpg)
                  {
                    tmp = "Grabbing EPG";
                    state.status = (int)CardStatus.Grabbing_EPG;
                  }
            state.idCard = card.IdCard;
            state.cardName = vcard.Name;
            state.cardType = (int)vcard.Type;
            state.cardTypeStr = vcard.Type.ToString();
            state.statusStr = tmp;
            state.idChannel = vcard.IdChannel;
            state.channel = vcard.ChannelName;
            state.userName = vcard.User.Name;
            states.Add(state);
          }
        }
      }
      catch (Exception ex)
      {
      }
      return states;
    }
    #endregion

    #region Movies
    [WebMethod]
    public WebMovie GetMovie(int idMovie)
    {
      WebMovie movie = new WebMovie();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\VideoDatabaseV5.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return movie;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT files.idFile,movieinfo.strGenre,movieinfo.strTitle,movieinfo.strPlot,(path.strPath || files.strFilename) as filename FROM files LEFT OUTER JOIN movieinfo ON (movieinfo.idMovie=files.idMovie) INNER JOIN path ON (path.idPath=files.idPath) WHERE files.idMovie="+idMovie.ToString();
      SQLiteDataReader reader = cmd.ExecuteReader();
      if (reader.Read())
        movie = new WebMovie(reader.GetInt32(0), SafeStr(reader,1),SafeStr(reader,2),SafeStr(reader,3),SafeStr(reader,4));
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return movie;
    }
    [WebMethod]
    public List<WebMovie> GetAllMovies()
    {
      List<WebMovie> movies = new List<WebMovie>();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\VideoDatabaseV5.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return movies;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT files.idMovie,movieinfo.strGenre,movieinfo.strTitle,movieinfo.strPlot,(path.strPath || files.strFilename) as filename FROM files LEFT OUTER JOIN movieinfo ON (movieinfo.idMovie=files.idMovie) INNER JOIN path ON (path.idPath=files.idPath)";
      SQLiteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
        movies.Add(new WebMovie(reader.GetInt32(0), SafeStr(reader,1),SafeStr(reader,2),SafeStr(reader,3),SafeStr(reader,4)));
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return movies;
    }
    #endregion

    #region Music
    [WebMethod]
    public WebMusicTrack GetMusicTrack(int idTrack)
    {
      WebMusicTrack track = new WebMusicTrack();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\MusicDatabaseV11.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return track;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT idTrack,strAlbum,strAlbumArtist,iTrack,strTitle,strPath FROM tracks WHERE idTrack=" + idTrack.ToString();
      SQLiteDataReader reader = cmd.ExecuteReader();
      if (reader.Read())
        track = new WebMusicTrack(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetInt32(3),reader.GetString(4),reader.GetString(5));
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return track;
    }
    [WebMethod]
    public List<WebMusicTrack> GetAllMusicTracks()
    {
      List<WebMusicTrack> tracks = new List<WebMusicTrack>();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\MusicDatabaseV11.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return tracks;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT idTrack,strAlbum,strAlbumArtist,iTrack,strTitle,strPath FROM tracks";
      SQLiteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
        tracks.Add(new WebMusicTrack(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5)));
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return tracks;
    }
    #endregion

    #region Pictures
    [WebMethod]
    public List<string> GetAllPicturePaths()
    {
      List<string> paths = new List<string>();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\PictureDatabase.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return paths;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT strFile FROM picture";
      SQLiteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        string p = reader.GetString(0);
        if (!paths.Contains(Path.GetDirectoryName(p)))
          paths.Add(Path.GetDirectoryName(p));
      }
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return paths;
    }
    public List<WebPicture> GetAllPictures()
    {
      List<WebPicture> pics = new List<WebPicture>();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\PictureDatabase.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return pics;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT * FROM picture";
      SQLiteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
        pics.Add(new WebPicture(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDateTime(3)));
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return pics;
    }
    [WebMethod]
    public List<WebPicture> GetAllPicturesByPath(string path)
    {
      List<WebPicture> pics = new List<WebPicture>();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\PictureDatabase.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return pics;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT * FROM picture WHERE strFile LIKE '"+path+"%'";
      SQLiteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        string s = reader.GetString(1);
        s = s.Remove(0, path.Length+1);
        if (s.IndexOf('\\')==-1)
          pics.Add(new WebPicture(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDateTime(3)));
      }
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return pics;
    }
    [WebMethod]
    public WebPicture GetPicture(int idPicture)
    {
      WebPicture pic = new WebPicture();
      SQLiteConnection db = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\PictureDatabase.db3;Pooling=true;Read Only=false");
      try
      {
        db.Open();
      }
      catch (Exception)
      {
        return pic;
      }
      SQLiteCommand cmd = db.CreateCommand();
      cmd.CommandText = "SELECT * FROM picture WHERE idPicture="+idPicture.ToString();
      SQLiteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
        pic=new WebPicture(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDateTime(3));
      reader.Close(); reader.Dispose(); cmd.Dispose(); db.Close(); db.Dispose();
      return pic;
    }
    #endregion

    #region Control functions
    [WebMethod]
    public WebTvResult StartTimeShifting(int idChannel)
    {
      
      ConnectToDatabase();
      VirtualCard vcard;
      TvResult result;
      string rtspURL="";
      string timeshiftFilename="";
      User me = new User(System.Guid.NewGuid().ToString("B"),false);
      try
      {
        result = RemoteControl.Instance.StartTimeShifting(ref me, idChannel, out vcard);
      }
      catch (Exception)
      {
        return new WebTvResult();
      }
      if (result == TvResult.Succeeded)
      {
        me.IdChannel = idChannel;
        me.CardId = vcard.Id;
        rtspURL = vcard.RTSPUrl;
        timeshiftFilename=vcard.TimeShiftFileName;
      }
      return new WebTvResult((int)result,rtspURL,timeshiftFilename,me);
    }
    [WebMethod]
    public bool StopTimeShifting(int idChannel,int idCard,string userName)
    {
      ConnectToDatabase();
      User user = new User(userName, false, idCard);
      user.IdChannel = idChannel;
      return RemoteControl.Instance.StopTimeShifting(ref user);
    }
    [WebMethod]
    public bool StopRecording(int idChannel, int idCard, string userName)
    {
      ConnectToDatabase();
      User user = new User(userName, false, idCard);
      user.IdChannel = idChannel;
      return RemoteControl.Instance.StopRecording(ref user);
    }
    [WebMethod]
    public void SendHeartBeat(int idChannel,int idCard,string userName)
    {
      ConnectToDatabase();
      User user = new User(userName, false, idCard);
      user.IdChannel = idChannel;
      RemoteControl.Instance.HeartBeat(user);
    }
    #endregion
  }
}
