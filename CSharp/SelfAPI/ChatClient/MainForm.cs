using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ChatLib;

namespace ChatClient {
    public partial class MainForm : Form {
        private readonly ChatClientController _controller;

        public MainForm() {
            InitializeComponent();
            this._controller             =  new ChatClientController();
            this._controller.ToUI        += ControllerOnToUI;
            this._controller.ToUIOnError += ControllerOnToUIOnError;

            ChangeDialog( DialogPage.CONNECTION_FORM );

            this.connectionForm1.SetController( this._controller );
            this.loginForm1.SetController( this._controller );
            this.registerForm1.SetController( this._controller );
            this.chatView1.SetController( this._controller );

            this.connectionForm1.OnConnectionStateUpdate += ConnectionForm1OnOnConnectionStateUpdate;

            this.loginForm1.RegisterButton.Click += (sender, args) => AimHelper( DialogPage.REGISTER_FORM, DialogPage.LOGIN_FORM );
            this.registerForm1.LoginButton.Click += (sender, args) => AimHelper( DialogPage.LOGIN_FORM,    DialogPage.REGISTER_FORM );
        }

        private void ConnectionForm1OnOnConnectionStateUpdate(ConnectionState obj) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine( obj );
            Console.ResetColor();

            if ( obj == ConnectionState.Closed ) {
                this.Invoke( new Action( () => AimHelper( DialogPage.CONNECTION_FORM, DialogPage.LOGIN_FORM ) ) );
            }
        }

