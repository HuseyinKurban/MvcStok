﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities db=new MvcDbStokEntities();
        // GET: Urun
        public ActionResult Index()
        {
            var degerler=db.TBLURUNLER.ToList();
            return View(degerler);
        }
        
        [HttpGet]
        public ActionResult UrunEkle()
        {
            //KATEGORİLERİ ÇEKME
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr=degerler;
            return View();
        }
       
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            //KATEGORİYİ EKLEME
            var ktg =db.TBLKATEGORILER.Where(x=>x.KATEGORIID==p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            //KATEGORİLERİ ÇEKME
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            

            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun=db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
            urun.FIYAT = p1.FIYAT;
            urun.STOK = p1.STOK;
            // urun.URUNKATEGORI = p1.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(x => x.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}