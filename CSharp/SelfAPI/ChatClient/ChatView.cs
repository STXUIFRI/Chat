#region using

using System;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using ChatLib;
using Message = ChatLib.Message;

#endregion

namespace ChatClient {
    public partial class ChatView : UserControl {
        private ChatClientController _comptroller;


        private int    _currentChat     = -1;
        private string _currentChatName = "Welcome Chanel";

        public ChatView() { InitializeComponent(); }

        public void SetController(ChatClientController comptroller) { this._comptroller = comptroller; }
        public void ChatUiUpdate(Data                  packet)      { Invoke( new Action( delegate { ChatUiUpdateInternal( packet ); } ) ); }

        private void ChatView_Load(object sender, EventArgs e) {
            GetLastChats();
            GetLastMessages();
            this.UserLabel.Text   = this._comptroller.MYUSERNAME;
            this.id_label.Text    = "TODO: TAKE Token"; //this._comptroller.MYID.ToString();
            this.TokenLabel.Text  = "TOKEN: " + this._comptroller.MYTOKEN;
            this.chatNameBox.Text = "Chat "   + new Random().Next();
            this.updater.Interval = 1000;
            this.updater.Start();
        }

        private void GetLastChats() {
            if ( this._comptroller == null ) throw new ArgumentNullException( nameof(this._comptroller) );

            var lastChatsPaket = new Data( Data.ActionEnum.GET_LAST_CHATS );

            this._comptroller.PaketQueue.Enqueue( lastChatsPaket );
        }

        private void GetLastMessages() {
            if ( this._comptroller == null ) throw new ArgumentNullException( nameof(this._comptroller) );

            if ( this._currentChat == -1 ) {
                ResetMessages();
                DisplayWelcomeMessage();

                return;
            }

            var chatSelector   = new ChatInfo( this._currentChat, 0, 0, "" );
            var lastChatsPaket = Data.GetLastMessages( chatSelector );

            this._comptroller.PaketQueue.Enqueue( lastChatsPaket );
        }

        private void ChatUiUpdateInternal(Data packet) {
            switch (packet.Action) {
                case Data.ActionEnum.SUCCEED_MESSAGE_SEND:
                    break;
                case Data.ActionEnum.SUCCEED_GET_LAST_MESSAGES:
                    var ms = packet.Messages;

                    if ( ms != null ) {
                        ResetMessages();
                        this.MessageView.Items.AddRange( ms.Select( x => new ListViewItem( new[] { x.Sender, x.Text, x.Chat.ToString() } ) ).ToArray() );
                        ScrollLastMessage();
                    }
                    else {
                        Console.WriteLine( nameof(packet.Messages) + " Object Null" );
                    }

                    break;
                case Data.ActionEnum.SUCCEED_GET_LAST_CHATS:
                    var ci = packet.Chats;
                    ResetChats();

                    if ( ci != null ) {
                        this.ChatsView.Items.AddRange( ci.Select( x => new ListViewItem( new[] { x.Title, x.ChatId.ToString(), x.Creator.ToString() } ) ).ToArray() );
                    }
                    else {
                        Console.WriteLine( nameof(packet.Chats) + " Object Null" );
                    }

                    break;
                case Data.ActionEnum.SUCCEED_GET_CHAT_INFO: break;
                case Data.ActionEnum.SUCCEED_CREATE_CHAT:
                    GetLastChats();
                    break;
                case Data.ActionEnum.SUCCEED_ADD_TO_CHAT: break;
                case Data.ActionEnum.SUCCEED:             break;
                default:                                  throw new ArgumentOutOfRangeException();
            }
        }

        private void ResetChats() {
            this.ChatsView.Items.Clear();
            this.ChatsView.Items.Add( new ListViewItem( new[] { "Welcome Chat", "-1", "-1" } ) );
        }

