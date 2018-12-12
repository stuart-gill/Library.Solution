using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        
        //list of all authors
        [HttpGet("/authors")]
        public ActionResult Index()
        {
            List<Author> allAuthors = Author.GetAll();
            return View(allAuthors);
        }

        //new author form
        [HttpGet("/authors/new")]
        public ActionResult New()
        {
            return View();
        }

        //create new author
        [HttpPost("/authors")]
        public ActionResult Create(string authorName)
        {
            Author newAuthor = new Author(authorName);
            newAuthor.Save();
            return RedirectToAction("Index");
        }

        //show an individual author and all books related to author
        [HttpGet ("/authors/{authorId}")]
        public ActionResult Show(int authorId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Author selectedAuthor = Author.Find(authorId);
            List<Book> authorBooks = Author.GetBooks(authorId);
            List<Book> allBooks = Book.GetAll();
            model.Add("author", selectedAuthor);
            model.Add("authorBooks", authorBooks);
            model.Add("allBooks", allBooks);
            return View(model);
        }

        //add book to authors_books join table
        [HttpPost("/authors/{authorId}/addBook")]
        public ActionResult AddBook(int authorId, int bookAdded)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Author selectedAuthor = Author.Find(authorId);
            Book book = Book.Find(bookAdded);
            selectedAuthor.AddBook(book);
            List<Book> authorBooks = Author.GetBooks(authorId);
            List<Book> allBooks = Book.GetAll();
            model.Add("authorBooks", authorBooks);
            model.Add("allBooks", allBooks);
            model.Add("author", selectedAuthor);
            return View("Show", model);
        }

    }
}