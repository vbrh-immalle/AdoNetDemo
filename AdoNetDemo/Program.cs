using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connString = "";
            string connString = @"Data Source=(localdb)\v11.0;Initial Catalog=Leerlingen;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(connString);

            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Leerlingen";

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                // wordt 1 keer doorlopen per rij
                Console.WriteLine("Aantal teruggekregen velden: {0}", rdr.FieldCount);

            }

            conn.Close();
        }
    }
}
