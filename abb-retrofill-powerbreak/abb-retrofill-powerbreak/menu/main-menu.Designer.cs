namespace abb_retrofill_powerbreak.menu
{
    partial class main_menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_menu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_reports = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_interrupt = new System.Windows.Forms.Button();
            this.btn_pwbreak = new System.Windows.Forms.Button();
            this.btn_retro = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.abb_logo;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 58);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.abb_logo;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btn_close);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 391);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 59);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btn_reports);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btn_interrupt);
            this.panel3.Controls.Add(this.btn_pwbreak);
            this.panel3.Controls.Add(this.btn_retro);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(141, 333);
            this.panel3.TabIndex = 2;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.abb_logo;
            this.btn_close.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_close.Location = new System.Drawing.Point(606, 9);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(114, 37);
            this.btn_close.TabIndex = 23;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_reports
            // 
            this.btn_reports.BackColor = System.Drawing.Color.Transparent;
            this.btn_reports.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.abb_logo;
            this.btn_reports.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reports.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_reports.Location = new System.Drawing.Point(11, 171);
            this.btn_reports.Name = "btn_reports";
            this.btn_reports.Size = new System.Drawing.Size(114, 37);
            this.btn_reports.TabIndex = 22;
            this.btn_reports.Text = "Reports";
            this.btn_reports.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Main Menu";
            // 
            // btn_interrupt
            // 
            this.btn_interrupt.BackColor = System.Drawing.Color.Transparent;
            this.btn_interrupt.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.abb_logo;
            this.btn_interrupt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_interrupt.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_interrupt.Location = new System.Drawing.Point(11, 128);
            this.btn_interrupt.Name = "btn_interrupt";
            this.btn_interrupt.Size = new System.Drawing.Size(114, 37);
            this.btn_interrupt.TabIndex = 21;
            this.btn_interrupt.Text = "Interrupts";
            this.btn_interrupt.UseVisualStyleBackColor = false;
            this.btn_interrupt.Click += new System.EventHandler(this.btn_interrupt_Click);
            // 
            // btn_pwbreak
            // 
            this.btn_pwbreak.BackColor = System.Drawing.Color.Transparent;
            this.btn_pwbreak.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.abb_logo;
            this.btn_pwbreak.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pwbreak.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_pwbreak.Location = new System.Drawing.Point(11, 86);
            this.btn_pwbreak.Name = "btn_pwbreak";
            this.btn_pwbreak.Size = new System.Drawing.Size(114, 37);
            this.btn_pwbreak.TabIndex = 19;
            this.btn_pwbreak.Text = "Powerbreak";
            this.btn_pwbreak.UseVisualStyleBackColor = false;
            this.btn_pwbreak.Click += new System.EventHandler(this.btn_menu_Click);
            // 
            // btn_retro
            // 
            this.btn_retro.BackColor = System.Drawing.Color.Transparent;
            this.btn_retro.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.abb_logo;
            this.btn_retro.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_retro.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_retro.Location = new System.Drawing.Point(11, 43);
            this.btn_retro.Name = "btn_retro";
            this.btn_retro.Size = new System.Drawing.Size(114, 37);
            this.btn_retro.TabIndex = 20;
            this.btn_retro.Text = "Retrofill";
            this.btn_retro.UseVisualStyleBackColor = false;
            this.btn_retro.Click += new System.EventHandler(this.btn_retro_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.foom_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(147, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(146, 62);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(147, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(326, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "Welcome to ABB\'s Label Dashboard";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(147, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 63);
            this.label3.TabIndex = 25;
            this.label3.Text = "To Navigate through the Dashboard\r\nRefer to these Navigation Buttons \r\nOn the Lef" +
    "t";
            // 
            // main_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::abb_retrofill_powerbreak.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(733, 450);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "main_menu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABB Main Menu";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_reports;
        private System.Windows.Forms.Button btn_interrupt;
        private System.Windows.Forms.Button btn_retro;
        private System.Windows.Forms.Button btn_pwbreak;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}