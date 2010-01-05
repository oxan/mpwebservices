using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using MediaPortal.Util;
using MediaPortal.TvServer.WebServices;


  public partial class PictureStreamer : System.Web.UI.Page
  {
    public string EncryptLine(string strLine)
    {
      if (strLine == null) return string.Empty;
      if (strLine.Length == 0) return string.Empty;
      CRCTool crc = new CRCTool();
      crc.Init(CRCTool.CRCCode.CRC32);
      ulong dwcrc = crc.calc(strLine);
      string strRet = String.Format("{0}", dwcrc);
      return strRet;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["authenticated"] == null)
        Response.Redirect("Login.aspx");
      string filename = "";
      string thumb = "";
      if (Request.QueryString["thumb"] != null)
      {
        ThumbPaths thumbs=Utils.GetThumbPaths();
        filename = Server.HtmlDecode(Request.QueryString["thumb"]);
        thumb = filename;
        thumb = EncryptLine(thumb);
        thumb = String.Format(thumbs.pictures+"\\" + thumb + Path.GetExtension(filename));
      }
      else if (Request.QueryString["picture"]!=null)
      {
        filename = Server.HtmlDecode(Request.QueryString["picture"]);
        thumb = filename;
        Response.AddHeader("Content-Disposition", "attachment;filename="+Path.GetFileName(filename)+";");
      }
      else if (Request.QueryString["tvlogo"] != null)
      {
        ThumbPaths paths=Utils.GetThumbPaths();
        thumb = paths.tv + "\\" + Request.QueryString["tvlogo"]+".png";
      }
      else if (Request.QueryString["radiologo"] != null)
      {
        ThumbPaths paths = Utils.GetThumbPaths();
        thumb = paths.radio + "\\" + Request.QueryString["radiologo"] + ".png";
      }
      if (!File.Exists(thumb))
        return;
      StreamReader fReader=new StreamReader(thumb);
      BinaryReader reader=new BinaryReader(fReader.BaseStream);
      FileInfo fi=new FileInfo(thumb);
      byte[] thumbData=new Byte[fi.Length];
      thumbData=reader.ReadBytes((int)fi.Length);
			Response.Clear();
      Response.ContentType = "image/" + Path.GetExtension(thumb).ToLower().Replace(".", "");
			Response.BinaryWrite(thumbData);
			Response.Flush();
			Response.End();
    }
  }

