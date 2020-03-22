#define LOCAL_TEST

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient {
    public sealed partial class ConnectionForm : UserControl {
        private ChatClientController _comptroller;
        public ConnectionForm() { InitializeComponent(); }

        public void SetController(ChatClientController comptroller) { this._comptroller = comptroller; }

        private void Connect_Click(object sender, EventArgs e) {
            this.Connect.Text  =  "   Abort  ";
            this.Connect.Click -= Connect_Click;
            this.Connect.Click += Abort_Click;

            var ipe = new IPEndPoint(
            #if LOCAL_TEST
                IPAddress.Parse( "127.0.0.1" )
            #else
                IPAddress.Parse( this.ipaddr.Text )
            #endif
                , (int) this.port.Value );
            this._comptroller.StartClient( ipe, OnOnConnectionStateUpdate );
        }

        private void Abort_Click(object sender, EventArgs e) {
            this.Connect.Text         =  " Connect ";
            this.Connect.Click        += Connect_Click;
            this.Connect.Click        -= Abort_Click;
            this._comptroller.Running =  false;
        }

        public event Action<ConnectionState> OnConnectionStateUpdate;

        private void OnOnConnectionStateUpdate(ConnectionState obj) { this.OnConnectionStateUpdate?.Invoke( obj ); }
    }
}
