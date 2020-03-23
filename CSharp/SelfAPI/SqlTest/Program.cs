using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlTest {
    class Program {
        static void Main(string[] args) {
            for ( int i = 0; i < 512; i++ ) {
                Console.WriteLine( i + ": " + (char) i );
            }

            Console.ReadLine();
            string myConnectionString = "SERVER=25.67.179.166;" +
                                        "DATABASE=chat;"        +
                                        "UID=PAdmin;"           +
                                        "PASSWORD=1234;";

            MySqlConnection connection = new MySqlConnection( myConnectionString );
            MySqlCommand    command    = connection.CreateCommand();
            command.CommandText = "SELECT * FROM user";
            MySqlDataReader Reader;
            connection.Open();
            Reader = command.ExecuteReader();

            while ( Reader.Read() ) {
                string row = "";
                for ( int i = 0; i < Reader.FieldCount; i++ )
                    row += Reader.GetValue( i ).ToString() + ", ";
                Console.WriteLine( row );
            }

            connection.Close();

            Console.ReadLine();
        }
    }
}
