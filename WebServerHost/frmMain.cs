using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using MediaPortal.TvServer.WebServices;

namespace WebServerHost
{
  public partial class frmMain : Form
  {
    int httpPort;
    string tvServerHost;
    int thumbWidth;
    int thumbHeight;
    string clientPlayer;
    int playerType;
    List<EncoderConfig> encCfgs;
    Cassini.Server webServer;

    public frmMain()
    {
      InitializeComponent();
      LoadConfig();
      nudPort.Value = (Decimal)httpPort;
      edHost.Text = tvServerHost;
      nudWidth.Value = thumbWidth;
      nudHeight.Value = thumbHeight;
      edPlayer.Text = clientPlayer;
      cbPlayerType.SelectedIndex = playerType;
      foreach (EncoderConfig cfg in encCfgs)
      {
        DataGridViewRow row = new DataGridViewRow();
        row.CreateCells(grid);
        row.Cells[0].Value = cfg.displayName;
        row.Cells[1].Value = cfg.useTranscoding;
        row.Cells[2].Value = cfg.fileName;
        row.Cells[3].Value = cfg.args;
        row.Cells[4].Value = cfg.inputMethod.ToString();
        row.Cells[5].Value = cfg.outputMethod.ToString();
        grid.Rows.Add(row);
      }
      grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
    }

    #region Persistance
    private void LoadConfig()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(Application.StartupPath + "\\htdocs\\config.xml");
      XmlNode gNode = doc.SelectSingleNode("/appconfig/config");
      httpPort = Int32.Parse(gNode.Attributes["httpport"].Value);
      tvServerHost = gNode.Attributes["tvserverhost"].Value;
      if (tvServerHost == "")
        tvServerHost = Environment.MachineName;
      thumbHeight = Int32.Parse(gNode.Attributes["thumbheight"].Value);
      thumbWidth = Int32.Parse(gNode.Attributes["thumbwidth"].Value);
      clientPlayer = gNode.Attributes["clientplayerpath"].Value;
      playerType = Int32.Parse(gNode.Attributes["playertype"].Value);
      XmlNodeList nodes=doc.SelectNodes("/appconfig/transcoders/transcoder");
      encCfgs=new List<EncoderConfig>();
      foreach (XmlNode node in nodes)
      {
        EncoderConfig cfg = new EncoderConfig(node.Attributes["name"].Value, (node.Attributes["usetranscoding"].Value == "1"), node.Attributes["filename"].Value, node.Attributes["args"].Value, (TransportMethod)Int32.Parse(node.Attributes["inputmethod"].Value), (TransportMethod)Int32.Parse(node.Attributes["outputmethod"].Value));
        encCfgs.Add(cfg);
      }
    }

    public void NewAttribute(XmlNode node, string name, string value)
    {
      XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
      attr.InnerText = value;
      node.Attributes.Append(attr);
    }
    public void NewAttribute(XmlNode node, string name, bool value)
    {
      XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
      if (value)
        attr.InnerText = "1";
      else
        attr.InnerText = "0";
      node.Attributes.Append(attr);
    }
    public void NewAttribute(XmlNode node, string name, int value)
    {
      XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
      attr.InnerText = value.ToString();
      node.Attributes.Append(attr);
    }

    private void SaveConfig()
    {
      XmlDocument doc = new XmlDocument();
      XmlNode root = doc.CreateElement("appconfig");
      XmlNode gNode = doc.CreateElement("config");
      NewAttribute(gNode, "httpport", httpPort);
      NewAttribute(gNode, "tvserverhost", tvServerHost);
      NewAttribute(gNode, "thumbwidth", thumbWidth);
      NewAttribute(gNode, "thumbheight", thumbHeight);
      NewAttribute(gNode, "clientplayerpath", clientPlayer);
      NewAttribute(gNode, "playertype", playerType);

      XmlNode transcoders = doc.CreateElement("transcoders");
      foreach (EncoderConfig cfg in encCfgs)
      {
        XmlNode node = doc.CreateElement("transcoder");
        NewAttribute(node, "name", cfg.displayName);
        NewAttribute(node, "usetranscoding", cfg.useTranscoding);
        NewAttribute(node, "filename", cfg.fileName);
        NewAttribute(node, "args", cfg.args);
        NewAttribute(node, "inputmethod", (int)cfg.inputMethod);
        NewAttribute(node, "outputmethod", (int)cfg.outputMethod);
        transcoders.AppendChild(node);
      }

      root.AppendChild(gNode);
      root.AppendChild(transcoders);
      doc.AppendChild(root);
      doc.Save(Application.StartupPath + "\\htdocs\\config.xml");
    }
    #endregion

    private TransportMethod GetTransportMethod(string s)
    {
      switch (s)
      {
        case "NamedPipes":
          return TransportMethod.NamedPipe;
        case "StandardIn":
          return TransportMethod.StandardIn;
        case "StandardOut":
          return TransportMethod.StandardOut;
      }
      return TransportMethod.NamedPipe;
    }
    private void BuildConfig()
    {
      httpPort = (int)nudPort.Value;
      tvServerHost = edHost.Text;
      thumbWidth = (int)nudWidth.Value;
      thumbHeight = (int)nudHeight.Value;
      clientPlayer = edPlayer.Text;
      playerType = cbPlayerType.SelectedIndex;
      encCfgs.Clear();
      foreach (DataGridViewRow row in grid.Rows)
      {
        if (row.Cells[0].Value!=null)
          encCfgs.Add(new EncoderConfig(row.Cells[0].Value.ToString(), (bool)row.Cells[1].Value, row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), GetTransportMethod(row.Cells[4].Value.ToString()), GetTransportMethod(row.Cells[5].Value.ToString())));
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      BuildConfig();
      SaveConfig();
    }

    private void btnStopServer_Click(object sender, EventArgs e)
    {
      webServer.Stop();
      webServer = null;
      linkLabel1.Visible = false;
      linkLabel2.Visible = false;
      toolStripStatusLabel1.Text = "WebServer stopped.";
    }

    private void btnStartServer_Click(object sender, EventArgs e)
    {
      button1_Click(sender, e);
      toolStripStatusLabel1.Text = "Starting webserver... This might take several seconds...";
      Update();
      webServer = new Cassini.Server((int)nudPort.Value, "/", Application.StartupPath+"\\htdocs");
      webServer.Start();
      linkLabel1.Text="http://"+Environment.MachineName+":"+nudPort.Value.ToString();
      linkLabel1.Visible = true;
      linkLabel2.Text = "http://" + Environment.MachineName + ":" + nudPort.Value.ToString()+"/TvServiceWebServices.asmx";
      linkLabel2.Visible = true;
      toolStripStatusLabel1.Text = "WebServer started.";
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process proc = new System.Diagnostics.Process();
      proc.StartInfo.FileName = linkLabel1.Text;
      proc.Start();
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process proc = new System.Diagnostics.Process();
      proc.StartInfo.FileName = linkLabel2.Text;
      proc.Start();
    }
  }
}
