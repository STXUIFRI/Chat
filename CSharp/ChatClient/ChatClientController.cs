#region using

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using SelfAPI;

#endregion

namespace ChatClient {
    internal class ChatClientController {

        private const int CHECK_INTERVAL      = 5;
        public const  int SLEEP_BETWEEN_SENDS = 100;
        private const int BUFFER_SIZE         = 1024 * 4;

        public Queue<Data> PaketQueue = new Queue<Data>();

        public void StartClient(IPEndPoint ipE) {
            TcpClient cl;

            while ( true ) {
                cl = new TcpClient();

                while ( !cl.Connected )
                    try {
                        Console.WriteLine( "connecting..." );
                        cl.Connect( ipE );
                    } catch (Exception e) {
                        Console.WriteLine( e.Message );
                    }

                Console.WriteLine( "connected: " + ipE );

                try {
                    var re = new Thread( () => {
                        try {
                            ReceivedPaketThread( ref cl );
                        } catch (Exception ex) {
                            Console.WriteLine( ex.Message );
                        }
                    } );

                    var se = new Thread( () => {
                        try {
                            SendPaketThread( ref cl );
                        } catch (Exception ex) {
                            Console.WriteLine( ex.Message );
                        }
                    } );

                    re.Start();
                    se.Start();
                } catch (Exception e) {
                    Console.WriteLine( e.Message );
                }

                cl.Close();
                //while ( cl.Connected ) {
                //    Thread.Sleep( 100 );
                //}
                //
                //cl.Close();
                cl.Dispose();
            }
        }


        private void SendPaketThread(ref TcpClient cl) {
            while ( cl.Connected ) {
                while ( this.PaketQueue.Count <= 0 ) Thread.Sleep( CHECK_INTERVAL * 2 );

                Thread.Sleep( SLEEP_BETWEEN_SENDS );
                var packets = this.PaketQueue.ToArray();

                SendDataList( packets, ref cl );
                Console.WriteLine( "Send: " + packets.Length + " " + nameof(packets) );
            }
        }

        private void ReceivedPaketThread(ref TcpClient cl) {
            while ( cl.Connected ) {
                SleepForClData( ref cl );

                var bytes       = RecBytes( ref cl );
                var dataPackets = ProcessReceived( Encoding.UTF8.GetString( bytes ), ref cl );

                ProcessReceivedPacketes( dataPackets );

                Console.WriteLine( "Received: " + dataPackets.Count + " " + nameof(dataPackets) );
            }
        }

        private void ProcessReceivedPacketes(List<Data> dataPackets) {
            foreach ( var dataPacket in dataPackets ) {
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
        }

        public event Action<Data> ToUI;

        protected virtual void OnToUi(Data obj) { this.ToUI?.Invoke( obj ); }


        #region ControalMethods

        public static void SleepForClData(ref TcpClient cl) {
            while ( cl.Available < 0 ) Thread.Sleep( CHECK_INTERVAL );
        }

        public static void SendDataList(Data data, ref TcpClient cl) { SendDataList( new[] { data }, ref cl ); }

        public static void SendDataList(IEnumerable<Data> dataObjects, ref TcpClient cl) {
            foreach ( var dataO in dataObjects ) {
                cl.Client.Send( Encoding.UTF8.GetBytes( JsonConvert.SerializeObject( dataO ) ) );
                Thread.Sleep( SLEEP_BETWEEN_SENDS );
            }
        }

        private static List<Data> ProcessReceived(string data, ref TcpClient cl) {
            var dataObs = new List<Data>();

            while ( true ) {
                try {
                    dataObs.Add( JsonConvert.DeserializeObject<Data>( data ) );
                } catch (JsonReaderException e) {
                    dataObs.Add( JsonConvert.DeserializeObject<Data>( data.Substring( 0, e.LinePosition ) ) );
                    data = data.Substring( e.LinePosition );
                    continue;
                }

                break;
            }

            return dataObs;
        }

        public static byte[] RecBytes(ref TcpClient cl) {
            SleepForClData( ref cl );

            var memoryStream = new MemoryStream();

            var buffer    = new byte[BUFFER_SIZE];
            int readBytes = cl.Client.Receive( buffer );

            while ( readBytes > 0 ) {
                memoryStream.Write( buffer, 0, readBytes );

                if ( cl.Available > 0 ) readBytes = cl.Client.Receive( buffer );
                else break;
            }

            var totalBytes = memoryStream.ToArray();
            memoryStream.Close();
            return totalBytes;
        }

        #endregion

    }
}
