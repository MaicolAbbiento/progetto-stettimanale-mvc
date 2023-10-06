using progetto_stettimanale_mvc.Models;
using System.Web.Mvc;

namespace progetto_stettimanale_mvc.Controllers
{
    public class ClientiController : Controller
    {
        // GET: Clienti
        [HttpGet]
        public ActionResult aggiungiunclente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult aggiungiunclente(Cliente p)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                cliente.addcliente(p);
                ViewBag.Errore = cliente.Errore;
            }
            return View();
        }
    }
}