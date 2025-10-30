
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsAppCollect
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private ComboBox cmbPortName;
        private ComboBox cmbBaudRate;
        private ComboBox cmbDataBits;
        private ComboBox cmbParity;
        private ComboBox cmbStopBits;
        private Button btnConnect;
        private Button btnDisconnect;
        private GroupBox groupBox2;
        private ComboBox cmbFunctionCode;
        private TextBox txtSlaveAddress;
        private TextBox txtStartAddress;
        private TextBox txtNumberOfPoints;
        private Button btnRead;
        private RichTextBox txtLog;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;

        private Button btnStartContinuousRead;
        private Button btnStopContinuousRead;
        private TextBox txtInterval;
        private Label lblInterval;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbFunctionCode = new System.Windows.Forms.ComboBox();
            this.txtSlaveAddress = new System.Windows.Forms.TextBox();
            this.txtStartAddress = new System.Windows.Forms.TextBox();
            this.txtNumberOfPoints = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnStartContinuousRead = new System.Windows.Forms.Button();
            this.btnStopContinuousRead = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbPortName);
            this.groupBox1.Controls.Add(this.cmbBaudRate);
            this.groupBox1.Controls.Add(this.cmbDataBits);
            this.groupBox1.Controls.Add(this.cmbParity);
            this.groupBox1.Controls.Add(this.cmbStopBits);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(533, 172);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口配置";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据位:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(287, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "校验位:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(287, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "停止位:";
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.Location = new System.Drawing.Point(107, 31);
            this.cmbPortName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(132, 23);
            this.cmbPortName.TabIndex = 5;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.Location = new System.Drawing.Point(107, 69);
            this.cmbBaudRate.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(132, 23);
            this.cmbBaudRate.TabIndex = 6;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.Location = new System.Drawing.Point(107, 106);
            this.cmbDataBits.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(132, 23);
            this.cmbDataBits.TabIndex = 7;
            // 
            // cmbParity
            // 
            this.cmbParity.Location = new System.Drawing.Point(373, 31);
            this.cmbParity.Margin = new System.Windows.Forms.Padding(4);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(132, 23);
            this.cmbParity.TabIndex = 8;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.Location = new System.Drawing.Point(373, 69);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(132, 23);
            this.cmbStopBits.TabIndex = 9;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnect.Location = new System.Drawing.Point(331, 106);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(83, 33);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.LightCoral;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(422, 106);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(84, 33);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblInterval);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtInterval);
            this.groupBox2.Controls.Add(this.cmbFunctionCode);
            this.groupBox2.Controls.Add(this.btnStartContinuousRead);
            this.groupBox2.Controls.Add(this.txtSlaveAddress);
            this.groupBox2.Controls.Add(this.btnStopContinuousRead);
            this.groupBox2.Controls.Add(this.txtStartAddress);
            this.groupBox2.Controls.Add(this.txtNumberOfPoints);
            this.groupBox2.Controls.Add(this.btnRead);
            this.groupBox2.Location = new System.Drawing.Point(16, 212);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(533, 150);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据采集";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 29);
            this.label6.TabIndex = 0;
            this.label6.Text = "功能码:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(20, 72);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = "起始地址:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(287, 35);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 29);
            this.label8.TabIndex = 2;
            this.label8.Text = "从站地址:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(287, 72);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 29);
            this.label9.TabIndex = 3;
            this.label9.Text = "数据长度:";
            // 
            // cmbFunctionCode
            // 
            this.cmbFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctionCode.Location = new System.Drawing.Point(107, 31);
            this.cmbFunctionCode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFunctionCode.Name = "cmbFunctionCode";
            this.cmbFunctionCode.Size = new System.Drawing.Size(172, 23);
            this.cmbFunctionCode.TabIndex = 4;
            // 
            // txtSlaveAddress
            // 
            this.txtSlaveAddress.Location = new System.Drawing.Point(373, 31);
            this.txtSlaveAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtSlaveAddress.Name = "txtSlaveAddress";
            this.txtSlaveAddress.Size = new System.Drawing.Size(132, 25);
            this.txtSlaveAddress.TabIndex = 5;
            this.txtSlaveAddress.Text = "2";
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.Location = new System.Drawing.Point(107, 69);
            this.txtStartAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.Size = new System.Drawing.Size(132, 25);
            this.txtStartAddress.TabIndex = 6;
            this.txtStartAddress.Text = "0";
            // 
            // txtNumberOfPoints
            // 
            this.txtNumberOfPoints.Location = new System.Drawing.Point(373, 69);
            this.txtNumberOfPoints.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumberOfPoints.Name = "txtNumberOfPoints";
            this.txtNumberOfPoints.Size = new System.Drawing.Size(132, 25);
            this.txtNumberOfPoints.TabIndex = 7;
            this.txtNumberOfPoints.Text = "2";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(373, 106);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(133, 38);
            this.btnRead.TabIndex = 8;
            this.btnRead.Text = "读取数据";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Location = new System.Drawing.Point(16, 375);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1033, 296);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // lblInterval
            // 
            this.lblInterval.Location = new System.Drawing.Point(6, 116);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(80, 20);
            this.lblInterval.TabIndex = 3;
            this.lblInterval.Text = "读取间隔(ms):";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(86, 113);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(60, 25);
            this.txtInterval.TabIndex = 4;
            this.txtInterval.Text = "1000";
            // 
            // btnStartContinuousRead
            // 
            this.btnStartContinuousRead.Location = new System.Drawing.Point(156, 113);
            this.btnStartContinuousRead.Name = "btnStartContinuousRead";
            this.btnStartContinuousRead.Size = new System.Drawing.Size(100, 25);
            this.btnStartContinuousRead.TabIndex = 5;
            this.btnStartContinuousRead.Text = "开始循环读取";
            this.btnStartContinuousRead.UseVisualStyleBackColor = true;
            this.btnStartContinuousRead.Click += new System.EventHandler(this.btnStartContinuousRead_Click);
            // 
            // btnStopContinuousRead
            // 
            this.btnStopContinuousRead.Enabled = false;
            this.btnStopContinuousRead.Location = new System.Drawing.Point(266, 113);
            this.btnStopContinuousRead.Name = "btnStopContinuousRead";
            this.btnStopContinuousRead.Size = new System.Drawing.Size(100, 25);
            this.btnStopContinuousRead.TabIndex = 6;
            this.btnStopContinuousRead.Text = "停止循环读取";
            this.btnStopContinuousRead.UseVisualStyleBackColor = true;
            this.btnStopContinuousRead.Click += new System.EventHandler(this.btnStopContinuousRead_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 688);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtLog);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Modbus RTU 数据采集器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
