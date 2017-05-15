namespace UdpSocket
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_PORT = new System.Windows.Forms.TextBox();
            this.RT_DEBUG = new System.Windows.Forms.RichTextBox();
            this.BT_CONNECT = new System.Windows.Forms.Button();
            this.TB_SEND = new System.Windows.Forms.TextBox();
            this.BT_SEND = new System.Windows.Forms.Button();
            this.TB_HOSTNAME = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.BT_TIMER = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // TB_PORT
            // 
            this.TB_PORT.Location = new System.Drawing.Point(44, 9);
            this.TB_PORT.Name = "TB_PORT";
            this.TB_PORT.Size = new System.Drawing.Size(100, 20);
            this.TB_PORT.TabIndex = 1;
            this.TB_PORT.Text = "2390";
            // 
            // RT_DEBUG
            // 
            this.RT_DEBUG.Location = new System.Drawing.Point(12, 61);
            this.RT_DEBUG.Name = "RT_DEBUG";
            this.RT_DEBUG.Size = new System.Drawing.Size(380, 335);
            this.RT_DEBUG.TabIndex = 2;
            this.RT_DEBUG.Text = "";
            // 
            // BT_CONNECT
            // 
            this.BT_CONNECT.Location = new System.Drawing.Point(317, 7);
            this.BT_CONNECT.Name = "BT_CONNECT";
            this.BT_CONNECT.Size = new System.Drawing.Size(75, 23);
            this.BT_CONNECT.TabIndex = 3;
            this.BT_CONNECT.Text = "Connect";
            this.BT_CONNECT.UseVisualStyleBackColor = true;
            this.BT_CONNECT.Click += new System.EventHandler(this.BT_CONNECT_Click);
            // 
            // TB_SEND
            // 
            this.TB_SEND.Location = new System.Drawing.Point(12, 35);
            this.TB_SEND.Name = "TB_SEND";
            this.TB_SEND.Size = new System.Drawing.Size(299, 20);
            this.TB_SEND.TabIndex = 4;
            // 
            // BT_SEND
            // 
            this.BT_SEND.Location = new System.Drawing.Point(317, 36);
            this.BT_SEND.Name = "BT_SEND";
            this.BT_SEND.Size = new System.Drawing.Size(42, 23);
            this.BT_SEND.TabIndex = 3;
            this.BT_SEND.Text = "Send";
            this.BT_SEND.UseVisualStyleBackColor = true;
            this.BT_SEND.Click += new System.EventHandler(this.BT_SEND_Click);
            // 
            // TB_HOSTNAME
            // 
            this.TB_HOSTNAME.Location = new System.Drawing.Point(211, 9);
            this.TB_HOSTNAME.Name = "TB_HOSTNAME";
            this.TB_HOSTNAME.Size = new System.Drawing.Size(100, 20);
            this.TB_HOSTNAME.TabIndex = 6;
            this.TB_HOSTNAME.Text = "10.0.0.104";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hostname";
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 20;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // BT_TIMER
            // 
            this.BT_TIMER.Location = new System.Drawing.Point(365, 36);
            this.BT_TIMER.Name = "BT_TIMER";
            this.BT_TIMER.Size = new System.Drawing.Size(23, 23);
            this.BT_TIMER.TabIndex = 7;
            this.BT_TIMER.Text = "T";
            this.BT_TIMER.UseVisualStyleBackColor = true;
            this.BT_TIMER.Click += new System.EventHandler(this.BT_TIMER_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 408);
            this.Controls.Add(this.BT_TIMER);
            this.Controls.Add(this.TB_HOSTNAME);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_SEND);
            this.Controls.Add(this.BT_SEND);
            this.Controls.Add(this.BT_CONNECT);
            this.Controls.Add(this.RT_DEBUG);
            this.Controls.Add(this.TB_PORT);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_PORT;
        private System.Windows.Forms.RichTextBox RT_DEBUG;
        private System.Windows.Forms.Button BT_CONNECT;
        private System.Windows.Forms.TextBox TB_SEND;
        private System.Windows.Forms.Button BT_SEND;
        private System.Windows.Forms.TextBox TB_HOSTNAME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Button BT_TIMER;
    }
}

