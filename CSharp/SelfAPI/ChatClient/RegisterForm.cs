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
    public partial class RegisterForm : UserControl {
        private ChatClientController _comptroller;

        public RegisterForm() {
            InitializeComponent();
            this.genderBox.DataSource = Enum.GetValues( typeof(LoginData.GenderTypes) );
        }

        public void SetController(ChatClientController controller) { this._comptroller = controller; }

        private void Register_Click(object sender, EventArgs e) {
            var regObj = new Data( Data.ActionEnum.REGISTER, GenerateRegisterData(), "-" );
            this._comptroller.PaketQueue.Enqueue( regObj );
        }

        private object GenerateRegisterData() { return LoginData.CreateRegisterData( this.userNameBox.Text, this.passwordBox.Text, (int) this.AgeBox.Value, (LoginData.GenderTypes) this.genderBox.SelectedItem ); }
    }
}
