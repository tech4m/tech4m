using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tech4mEntity;

namespace tech4mUI.Controllers
{
    public class BookController : Controller
    {
        public Tech4mService.Tech4mServiceClient service { get; set; }
        // GET: Book
        public ActionResult Index()
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<Book> lstBook = new List<Book>();
            lstBook = service.GetBookList(0, 0, 0).ToList();
            return View();
        }

        public ActionResult BookById(int bookId)
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<Book> lstBook = new List<Book>();
            lstBook = service.GetBookList(bookId, 0, 0).ToList();
            Session["BookInfo"] = lstBook;
            return View();
        }

        public ActionResult BookList(int bookId, int bookSubCategoryId, int bookCategoryId)
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<Book> lstBook = new List<Book>();
            lstBook = service.GetBookList(bookId, bookSubCategoryId, bookCategoryId).ToList();
            Session["BookList"] = lstBook;
            return View();
        }
    }
}