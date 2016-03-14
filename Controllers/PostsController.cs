using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using tech4mEntity;

namespace tech4mUI.Controllers
{
    public class PostsController : Controller
    {
        //Todo dont just return true;
        public bool IsAdmin
        {
            get
            {
                return true;/* Session["IsAdmin"] != null && (bool)Session["IsAdmin"];*/
            }
        }
        public Tech4mService.Tech4mServiceClient service { get; set; }
        // GET: Posts
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Update(int? id, string title, string body, DateTime dateTime, string tags)
        {
            service = new Tech4mService.Tech4mServiceClient();
            Post post = new Post();
            Post postFetch = GetPost(id);
            if (!IsAdmin)
            {
                return RedirectToAction("Index");
            }

            if (postFetch == null)
            {
                post.Title = title;
                //post.Body = body;
                //post.DateTime = dateTime.ToString();
            }
            else
            {
                post.Title = postFetch.Title;
                //post.Body = postFetch.Body;
                //post.DateTime = postFetch.DateTime;
                post.Tags = postFetch.Tags;
            }
            tags = tags ?? string.Empty;
            string[] tagNames = tags.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string tagName in tagNames)
            {

            }

            if (!id.HasValue)
            {

            }
            return RedirectToAction("Details", new { id = post.ID });
        }

        public ActionResult Edit(int? id)
        {
            service = new Tech4mService.Tech4mServiceClient();
            Post post = GetPost(id);
            //Tag[] tagArray = service.GetPostTag(id);
            //if (service.GetPostTag(id) != null)
            //{
            //    List<Tag> postTag = tagArray.ToList();
            //    StringBuilder tagList = new StringBuilder();
            //    foreach (Tag tag in postTag)
            //    {
            //        tagList.AppendFormat("{0}", tag.Name);
            //    }
            //    ViewBag.Tags = tagList.ToString();
            //}
            //else
            //{
            //    post = new Post();
            //    //post.DateTime = DateTime.UtcNow.ToString();
            //    //post.Body = "";
            //    //post.ID = -1;
            //    //post.Tags = "";
            //    //post.Title = "";
            //}
            return View(post);
        }

        private Tag GetTag(string tagName)
        {
            service = new Tech4mService.Tech4mServiceClient();
            Tag tag = new Tag();
            //tag = service.GetTags(tagName);
            return tag;
        }

        private Post GetPost(int? id)
        {
            service = new Tech4mService.Tech4mServiceClient();
            Post post = new Post();
            //post = service.GetPost(id);
            return id.HasValue ? post : new Post() { ID = -1 };
        }
    }
}