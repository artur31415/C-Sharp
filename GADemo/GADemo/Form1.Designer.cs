namespace GADemo
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
            this.RT_FEEDBACK0 = new System.Windows.Forms.RichTextBox();
            this.TB_TARGET = new System.Windows.Forms.TextBox();
            this.BT_EVOLVE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_MUT_RATE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_POP_SIZE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.T_UPDATE = new System.Windows.Forms.Timer(this.components);
            this.L_HIGH = new System.Windows.Forms.Label();
            this.RT_FEEDBACK1 = new System.Windows.Forms.RichTextBox();
            this.PB_ENVIROMENT = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ENVIROMENT)).BeginInit();
            this.SuspendLayout();
            // 
            // RT_FEEDBACK0
            // 
            this.RT_FEEDBACK0.Location = new System.Drawing.Point(6, 19);
            this.RT_FEEDBACK0.Name = "RT_FEEDBACK0";
            this.RT_FEEDBACK0.Size = new System.Drawing.Size(214, 564);
            this.RT_FEEDBACK0.TabIndex = 0;
            this.RT_FEEDBACK0.Text = "";
            // 
            // TB_TARGET
            // 
            this.TB_TARGET.Location = new System.Drawing.Point(92, 27);
            this.TB_TARGET.Name = "TB_TARGET";
            this.TB_TARGET.Size = new System.Drawing.Size(166, 20);
            this.TB_TARGET.TabIndex = 1;
            this.TB_TARGET.Text = "to be or not to be";
            // 
            // BT_EVOLVE
            // 
            this.BT_EVOLVE.Location = new System.Drawing.Point(15, 159);
            this.BT_EVOLVE.Name = "BT_EVOLVE";
            this.BT_EVOLVE.Size = new System.Drawing.Size(75, 23);
            this.BT_EVOLVE.TabIndex = 2;
            this.BT_EVOLVE.Text = "Evolve";
            this.BT_EVOLVE.UseVisualStyleBackColor = true;
            this.BT_EVOLVE.Click += new System.EventHandler(this.BT_EVOLVE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Target Phrase";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RT_FEEDBACK0);
            this.groupBox1.Location = new System.Drawing.Point(306, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 589);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feedback";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_MUT_RATE);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TB_POP_SIZE);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(15, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configurations";
            // 
            // TB_MUT_RATE
            // 
            this.TB_MUT_RATE.Location = new System.Drawing.Point(83, 45);
            this.TB_MUT_RATE.Name = "TB_MUT_RATE";
            this.TB_MUT_RATE.Size = new System.Drawing.Size(83, 20);
            this.TB_MUT_RATE.TabIndex = 1;
            this.TB_MUT_RATE.Text = "0.01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mut Rate";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // TB_POP_SIZE
            // 
            this.TB_POP_SIZE.Location = new System.Drawing.Point(83, 19);
            this.TB_POP_SIZE.Name = "TB_POP_SIZE";
            this.TB_POP_SIZE.Size = new System.Drawing.Size(83, 20);
            this.TB_POP_SIZE.TabIndex = 1;
            this.TB_POP_SIZE.Text = "200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pop Size";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // T_UPDATE
            // 
            this.T_UPDATE.Tick += new System.EventHandler(this.T_UPDATE_Tick);
            // 
            // L_HIGH
            // 
            this.L_HIGH.AutoSize = true;
            this.L_HIGH.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_HIGH.Location = new System.Drawing.Point(12, 240);
            this.L_HIGH.Name = "L_HIGH";
            this.L_HIGH.Size = new System.Drawing.Size(217, 31);
            this.L_HIGH.TabIndex = 6;
            this.L_HIGH.Text = "to be or not to be";
            // 
            // RT_FEEDBACK1
            // 
            this.RT_FEEDBACK1.Location = new System.Drawing.Point(15, 300);
            this.RT_FEEDBACK1.Name = "RT_FEEDBACK1";
            this.RT_FEEDBACK1.Size = new System.Drawing.Size(285, 301);
            this.RT_FEEDBACK1.TabIndex = 1;
            this.RT_FEEDBACK1.Text = "";
            // 
            // PB_ENVIROMENT
            // 
            this.PB_ENVIROMENT.BackColor = System.Drawing.Color.Maroon;
            this.PB_ENVIROMENT.Location = new System.Drawing.Point(538, 10);
            this.PB_ENVIROMENT.Name = "PB_ENVIROMENT";
            this.PB_ENVIROMENT.Size = new System.Drawing.Size(800, 700);
            this.PB_ENVIROMENT.TabIndex = 7;
            this.PB_ENVIROMENT.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.button1_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(15, 610);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 36);
            this.panel1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 722);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PB_ENVIROMENT);
            this.Controls.Add(this.RT_FEEDBACK1);
            this.Controls.Add(this.L_HIGH);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_EVOLVE);
            this.Controls.Add(this.TB_TARGET);
            this.Name = "Form1";
            this.Text = "GA Demo MK1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ENVIROMENT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RT_FEEDBACK0;
        private System.Windows.Forms.TextBox TB_TARGET;
        private System.Windows.Forms.Button BT_EVOLVE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TB_MUT_RATE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_POP_SIZE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer T_UPDATE;
        private System.Windows.Forms.Label L_HIGH;
        private System.Windows.Forms.RichTextBox RT_FEEDBACK1;
        private System.Windows.Forms.PictureBox PB_ENVIROMENT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}

