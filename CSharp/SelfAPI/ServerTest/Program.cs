using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatClient;
using ChatLib;

namespace ServerTest {
    class Program {


        static void Main(string[] args) {
            StaticTcpClientOpperations.SLEEP_BETWEEN_SENDS = 10;
            new Program();
        }

        public Program() {
            //for ( int j = 0; j < 20; j++ ) {
            //    var cp = j;
            //    new Thread( () => {
            //        ChatClientController _controller = new ChatClientController();
            //        _controller.SQLINJECTIONTEST = false;
            //        _controller.StartClient( new IPEndPoint( IPAddress.Parse( "25.67.179.166" ), 6969 ), Callback );
            //        _controller.ToUI        += ControllerOnToUI;
            //        _controller.ToUIOnError += ControllerOnToUIOnError;
            //
            //        ChatLib.LoginData loginData = LoginData.CreateLoginData( "XURI" +cp, "123" );
            //        //ChatLib.LoginData loginData    = LoginData.CreateLoginData( "XURI44", "123" );
            //        //ChatLib.LoginData registerData = LoginData.CreateRegisterData( "XURI44", "123", -1, LoginData.GenderTypes.DIVERS );
            //        //this._controller.PaketQueue.Enqueue( Data.CreateRegister( registerData ) );
            //
            //        //for ( int i = 0; i < 200; i++ ) {
            //        //    ChatLib.LoginData registerData = LoginData.CreateRegisterData( "XURI" + i, "123", -1, LoginData.GenderTypes.DIVERS );
            //        //    this._controller.PaketQueue.Enqueue( Data.CreateRegister( registerData ) );
            //        //    Thread.Sleep( 100 );
            //        //}
            //        //for ( int i = 0; i < 10; i++ ) {
            //        //    //this._controller.PaketQueue.Enqueue( Data.CreateChat( new ChatInfo( 0, 0, 0, "Chat: " + i ) ) );
            //        //
            //        //    for ( int j = 0; j < 10; j++ ) {
            //        //        this._controller.PaketQueue.Enqueue( Data.CreateInvite( new ChatInfo( i, 0, 0, "" ), LoginData.OnlyUserName( "XURI" + j ) ) );
            //        //    } 
            //        //}
            //        _controller.PaketQueue.Enqueue( Data.CreateRegister( loginData ) );
            //        _controller.PaketQueue.Enqueue( Data.CreateLogin( loginData ) );
            //
            //
            //        for ( int i = 0; i < 100; i++ ) {
            //            Thread.Sleep( 500 );
            //            _controller.PaketQueue.Enqueue( Data.CreateChat( new ChatInfo( 0, 0, 0, "Chat: " + i ) ) );
            //            for ( int k = 0; k < 10; k++ ) {  
            //                Thread.Sleep( 100 );
            //                _controller.PaketQueue.Enqueue( Data.CreateInvite( new ChatInfo( i, 0, 0, "" ), LoginData.OnlyUserName( "XURI" + k ) ) );
            //            } 
            //        }
            //
            //    } ).Start();
            //    
            //    Thread.Sleep( 1000 );
            //}
            ChatClientController _controller = new ChatClientController();
            _controller.SQLINJECTIONTEST = false;
            _controller.StartClient( new IPEndPoint( IPAddress.Parse( "25.67.179.166" ), 6969 ), Callback );
            _controller.ToUI        += ControllerOnToUI;
            _controller.ToUIOnError += ControllerOnToUIOnError;
            //ChatLib.LoginData loginData = LoginData.CreateLoginData( "XURI" + new Random().Next(), "123" );
            //_controller.PaketQueue.Enqueue( Data.CreateRegister( loginData ) );
            //_controller.PaketQueue.Enqueue( Data.CreateLogin( loginData ) );
                  var r = new Random();
            for ( int i = 0; i < 5; i++ ) {
                _controller.PaketQueue.Enqueue( Data.SendMessage( new Message(DateTime.Now + ": "+ r.Next(), 0, 1,0) ) );
                //Thread.Sleep( 300 );
            }

            //for ( int i = 0; i < 10; i++ ) {
            //    _controller.PaketQueue.Enqueue( Data.CreateInvite( new ChatInfo(  ),  ) );
            //    Thread.Sleep( 500 );
            //}


            Console.ReadLine();
        }

        private int errors = 0;
        private int infos = 0;
        private int callback = 0;

        private void ControllerOnToUIOnError(Data obj) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine( obj.Action );
            Console.ResetColor();
            this.errors++;
            settitel();
        }

        private void ControllerOnToUI(Data obj) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine( obj.Action );
            Console.ResetColor();
            this.infos++;
            settitel();
        }

        private void Callback(ConnectionState obj) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine( obj );
            Console.ResetColor();
            this.callback++;
            settitel();
        }

        private void settitel() { Console.Title = "ERRORS: " + this.errors + "  MESSAGES: " + this.infos + "  CALLBACKS: " + this.callback; }
    }
}
