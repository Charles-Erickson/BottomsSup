using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BottomsSup.Models;

namespace BottomsSup.Controllers
{
    public class TokensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tokens
        public ActionResult Index()
        {
            return View(db.Tokens.ToList());
        }

        // GET: Tokens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tokens tokens = db.Tokens.Find(id);
            if (tokens == null)
            {
                return HttpNotFound();
            }
            return View(tokens);
        }

        // GET: Tokens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TokenId,TokenName,TokenPrice")] Tokens tokens)
        {
            if (ModelState.IsValid)
            {
                tokens.TokenName = "Drink Token";
                tokens.TokenPrice = 500;
                db.Tokens.Add(tokens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tokens);
        }

        // GET: Tokens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tokens tokens = db.Tokens.Find(id);
            if (tokens == null)
            {
                return HttpNotFound();
            }
            return View(tokens);
        }

        // POST: Tokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TokenId,TokenName,TokenPrice")] Tokens tokens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tokens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tokens);
        }

        // GET: Tokens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tokens tokens = db.Tokens.Find(id);
            if (tokens == null)
            {
                return HttpNotFound();
            }
            return View(tokens);
        }

        // POST: Tokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tokens tokens = db.Tokens.Find(id);
            db.Tokens.Remove(tokens);
            db.SaveChanges();
            return RedirectToAction("Index");
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
