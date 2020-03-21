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
    public partial class ConnectionForm : UserControl {
        public ConnectionForm() { InitializeComponent(); }

        private void Connect_Click(object sender, EventArgs e) {
            this.Connect.Text = "Conecting...";
            var ipe = new IPEndPoint( IPAddress.Parse( this.ipaddr.Text ), (int) this.port.Value );
            OnConnectClick( ipe );
        }

        public event Action<IPEndPoint> connect_click;
        protected virtual void          OnConnectClick(IPEndPoint obj) { this.connect_click?.Invoke( obj ); }
    }
}
