using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salesforce.Common;
using Salesforce.Common.Models;

namespace SalesBuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _authorizationEndpointUrl = ConfigurationManager.AppSettings["AuthorizationEndpointUrl"];
        private readonly string _consumerKey = ConfigurationManager.AppSettings["Salesforce1:ConsumerKey"];
        private readonly string _callbackUrl = ConfigurationManager.AppSettings["CallbackUrl"];

        public HomeController()
        {
            ViewBag.LoggedIn = false;
        }

        public ActionResult Index()
        {
            if (Request.QueryString.HasKeys())
            {
                var token = Request.QueryString["token"];
                var apiVersion = Request.QueryString["api"];
                var instanceUrl = Request.QueryString["instance_url"];

                ViewBag.Token = token;
                ViewBag.ApiVersion = apiVersion;
                ViewBag.InstanceUrl = instanceUrl;

                ViewBag.LoggedIn = true;
            }

            return View();
        }

        public ActionResult Login()
        {
            var url =
                Common.FormatAuthUrl(
                    _authorizationEndpointUrl,
                    ResponseTypes.Code,
                    _consumerKey,
                    HttpUtility.UrlEncode(_callbackUrl));

            return Redirect(url);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}