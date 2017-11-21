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

        static void ToonAlleLeerlingen(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Achternaam, Voornaam FROM Leerlingen";

            SqlDataReader rdr = cmd.ExecuteReader();

            Console.WriteLine("Deze leerlingen zitten in de database:");
            while (rdr.Read())
            {
                // wordt 1 keer doorlopen per rij
                Console.WriteLine("Aantal teruggekregen velden: {0}", rdr.FieldCount);

                Console.WriteLine("Voornaam: " + rdr[0]);
                Console.WriteLine("Achternaam: " + rdr[1]);
            }

            rdr.Close();
        }

        static void ToonAantalLeerlingen(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM Leerlingen";

            var aantal = cmd.ExecuteScalar();

            Console.WriteLine("Het aantal leerlingen in de database: {0}.", aantal);
        }

        static void AddLeerling(SqlConnection conn, string Voornaam, string Achternaam)
        {
            var cmd = new SqlCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "INSERT INTO Leerlingen(Voornaam, Achternaam) VALUES( " + Voornaam + "," + Achternaam + ")";
            cmd.CommandText = String.Format("INSERT INTO Leerlingen(Voornaam, Achternaam) VALUES('{0}', '{1}')", Voornaam, Achternaam);

            cmd.ExecuteNonQuery();
        }

        static void AddLeerlingSafe(SqlConnection conn, string Voornaam, string Achternaam)
        {
            var cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.Parameters.Add(new SqlParameter("vnaam", Voornaam));
            cmd.Parameters.Add(new SqlParameter("anaam", Achternaam));

            cmd.CommandText = "INSERT INTO Leerlingen(Voornaam, Achternaam) VALUES(@vnaam, @anaam)";

            cmd.ExecuteNonQuery();
        }

        static void AskUserForNewLeerling(SqlConnection conn)
        {
            Console.Write("Geef de voornaam: ");
            var voornaam = Console.ReadLine();
            Console.Write("Geef de achternaam: ");
            var achternaam = Console.ReadLine();

            AddLeerlingSafe(conn, voornaam, achternaam);
        }

        

        static void Main(string[] args)
        {
            //string connString = "";
            string connString = @"Data Source=(localdb)\v11.0;Initial Catalog=Leerlingen;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            //AddLeerling(conn, "Freddy", "F");

            ToonAantalLeerlingen(conn);
            
            ToonAlleLeerlingen(conn);
            conn.Close();
        }
    }
}
