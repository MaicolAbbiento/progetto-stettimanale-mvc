using progetto_stettimanale_mvc.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace progetto_stettimanale_mvc.Controllers
{
    public class ServiziController : Controller
    {
        // GET: Servizi
        [HttpGet]
        public ActionResult aggiungiservizioaospite()
        {
            Servizio servizio = new Servizio();
            List<string> lista = servizio.getservizio();
            ViewBag.o = lista;
            return View();
        }

        [HttpPost]
        public ActionResult aggiungiservizioaospite(Servizio c, string valore)
        {
            Servizio servizio = new Servizio();
            c.descrizione = valore;
            c.idtiposervizio = c.getidservizio(valore);
            servizio.addservizio(c);

            return RedirectToAction("aggiungiservizioaospite");
        }
    }
}