        private void ResetMessages() {
            this.MessageView.Items.Clear();
            this.MessageView.Items.Add( new ListViewItem( new[] { "System", "________________________________________" } ) );
            this.MessageView.Items.Add( new ListViewItem( new[] { "System", "      Begin of " + this._currentChatName } ) );
            this.MessageView.Items.Add( new ListViewItem( new[] { "System", "‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾" } ) );
        }

        private void DisplayWelcomeMessage() {
            this.MessageView.Items.Add( new ListViewItem( new[] { "System", "_______________________________________" } ) );
            this.MessageView.Items.Add( new ListViewItem( new[] { "System", "|     Welcome To The Open Cat App     |" } ) );
            this.MessageView.Items.Add( new ListViewItem( new[] { "System", "|            Feature Text!            |" } ) );
            this.MessageView.Items.Add( new ListViewItem( new[] { "System", "‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾" } ) );
        }

        private void ChatsView_SelectedIndexChanged(object sender, EventArgs e) {
            if ( this.ChatsView.SelectedItems.Count > 0 ) {
                this._currentChat     = int.Parse( this.ChatsView.SelectedItems[0].SubItems[1].Text );
                this._currentChatName = this.ChatsView.SelectedItems[0].SubItems[0].Text;
                GetLastMessages();
            }
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e) {
            if ( e.KeyData == Keys.Return ) {
                SendButton_Click( sender, e );

                e.SuppressKeyPress = true;
            }
        }

        private void ScrollLastMessage() { this.MessageView.Items[this.MessageView.Items.Count - 1].EnsureVisible(); }

        private void SendButton_Click(object sender, EventArgs e) {
            string text = this.UserInput.Text;

            if ( string.IsNullOrEmpty( text ) ) {
                this.UserInput.BackColor = Color.DarkRed;
                SystemSounds.Exclamation.Play();
                return;
            }

            var msg    = new Message( text, this._currentChat );
            var packet = Data.SendMessage( msg );
            this._comptroller.PaketQueue.Enqueue( packet );

            this.MessageView.Items.Add( new ListViewItem( new[] { this.UserLabel.Text, text, this._currentChat.ToString() } ) );
            ScrollLastMessage();
            this.UserInput.Text = "";
        }

        private void UserInput_TextChanged(object sender, EventArgs e) { ( sender as Control ).BackColor = this.BackColor; }

        private void Button2_Click(object sender, EventArgs e) {
            GetLastChats();
            GetLastMessages();
        }

        private void Button1_Click(object sender, EventArgs e) { GetLastChats(); }

        private void Button3_Click(object sender, EventArgs e) { GetLastMessages(); }

        private void CChat_Click(object sender, EventArgs e) {
            if ( MessageBox.Show( "Are you Sure you Want To Create The Chat \"" + this.chatNameBox.Text + "\"", "Create Chat", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {

                var chatToCreate = new ChatInfo( 0, 0, 0, this.chatNameBox.Text );

                var packet = Data.CreateChat( chatToCreate );
                this._comptroller.PaketQueue.Enqueue( packet );
            }
        }

        private void Invite_Click(object sender, EventArgs e) {
            if ( this._currentChat == -1 ) {
                MessageBox.Show( "Select a Chat First (Not The Welcome Chat)" );
                return;
            }

            if ( MessageBox.Show( "Are you Sure you Want To invite \"" + this.inviteUsernameBox.Text + "\" to chat \"" + this._currentChatName + "\"", "Invite", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
                var chatToCreate = new ChatInfo( this._currentChat, 0, 0, "" );
                var reqUser      = LoginData.OnlyUserName( this.inviteUsernameBox.Text );

                var packet = Data.CreateInvite( chatToCreate, reqUser );
                this._comptroller.PaketQueue.Enqueue( packet );
            }
        }

        private void Updater_Tick(object sender, EventArgs e) {
            //GetLastMessages();
            //GetLastChats();
            this.columnHeader2.Width = -2;
        }
    }
}
