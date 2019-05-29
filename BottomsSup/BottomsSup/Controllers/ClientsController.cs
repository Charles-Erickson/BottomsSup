using BottomsSup.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BottomsSup.Controllers
{
    public class ClientsController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();
        HttpClient apiClient = new HttpClient();

        // GET: Clients
        public ActionResult Index()
        {

            //var ClientLoggedIn = User.Identity.GetUserId();
            //var client = db.Clients.Select(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            var client = db.Clients;
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
                    return RedirectToAction("Index");
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

        [HttpPost, ActionName("AddFriend")]
        [ValidateAntiForgeryToken]
        public ActionResult AddFriend(int id)
        {
            Client client;
            if (ModelState.IsValid)
            {
                Client friend = db.Clients.Where(h => h.ClientId == id).FirstOrDefault();
                var ClientLoggedIn = User.Identity.GetUserId();
                client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
                List<Client> friends = new List<Client>();
                friends.AddRange(client.Friends);
                friends.Add(friend);
                client.Friends = friends;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            client = null;
            return View(client);
        }


        [HttpPost, ActionName("SearchFriends")]
        [ValidateAntiForgeryToken]
        public ActionResult SearchFriends([Bind(Include = "FriendName")] int id)
        {
            Client client = db.Clients.Find(id);
            
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                var matchs = db.Clients.Where(d => d.FirstName == client.FirstName || d.LastName == client.LastName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }


        public ActionResult Stripe()
        {
            var stripePublishKey = ConfigurationManager.AppSettings[APIKeys.StripeApi];
            ViewBag.StripePublishKey = stripePublishKey;
            return View();
        }

        public ActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = 500,//charge in cents
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });
            
            // further application specific code goes here

            return View();
        }

        public ActionResult BarFlyList()
        {

            //var ClientLoggedIn = User.Identity.GetUserId();
            //var client = db.Clients.Select(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            var client = db.Clients;
            return View(client);

        }



        [HttpPost, ActionName("SendToken")]
        [ValidateAntiForgeryToken]
        public ActionResult SendToken(int id, Client client)
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            var friend = db.Clients.Where(e => e.ClientId == id).FirstOrDefault();
            if (client.Tokens.Count() > 0)
            {
                List<Tokens> tokenList = new List<Tokens>();
                List<Tokens> friendList = new List<Tokens>();
                tokenList.AddRange(client.Tokens);
                tokenList.RemoveAt(0);
                var token = db.Tokens.Create();
                friendList.Add(token);
                client.Tokens = tokenList;
                friend.Tokens = friendList;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }


        [HttpPost, ActionName("SpendToken")]
        [ValidateAntiForgeryToken]
        public ActionResult SpendToken(int id, Client client)
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            var bar = db.Bars.Where(e => e.BarId == id).FirstOrDefault();
            if (client.Tokens.Count() > 0)
            {
                List<Tokens> tokenList = new List<Tokens>();
                List<Tokens> BarList = new List<Tokens>();
                tokenList.AddRange(client.Tokens);
                tokenList.RemoveAt(0);
                var token = db.Tokens.Create();
                BarList.Add(token);
                client.Tokens = tokenList;
                bar.Tokens = BarList;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpPost, ActionName("ViewBars")]
        public ActionResult ViewBars ()
        {
            //BarsController bar = new BarsController();
            return RedirectToAction("ClientBarList");
           
        }




        //[HttpPost, ActionName("BuyTokens")]
        //[ValidateAntiForgeryToken]
        //public ActionResult BuyTokens(int id)
        //{
        //   Client user= db.Clients.Find(id);
        //    apiClient.BaseAddress


        //}

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

