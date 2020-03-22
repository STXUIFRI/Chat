#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatLib;

#endregion

namespace ChatClient {
    public class ChatClientController {
        private readonly List<Thread> _threads   = new List<Thread>();
        public readonly  Queue<Data>  PaketQueue = new Queue<Data>();

        private bool _running;

        public bool Running {
            get => this._running;
            set {
                if ( !value ) {
                    while ( this._threads.Count > 0 ) {
                        this._threads[0]?.Abort();
                        this._threads.RemoveAt( 0 );
                    }

                    this._threads.Clear();
                }

                this._running = value;
            }
        }

        private async void RunClientDaemon(IPEndPoint ipE, Action<ConnectionState> callback) {
            TcpClient cl;
            this.Running = true;

            while ( this.Running ) {
                cl = new TcpClient();
                callback?.Invoke( ConnectionState.Connecting );

                try {
                    var re = new Thread( async () => {
                        try {
                            while ( !cl.Connected ) Thread.Sleep( StaticTcpClientOpperations.CHECK_INTERVAL );
                            await ReceivedPaketThread( cl, callback );
                        } catch (Exception ex) {
                            Console.WriteLine( ex.Message );
                        }
                    } ) { Name = "ReceivePacket Thread" };

                    var se = new Thread( async () => {
                        try {
                            while ( !cl.Connected ) Thread.Sleep( StaticTcpClientOpperations.CHECK_INTERVAL );
                            await SendPaketThread( cl, callback );
                        } catch (Exception ex) {
                            Console.WriteLine( ex.Message );
                        }
                    } ) { Name = "SendPacket Thread" };
                    this._threads.Add( re );
                    this._threads.Add( se );
                    re.Start();
                    se.Start();
                } catch (Exception e) {
                    Console.WriteLine( e.Message );
                }

                while ( !cl.Connected ) {
                    try {
                        await cl.ConnectAsync( ipE.Address, ipE.Port );
                    } catch (Exception e) {
                        Console.WriteLine( e.Message );
                    }

                    callback?.Invoke( ConnectionState.Broken );
                }

                callback?.Invoke( ConnectionState.Open );

                while ( cl.Connected ) Thread.Sleep( StaticTcpClientOpperations.RECONNECT_INTERVAL );
                callback?.Invoke( ConnectionState.Closed );

                this.Running = false;
                cl.Close();
                cl.Dispose();
                this.Running = true;
            }
        }

        public void StartClient(IPEndPoint ipE, Action<ConnectionState> callback) {
            if ( this.Running ) return;

            var t = new Thread( () => {
                RunClientDaemon( ipE, callback );
            } ) { Name = "Network Client Thread" };
            t.Start();
            this._threads.Add( t );
        }

        private async Task SendPaketThread(TcpClient cl, Action<ConnectionState> callback) {
            while ( cl.Connected ) {
                Data[] packets = default;

                try {
                    while ( this.PaketQueue.Count <= 0 ) Thread.Sleep( StaticTcpClientOpperations.CHECK_INTERVAL * 2 );

                    Thread.Sleep( StaticTcpClientOpperations.SLEEP_BETWEEN_SENDS );
                    packets = this.PaketQueue.ToArray();
                    this.PaketQueue.Clear();

                    await StaticTcpClientOpperations.SendDataList( packets, cl );
                } catch (Exception ex) {
                    Console.WriteLine( ex.Message );
                }

                if ( packets == null ) continue;

                callback?.Invoke( ConnectionState.Executing );

                Console.WriteLine( "Send: " + packets.Length + " " + nameof(packets) );
            }
        }

        private async Task ReceivedPaketThread(TcpClient cl, Action<ConnectionState> callback) {
            while ( cl.Connected ) {
                List<Data> dataPackets = default;

                try {
                    StaticTcpClientOpperations.SleepForClData( ref cl );

                    var bytes = await StaticTcpClientOpperations.RecBytes( cl );
                    dataPackets = StaticTcpClientOpperations.ProcessReceived( Encoding.UTF8.GetString( bytes ) );

                    ProcessReceivedPacketes( dataPackets );
                } catch (Exception ex) {
                    Console.WriteLine( ex.Message );
                }

                if ( dataPackets == null ) continue;

                callback?.Invoke( ConnectionState.Fetching );

                Console.WriteLine( "Received: " + dataPackets.Count + " " + nameof(dataPackets) );
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
                        Console.WriteLine( "Fail: Not A Server Response Action:\n" + dataPacket.Action );
                        break;
                    case Data.ActionEnum.SUCCEED:
                        Console.WriteLine( "succeed" );
                        break;

                    case Data.ActionEnum.CONNECTED:
                    case Data.ActionEnum.SUCCEED_LOGIN:
                    case Data.ActionEnum.SUCCEED_REGISTER:
                    case Data.ActionEnum.SUCCEED_MESSAGE_SEND:
                    case Data.ActionEnum.SUCCEED_GET_LAST_MESSAGES:
                    case Data.ActionEnum.SUCCEED_GET_LAST_CHATS:
                    case Data.ActionEnum.SUCCEED_GET_CHAT_INFO:
                    case Data.ActionEnum.SUCCEED_CREATE_CHAT:
                    case Data.ActionEnum.SUCCEED_ADD_TO_CHAT:
                        OnToUi( dataPacket );

                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
        }

        public event Action<Data> ToUI;

        protected virtual void OnToUi(Data obj) { this.ToUI?.Invoke( obj ); }
    }
}
