using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Author
    {
        private int _id;
        private string _name;

        public Author(string name, int id=0)
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
            cmd.CommandText = @"INSERT INTO authors (name) VALUES (@name);";
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

        public static List<Author> GetAll()
        {
            List<Author> allAuthors = new List<Author> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int AuthorId = rdr.GetInt32(0);
                string AuthorName = rdr.GetString(1);
                Author newAuthor = new Author(AuthorName, AuthorId);
                allAuthors.Add(newAuthor);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAuthors;
        }

        public static List<Author> GetAuthorsByName(string authorSearch)
        {
            List<Author> foundAuthors = new List<Author>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors WHERE name LIKE '%" + authorSearch + "%';";
           
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string searchName = "";
            while(rdr.Read())
                {
                    id = rdr.GetInt32(0);
                    searchName = rdr.GetString(1);
                    Author tempAuthor = new Author(searchName, id);
                    foundAuthors.Add(tempAuthor);
                }
            conn.Close();
            if (conn != null)
                {
                    conn.Dispose();
                }
            return foundAuthors;
        }

        public static Author Find(int authorId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors WHERE id = (@authorId);";
            MySqlParameter author_id = new MySqlParameter();
            author_id.ParameterName = "@authorId";
            author_id.Value = authorId;
            cmd.Parameters.Add(author_id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string AuthorName = "";
            int AuthorId = 0;
            while (rdr.Read())
            {
                AuthorId = rdr.GetInt32(0);
                AuthorName = rdr.GetString(1);
            }
            Author foundAuthor = new Author(AuthorName, AuthorId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            Console.WriteLine(foundAuthor.GetName());
            return foundAuthor;
        }

        public void AddBook(Book newBook)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO authors_books (author_id, book_id) VALUES (@AuthorId, @BookId);";
            MySqlParameter author_id = new MySqlParameter();
            author_id.ParameterName = "@AuthorId";
            author_id.Value = _id;
            cmd.Parameters.Add(author_id);
            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@BookId";
            book_id.Value = newBook.GetId();
            cmd.Parameters.Add(book_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Book> GetBooks(int id)
        {
            List<Book> authorsBooks = new List<Book>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT books.* FROM authors
                JOIN authors_books on (authors.id = authors_books.author_id)
                JOIN books ON (authors_books.book_id = books.id)
                WHERE authors.id = @AuthorId;";
            cmd.Parameters.AddWithValue("@AuthorId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int bookId = rdr.GetInt32(0);
                string bookName = rdr.GetString(1);
                Book newBook = new Book(bookName, bookId);
                authorsBooks.Add(newBook);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return authorsBooks;
        }


        //     public List<Book> GetBooks()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT book_id FROM authors_books WHERE author_id = (@AuthorId);";
        //     MySqlParameter authorIdParameter = new MySqlParameter();
        //     authorIdParameter.ParameterName = "@AuthorId";
        //     authorIdParameter.Value = _id;
        //     cmd.Parameters.Add(authorIdParameter);
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     List<int> BookIds = new List<int> { };
        //     while (rdr.Read())
        //     {
        //         string BookId = rdr.GetInt32(1);
        //         BookIds.Add(BoodId);
        //     }
        //     rdr.Dispose();
        //     List<Book> books = new List<Book> { };
        //     foreach (int BookId in BookIds)
        //     {

        //         var itemQuery = conn.CreateCommand() as MySqlCommand;
        //         itemQuery.CommandText = @"SELECT * FROM books WHERE id = @BookId;";
        //         MySqlParameter bookIdParameter = new MySqlParameter();
        //         bookIdParameter.ParameterName = "@BookId";
        //         bookIdParameter.Value = bookId;
        //         itemQuery.Parameters.Add(bookIdParameter);
        //         var itemQueryRdr = itemQuery.ExecuteReader() as MySqlDataReader;
        //         while (itemQueryRdr.Read())
        //         {
        //             int thisBookId = itemQueryRdr.GetInt32(0);
        //             string thisBookName = itemQueryRdr.GetString(1);
        //             Book foundBook = new Book(thisBookId, thisBookName);
        //             books.Add(foundBook);
        //         }
        //         itemQueryRdr.Dispose();
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return books;
        // }
    }    
}
