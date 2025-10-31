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
            this.SuspendLayout();

            // 主窗体设置
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Text = "Modbus数据采集器 - RTU/TCP双协议支持";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            // TabControl
            this.tabControl1 = new TabControl();
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Size = new System.Drawing.Size(1176, 400);
            this.tabControl1.TabIndex = 0;

            // RTU页面
            this.tabPageRTU = new TabPage();
            this.tabPageRTU.Text = "Modbus RTU";
            this.tabPageRTU.BackColor = System.Drawing.Color.White;

            // TCP页面
            this.tabPageTCP = new TabPage();
            this.tabPageTCP.Text = "Modbus TCP";
            this.tabPageTCP.BackColor = System.Drawing.Color.White;

            this.tabControl1.Controls.Add(this.tabPageRTU);
            this.tabControl1.Controls.Add(this.tabPageTCP);

            // ========== RTU页面布局 ==========
            InitializeRTUPage();

            // ========== TCP页面布局 ==========
            InitializeTCPPage();

            // ========== 日志控件 ==========
            this.txtLog = new RichTextBox();
            this.txtLog.Location = new System.Drawing.Point(12, 420);
            this.txtLog.Size = new System.Drawing.Size(1176, 368);
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.TabIndex = 1;

            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtLog);

            this.ResumeLayout(false);
        }

        private void InitializeRTUPage()
        {
            // RTU配置组
            this.groupBoxRTUConfig = new GroupBox();
            this.groupBoxRTUConfig.Location = new System.Drawing.Point(20, 20);
            this.groupBoxRTUConfig.Size = new System.Drawing.Size(550, 160);
            this.groupBoxRTUConfig.Text = "串口配置";
            this.groupBoxRTUConfig.BackColor = System.Drawing.Color.WhiteSmoke;

            // RTU数据采集组
            this.groupBoxRTUData = new GroupBox();
            this.groupBoxRTUData.Location = new System.Drawing.Point(590, 20);
            this.groupBoxRTUData.Size = new System.Drawing.Size(550, 160);
            this.groupBoxRTUData.Text = "数据采集";
            this.groupBoxRTUData.BackColor = System.Drawing.Color.WhiteSmoke;

            // 标签和控件初始化
            InitializeRTUControls();

            this.tabPageRTU.Controls.Add(this.groupBoxRTUConfig);
            this.tabPageRTU.Controls.Add(this.groupBoxRTUData);
        }

        private void InitializeRTUControls()
        {
            // ========== RTU配置组控件 ==========
            int x = 20, y = 30, labelWidth = 80, controlWidth = 120, spacing = 40;

            // 第一列
            this.label1 = CreateLabel("端口号:", x, y, labelWidth);
            this.cmbPortName = CreateComboBox(x + labelWidth, y, controlWidth);

            this.label2 = CreateLabel("波特率:", x, y + spacing, labelWidth);
            this.cmbBaudRate = CreateComboBox(x + labelWidth, y + spacing, controlWidth);
            this.cmbBaudRate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });

            this.label3 = CreateLabel("数据位:", x, y + spacing * 2, labelWidth);
            this.cmbDataBits = CreateComboBox(x + labelWidth, y + spacing * 2, controlWidth);
            this.cmbDataBits.Items.AddRange(new object[] { "7", "8" });

            // 第二列
            int col2X = x + labelWidth + controlWidth + 40;
            this.label4 = CreateLabel("校验位:", col2X, y, labelWidth);
            this.cmbParity = CreateComboBox(col2X + labelWidth, y, controlWidth);
            this.cmbParity.Items.AddRange(new object[] { "无", "奇校验", "偶校验" });

            this.label5 = CreateLabel("停止位:", col2X, y + spacing, labelWidth);
            this.cmbStopBits = CreateComboBox(col2X + labelWidth, y + spacing, controlWidth);
            this.cmbStopBits.Items.AddRange(new object[] { "1", "1.5", "2" });

            // 按钮
            this.btnConnect = CreateButton("连接", col2X + labelWidth, y + spacing * 2, 80, 30, Color.LightGreen);
            this.btnDisconnect = CreateButton("断开", col2X + labelWidth + 90, y + spacing * 2, 80, 30, Color.LightCoral);
            this.btnDisconnect.Enabled = false;

            // 添加到配置组
            AddControlsToGroup(groupBoxRTUConfig, new Control[] {
                label1, cmbPortName, label2, cmbBaudRate, label3, cmbDataBits,
                label4, cmbParity, label5, cmbStopBits, btnConnect, btnDisconnect
            });

            // ========== RTU数据采集组控件 ==========
            x = 20; y = 30;

            // 第一行
            this.label6 = CreateLabel("功能码:", x, y, labelWidth);
            this.cmbFunctionCode = CreateComboBox(x + labelWidth, y, controlWidth + 40);
            this.cmbFunctionCode.Items.AddRange(new object[] {
                "01 Read Coils", "02 Read Inputs",
                "03 Read Holding Registers", "04 Read Input Registers"
            });

            this.label7 = CreateLabel("从站地址:", x + labelWidth + controlWidth + 60, y, labelWidth);
            this.txtSlaveAddress = CreateTextBox(x + labelWidth + controlWidth + 60 + labelWidth, y, 60, "1");

            // 第二行
            this.label8 = CreateLabel("起始地址:", x, y + spacing, labelWidth);
            this.txtStartAddress = CreateTextBox(x + labelWidth, y + spacing, 60, "0");

            this.label9 = CreateLabel("数据长度:", x + labelWidth + controlWidth + 60, y + spacing, labelWidth);
            this.txtNumberOfPoints = CreateTextBox(x + labelWidth + controlWidth + 60 + labelWidth, y + spacing, 60, "10");

            // 第三行
            this.label10 = CreateLabel("读取间隔(ms):", x, y + spacing * 2, labelWidth + 20);
            this.txtInterval = CreateTextBox(x + labelWidth + 20, y + spacing * 2, 60, "1000");

            this.btnStartContinuousRead = CreateButton("开始循环读取", x + labelWidth + 100, y + spacing * 2, 120, 30, Color.LightBlue);
            this.btnStopContinuousRead = CreateButton("停止循环读取", x + labelWidth + 240, y + spacing * 2, 120, 30, Color.LightSalmon);
            this.btnStopContinuousRead.Enabled = false;

            this.btnRead = CreateButton("读取数据", x + labelWidth + 380, y + spacing * 2, 100, 30, Color.LightGoldenrodYellow);

            // 添加到数据组
            AddControlsToGroup(groupBoxRTUData, new Control[] {
                label6, cmbFunctionCode, label7, txtSlaveAddress,
                label8, txtStartAddress, label9, txtNumberOfPoints,
                label10, txtInterval, btnStartContinuousRead, btnStopContinuousRead, btnRead
            });
        }

        private void InitializeTCPPage()
        {
            // TCP配置组
            this.groupBoxTCPConfig = new GroupBox();
            this.groupBoxTCPConfig.Location = new System.Drawing.Point(20, 20);
            this.groupBoxTCPConfig.Size = new System.Drawing.Size(550, 160);
            this.groupBoxTCPConfig.Text = "TCP配置";
            this.groupBoxTCPConfig.BackColor = System.Drawing.Color.WhiteSmoke;

            // TCP数据采集组
            this.groupBoxTCPData = new GroupBox();
            this.groupBoxTCPData.Location = new System.Drawing.Point(590, 20);
            this.groupBoxTCPData.Size = new System.Drawing.Size(550, 160);
            this.groupBoxTCPData.Text = "数据采集";
            this.groupBoxTCPData.BackColor = System.Drawing.Color.WhiteSmoke;

            // 标签和控件初始化
            InitializeTCPControls();

            this.tabPageTCP.Controls.Add(this.groupBoxTCPConfig);
            this.tabPageTCP.Controls.Add(this.groupBoxTCPData);
        }

        private void InitializeTCPControls()
        {
            // ========== TCP配置组控件 ==========
            int x = 20, y = 30, labelWidth = 80, controlWidth = 150, spacing = 40;

            this.label11 = CreateLabel("IP地址:", x, y, labelWidth);
            this.txtIPAddress = CreateTextBox(x + labelWidth, y, controlWidth, "192.168.1.100");

            this.label12 = CreateLabel("端口号:", x, y + spacing, labelWidth);
            this.txtPort = CreateTextBox(x + labelWidth, y + spacing, controlWidth, "502");

            // 按钮
            this.btnConnectTCP = CreateButton("连接", x + labelWidth + controlWidth + 20, y, 80, 30, Color.LightGreen);
            this.btnDisconnectTCP = CreateButton("断开", x + labelWidth + controlWidth + 20, y + spacing, 80, 30, Color.LightCoral);
            this.btnDisconnectTCP.Enabled = false;

            AddControlsToGroup(groupBoxTCPConfig, new Control[] {
                label11, txtIPAddress, label12, txtPort, btnConnectTCP, btnDisconnectTCP
            });

            // ========== TCP数据采集组控件 ==========
            x = 20; y = 30;

            // 第一行
            this.label13 = CreateLabel("功能码:", x, y, labelWidth);
            this.cmbFunctionCodeTCP = CreateComboBox(x + labelWidth, y, controlWidth);
            this.cmbFunctionCodeTCP.Items.AddRange(new object[] {
                "01 Read Coils", "02 Read Inputs",
                "03 Read Holding Registers", "04 Read Input Registers"
            });

            this.label14 = CreateLabel("从站地址:", x + labelWidth + controlWidth + 20, y, labelWidth);
            this.txtSlaveAddressTCP = CreateTextBox(x + labelWidth + controlWidth + 20 + labelWidth, y, 60, "1");

            // 第二行
            this.label15 = CreateLabel("起始地址:", x, y + spacing, labelWidth);
            this.txtStartAddressTCP = CreateTextBox(x + labelWidth, y + spacing, 60, "0");

            this.label16 = CreateLabel("数据长度:", x + labelWidth + controlWidth + 20, y + spacing, labelWidth);
            this.txtNumberOfPointsTCP = CreateTextBox(x + labelWidth + controlWidth + 20 + labelWidth, y + spacing, 60, "10");

            // 第三行
            this.label17 = CreateLabel("读取间隔(ms):", x, y + spacing * 2, labelWidth + 20);
            this.txtIntervalTCP = CreateTextBox(x + labelWidth + 20, y + spacing * 2, 60, "1000");

            this.btnStartContinuousReadTCP = CreateButton("开始循环读取", x + labelWidth + 100, y + spacing * 2, 120, 30, Color.LightBlue);
            this.btnStopContinuousReadTCP = CreateButton("停止循环读取", x + labelWidth + 240, y + spacing * 2, 120, 30, Color.LightSalmon);
            this.btnStopContinuousReadTCP.Enabled = false;

            this.btnReadTCP = CreateButton("读取数据", x + labelWidth + 380, y + spacing * 2, 100, 30, Color.LightGoldenrodYellow);

            AddControlsToGroup(groupBoxTCPData, new Control[] {
                label13, cmbFunctionCodeTCP, label14, txtSlaveAddressTCP,
                label15, txtStartAddressTCP, label16, txtNumberOfPointsTCP,
                label17, txtIntervalTCP, btnStartContinuousReadTCP, btnStopContinuousReadTCP, btnReadTCP
            });
        }

        // 辅助方法
        private Label CreateLabel(string text, int x, int y, int width)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(width, 25);
            label.TextAlign = ContentAlignment.MiddleRight;
            return label;
        }

        private TextBox CreateTextBox(int x, int y, int width, string defaultText = "")
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 25);
            textBox.Text = defaultText;
            return textBox;
        }

        private ComboBox CreateComboBox(int x, int y, int width)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(x, y);
            comboBox.Size = new Size(width, 25);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            return comboBox;
        }

        private Button CreateButton(string text, int x, int y, int width, int height, Color backColor)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            button.BackColor = backColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            return button;
        }

        private void AddControlsToGroup(GroupBox groupBox, Control[] controls)
        {
            foreach (Control control in controls)
            {
                groupBox.Controls.Add(control);
            }
        }
    }
}