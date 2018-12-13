using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Copy
    {
        private int _id;
        private string _name;
        private string _author;
        private bool _checkedOut;

        public Copy(string name, string author, bool checkedOut=false, int id=0)
        {
            _name=name;
            _author=author;
            _checkedOut=checkedOut;
            _id = id;
        }


        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetAuthor()
        {
            return _author;
        }

        public bool GetCheckedOut()
        {
            return _checkedOut;
        }
    
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO copies (name, author, checked_out) VALUES (@Name, @Author, @CheckedOut);";
            cmd.Parameters.AddWithValue("@Name", this._name);
            cmd.Parameters.AddWithValue("@Author", this._author);
            cmd.Parameters.AddWithValue("@CheckedOut", this._checkedOut);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(bool newCheckoutStatus)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE copies SET checked_out = @newCheckoutStatus WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter checkoutStatus = new MySqlParameter();
            checkoutStatus.ParameterName = "@newCheckoutStatus";
            checkoutStatus.Value = newCheckoutStatus;
            cmd.Parameters.Add(checkoutStatus);
            cmd.ExecuteNonQuery();
            _checkedOut = newCheckoutStatus;
            conn.Close();
            if (conn != null)
                {
                    conn.Dispose();
                }
        }


        public static List<Copy> GetAll()
        {
            List<Copy> allCopies = new List<Copy> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM copies;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int CopyId = rdr.GetInt32(0);
                string CopyName = rdr.GetString(1);
                string CopyAuthor = rdr.GetString(2);
                bool CopyCheckedOut = rdr.GetBoolean(3);
                Copy newCopy = new Copy(CopyName, CopyAuthor, CopyCheckedOut, CopyId);
                allCopies.Add(newCopy);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCopies;
        }

        public static Copy Find(int copyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM copies WHERE id = (@copyId);";
            MySqlParameter copy_id = new MySqlParameter();
            copy_id.ParameterName = "@copyId";
            copy_id.Value = copyId;
            cmd.Parameters.Add(copy_id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string CopyName = "";
            string CopyAuthor="";
            bool CopyCheckedOut = false;
            int CopyId = 0;
            while (rdr.Read())
            {
                CopyId = rdr.GetInt32(0);
                CopyName = rdr.GetString(1);
                CopyAuthor = rdr.GetString(2);
                CopyCheckedOut = rdr.GetBoolean(3);
            }
            Copy foundCopy = new Copy(CopyName, CopyAuthor, CopyCheckedOut, CopyId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            Console.WriteLine(foundCopy.GetName());
            return foundCopy;
        }
    }
}
        // public void AddBook(Book newBook)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"INSERT INTO authors_books (author_id, book_id VALUES (@AuthorId, @BookId);";
        //     MySqlParameter author_id = new MySqlParameter();
        //     author_id.ParameterName = "@AuthorId";
        //     author_id.Value = _id;
        //     cmd.Parameters.Add(author_id);
        //     MySqlParameter book_id = new MySqlParameter();
        //     book_id.ParameterName = "@BookId";
        //     book_id.Value = newBook.GetId();
        //     cmd.Parameters.Add(book_id);
        //     cmd.ExecuteNonQuery();
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }

        // public static List<Book> GetBooks(int id)
        // {
        //     List<Book> authorsBooks = new List<Book>{};
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT books.* FROM authors
        //         JOIN authors_books on (authors.id = authors_books.author_id)
        //         JOIN books ON (authors_books.book_id = books.id)
        //         WHERE authors.id = @AuthorId;";
        //     cmd.Parameters.AddWithValue("@AuthorId", id);
        //     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     while(rdr.Read())
        //     {
        //         int bookId = rdr.GetInt32(0);
        //         string bookName = rdr.GetString(1);
        //         Book newBook = new Book(bookName, bookId);
        //         authorsBooks.Add(newBook);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return authorsBooks;
        // }