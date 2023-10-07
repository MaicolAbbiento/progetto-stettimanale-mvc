using progetto_stettimanale_mvc.Models;
using System.Web.Mvc;

namespace progetto_stettimanale_mvc.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult login()
        {
            camera camera = new camera();
            camera.cameralasciata();
            return View();
        }

        [HttpPost]
        public ActionResult login(Dipendente d)
        {
            Dipendente dipendente = new Dipendente();
            if (ModelState.IsValid)
            {
                dipendente.getutente(d);
            }

            return RedirectToAction("prenota", "Prenotazione");
        }
    }
}