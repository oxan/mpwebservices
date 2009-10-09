using System;
using System.Data;
using System.Configuration;
using System.Web;

using MediaPortal.TvServer.WebServices;

namespace MediaPortal.TvServer.WebServices
{
  public class EncoderConfig
  {
    public string displayName;
    public bool useTranscoding;
    public string fileName;
    public string args;
    public TransportMethod inputMethod;
    public TransportMethod outputMethod;

    public EncoderConfig(string displayName,bool useTranscoding, string fileName, string args, TransportMethod inputMethod, TransportMethod outputMethod)
    {
      this.displayName = displayName;
      this.useTranscoding = useTranscoding;
      this.fileName = fileName;
      this.args = args;
      this.inputMethod = inputMethod;
      this.outputMethod = outputMethod;
    }
  }
}
