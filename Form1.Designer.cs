using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsAppCollect
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Tab控件
        private TabControl tabControl1;
        private TabPage tabPageRTU;
        private TabPage tabPageTCP;

        // RTU页面控件
        private GroupBox groupBoxRTUConfig;
        private ComboBox cmbPortName;
        private ComboBox cmbBaudRate;
        private ComboBox cmbDataBits;
        private ComboBox cmbParity;
        private ComboBox cmbStopBits;
        private Button btnConnect;
        private Button btnDisconnect;
        private GroupBox groupBoxRTUData;
        private ComboBox cmbFunctionCode;
        private TextBox txtSlaveAddress;
        private TextBox txtStartAddress;
        private TextBox txtNumberOfPoints;
        private Button btnRead;
        private TextBox txtInterval;
        private Button btnStartContinuousRead;
        private Button btnStopContinuousRead;

        // TCP页面控件
        private GroupBox groupBoxTCPConfig;
        private TextBox txtIPAddress;
        private TextBox txtPort;
        private Button btnConnectTCP;
        private Button btnDisconnectTCP;
        private GroupBox groupBoxTCPData;
        private ComboBox cmbFunctionCodeTCP;
        private TextBox txtSlaveAddressTCP;
        private TextBox txtStartAddressTCP;
        private TextBox txtNumberOfPointsTCP;
        private Button btnReadTCP;
        private TextBox txtIntervalTCP;
        private Button btnStartContinuousReadTCP;
        private Button btnStopContinuousReadTCP;

        // 日志控件
        private RichTextBox txtLog;

        // 标签控件
        private Label label1, label2, label3, label4, label5;
        private Label label6, label7, label8, label9, label10;
        private Label label11, label12, label13, label14, label15;
        private Label label16, label17, label18, label19, label20;

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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageRTU = new System.Windows.Forms.TabPage();
            this.groupBoxRTUConfig = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.groupBoxRTUData = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFunctionCode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSlaveAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStartAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNumberOfPoints = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnStartContinuousRead = new System.Windows.Forms.Button();
            this.btnStopContinuousRead = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.tabPageTCP = new System.Windows.Forms.TabPage();
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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageRTU.SuspendLayout();
            this.groupBoxRTUConfig.SuspendLayout();
            this.groupBoxRTUData.SuspendLayout();
            this.tabPageTCP.SuspendLayout();
            this.groupBoxTCPConfig.SuspendLayout();
            this.groupBoxTCPData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageRTU);
            this.tabControl1.Controls.Add(this.tabPageTCP);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1176, 400);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageRTU
            // 
            this.tabPageRTU.BackColor = System.Drawing.Color.White;
            this.tabPageRTU.Controls.Add(this.groupBoxRTUConfig);
            this.tabPageRTU.Controls.Add(this.groupBoxRTUData);
            this.tabPageRTU.Location = new System.Drawing.Point(4, 29);
            this.tabPageRTU.Name = "tabPageRTU";
            this.tabPageRTU.Size = new System.Drawing.Size(1168, 367);
            this.tabPageRTU.TabIndex = 0;
            this.tabPageRTU.Text = "Modbus RTU";
            // 
            // groupBoxRTUConfig
            // 
            this.groupBoxRTUConfig.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxRTUConfig.Controls.Add(this.label1);
            this.groupBoxRTUConfig.Controls.Add(this.cmbPortName);
            this.groupBoxRTUConfig.Controls.Add(this.label2);
            this.groupBoxRTUConfig.Controls.Add(this.cmbBaudRate);
            this.groupBoxRTUConfig.Controls.Add(this.label3);
            this.groupBoxRTUConfig.Controls.Add(this.cmbDataBits);
            this.groupBoxRTUConfig.Controls.Add(this.label4);
            this.groupBoxRTUConfig.Controls.Add(this.cmbParity);
            this.groupBoxRTUConfig.Controls.Add(this.label5);
            this.groupBoxRTUConfig.Controls.Add(this.cmbStopBits);
            this.groupBoxRTUConfig.Controls.Add(this.btnConnect);
            this.groupBoxRTUConfig.Controls.Add(this.btnDisconnect);
            this.groupBoxRTUConfig.Location = new System.Drawing.Point(17, 18);
            this.groupBoxRTUConfig.Name = "groupBoxRTUConfig";
            this.groupBoxRTUConfig.Size = new System.Drawing.Size(550, 160);
            this.groupBoxRTUConfig.TabIndex = 0;
            this.groupBoxRTUConfig.TabStop = false;
            this.groupBoxRTUConfig.Text = "串口配置";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.Location = new System.Drawing.Point(100, 30);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(120, 28);
            this.cmbPortName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(100, 70);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(120, 28);
            this.cmbBaudRate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据位:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cmbDataBits.Location = new System.Drawing.Point(100, 110);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(120, 28);
            this.cmbDataBits.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(260, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "校验位:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.Items.AddRange(new object[] {
            "无",
            "奇校验",
            "偶校验"});
            this.cmbParity.Location = new System.Drawing.Point(340, 30);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(120, 28);
            this.cmbParity.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(260, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cmbStopBits.Location = new System.Drawing.Point(340, 70);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(120, 28);
            this.cmbStopBits.TabIndex = 9;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnect.Location = new System.Drawing.Point(290, 110);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 30);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.LightCoral;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(380, 110);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(80, 30);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            // 
            // groupBoxRTUData
            // 
            this.groupBoxRTUData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxRTUData.Controls.Add(this.label6);
            this.groupBoxRTUData.Controls.Add(this.cmbFunctionCode);
            this.groupBoxRTUData.Controls.Add(this.label7);
            this.groupBoxRTUData.Controls.Add(this.txtSlaveAddress);
            this.groupBoxRTUData.Controls.Add(this.label8);
            this.groupBoxRTUData.Controls.Add(this.txtStartAddress);
            this.groupBoxRTUData.Controls.Add(this.label9);
            this.groupBoxRTUData.Controls.Add(this.txtNumberOfPoints);
            this.groupBoxRTUData.Controls.Add(this.label10);
            this.groupBoxRTUData.Controls.Add(this.txtInterval);
            this.groupBoxRTUData.Controls.Add(this.btnStartContinuousRead);
            this.groupBoxRTUData.Controls.Add(this.btnStopContinuousRead);
            this.groupBoxRTUData.Controls.Add(this.btnRead);
            this.groupBoxRTUData.Location = new System.Drawing.Point(17, 195);
            this.groupBoxRTUData.Name = "groupBoxRTUData";
            this.groupBoxRTUData.Size = new System.Drawing.Size(550, 160);
            this.groupBoxRTUData.TabIndex = 1;
            this.groupBoxRTUData.TabStop = false;
            this.groupBoxRTUData.Text = "数据采集";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "功能码:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFunctionCode
            // 
            this.cmbFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctionCode.Items.AddRange(new object[] {
            "01 Read Coils",
            "02 Read Inputs",
            "03 Read Holding Registers",
            "04 Read Input Registers"});
            this.cmbFunctionCode.Location = new System.Drawing.Point(100, 30);
            this.cmbFunctionCode.Name = "cmbFunctionCode";
            this.cmbFunctionCode.Size = new System.Drawing.Size(160, 28);
            this.cmbFunctionCode.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(340, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "从站地址:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSlaveAddress
            // 
            this.txtSlaveAddress.Location = new System.Drawing.Point(420, 30);
            this.txtSlaveAddress.Name = "txtSlaveAddress";
            this.txtSlaveAddress.Size = new System.Drawing.Size(60, 27);
            this.txtSlaveAddress.TabIndex = 3;
            this.txtSlaveAddress.Text = "1";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(20, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "起始地址:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.Location = new System.Drawing.Point(100, 70);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.Size = new System.Drawing.Size(60, 27);
            this.txtStartAddress.TabIndex = 5;
            this.txtStartAddress.Text = "0";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(340, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "数据长度:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumberOfPoints
            // 
            this.txtNumberOfPoints.Location = new System.Drawing.Point(420, 70);
            this.txtNumberOfPoints.Name = "txtNumberOfPoints";
            this.txtNumberOfPoints.Size = new System.Drawing.Size(60, 27);
            this.txtNumberOfPoints.TabIndex = 7;
            this.txtNumberOfPoints.Text = "10";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(20, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "读取间隔(ms):";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(120, 110);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(60, 27);
            this.txtInterval.TabIndex = 9;
            this.txtInterval.Text = "500";
            // 
            // btnStartContinuousRead
            // 
            this.btnStartContinuousRead.BackColor = System.Drawing.Color.LightBlue;
            this.btnStartContinuousRead.Location = new System.Drawing.Point(190, 110);
            this.btnStartContinuousRead.Name = "btnStartContinuousRead";
            this.btnStartContinuousRead.Size = new System.Drawing.Size(120, 30);
            this.btnStartContinuousRead.TabIndex = 10;
            this.btnStartContinuousRead.Text = "开始循环读取";
            this.btnStartContinuousRead.UseVisualStyleBackColor = false;
            // 
            // btnStopContinuousRead
            // 
            this.btnStopContinuousRead.BackColor = System.Drawing.Color.LightSalmon;
            this.btnStopContinuousRead.Enabled = false;
            this.btnStopContinuousRead.Location = new System.Drawing.Point(320, 110);
            this.btnStopContinuousRead.Name = "btnStopContinuousRead";
            this.btnStopContinuousRead.Size = new System.Drawing.Size(120, 30);
            this.btnStopContinuousRead.TabIndex = 11;
            this.btnStopContinuousRead.Text = "停止循环读取";
            this.btnStopContinuousRead.UseVisualStyleBackColor = false;
            // 
            // btnRead
            // 
            this.btnRead.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnRead.Location = new System.Drawing.Point(450, 110);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(80, 30);
            this.btnRead.TabIndex = 12;
            this.btnRead.Text = "读取数据";
            this.btnRead.UseVisualStyleBackColor = false;
            // 
            // tabPageTCP
            // 
            this.tabPageTCP.BackColor = System.Drawing.Color.White;
            this.tabPageTCP.Controls.Add(this.groupBoxTCPData);
            this.tabPageTCP.Controls.Add(this.groupBoxTCPConfig);
            this.tabPageTCP.Location = new System.Drawing.Point(4, 29);
            this.tabPageTCP.Name = "tabPageTCP";
            this.tabPageTCP.Size = new System.Drawing.Size(1168, 367);
            this.tabPageTCP.TabIndex = 1;
            this.tabPageTCP.Text = "Modbus TCP";
            // 
            // groupBoxTCPConfig
            // 
            this.groupBoxTCPConfig.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxTCPConfig.Controls.Add(this.label11);
            this.groupBoxTCPConfig.Controls.Add(this.txtIPAddress);
            this.groupBoxTCPConfig.Controls.Add(this.label12);
            this.groupBoxTCPConfig.Controls.Add(this.txtPort);
            this.groupBoxTCPConfig.Controls.Add(this.btnConnectTCP);
            this.groupBoxTCPConfig.Controls.Add(this.btnDisconnectTCP);
            this.groupBoxTCPConfig.Location = new System.Drawing.Point(17, 15);
            this.groupBoxTCPConfig.Name = "groupBoxTCPConfig";
            this.groupBoxTCPConfig.Size = new System.Drawing.Size(550, 160);
            this.groupBoxTCPConfig.TabIndex = 0;
            this.groupBoxTCPConfig.TabStop = false;
            this.groupBoxTCPConfig.Text = "TCP配置";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(20, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "IP地址:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(100, 30);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(150, 27);
            this.txtIPAddress.TabIndex = 1;
            this.txtIPAddress.Text = "127.0.0.1";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(20, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "端口号:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(100, 70);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(150, 27);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "10502";
            // 
            // btnConnectTCP
            // 
            this.btnConnectTCP.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnectTCP.Location = new System.Drawing.Point(270, 30);
            this.btnConnectTCP.Name = "btnConnectTCP";
            this.btnConnectTCP.Size = new System.Drawing.Size(80, 30);
            this.btnConnectTCP.TabIndex = 4;
            this.btnConnectTCP.Text = "连接";
            this.btnConnectTCP.UseVisualStyleBackColor = false;
            // 
            // btnDisconnectTCP
            // 
            this.btnDisconnectTCP.BackColor = System.Drawing.Color.LightCoral;
            this.btnDisconnectTCP.Enabled = false;
            this.btnDisconnectTCP.Location = new System.Drawing.Point(270, 70);
            this.btnDisconnectTCP.Name = "btnDisconnectTCP";
            this.btnDisconnectTCP.Size = new System.Drawing.Size(80, 30);
            this.btnDisconnectTCP.TabIndex = 5;
            this.btnDisconnectTCP.Text = "断开";
            this.btnDisconnectTCP.UseVisualStyleBackColor = false;
            // 
            // groupBoxTCPData
            // 
            this.groupBoxTCPData.BackColor = System.Drawing.Color.WhiteSmoke;
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
            this.groupBoxTCPData.Location = new System.Drawing.Point(17, 191);
            this.groupBoxTCPData.Name = "groupBoxTCPData";
            this.groupBoxTCPData.Size = new System.Drawing.Size(550, 160);
            this.groupBoxTCPData.TabIndex = 1;
            this.groupBoxTCPData.TabStop = false;
            this.groupBoxTCPData.Text = "数据采集";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(20, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "功能码:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFunctionCodeTCP
            // 
            this.cmbFunctionCodeTCP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctionCodeTCP.Items.AddRange(new object[] {
            "01 Read Coils",
            "02 Read Inputs",
            "03 Read Holding Registers",
            "04 Read Input Registers"});
            this.cmbFunctionCodeTCP.Location = new System.Drawing.Point(100, 30);
            this.cmbFunctionCodeTCP.Name = "cmbFunctionCodeTCP";
            this.cmbFunctionCodeTCP.Size = new System.Drawing.Size(150, 28);
            this.cmbFunctionCodeTCP.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(270, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 20);
            this.label14.TabIndex = 2;
            this.label14.Text = "从站地址:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSlaveAddressTCP
            // 
            this.txtSlaveAddressTCP.Location = new System.Drawing.Point(350, 30);
            this.txtSlaveAddressTCP.Name = "txtSlaveAddressTCP";
            this.txtSlaveAddressTCP.Size = new System.Drawing.Size(60, 27);
            this.txtSlaveAddressTCP.TabIndex = 3;
            this.txtSlaveAddressTCP.Text = "1";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(20, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 20);
            this.label15.TabIndex = 4;
            this.label15.Text = "起始地址:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartAddressTCP
            // 
            this.txtStartAddressTCP.Location = new System.Drawing.Point(100, 70);
            this.txtStartAddressTCP.Name = "txtStartAddressTCP";
            this.txtStartAddressTCP.Size = new System.Drawing.Size(60, 27);
            this.txtStartAddressTCP.TabIndex = 5;
            this.txtStartAddressTCP.Text = "0";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(270, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 20);
            this.label16.TabIndex = 6;
            this.label16.Text = "数据长度:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumberOfPointsTCP
            // 
            this.txtNumberOfPointsTCP.Location = new System.Drawing.Point(350, 70);
            this.txtNumberOfPointsTCP.Name = "txtNumberOfPointsTCP";
            this.txtNumberOfPointsTCP.Size = new System.Drawing.Size(60, 27);
            this.txtNumberOfPointsTCP.TabIndex = 7;
            this.txtNumberOfPointsTCP.Text = "10";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(20, 110);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 20);
            this.label17.TabIndex = 8;
            this.label17.Text = "读取间隔(ms):";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIntervalTCP
            // 
            this.txtIntervalTCP.Location = new System.Drawing.Point(120, 110);
            this.txtIntervalTCP.Name = "txtIntervalTCP";
            this.txtIntervalTCP.Size = new System.Drawing.Size(60, 27);
            this.txtIntervalTCP.TabIndex = 9;
            this.txtIntervalTCP.Text = "5000";
            // 
            // btnStartContinuousReadTCP
            // 
            this.btnStartContinuousReadTCP.BackColor = System.Drawing.Color.LightBlue;
            this.btnStartContinuousReadTCP.Location = new System.Drawing.Point(190, 110);
            this.btnStartContinuousReadTCP.Name = "btnStartContinuousReadTCP";
            this.btnStartContinuousReadTCP.Size = new System.Drawing.Size(120, 30);
            this.btnStartContinuousReadTCP.TabIndex = 10;
            this.btnStartContinuousReadTCP.Text = "开始循环读取";
            this.btnStartContinuousReadTCP.UseVisualStyleBackColor = false;
            // 
            // btnStopContinuousReadTCP
            // 
            this.btnStopContinuousReadTCP.BackColor = System.Drawing.Color.LightSalmon;
            this.btnStopContinuousReadTCP.Enabled = false;
            this.btnStopContinuousReadTCP.Location = new System.Drawing.Point(320, 110);
            this.btnStopContinuousReadTCP.Name = "btnStopContinuousReadTCP";
            this.btnStopContinuousReadTCP.Size = new System.Drawing.Size(120, 30);
            this.btnStopContinuousReadTCP.TabIndex = 11;
            this.btnStopContinuousReadTCP.Text = "停止循环读取";
            this.btnStopContinuousReadTCP.UseVisualStyleBackColor = false;
            // 
            // btnReadTCP
            // 
            this.btnReadTCP.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnReadTCP.Location = new System.Drawing.Point(450, 110);
            this.btnReadTCP.Name = "btnReadTCP";
            this.btnReadTCP.Size = new System.Drawing.Size(80, 30);
            this.btnReadTCP.TabIndex = 12;
            this.btnReadTCP.Text = "读取数据";
            this.btnReadTCP.UseVisualStyleBackColor = false;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(12, 420);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1176, 368);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtLog);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modbus数据采集器 - RTU/TCP协议支持";
            this.tabControl1.ResumeLayout(false);
            this.tabPageRTU.ResumeLayout(false);
            this.groupBoxRTUConfig.ResumeLayout(false);
            this.groupBoxRTUData.ResumeLayout(false);
            this.groupBoxRTUData.PerformLayout();
            this.tabPageTCP.ResumeLayout(false);
            this.groupBoxTCPConfig.ResumeLayout(false);
            this.groupBoxTCPConfig.PerformLayout();
            this.groupBoxTCPData.ResumeLayout(false);
            this.groupBoxTCPData.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}