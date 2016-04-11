using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesBuddy.Models;

namespace SalesBuddy.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Column()
        {
            return View();
        }

        public JsonResult GetSalesData()
        {
            List<Goal> goals = new List<Goal>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                goals = db.Goals.ToList();
            }

            var chartData = new object[goals.Count + 1];
            chartData[0] = new object[]
            {
                "Auto Goal",
                "Auto Actual",
                "Mortgage Goal",
                "Mortgage Actual",
                //"Credit Card Goal",
                //"Credit Card Actual",
                //"Checking Goal",
                //"Checking Actual"
            };
            int j = 0;
            foreach (var goal in goals)
            {
                j++;
                chartData[j] = new object[]
                {
                    goal.AutoGoal, goal.AutoActual, goal.MortgageGoal, goal.MortgageActual
                };
            }
            return new JsonResult {Data = chartData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}