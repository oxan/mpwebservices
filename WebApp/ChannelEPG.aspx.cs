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

  public partial class ChannelEPG : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      ServiceInterface server = new ServiceInterface();
      Label1.Text = Request.QueryString["channelName"];
      List<WebProgram> progs=server.GetTodayEPGForChannel(Int32.Parse(Request.QueryString["idChannel"]));
      DataTable dt=new DataTable();
      dt.Columns.Add("time",typeof(string));
      dt.Columns.Add("genre", typeof(string));
      dt.Columns.Add("program",typeof(string));
      foreach (WebProgram p in progs)
      {
        DataRow row = dt.NewRow();
        row["time"] = p.startTime.ToString() + "-" + p.endTime.ToShortTimeString();
        row["genre"] = p.genre;
        row["program"] = "<b>" + p.Title + "</b><br/>" + p.description;
        dt.Rows.Add(row);
      }
      grid.DataSource = dt;
      grid.DataBind();
    }
  }

