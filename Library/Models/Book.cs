using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Book
    {
        private int _id;
        private string _name;

        public Book(string name, int id=0)
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
            cmd.CommandText = @"INSERT INTO books (name) VALUES (@name);";
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

        public static List<Book> GetAll()
        {
            List<Book> allBooks = new List<Book> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM books;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int BookId = rdr.GetInt32(0);
                string BookName = rdr.GetString(1);
                Book newBook = new Book(BookName, BookId);
                allBooks.Add(newBook);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allBooks;
        }


        public static Book GetBookByTitle(string title)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM books WHERE name LIKE '" + title + "%';";
        // MySqlParameter book_title = new MySqlParameter();
        // book_title.ParameterName = "@BookName";
        // book_title.Value = title;
        // cmd.Parameters.Add(book_title);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int id = 0;
        string searchTitle = "";
        while(rdr.Read())
            {
                id = rdr.GetInt32(0);
                searchTitle = rdr.GetString(1);
            }
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        Book book = new Book(searchTitle, id);
        return book;
        }


        public static Book Find(int bookId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM books WHERE id = (@bookId);";
            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@bookId";
            book_id.Value = bookId;
            cmd.Parameters.Add(book_id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string BookName = "";
            int BookId = 0;
            while (rdr.Read())
            {
                BookId = rdr.GetInt32(0);
                BookName = rdr.GetString(1);
            }
            Book foundBook = new Book(BookName, BookId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            Console.WriteLine(foundBook.GetName());
            return foundBook;
        }



        public void AddAuthor(Author newAuthor)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO authors_books (author_id, book_id) VALUES (@AuthorId, @BookId);";
            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@BookId";
            book_id.Value = _id;
            cmd.Parameters.Add(book_id);
            MySqlParameter author_id = new MySqlParameter();
            author_id.ParameterName = "@AuthorId";
            author_id.Value = newAuthor.GetId();
            cmd.Parameters.Add(author_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Author> GetAuthors(int id)
        {
            List<Author> bookAuthors = new List<Author>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT authors.* FROM books
                JOIN authors_books on (books.id = authors_books.book_id)
                JOIN authors ON (authors_books.author_id = authors.id)
                WHERE books.id = @BookId;";
            cmd.Parameters.AddWithValue("@BookId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int authorId = rdr.GetInt32(0);
                string authorName = rdr.GetString(1);
                Author newAuthor = new Author(authorName, authorId);
                bookAuthors.Add(newAuthor);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return bookAuthors;
        }


        //     public List<Book> GetBooks()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT book_id FROM authors_books WHERE author_id = (@BookId);";
        //     MySqlParameter authorIdParameter = new MySqlParameter();
        //     authorIdParameter.ParameterName = "@BookId";
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
