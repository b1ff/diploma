using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Controllers
{
    [ActionLinkArea("")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Gamify Your Project!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
