using System.Web.Mvc;

namespace progetto_stettimanale_mvc.Controllers
{
    public class StanzeController : Controller
    {
        [Authorize]
        // GET: Stanze
        [HttpGet]
        public ActionResult aggiungistanze()
        {
            return View();
        }

        [HttpPost]
        public ActionResult aggiungistanze(string a)
        {
            return View();
        }
    }
}