namespace SerialToWeb
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Refresh = new System.Windows.Forms.Button();
            this.baud = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.status_label = new System.Windows.Forms.Label();
            this.PortName = new System.Windows.Forms.ComboBox();
            this.PICSerial = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HOST_DB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PORT_DB = new System.Windows.Forms.TextBox();
            this.PASSWORD_DB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.USERNAME_DB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(375, 414);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "SeeOrders";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(83, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "OpenOrders";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Refresh);
            this.groupBox2.Controls.Add(this.baud);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.Connect);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.PortName);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 123);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Serial Connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "BaudRate";
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(158, 39);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(98, 23);
            this.Refresh.TabIndex = 13;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // baud
            // 
            this.baud.Location = new System.Drawing.Point(69, 13);
            this.baud.Name = "baud";
            this.baud.Size = new System.Drawing.Size(83, 20);
            this.baud.TabIndex = 8;
            this.baud.Text = "9600";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "PortNames";
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(158, 12);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(98, 23);
            this.Connect.TabIndex = 9;
            this.Connect.Text = "Conect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.status_label);
            this.groupBox3.Location = new System.Drawing.Point(7, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 52);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_label.ForeColor = System.Drawing.Color.Red;
            this.status_label.Location = new System.Drawing.Point(6, 16);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(120, 25);
            this.status_label.TabIndex = 5;
            this.status_label.Text = "Disconected";
            // 
            // PortName
            // 
            this.PortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortName.FormattingEnabled = true;
            this.PortName.Location = new System.Drawing.Point(69, 39);
            this.PortName.Name = "PortName";
            this.PortName.Size = new System.Drawing.Size(83, 21);
            this.PortName.TabIndex = 10;
            // 
            // PICSerial
            // 
            this.PICSerial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.PICSerial_DataReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PASSWORD_DB);
            this.groupBox1.Controls.Add(this.PORT_DB);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.USERNAME_DB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.HOST_DB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 68);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Connection Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Host";
            // 
            // HOST_DB
            // 
            this.HOST_DB.Location = new System.Drawing.Point(43, 13);
            this.HOST_DB.Name = "HOST_DB";
            this.HOST_DB.Size = new System.Drawing.Size(60, 20);
            this.HOST_DB.TabIndex = 8;
            this.HOST_DB.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Port";
            // 
            // PORT_DB
            // 
            this.PORT_DB.Location = new System.Drawing.Point(43, 39);
            this.PORT_DB.Name = "PORT_DB";
            this.PORT_DB.Size = new System.Drawing.Size(60, 20);
            this.PORT_DB.TabIndex = 20;
            this.PORT_DB.Text = "3306";
            // 
            // PASSWORD_DB
            // 
            this.PASSWORD_DB.Location = new System.Drawing.Point(170, 39);
            this.PASSWORD_DB.Name = "PASSWORD_DB";
            this.PASSWORD_DB.Size = new System.Drawing.Size(60, 20);
            this.PASSWORD_DB.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Username";
            // 
            // USERNAME_DB
            // 
            this.USERNAME_DB.Location = new System.Drawing.Point(170, 13);
            this.USERNAME_DB.Name = "USERNAME_DB";
            this.USERNAME_DB.Size = new System.Drawing.Size(60, 20);
            this.USERNAME_DB.TabIndex = 22;
            this.USERNAME_DB.Text = "root";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Password";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(12, 215);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 68);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Database Comands";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.richTextBox1);
            this.groupBox5.Location = new System.Drawing.Point(285, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(387, 443);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Database Output";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 467);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Serial2Web v0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.TextBox baud;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.ComboBox PortName;
        private System.IO.Ports.SerialPort PICSerial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HOST_DB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PORT_DB;
        private System.Windows.Forms.TextBox PASSWORD_DB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox USERNAME_DB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}

