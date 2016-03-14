using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tech4mUI.Controllers
{
    public class InterviewQuestionController : Controller
    {
        public Tech4mService.Tech4mServiceClient service { get; set; }
        // GET: Book
        public ActionResult Index()
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<tech4mEntity.InterviewQuestion> lstInterviewQuestion = new List<tech4mEntity.InterviewQuestion>();
            lstInterviewQuestion = service.GetInterviewQuestionList(0, 0, 0).ToList();
            return View();
        }

        public ActionResult InterviewQuestionById(int interviewQuestionId)
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<tech4mEntity.InterviewQuestion> lstInterviewQuestion = new List<tech4mEntity.InterviewQuestion>();
            lstInterviewQuestion = service.GetInterviewQuestionList(interviewQuestionId, 0, 0).ToList();
            Session["InterviewQuestionInfo"] = lstInterviewQuestion;
            return View();
        }

        public ActionResult InterviewQuestionCategoryHome(int interviewQuestionSubCategoryId)
        {
            return View();
        }
        public ActionResult InterviewQuestionList(int interviewQuestionId, int interviewQuestionSubCategoryId, int interviewQuestionCategoryId)
        {
            service = new Tech4mService.Tech4mServiceClient();
            List<tech4mEntity.InterviewQuestion> lstInterviewQuestion = new List<tech4mEntity.InterviewQuestion>();
            lstInterviewQuestion = service.GetInterviewQuestionList(interviewQuestionId, interviewQuestionSubCategoryId, interviewQuestionCategoryId).ToList();
            Session["InterviewQuestionList"] = lstInterviewQuestion;
            return View();
        }
    }
}