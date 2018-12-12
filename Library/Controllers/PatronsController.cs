using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class PatronsController : Controller
    {
        
        //list of all patrons
        [HttpGet("/patrons")]
        public ActionResult Index()
        {
            List<Patron> allPatrons = Patron.GetAll();
            return View(allPatrons);
        }

        //new patron form
        [HttpGet("/patrons/new")]
        public ActionResult New()
        {
            return View();
        }

        //create new patron, does not allow duplicate names
        [HttpPost("/patrons")]
        public ActionResult Create(string patronName)
        {
            bool isSame = false;
            string tempName = "";
            List<Patron> allPatrons = Patron.GetAll();
            foreach(Patron patron in allPatrons)
            {
                tempName = patron.GetName();
                if (tempName == patronName)
                {
                    isSame = true; 
                }
            }
            if (isSame == false)
            {
                Patron newPatron = new Patron(patronName);
                newPatron.Save();
            }
            return RedirectToAction("Index");
        }

        //show an individual patron and all copies related to patron
        [HttpGet ("/patrons/{patronId}")]
        public ActionResult Show(int patronId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Patron selectedPatron = Patron.Find(patronId);
            List<Copy> patronCopies = Patron.GetCopies(patronId);
            List<Copy> allCopies = Copy.GetAll();
            model.Add("patron", selectedPatron);
            model.Add("patronCopies", patronCopies);
            model.Add("allCopies", allCopies);
            return View(model);
        }

        //add copy to patrons_copys join table
        [HttpPost("/patrons/{patronId}/addCopy")]
        public ActionResult AddCopy(int patronId, int copyAdded)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Patron selectedPatron = Patron.Find(patronId);
            Copy copy = Copy.Find(copyAdded);
            selectedPatron.AddCopy(copy);
            List<Copy> patronCopies = Patron.GetCopies(patronId);
            List<Copy> allCopies = Copy.GetAll();
            model.Add("patronCopies", patronCopies);
            model.Add("allCopies", allCopies);
            model.Add("patron", selectedPatron);
            return View("Show", model);
        }

    }
}