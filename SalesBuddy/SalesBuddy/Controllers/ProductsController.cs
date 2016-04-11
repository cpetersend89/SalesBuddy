using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SalesBuddy.Models;

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
            var adminSalesList = db.Products.OrderByDescending(x => x.CreatedDate).ToList();
            var userSalesList = db.Products.Where(x => x.UserEmail == user.UserName).OrderByDescending(x => x.CreatedDate).ToList();
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            if (product.AutoLoan > 0)
            {
                GoalsController.GetAutoActual(product.UserEmail);
            }
            if (product.MortgageLoan > 0)
            {
                GoalsController.GetMortgageActual(product.UserEmail);
            }
            if (product.CreditCard > 0)
            {
                GoalsController.GetCreditCardActual(product.UserEmail);
            }
            if (product.CheckingAccount > 0)
            {
                GoalsController.GetCheckingActual(product.UserEmail);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
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
                if (_product.AutoLoan > 0)
                {
                    GoalsController.GetAutoActual(_product.UserEmail);
                }
                if (_product.MortgageLoan > 0)
                {
                    GoalsController.GetMortgageActual(_product.UserEmail);
                }
                if (_product.CreditCard > 0)
                {
                    GoalsController.GetCreditCardActual(_product.UserEmail);
                }
                if (_product.CheckingAccount > 0)
                {
                    GoalsController.GetCheckingActual(_product.UserEmail);
                }
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