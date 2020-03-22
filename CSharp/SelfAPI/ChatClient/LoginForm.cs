using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatLib;

namespace ChatClient {
    public partial class LoginForm : UserControl {
        private ChatClientController _comptroller;
        public LoginForm() { InitializeComponent(); }
        public void SetController(ChatClientController controller) { this._comptroller = controller; }
        
        public Data GetLoginPaket() { return new Data( Data.ActionEnum.LOGIN, GenerateLoginData(), "-" ); }

        private object GenerateLoginData() { return LoginData.CreateLoginData( this.userNameBox.Text, this.passwordBox.Text ); }

        private void Login_Click(object sender, EventArgs e) { this._comptroller.PaketQueue.Enqueue( GetLoginPaket() ); }
    }
}
