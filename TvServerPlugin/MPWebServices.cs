using System;
using System.Collections.Generic;
using System.Text;
using TvLibrary.Log;
using TvControl;

namespace TvServerPlugin
{
  class MPWebServices: TvEngine.ITvServerPlugin
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
      get { return "Andreas Kwasnik"; }
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
    [CLSCompliant(false)]
    public void Start(IController controller)
    {
      Log.Info("MPWebServices: start");
      if (!LoadSettings())
      {
        Log.Error("MPWebServices: settings are invalid. Can't start web server!!!");
        return;
      }
      Log.Info("MPWebServices: Starting web server...");
      webServer = new Cassini.Server(port, "/", Application.StartupPath + "\\plugins\\MPWebServices\\htdocs");
      webServer.Start();
      Log.Info("MPWebServices: web server started.");
    }
    public void Stop()
    {
      Log.Info("plugin: MPWebServices stop");
      webServer.Stop();
      webServer = null;
    }
    [CLSCompliant(false)]
    public SetupTv.SectionSettings Setup
    {
      get { return new SetupTv.Sections.ComSkipSetup(); }
    }
    #endregion
  }
}
