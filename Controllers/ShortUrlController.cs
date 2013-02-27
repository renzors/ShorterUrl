using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamMinEdu.Models;

namespace ExamMinEdu.Controllers
{
    /*Llamamos "U" al controlador con el fin de obtener una ruta corta*/
    public class ShortUrlController : Controller
    {
        private LocalContext db = new LocalContext();

        public ActionResult Index()
        {            
            return View(db.direcciones.ToList().OrderByDescending(o => o.FCreacion));
        }

        public ActionResult DireccionExistente()
        {
            return View();
        }

        public ActionResult ShortUrl(string shortname)
        {
            Url url = null;
            try
            {
                url = db.direcciones.Where(o => o.UrlDestino == shortname).Single();
            }
            catch
            {
                return Redirect("Index");
            }            
            if (ModelState.IsValid)
            {
                db.Entry(url).State = EntityState.Modified;
                url.NVisitas += 1;
                db.SaveChanges();
            };
            ViewBag.Link = url.UrlOrigen;            
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Url url)
        {
            url.UrlDestino = url.UrlDestino.Trim().ToLower();
            url.UrlOrigen = url.UrlOrigen.Trim();
            url.FCreacion = DateTime.Now;
            url.NVisitas = 0;
            if (ModelState.IsValid)
            {                
                db.direcciones.Add(url);
                try
                {
                    db.SaveChanges();
                }
                catch {
                    return RedirectToAction("DireccionExistente");
                }
                return RedirectToAction("Index");
            }
            return View(url);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}