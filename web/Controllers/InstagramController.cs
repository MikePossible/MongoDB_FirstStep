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

    using RetrogameWeb.Data.Entities;
    using RetrogameWeb.Data.Services;

    #endregion

    public class InstagramController : Controller
    {
        //ToDo implement IoC
        private Instagram insta = new Instagram();
        private CityService _svc = new CityService();


        /// <summary>
        /// /Instagram/Callback
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Callback(string code)
        {
            ViewBag.Code = code;
            var lst = new List<InstagramImage>();

            try
            {
                // Authorize
                // If the sessiond doesn't contain the Instagram object then connect to the API
                if (this.insta.SessionOAuth == null)
                    this.insta.Authorize(code);

                ViewBag.Token = this.insta.SessionOAuth.Access_Token;
                ViewBag.Username = this.insta.SessionOAuth.User.Username;


                // Create subscription
                // insta.CreateSubscription();

                // Get most popular images for Paris
                City paris = this._svc.GetByName("Sao Paulo");
                lst = this.insta.GetMostPopularImages(ViewBag.Token, paris);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();                
            }

            ViewBag.List = lst;

            return View();
        }


        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Populate MongoDB City
            this.PopulateCity();

            ViewBag.AuthLink = this.insta.Authenticate();            

            return View();
        }

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
                if (!this._svc.Contains(c))
                    this._svc.Create(c);
            }
        }
    }// class
}// namespace
