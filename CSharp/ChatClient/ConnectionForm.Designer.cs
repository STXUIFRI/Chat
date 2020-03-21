namespace ChatClient
{
    partial class ConnectionForm
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipaddr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.port)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 168);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ip:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 263);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // ipaddr
            // 
            this.ipaddr.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ipaddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipaddr.Cursor = System.Windows.Forms.Cursors.Default;
            this.ipaddr.ForeColor = System.Drawing.Color.OrangeRed;
            this.ipaddr.Location = new System.Drawing.Point(50, 207);
            this.ipaddr.Margin = new System.Windows.Forms.Padding(50, 10, 50, 50);
            this.ipaddr.Name = "ipaddr";
            this.ipaddr.Size = new System.Drawing.Size(300, 35);
            this.ipaddr.TabIndex = 2;
            this.ipaddr.Text = "25.67.179.166";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(101, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 39);
            this.label3.TabIndex = 4;
            this.label3.Text = "Connection";
            // 
            // Connect
            // 
            this.Connect.AutoSize = true;
            this.Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Connect.Location = new System.Drawing.Point(140, 390);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(122, 41);
            this.Connect.TabIndex = 5;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(149, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 73);
            this.label4.TabIndex = 7;
            this.label4.Text = "🎞";
            // 
            // port
            // 
            this.port.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.port.ForeColor = System.Drawing.Color.OrangeRed;
            this.port.Location = new System.Drawing.Point(50, 302);
            this.port.Margin = new System.Windows.Forms.Padding(50, 10, 50, 50);
            this.port.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(300, 35);
            this.port.TabIndex = 8;
            this.port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.port.Value = new decimal(new int[] {
            6969,
            0,
            0,
            0});
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.Controls.Add(this.port);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ipaddr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.OrangeRed;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "ConnectionForm";
            this.Size = new System.Drawing.Size(400, 500);
            ((System.ComponentModel.ISupportInitialize)(this.port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipaddr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown port;
    }
}
