﻿using System;
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
      if (Session["authenticated"] == null)
        Response.Redirect("Login.aspx");
      ServiceInterface server = new ServiceInterface();
      Label1.Text = Request.QueryString["channelName"];
      List<WebProgram> progs=server.GetTodayEPGForChannel(Int32.Parse(Request.QueryString["idChannel"]));
      DataTable dt=new DataTable();
      dt.Columns.Add("time",typeof(string));
      dt.Columns.Add("genre", typeof(string));
      dt.Columns.Add("program",typeof(string));
      dt.Columns.Add("idChannel", typeof(int));
      foreach (WebProgram p in progs)
      {
        DataRow row = dt.NewRow();
        row["time"] = p.startTime.ToString() + "-" + p.endTime.ToShortTimeString();
        row["genre"] = p.genre;
        row["program"] = "<b>" + p.Title + "</b><br/>" + p.description;
        row["idChannel"] = p.idChannel;
        dt.Rows.Add(row);
      }
      grid.DataSource = dt;
      grid.DataBind();
    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int rowIndex = Int32.Parse((string)e.CommandArgument);
      int idx = (int)grid.DataKeys[rowIndex].Value;
      RegisterStartupScript("newschedule", "<script>window.open('" + Utils.GetStreamURL()+"/ScheduleEditor.aspx?idChannel="+idx.ToString() + "');</script>");
    }
}

