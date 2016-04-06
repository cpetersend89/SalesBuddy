using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SalesBuddy.Models.Salesforce;
using SalesBuddy.Salesforce;
using Salesforce.Common.Models;

namespace SalesBuddy.Controllers
{
    public class LeadsController : Controller
    {
        const string _LeadsPostBinding = "Id,Salutation,FirstName,LastName,Company,Street,City,PostalCode,Phone,MobilePhone,Fax,Email,Website,Status,CreatedDate,CreatedById,Description";
        // GET: Leads
        public async Task<ActionResult> Index()
        {
            IEnumerable<Lead> selectedLeads = Enumerable.Empty<Lead>();
            try
            {
                selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Lead> leads =
                            await client.QueryAsync<Lead>("SELECT Id, LastName, Salutation, FirstName, Phone, MobilePhone, Email From Lead");
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
            return View(selectedLeads);
        }
        public async Task<ActionResult> Details(string id)
        {
            IEnumerable<Lead> selectedLeads = Enumerable.Empty<Lead>();
            try
            {
                selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Lead> leads =
                            await client.QueryAsync<Lead>("SELECT Id, Salutation, FirstName, LastName, Company, Street, City, PostalCode, Phone, MobilePhone, Fax, Email, Website, Status, CreatedDate, CreatedById, Description From Lead Where Id = '" + id + "'");
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
            IEnumerable<Lead> selectedLeads = Enumerable.Empty<Lead>();
            try
            {
                selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        QueryResult<Lead> leads =
                            await client.QueryAsync<Lead>("SELECT Id, Salutation, FirstName, LastName, Company, Street, City, PostalCode, Phone, MobilePhone, Fax, Email, Website, Status, CreatedDate, CreatedById, Description From Lead Where Id = '" + id + "'");
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
        public async Task<ActionResult> Edit([Bind(Include = _LeadsPostBinding)] Lead lead)
        {
            SuccessResponse success = new SuccessResponse();
            try
            {
                success = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        success = await client.UpdateAsync("Lead", lead.Id, lead);
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
            IEnumerable<Lead> selectedLeads = Enumerable.Empty<Lead>();
            try
            {
                selectedLeads = await SalesforceService.MakeAuthenticatedClientRequestAsync(
                async (client) =>
                {
                    // Query the properties you'll display for the user to confirm they wish to delete this Contact
                    QueryResult<Lead> leads =
                        await client.QueryAsync<Lead>(string.Format("SELECT Id, Salutation, FirstName, LastName, Company, Street, City, PostalCode, Phone, MobilePhone, Fax, Email, Website, Status, CreatedDate, CreatedById, Description From Lead Where Id='{0}'", id));
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
        public async Task<ActionResult> Create([Bind(Include = _LeadsPostBinding)] Lead lead)
        {
            String id = String.Empty;
            try
            {
                id = (await SalesforceService.MakeAuthenticatedClientRequestAsync(
                    async (client) =>
                    {
                        return await client.CreateAsync("Lead", lead);
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