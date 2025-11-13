namespace WinFormsAppCollect.Forms
{
    partial class RtuForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBoxRTUConfig;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;

        private System.Windows.Forms.GroupBox groupBoxRTUData;
        private System.Windows.Forms.ComboBox cmbFunctionCode;
        private System.Windows.Forms.TextBox txtSlaveAddress;
        private System.Windows.Forms.TextBox txtStartAddress;
        private System.Windows.Forms.TextBox txtNumberOfPoints;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnStartContinuousRead;
        private System.Windows.Forms.Button btnStopContinuousRead;

        private System.Windows.Forms.Label label1, label2, label3, label4, label5;
        private System.Windows.Forms.Label label6, label7, label8, label9, label10;

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
            this.groupBoxRTUConfig = new System.Windows.Forms.GroupBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();

            this.groupBoxRTUData = new System.Windows.Forms.GroupBox();
            this.cmbFunctionCode =  new System.Windows.Forms.ComboBox();
            this.txtSlaveAddress = new System.Windows.Forms.TextBox();
            this.txtStartAddress = new System.Windows.Forms.TextBox();
            this.txtNumberOfPoints = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnStartContinuousRead = new System.Windows.Forms.Button();
            this.btnStopContinuousRead = new System.Windows.Forms.Button();

            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();

            // groupBoxRTUConfig
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
            this.groupBoxRTUConfig.Location = new System.Drawing.Point(10, 10);
            this.groupBoxRTUConfig.Name = "groupBoxRTUConfig";
            this.groupBoxRTUConfig.Size = new System.Drawing.Size(540, 160);
            this.groupBoxRTUConfig.TabIndex = 0;
            this.groupBoxRTUConfig.TabStop = false;
            this.groupBoxRTUConfig.Text = "串口配置";

            // groupBoxRTUData
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
            this.groupBoxRTUData.Location = new System.Drawing.Point(10, 180);
            this.groupBoxRTUData.Name = "groupBoxRTUData";
            this.groupBoxRTUData.Size = new System.Drawing.Size(540, 160);
            this.groupBoxRTUData.TabIndex = 1;
            this.groupBoxRTUData.TabStop = false;
            this.groupBoxRTUData.Text = "数据采集";

            // Labels
            this.label1.Text = "端口号:";
            this.label2.Text = "波特率:";
            this.label3.Text = "数据位:";
            this.label4.Text = "校验位:";
            this.label5.Text = "停止位:";
            this.label6.Text = "功能码:";
            this.label7.Text = "从站地址:";
            this.label8.Text = "起始地址:";
            this.label9.Text = "数据长度:";
            this.label10.Text = "读取间隔(ms):";

            // Buttons
            this.btnConnect.Text = "连接";
            this.btnDisconnect.Text = "断开";
            this.btnRead.Text = "读取数据";
            this.btnStartContinuousRead.Text = "开始循环读取";
            this.btnStopContinuousRead.Text = "停止循环读取";

            // ComboBoxes
            this.cmbBaudRate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            this.cmbDataBits.Items.AddRange(new object[] { "7", "8" });
            this.cmbParity.Items.AddRange(new object[] { "无", "奇校验", "偶校验" });
            this.cmbStopBits.Items.AddRange(new object[] { "1", "1.5", "2" });
            this.cmbFunctionCode.Items.AddRange(new object[] {
                "01 Read Coils", "02 Read Inputs", "03 Read Holding Registers", "04 Read Input Registers"});

            // TextBoxes
            this.txtSlaveAddress.Text = "1";
            this.txtStartAddress.Text = "0";
            this.txtNumberOfPoints.Text = "10";
            this.txtInterval.Text = "1000";

            // RtuForm
            this.Controls.Add(this.groupBoxRTUData);
            this.Controls.Add(this.groupBoxRTUConfig);
            this.Name = "RtuForm";
            this.Size = new System.Drawing.Size(560, 350);
        }
    }
}