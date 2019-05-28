using BottomsSup.Models;
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
    public class ClientsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        public ActionResult Index()
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            var client = db.Clients.Select(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            return View(client);

        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

            // POST: Clients/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "ClientId,FirstName,LastName,DateOfBirth,UserId")] Client client)
            {
                if (ModelState.IsValid)
                {
                    client.ApplicationUserId = User.Identity.GetUserId();
                    //client.Age = GetAge(client.DateOfBirth);
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", client.ApplicationUserId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", client.ApplicationUserId);
            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,LastName,DateOfBirth,UserId")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", client.ApplicationUserId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public int GetAge(Client client)
        //{
        //    var today = DateTime.Today;
        //    var dob = client.DateOfBirth;
        //    var a = (today.Year * 100 + today.Month) * 100 + today.Day;
        //    var b = (DateOfBirth.Year * 100 + DateOfBirth.Month) * 100 + dateOfBirth.Day;

        //    return (a - b) / 10000;
        //}

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

