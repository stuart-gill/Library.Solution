using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Patron
    {
        private int _id;
        private string _name;

        public Patron(string name, int id=0)
        {
            _id = id;
            _name = name;
        }


        public int GetId()
        {
            return _id;
        }
    
        public string GetName()
        {
            return _name;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO patrons (name) VALUES (@name);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Patron> GetAll()
        {
            List<Patron> allPatrons = new List<Patron> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int PatronId = rdr.GetInt32(0);
                string PatronName = rdr.GetString(1);
                Patron newPatron = new Patron(PatronName, PatronId);
                allPatrons.Add(newPatron);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allPatrons;
        }

        public static Patron Find(int patronId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons WHERE id = (@patronId);";
            MySqlParameter patron_id = new MySqlParameter();
            patron_id.ParameterName = "@patronId";
            patron_id.Value = patronId;
            cmd.Parameters.Add(patron_id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string PatronName = "";
            int PatronId = 0;
            while (rdr.Read())
            {
                PatronId = rdr.GetInt32(0);
                PatronName = rdr.GetString(1);
            }
            Patron foundPatron = new Patron(PatronName, PatronId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            Console.WriteLine(foundPatron.GetName());
            return foundPatron;
        }

        public void AddCopy(Copy newCopy)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO patrons_copies (patron_id, copy_id) VALUES (@PatronId, @CopyId);";
            MySqlParameter patron_id = new MySqlParameter();
            patron_id.ParameterName = "@PatronId";
            patron_id.Value = _id;
            cmd.Parameters.Add(patron_id);
            MySqlParameter copy_id = new MySqlParameter();
            copy_id.ParameterName = "@CopyId";
            copy_id.Value = newCopy.GetId();
            cmd.Parameters.Add(copy_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Copy> GetCopies(int id)
        {
            List<Copy> patronsCopies = new List<Copy>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT copies.* FROM patrons
                JOIN patrons_copies on (patrons.id = patrons_copies.patron_id)
                JOIN copies ON (patrons_copies.copy_id = copies.id)
                WHERE patrons.id = @PatronId;";
            cmd.Parameters.AddWithValue("@PatronId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string copyName = rdr.GetString(1);
                string copyAuthor = rdr.GetString(2);
                bool copyCheckoutStatus = rdr.GetBoolean(3);
                int copyId = rdr.GetInt32(0);
                Copy newCopy = new Copy(copyName,copyAuthor,copyCheckoutStatus,copyId);
                patronsCopies.Add(newCopy);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return patronsCopies;
        }
    }
}