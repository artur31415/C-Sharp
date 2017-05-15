namespace UdpSocketController
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RT_VERBOSE = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BT_SEARCH = new System.Windows.Forms.Button();
            this.BT_CONNECT = new System.Windows.Forms.Button();
            this.TB_HOSTNAME = new System.Windows.Forms.TextBox();
            this.TB_PORT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CB_KEY_3 = new System.Windows.Forms.ComboBox();
            this.L_KEY_3 = new System.Windows.Forms.Label();
            this.CB_KEY_2 = new System.Windows.Forms.ComboBox();
            this.L_KEY_2 = new System.Windows.Forms.Label();
            this.CB_KEY_1 = new System.Windows.Forms.ComboBox();
            this.L_KEY_1 = new System.Windows.Forms.Label();
            this.CB_KEY_0 = new System.Windows.Forms.ComboBox();
            this.L_KEY_0 = new System.Windows.Forms.Label();
            this.DemoTimer = new System.Windows.Forms.Timer(this.components);
            this.TB_TOL = new System.Windows.Forms.TrackBar();
            this.L_TOL = new System.Windows.Forms.Label();
            this.TB_MOV = new System.Windows.Forms.TrackBar();
            this.L_MOV = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_TOL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TB_MOV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RT_VERBOSE);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BT_SEARCH);
            this.groupBox1.Controls.Add(this.BT_CONNECT);
            this.groupBox1.Controls.Add(this.TB_HOSTNAME);
            this.groupBox1.Controls.Add(this.TB_PORT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 245);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conection";
            // 
            // RT_VERBOSE
            // 
            this.RT_VERBOSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RT_VERBOSE.Location = new System.Drawing.Point(12, 101);
            this.RT_VERBOSE.Name = "RT_VERBOSE";
            this.RT_VERBOSE.Size = new System.Drawing.Size(223, 134);
            this.RT_VERBOSE.TabIndex = 1;
            this.RT_VERBOSE.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Verbose";
            // 
            // BT_SEARCH
            // 
            this.BT_SEARCH.Location = new System.Drawing.Point(176, 45);
            this.BT_SEARCH.Name = "BT_SEARCH";
            this.BT_SEARCH.Size = new System.Drawing.Size(59, 20);
            this.BT_SEARCH.TabIndex = 9;
            this.BT_SEARCH.Text = "Search";
            this.BT_SEARCH.UseVisualStyleBackColor = true;
            this.BT_SEARCH.Click += new System.EventHandler(this.BT_SEARCH_Click);
            // 
            // BT_CONNECT
            // 
            this.BT_CONNECT.Location = new System.Drawing.Point(176, 19);
            this.BT_CONNECT.Name = "BT_CONNECT";
            this.BT_CONNECT.Size = new System.Drawing.Size(59, 20);
            this.BT_CONNECT.TabIndex = 9;
            this.BT_CONNECT.Text = "Connect";
            this.BT_CONNECT.UseVisualStyleBackColor = true;
            this.BT_CONNECT.Click += new System.EventHandler(this.BT_CONNECT_Click);
            // 
            // TB_HOSTNAME
            // 
            this.TB_HOSTNAME.Location = new System.Drawing.Point(70, 45);
            this.TB_HOSTNAME.Name = "TB_HOSTNAME";
            this.TB_HOSTNAME.Size = new System.Drawing.Size(100, 20);
            this.TB_HOSTNAME.TabIndex = 11;
            this.TB_HOSTNAME.Text = "10.0.0.104";
            // 
            // TB_PORT
            // 
            this.TB_PORT.Location = new System.Drawing.Point(70, 19);
            this.TB_PORT.Name = "TB_PORT";
            this.TB_PORT.Size = new System.Drawing.Size(100, 20);
            this.TB_PORT.TabIndex = 8;
            this.TB_PORT.Text = "2390";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Hostname";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.L_MOV);
            this.groupBox2.Controls.Add(this.L_TOL);
            this.groupBox2.Controls.Add(this.TB_MOV);
            this.groupBox2.Controls.Add(this.TB_TOL);
            this.groupBox2.Controls.Add(this.CB_KEY_3);
            this.groupBox2.Controls.Add(this.L_KEY_3);
            this.groupBox2.Controls.Add(this.CB_KEY_2);
            this.groupBox2.Controls.Add(this.L_KEY_2);
            this.groupBox2.Controls.Add(this.CB_KEY_1);
            this.groupBox2.Controls.Add(this.L_KEY_1);
            this.groupBox2.Controls.Add(this.CB_KEY_0);
            this.groupBox2.Controls.Add(this.L_KEY_0);
            this.groupBox2.Location = new System.Drawing.Point(262, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 245);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // CB_KEY_3
            // 
            this.CB_KEY_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_KEY_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_KEY_3.FormattingEnabled = true;
            this.CB_KEY_3.Location = new System.Drawing.Point(59, 109);
            this.CB_KEY_3.Name = "CB_KEY_3";
            this.CB_KEY_3.Size = new System.Drawing.Size(93, 24);
            this.CB_KEY_3.TabIndex = 16;
            // 
            // L_KEY_3
            // 
            this.L_KEY_3.AutoSize = true;
            this.L_KEY_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_KEY_3.Location = new System.Drawing.Point(6, 112);
            this.L_KEY_3.Name = "L_KEY_3";
            this.L_KEY_3.Size = new System.Drawing.Size(47, 17);
            this.L_KEY_3.TabIndex = 17;
            this.L_KEY_3.Text = "KEY 3";
            // 
            // CB_KEY_2
            // 
            this.CB_KEY_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_KEY_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_KEY_2.FormattingEnabled = true;
            this.CB_KEY_2.Location = new System.Drawing.Point(59, 79);
            this.CB_KEY_2.Name = "CB_KEY_2";
            this.CB_KEY_2.Size = new System.Drawing.Size(93, 24);
            this.CB_KEY_2.TabIndex = 14;
            // 
            // L_KEY_2
            // 
            this.L_KEY_2.AutoSize = true;
            this.L_KEY_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_KEY_2.Location = new System.Drawing.Point(6, 82);
            this.L_KEY_2.Name = "L_KEY_2";
            this.L_KEY_2.Size = new System.Drawing.Size(47, 17);
            this.L_KEY_2.TabIndex = 15;
            this.L_KEY_2.Text = "KEY 2";
            // 
            // CB_KEY_1
            // 
            this.CB_KEY_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_KEY_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_KEY_1.FormattingEnabled = true;
            this.CB_KEY_1.Location = new System.Drawing.Point(59, 49);
            this.CB_KEY_1.Name = "CB_KEY_1";
            this.CB_KEY_1.Size = new System.Drawing.Size(93, 24);
            this.CB_KEY_1.TabIndex = 12;
            // 
            // L_KEY_1
            // 
            this.L_KEY_1.AutoSize = true;
            this.L_KEY_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_KEY_1.Location = new System.Drawing.Point(6, 52);
            this.L_KEY_1.Name = "L_KEY_1";
            this.L_KEY_1.Size = new System.Drawing.Size(47, 17);
            this.L_KEY_1.TabIndex = 13;
            this.L_KEY_1.Text = "KEY 1";
            // 
            // CB_KEY_0
            // 
            this.CB_KEY_0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_KEY_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_KEY_0.FormattingEnabled = true;
            this.CB_KEY_0.Location = new System.Drawing.Point(59, 19);
            this.CB_KEY_0.Name = "CB_KEY_0";
            this.CB_KEY_0.Size = new System.Drawing.Size(93, 24);
            this.CB_KEY_0.TabIndex = 2;
            // 
            // L_KEY_0
            // 
            this.L_KEY_0.AutoSize = true;
            this.L_KEY_0.BackColor = System.Drawing.Color.Transparent;
            this.L_KEY_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_KEY_0.Location = new System.Drawing.Point(6, 22);
            this.L_KEY_0.Name = "L_KEY_0";
            this.L_KEY_0.Size = new System.Drawing.Size(47, 17);
            this.L_KEY_0.TabIndex = 11;
            this.L_KEY_0.Text = "KEY 0";
            // 
            // DemoTimer
            // 
            this.DemoTimer.Interval = 50;
            this.DemoTimer.Tick += new System.EventHandler(this.DemoTimer_Tick);
            // 
            // TB_TOL
            // 
            this.TB_TOL.Location = new System.Drawing.Point(9, 139);
            this.TB_TOL.Maximum = 99;
            this.TB_TOL.Name = "TB_TOL";
            this.TB_TOL.Size = new System.Drawing.Size(104, 45);
            this.TB_TOL.TabIndex = 18;
            this.TB_TOL.Scroll += new System.EventHandler(this.TB_TOL_Scroll);
            // 
            // L_TOL
            // 
            this.L_TOL.AutoSize = true;
            this.L_TOL.BackColor = System.Drawing.Color.Transparent;
            this.L_TOL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_TOL.Location = new System.Drawing.Point(117, 153);
            this.L_TOL.Name = "L_TOL";
            this.L_TOL.Size = new System.Drawing.Size(60, 17);
            this.L_TOL.TabIndex = 19;
            this.L_TOL.Text = "Tol = 99";
            this.L_TOL.Click += new System.EventHandler(this.label4_Click);
            // 
            // TB_MOV
            // 
            this.TB_MOV.Location = new System.Drawing.Point(9, 190);
            this.TB_MOV.Maximum = 50;
            this.TB_MOV.Name = "TB_MOV";
            this.TB_MOV.Size = new System.Drawing.Size(104, 45);
            this.TB_MOV.TabIndex = 18;
            this.TB_MOV.Scroll += new System.EventHandler(this.TB_MOV_Scroll);
            // 
            // L_MOV
            // 
            this.L_MOV.AutoSize = true;
            this.L_MOV.BackColor = System.Drawing.Color.Transparent;
            this.L_MOV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_MOV.Location = new System.Drawing.Point(119, 204);
            this.L_MOV.Name = "L_MOV";
            this.L_MOV.Size = new System.Drawing.Size(58, 17);
            this.L_MOV.TabIndex = 19;
            this.L_MOV.Text = "Mov = 1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 267);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "WiGlove MK I";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TB_TOL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TB_MOV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox RT_VERBOSE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BT_CONNECT;
        private System.Windows.Forms.TextBox TB_HOSTNAME;
        private System.Windows.Forms.TextBox TB_PORT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CB_KEY_0;
        private System.Windows.Forms.Label L_KEY_0;
        private System.Windows.Forms.ComboBox CB_KEY_3;
        private System.Windows.Forms.Label L_KEY_3;
        private System.Windows.Forms.ComboBox CB_KEY_2;
        private System.Windows.Forms.Label L_KEY_2;
        private System.Windows.Forms.ComboBox CB_KEY_1;
        private System.Windows.Forms.Label L_KEY_1;
        private System.Windows.Forms.Timer DemoTimer;
        private System.Windows.Forms.Button BT_SEARCH;
        private System.Windows.Forms.Label L_MOV;
        private System.Windows.Forms.Label L_TOL;
        private System.Windows.Forms.TrackBar TB_MOV;
        private System.Windows.Forms.TrackBar TB_TOL;
    }
}

