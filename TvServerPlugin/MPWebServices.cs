using System;
using System.Collections.Generic;
using System.Text;
using TvLibrary.Log;
using TvControl;

namespace TvServerPlugin
{
  public class MPWebServices: TvEngine.ITvServerPlugin
  {
    private Cassini.Server webServer = null;
    
    #region Properties
    /// <summary>
    /// returns the name of the plugin
    /// </summary>
    public string Name
    {
      get { return "MediaPortal WebServices"; }
    }
    /// <summary>
    /// returns the version of the plugin
    /// </summary>
    public string Version
    {
      get { return "1.0.0.2"; }
    }
    /// <summary>
    /// returns the author of the plugin
    /// </summary>
    public string Author
    {
      get { return "gemx (Andreas Kwasnik)"; }
    }
    /// <summary>
    /// returns if the plugin should only run on the master server
    /// or also on slave servers
    /// </summary>
    public bool MasterOnly
    {
      get { return true; }
    }
    #endregion Properties

    #region IPlugin Members
    public void Start(IController controller)
    {
      System.Threading.Thread th = new System.Threading.Thread(StartPlugin);
      th.Start();
    }
    public void Stop()
    {
      StopPlugin();
    }
    public SetupTv.SectionSettings Setup
    {
      get { return new MPWebServicesSetup(); }
    }
    #endregion

    private void StartPlugin()
    {
      Log.Info("MPWebServices: start");
      if (!Settings.LoadSettings())
      {
        Log.Error("MPWebServices: settings are invalid. Can't start web server!!!");
        return;
      }
      Log.Info("MPWebServices: Starting web server...");
      try
      {
        webServer = new Cassini.Server(Settings.httpPort, "/", Settings.baseDir + "\\MPWebServices\\htdocs");
        webServer.Start();
      }
      catch (Exception ex)
      {
        Log.Error("MPWebServices: Exception raised while trying to start webserver. " + ex.Message);
        webServer = null;
        return;
      }
      Log.Info("MPWebServices: web server started.");
    }
    private void StopPlugin()
    {
      if (webServer != null)
      {
        Log.Info("plugin: MPWebServices stop");
        webServer.Stop();
        webServer = null;
      }
    }
  }
}
