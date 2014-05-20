using RetrogameWeb.Data.Entities;
using RetrogameWeb.Data.Services;

namespace FirstStep.Controllers
{
    #region Using

    using FirstStep.Models;
    using InstaSharp;
    using InstaSharp.Endpoints;
    using InstaSharp.Models;
    using InstaSharp.Models.Responses;    
    using System;
    using System.Collections.Generic;        
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    #endregion

    public class InstagramController : Controller
    {
        //Constructor
        //ToDo implement IoC
        private Instagram insta = new Instagram();
        public InstagramController() { }// Instagram

        CityService svc = new CityService();


        // GET: /Instagram/Callback
        //[Get("oauth")]   
        [HttpGet]
        public ActionResult Callback(string code)
        {
            ViewBag.Code = code;
            var lst = new List<InstagramImage>();

            try
            {
                //Authorize
                //If the sessiond doesn't contain the Instagram object then connect to the API
                if (insta.SessionOAuth == null)
                    insta.Authorize(code);

                ViewBag.Token = insta.SessionOAuth.Access_Token;
                ViewBag.Username = insta.SessionOAuth.User.Username;


                //Create subscription
                //insta.CreateSubscription();

                // Get most popular images for Paris
                City paris = this.svc.GetByName("Sao Paulo");
                
                lst = this.insta.GetMostPopularImages(ViewBag.Token, paris);

                //Doesn't work
                //var auth = new OAuth(insta.Config);

                ////Callback to instagram
                //var oauthResp = auth.RequestToken(code);

            }
            catch (Exception ex)
            {
                var error = ex.ToString();                
            }

            ViewBag.List = lst;

            return View();

            //return RedirectToAction("Index");
        }// Index()


        //Get: /Instagram/
        public ActionResult Index()
        {
            // Populate MongoDB City
            this.PopulateCity();

            ViewData["AuthLink"] = insta.Authenticate();            

            return View();
        }// Index()

        /// <summary>
        /// Add cities into MongoDB
        /// Would be better to populate it by using following api http://api.geonames.org/search?q=london&maxRows=10&username=demo
        /// </summary>
        private void PopulateCity()
        {
            var lst = new List<City>()
            {
                new City() { Name = "Paris", Latitude = "48.85341", Longitude = "2.3488" },
                new City() { Name = "Zagreb", Latitude = "45.81444", Longitude = "15.97798" },
                new City() { Name = "Sao Paulo", Latitude = "-23.5475", Longitude = "-46.63611" },
                new City() { Name = "London", Latitude = "51.50853", Longitude = "-0.12574" }
            };

            // Insert into the DB
            foreach (City c in lst)
            {
                if (!this.svc.Contains(c))
                {
                    svc.Create(c);
                }
            }
        }
    }// class
}// namespace
