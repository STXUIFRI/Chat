namespace ChatClient
{
    partial class MainForm
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.registerForm1 = new ChatClient.RegisterForm();
            this.connectionForm1 = new ChatClient.ConnectionForm();
            this.loginForm1 = new ChatClient.LoginForm();
            this.chatView1 = new ChatClient.ChatView();
            this.SuspendLayout();
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 3;
            this.animationTimer.Tick += new System.EventHandler(this.AnimationTimer_Tick);
            // 
            // registerForm1
            // 
            this.registerForm1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.registerForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registerForm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerForm1.ForeColor = System.Drawing.Color.OrangeRed;
            this.registerForm1.Location = new System.Drawing.Point(0, 0);
            this.registerForm1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.registerForm1.Name = "registerForm1";
            this.registerForm1.Size = new System.Drawing.Size(400, 499);
            this.registerForm1.TabIndex = 3;
            // 
            // connectionForm1
            // 
            this.connectionForm1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.connectionForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionForm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionForm1.ForeColor = System.Drawing.Color.OrangeRed;
            this.connectionForm1.Location = new System.Drawing.Point(0, 0);
            this.connectionForm1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.connectionForm1.Name = "connectionForm1";
            this.connectionForm1.Size = new System.Drawing.Size(400, 499);
            this.connectionForm1.TabIndex = 1;
            // 
            // loginForm1
            // 
            this.loginForm1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.loginForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginForm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginForm1.ForeColor = System.Drawing.Color.OrangeRed;
            this.loginForm1.Location = new System.Drawing.Point(0, 0);
            this.loginForm1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.loginForm1.Name = "loginForm1";
            this.loginForm1.Size = new System.Drawing.Size(400, 499);
            this.loginForm1.TabIndex = 0;
            // 
            // chatView1
            // 
            this.chatView1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.chatView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatView1.ForeColor = System.Drawing.Color.OrangeRed;
            this.chatView1.Location = new System.Drawing.Point(0, 0);
            this.chatView1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.chatView1.Name = "chatView1";
            this.chatView1.Size = new System.Drawing.Size(889, 713);
            this.chatView1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Purple;
            this.ClientSize = new System.Drawing.Size(400, 499);
            this.Controls.Add(this.registerForm1);
            this.Controls.Add(this.connectionForm1);
            this.Controls.Add(this.loginForm1);
            this.Controls.Add(this.chatView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "MainForm";
            this.Text = "ChatApi";
            this.ResumeLayout(false);

        }

        #endregion

        private LoginForm loginForm1;
        private ConnectionForm connectionForm1;
        private ChatView chatView1;
        private RegisterForm registerForm1;
        private System.Windows.Forms.Timer animationTimer;
    }
}

