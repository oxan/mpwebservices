using System;
using System.Collections;
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
using System.IO;


  public partial class Streamer : System.Web.UI.Page
  {
    int usedCard = -1;
    int usedChannel = -1;
    string tvServerUsername = "";

    const int bufferSize = 524288;

    protected void Page_Load(object sender, EventArgs e)
    {
      string filename="";
      ServiceInterface server = new ServiceInterface();
      if (Request.QueryString["idChannel"]!=null)
      {
        WebTvResult res = server.StartTimeShifting(Int32.Parse(Request.QueryString["idChannel"]));
        if (res.result != 0)
        {
          Response.Output.WriteLine("ERROR: StartTimeShifting failed with error: " + res.result.ToString());
          Response.End();
          return;
        }
        usedCard = res.user.idCard;
        usedChannel = res.user.idChannel;
        tvServerUsername = res.user.name;
        filename = res.timeshiftFile;
      }
      else if (Request.QueryString["idRecording"]!=null)
      {
        WebRecording rec=server.GetRecording(Int32.Parse(Request.QueryString["idMovie"]));
        filename = rec.fileName;
      }
      else if (Request.QueryString["idMovie"]!=null)
      {
        WebMovie movie=server.GetMovie(Int32.Parse(Request.QueryString["idMovie"]));
        filename=movie.file;
      }
      else if (Request.QueryString["idMusicTrack"]!=null)
      {
        WebMusicTrack track = server.GetMusicTrack(Int32.Parse(Request.QueryString["idMusicTrack"]));
        filename = track.file;
      }
      if (!File.Exists(filename))
        return;
      List<EncoderConfig> cfgs = Utils.LoadConfig();
      EncoderConfig cfg = cfgs[Int32.Parse(Request.QueryString["idProfile"])];
      if (!cfg.useTranscoding)
        Response.TransmitFile(filename);
      else
      {
        Stream mediaStream;
        if (Request.QueryString["idChannel"]!=null)
          mediaStream = new TsBuffer(filename);
        else
          mediaStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        EncoderWrapper encoder = new EncoderWrapper(mediaStream, cfg);
        byte[] buffer = new byte[bufferSize];
        int read;
        while ((read = mediaStream.Read(buffer, 0, buffer.Length)) > 0)
        {
          try
          {
            Response.OutputStream.Write(buffer, 0, read);
          }
          catch (Exception)
          {
            break; // stream is closed
          }
          server.SendHeartBeat(usedChannel, usedCard, tvServerUsername);
        }
        if (Request.QueryString["idChannel"] != null)
          server.StopTimeShifting(usedChannel, usedCard, tvServerUsername);
        encoder.StopProcess();
      }
    }
  }

