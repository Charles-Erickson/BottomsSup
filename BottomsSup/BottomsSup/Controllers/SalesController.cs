using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BottomsSup.Models;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;

namespace BottomsSup.Controllers
{
    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //var;
        // GET: Sales
        public ActionResult Index()
        {
            var salesList = db.Sales.Include(s => s.Bar);
            var BarLoggedIn = User.Identity.GetUserId();
            var barId = db.Bars.Where(d => d.ApplicationUserId == BarLoggedIn).Select(f => f.BarId).FirstOrDefault();
            var sales = db.Sales.Where(k => k.BarId == barId);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.BarId = new SelectList(db.Bars, "BarId", "BarName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesId,DateOfSales,TotalSales,TotalLabor,BarId")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                var userId= User.Identity.GetUserId();
                sales.BarId = db.Bars.Where(b => b.ApplicationUserId == userId).Select(j => j.BarId).FirstOrDefault();
                //sales.TotalLabor= Convert.ToDouble(sales.TotalLabor);
                var dailySales = Convert.ToDouble(sales.TotalSales);
                var dailyLabor = Convert.ToDouble(sales.TotalLabor);
                sales.LaborPercentage = dailyLabor / dailySales;
                db.Sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BarId = new SelectList(db.Bars, "BarId", "BarName", sales.BarId);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.BarId = new SelectList(db.Bars, "BarId", "BarName", sales.BarId);
            return View(sales);
        }
        
        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesId,DateOfSales,TotalSales,TotalLabor,LaborPercentage,BarId")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BarId = new SelectList(db.Bars, "BarId", "BarName", sales.BarId);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    //    [HttpPost, ActionName("Chart")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult CreateChart(int id)
    //    {
    //        //        //    new Chart(width: 800, height: 200)
    //        //        //        .AddTitle("Sales Record")
    //        //        //        .AddSeries(

    //        //        //        xValue:


    //        var data = Database.Open("Sales");
    //    var dbdata = data.Query("SELECT Date,  FROM Sales");
    //    var myChart = new Chart(width: 600, height: 400)
    //       .AddTitle("Sales Record")
    //       .DataBindTable(dataSource: dbdata, xField: "Date")
    //       .Write();
    //}

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string query = "SELECT DISTINCT ShipCountry FROM Orders";
            DataTable dt = GetData(query);
            DB.DataSource = dt;
            ddlCountries.DataTextField = "ShipCountry";
            ddlCountries.DataValueField = "ShipCountry";
            ddlCountries.DataBind();
            ddlCountries.Items.Insert(0, new ListItem("Select Country", ""));
        }
    }

    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    sda.Fill(dt);
                }

                return dt;
            }
        }
    }
}
