using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tech4mUI.Models;

namespace tech4mUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly Tech4mService.Tech4mServiceClient _blogRepository;
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public BlogController(Tech4mService.Tech4mServiceClient blogRepository)
        {
            _blogRepository = blogRepository;
        }

        /// <summary>
        /// Return the list page with latest posts.
        /// </summary>
        /// <param name="p">pagination number</param>
        /// <returns></returns>
        public ViewResult Posts(int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, p);
            ViewBag.Title = "Latest Posts";
            return View("List", viewModel);
        }

        /// <summary>
        /// Return a particular post based on the puslished year, month and url slug.
        /// </summary>
        /// <param name="year">Published year</param>
        /// <param name="month">Published month</param>
        /// <param name="title">Url slug</param>
        /// <returns></returns>
        public ViewResult Post(int year, int month, string title)
        {
            //var post = _blogRepository.Post(year, month, title);
            var post = _blogRepository.PostByMonthYear(year, month, title);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View(post);
        }

        /// <summary>
        /// Return the posts belongs to a category.
        /// </summary>
        /// <param name="category">Url slug</param>
        /// <param name="p">Pagination number</param>
        /// <returns></returns>
        public ViewResult Category(string category, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, category, "Category", p);

            if (viewModel.Category == null)
                throw new HttpException(404, "Category not found");

            //ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.Category.Name);
            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.Category.CategoryName);
            return View("List", viewModel);
        }

        /// <summary>
        /// Return the posts belongs to a tag.
        /// </summary>
        /// <param name="tag">Url slug</param>
        /// <param name="p">Pagination number</param>
        /// <returns></returns>
        public ViewResult Tag(string tag, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);

            if (viewModel.Tag == null)
                throw new HttpException(404, "Tag not found");

            ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.Name);
            return View("List", viewModel);
        }

        /// <summary>
        /// Return the posts that matches the search text.
        /// </summary>
        /// <param name="s">search text</param>
        /// <param name="p">Pagination number</param>
        /// <returns></returns>
        public ViewResult Search(string s, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found for search text ""{0}""", s);

            var viewModel = new ListViewModel(_blogRepository, s, "Search", p);
            return View("List", viewModel);
        }

        /// <summary>
        /// Child action that returns the sidebar partial view.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(_blogRepository);
            return PartialView("_Sidebars", widgetViewModel);
        }
    }
}