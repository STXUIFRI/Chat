#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ChatLib;

#endregion

namespace ChatClient {
    public class ChatClientController {

        public static readonly Regex theRegex = new Regex( @"/(\%27)|(\')|(\-\-)|(\%23)|(#)/ix", RegexOptions.Compiled | RegexOptions.IgnoreCase );

        private readonly List<Thread> _threads   = new List<Thread>();
        public readonly  Queue<Data>  PaketQueue = new Queue<Data>();

        private bool _running;

        private TcpClient cl;
        public  bool      SQLINJECTIONTEST = true;

        public string MYTOKEN { get; set; }

        //public int    MYID       => int.Parse( this.MYTOKEN ); //TODO: Ask For Real Id
        public string MYUSERNAME { get; set; }

        public bool Running {
            get => this._running;
            set {
                if ( !value ) {
                    while ( this._threads.Count > 0 ) {
                        this._threads[0]?.Abort();
                        this._threads.RemoveAt( 0 );
                        this.cl?.Close();
                    }

                    this._threads.Clear();
                }

                this._running = value;
            }
        }

        private async void RunClientDaemon(IPEndPoint ipE, Action<ConnectionState> callback) {
            this.Running = true;

            while ( this.Running ) {
                this.cl = new TcpClient();
                callback?.Invoke( ConnectionState.Connecting );

                try {
                    var re = new Thread( async () => {
                        try {
                            while ( !this.cl.Connected ) Thread.Sleep( StaticTcpClientOpperations.CHECK_INTERVAL );
                            await ReceivedPaketThread( this.cl, callback );
                        } catch (Exception ex) {
                            Console.WriteLine( ex );
                        }
                    } ) { Name = "ReceivePacket Thread" };

                    var se = new Thread( async () => {
                        try {
                            while ( !this.cl.Connected ) Thread.Sleep( StaticTcpClientOpperations.CHECK_INTERVAL );
                            await SendPaketThread( this.cl, callback );
                        } catch (Exception ex) {
                            Console.WriteLine( ex );
                        }
                    } ) { Name = "SendPacket Thread" };
                    this._threads.Add( re );
                    this._threads.Add( se );
                    re.Start();
                    se.Start();
                } catch (Exception e) {
                    Console.WriteLine( e );
                }

                while ( !this.cl.Connected ) {
                    try {
                        await this.cl.ConnectAsync( ipE.Address, ipE.Port );
                    } catch (Exception e) {
                        Console.WriteLine( e );
                    }

                    callback?.Invoke( ConnectionState.Broken );
                }

                callback?.Invoke( ConnectionState.Open );

                while ( this.cl.Connected ) Thread.Sleep( StaticTcpClientOpperations.RECONNECT_INTERVAL );
                callback?.Invoke( ConnectionState.Closed );

                this.Running = false;
                this.cl.Close();
                this.cl.Dispose();
                this.Running = true;
            }
        }

        public void StartClient(IPEndPoint ipE, Action<ConnectionState> callback) {
            if ( this.Running ) return;

            if ( string.IsNullOrEmpty( this.MYTOKEN ) ) this.MYTOKEN = "-";

            var t = new Thread( () => { RunClientDaemon( ipE, callback ); } ) { Name = "Network Client Thread" };
            t.Start();
            this._threads.Add( t );
        }

        private async Task SendPaketThread(TcpClient cl, Action<ConnectionState> callback) {
            try {
                while ( cl.Connected ) {
                    Data[] packets = default;

                    try {
                        while ( this.PaketQueue.Count <= 0 ) Thread.Sleep( StaticTcpClientOpperations.CHECK_INTERVAL * 2 );

                        Thread.Sleep( StaticTcpClientOpperations.SLEEP_BETWEEN_SENDS );
                        packets = this.PaketQueue.ToArray();
                        this.PaketQueue.Clear();
                        Data[] finalSend = default;

                        if ( this.SQLINJECTIONTEST ) {
                            var toSend     = new List<Data>( this.PaketQueue.Count );
                            var notAvailed = new List<Data>();

                            foreach ( var packet in packets ) {
                                packet.Token = this.MYTOKEN;
                                var vailed = true;

                                if ( packet.Chats != null )
                                    foreach ( var chat in packet.Chats )
                                        if ( !string.IsNullOrEmpty( chat.Title ) && theRegex.IsMatch( chat.Title ) )
                                            vailed = false;

                                if ( packet.Messages != null )
                                    foreach ( var chat in packet.Messages )
                                        if ( !string.IsNullOrEmpty( chat.Text ) && theRegex.IsMatch( chat.Text ) )
                                            vailed = false;

                                if ( packet.Login != null ) {
                                    if ( !string.IsNullOrEmpty( packet.Login.Name ) && theRegex.IsMatch( packet.Login.Name ) ) vailed = false;

                                    if ( !string.IsNullOrEmpty( packet.Login.Password ) && theRegex.IsMatch( packet.Login.Password ) ) vailed = false;
                                }

                                if ( vailed ) {
                                    toSend.Add( packet );
                                }
                                else {
                                    packet.Action = Data.ActionEnum.ERROR_SQL_INJECTION_DETECTED;
                                    notAvailed.Add( packet );
                                }
                            }

                            foreach ( var data in notAvailed ) OnToUiOnError( data );

                            finalSend = toSend.ToArray();
                        }
                        else {
                            foreach ( var packet in packets ) packet.Token = this.MYTOKEN;

                            finalSend = packets;
                        }

                        await StaticTcpClientOpperations.SendDataList( finalSend, cl );
                    } catch (Exception ex) {
                        Console.WriteLine( ex );
                    }

                    if ( packets == null ) continue;

                    callback?.Invoke( ConnectionState.Executing );

                    Console.WriteLine( "Send: " + packets.Length + " " + nameof(packets) );
                }
            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine( "Suppressed Exception" );
                Console.ResetColor();
            }
        }

