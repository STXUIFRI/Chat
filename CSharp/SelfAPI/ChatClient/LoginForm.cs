using System;
using System.Security.Policy;
using System.Windows.Forms;
using ChatLib;

namespace ChatClient {
    public partial class LoginForm : UserControl {
        private ChatClientController _comptroller;
        public LoginForm() { InitializeComponent(); }
        public void SetController(ChatClientController controller) { this._comptroller = controller; }

        public Data GetLoginPaket() {
            return this.checkBox1.Checked ? new Data( Data.ActionEnum.LOGIN, LoginData.CreateLoginData( "XURI0", "123" ) ) : new Data( Data.ActionEnum.LOGIN, GenerateLoginData() );
        }

        private LoginData GenerateLoginData() { return LoginData.CreateLoginData( this.userNameBox.Text, this.passwordBox.Text ); }

        private void Login_Click(object sender, EventArgs e) { this._comptroller.PaketQueue.Enqueue( GetLoginPaket() ); }

        public void setLoginData(Data loginP) {
            if ( loginP.Login == null ) return;

            this.userNameBox.Text = loginP.Login.Name;
            this.passwordBox.Text = loginP.Login.Password;
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e) {
            if ( e.KeyCode != Keys.Return ) return;

            e.SuppressKeyPress = true;
            Login_Click( sender, e );
        }
    }
}
