using System;
using System.Collections.Generic;
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
using MediaPortal.TvServer.WebServices;
using MediaPortal.TvServer.WebServices.Classes;

  public partial class EPGSearch : System.Web.UI.Page
  {
    private string GetScraperLink(string s)
    {
      string url = Utils.GetScraperURL();
      url = String.Format(url, s);
      return "<a href=\"" + url + "\" target=_blank>" + s + "</a>";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["authenticated"] == null)
        Response.Redirect("Login.aspx");
      if (!IsPostBack)
      {
        if (Request.QueryString["title"] != null)
        {
          edTitle.Text = Server.HtmlDecode(Request.QueryString["title"]+"%");
          ExecSearch();
        }
      }
      
    }
    protected void ExecSearch()
    {
      ServiceInterface server = new ServiceInterface();
      List<WebProgram> progs = server.SearchEPG(edTitle.Text);
      DataTable dt = new DataTable();
      dt.Columns.Add("time", typeof(string));
      dt.Columns.Add("genre", typeof(string));
      dt.Columns.Add("channel", typeof(string));
      dt.Columns.Add("program", typeof(string));
      dt.Columns.Add("logo", typeof(string));
      dt.Columns.Add("idProgram", typeof(int));
      foreach (WebProgram p in progs)
      {
        DataRow row = dt.NewRow();
        string s = p.startTime.ToString() + "-" + p.endTime.ToShortTimeString();
        if (p.startTime < DateTime.Now && p.endTime > DateTime.Now)
          s = "<a name=\"currentdatetime\"></a>" + s;
        row["time"] = s;
        row["genre"] = p.genre;
        row["channel"] = p.channelName;
        row["program"] = "<b>" + GetScraperLink(p.Title) + "</b><br/>" + p.description;
        row["logo"]=Utils.GetStreamURL() + "/PictureStreamer.aspx?tvlogo=" + Server.HtmlEncode(p.channelName);
        row["idProgram"] = p.idProgram;
        dt.Rows.Add(row);
      }
      grid.DataSource = dt;
      grid.DataBind();
      int width; int height;
      Utils.GetThumbDimensions(out width, out height);
      grid.Columns[2].ControlStyle.Width = width;
      grid.Columns[2].ControlStyle.Height = height;
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int rowIndex = Int32.Parse((string)e.CommandArgument);
      int idx = (int)grid.DataKeys[rowIndex].Value;
      RegisterStartupScript("newschedule", "<script>window.open('" + Utils.GetStreamURL()+"/ScheduleEditor.aspx?idProgram="+idx.ToString() + "');</script>");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
      ExecSearch();
    }
}

