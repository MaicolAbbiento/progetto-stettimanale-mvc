using progetto_stettimanale_mvc.Models;
using System.Web.Mvc;

namespace progetto_stettimanale_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult login()
        {
            camera camera = new camera();
            camera.cameralasciata();
            return View();
        }
    }
}