namespace WinFormsAppCollect.Forms
{
    partial class OpcForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBoxOPCConfig;
        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.Button btnConnectOPC;
        private System.Windows.Forms.Button btnDisconnectOPC;

        private System.Windows.Forms.GroupBox groupBoxOPCData;
        private System.Windows.Forms.TextBox txtNodeId;
        private System.Windows.Forms.TextBox txtIntervalOPC;
        private System.Windows.Forms.Button btnReadOPC;
        private System.Windows.Forms.Button btnStartContinuousReadOPC;
        private System.Windows.Forms.Button btnStopContinuousReadOPC;

        private System.Windows.Forms.Label label18, label19, label20;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBoxOPCConfig = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.btnConnectOPC = new System.Windows.Forms.Button();
            this.btnDisconnectOPC = new System.Windows.Forms.Button();
            this.groupBoxOPCData = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtNodeId = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtIntervalOPC = new System.Windows.Forms.TextBox();
            this.btnReadOPC = new System.Windows.Forms.Button();
            this.btnStartContinuousReadOPC = new System.Windows.Forms.Button();
            this.btnStopContinuousReadOPC = new System.Windows.Forms.Button();
            this.groupBoxOPCConfig.SuspendLayout();
            this.groupBoxOPCData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOPCConfig
            // 
            this.groupBoxOPCConfig.Controls.Add(this.label18);
            this.groupBoxOPCConfig.Controls.Add(this.txtServerUrl);
            this.groupBoxOPCConfig.Controls.Add(this.btnConnectOPC);
            this.groupBoxOPCConfig.Controls.Add(this.btnDisconnectOPC);
            this.groupBoxOPCConfig.Location = new System.Drawing.Point(15, 15);
            this.groupBoxOPCConfig.Name = "groupBoxOPCConfig";
            this.groupBoxOPCConfig.Size = new System.Drawing.Size(530, 120);
            this.groupBoxOPCConfig.TabIndex = 0;
            this.groupBoxOPCConfig.TabStop = false;
            this.groupBoxOPCConfig.Text = "服务器配置";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 20);
            this.label18.TabIndex = 0;
            this.label18.Text = "服务器URL:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Location = new System.Drawing.Point(105, 33);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(300, 27);
            this.txtServerUrl.TabIndex = 1;
            this.txtServerUrl.Text = "opc.tcp://LAPTOP-DQR1RDDU:53530";
            // 
            // btnConnectOPC
            // 
            this.btnConnectOPC.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnectOPC.Location = new System.Drawing.Point(33, 77);
            this.btnConnectOPC.Name = "btnConnectOPC";
            this.btnConnectOPC.Size = new System.Drawing.Size(80, 30);
            this.btnConnectOPC.TabIndex = 2;
            this.btnConnectOPC.Text = "连接";
            this.btnConnectOPC.UseVisualStyleBackColor = false;
            this.btnConnectOPC.Click += new System.EventHandler(this.btnConnectOPC_Click);
            // 
            // btnDisconnectOPC
            // 
            this.btnDisconnectOPC.BackColor = System.Drawing.Color.LightCoral;
            this.btnDisconnectOPC.Enabled = false;
            this.btnDisconnectOPC.Location = new System.Drawing.Point(131, 77);
            this.btnDisconnectOPC.Name = "btnDisconnectOPC";
            this.btnDisconnectOPC.Size = new System.Drawing.Size(80, 30);
            this.btnDisconnectOPC.TabIndex = 3;
            this.btnDisconnectOPC.Text = "断开";
            this.btnDisconnectOPC.UseVisualStyleBackColor = false;
            this.btnDisconnectOPC.Click += new System.EventHandler(this.btnDisconnectOPC_Click);
            // 
            // groupBoxOPCData
            // 
            this.groupBoxOPCData.Controls.Add(this.label19);
            this.groupBoxOPCData.Controls.Add(this.txtNodeId);
            this.groupBoxOPCData.Controls.Add(this.label20);
            this.groupBoxOPCData.Controls.Add(this.txtIntervalOPC);
            this.groupBoxOPCData.Controls.Add(this.btnReadOPC);
            this.groupBoxOPCData.Controls.Add(this.btnStartContinuousReadOPC);
            this.groupBoxOPCData.Controls.Add(this.btnStopContinuousReadOPC);
            this.groupBoxOPCData.Location = new System.Drawing.Point(15, 145);
            this.groupBoxOPCData.Name = "groupBoxOPCData";
            this.groupBoxOPCData.Size = new System.Drawing.Size(530, 160);
            this.groupBoxOPCData.TabIndex = 1;
            this.groupBoxOPCData.TabStop = false;
            this.groupBoxOPCData.Text = "数据采集";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(6, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 20);
            this.label19.TabIndex = 0;
            this.label19.Text = "节点ID:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNodeId
            // 
            this.txtNodeId.Location = new System.Drawing.Point(105, 47);
            this.txtNodeId.Name = "txtNodeId";
            this.txtNodeId.Size = new System.Drawing.Size(189, 27);
            this.txtNodeId.TabIndex = 1;
            this.txtNodeId.Text = "ns=3;i=1004";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(329, 50);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 20);
            this.label20.TabIndex = 2;
            this.label20.Text = "读取间隔:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIntervalOPC
            // 
            this.txtIntervalOPC.Location = new System.Drawing.Point(415, 47);
            this.txtIntervalOPC.Name = "txtIntervalOPC";
            this.txtIntervalOPC.Size = new System.Drawing.Size(80, 27);
            this.txtIntervalOPC.TabIndex = 3;
            this.txtIntervalOPC.Text = "1000";
            this.txtIntervalOPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReadOPC
            // 
            this.btnReadOPC.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnReadOPC.Location = new System.Drawing.Point(33, 98);
            this.btnReadOPC.Name = "btnReadOPC";
            this.btnReadOPC.Size = new System.Drawing.Size(80, 30);
            this.btnReadOPC.TabIndex = 4;
            this.btnReadOPC.Text = "读取";
            this.btnReadOPC.UseVisualStyleBackColor = false;
            this.btnReadOPC.Click += new System.EventHandler(this.btnReadOPC_Click);
            // 
            // btnStartContinuousReadOPC
            // 
            this.btnStartContinuousReadOPC.BackColor = System.Drawing.Color.LightBlue;
            this.btnStartContinuousReadOPC.Location = new System.Drawing.Point(129, 98);
            this.btnStartContinuousReadOPC.Name = "btnStartContinuousReadOPC";
            this.btnStartContinuousReadOPC.Size = new System.Drawing.Size(120, 30);
            this.btnStartContinuousReadOPC.TabIndex = 5;
            this.btnStartContinuousReadOPC.Text = "开始循环读取";
            this.btnStartContinuousReadOPC.UseVisualStyleBackColor = false;
            this.btnStartContinuousReadOPC.Click += new System.EventHandler(this.btnStartContinuousReadOPC_Click);
            // 
            // btnStopContinuousReadOPC
            // 
            this.btnStopContinuousReadOPC.BackColor = System.Drawing.Color.LightSalmon;
            this.btnStopContinuousReadOPC.Enabled = false;
            this.btnStopContinuousReadOPC.Location = new System.Drawing.Point(266, 98);
            this.btnStopContinuousReadOPC.Name = "btnStopContinuousReadOPC";
            this.btnStopContinuousReadOPC.Size = new System.Drawing.Size(115, 30);
            this.btnStopContinuousReadOPC.TabIndex = 6;
            this.btnStopContinuousReadOPC.Text = "停止循环读取";
            this.btnStopContinuousReadOPC.UseVisualStyleBackColor = false;
            this.btnStopContinuousReadOPC.Click += new System.EventHandler(this.btnStopContinuousReadOPC_Click);
            // 
            // OpcForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBoxOPCData);
            this.Controls.Add(this.groupBoxOPCConfig);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "OpcForm";
            this.Size = new System.Drawing.Size(560, 320);
            this.groupBoxOPCConfig.ResumeLayout(false);
            this.groupBoxOPCConfig.PerformLayout();
            this.groupBoxOPCData.ResumeLayout(false);
            this.groupBoxOPCData.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}