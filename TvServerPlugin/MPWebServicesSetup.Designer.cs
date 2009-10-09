namespace TvServerPlugin
{
  partial class MPWebServicesSetup : SetupTv.SectionSettings
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
      this.tabCtrl = new System.Windows.Forms.TabControl();
      this.tabWebServer = new System.Windows.Forms.TabPage();
      this.button6 = new System.Windows.Forms.Button();
      this.label13 = new System.Windows.Forms.Label();
      this.edURL = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.edPwd = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.edUid = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.edPlayer = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.cbPlayerType = new System.Windows.Forms.ComboBox();
      this.nudPort = new System.Windows.Forms.NumericUpDown();
      this.label9 = new System.Windows.Forms.Label();
      this.tabThumbnails = new System.Windows.Forms.TabPage();
      this.nudThumbHeight = new System.Windows.Forms.NumericUpDown();
      this.label6 = new System.Windows.Forms.Label();
      this.nudThumbWidth = new System.Windows.Forms.NumericUpDown();
      this.label7 = new System.Windows.Forms.Label();
      this.tabDBLocations = new System.Windows.Forms.TabPage();
      this.button5 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.edMovingPictures = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.edTvSeries = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.edPictures = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.edMusic = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.edMovies = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabTranscoding = new System.Windows.Forms.TabPage();
      this.grid = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.openDlg = new System.Windows.Forms.OpenFileDialog();
      this.label14 = new System.Windows.Forms.Label();
      this.tabCtrl.SuspendLayout();
      this.tabWebServer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
      this.tabThumbnails.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudThumbHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudThumbWidth)).BeginInit();
      this.tabDBLocations.SuspendLayout();
      this.tabTranscoding.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.SuspendLayout();
      // 
      // tabCtrl
      // 
      this.tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tabCtrl.Controls.Add(this.tabWebServer);
      this.tabCtrl.Controls.Add(this.tabThumbnails);
      this.tabCtrl.Controls.Add(this.tabDBLocations);
      this.tabCtrl.Controls.Add(this.tabTranscoding);
      this.tabCtrl.Location = new System.Drawing.Point(0, 40);
      this.tabCtrl.Name = "tabCtrl";
      this.tabCtrl.SelectedIndex = 0;
      this.tabCtrl.Size = new System.Drawing.Size(416, 229);
      this.tabCtrl.TabIndex = 0;
      // 
      // tabWebServer
      // 
      this.tabWebServer.Controls.Add(this.button6);
      this.tabWebServer.Controls.Add(this.label13);
      this.tabWebServer.Controls.Add(this.edURL);
      this.tabWebServer.Controls.Add(this.label12);
      this.tabWebServer.Controls.Add(this.edPwd);
      this.tabWebServer.Controls.Add(this.label11);
      this.tabWebServer.Controls.Add(this.edUid);
      this.tabWebServer.Controls.Add(this.label10);
      this.tabWebServer.Controls.Add(this.edPlayer);
      this.tabWebServer.Controls.Add(this.label8);
      this.tabWebServer.Controls.Add(this.cbPlayerType);
      this.tabWebServer.Controls.Add(this.nudPort);
      this.tabWebServer.Controls.Add(this.label9);
      this.tabWebServer.Location = new System.Drawing.Point(4, 22);
      this.tabWebServer.Name = "tabWebServer";
      this.tabWebServer.Padding = new System.Windows.Forms.Padding(3);
      this.tabWebServer.Size = new System.Drawing.Size(408, 203);
      this.tabWebServer.TabIndex = 0;
      this.tabWebServer.Text = "WebServer";
      this.tabWebServer.UseVisualStyleBackColor = true;
      // 
      // button6
      // 
      this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button6.Location = new System.Drawing.Point(363, 64);
      this.button6.Name = "button6";
      this.button6.Size = new System.Drawing.Size(36, 23);
      this.button6.TabIndex = 34;
      this.button6.Text = "...";
      this.button6.UseVisualStyleBackColor = true;
      this.button6.Click += new System.EventHandler(this.button6_Click);
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(11, 147);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(82, 13);
      this.label13.TabIndex = 33;
      this.label13.Text = "Streaming URL:";
      // 
      // edURL
      // 
      this.edURL.Location = new System.Drawing.Point(131, 144);
      this.edURL.Name = "edURL";
      this.edURL.Size = new System.Drawing.Size(227, 20);
      this.edURL.TabIndex = 32;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(13, 121);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(56, 13);
      this.label12.TabIndex = 31;
      this.label12.Text = "Password:";
      // 
      // edPwd
      // 
      this.edPwd.Location = new System.Drawing.Point(131, 118);
      this.edPwd.Name = "edPwd";
      this.edPwd.Size = new System.Drawing.Size(140, 20);
      this.edPwd.TabIndex = 30;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(11, 95);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(58, 13);
      this.label11.TabIndex = 29;
      this.label11.Text = "Username:";
      // 
      // edUid
      // 
      this.edUid.Location = new System.Drawing.Point(131, 92);
      this.edUid.Name = "edUid";
      this.edUid.Size = new System.Drawing.Size(140, 20);
      this.edUid.TabIndex = 28;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(11, 69);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(118, 13);
      this.label10.TabIndex = 27;
      this.label10.Text = "Client Streaming Player:";
      // 
      // edPlayer
      // 
      this.edPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edPlayer.Location = new System.Drawing.Point(131, 66);
      this.edPlayer.Name = "edPlayer";
      this.edPlayer.Size = new System.Drawing.Size(226, 20);
      this.edPlayer.TabIndex = 26;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(11, 43);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(62, 13);
      this.label8.TabIndex = 25;
      this.label8.Text = "Player type:";
      // 
      // cbPlayerType
      // 
      this.cbPlayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPlayerType.FormattingEnabled = true;
      this.cbPlayerType.Items.AddRange(new object[] {
            "Player installed on client",
            "VLC Browser plugin"});
      this.cbPlayerType.Location = new System.Drawing.Point(131, 39);
      this.cbPlayerType.Name = "cbPlayerType";
      this.cbPlayerType.Size = new System.Drawing.Size(177, 21);
      this.cbPlayerType.TabIndex = 24;
      // 
      // nudPort
      // 
      this.nudPort.Location = new System.Drawing.Point(131, 13);
      this.nudPort.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
      this.nudPort.Name = "nudPort";
      this.nudPort.Size = new System.Drawing.Size(53, 20);
      this.nudPort.TabIndex = 23;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(11, 15);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(52, 13);
      this.label9.TabIndex = 22;
      this.label9.Text = "Http Port:";
      // 
      // tabThumbnails
      // 
      this.tabThumbnails.Controls.Add(this.nudThumbHeight);
      this.tabThumbnails.Controls.Add(this.label6);
      this.tabThumbnails.Controls.Add(this.nudThumbWidth);
      this.tabThumbnails.Controls.Add(this.label7);
      this.tabThumbnails.Location = new System.Drawing.Point(4, 22);
      this.tabThumbnails.Name = "tabThumbnails";
      this.tabThumbnails.Padding = new System.Windows.Forms.Padding(3);
      this.tabThumbnails.Size = new System.Drawing.Size(428, 247);
      this.tabThumbnails.TabIndex = 1;
      this.tabThumbnails.Text = "Thumbnails";
      this.tabThumbnails.UseVisualStyleBackColor = true;
      // 
      // nudThumbHeight
      // 
      this.nudThumbHeight.Location = new System.Drawing.Point(121, 44);
      this.nudThumbHeight.Name = "nudThumbHeight";
      this.nudThumbHeight.Size = new System.Drawing.Size(53, 20);
      this.nudThumbHeight.TabIndex = 21;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 48);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(77, 13);
      this.label6.TabIndex = 20;
      this.label6.Text = "Thumb Height:";
      // 
      // nudThumbWidth
      // 
      this.nudThumbWidth.Location = new System.Drawing.Point(121, 18);
      this.nudThumbWidth.Name = "nudThumbWidth";
      this.nudThumbWidth.Size = new System.Drawing.Size(53, 20);
      this.nudThumbWidth.TabIndex = 19;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 22);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(74, 13);
      this.label7.TabIndex = 18;
      this.label7.Text = "Thumb Width:";
      // 
      // tabDBLocations
      // 
      this.tabDBLocations.Controls.Add(this.button5);
      this.tabDBLocations.Controls.Add(this.button4);
      this.tabDBLocations.Controls.Add(this.button3);
      this.tabDBLocations.Controls.Add(this.button2);
      this.tabDBLocations.Controls.Add(this.button1);
      this.tabDBLocations.Controls.Add(this.edMovingPictures);
      this.tabDBLocations.Controls.Add(this.label5);
      this.tabDBLocations.Controls.Add(this.edTvSeries);
      this.tabDBLocations.Controls.Add(this.label4);
      this.tabDBLocations.Controls.Add(this.edPictures);
      this.tabDBLocations.Controls.Add(this.label3);
      this.tabDBLocations.Controls.Add(this.edMusic);
      this.tabDBLocations.Controls.Add(this.label2);
      this.tabDBLocations.Controls.Add(this.edMovies);
      this.tabDBLocations.Controls.Add(this.label1);
      this.tabDBLocations.Location = new System.Drawing.Point(4, 22);
      this.tabDBLocations.Name = "tabDBLocations";
      this.tabDBLocations.Padding = new System.Windows.Forms.Padding(3);
      this.tabDBLocations.Size = new System.Drawing.Size(428, 247);
      this.tabDBLocations.TabIndex = 2;
      this.tabDBLocations.Text = "MP DB Locations";
      this.tabDBLocations.UseVisualStyleBackColor = true;
      // 
      // button5
      // 
      this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button5.Location = new System.Drawing.Point(384, 118);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(36, 23);
      this.button5.TabIndex = 14;
      this.button5.Text = "...";
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new System.EventHandler(this.button5_Click);
      // 
      // button4
      // 
      this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button4.Location = new System.Drawing.Point(384, 92);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(36, 23);
      this.button4.TabIndex = 13;
      this.button4.Text = "...";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // button3
      // 
      this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button3.Location = new System.Drawing.Point(384, 67);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(36, 23);
      this.button3.TabIndex = 12;
      this.button3.Text = "...";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.Location = new System.Drawing.Point(384, 41);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(36, 23);
      this.button2.TabIndex = 11;
      this.button2.Text = "...";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(384, 15);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(36, 23);
      this.button1.TabIndex = 10;
      this.button1.Text = "...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // edMovingPictures
      // 
      this.edMovingPictures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edMovingPictures.Location = new System.Drawing.Point(93, 120);
      this.edMovingPictures.Name = "edMovingPictures";
      this.edMovingPictures.Size = new System.Drawing.Size(285, 20);
      this.edMovingPictures.TabIndex = 9;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(7, 123);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(86, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "Moving Pictures:";
      // 
      // edTvSeries
      // 
      this.edTvSeries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edTvSeries.Location = new System.Drawing.Point(93, 94);
      this.edTvSeries.Name = "edTvSeries";
      this.edTvSeries.Size = new System.Drawing.Size(285, 20);
      this.edTvSeries.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(7, 97);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(55, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Tv Series:";
      // 
      // edPictures
      // 
      this.edPictures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edPictures.Location = new System.Drawing.Point(93, 68);
      this.edPictures.Name = "edPictures";
      this.edPictures.Size = new System.Drawing.Size(285, 20);
      this.edPictures.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(7, 71);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(48, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Pictures:";
      // 
      // edMusic
      // 
      this.edMusic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edMusic.Location = new System.Drawing.Point(93, 42);
      this.edMusic.Name = "edMusic";
      this.edMusic.Size = new System.Drawing.Size(285, 20);
      this.edMusic.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Music:";
      // 
      // edMovies
      // 
      this.edMovies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edMovies.Location = new System.Drawing.Point(93, 16);
      this.edMovies.Name = "edMovies";
      this.edMovies.Size = new System.Drawing.Size(285, 20);
      this.edMovies.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(44, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Movies:";
      // 
      // tabTranscoding
      // 
      this.tabTranscoding.Controls.Add(this.grid);
      this.tabTranscoding.Location = new System.Drawing.Point(4, 22);
      this.tabTranscoding.Name = "tabTranscoding";
      this.tabTranscoding.Padding = new System.Windows.Forms.Padding(3);
      this.tabTranscoding.Size = new System.Drawing.Size(428, 247);
      this.tabTranscoding.TabIndex = 3;
      this.tabTranscoding.Text = "Transcoding";
      this.tabTranscoding.UseVisualStyleBackColor = true;
      // 
      // grid
      // 
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7});
      this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grid.Location = new System.Drawing.Point(3, 3);
      this.grid.Name = "grid";
      this.grid.Size = new System.Drawing.Size(422, 241);
      this.grid.TabIndex = 7;
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
            "Filename",
            "NamedPipe",
            "StandardIn"});
      this.Column5.Name = "Column5";
      this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // Column7
      // 
      this.Column7.HeaderText = "OutputMethod";
      this.Column7.Items.AddRange(new object[] {
            "NamedPipe",
            "StandardOut"});
      this.Column7.Name = "Column7";
      // 
      // openDlg
      // 
      this.openDlg.FileName = "openFileDialog1";
      this.openDlg.Filter = "SQLite Databases (*.db3)|*.db3";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label14.ForeColor = System.Drawing.Color.Red;
      this.label14.Location = new System.Drawing.Point(4, 11);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(405, 13);
      this.label14.TabIndex = 1;
      this.label14.Text = "Important: If you change the HTTP Port you have to restart TvService";
      // 
      // MPWebServicesSetup
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.label14);
      this.Controls.Add(this.tabCtrl);
      this.Name = "MPWebServicesSetup";
      this.Size = new System.Drawing.Size(416, 269);
      this.tabCtrl.ResumeLayout(false);
      this.tabWebServer.ResumeLayout(false);
      this.tabWebServer.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
      this.tabThumbnails.ResumeLayout(false);
      this.tabThumbnails.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudThumbHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudThumbWidth)).EndInit();
      this.tabDBLocations.ResumeLayout(false);
      this.tabDBLocations.PerformLayout();
      this.tabTranscoding.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TabControl tabCtrl;
    private System.Windows.Forms.TabPage tabWebServer;
    private System.Windows.Forms.TabPage tabThumbnails;
    private System.Windows.Forms.TabPage tabDBLocations;
    private System.Windows.Forms.TabPage tabTranscoding;
    private System.Windows.Forms.DataGridView grid;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
    private System.Windows.Forms.DataGridViewComboBoxColumn Column7;
    private System.Windows.Forms.TextBox edMovingPictures;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox edTvSeries;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox edPictures;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edMusic;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox edMovies;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.OpenFileDialog openDlg;
    private System.Windows.Forms.NumericUpDown nudThumbHeight;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown nudThumbWidth;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cbPlayerType;
    private System.Windows.Forms.NumericUpDown nudPort;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox edPlayer;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox edUid;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox edURL;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox edPwd;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Label label14;
  }
}

