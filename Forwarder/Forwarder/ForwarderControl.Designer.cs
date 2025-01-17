﻿namespace Forwarder
{
    partial class ForwarderControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForwarderControl));
            this.pConf = new System.Windows.Forms.Panel();
            this.rbFollow = new System.Windows.Forms.RadioButton();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btClear = new System.Windows.Forms.Button();
            this.btStart = new System.Windows.Forms.Button();
            this.tbDestPort = new System.Windows.Forms.TextBox();
            this.lbDestPort = new System.Windows.Forms.Label();
            this.tbDestHost = new System.Windows.Forms.TextBox();
            this.lbDestHost = new System.Windows.Forms.Label();
            this.cbLocal = new System.Windows.Forms.CheckBox();
            this.tbSrcPort = new System.Windows.Forms.TextBox();
            this.lbSrc = new System.Windows.Forms.Label();
            this.lvHistory = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilMsg = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.pConf.SuspendLayout();
            this.SuspendLayout();
            // 
            // pConf
            // 
            this.pConf.Controls.Add(this.checkBoxLog);
            this.pConf.Controls.Add(this.rbFollow);
            this.pConf.Controls.Add(this.lbInfo);
            this.pConf.Controls.Add(this.btClear);
            this.pConf.Controls.Add(this.btStart);
            this.pConf.Controls.Add(this.tbDestPort);
            this.pConf.Controls.Add(this.lbDestPort);
            this.pConf.Controls.Add(this.tbDestHost);
            this.pConf.Controls.Add(this.lbDestHost);
            this.pConf.Controls.Add(this.cbLocal);
            this.pConf.Controls.Add(this.tbSrcPort);
            this.pConf.Controls.Add(this.lbSrc);
            this.pConf.Dock = System.Windows.Forms.DockStyle.Top;
            this.pConf.Location = new System.Drawing.Point(0, 0);
            this.pConf.Name = "pConf";
            this.pConf.Size = new System.Drawing.Size(424, 74);
            this.pConf.TabIndex = 0;
            this.pConf.Paint += new System.Windows.Forms.PaintEventHandler(this.pConf_Paint);
            // 
            // rbFollow
            // 
            this.rbFollow.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFollow.Checked = true;
            this.rbFollow.Image = global::Forwarder.Properties.Resources.list_follow;
            this.rbFollow.Location = new System.Drawing.Point(28, 48);
            this.rbFollow.Name = "rbFollow";
            this.rbFollow.Size = new System.Drawing.Size(25, 23);
            this.rbFollow.TabIndex = 21;
            this.rbFollow.TabStop = true;
            this.toolTip1.SetToolTip(this.rbFollow, "Follow events");
            this.rbFollow.UseVisualStyleBackColor = true;
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.Location = new System.Drawing.Point(274, 54);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(147, 15);
            this.lbInfo.TabIndex = 19;
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btClear.Image = global::Forwarder.Properties.Resources.list_clear;
            this.btClear.Location = new System.Drawing.Point(1, 48);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(25, 23);
            this.btClear.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btClear, "Clear event list");
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btStart.Location = new System.Drawing.Point(360, 23);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(61, 21);
            this.btStart.TabIndex = 17;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // tbDestPort
            // 
            this.tbDestPort.Location = new System.Drawing.Point(274, 25);
            this.tbDestPort.Name = "tbDestPort";
            this.tbDestPort.Size = new System.Drawing.Size(41, 21);
            this.tbDestPort.TabIndex = 16;
            this.tbDestPort.Text = "33211";
            this.tbDestPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDestPort.TextChanged += new System.EventHandler(this.ConfChanged);
            // 
            // lbDestPort
            // 
            this.lbDestPort.AutoSize = true;
            this.lbDestPort.Location = new System.Drawing.Point(243, 29);
            this.lbDestPort.Name = "lbDestPort";
            this.lbDestPort.Size = new System.Drawing.Size(35, 12);
            this.lbDestPort.TabIndex = 15;
            this.lbDestPort.Text = "Port:";
            // 
            // tbDestHost
            // 
            this.tbDestHost.Location = new System.Drawing.Point(76, 27);
            this.tbDestHost.Name = "tbDestHost";
            this.tbDestHost.Size = new System.Drawing.Size(161, 21);
            this.tbDestHost.TabIndex = 14;
            this.tbDestHost.Text = "127.0.0.1";
            this.tbDestHost.TextChanged += new System.EventHandler(this.ConfChanged);
            // 
            // lbDestHost
            // 
            this.lbDestHost.AutoSize = true;
            this.lbDestHost.Location = new System.Drawing.Point(7, 30);
            this.lbDestHost.Name = "lbDestHost";
            this.lbDestHost.Size = new System.Drawing.Size(77, 12);
            this.lbDestHost.TabIndex = 13;
            this.lbDestHost.Text = "Destination:";
            // 
            // cbLocal
            // 
            this.cbLocal.AutoSize = true;
            this.cbLocal.Location = new System.Drawing.Point(123, 6);
            this.cbLocal.Name = "cbLocal";
            this.cbLocal.Size = new System.Drawing.Size(156, 16);
            this.cbLocal.TabIndex = 12;
            this.cbLocal.Text = "only local connections";
            this.cbLocal.UseVisualStyleBackColor = true;
            this.cbLocal.CheckedChanged += new System.EventHandler(this.ConfChanged);
            // 
            // tbSrcPort
            // 
            this.tbSrcPort.Location = new System.Drawing.Point(76, 3);
            this.tbSrcPort.Name = "tbSrcPort";
            this.tbSrcPort.Size = new System.Drawing.Size(41, 21);
            this.tbSrcPort.TabIndex = 11;
            this.tbSrcPort.Text = "8088";
            this.tbSrcPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSrcPort.TextChanged += new System.EventHandler(this.ConfChanged);
            // 
            // lbSrc
            // 
            this.lbSrc.AutoSize = true;
            this.lbSrc.Location = new System.Drawing.Point(4, 6);
            this.lbSrc.Name = "lbSrc";
            this.lbSrc.Size = new System.Drawing.Size(77, 12);
            this.lbSrc.TabIndex = 10;
            this.lbSrc.Text = "Source Port:";
            // 
            // lvHistory
            // 
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.GridLines = true;
            this.lvHistory.HideSelection = false;
            this.lvHistory.Location = new System.Drawing.Point(0, 74);
            this.lvHistory.MultiSelect = false;
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(424, 316);
            this.lvHistory.SmallImageList = this.ilMsg;
            this.lvHistory.TabIndex = 1;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            this.lvHistory.DoubleClick += new System.EventHandler(this.lvHistory_DoubleClick);
            this.lvHistory.Resize += new System.EventHandler(this.lvHistory_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Events";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Send";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Recv.";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 70;
            // 
            // ilMsg
            // 
            this.ilMsg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMsg.ImageStream")));
            this.ilMsg.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMsg.Images.SetKeyName(0, "msg_info.png");
            this.ilMsg.Images.SetKeyName(1, "msg_error.png");
            this.ilMsg.Images.SetKeyName(2, "msg_connect.png");
            this.ilMsg.Images.SetKeyName(3, "msg_go.png");
            this.ilMsg.Images.SetKeyName(4, "msg_come.png");
            this.ilMsg.Images.SetKeyName(5, "msg_finish.png");
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Location = new System.Drawing.Point(286, 6);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(42, 16);
            this.checkBoxLog.TabIndex = 22;
            this.checkBoxLog.Text = "Log";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            // 
            // ForwarderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvHistory);
            this.Controls.Add(this.pConf);
            this.Name = "ForwarderControl";
            this.Size = new System.Drawing.Size(424, 390);
            this.pConf.ResumeLayout(false);
            this.pConf.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pConf;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.TextBox tbDestPort;
        private System.Windows.Forms.Label lbDestPort;
        private System.Windows.Forms.TextBox tbDestHost;
        private System.Windows.Forms.Label lbDestHost;
        private System.Windows.Forms.CheckBox cbLocal;
        private System.Windows.Forms.TextBox tbSrcPort;
        private System.Windows.Forms.Label lbSrc;
        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList ilMsg;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton rbFollow;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.CheckBox checkBoxLog;
    }
}
