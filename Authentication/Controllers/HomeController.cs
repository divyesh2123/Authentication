using Authentication.filter;
using System.Web.Mvc;

namespace Authentication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Editor")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}