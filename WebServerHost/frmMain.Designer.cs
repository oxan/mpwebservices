﻿namespace WebServerHost
{
  partial class frmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnSave = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.nudPort = new System.Windows.Forms.NumericUpDown();
      this.edHost = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.grid = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.btnStartServer = new System.Windows.Forms.Button();
      this.btnStopServer = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.label3 = new System.Windows.Forms.Label();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.label4 = new System.Windows.Forms.Label();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(280, 9);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.button1_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(52, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Http Port:";
      // 
      // nudPort
      // 
      this.nudPort.Location = new System.Drawing.Point(122, 12);
      this.nudPort.Name = "nudPort";
      this.nudPort.Size = new System.Drawing.Size(53, 20);
      this.nudPort.TabIndex = 3;
      // 
      // edHost
      // 
      this.edHost.Location = new System.Drawing.Point(122, 38);
      this.edHost.Name = "edHost";
      this.edHost.Size = new System.Drawing.Size(140, 20);
      this.edHost.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(103, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "TvServer hostname:";
      // 
      // grid
      // 
      this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7});
      this.grid.Location = new System.Drawing.Point(16, 76);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(662, 179);
      this.grid.TabIndex = 6;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "Name";
      this.Column1.Name = "Column1";
      // 
      // Column2
      // 
      this.Column2.HeaderText = "Transcode";
      this.Column2.Name = "Column2";
      // 
      // Column3
      // 
      this.Column3.HeaderText = "Transcoder";
      this.Column3.Name = "Column3";
      // 
      // Column4
      // 
      this.Column4.HeaderText = "Params";
      this.Column4.Name = "Column4";
      // 
      // Column5
      // 
      this.Column5.HeaderText = "InputMethod";
      this.Column5.Items.AddRange(new object[] {
            "NamedPipe",
            "StandardIn",
            "StandardOut"});
      this.Column5.Name = "Column5";
      this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // Column7
      // 
      this.Column7.HeaderText = "OutputMethod";
      this.Column7.Items.AddRange(new object[] {
            "NamedPipe",
            "StandardIn",
            "StandardOut"});
      this.Column7.Name = "Column7";
      // 
      // btnStartServer
      // 
      this.btnStartServer.Location = new System.Drawing.Point(372, 8);
      this.btnStartServer.Name = "btnStartServer";
      this.btnStartServer.Size = new System.Drawing.Size(75, 23);
      this.btnStartServer.TabIndex = 7;
      this.btnStartServer.Text = "Start Server";
      this.btnStartServer.UseVisualStyleBackColor = true;
      this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
      // 
      // btnStopServer
      // 
      this.btnStopServer.Location = new System.Drawing.Point(453, 8);
      this.btnStopServer.Name = "btnStopServer";
      this.btnStopServer.Size = new System.Drawing.Size(75, 23);
      this.btnStopServer.TabIndex = 8;
      this.btnStopServer.Text = "Stop Server";
      this.btnStopServer.UseVisualStyleBackColor = true;
      this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(378, 37);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(55, 13);
      this.linkLabel1.TabIndex = 9;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "linkLabel1";
      this.linkLabel1.Visible = false;
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(280, 37);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(77, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "WebApp URL:";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 267);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(690, 22);
      this.statusStrip1.TabIndex = 11;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(103, 17);
      this.toolStripStatusLabel1.Text = "WebServer stopped";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(280, 57);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(94, 13);
      this.label4.TabIndex = 13;
      this.label4.Text = "WebService URL:";
      // 
      // linkLabel2
      // 
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Location = new System.Drawing.Point(378, 57);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(55, 13);
      this.linkLabel2.TabIndex = 12;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "linkLabel2";
      this.linkLabel2.Visible = false;
      this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(690, 289);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.linkLabel2);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.btnStopServer);
      this.Controls.Add(this.btnStartServer);
      this.Controls.Add(this.grid);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edHost);
      this.Controls.Add(this.nudPort);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnSave);
      this.Name = "frmMain";
      this.Text = "MediaPortal WebServices Host";
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown nudPort;
    private System.Windows.Forms.TextBox edHost;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
    private System.Windows.Forms.DataGridViewComboBoxColumn Column7;
    private System.Windows.Forms.Button btnStartServer;
    private System.Windows.Forms.Button btnStopServer;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.LinkLabel linkLabel2;
  }
}

