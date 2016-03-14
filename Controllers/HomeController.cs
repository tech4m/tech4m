using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using tech4mUI.Models;
using tech4mEntity;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.ServiceModel.Syndication;
using tech4mUI.Extensions;
namespace tech4mUI.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly Tech4mService.Tech4mServiceClient _blogRepository;

        public HomeController(Tech4mService.Tech4mServiceClient blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        /// <summary>
        /// Return the About me page.
        /// </summary>
        /// <returns></returns>
        public ViewResult Aboutme()
        {
            return View();
        }
        /// <summary>
        /// Return the contact form.
        /// </summary>
        /// <returns></returns>
        public ViewResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Send an email to the blog admin from the POSTed contact form.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var client = new SmtpClient())
                {
                    var adminEmail = ConfigurationManager.AppSettings["AdminEmail"];
                    var from = new MailAddress(adminEmail, "tech4M");
                    var to = new MailAddress(adminEmail, "tech4M Admin");

                    using (var message = new MailMessage(from, to))
                    {
                        message.Body = contact.Body;
                        message.IsBodyHtml = true;
                        message.BodyEncoding = Encoding.UTF8;

                        message.Subject = contact.Subject;
                        message.SubjectEncoding = Encoding.UTF8;

                        message.ReplyTo = new MailAddress(contact.Email);

                        client.Send(message);
                    }
                }

                return View("Thanks");
            }

            return View();
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { });
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        /// <summary>
        /// Generate and return RSS feed.
        /// </summary>
        /// <returns></returns>
        public ActionResult Feed()
        {
            var blogTitle = ConfigurationManager.AppSettings["BlogTitle"];
            var blogDescription = ConfigurationManager.AppSettings["BlogDescription"];
            var blogUrl = ConfigurationManager.AppSettings["BlogUrl"];

            //var posts = _blogRepository.Posts(0, 25).Select
            var posts = _blogRepository.PostWithPaging(0, 25).Select
        (
            p => new SyndicationItem
                (
                    p.Title,
                    p.Description,
                    new Uri(string.Concat(blogUrl, p.Href(Url)))
                )
        );

            var feed = new SyndicationFeed(blogTitle, blogDescription, new Uri(blogUrl), posts)
            {
                Copyright = new TextSyndicationContent(String.Format("Copyright © {0}", blogTitle)),
                Language = "en-US"
            };

            return new FeedResult(new Rss20FeedFormatter(feed));
        }
    }
}