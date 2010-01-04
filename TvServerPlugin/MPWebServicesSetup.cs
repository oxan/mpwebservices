using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MediaPortal.TvServer.WebServices;
using TvLibrary.Log;

namespace TvServerPlugin
{
  public partial class MPWebServicesSetup : SetupTv.SectionSettings
  {
    public MPWebServicesSetup()
    {
      InitializeComponent();
    }

    #region SetupTv.SectionSettings
    public override void OnSectionDeActivated()
    {
      Log.Info("MPWebServices: Configuration deactivated");
      SaveConfig();
      base.OnSectionDeActivated();
    }
    public override void OnSectionActivated()
    {
      Log.Info("MPWebServices: Configuration activated");
      LoadConfig();
      base.OnSectionActivated();
    }
    #endregion SetupTv.SectionSettings

    #region Persistance
    private TransportMethod GetTransportMethod(string s)
    {
      switch (s)
      {
        case "Filename":
          return TransportMethod.Filename;
        case "NamedPipes":
          return TransportMethod.NamedPipe;
        case "StandardIn":
          return TransportMethod.StandardIn;
        case "StandardOut":
          return TransportMethod.StandardOut;
      }
      return TransportMethod.NamedPipe;
    }
    public void LoadConfig()
    {
      Settings.LoadSettings();
      
      nudPort.Value = Settings.httpPort;
      cbPlayerType.SelectedIndex = Settings.playerType;
      edPlayer.Text = Settings.clientPlayer;
      edUid.Text = Settings.webUid;
      edPwd.Text = Settings.webPwd;
      edURL.Text = Settings.streamURL;
      edScraper.Text = Settings.scraperURL;

      nudThumbHeight.Value = Settings.thumbHeight;
      nudThumbWidth.Value = Settings.thumbWidth;

      thTv.Text = Settings.thumbs.tv;
      thRadio.Text = Settings.thumbs.radio;
      thPictures.Text = Settings.thumbs.pictures;

      edMovies.Text = Settings.dbLocations.db_movies;
      edMusic.Text = Settings.dbLocations.db_music;
      edPictures.Text = Settings.dbLocations.db_pictures;
      edTvSeries.Text = Settings.dbLocations.db_tvseries;
      edMovingPictures.Text = Settings.dbLocations.db_movingpictures;

      grid.Rows.Clear();
      foreach (EncoderConfig cfg in Settings.encCfgs)
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
    public void SaveConfig()
    {
      Settings.httpPort = (int)nudPort.Value;
      Settings.playerType = cbPlayerType.SelectedIndex;
      Settings.clientPlayer=edPlayer.Text;
      Settings.webUid = edUid.Text;
      Settings.webPwd = edPwd.Text;
      Settings.streamURL = edURL.Text;
      Settings.scraperURL = edScraper.Text;

      Settings.thumbHeight = (int)nudThumbHeight.Value;
      Settings.thumbWidth = (int)nudThumbWidth.Value;
      Settings.thumbs.tv = thTv.Text;
      Settings.thumbs.radio = thRadio.Text;
      Settings.thumbs.pictures = thPictures.Text;

      Settings.dbLocations.db_movies = edMovies.Text;
      Settings.dbLocations.db_music = edMusic.Text;
      Settings.dbLocations.db_pictures = edPictures.Text; ;
      Settings.dbLocations.db_tvseries = edTvSeries.Text;
      Settings.dbLocations.db_movingpictures = edMovingPictures.Text;

      Settings.encCfgs.Clear();
      foreach (DataGridViewRow row in grid.Rows)
      {
        if (row.Cells[0].Value != null)
          Settings.encCfgs.Add(new EncoderConfig(row.Cells[0].Value.ToString(), (bool)row.Cells[1].Value, row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), GetTransportMethod(row.Cells[4].Value.ToString()), GetTransportMethod(row.Cells[5].Value.ToString())));
      }
      Settings.SaveSettings();
    }
    #endregion

    #region Databases
    private void button1_Click(object sender, EventArgs e)
    {
      if (openDlg.ShowDialog() == DialogResult.OK)
        edMovies.Text = openDlg.FileName;
    }
    private void button2_Click(object sender, EventArgs e)
    {
      if (openDlg.ShowDialog() == DialogResult.OK)
        edMusic.Text = openDlg.FileName;
    }
    private void button3_Click(object sender, EventArgs e)
    {
      if (openDlg.ShowDialog() == DialogResult.OK)
        edPictures.Text = openDlg.FileName;
    }
    private void button4_Click(object sender, EventArgs e)
    {
      if (openDlg.ShowDialog() == DialogResult.OK)
        edTvSeries.Text = openDlg.FileName;
    }
    private void button5_Click(object sender, EventArgs e)
    {
      if (openDlg.ShowDialog() == DialogResult.OK)
        edMovingPictures.Text = openDlg.FileName;
    }
    #endregion

    #region Thumbs
    private void button12_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        thTv.Text = folderBrowserDialog1.SelectedPath;
    }
    private void button11_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        thRadio.Text = folderBrowserDialog1.SelectedPath;
    }
    private void button10_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        thPictures.Text = folderBrowserDialog1.SelectedPath;
    }
    #endregion

    private void button6_Click(object sender, EventArgs e)
    {
      string filter = openDlg.Filter;
      openDlg.Filter = "Applications (*.exe)|*.exe";
      if (openDlg.ShowDialog() == DialogResult.OK)
        edPlayer.Text = openDlg.FileName;
      openDlg.Filter = filter;
    }

    private void btnHelp_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process proc = new System.Diagnostics.Process();
      proc.StartInfo.FileName = Settings.baseDir + "\\MPWebServices\\MPWebServices_help.html";
      proc.StartInfo.UseShellExecute = true;
      proc.Start();
    }
  }

}
