using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SelfAPI;

namespace ChatClient {
    public partial class MainForm : Form {
        private ChatClientController controller;

        public MainForm() {
            this.controller      =  new ChatClientController();
            this.controller.ToUI += ControllerOnToUI;
            InitializeComponent();
            this.loginForm1.Visible            =  false;
            this.chatView1.Visible             =  false;
            this.connectionForm1.Visible       =  true;
            this.connectionForm1.connect_click += delegate(IPEndPoint point) { System.Threading.Tasks.Task.Run( () => ConnectionForm1OnConnect_click( point ) ); };
            this.loginForm1.SendPaket          += delegate(Data       packet) { System.Threading.Tasks.Task.Run( () => this.controller.PaketQueue.Enqueue( packet ) ); };
        }

        private void ControllerOnToUI(Data obj) {
            switch (obj.Action) {
                case Data.ActionEnum.CONNECTED:
                    this.connectionForm1.Visible = false;
                    this.chatView1.Visible       = false;
                    this.loginForm1.Visible      = true;
                    break;
                case Data.ActionEnum.SUCCEED_REGISTER:
                    this.loginForm1.Enabled = false;
                    Data loginP = this.loginForm1.GetLoginPaket();
                    this.controller.PaketQueue.Enqueue( loginP );
                    break;
                case Data.ActionEnum.SUCCEED_LOGIN:
                    this.connectionForm1.Visible = false;
                    this.loginForm1.Visible      = false;
                    this.chatView1.Visible       = true;

                    break;

                default: break;
            }

            Console.WriteLine( "UI:" + obj );
        }

        private async Task ConnectionForm1OnConnect_click(IPEndPoint obj) { this.controller.StartClient( obj ); }
    }
}
