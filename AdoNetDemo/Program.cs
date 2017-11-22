using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    class Program
    {

        static void ToonAlleLeerlingen(MySqlConnection conn)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Achternaam, Voornaam FROM Leerlingen";

            MySqlDataReader rdr = cmd.ExecuteReader();

            Console.WriteLine("Deze leerlingen zitten in de database:");
            while (rdr.Read())
            {
                // wordt 1 keer doorlopen per rij
                Console.WriteLine("Aantal teruggekregen velden: {0}", rdr.FieldCount);

                Console.WriteLine("Voornaam: " + rdr[0]);
                Console.WriteLine("Achternaam: " + rdr[1]);
            }
        }

        static void ToonAantalLeerlingen(MySqlConnection conn)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM Leerlingen";

            var aantal = cmd.ExecuteScalar();

            Console.WriteLine("Het aantal leerlingen in de database: {0}.", aantal);
        }

        static void AddLeerling(MySqlConnection conn, string Voornaam, string Achternaam)
        {
            var cmd = new MySqlCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "INSERT INTO Leerlingen(Voornaam, Achternaam) VALUES( " + Voornaam + "," + Achternaam + ")";
            cmd.CommandText = String.Format("INSERT INTO Leerlingen(Voornaam, Achternaam) VALUES('{0}', '{1}')", Voornaam, Achternaam);

            cmd.ExecuteNonQuery();
        }

        static void AddLeerlingSafe(MySqlConnection conn, string Voornaam, string Achternaam)
        {
            var cmd = new MySqlCommand();
            cmd.Connection = conn;

            cmd.Parameters.Add(new MySqlParameter("vnaam", Voornaam));
            cmd.Parameters.Add(new MySqlParameter("anaam", Achternaam));

            cmd.CommandText = "INSERT INTO Leerlingen(Voornaam, Achternaam) VALUES(@vnaam, @anaam)";

            cmd.ExecuteNonQuery();
        }

        static void AskUserForNewLeerling(MySqlConnection conn)
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
            string connString = @"Server=192.168.56.101;Database=SchoolDb;Uid=imma;Pwd=imma;";

            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            //AddLeerling(conn, "Freddy", "F");

            ToonAantalLeerlingen(conn);
            
            ToonAlleLeerlingen(conn);
            conn.Close();
        }
    }
}
