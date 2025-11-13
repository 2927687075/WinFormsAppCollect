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
            this.groupBoxRTUConfig.SuspendLayout();
            this.groupBoxRTUData.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRTUConfig
            // 
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
            this.groupBoxRTUConfig.Location = new System.Drawing.Point(15, 15);
            this.groupBoxRTUConfig.Name = "groupBoxRTUConfig";
            this.groupBoxRTUConfig.Size = new System.Drawing.Size(530, 150);
            this.groupBoxRTUConfig.TabIndex = 0;
            this.groupBoxRTUConfig.TabStop = false;
            this.groupBoxRTUConfig.Text = "串口配置";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(85, 30);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(100, 28);
            this.cmbPortName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(200, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(265, 30);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(100, 28);
            this.cmbBaudRate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(380, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据位:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cmbDataBits.Location = new System.Drawing.Point(445, 30);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(79, 28);
            this.cmbDataBits.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "校验位:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "无",
            "奇校验",
            "偶校验"});
            this.cmbParity.Location = new System.Drawing.Point(85, 70);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(100, 28);
            this.cmbParity.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(200, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cmbStopBits.Location = new System.Drawing.Point(265, 70);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(100, 28);
            this.cmbStopBits.TabIndex = 9;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnect.Location = new System.Drawing.Point(24, 114);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(97, 30);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.LightCoral;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(142, 114);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(102, 30);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // groupBoxRTUData
            // 
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
            this.groupBoxRTUData.Location = new System.Drawing.Point(15, 175);
            this.groupBoxRTUData.Name = "groupBoxRTUData";
            this.groupBoxRTUData.Size = new System.Drawing.Size(530, 150);
            this.groupBoxRTUData.TabIndex = 1;
            this.groupBoxRTUData.TabStop = false;
            this.groupBoxRTUData.Text = "数据采集";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "功能码:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFunctionCode
            // 
            this.cmbFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctionCode.FormattingEnabled = true;
            this.cmbFunctionCode.Items.AddRange(new object[] {
            "01 Read Coils",
            "02 Read Inputs",
            "03 Read Holding Registers",
            "04 Read Input Registers"});
            this.cmbFunctionCode.Location = new System.Drawing.Point(85, 30);
            this.cmbFunctionCode.Name = "cmbFunctionCode";
            this.cmbFunctionCode.Size = new System.Drawing.Size(138, 28);
            this.cmbFunctionCode.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(2, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "从站地址:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSlaveAddress
            // 
            this.txtSlaveAddress.Location = new System.Drawing.Point(85, 71);
            this.txtSlaveAddress.Name = "txtSlaveAddress";
            this.txtSlaveAddress.Size = new System.Drawing.Size(78, 27);
            this.txtSlaveAddress.TabIndex = 3;
            this.txtSlaveAddress.Text = "1";
            this.txtSlaveAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(169, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "起始地址:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.Location = new System.Drawing.Point(263, 71);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.Size = new System.Drawing.Size(81, 27);
            this.txtStartAddress.TabIndex = 5;
            this.txtStartAddress.Text = "0";
            this.txtStartAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(358, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "数据长度:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumberOfPoints
            // 
            this.txtNumberOfPoints.Location = new System.Drawing.Point(443, 74);
            this.txtNumberOfPoints.Name = "txtNumberOfPoints";
            this.txtNumberOfPoints.Size = new System.Drawing.Size(81, 27);
            this.txtNumberOfPoints.TabIndex = 7;
            this.txtNumberOfPoints.Text = "10";
            this.txtNumberOfPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(325, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "读取间隔(ms):";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(443, 28);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(81, 27);
            this.txtInterval.TabIndex = 9;
            this.txtInterval.Text = "1000";
            this.txtInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStartContinuousRead
            // 
            this.btnStartContinuousRead.BackColor = System.Drawing.Color.LightBlue;
            this.btnStartContinuousRead.Location = new System.Drawing.Point(120, 111);
            this.btnStartContinuousRead.Name = "btnStartContinuousRead";
            this.btnStartContinuousRead.Size = new System.Drawing.Size(124, 30);
            this.btnStartContinuousRead.TabIndex = 10;
            this.btnStartContinuousRead.Text = "开始循环读取";
            this.btnStartContinuousRead.UseVisualStyleBackColor = false;
            this.btnStartContinuousRead.Click += new System.EventHandler(this.btnStartContinuousRead_Click);
            // 
            // btnStopContinuousRead
            // 
            this.btnStopContinuousRead.BackColor = System.Drawing.Color.LightSalmon;
            this.btnStopContinuousRead.Enabled = false;
            this.btnStopContinuousRead.Location = new System.Drawing.Point(265, 111);
            this.btnStopContinuousRead.Name = "btnStopContinuousRead";
            this.btnStopContinuousRead.Size = new System.Drawing.Size(122, 30);
            this.btnStopContinuousRead.TabIndex = 11;
            this.btnStopContinuousRead.Text = "停止循环读取";
            this.btnStopContinuousRead.UseVisualStyleBackColor = false;
            this.btnStopContinuousRead.Click += new System.EventHandler(this.btnStopContinuousRead_Click);
            // 
            // btnRead
            // 
            this.btnRead.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnRead.Location = new System.Drawing.Point(6, 109);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(90, 30);
            this.btnRead.TabIndex = 12;
            this.btnRead.Text = "读取数据";
            this.btnRead.UseVisualStyleBackColor = false;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // RtuForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBoxRTUData);
            this.Controls.Add(this.groupBoxRTUConfig);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "RtuForm";
            this.Size = new System.Drawing.Size(560, 340);
            this.groupBoxRTUConfig.ResumeLayout(false);
            this.groupBoxRTUData.ResumeLayout(false);
            this.groupBoxRTUData.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}