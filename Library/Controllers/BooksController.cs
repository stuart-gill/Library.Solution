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


        // [HttpPost("/books")]
        // public ActionResult Create(string bookName)
        // {
        //     Book newBook = new Book(bookName);
        //     newBook.Save();
            
        //     List<Book> allBooks = Book.GetAll();
        //     return View("Index", allBooks);
        // }

        //create new book, author and title both
        [HttpPost("/books")]
        public ActionResult Create(string bookName, string bookAuthor)
        {

            //check for book title duplicates, add book title:
            bool isSameName = false;
            string tempName = "";
            List<Book> allBooks = Book.GetAll();
            foreach(Book book in allBooks)
            {
                tempName = book.GetName();
                if (tempName == bookName)
                {
                    isSameName = true; 
                }
            }
            if (isSameName == false)
            {
                Book newBook = new Book(bookName);
                newBook.Save();
            
            
            //check for author duplicates, add book author:
            bool isSameAuthor = false;
            string tempAuthor = "";
            List<Author> allAuthors = Author.GetAll();
            foreach(Author author in allAuthors)
            {
                tempAuthor = author.GetName();
                if (tempAuthor == bookAuthor)
                {
                    isSameAuthor = true;
                    newBook.AddAuthor(author); 
                }
            }
            if (isSameAuthor == false)
            {
                Author newAuthor = new Author(bookAuthor);
                newAuthor.Save();
                newBook.AddAuthor(newAuthor);
            }
          
            }
            Copy newCopy = new Copy(bookName, bookAuthor);
            newCopy.Save();


            return RedirectToAction("Index");
        }

        //show indidividual books and copies of this book
        [HttpGet("/books/{bookId}")]
        public ActionResult Show(int bookId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Book selectedBook = Book.Find(bookId);
            List<Author> bookAuthors = Book.GetAuthors(bookId);
            string bookAuthor = bookAuthors[0].GetName();
            string bookName = selectedBook.GetName();
            List<Copy> bookCopies = Copy.GetByAuthorTitle(bookName, bookAuthor);
            List<Author> allAuthors = Author.GetAll();
            List<Copy> availableCopies = new List<Copy> {};
            foreach (Copy copy in bookCopies)
            {
                if (copy.GetCheckoutStatus() == false)
                {
                    availableCopies.Add(copy);
                }
            }
            model.Add("bookCopies", bookCopies);
            model.Add("bookAuthors", bookAuthors);
            model.Add("allAuthors", allAuthors);
            model.Add("book", selectedBook);
            model.Add("availableCopies", availableCopies);
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

          [HttpGet("/books/search/")]
          public ActionResult Search()
          {
              return View("Search");
          }


        [HttpPost("/books/search/")]
        public ActionResult Search(string searchTitle)
        {
         List<Dictionary<string,object>> dictionaryList = new List<Dictionary<string,object>> {};
         

         List<Book> searchedBooks = Book.GetBookByTitle(searchTitle);
         
         foreach(Book book in searchedBooks)
         {
             Dictionary<string, object> model = new Dictionary<string, object>();
             int bookId = book.GetId();
             List<Author> searchedBookAuthors = Book.GetAuthors(bookId);
             model.Add("searchedBookAuthors", searchedBookAuthors);
             model.Add("book", book);
             dictionaryList.Add(model);
         }
        
        // List<Book> allBooks = Book.GetAll();
        // model.Add("allBooks", allBooks);
        
        return View("SearchResult", dictionaryList);
        }
    }
}
