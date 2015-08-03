namespace CZJC.WebAPI.OWIN
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bt启动服务 = new System.Windows.Forms.Button();
            this.bt服务设置 = new System.Windows.Forms.Button();
            this.rtxtMsg = new System.Windows.Forms.RichTextBox();
            this.timerMsg = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // bt启动服务
            // 
            this.bt启动服务.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt启动服务.Location = new System.Drawing.Point(108, 398);
            this.bt启动服务.Name = "bt启动服务";
            this.bt启动服务.Size = new System.Drawing.Size(90, 30);
            this.bt启动服务.TabIndex = 8;
            this.bt启动服务.Tag = "0";
            this.bt启动服务.Text = "启动服务";
            this.bt启动服务.UseVisualStyleBackColor = true;
            this.bt启动服务.Click += new System.EventHandler(this.bt启动服务_Click);
            // 
            // bt服务设置
            // 
            this.bt服务设置.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt服务设置.Location = new System.Drawing.Point(12, 398);
            this.bt服务设置.Name = "bt服务设置";
            this.bt服务设置.Size = new System.Drawing.Size(90, 30);
            this.bt服务设置.TabIndex = 7;
            this.bt服务设置.Text = "服务设置";
            this.bt服务设置.UseVisualStyleBackColor = true;
            this.bt服务设置.Click += new System.EventHandler(this.bt服务设置_Click);
            // 
            // rtxtMsg
            // 
            this.rtxtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtMsg.Location = new System.Drawing.Point(12, 12);
            this.rtxtMsg.Name = "rtxtMsg";
            this.rtxtMsg.Size = new System.Drawing.Size(560, 380);
            this.rtxtMsg.TabIndex = 6;
            this.rtxtMsg.Text = "";
            // 
            // timerMsg
            // 
            this.timerMsg.Interval = 1000;
            this.timerMsg.Tick += new System.EventHandler(this.timerMsg_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 440);
            this.Controls.Add(this.bt启动服务);
            this.Controls.Add(this.bt服务设置);
            this.Controls.Add(this.rtxtMsg);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标准化服务";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt启动服务;
        private System.Windows.Forms.Button bt服务设置;
        public System.Windows.Forms.RichTextBox rtxtMsg;
        private System.Windows.Forms.Timer timerMsg;
    }
}