        private async Task ReceivedPaketThread(TcpClient cl, Action<ConnectionState> callback) {
            try {
                while ( cl.Connected ) {
                    List<Data> dataPackets = default;

                    try {
                        StaticTcpClientOpperations.SleepForClData( ref cl );

                        var    bytes      = await StaticTcpClientOpperations.RecBytes( cl );
                        string stringData = Encoding.UTF8.GetString( bytes );
                        Console.WriteLine( stringData );
                        dataPackets = StaticTcpClientOpperations.ProcessReceived( stringData );
                        ProcessReceivedPacketes( dataPackets );
                    } catch (Exception ex) {
                        Console.WriteLine( ex );
                        //OnToUiOnError( new Data(Data.ActionEnum.ERROR_CLIENT_CLOSED) );
                    }

                    if ( dataPackets == null ) continue;

                    callback?.Invoke( ConnectionState.Fetching );

                    Console.WriteLine( "Received: " + dataPackets.Count + " " + nameof(dataPackets) );
                }
            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine( "Suppressed Exception" );
                Console.ResetColor();
            }
        }

        private void ProcessReceivedPacketes(List<Data> dataPackets) {
            foreach ( var dataPacket in dataPackets )
                switch (dataPacket.Action) {
                    case Data.ActionEnum.REGISTER:
                    case Data.ActionEnum.LOGIN:
                    case Data.ActionEnum.SEND_MESSAGE:
                    case Data.ActionEnum.GET_LAST_MESSAGES:
                    case Data.ActionEnum.GET_LAST_CHATS:
                    case Data.ActionEnum.GET_CHAT_INFO:
                    case Data.ActionEnum.CREATE_CHAT:
                    case Data.ActionEnum.ADD_TO_CHAT:
                    case Data.ActionEnum.GET_INVITES:
                    case Data.ActionEnum.ACCEPT_INVITE:
                    case Data.ActionEnum.DENY_INVITE:
                        Console.WriteLine( "Fail: Not A Server Response Action:\n" + dataPacket.Action );
                        break;
                    case Data.ActionEnum.SUCCEED:
                        Console.WriteLine( "succeed" );
                        OnToUi( dataPacket );
                        break;

                    case Data.ActionEnum.SUCCEED_LOGIN:
                        if ( dataPacket.Token != null ) this.MYTOKEN = dataPacket.Token;

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine( "TOKEN SET" );
                        Console.ResetColor();
                        OnToUi( dataPacket );
                        break;
                    case Data.ActionEnum.CONNECTED:
                    case Data.ActionEnum.SUCCEED_REGISTER:
                    case Data.ActionEnum.SUCCEED_MESSAGE_SEND:
                    case Data.ActionEnum.SUCCEED_GET_LAST_MESSAGES:
                    case Data.ActionEnum.SUCCEED_GET_LAST_CHATS:
                    case Data.ActionEnum.SUCCEED_GET_CHAT_INFO:
                    case Data.ActionEnum.SUCCEED_CREATE_CHAT:
                    case Data.ActionEnum.SUCCEED_ADD_TO_CHAT:
                    case Data.ActionEnum.SUCCEED_GET_INVITES:
                    case Data.ActionEnum.SUCCEED_ACCEPT_INVITE:
                    case Data.ActionEnum.SUCCEED_DENY_INVITE:
                        OnToUi( dataPacket );
                        break;

                    case Data.ActionEnum.ERROR:
                    case Data.ActionEnum.ERROR_LOGIN:
                    case Data.ActionEnum.ERROR_REGISTER:
                    case Data.ActionEnum.ERROR_MESSAGE_SEND:
                    case Data.ActionEnum.ERROR_GET_LAST_MESSAGES:
                    case Data.ActionEnum.ERROR_GET_LAST_CHATS:
                    case Data.ActionEnum.ERROR_GET_CHAT_INFO:
                    case Data.ActionEnum.ERROR_CREATE_CHAT:
                    case Data.ActionEnum.ERROR_ADD_TO_CHAT:
                    case Data.ActionEnum.ERROR_SQL_INJECTION_DETECTED:
                    case Data.ActionEnum.ERROR_CLIENT_CLOSED:
                    case Data.ActionEnum.ERROR_GET_INVITES:
                    case Data.ActionEnum.ERROR_ACCEPT_INVITE:
                    case Data.ActionEnum.ERROR_DENY_INVITE:
                        OnToUiOnError( dataPacket );
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
        }

        public event Action<Data> ToUI;

        protected virtual void OnToUi(Data obj) { this.ToUI?.Invoke( obj ); }


        public event Action<Data> ToUIOnError;
        protected virtual void    OnToUiOnError(Data obj) { this.ToUIOnError?.Invoke( obj ); }
    }
}
