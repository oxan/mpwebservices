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


  public partial class PictureViewer : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["authenticated"] == null)
        Response.Redirect("Login.aspx");
      Image img = new Image();
      img.ImageUrl = "PictureStreamer.aspx?picture=" + Request.QueryString["picture"];
      img.AlternateText = System.IO.Path.GetFileNameWithoutExtension(Server.UrlDecode(Request.QueryString["picture"]));
      picBox.Controls.Add(img);
    }
  }

