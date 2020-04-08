namespace ChatClient
{
    partial class ChatView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Select A Chat To View Messages");
            this.id_label = new System.Windows.Forms.Label();
            this.UserInput = new System.Windows.Forms.TextBox();
            this.UserLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.MessageView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChatsView = new System.Windows.Forms.ListView();
            this.ChatName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.getnvites = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.inviteUsernameBox = new System.Windows.Forms.TextBox();
            this.invite = new System.Windows.Forms.Button();
            this.chatNameBox = new System.Windows.Forms.TextBox();
            this.CChat = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tabControal1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SendButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.inviteMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.invitesView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel10 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TokenLabel = new System.Windows.Forms.Label();
            this.updater = new System.Windows.Forms.Timer(this.components);
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControal1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // id_label
            // 
            this.id_label.AutoSize = true;
            this.id_label.Location = new System.Drawing.Point(106, 12);
            this.id_label.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.id_label.Name = "id_label";
            this.id_label.Size = new System.Drawing.Size(45, 29);
            this.id_label.TabIndex = 0;
            this.id_label.Text = "ID:";
            // 
            // UserInput
            // 
            this.UserInput.AcceptsReturn = true;
            this.UserInput.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.UserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserInput.Cursor = System.Windows.Forms.Cursors.Default;
            this.UserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserInput.ForeColor = System.Drawing.Color.OrangeRed;
            this.UserInput.Location = new System.Drawing.Point(0, 0);
            this.UserInput.MaxLength = 4096;
            this.UserInput.Name = "UserInput";
            this.UserInput.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.UserInput.Size = new System.Drawing.Size(553, 35);
            this.UserInput.TabIndex = 3;
            this.UserInput.TextChanged += new System.EventHandler(this.UserInput_TextChanged);
            this.UserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserInput_KeyDown);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.Location = new System.Drawing.Point(-3, 51);
            this.UserLabel.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(192, 39);
            this.UserLabel.TabIndex = 4;
            this.UserLabel.Text = "UserName";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Chats";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 42);
            this.label4.TabIndex = 7;
            this.label4.Text = "🙍‍♂️🙍‍♀️";
            // 
            // MessageView
            // 
            this.MessageView.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.MessageView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.MessageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageView.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageView.ForeColor = System.Drawing.Color.OrangeRed;
            this.MessageView.HideSelection = false;
            this.MessageView.HoverSelection = true;
            this.MessageView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MessageView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.MessageView.Location = new System.Drawing.Point(3, 3);
            this.MessageView.Name = "MessageView";
            this.MessageView.Size = new System.Drawing.Size(669, 505);
            this.MessageView.TabIndex = 8;
            this.MessageView.UseCompatibleStateImageBehavior = false;
            this.MessageView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Sender";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.Width = 579;
            // 
            // ChatsView
            // 
            this.ChatsView.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ChatsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChatName});
            this.ChatsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatsView.ForeColor = System.Drawing.Color.OrangeRed;
            this.ChatsView.HideSelection = false;
            this.ChatsView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ChatsView.Location = new System.Drawing.Point(0, 0);
            this.ChatsView.MultiSelect = false;
            this.ChatsView.Name = "ChatsView";
            this.ChatsView.Size = new System.Drawing.Size(213, 588);
            this.ChatsView.TabIndex = 8;
            this.ChatsView.UseCompatibleStateImageBehavior = false;
            this.ChatsView.View = System.Windows.Forms.View.Details;
            this.ChatsView.SelectedIndexChanged += new System.EventHandler(this.ChatsView_SelectedIndexChanged);
            // 
            // ChatName
            // 
            this.ChatName.Text = "ChatName";
            this.ChatName.Width = 193;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.id_label);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.UserLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(900, 90);
            this.panel4.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(612, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 90);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Refrech";
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.getnvites);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 20);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(282, 36);
            this.panel5.TabIndex = 13;
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(180, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 36);
            this.button3.TabIndex = 10;
            this.button3.Text = "Messages";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // getnvites
            // 
            this.getnvites.AutoSize = true;
            this.getnvites.Dock = System.Windows.Forms.DockStyle.Left;
            this.getnvites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getnvites.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getnvites.Location = new System.Drawing.Point(90, 0);
            this.getnvites.Name = "getnvites";
            this.getnvites.Size = new System.Drawing.Size(90, 36);
            this.getnvites.TabIndex = 11;
            this.getnvites.Text = "Invites";
            this.getnvites.UseVisualStyleBackColor = true;
            this.getnvites.Click += new System.EventHandler(this.Getnvites_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(282, 31);
            this.panel3.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(282, 31);
            this.button2.TabIndex = 6;
            this.button2.Text = "All";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // inviteUsernameBox
            // 
            this.inviteUsernameBox.AcceptsReturn = true;
            this.inviteUsernameBox.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.inviteUsernameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inviteUsernameBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.inviteUsernameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inviteUsernameBox.ForeColor = System.Drawing.Color.OrangeRed;
            this.inviteUsernameBox.Location = new System.Drawing.Point(89, 0);
            this.inviteUsernameBox.Name = "inviteUsernameBox";
            this.inviteUsernameBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.inviteUsernameBox.Size = new System.Drawing.Size(369, 35);
            this.inviteUsernameBox.TabIndex = 15;
            // 
            // invite
            // 
            this.invite.AutoSize = true;
            this.invite.Dock = System.Windows.Forms.DockStyle.Right;
            this.invite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.invite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invite.Location = new System.Drawing.Point(461, 31);
            this.invite.Name = "invite";
            this.invite.Size = new System.Drawing.Size(205, 77);
            this.invite.TabIndex = 14;
            this.invite.Text = "Invite to Selected Chat";
            this.invite.UseVisualStyleBackColor = true;
            this.invite.Click += new System.EventHandler(this.Invite_Click);
            // 
            // chatNameBox
            // 
            this.chatNameBox.AcceptsReturn = true;
            this.chatNameBox.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.chatNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatNameBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.chatNameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatNameBox.ForeColor = System.Drawing.Color.OrangeRed;
            this.chatNameBox.Location = new System.Drawing.Point(89, 0);
            this.chatNameBox.Name = "chatNameBox";
            this.chatNameBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.chatNameBox.Size = new System.Drawing.Size(461, 35);
            this.chatNameBox.TabIndex = 13;
            // 
            // CChat
            // 
            this.CChat.AutoSize = true;
            this.CChat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CChat.Dock = System.Windows.Forms.DockStyle.Right;
            this.CChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CChat.Location = new System.Drawing.Point(550, 0);
            this.CChat.MaximumSize = new System.Drawing.Size(0, 35);
            this.CChat.Name = "CChat";
            this.CChat.Size = new System.Drawing.Size(113, 35);
            this.CChat.TabIndex = 12;
            this.CChat.Text = "CreateChat";
            this.CChat.UseVisualStyleBackColor = true;
            this.CChat.Click += new System.EventHandler(this.CChat_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 90);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ChatsView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Size = new System.Drawing.Size(900, 588);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 11;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tabControal1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(683, 588);
            this.panel6.TabIndex = 17;
            // 
            // tabControal1
            // 
            this.tabControal1.Controls.Add(this.tabPage1);
            this.tabControal1.Controls.Add(this.tabPage2);
            this.tabControal1.Controls.Add(this.tabPage3);
            this.tabControal1.Controls.Add(this.tabPage5);
            this.tabControal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControal1.Location = new System.Drawing.Point(0, 0);
            this.tabControal1.Name = "tabControal1";
            this.tabControal1.SelectedIndex = 0;
            this.tabControal1.Size = new System.Drawing.Size(683, 588);
            this.tabControal1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.tabPage1.Controls.Add(this.MessageView);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(675, 546);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.UserInput);
            this.panel2.Controls.Add(this.SendButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 508);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(669, 35);
            this.panel2.TabIndex = 9;
            // 
            // SendButton
            // 
            this.SendButton.AutoSize = true;
            this.SendButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendButton.Location = new System.Drawing.Point(553, 0);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(116, 35);
            this.SendButton.TabIndex = 10;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(675, 562);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Utils";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel9);
            this.groupBox3.Controls.Add(this.panel8);
            this.groupBox3.Controls.Add(this.invite);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(669, 111);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Invite to Chat";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.inviteMessage);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(3, 71);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(458, 35);
            this.panel9.TabIndex = 20;
            // 
            // inviteMessage
            // 
            this.inviteMessage.AcceptsReturn = true;
            this.inviteMessage.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.inviteMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inviteMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.inviteMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inviteMessage.ForeColor = System.Drawing.Color.OrangeRed;
            this.inviteMessage.Location = new System.Drawing.Point(126, 0);
            this.inviteMessage.Name = "inviteMessage";
            this.inviteMessage.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.inviteMessage.Size = new System.Drawing.Size(332, 35);
            this.inviteMessage.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 29);
            this.label3.TabIndex = 18;
            this.label3.Text = "Message:";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.inviteUsernameBox);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 31);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(458, 40);
            this.panel8.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 29);
            this.label2.TabIndex = 16;
            this.label2.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 72);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Chat";
            // 
            // panel7
            // 
            this.panel7.AutoSize = true;
            this.panel7.Controls.Add(this.chatNameBox);
            this.panel7.Controls.Add(this.CChat);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 31);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(663, 38);
            this.panel7.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 29);
            this.label1.TabIndex = 14;
            this.label1.Text = "Name:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.tabPage3.Controls.Add(this.invitesView);
            this.tabPage3.Controls.Add(this.panel10);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(675, 562);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Invites";
            // 
            // invitesView
            // 
            this.invitesView.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.invitesView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6});
            this.invitesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.invitesView.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitesView.ForeColor = System.Drawing.Color.OrangeRed;
            this.invitesView.HideSelection = false;
            this.invitesView.HoverSelection = true;
            this.invitesView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.invitesView.Location = new System.Drawing.Point(3, 3);
            this.invitesView.Name = "invitesView";
            this.invitesView.Size = new System.Drawing.Size(669, 515);
            this.invitesView.TabIndex = 9;
            this.invitesView.UseCompatibleStateImageBehavior = false;
            this.invitesView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Sender";
            this.columnHeader3.Width = 109;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Chat";
            this.columnHeader5.Width = 81;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Message";
            this.columnHeader6.Width = 422;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.button5);
            this.panel10.Controls.Add(this.button4);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(3, 518);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(669, 41);
            this.panel10.TabIndex = 10;
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(84, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(585, 41);
            this.button5.TabIndex = 11;
            this.button5.Text = "ACCEPT";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.accept_Click);
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 41);
            this.button4.TabIndex = 6;
            this.button4.Text = "DENY";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Deny_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(675, 562);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TokenLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 678);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 22);
            this.panel1.TabIndex = 9;
            // 
            // TokenLabel
            // 
            this.TokenLabel.AutoSize = true;
            this.TokenLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TokenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TokenLabel.Location = new System.Drawing.Point(0, 0);
            this.TokenLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.TokenLabel.Name = "TokenLabel";
            this.TokenLabel.Size = new System.Drawing.Size(58, 20);
            this.TokenLabel.TabIndex = 1;
            this.TokenLabel.Text = "Token";
            // 
            // updater
            // 
            this.updater.Interval = 10000;
            this.updater.Tick += new System.EventHandler(this.Updater_Tick);
            // 
            // ChatView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.OrangeRed;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "ChatView";
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.ChatView_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tabControal1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label id_label;
        private System.Windows.Forms.TextBox UserInput;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView MessageView;
        private System.Windows.Forms.ListView ChatsView;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColumnHeader ChatName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label TokenLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox chatNameBox;
        private System.Windows.Forms.Button CChat;
        private System.Windows.Forms.TextBox inviteUsernameBox;
        private System.Windows.Forms.Button invite;
        private System.Windows.Forms.Timer updater;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TabControl tabControal1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView invitesView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox inviteMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button getnvites;
    }
}
