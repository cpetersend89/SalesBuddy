using System;
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
using SalesBuddy.Models.Salesforce1;
using SalesBuddy.Salesforce1;
using Salesforce.Common.Models;

namespace SalesBuddy.Controllers
{
    public class ProductsController : Controller
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ProductsController()
        {
        }

        public ProductsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public const string ProductsPostBinding = "ProductId,ProductType,ProductCode,AutoLoan,MortgageLoan,CreditCard,CheckingAccount,UserEmail";
        // GET: Products
        public async Task<ActionResult> Index()
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var adminSalesList = db.Products.ToList();
            var userSalesList = db.Products.Where(x => x.UserEmail == user.UserName).ToList();
            if(User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    return View(adminSalesList);
                }
            }
            return View(userSalesList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductType,CreatedDate,AutoLoan,MortgageLoan,CreditCard,CheckingAccount,UserEmail")]Product product)
        {
            

            if (ModelState.IsValid)
            {
                //var _product = new Product
                //{
                //    ProductType = product.ProductType,
                //    AutoLoan = product.AutoLoan,
                //    MortgageLoan = product.MortgageLoan,
                //    CreditCard = product.CreditCard,
                //    CheckingAccount = product.CheckingAccount,
                //    UserEmail = product.UserEmail
                //};
                db.Set<Product>().AddOrUpdate(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());


            if (ModelState.IsValid)
            {
                var _product = new Product
                {
                    ProductId = product.ProductId,
                    ProductType = product.ProductType,
                    CreatedDate = product.CreatedDate,
                    ProductCode = product.ProductCode,
                    AutoLoan = product.AutoLoan,
                    MortgageLoan = product.MortgageLoan,
                    CreditCard = product.CreditCard,
                    CheckingAccount = product.CheckingAccount,
                    UserEmail = product.UserEmail
                };
                db.Products.Add(_product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult _AutoLoan()
        {
            return PartialView();
        }

        public ActionResult _MortgageLoan()
        {
            return PartialView();
        }

        public ActionResult _CreditCard()
        {
            return PartialView();
        }

        public ActionResult _CheckingAccount()
        {
            return PartialView();
        }


    }
}