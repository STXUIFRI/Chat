#region using

using System;
using System.Windows.Forms;
using ChatLib;
using Message = ChatLib.Message;
using Newtonsoft.Json.Linq;

#endregion

namespace ChatClient {
    public partial class ChatView : UserControl {
        private ChatClientController _comptroller;

        public ChatView() { InitializeComponent(); }

        public void SetController(ChatClientController comptroller) { this._comptroller = comptroller; }
        public void ChatUiUpdate(Data                  packet)      { Invoke( new Action( delegate { ChatUiUpdateInternal( packet ); } ) ); }

        private void ChatView_Load(object sender, EventArgs e) { }

        void GetLastChats() {
            if ( this._comptroller == null ) throw new ArgumentNullException( nameof(this._comptroller) );

            var lastChatsPaket = new Data( Data.ActionEnum.GET_LAST_CHATS );

            this._comptroller.PaketQueue.Enqueue( lastChatsPaket );
        }

        void GetLastMessages() {
            if ( this._comptroller == null ) throw new ArgumentNullException( nameof(this._comptroller) );

            var lastChatsPaket = new Data( Data.ActionEnum.GET_LAST_MESSAGES );

            this._comptroller.PaketQueue.Enqueue( lastChatsPaket );
        }


        int currentChat;

        private void ChatUiUpdateInternal(Data packet) {
            switch (packet.Action) {
                case Data.ActionEnum.SUCCEED_MESSAGE_SEND:
                    MessageBox.Show( packet.Action.ToString() );
                    break;
                case Data.ActionEnum.SUCCEED_GET_LAST_MESSAGES:
                    //if ( packet.DataObj is Message[] ms ) {
                    //    this.MessageView.Items.Clear();
                    //    this.MessageView.Items.AddRange( ms.Where( x => x.Chat == this.currentChat ).Select( x => new ListViewItem( x.Text ) ).ToArray() );
                    //}
                    //else {
                    //    Console.WriteLine( packet.DataObj.GetType() );
                    //}
                    //
                    break;
                case Data.ActionEnum.SUCCEED_GET_LAST_CHATS:
                    var ci =  packet.Chats;
                    
                    if ( ci != null ) {
                        this.MessageView.Items.Clear();
                        //this.MessageView.Items.AddRange( ci.Select( x => new ListViewItem(((ChatInfo) x).Title ) ).ToArray() );
                    }
                    else {
                        Console.WriteLine( packet.Chats.GetType() );
                    }

                    break;
                case Data.ActionEnum.SUCCEED_GET_CHAT_INFO: break;
                case Data.ActionEnum.SUCCEED_CREATE_CHAT:   break;
                case Data.ActionEnum.SUCCEED_ADD_TO_CHAT:   break;
                case Data.ActionEnum.SUCCEED:               break;
                default:                                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Button1_Click(object sender, EventArgs e) { GetLastChats(); }

        private void Button3_Click(object sender, EventArgs e) { GetLastMessages(); }
    }
}
