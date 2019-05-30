﻿using BottomsSup.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BottomsSup.Controllers
{
    public class BarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bars
        public ActionResult Index()
        {
            var BarLoggedIn = User.Identity.GetUserId();
            var bars = db.Bars.Where(e => e.ApplicationUserId == BarLoggedIn);
            return View(bars);

        }

        // GET: Bars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = db.Bars.Find(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // GET: Bars/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Bars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarId,BarName,Open,Close,Address,City,State,Zipcode,UserId")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                bar.ApplicationUserId = User.Identity.GetUserId();
                string address = (bar.Address + "+" + bar.City + "+" + bar.State + "+" + bar.Zipcode);
                GeoController geocode = new GeoController();
                geocode.SendRequest(address);
                bar.Lat = geocode.latitude;
                bar.Lng = geocode.longitude;
                bar.PhoneNumber = db.Users.Where(w => w.Id == bar.ApplicationUserId).Select(j => j.PhoneNumber).FirstOrDefault();
                bar.SalesRecord = db.Sales.Where(f => f.BarId == bar.BarId).ToList();
                db.Bars.Add(bar);
                db.SaveChanges();
                return View("Details");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", bar.ApplicationUserId);
            return View(bar);
        }

        // GET: Bars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = db.Bars.Find(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", bar.ApplicationUserId);
            return View(bar);
        }

        // POST: Bars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarId,BarName,Address,City,State,Zipcode,UserId,Open,Close")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", bar.ApplicationUserId);
            return View(bar);
        }

        // GET: Bars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = db.Bars.Find(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bar bar= db.Bars.Find(id);
            db.Bars.Remove(bar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Bars/Details/5
        public ActionResult ClientBarView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = db.Bars.Find(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // GET: Bars
        public ActionResult ClientBarList()
        {
            var bars = db.Bars;
            return View(bars);

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
