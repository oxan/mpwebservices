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
using System.Diagnostics;
using System.Management;


  public partial class TvServerStatus : System.Web.UI.Page
  {
    private static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["authenticated"] == null)
        Response.Redirect("Login.aspx");
      List<string> recPaths = new List<string>();
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
        if (!recPaths.Contains(state.recordingFolder))
          recPaths.Add(state.recordingFolder);
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
      RefreshServerInfo(recPaths);
    }

    private void RefreshServerInfo(List<string> recPaths)
    {
      string manufacturer = "";
      string model = "";
      string computername = "";
      int totalPhysicalMem = 0;

      ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT model,manufacturer,name,totalPhysicalMemory FROM Win32_ComputerSystem");
      ManagementObjectCollection queryCollection = query.Get();
      foreach (ManagementObject mo in queryCollection)
      {
        model = mo["model"].ToString();
        manufacturer = mo["manufacturer"].ToString();
        computername = mo["name"].ToString();
        totalPhysicalMem = Convert.ToInt32(mo["totalphysicalmemory"]);
        totalPhysicalMem = (totalPhysicalMem / 1024) / 1024;
      }

      lMachinename.Text = computername + " (" + manufacturer + " - " + model + ")";
      lOS.Text = GetOSVersionStr();

      float cpuUsage=cpuCounter.NextValue();
      if (cpuUsage == 0)
      {
        System.Threading.Thread.Sleep(1000);
        cpuUsage = cpuCounter.NextValue();
      }

      lCPU.Text = Convert.ToInt32(cpuUsage).ToString() + " %";

      PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
      lMemory.Text = ramCounter.NextValue() + " Mb / "+totalPhysicalMem.ToString()+ "Mb";

      string usages="";
      foreach (string drive in recPaths)
        usages+=GetDriveUsageStr(drive.Substring(0,2))+"<br/>";
      lSpace.Text=usages;
    }
    protected string GetOSVersionStr()
    {
      string ret = "";
      ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT Caption,BuildNumber,BuildType,OSArchitecture FROM Win32_OperatingSystem");
      ManagementObjectCollection queryCollection=query.Get();

      foreach (ManagementObject os in queryCollection)
        ret=os["Caption"].ToString() + " (Build " + os["BuildNumber"].ToString() + " - " + os["BuildType"] + " - " + os["OSArchitecture"]+")";
      return ret;
    }

    protected string GetDriveUsageStr(string driveLetter)
    {
      string ret="";
      try
      {
        ManagementObject drive = new ManagementObject("Win32_LogicalDisk.DeviceID=\""+driveLetter+"\"");
        drive.Get();
        UInt64 total=(UInt64)drive["Size"];
        UInt64 free=(UInt64)drive["FreeSpace"];
        UInt64 used=total-free;
        total=(total/1024)/1024;
        used=(used/1024)/1024;
        free = (free / 1024) / 1024;
        ret=driveLetter+" free:  "+free.ToString()+" Mb  used: "+used.ToString()+" Mb / "+total.ToString()+" Mb";
      }
      catch (Exception)
      {
        return driveLetter+" (unknown)";
      }
      return ret;
    }
  }

