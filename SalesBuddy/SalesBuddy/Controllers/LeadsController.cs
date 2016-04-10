using System;
using System.Collections.Generic;
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
    public class LeadsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public LeadsController()
        {
        }

        public LeadsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        public const string LeadsPostBinding = "Id,Salutation,FirstName,LastName,Company,Street,City,State,PostalCode,Phone,MobilePhone,Fax,Email,Status,CreatedDate,Description,UserEmail__c";
        // GET: Leads
        public async Task<ActionResult> Index()
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            IEnumerable<LeadViewModel> selectedLeads = Enumerable.Empty<LeadViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    try
                    {
                        selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                            async (client) =>
                            {
                                QueryResult<LeadViewModel> leads =
                                    await
                                        client.QueryAsync<LeadViewModel>(
                                            "SELECT Id, LastName, Salutation, FirstName, Phone, MobilePhone, Email, UserEmail__c From Lead");
                                return leads.Records;
                            }
                            );
                    }
                    catch (Exception e)
                    {
                        this.ViewBag.OperationName = "query Salesforce Leads";
                        this.ViewBag.AuthorizationUrl =
                            SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                        this.ViewBag.ErrorMessage = e.Message;
                    }
                }
                else
                {
                    try
                    {
                        selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                            async (client) =>
                            {
                                QueryResult<LeadViewModel> leads =
                                    await client.QueryAsync<LeadViewModel>("SELECT Id, LastName, Salutation, FirstName, Phone, MobilePhone, Email, UserEmail__c From Lead Where UserEmail__c = '" + user.UserName + "'");
                                return leads.Records;
                            }
                            );
                    }
                    catch (Exception e)
                    {
                        this.ViewBag.OperationName = "query Salesforce Leads";
                        this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                        this.ViewBag.ErrorMessage = e.Message;
                    }
                }
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedLeads);
        }
        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<LeadViewModel> selectedLeads = Enumerable.Empty<LeadViewModel>();
            try
            {
                selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<LeadViewModel> leads =
                            await client.QueryAsync<LeadViewModel>("SELECT Id, Salutation, FirstName, LastName, Company, Street, City, State, PostalCode, Phone, MobilePhone, Fax, Email, Status, CreatedDate, Description, UserEmail__c From Lead Where Id = '" + id + "'");
                        return leads.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Salesforce Lead Details";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedLeads.FirstOrDefault());
        }
        public async Task<ActionResult> Edit(string id)
        {
            IEnumerable<LeadViewModel> selectedLeads = Enumerable.Empty<LeadViewModel>();
            try
            {
                selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<LeadViewModel> leads =
                            await client.QueryAsync<LeadViewModel>("SELECT Id, Salutation, FirstName, LastName, Company, Street, City, State, PostalCode, Phone, MobilePhone, Fax, Email, Status, Description From Lead Where Id = '" + id + "'");
                        return leads.Records;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Leads";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            return View(selectedLeads.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeadViewModel lead)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var Lead = new LeadViewModel
            {
                Salutation = lead.Salutation,
                FirstName = lead.FirstName,
                LastName = lead.LastName,
                Company = lead.Company,
                Street = lead.Street,
                City = lead.City,
                State = lead.State,
                PostalCode = lead.PostalCode,
                Phone = lead.Phone,
                MobilePhone = lead.MobilePhone,
                Fax = lead.Fax,
                Email = lead.Email,
                Status = lead.Status,
                Description = lead.Description,
                UserEmail__c = user.Email
            };
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("Lead", lead.Id, Lead);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Edit Salesforce Leads";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(lead);
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<LeadViewModel> selectedLeads = Enumerable.Empty<LeadViewModel>();
            try
            {
                selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Contact
                    QueryResult<LeadViewModel> leads =
                        await client.QueryAsync<LeadViewModel>(string.Format("SELECT Id, Salutation, FirstName, LastName, Company, Street, City, State, PostalCode, Phone, MobilePhone, Fax, Email, Status, CreatedDate, Description, UserEmail__c  From Lead Where Id='{0}'", id));
                    return leads.Records;
                }
                );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "query Salesforce Leads";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (selectedLeads.Count() == 0)
            {
                return View();
            }
            else
            {
                return View(selectedLeads.FirstOrDefault());
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            bool success = false;
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.DeleteAsync("Lead", id);
                        return success;
                    }
                    );
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Delete Salesforce Leads";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeadViewModel lead)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var Lead = new LeadViewModel
            {
                Id = lead.Id,
                Salutation = lead.Salutation,
                FirstName = lead.FirstName,
                LastName = lead.LastName,
                Company = lead.Company,
                Street = lead.Street,
                City = lead.City,
                State = lead.State,
                PostalCode = lead.PostalCode,
                Phone = lead.Phone,
                MobilePhone = lead.MobilePhone,
                Fax = lead.Fax,
                Email = lead.Email,
                Status = lead.Status,
                CreatedDate = lead.CreatedDate,
                Description = lead.Description,
                UserEmail__c = user.Email
            };

            String id = String.Empty;
            try
            {
                id = (await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        return await client.CreateAsync("Lead", Lead);
                    }
                    )).ToString();
            }
            catch (Exception e)
            {
                this.ViewBag.OperationName = "Create Salesforce Lead";
                this.ViewBag.AuthorizationUrl = SalesforceOAuthRedirectHandler.GetAuthorizationUrl(this.Request.Url.ToString());
                this.ViewBag.ErrorMessage = e.Message;
            }
            if (this.ViewBag.ErrorMessage == "AuthorizationRequired")
            {
                return Redirect(this.ViewBag.AuthorizationUrl);
            }
            if (this.ViewBag.ErrorMessage == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(lead);
            }
        }
    }
}