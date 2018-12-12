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
    }
}

        // public static List<Author> GetAll()
        // {
        //     List<Author> allAuthors = new List<Author> { };
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT * FROM authors;";
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;

        //     while (rdr.Read())
        //     {
        //         int AuthorId = rdr.GetInt32(0);
        //         string AuthorName = rdr.GetString(1);
        //         Author newAuthor = new Author(AuthorName, AuthorId);
        //         allAuthors.Add(newAuthor);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return allAuthors;
        // }

        // public static Author Find(int authorId)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT * FROM authors WHERE id = (@authorId);";
        //     MySqlParameter author_id = new MySqlParameter();
        //     author_id.ParameterName = "@authorId";
        //     author_id.Value = authorId;
        //     cmd.Parameters.Add(author_id);
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     string AuthorName = "";
        //     int AuthorId = 0;
        //     while (rdr.Read())
        //     {
        //         AuthorId = rdr.GetInt32(0);
        //         AuthorName = rdr.GetString(1);
        //     }
        //     Author foundAuthor = new Author(AuthorName, AuthorId);
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     Console.WriteLine(foundAuthor.GetName());
        //     return foundAuthor;
        // }

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