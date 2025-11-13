namespace WinFormsAppCollect.Controls
{
    partial class LogControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnClear;

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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();

            // txtLog
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(600, 300);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(510, 310);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // LogControl
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtLog);
            this.Name = "LogControl";
            this.Size = new System.Drawing.Size(600, 350);
        }
    }
}