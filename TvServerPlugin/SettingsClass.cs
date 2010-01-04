using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MediaPortal.TvServer.WebServices;
using System.IO;
using TvLibrary.Log;

namespace TvServerPlugin
{
  public struct DBLocations
  {
    public string db_movies;
    public string db_music;
    public string db_pictures;
    public string db_tvseries;
    public string db_movingpictures;
  }
  public struct ThumbPaths
  {
    public string tv;
    public string radio;
    public string pictures;
  }
  class Settings
  {
    public static string baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    private static string mpDbDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\database\";

    public static int httpPort;
    public static int thumbWidth;
    public static int thumbHeight;
    public static string clientPlayer;
    public static string streamURL;
    public static string scraperURL;
    public static int playerType;
    public static string webUid;
    public static string webPwd;
    public static DBLocations dbLocations;
    public static ThumbPaths thumbs;
    public static List<EncoderConfig> encCfgs;

    private static string SetDbLocation(XmlNode node, string dbName)
    {
      string fn = node.Attributes["filename"].Value;
      if (!File.Exists(fn))
      {
        if (File.Exists(mpDbDir + dbName))
          fn = mpDbDir + dbName;
        else
          fn = "";
      }
      return fn;
    }

    public static bool LoadSettings()
    {
      try
      {
        XmlDocument doc = new XmlDocument();
        Log.Info("MPWebServices: config.xml=" + baseDir + "\\MPWebServices\\htdocs\\config.xml");
        doc.Load(baseDir + "\\MPWebServices\\htdocs\\config.xml");
        XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
        httpPort = Int32.Parse(gNode.Attributes["httpport"].Value);
        thumbHeight = Int32.Parse(gNode.Attributes["thumbheight"].Value);
        thumbWidth = Int32.Parse(gNode.Attributes["thumbwidth"].Value);
        clientPlayer = gNode.Attributes["clientplayerpath"].Value;
        playerType = Int32.Parse(gNode.Attributes["playertype"].Value);
        streamURL = gNode.Attributes["streamurl"].Value;
        scraperURL = gNode.Attributes["scraper_url"].Value;
        if (streamURL == "")
          streamURL = "http://" + Environment.MachineName + ":" + httpPort.ToString();
        webUid = gNode.Attributes["username"].Value;
        webPwd = gNode.Attributes["password"].Value;
        XmlNodeList dbNodes = doc.SelectNodes("/appconfig/mpdatabases/database");
        foreach (XmlNode node in dbNodes)
        {
          switch (node.Attributes["name"].Value)
          {
            case "movies":
              dbLocations.db_movies = SetDbLocation(node, "VideoDatabaseV5.db3");
              break;
            case "music":
              dbLocations.db_music = SetDbLocation(node, "MusicDatabaseV11.db3");
              break;
            case "pictures":
              dbLocations.db_pictures = SetDbLocation(node, "PictureDatabase.db3");
              break;
            case "tvseries":
              dbLocations.db_tvseries = SetDbLocation(node, "TvSeriesDatabase4.db3");
              break;
            case "movingpictures":
              dbLocations.db_movingpictures = SetDbLocation(node, "movingpictures.db3");
              break;
          }
        }
        dbNodes = doc.SelectNodes("/appconfig/thumbpaths/thumb");
        foreach (XmlNode node in dbNodes)
        {
          switch (node.Attributes["name"].Value)
          {
            case "tv":
              thumbs.tv = node.Attributes["path"].Value;
              break;
            case "radio":
              thumbs.radio = node.Attributes["path"].Value;
              break;
            case "pictures":
              thumbs.pictures = node.Attributes["path"].Value;
              break;
          }
        }
        XmlNodeList nodes = doc.SelectNodes("/appconfig/transcoders/transcoder");
        encCfgs = new List<EncoderConfig>();
        foreach (XmlNode node in nodes)
        {
          EncoderConfig cfg = new EncoderConfig(node.Attributes["name"].Value, (node.Attributes["usetranscoding"].Value == "1"), node.Attributes["filename"].Value, node.Attributes["args"].Value, (TransportMethod)Int32.Parse(node.Attributes["inputmethod"].Value), (TransportMethod)Int32.Parse(node.Attributes["outputmethod"].Value));
          encCfgs.Add(cfg);
        }
      }
      catch (Exception ex)
      {
        Log.Error("MPWebServices: Exception raised while loading settings:" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
        return false;
      }
      return true;
    }

    private static void NewAttribute(XmlNode node, string name, string value)
    {
      XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
      attr.InnerText = value;
      node.Attributes.Append(attr);
    }
    private static void NewAttribute(XmlNode node, string name, bool value)
    {
      XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
      if (value)
        attr.InnerText = "1";
      else
        attr.InnerText = "0";
      node.Attributes.Append(attr);
    }
    private static void NewAttribute(XmlNode node, string name, int value)
    {
      XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
      attr.InnerText = value.ToString();
      node.Attributes.Append(attr);
    }
    private static void AddDbPath(string name, string path, XmlNode dbNode,XmlDocument doc)
    {
      XmlNode node = doc.CreateElement("database");
      NewAttribute(node, "name", name);
      NewAttribute(node, "filename", path);
      dbNode.AppendChild(node);
    }
    private static void AddThumbPath(string name, string path, XmlNode dbNode, XmlDocument doc)
    {
      XmlNode node = doc.CreateElement("thumb");
      NewAttribute(node, "name", name);
      NewAttribute(node, "path", path);
      dbNode.AppendChild(node);
    }
    public static void SaveSettings()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = doc.CreateElement("appconfig");
      XmlNode gNode = doc.CreateElement("config");
      NewAttribute(gNode, "httpport", httpPort);
      NewAttribute(gNode, "thumbwidth", thumbWidth);
      NewAttribute(gNode, "thumbheight", thumbHeight);
      NewAttribute(gNode, "clientplayerpath", clientPlayer);
      NewAttribute(gNode, "playertype", playerType);
      NewAttribute(gNode, "streamurl", streamURL);
      NewAttribute(gNode, "scraper_url", scraperURL);
      NewAttribute(gNode, "username", webUid);
      NewAttribute(gNode, "password", webPwd);

      XmlNode dbpaths = doc.CreateElement("mpdatabases");
      AddDbPath("movies", dbLocations.db_movies, dbpaths, doc);
      AddDbPath("music", dbLocations.db_music, dbpaths, doc);
      AddDbPath("pictures", dbLocations.db_pictures, dbpaths, doc);
      AddDbPath("tvseries", dbLocations.db_tvseries, dbpaths, doc);
      AddDbPath("movingpictures", dbLocations.db_movingpictures, dbpaths, doc);

      XmlNode thpaths = doc.CreateElement("thumbpaths");
      AddThumbPath("tv", thumbs.tv, thpaths, doc);
      AddThumbPath("radio", thumbs.radio, thpaths, doc);
      AddThumbPath("pictures", thumbs.pictures, thpaths, doc);

      XmlNode transcoders = doc.CreateElement("transcoders");
      foreach (EncoderConfig cfg in encCfgs)
      {
        XmlNode node = doc.CreateElement("transcoder");
        NewAttribute(node, "name", cfg.displayName);
        NewAttribute(node, "usetranscoding", cfg.useTranscoding);
        NewAttribute(node, "filename", cfg.fileName);
        NewAttribute(node, "args", cfg.args);
        NewAttribute(node, "inputmethod", (int)cfg.inputMethod);
        NewAttribute(node, "outputmethod", (int)cfg.outputMethod);
        transcoders.AppendChild(node);
      }
      root.AppendChild(gNode);
      root.AppendChild(transcoders);
      root.AppendChild(dbpaths);
      root.AppendChild(thpaths);
      doc.AppendChild(root);
      doc.Save(baseDir + "\\MPWebServices\\htdocs\\config.xml");
    }
  }
}