        private void ControllerOnToUIOnError(Data obj) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine( obj.Action );
            Console.ResetColor();
            MessageBox.Show( obj.Action.ToString() );
        }


        private void ControllerOnToUI(Data obj) {
            Invoke( new Action( () => {
                switch (obj.Action) {
                    case Data.ActionEnum.CONNECTED:
                        AimHelper( DialogPage.LOGIN_FORM, DialogPage.CONNECTION_FORM );
                        break;
                    case Data.ActionEnum.SUCCEED_REGISTER: {
                        var loginP = this.registerForm1.GetLoginPaket();
                        this.loginForm1.setLoginData( loginP );
                        AimHelper( DialogPage.LOGIN_FORM, DialogPage.REGISTER_FORM );
                        this._controller.PaketQueue.Enqueue( loginP );
                        break;
                    }
                    case Data.ActionEnum.SUCCEED_LOGIN:
                        this._controller.MYTOKEN    = obj.Token;
                        this._controller.MYUSERNAME = this.loginForm1.GetLoginPaket().Login.Name;
                        AimHelper( DialogPage.CHAT_VIEW, DialogPage.LOGIN_FORM );
                        break;

                    case Data.ActionEnum.SUCCEED_MESSAGE_SEND:
                    case Data.ActionEnum.SUCCEED_GET_LAST_MESSAGES:
                    case Data.ActionEnum.SUCCEED_GET_LAST_CHATS:
                    case Data.ActionEnum.SUCCEED_GET_CHAT_INFO:
                    case Data.ActionEnum.SUCCEED_CREATE_CHAT:
                    case Data.ActionEnum.SUCCEED_ADD_TO_CHAT:
                    case Data.ActionEnum.SUCCEED_GET_INVITES:
                    case Data.ActionEnum.SUCCEED_ACCEPT_INVITE:
                        this.chatView1.ChatUiUpdate( obj );
                        break;
                    default: break;
                }
            } ) );
            Console.WriteLine( "UI:" + obj );
        }


        public readonly Size initSize = new Size( 416, 538 );

        private void ChangeDialog(DialogPage page) {
            switch (page) {
                case DialogPage.CONNECTION_FORM:
                    this.connectionForm1.Visible = true;
                    this.loginForm1.Visible      = false;
                    this.chatView1.Visible       = false;
                    this.registerForm1.Visible   = false;
                    break;
                case DialogPage.CHAT_VIEW:
                    this.connectionForm1.Visible = false;
                    this.loginForm1.Visible      = false;
                    this.chatView1.Visible       = true;
                    this.registerForm1.Visible   = false;
                    break;
                case DialogPage.LOGIN_FORM:
                    this.connectionForm1.Visible = false;
                    this.loginForm1.Visible      = true;
                    this.chatView1.Visible       = false;
                    this.registerForm1.Visible   = false;
                    break;
                case DialogPage.REGISTER_FORM:
                    this.connectionForm1.Visible = false;
                    this.loginForm1.Visible      = false;
                    this.chatView1.Visible       = false;
                    this.registerForm1.Visible   = true;
                    break;
                default: throw new ArgumentOutOfRangeException( nameof(page), page, null );
            }

            if ( page == DialogPage.CHAT_VIEW ) {
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                this.Size            = this.ChatSize;
            }
            else {
                this.Size            = this.initSize;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }
        }

        public readonly Size ChatSize = new Size( 900, 700 );

        private enum DialogPage {
            CONNECTION_FORM, CHAT_VIEW, LOGIN_FORM, REGISTER_FORM
        }

        private UserControl toshow;
        private UserControl tohide;
        private DialogPage  toshowtype;

        private double starte = 1;

        private void AnimationTimer_Tick(object sender, EventArgs e) {
            this.starte += this.animationTimer.Interval * Math.Max( this.starte / 10, 1 );

            var std = (int) this.starte;

            int h = this.ClientSize.Height;
            int w = this.ClientSize.Width;

            this.toshow.Size     = new Size( std,     h );
            this.tohide.Size     = new Size( w - std, h );
            this.tohide.Location = new Point( std, 0 );

            if ( this.starte >= w ) {
                bool final = true;

                if ( this.toshowtype == DialogPage.CHAT_VIEW ) {
                    if ( this.Width < this.ChatSize.Width ) {
                        this.Width += this.animationTimer.Interval * 2;
                        final      =  false;
                    }

                    if ( this.Height < this.ChatSize.Height ) {
                        this.Height += this.animationTimer.Interval * 2;
                        final       =  false;
                    }
                }

                ChangeDialog( this.toshowtype );

                if ( final ) {
                    this.toshow.Size     = this.ClientSize;
                    this.tohide.Size     = this.ClientSize;
                    this.toshow.Location = Point.Empty;
                    this.tohide.Location = Point.Empty;

                    this.toshow.Enabled = true;
                    this.tohide.Enabled = true;

                    this.tohide.Visible = false;
                    this.tohide.Dock    = DockStyle.Fill;
                    this.toshow.Dock    = DockStyle.Fill;
                    this.starte         = 1;
                    this.animationTimer.Stop();
                }
                else {
                    this.chatView1.Dock = DockStyle.Fill;
                }

                Application.DoEvents();
            }
        }

        private void AimHelper(DialogPage show, DialogPage hide) {
            if ( show == hide ) throw new Exception( "diffrent types" );

            StartAnimation( TypeToControl( show ), TypeToControl( hide ), show );
        }

        private UserControl TypeToControl(DialogPage page) {
            return page switch {
                DialogPage.CONNECTION_FORM => (UserControl) this.connectionForm1,
                DialogPage.CHAT_VIEW => this.chatView1,
                DialogPage.LOGIN_FORM => this.loginForm1,
                DialogPage.REGISTER_FORM => this.registerForm1,
                _ => throw new ArgumentOutOfRangeException( nameof(page), page, null )
            };
        }

        private void StartAnimation(UserControl tosh, UserControl tohi, DialogPage toshtype) {
            if ( this.starte != 1 ) {
                this.starte = this.Width + 10;
                Thread.Sleep( this.animationTimer.Interval * 2 );
            }

            this.toshow     = tosh;
            this.tohide     = tohi;
            this.toshowtype = toshtype;

            this.tohide.BringToFront();

            this.toshow.Enabled = false;
            this.tohide.Enabled = false;
            this.tohide.Visible = true;
            this.toshow.Visible = true;
            this.tohide.Dock    = DockStyle.None;
            this.tohide.Dock    = DockStyle.None;
            this.animationTimer.Start();
        }
    }
}
