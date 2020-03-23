using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatLib;
using Newtonsoft.Json;
using Message = ChatLib.Message;

namespace ChatServer {
    class Program {
        static void Main(string[] args) { MinimalServer(); }

        private static void MinimalServer() {
            IPEndPoint ip     = new IPEndPoint( IPAddress.Any, 6969 );
            var        server = new TcpListener( ip );
            Console.WriteLine( "starting server..." );
            server.Start();

            while ( true ) {
                Console.WriteLine( "waiting for clients..." );
                var cl = server.AcceptTcpClient();
                Task.Run( () => HandleClient( cl ) );
            }
        }

        static readonly Data welcomePaket = new Data( Data.ActionEnum.CONNECTED );

        static public Queue<Data> PacketQueue = new Queue<Data>();

        private static void HandleClient(TcpClient cl) {
            cl.Client.Send( toJson( welcomePaket ) );

            while ( cl.Connected ) {
                SleepForClData( ref cl );

                var recBytes = RecBytes( ref cl );

                foreach ( var data in ProcessReceived( Encoding.UTF8.GetString( recBytes ) ) ) {
                    Data reData = default;
                    var r   = new Random();
                    int max = r.Next( 0, 1000 );

                    switch (data.Action) {
                        case Data.ActionEnum.CONNECTED: break;
                        case Data.ActionEnum.REGISTER:
                            reData = new Data( Data.ActionEnum.SUCCEED_REGISTER );
                            break;
                        case Data.ActionEnum.LOGIN:
                            reData = new Data( Data.ActionEnum.SUCCEED_LOGIN, "0" );
                            break;
                        case Data.ActionEnum.SEND_MESSAGE:
                            reData = new Data( Data.ActionEnum.SUCCEED_MESSAGE_SEND );
                            break;
                        case Data.ActionEnum.GET_LAST_MESSAGES:

                            if ( data.Chats != null && data.Chats.Length > 0 ) {
                                int chatID = data.Chats[0].ChatId;
                                var msList = new List<Message>();

                                for ( int i = 0; i < max; i++ ) {
                                    var mn = new Message( "Message number " + i, 10, r.NextDouble() > .5 ? chatID : 5 );
                                    msList.Add( mn );
                                }

                                reData = new Data( Data.ActionEnum.SUCCEED_GET_LAST_MESSAGES,  msList.ToArray() );
                            }
                            else {
                                reData = new Data( Data.ActionEnum.ERROR_GET_LAST_MESSAGES, "" );
                            }

                            break;
                        case Data.ActionEnum.GET_LAST_CHATS:

                            var chList = new List<ChatInfo>();

                            for ( int i = 0; i < max; i++ ) {
                                var ch = new ChatInfo( i, r.Next(), 0, "Chat " + "number " + i );
                                chList.Add( ch );
                            }

                            reData = new Data( Data.ActionEnum.SUCCEED_GET_LAST_CHATS, chList.ToArray()  );
                            break;
                        case Data.ActionEnum.GET_CHAT_INFO:
                            reData = new Data( Data.ActionEnum.SUCCEED_GET_CHAT_INFO );
                            break;
                        case Data.ActionEnum.CREATE_CHAT:
                            reData = new Data( Data.ActionEnum.SUCCEED_CREATE_CHAT );
                            break;
                        case Data.ActionEnum.ADD_TO_CHAT:
                            reData = new Data( Data.ActionEnum.SUCCEED_ADD_TO_CHAT );
                            break;

                        case Data.ActionEnum.SUCCEED:
                        case Data.ActionEnum.SUCCEED_LOGIN:
                        case Data.ActionEnum.SUCCEED_REGISTER:
                        case Data.ActionEnum.SUCCEED_MESSAGE_SEND:
                        case Data.ActionEnum.SUCCEED_GET_LAST_MESSAGES:
                        case Data.ActionEnum.SUCCEED_GET_LAST_CHATS:
                        case Data.ActionEnum.SUCCEED_GET_CHAT_INFO:
                        case Data.ActionEnum.SUCCEED_CREATE_CHAT:
                        case Data.ActionEnum.SUCCEED_ADD_TO_CHAT: break;
                        default: throw new ArgumentOutOfRangeException();
                    }

                    PacketQueue.Enqueue( reData );
                }

                while ( PacketQueue.Count > 0 ) {
                    cl.Client.Send( toJson( PacketQueue.Dequeue() ) );
                }
            }
        }

        static byte[] toJson(Data d) {
            string jsonData = JsonConvert.SerializeObject( ( d ) );
            return Encoding.UTF8.GetBytes( jsonData );
        }

        public static byte[] RecBytes(ref TcpClient cl) {
            SleepForClData( ref cl );

            MemoryStream memoryStream = new MemoryStream();

            byte[] buffer    = new byte[BUFFER_SIZE];
            int    readBytes = cl.Client.Receive( buffer );

            while ( readBytes > 0 ) {
                memoryStream.Write( buffer, 0, readBytes );

                if ( cl.Available > 0 ) {
                    readBytes = cl.Client.Receive( buffer );
                }
                else {
                    break;
                }
            }

            var totalBytes = memoryStream.ToArray();
            memoryStream.Close();
            return totalBytes;
        }

        public static int BUFFER_SIZE => 1024 * 4;

        public static void SleepForClData(ref TcpClient cl) {
            while ( cl.Available < 0 ) {
                Thread.Sleep( CHECK_INTERVAL );
            }
        }

        public static int CHECK_INTERVAL => 10;

        public static List<Data> ProcessReceived(string data) {
            var dataObs = new List<Data>();

            while ( true ) {
                try {
                    var d = JsonConvert.DeserializeObject<Data>( data );
                    dataObs.Add( d );
                } catch (JsonReaderException e) {
                    dataObs.Add( JsonConvert.DeserializeObject<Data>( data.Substring( 0, e.LinePosition ) ) );
                    data = data.Substring( e.LinePosition );
                    continue;
                } catch (JsonSerializationException e) {
                    Console.WriteLine( data );
                    Console.WriteLine( e.LinePosition );
                    Console.WriteLine( data.Substring( 0, e.LinePosition ) );
                    Console.WriteLine( data.Substring( e.LinePosition ) );
                    Console.WriteLine( e.Path );

                    Console.WriteLine( e.Message );
                    Console.WriteLine( e.StackTrace );
                }

                break;
            }

            return dataObs;
        }
    }
}
