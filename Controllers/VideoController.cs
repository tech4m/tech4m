using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tech4mUI.Controllers
{
    public class VideoController : Controller
    {
        public Tech4mService.Tech4mServiceClient service { get; set; }
        // GET: Book
        public ActionResult Index()
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<tech4mEntity.Video> lstVideo = new List<tech4mEntity.Video>();
            lstVideo = service.GetVideoList(0, 0, 0).ToList();
            return View();
        }

        public ActionResult VideoById(int videoId)
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<tech4mEntity.Video> lstVideo = new List<tech4mEntity.Video>();
            lstVideo = service.GetVideoList(videoId, 0, 0).ToList();
            Session["VideoInfo"] = lstVideo;
            return View();
        }

        public ActionResult VideoList(int videoId, int videoSubCategoryId, int videoCategoryId)
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<tech4mEntity.Video> lstVideo = new List<tech4mEntity.Video>();
            lstVideo = service.GetVideoList(videoId, videoSubCategoryId, videoCategoryId).ToList();
            Session["VideoList"] = lstVideo;
            return View();
        }
    }
}