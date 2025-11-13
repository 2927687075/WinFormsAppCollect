namespace WinFormsAppCollect
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageRTU;
        private System.Windows.Forms.TabPage tabPageTCP;
        private System.Windows.Forms.TabPage tabPageOPC;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnClearLog;

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
            this.tabPageTCP = new System.Windows.Forms.TabPage();
            this.tabPageOPC =  new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnClearLog = new System.Windows.Forms.Button();

            // tabControl1
            this.tabControl1.Controls.Add(this.tabPageRTU);
            this.tabControl1.Controls.Add(this.tabPageTCP);
            this.tabControl1.Controls.Add(this.tabPageOPC);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1176, 400);
            this.tabControl1.TabIndex = 0;

            // tabPageRTU
            this.tabPageRTU.BackColor = System.Drawing.Color.White;
            this.tabPageRTU.Location = new System.Drawing.Point(4, 29);
            this.tabPageRTU.Name = "tabPageRTU";
            this.tabPageRTU.Size = new System.Drawing.Size(1168, 367);
            this.tabPageRTU.TabIndex = 0;
            this.tabPageRTU.Text = "Modbus RTU";

            // tabPageTCP
            this.tabPageTCP.BackColor = System.Drawing.Color.White;
            this.tabPageTCP.Location = new System.Drawing.Point(4, 29);
            this.tabPageTCP.Name = "tabPageTCP";
            this.tabPageTCP.Size = new System.Drawing.Size(1168, 367);
            this.tabPageTCP.TabIndex = 1;
            this.tabPageTCP.Text = "Modbus TCP";

            // tabPageOPC
            this.tabPageOPC.BackColor = System.Drawing.Color.White;
            this.tabPageOPC.Location = new System.Drawing.Point(4, 29);
            this.tabPageOPC.Name = "tabPageOPC";
            this.tabPageOPC.Size = new System.Drawing.Size(1168, 367);
            this.tabPageOPC.TabIndex = 2;
            this.tabPageOPC.Text = "OPC UA";

            // txtLog
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(12, 420);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(1176, 340);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";

            // btnClearLog
            this.btnClearLog.Location = new System.Drawing.Point(1090, 770);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(90, 30);
            this.btnClearLog.TabIndex = 2;
            this.btnClearLog.Text = "清空日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modbus数据采集器 - RTU/TCP/OPC UA协议支持";
        }
    }
}