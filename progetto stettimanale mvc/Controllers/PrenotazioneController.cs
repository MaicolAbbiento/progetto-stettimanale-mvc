using Antlr.Runtime.Misc;
using progetto_stettimanale_mvc.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace progetto_stettimanale_mvc.Controllers
{
    [Authorize]
    public class PrenotazioneController : Controller
    {
        // GET: Prenotazione
        [HttpGet]
        public ActionResult prenota()
        {
            camera camera = new camera();
            camera.cameralasciata();
            List<string> lista = new List<string>
            {
                "mezza pensione",
                "pensione completa",
                "bed and breckfast"
            };
            ViewBag.o = lista;
            return View();
        }

        [HttpPost]
        public ActionResult prenota(Prenotazione p, string descrizone)
        {
            Prenotazione preno = new Prenotazione();
            p.Dettagli = descrizone;
            preno.addprenotazione(p);
            return RedirectToAction("prenota");
        }

        public ActionResult listaprenotazioni()

        {
            Prenotazione preno = new Prenotazione();
            List<Prenotazione> prenotaziones = preno.checkout();

            prenotaziones.Reverse();
            return View(prenotaziones);
        }

        public ActionResult checkout(int id)
        {
            Servizio servizio = new Servizio();
            List<int> servizzi = servizio.selectservizi(id);
            List<Servizio> servizziPrenotazione = new List<Servizio>();
            decimal tots = 0;
            foreach (var item in servizzi)
            {
                Servizio servizioPrenotazione = servizio.getserviziutente(item);
                servizziPrenotazione.Add(servizioPrenotazione);
                tots += servizioPrenotazione.costo;
            }

            ViewBag.tots = tots.ToString("C2");
            Prenotazione p = new Prenotazione();
            p = p.selectprenotazione(id);
            decimal tot = tots + p.Costotot;
            ViewBag.tottaleass = tot.ToString("C2");

            return View(servizziPrenotazione);
        }

        public ActionResult queryPage()
        {
            return View();
        }

        public JsonResult query1(string codicefiscale1)
        {
            Prenotazione p = new Prenotazione();
            List<Prenotazione> prenotazione = p.selectprenotazioneQuery(codicefiscale1);
            return Json(prenotazione);
        }

        public JsonResult query2()

        {
            Prenotazione p = new Prenotazione();
            List<Prenotazione> prenotazione = p.selectprenotazioneQuery("pensione completa");
            return Json(prenotazione, JsonRequestBehavior.AllowGet);
        }
    }
}