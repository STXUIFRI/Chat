#region using

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace ChatLib {
    public static class StaticTcpClientOpperations {

        public const  int CHECK_INTERVAL      = 5;
        public const  int RECONNECT_INTERVAL  = 500;
        public const  int SLEEP_BETWEEN_SENDS = 100;
        private const int BUFFER_SIZE         = 1024 * 4;

        #region ControalMethods

        public static void SleepForClData(ref TcpClient cl) {
            while ( cl.Available < 0 ) Thread.Sleep( CHECK_INTERVAL );
        }

        private static async Task SendDataList(Data data, TcpClient cl) { await SendDataList( new[] { data }, cl ); }

        public static async Task SendDataList(IEnumerable<Data> dataObjects, TcpClient cl) {
            foreach ( var dataO in dataObjects ) {
                await cl.Client.SendAsync( new ArraySegment<byte>( Encoding.UTF8.GetBytes( JsonConvert.SerializeObject( dataO ) ) ), SocketFlags.None );
                Thread.Sleep( SLEEP_BETWEEN_SENDS );
            }
        }

        public static List<Data> ProcessReceived(string data) {
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

        public static async Task<byte[]> RecBytes(TcpClient cl) {
            SleepForClData( ref cl );

            var memoryStream = new MemoryStream();

            var buffer = new byte[BUFFER_SIZE];
            var ars    = new ArraySegment<byte>( buffer );

            int readBytes = await cl.Client.ReceiveAsync( ars, SocketFlags.None );

            while ( readBytes > 0 ) {
                await memoryStream.WriteAsync( ars.Array, 0, readBytes );

                if ( cl.Available > 0 ) readBytes = await cl.Client.ReceiveAsync( ars, SocketFlags.None );
                else break;
            }

            var totalBytes = memoryStream.ToArray();
            memoryStream.Close();
            return totalBytes;
        }

        #endregion

    }
}
