using System;
using System.Web.Mvc;

namespace RedditBet.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Todo: need to restrict this View 

            return View();
        }
    }
}
