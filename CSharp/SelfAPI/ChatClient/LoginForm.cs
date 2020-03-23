using System;
using System.Windows.Forms;
using ChatLib;

namespace ChatClient {
    public partial class LoginForm : UserControl {
        private ChatClientController _comptroller;
        public LoginForm() { InitializeComponent(); }
        public void SetController(ChatClientController controller) { this._comptroller = controller; }

        public Data GetLoginPaket() {
            //return new Data( Data.ActionEnum.LOGIN, LoginData.CreateLoginData("XURI0", "123") );
            
            return new Data( Data.ActionEnum.LOGIN, GenerateLoginData() );
        }

        private LoginData GenerateLoginData() { return LoginData.CreateLoginData( this.userNameBox.Text, this.passwordBox.Text ); }

        private void Login_Click(object sender, EventArgs e) { this._comptroller.PaketQueue.Enqueue( GetLoginPaket() ); }

        public void setLoginData(Data loginP) {
            if ( loginP.Login == null ) return;

            this.userNameBox.Text = loginP.Login.Name;
            this.passwordBox.Text = loginP.Login.Password;
        }
    }
}
