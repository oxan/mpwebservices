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
      string filename = "";
      string thumb = "";
      if (Request.QueryString["thumb"] != null)
      {
        filename = Request.QueryString["thumb"];
        thumb = filename;
        thumb = EncryptLine(thumb);
        thumb = String.Format(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\thumbs\Pictures\" + thumb + Path.GetExtension(filename));
      }
      else
      {
        filename = Request.QueryString["picture"];
        thumb = filename;
        Response.AddHeader("Content-Disposition", "attachment;filename="+Path.GetFileName(filename)+";");
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

