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
        private int _quantity;

        public Copy(string name, string author, int quantity, int id=0)
        {
            _name=name;
            _author=author;
            _quantity=quantity;
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

        public int GetQuantity()
        {
            return _quantity;
        }
    
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO copies (name, author, quantity) VALUES (@Name, @Author, @Quantity);";
            cmd.Parameters.AddWithValue("@Name", this._name);
            cmd.Parameters.AddWithValue("@Author", this._author);
            cmd.Parameters.AddWithValue("@Quantity", this._quantity);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            
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
                int CopyQuantity = rdr.GetInt32(3);
                Copy newCopy = new Copy(CopyName, CopyAuthor, CopyQuantity, CopyId);
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
            int CopyQuantity = 0;
            int CopyId = 0;
            while (rdr.Read())
            {
                CopyId = rdr.GetInt32(0);
                CopyName = rdr.GetString(1);
                CopyAuthor = rdr.GetString(2);
                CopyQuantity = rdr.GetInt32(3);
            }
            Copy foundCopy = new Copy(CopyName, CopyAuthor, CopyQuantity, CopyId);
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