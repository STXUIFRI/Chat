using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Newtonsoft.Json;

namespace APTtoJava {
    internal static class Program {
        static        List<Thread> threads     = new List<Thread>();
        private const int          BUFFER_SIZE = 24 * 4;

        private static void Main(string[] args) {
            var txy = new Thread( () => {
                while ( true ) {
                    Console.ReadKey();
                    Console.SetCursorPosition( Console.CursorLeft - 1, Console.CursorTop );
                    Console.Write( "Health: " + ++G_Object.Health + "\t\t" );
                }
            } );
            txy.Start();

            for ( int i = 0; i < 100; i++ ) {
                var t = new Thread( () => {
                    var ipE = new IPEndPoint( IPAddress.Parse( "25.67.179.166" ), 6969 );
                    StartClient( ipE );
                } );
                threads.Add( t );
                t.Start();
                Thread.Sleep( 10 );
            }

            

            //StartServer(ipE);
        }

        private static void StartClient(IPEndPoint ipE) {
            TcpClient cl;

            while ( true ) {
                cl = new TcpClient();

                while ( !cl.Connected ) {
                    try {
                        Console.WriteLine( "connecting..." );
                        cl.Connect( ipE );
                    } catch (Exception e) {
                        Console.WriteLine( e.Message );
                    }
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

                    threads.Add( re );
                    threads.Add( se );

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

        private static void SendPaketThread(ref TcpClient cl) {
            while ( cl.Connected ) {
                string jsonData  = JsonConvert.SerializeObject( ( G_Object ) );
                var    dataBytes = Encoding.UTF8.GetBytes( jsonData );
                cl.Client.Send( dataBytes, dataBytes.Length, SocketFlags.None );
                Console.WriteLine( "Send Data: " + dataBytes.Length + " bytes" );
                Thread.Sleep( 100 );
            }
        }

        private static void ReceivedPaketThread(ref TcpClient cl) {
            while ( cl.Connected ) {
                SleepForClData( ref cl );

                var bytes   = RecBytes( cl );
                var dataObs = ProcessReceived( Encoding.UTF8.GetString( bytes ), cl );

                foreach ( var dataOb in dataObs ) {
                    Console.WriteLine( dataOb );
                }
            }
        }


        public static void StartServer(IPEndPoint ipE) {
            ipE.Address = IPAddress.Any;
            var server = new TcpListener( ipE );
            Console.WriteLine( "starting server..." );
            server.Start();
            Console.WriteLine( "waiting for clients..." );

            while ( true ) {
                var tcpClient = server.AcceptTcpClient();
                Console.WriteLine( "client connected..." );
                var t = new Thread( () => {
                    try {
                        HandleClient( tcpClient );
                    } catch (Exception e) {
                        Console.WriteLine( e.Message );
                    }
                } );
                threads.Add( t );
                t.Start();
            }
        }


        public static Data G_Object = new Data( 100, "Heinz, Peter", 99, Data.MethodOptions.POST, Data.ErrorOptions.ERROR_NONE, Data.ResponseOptions.OK );

        private static void HandleClient(TcpClient cl) {
            Random       rng     = new Random();
            StreamWriter sWriter = new StreamWriter( cl.GetStream(), Encoding.UTF8 );
            StreamReader sReader = new StreamReader( cl.GetStream(), Encoding.UTF8 );

            while ( cl.Connected ) {
                SleepForClData( ref cl );

                var totalBytes = RecBytes( cl );

                string readData = Encoding.UTF8.GetString( totalBytes );
                var    dataObs  = ProcessReceived( readData, cl );

                foreach ( var data1 in dataObs ) {
                    ProgressData( data1, cl );
                }
            }
        }
                                                             
        public static byte[] RecBytes(TcpClient cl) {
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

        public static List<Data> ProcessReceived(string data, TcpClient cl) {
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

        public static void ProgressData(Data data, TcpClient cl) {
            //Console.WriteLine( response );
            if ( data != null ) {
                if ( data.Error == Data.ErrorOptions.ERROR_NONE ) {
                    G_Object.Health = data.Health;

                    if ( G_Object.Health <= 0 ) {
                        G_Object.Error    = Data.ErrorOptions.ACTION_NOT_PERMITTED;
                        G_Object.Response = Data.ResponseOptions.ERROR;
                        Console.WriteLine( G_Object.Error );
                        return;
                    }

                    Console.WriteLine( DateTime.Now + ": " + G_Object );

                    string jsonData  = JsonConvert.SerializeObject( ( G_Object ) );
                    byte[] dataBytes = Encoding.UTF8.GetBytes( jsonData );
                    cl.Client.Send( dataBytes, dataBytes.Length, SocketFlags.None );
                }
                else {
                    Console.WriteLine( data.Error );
                }
            }
        }

        const int CHECK_INTERVAL = 5;

        public static void SleepForClData(ref TcpClient cl) {
            while ( cl.Available < 0 ) {
                Thread.Sleep( CHECK_INTERVAL );
            }
        }
    }
}
