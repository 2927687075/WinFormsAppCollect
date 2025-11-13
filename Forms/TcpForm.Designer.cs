namespace WinFormsAppCollect.Forms
{
    partial class TcpForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBoxTCPConfig;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnectTCP;
        private System.Windows.Forms.Button btnDisconnectTCP;

        private System.Windows.Forms.GroupBox groupBoxTCPData;
        private System.Windows.Forms.ComboBox cmbFunctionCodeTCP;
        private System.Windows.Forms.TextBox txtSlaveAddressTCP;
        private System.Windows.Forms.TextBox txtStartAddressTCP;
        private System.Windows.Forms.TextBox txtNumberOfPointsTCP;
        private System.Windows.Forms.TextBox txtIntervalTCP;
        private System.Windows.Forms.Button btnReadTCP;
        private System.Windows.Forms.Button btnStartContinuousReadTCP;
        private System.Windows.Forms.Button btnStopContinuousReadTCP;

        private System.Windows.Forms.Label label11, label12, label13, label14, label15;
        private System.Windows.Forms.Label label16, label17;

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
            this.groupBoxTCPConfig = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnectTCP = new System.Windows.Forms.Button();
            this.btnDisconnectTCP = new System.Windows.Forms.Button();
            this.groupBoxTCPData = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbFunctionCodeTCP = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSlaveAddressTCP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtStartAddressTCP = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNumberOfPointsTCP = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtIntervalTCP = new System.Windows.Forms.TextBox();
            this.btnStartContinuousReadTCP = new System.Windows.Forms.Button();
            this.btnStopContinuousReadTCP = new System.Windows.Forms.Button();
            this.btnReadTCP = new System.Windows.Forms.Button();
            this.groupBoxTCPConfig.SuspendLayout();
            this.groupBoxTCPData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTCPConfig
            // 
            this.groupBoxTCPConfig.Controls.Add(this.label11);
            this.groupBoxTCPConfig.Controls.Add(this.txtIPAddress);
            this.groupBoxTCPConfig.Controls.Add(this.label12);
            this.groupBoxTCPConfig.Controls.Add(this.txtPort);
            this.groupBoxTCPConfig.Controls.Add(this.btnConnectTCP);
            this.groupBoxTCPConfig.Controls.Add(this.btnDisconnectTCP);
            this.groupBoxTCPConfig.Location = new System.Drawing.Point(10, 10);
            this.groupBoxTCPConfig.Name = "groupBoxTCPConfig";
            this.groupBoxTCPConfig.Size = new System.Drawing.Size(540, 160);
            this.groupBoxTCPConfig.TabIndex = 0;
            this.groupBoxTCPConfig.TabStop = false;
            this.groupBoxTCPConfig.Text = "TCP配置";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 0;
            this.label11.Text = "IP地址:";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(0, 0);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(100, 25);
            this.txtIPAddress.TabIndex = 1;
            this.txtIPAddress.Text = "127.0.0.1";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 2;
            this.label12.Text = "端口号:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(0, 0);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 25);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "502";
            // 
            // btnConnectTCP
            // 
            this.btnConnectTCP.Location = new System.Drawing.Point(0, 0);
            this.btnConnectTCP.Name = "btnConnectTCP";
            this.btnConnectTCP.Size = new System.Drawing.Size(75, 23);
            this.btnConnectTCP.TabIndex = 4;
            this.btnConnectTCP.Text = "连接";
            // 
            // btnDisconnectTCP
            // 
            this.btnDisconnectTCP.Location = new System.Drawing.Point(0, 0);
            this.btnDisconnectTCP.Name = "btnDisconnectTCP";
            this.btnDisconnectTCP.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnectTCP.TabIndex = 5;
            this.btnDisconnectTCP.Text = "断开";
            // 
            // groupBoxTCPData
            // 
            this.groupBoxTCPData.Controls.Add(this.label13);
            this.groupBoxTCPData.Controls.Add(this.cmbFunctionCodeTCP);
            this.groupBoxTCPData.Controls.Add(this.label14);
            this.groupBoxTCPData.Controls.Add(this.txtSlaveAddressTCP);
            this.groupBoxTCPData.Controls.Add(this.label15);
            this.groupBoxTCPData.Controls.Add(this.txtStartAddressTCP);
            this.groupBoxTCPData.Controls.Add(this.label16);
            this.groupBoxTCPData.Controls.Add(this.txtNumberOfPointsTCP);
            this.groupBoxTCPData.Controls.Add(this.label17);
            this.groupBoxTCPData.Controls.Add(this.txtIntervalTCP);
            this.groupBoxTCPData.Controls.Add(this.btnStartContinuousReadTCP);
            this.groupBoxTCPData.Controls.Add(this.btnStopContinuousReadTCP);
            this.groupBoxTCPData.Controls.Add(this.btnReadTCP);
            this.groupBoxTCPData.Location = new System.Drawing.Point(10, 180);
            this.groupBoxTCPData.Name = "groupBoxTCPData";
            this.groupBoxTCPData.Size = new System.Drawing.Size(540, 160);
            this.groupBoxTCPData.TabIndex = 1;
            this.groupBoxTCPData.TabStop = false;
            this.groupBoxTCPData.Text = "数据采集";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 0;
            this.label13.Text = "功能码:";
            // 
            // cmbFunctionCodeTCP
            // 
            this.cmbFunctionCodeTCP.Items.AddRange(new object[] {
            "01 Read Coils",
            "02 Read Inputs",
            "03 Read Holding Registers",
            "04 Read Input Registers"});
            this.cmbFunctionCodeTCP.Location = new System.Drawing.Point(0, 0);
            this.cmbFunctionCodeTCP.Name = "cmbFunctionCodeTCP";
            this.cmbFunctionCodeTCP.Size = new System.Drawing.Size(121, 23);
            this.cmbFunctionCodeTCP.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 2;
            this.label14.Text = "从站地址:";
            // 
            // txtSlaveAddressTCP
            // 
            this.txtSlaveAddressTCP.Location = new System.Drawing.Point(0, 0);
            this.txtSlaveAddressTCP.Name = "txtSlaveAddressTCP";
            this.txtSlaveAddressTCP.Size = new System.Drawing.Size(100, 25);
            this.txtSlaveAddressTCP.TabIndex = 3;
            this.txtSlaveAddressTCP.Text = "1";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 23);
            this.label15.TabIndex = 4;
            this.label15.Text = "起始地址:";
            // 
            // txtStartAddressTCP
            // 
            this.txtStartAddressTCP.Location = new System.Drawing.Point(0, 0);
            this.txtStartAddressTCP.Name = "txtStartAddressTCP";
            this.txtStartAddressTCP.Size = new System.Drawing.Size(100, 25);
            this.txtStartAddressTCP.TabIndex = 5;
            this.txtStartAddressTCP.Text = "0";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 23);
            this.label16.TabIndex = 6;
            this.label16.Text = "数据长度:";
            // 
            // txtNumberOfPointsTCP
            // 
            this.txtNumberOfPointsTCP.Location = new System.Drawing.Point(0, 0);
            this.txtNumberOfPointsTCP.Name = "txtNumberOfPointsTCP";
            this.txtNumberOfPointsTCP.Size = new System.Drawing.Size(100, 25);
            this.txtNumberOfPointsTCP.TabIndex = 7;
            this.txtNumberOfPointsTCP.Text = "10";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 23);
            this.label17.TabIndex = 8;
            this.label17.Text = "读取间隔(ms):";
            // 
            // txtIntervalTCP
            // 
            this.txtIntervalTCP.Location = new System.Drawing.Point(0, 0);
            this.txtIntervalTCP.Name = "txtIntervalTCP";
            this.txtIntervalTCP.Size = new System.Drawing.Size(100, 25);
            this.txtIntervalTCP.TabIndex = 9;
            this.txtIntervalTCP.Text = "1000";
            // 
            // btnStartContinuousReadTCP
            // 
            this.btnStartContinuousReadTCP.Location = new System.Drawing.Point(0, 0);
            this.btnStartContinuousReadTCP.Name = "btnStartContinuousReadTCP";
            this.btnStartContinuousReadTCP.Size = new System.Drawing.Size(75, 23);
            this.btnStartContinuousReadTCP.TabIndex = 10;
            this.btnStartContinuousReadTCP.Text = "开始循环读取";
            // 
            // btnStopContinuousReadTCP
            // 
            this.btnStopContinuousReadTCP.Location = new System.Drawing.Point(0, 0);
            this.btnStopContinuousReadTCP.Name = "btnStopContinuousReadTCP";
            this.btnStopContinuousReadTCP.Size = new System.Drawing.Size(75, 23);
            this.btnStopContinuousReadTCP.TabIndex = 11;
            this.btnStopContinuousReadTCP.Text = "停止循环读取";
            // 
            // btnReadTCP
            // 
            this.btnReadTCP.Location = new System.Drawing.Point(0, 0);
            this.btnReadTCP.Name = "btnReadTCP";
            this.btnReadTCP.Size = new System.Drawing.Size(75, 23);
            this.btnReadTCP.TabIndex = 12;
            this.btnReadTCP.Text = "读取数据";
            // 
            // TcpForm
            // 
            this.Controls.Add(this.groupBoxTCPData);
            this.Controls.Add(this.groupBoxTCPConfig);
            this.Name = "TcpForm";
            this.Size = new System.Drawing.Size(560, 350);
            this.groupBoxTCPConfig.ResumeLayout(false);
            this.groupBoxTCPConfig.PerformLayout();
            this.groupBoxTCPData.ResumeLayout(false);
            this.groupBoxTCPData.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}