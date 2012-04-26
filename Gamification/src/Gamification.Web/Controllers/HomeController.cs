using System.Web.Mvc;

namespace Gamification.Web.Controllers
{
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
