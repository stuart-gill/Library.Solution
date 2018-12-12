using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System;

namespace Library.Controllers
{
    public class BooksController : Controller
    {

        [HttpGet("/books")]
        public ActionResult Index()
        {
            List<Book> allBooks = Book.GetAll();
            return View(allBooks);
        }


        [HttpGet("/books/new")]
        public ActionResult New()
        {
            return View();
        }


        [HttpPost("/books")]
        public ActionResult Create(string bookName)
        {
            Book newBook = new Book(bookName);
            newBook.Save();
            
            List<Book> allBooks = Book.GetAll();
            return View("Index", allBooks);
        }

        //show indidividual books 
        [HttpGet("/books/{bookId}")]
        public ActionResult Show(int bookId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Book selectedBook = Book.Find(bookId);
            List<Author> bookAuthors = Book.GetAuthors(bookId);
            List<Author> allAuthors = Author.GetAll();
            model.Add("bookAuthors", bookAuthors);
            model.Add("allAuthors", allAuthors);
            model.Add("book", selectedBook);

            return View("Show", model);
        }


        //add author to books_authors join table
        [HttpPost("/books/{bookId}/addAuthor")]
        public ActionResult AddAuthor(int bookId, int authorAdded)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Book selectedBook = Book.Find(bookId);
            Author author = Author.Find(authorAdded);
            selectedBook.AddAuthor(author);
            List<Author> bookAuthors = Book.GetAuthors(bookId);
            List<Author> allAuthors = Author.GetAll();
            model.Add("bookAuthors", bookAuthors);
            model.Add("allAuthors", allAuthors);
            model.Add("book", selectedBook);
            return View("Show", model);
        }

    }
}
