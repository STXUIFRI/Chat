using System;
using System.Windows.Forms;
using ChatLib;

namespace ChatClient {
    public partial class RegisterForm : UserControl {
        private ChatClientController _comptroller;

        public RegisterForm() {
            InitializeComponent();
            this.genderBox.DataSource = Enum.GetValues( typeof(LoginData.GenderTypes) );

            this.userNameBox.Text = "XURI" + new Random().Next( 0, 100 );
            this.passwordBox.Text = "123";
        }

        public void SetController(ChatClientController controller) { this._comptroller = controller; }

        private void Register_Click(object sender, EventArgs e) {
            var regObj = Data.CreateRegister(  GenerateRegisterData());
            this._comptroller.PaketQueue.Enqueue( regObj );
        }

        private LoginData GenerateRegisterData() { return LoginData.CreateRegisterData( this.userNameBox.Text, this.passwordBox.Text, (int) this.AgeBox.Value, (LoginData.GenderTypes) this.genderBox.SelectedItem ); }

        
        public Data GetLoginPaket() { return new Data( Data.ActionEnum.LOGIN, GenerateLoginData() ); }

        private LoginData GenerateLoginData() { return LoginData.CreateLoginData( this.userNameBox.Text, this.passwordBox.Text ); }
    }
}
