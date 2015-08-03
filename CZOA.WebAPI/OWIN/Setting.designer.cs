namespace CZJC.WebAPI.OWIN
{
    partial class Setting
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
            this.label6 = new System.Windows.Forms.Label();
            this.txt服务IP地址 = new System.Windows.Forms.TextBox();
            this.bt确定 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "服务地址：(可多个，每行一个)";
            // 
            // txt服务IP地址
            // 
            this.txt服务IP地址.Location = new System.Drawing.Point(12, 30);
            this.txt服务IP地址.Multiline = true;
            this.txt服务IP地址.Name = "txt服务IP地址";
            this.txt服务IP地址.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt服务IP地址.Size = new System.Drawing.Size(269, 69);
            this.txt服务IP地址.TabIndex = 10;
            // 
            // bt确定
            // 
            this.bt确定.Location = new System.Drawing.Point(201, 105);
            this.bt确定.Name = "bt确定";
            this.bt确定.Size = new System.Drawing.Size(80, 28);
            this.bt确定.TabIndex = 8;
            this.bt确定.Text = "确定";
            this.bt确定.UseVisualStyleBackColor = true;
            this.bt确定.Click += new System.EventHandler(this.bt确定_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 142);
            this.Controls.Add(this.bt确定);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt服务IP地址);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务设置";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt服务IP地址;
        private System.Windows.Forms.Button bt确定;
    }
}