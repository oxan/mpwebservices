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
using MediaPortal.TvServer.WebServices;
using MediaPortal.TvServer.WebServices.Classes;

public partial class ScheduleEditor : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["authenticated"] == null)
      Response.Redirect("Login.aspx");
    if (IsPostBack)
      return;
    ServiceInterface server = new ServiceInterface();
    if (Request.QueryString["idSchedule"] != null)
    {
      lHeading.Text = "Edit Schedule";
      WebSchedule sched = server.GetSchedule(Int32.Parse(Request.QueryString["idSchedule"]));
      lChannel.Text = sched.channelName;
      hfIdChannel.Value = sched.idChannel.ToString();
      edStart.Text = sched.startTime.ToString();
      edEnd.Text = sched.endTime.ToString();
      edTitle.Text = sched.programName;
      cbScheduleType.SelectedValue = sched.scheduleType.ToString();
    }
    else if (Request.QueryString["idProgram"] != null)
    {
      WebProgram p = server.GetEPG(Int32.Parse(Request.QueryString["idProgram"]));
      lChannel.Text = p.channelName;
      hfIdChannel.Value = p.idChannel.ToString();
      cbChannel.SelectedValue = p.idChannel.ToString();
      edStart.Text = p.startTime.ToString();
      edEnd.Text = p.endTime.ToString();
      edTitle.Text = p.Title;
    }
  }

  protected void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
  {
    ServiceInterface server = new ServiceInterface();
    List<WebChannel> channels=null;
    if (cbChannelType.SelectedIndex == 1)
      channels = server.GetChannelsInTvGroup(Int32.Parse(cbGroup.SelectedValue));
    else if (cbChannelType.SelectedIndex == 2)
      channels = server.GetChannelsInRadioGroup(Int32.Parse(cbGroup.SelectedValue));
    cbChannel.Items.Clear();
    foreach (WebChannel ch in channels)
      cbChannel.Items.Add(new ListItem(ch.displayName, ch.idChannel.ToString()));
    cbChannel.SelectedIndex = 0;
    lChannel.Text = cbChannel.SelectedItem.Text;
    hfIdChannel.Value = cbChannel.SelectedItem.Value;   
  }

  protected void cbChannelType_SelectedIndexChanged(object sender, EventArgs e)
  {
    ServiceInterface server = new ServiceInterface();
    cbGroup.Items.Clear();
    List<WebChannelGroup> groups=null;
    if (cbChannelType.SelectedIndex == 1)
      groups = server.GetTvChannelGroups();
    else if (cbChannelType.SelectedIndex == 2)
      groups = server.GetRadioChannelGroups();
    else
    {
      cbGroup.Items.Clear();
      cbChannel.Items.Clear();
      return;
    }

    int idx = 0;
    int allIndex = -1;
    int index = -1;
    foreach (WebChannelGroup g in groups)
    {
      if (g.name == "All Channels" && allIndex == -1)
        allIndex = idx;
      if (g.name != "All Channels" && index == -1)
        index = idx;
      cbGroup.Items.Add(new ListItem(g.name, g.idGroup.ToString()));
      idx++;
    }
    if (index != -1)
      cbGroup.SelectedIndex = index;
    else
      cbGroup.SelectedIndex = allIndex;
    cbGroup_SelectedIndexChanged(sender, e);
  }

  protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
  {
    DateTime dt;
    args.IsValid = DateTime.TryParse(args.Value, out dt);
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    int idChannel;
    if (!Int32.TryParse(hfIdChannel.Value,out idChannel))
    {
      lChanError.Visible=true;
      return;
    }
    ServiceInterface server = new ServiceInterface();
    if (Request.QueryString["idSchedule"]!=null)
      server.DeleteSchedule(Int32.Parse(Request.QueryString["idSchedule"]));
    server.AddSchedule(idChannel, edTitle.Text, DateTime.Parse(edStart.Text), DateTime.Parse(edEnd.Text), Int32.Parse(cbScheduleType.SelectedValue));
    RegisterStartupScript("close", "<script>window.close();</script>");
  }
  protected void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
  {
    lChannel.Text = cbChannel.SelectedItem.Text;
    hfIdChannel.Value = cbChannel.SelectedItem.Value;
    
  }
}
