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
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.btnConnectOPC = new System.Windows.Forms.Button();
            this.btnDisconnectOPC = new System.Windows.Forms.Button();

            this.groupBoxOPCData = new System.Windows.Forms.GroupBox();
            this.txtNodeId = new System.Windows.Forms.TextBox();
            this.txtIntervalOPC = new System.Windows.Forms.TextBox();
            this.btnReadOPC = new System.Windows.Forms.Button();
            this.btnStartContinuousReadOPC = new System.Windows.Forms.Button();
            this.btnStopContinuousReadOPC = new System.Windows.Forms.Button();

            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();

            // groupBoxOPCConfig
            this.groupBoxOPCConfig.Controls.Add(this.label18);
            this.groupBoxOPCConfig.Controls.Add(this.txtServerUrl);
            this.groupBoxOPCConfig.Controls.Add(this.btnConnectOPC);
            this.groupBoxOPCConfig.Controls.Add(this.btnDisconnectOPC);
            this.groupBoxOPCConfig.Location = new System.Drawing.Point(10, 10);
            this.groupBoxOPCConfig.Name = "groupBoxOPCConfig";
            this.groupBoxOPCConfig.Size = new System.Drawing.Size(540, 160);
            this.groupBoxOPCConfig.TabIndex = 0;
            this.groupBoxOPCConfig.TabStop = false;
            this.groupBoxOPCConfig.Text = "服务器配置";

            // groupBoxOPCData
            this.groupBoxOPCData.Controls.Add(this.label19);
            this.groupBoxOPCData.Controls.Add(this.txtNodeId);
            this.groupBoxOPCData.Controls.Add(this.label20);
            this.groupBoxOPCData.Controls.Add(this.txtIntervalOPC);
            this.groupBoxOPCData.Controls.Add(this.btnReadOPC);
            this.groupBoxOPCData.Controls.Add(this.btnStartContinuousReadOPC);
            this.groupBoxOPCData.Controls.Add(this.btnStopContinuousReadOPC);
            this.groupBoxOPCData.Location = new System.Drawing.Point(10, 180);
            this.groupBoxOPCData.Name = "groupBoxOPCData";
            this.groupBoxOPCData.Size = new System.Drawing.Size(540, 160);
            this.groupBoxOPCData.TabIndex = 1;
            this.groupBoxOPCData.TabStop = false;
            this.groupBoxOPCData.Text = "数据采集";

            // Labels
            this.label18.Text = "服务器URL:";
            this.label19.Text = "节点ID:";
            this.label20.Text = "读取间隔(ms):";

            // Buttons
            this.btnConnectOPC.Text = "连接";
            this.btnDisconnectOPC.Text = "断开";
            this.btnReadOPC.Text = "读取";
            this.btnStartContinuousReadOPC.Text = "开始循环读取";
            this.btnStopContinuousReadOPC.Text = "停止循环读取";

            // TextBoxes
            this.txtServerUrl.Text = "opc.tcp://localhost:4840";
            this.txtNodeId.Text = "ns=2;s=Demo.Dynamic.Scalar.Double";
            this.txtIntervalOPC.Text = "1000";

            // OpcForm
            this.Controls.Add(this.groupBoxOPCData);
            this.Controls.Add(this.groupBoxOPCConfig);
            this.Name = "OpcForm";
            this.Size = new System.Drawing.Size(560, 350);
        }
    }
}