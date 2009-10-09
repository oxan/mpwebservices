using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;
using System.IO;

using MediaPortal.TvServer.WebServices;

namespace MediaPortal.TvServer.WebServices
{
  public struct DBLocations
  {
    public string db_movies;
    public string db_music;
    public string db_pictures;
    public string db_tvseries;
    public string db_movingpictures;
  }

  public class Utils
  {
    public static List<EncoderConfig>  LoadConfig()
    {
      List<EncoderConfig> encCfgs = new List<EncoderConfig>();
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNodeList nodes = doc.SelectNodes("/appconfig/transcoders/transcoder");
      encCfgs = new List<EncoderConfig>();
      foreach (XmlNode node in nodes)
      {
        EncoderConfig cfg = new EncoderConfig(node.Attributes["name"].Value, (node.Attributes["usetranscoding"].Value == "1"), node.Attributes["filename"].Value, node.Attributes["args"].Value, (TransportMethod)Int32.Parse(node.Attributes["inputmethod"].Value), (TransportMethod)Int32.Parse(node.Attributes["outputmethod"].Value));
        encCfgs.Add(cfg);
      }
      return encCfgs;
    }
    public static string GetStreamURL()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
      return gNode.Attributes["streamurl"].Value;
    }
    public static void GetThumbDimensions(out int width,out int height)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
      width=Int32.Parse(gNode.Attributes["thumbwidth"].Value);
      height = Int32.Parse(gNode.Attributes["thumbheight"].Value);
    }
    public static string GetClientPlayerPath()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
      return gNode.Attributes["clientplayerpath"].Value;
    }
    public static int GetPlayerType()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
      return Int32.Parse(gNode.Attributes["playertype"].Value);
    }
    public static void GetLogin(out string uid,out string pwd)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
      uid=gNode.Attributes["username"].Value;
      pwd=gNode.Attributes["password"].Value;
    }
    public static DBLocations GetMPDbLocations()
    {
      DBLocations dbLocations = new DBLocations();
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNodeList dbNodes = doc.SelectNodes("/appconfig/mpdatabases/database");
      foreach (XmlNode node in dbNodes)
      {
        switch (node.Attributes["name"].Value)
        {
          case "movies":
            dbLocations.db_movies = node.Attributes["filename"].Value;
            break;
          case "music":
            dbLocations.db_music = node.Attributes["filename"].Value;
            break;
          case "pictures":
            dbLocations.db_pictures = node.Attributes["filename"].Value;
            break;
          case "tvseries":
            dbLocations.db_tvseries = node.Attributes["filename"].Value;
            break;
          case "movingpictures":
            dbLocations.db_movingpictures = node.Attributes["filename"].Value;
            break;
        }
      }
      return dbLocations;
    }
  }
}
