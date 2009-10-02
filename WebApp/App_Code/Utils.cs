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
    public static string GetTvServerHostFromConfig()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
      XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
      return gNode.Attributes["tvserverhost"].Value;
    }
  }
}
