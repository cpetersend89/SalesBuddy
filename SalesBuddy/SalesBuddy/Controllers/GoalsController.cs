using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SalesBuddy.Models;


namespace SalesBuddy.Controllers
{
    public class GoalsController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        // GET: Goals
        public ActionResult Index()
        {
            return View(db.Goals.ToList());
        }

        // GET: Goals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // GET: Goals/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GoalId,UserEmail,AutoGoal,AutoActual,AutoPercentage,MortgageGoal,MortgageActual,MortgagePercentage,CreditCardGoal,CreditCardActual,CreditCardPercentage,CheckingGoal,CheckingActual,CheckingPercentage,StartDate,EndDate")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Goals.Add(goal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goal);
        }

        // GET: Goals/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GoalId,UserEmail,AutoGoal,AutoActual,AutoPercentage,MortgageGoal,MortgageActual,MortgagePercentage,CreditCardGoal,CreditCardActual,CreditCardPercentage,CheckingGoal,CheckingActual,CheckingPercentage,StartDate,EndDate")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goal);
        }

        // GET: Goals/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goal goal = db.Goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // POST: Goals/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goal goal = db.Goals.Find(id);
            db.Goals.Remove(goal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public static void GetAutoActual(string userEmail)
        {
            decimal percent = 0;
            var autoLoans = db.Products.Where(x => x.UserEmail == userEmail).ToList();
            decimal total = autoLoans.Sum(autoLoan => autoLoan.AutoLoan);
            var autoTotal = db.Goals.FirstOrDefault(y => y.UserEmail == userEmail);
            decimal autoStartTotal = 0;
            autoTotal.AutoActual = autoStartTotal;
            decimal newAutoTotal = autoTotal.AutoActual + total;
            autoTotal.AutoActual = newAutoTotal;
            autoTotal.AutoPercentage = percent;
            percent = (autoTotal.AutoActual/autoTotal.AutoGoal)*100;
            autoTotal.AutoPercentage = percent;
            db.Set<Goal>().AddOrUpdate(autoTotal);
            db.SaveChanges();
        }

        public static void GetMortgageActual(string userEmail)
        {
            decimal percent = 0;
            var mortgageLoans = db.Products.Where(x => x.UserEmail == userEmail).ToList();
            decimal total = mortgageLoans.Sum(mortgageLoan => mortgageLoan.MortgageLoan);
            var mortgageTotal = db.Goals.FirstOrDefault(y => y.UserEmail == userEmail);
            decimal mortgageStartTotal = 0;
            mortgageTotal.MortgageActual = mortgageStartTotal;
            decimal newMortgageTotal = mortgageTotal.MortgageActual + total;
            mortgageTotal.MortgageActual = newMortgageTotal;
            mortgageTotal.MortgagePercentage = percent;
            percent = (mortgageTotal.MortgageActual / mortgageTotal.MortgageGoal) * 100;
            mortgageTotal.MortgagePercentage = percent;
            db.Set<Goal>().AddOrUpdate(mortgageTotal);
            db.SaveChanges();
        }
        public static void GetCreditCardActual(string userEmail)
        {
            decimal percent = 0;
            var Credit = db.Products.Where(x => x.UserEmail == userEmail).ToList();
            decimal total = Credit.Sum(credit => credit.CreditCard);
            var creditTotal = db.Goals.FirstOrDefault(y => y.UserEmail == userEmail);
            decimal creditStartTotal = 0;
            creditTotal.CreditCardActual = creditStartTotal;
            decimal newCreditTotal = creditTotal.CreditCardActual + total;
            creditTotal.CreditCardActual = newCreditTotal;
            creditTotal.CreditCardPercentage = percent;
            percent = (creditTotal.CreditCardActual / creditTotal.CreditCardGoal) * 100;
            creditTotal.CreditCardPercentage = percent;
            db.Set<Goal>().AddOrUpdate(creditTotal);
            db.SaveChanges();
        }
        public static void GetCheckingActual(string userEmail)
        {
            decimal percent = 0;
            var Checking = db.Products.Where(x => x.UserEmail == userEmail).ToList();
            decimal total = Checking.Sum(checking => checking.CheckingAccount);
            var checkingTotal = db.Goals.FirstOrDefault(y => y.UserEmail == userEmail);
            decimal checkingStartTotal = 0;
            checkingTotal.CheckingActual = checkingStartTotal;
            decimal newCheckingTotal = checkingTotal.CheckingActual + total;
            checkingTotal.CheckingActual = newCheckingTotal;
            checkingTotal.CheckingPercentage = percent;
            percent = (checkingTotal.CheckingActual / checkingTotal.CheckingGoal) * 100;
            checkingTotal.CheckingPercentage = percent;
            db.Set<Goal>().AddOrUpdate(checkingTotal);
            db.SaveChanges();
        }


    }
}
