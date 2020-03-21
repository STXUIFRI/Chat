using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SelfAPI;

namespace ChatClient {
    public partial class LoginForm : UserControl {
        public LoginForm() { InitializeComponent(); }

        private void Register_Click(object sender, EventArgs e) {
            var regObj = new Data() { Action = Data.ActionEnum.REGISTER, DataObj = GenerateLoginData(), Token = "-" };
            OnSendPaket( regObj );
        }

        public Data GetLoginPaket() { return new Data() { Action = Data.ActionEnum.LOGIN, DataObj = GenerateLoginData(), Token = "-" }; }

        private object GenerateLoginData() {
            throw new NotImplementedException();

            return "TODO:: CredentialsObject";
        }

        private void Login_Click(object sender, EventArgs e) { OnSendPaket( GetLoginPaket() ); }

        public event Action<Data> SendPaket;
        protected virtual void    OnSendPaket(Data obj) { this.SendPaket?.Invoke( obj ); }
    }
}
