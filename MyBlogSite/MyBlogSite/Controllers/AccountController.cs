using System.Web.Mvc;

namespace MyBlogSite.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public ActionResult Login()
        {
            return View();
        }
    }
}