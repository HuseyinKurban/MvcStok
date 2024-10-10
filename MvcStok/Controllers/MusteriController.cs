using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db=new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler=from d in db.TBLMUSTERILER select d;
            if(!string.IsNullOrEmpty(p))
            {
                //arama işlemi
                //müşteri adını içeren
                degerler = degerler.Where(x => x.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());

           // var degerler=db.TBLMUSTERILER.ToList();
            //return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]

        public ActionResult YeniMusteri(TBLMUSTERILER p1)
        {
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult SIL(int id)
        {
            var musteri=db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", mus);
        }

        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var mus = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            mus.MUSTERIAD = p1.MUSTERIAD;
            mus.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}