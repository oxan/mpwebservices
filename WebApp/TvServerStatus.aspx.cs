using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MediaPortal.TvServer.WebServices;
using MediaPortal.TvServer.WebServices.Classes;


  public partial class TvServerStatus : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      ServiceInterface server = new ServiceInterface();
      if (hfAction.Value != "")
      {
        if (hfAction.Value == "0")
          server.StopTimeShifting(Int32.Parse(hfIdChannel.Value), Int32.Parse(hfIdCard.Value), hfUsername.Value);
        else if (hfAction.Value == "1")
          server.StopRecording(Int32.Parse(hfIdChannel.Value), Int32.Parse(hfIdCard.Value), hfUsername.Value);
        hfAction.Value = "";
      }
      List<WebTvServerStatus> states = server.GetTvServerStatus();
      DataTable dt=new DataTable();
      dt.Columns.Add("name",typeof(string));
      dt.Columns.Add("type",typeof(string));
      dt.Columns.Add("state",typeof(string));
      dt.Columns.Add("channel",typeof(string));
      dt.Columns.Add("user",typeof(string));
      dt.Columns.Add("action", typeof(string));
      foreach (WebTvServerStatus state in states)
      {
        DataRow row = dt.NewRow();
        row["name"] = state.cardName;
        row["type"] = state.cardTypeStr;
        row["state"] = state.statusStr;
        row["channel"] = state.channel;
        row["user"] = state.userName;
        if ((CardStatus)state.status == CardStatus.TimeShifting)
        {
          row["action"]="<input type=button ID=\"btn\" Value=\"Stop timeshift\" onclick=KickSession("+state.idCard.ToString()+","+state.idChannel.ToString()+",'"+state.userName+"',0); />";
        }
        else if ((CardStatus)state.status == CardStatus.Recording)
        {
          row["action"] = "<input type=button ID=\"btn\" Value=\"Stop recording\" onclick=KickSession(" + state.idCard.ToString() + "," + state.idChannel.ToString() + ",'" + state.userName + "',1); />";
        }
        dt.Rows.Add(row);
      }
      grid.DataSource = dt;
      grid.DataBind();
    }
  }

