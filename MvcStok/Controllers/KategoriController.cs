using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db=new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler=db.TBLKATEGORILER.ToList();
            //1. parametre kacıncı sayfa
            //2. parametre kaç adet (her sayfada kaç adet olsun)
            var degerler=db.TBLKATEGORILER.ToList().ToPagedList(sayfa,2);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if(!ModelState.IsValid)//model doğrulama kurallarına uymayan alanlar varsa 
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        
        public ActionResult SIL(int id)
        {
            //idyi arayıp kategori değişkenine ata
            var kategori=db.TBLKATEGORILER.Find(id);
            //tablodan kategori değişkenine atanan idyi sil
            db.TBLKATEGORILER.Remove(kategori);
            //değişiklikleri kaydet
            db.SaveChanges();
            //index sayfasına yönlendir
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir",ktgr);
        }

        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}