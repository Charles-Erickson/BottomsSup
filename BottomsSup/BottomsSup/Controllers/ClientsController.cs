﻿using BottomsSup.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using Stripe.Checkout;
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


        public ActionResult ClientProfile()
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            Client client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
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
                    return RedirectToAction("ClientProfile");
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

//       public StripeConfiguration.SetApiKey(APIKeys.StirpeAPI);

//   public var options = new ChargeCreateOptions
//{
//    Amount = 500,
//    Currency = "usd",
//    Description = "",
//    SourceId = "tok_visa" // obtained with Stripe.js,
//};
//        var service = new ChargeService();
//        Charge charge = service.Create(options);


        //public ActionResult AddFriend(int id)
        //{
        //    Client client;
        //    if (ModelState.IsValid)
        //    {
        //        Client friend = db.Clients.Where(h => h.ClientId == id).FirstOrDefault();
        //        var ClientLoggedIn = User.Identity.GetUserId();
        //        client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
        //        List<Client> friends = new List<Client>();
        //        if (client.Friends == null)
        //        {
        //            friends.Add(friend);
        //            client.Friends = friends;
        //            db.SaveChanges();
        //            return RedirectToAction("ClientProfile");
        //        }
        //        else
        //        {
        //            friends.AddRange(client.Friends);
        //            friends.Add(friend);
        //            client.Friends = friends;
        //            db.SaveChanges();
        //            return RedirectToAction("ClientProfile");
        //        }
        //    }
        //    client = null;
        //    return View(client);
        //}
        //[HttpPost]
        //public ActionResult friendSearch(string name)
        //{
        //    var clients = db.Clients;
        //    if (ModelState.IsValid)
        //    {
           
        //        var matchs = db.Clients.Where(d => d.FirstName == name || d.LastName == name);
        //        return View(matchs);
        //    }
        //    return View(clients);
        //}


        //public ActionResult SearchFriends(string name)
        //{
        //    Client client = db.Clients.Find(id);
        //    db.Entry(client).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("friendSearch");
        //}



        public ActionResult addToken()
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            var client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();

            client.Tokens = client.Tokens +1;
            //List<Tokens> tokens = new List<Tokens>();
            //var token = db.Tokens.Create();
            //if (client.Tokens==null)
            //{
            //    tokens.Add(token);
            //    client.Tokens = tokens;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    tokens.AddRange(client.Tokens);
            //    tokens.Add(token);
            //    client.Tokens = tokens;
            db.SaveChanges();
            return RedirectToAction("ClientProfile");

        }


        //var stripePublishKey = ConfigurationManager.AppSettings[APIKeys.StripeApi];
        //ViewBag.StripePublishKey = stripePublishKey;

        public async System.Threading.Tasks.Task<ActionResult> StripeAsync()
        {

            using (HttpClient http = new HttpClient())
            {
                string apiUrl = "http://localhost:49839/";
                http.BaseAddress = new Uri(apiUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await http.GetAsync(apiUrl);
                string data = await response.Content.ReadAsStringAsync();
                //var jsonResults = JsonConvert.DeserializeObject<JObject>(data);
                //var jsonResults = JsonConvert.DeserializeObject<JObject>(data);
                //var results = jsonResults["results"][0];
                //var apiToken = results["tokens"]["get"];
                return RedirectToAction("addToken");
            }
        }

        //public ActionResult Charge(string stripeEmail, string stripeToken)
        //{
        //    var customers = new StripeCustomerService();
        //    var charges = new StripeChargeService();
        //    StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings[APIKeys.StripeApi]);


        //    var customer = customers.Create(new StripeCustomerCreateOptions
        //    {
        //        Email = stripeEmail,
        //        SourceToken = stripeToken
        //    });

        //    var charge = charges.Create(new StripeChargeCreateOptions
        //    {
        //        Amount = 500,//charge in cents
        //        Description = "Sample Charge",
        //        Currency = "usd",
        //        CustomerId = customer.Id
        //    });
            
            // further application specific code goes here

        //    return View();
        //}

        public ActionResult FriendList()
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            var client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            //IEnumerable<Friends> friends=db.Friends.
            FriendsController friends = new FriendsController();
            friends.FriendList(client.ClientId);
            return View(friends);
        }

  
        public ActionResult ViewBarFlys(string searchString)
        {
            var clients = from s in db.Clients
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            //var clients = db.Clients;
            return View(clients);
        }


        public ActionResult SendToken(Client client, int id)
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            var friend = db.Clients.Where(e => e.ClientId == id).FirstOrDefault();
            if (client.Tokens > 0)
            {
                client.Tokens = client.Tokens - 1;
                friend.Tokens = friend.Tokens + 1;
                //List<Tokens> tokenList = new List<Tokens>();
                //List<Tokens> friendList = new List<Tokens>();
                //tokenList.AddRange(client.Tokens);
                //tokenList.RemoveAt(0);
                //var token = db.Tokens.Create();
                //friendList.Add(token);
                //client.Tokens = tokenList;
                //friend.Tokens = friendList;
                db.SaveChanges();
                return RedirectToAction("ClientProfile");
            }
            return View(client);
        }


        public ActionResult Checkout(int? id)
        {
            StripeConfiguration.SetApiKey(APIKeys.StripeApi);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
        "card",
    },
                LineItems = new List<SessionLineItemOptions> {
        new SessionLineItemOptions {
            Name = "Drink Token",
            Description = "Token for Bottoms Sup",
            Amount = 500,
            Currency = "usd",
            Quantity = 1,
        },
    },
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return RedirectToAction("PickUpTab");
        }



        public ActionResult PickUpTab(int? id)
        {
            StripeConfiguration.SetApiKey(APIKeys.StripeApi);

            // Token is created using Checkout or Elements!
            // Get the payment token submitted by the form:
            //var coin = model.Token; // Using ASP.NET MVC

            //var options = new ChargeCreateOptions
            //{
            //    Amount = 500,
            //    Currency = "usd",
            //    Description = " charge",
            //    SourceId = coin,
            //};
            //var service = new ChargeService();
            //Charge charge = service.Create(options);
            return RedirectToAction("StripeAsync");
        }




        public ActionResult SpendToken(int id, Client client)
        {
            
            var ClientLoggedIn = User.Identity.GetUserId();
            client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).FirstOrDefault();
            var bar = db.Bars.Where(e => e.BarId == id).FirstOrDefault();
            if (client.Tokens> 0)
            {
                client.Tokens = client.Tokens - 1;
                bar.Tokens = bar.Tokens + 1;
                //List<Tokens> tokenList = new List<Tokens>();
                //List<Tokens> BarList = new List<Tokens>();
                //tokenList.AddRange(client.Tokens);
                //tokenList.RemoveAt(0);
                //var token = db.Tokens.Create();
                //BarList.Add(token);
                //client.Tokens = tokenList;
                //bar.Tokens = BarList;
                db.SaveChanges();
                return RedirectToAction("ClientProfile");
            }
            return View(client);
        }

        //[HttpPost, ActionName("ViewBars")]
        //public ActionResult ViewBars ()
        //{
        //    //BarsController bar = new BarsController();
        //    return RedirectToAction("ClientBarList");
           
        //}




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

