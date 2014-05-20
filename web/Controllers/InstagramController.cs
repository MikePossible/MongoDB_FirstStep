using System.Web.Helpers;
using FirstStep.Models.ViewModel;

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
        public ActionResult Callback(string code, string City)
        {
            var vm = new CallBackVM();
            vm.InstagramCode = code;
            vm.Cities = new SelectList(this.GetListCity(), "Name", "Name");

            try
            {
                // Authorize
                // If the sessiond doesn't contain the Instagram object then connect to the API
                if (this.insta.SessionOAuth == null)
                    insta.Authorize(code);

                vm.AuthorizationToken = this.insta.SessionOAuth.Access_Token;
                vm.UserName = this.insta.SessionOAuth.User.Username;

                // Create subscription
                // insta.CreateSubscription();
                
                // Get most popular images for Paris
                //City paris = this._svc.GetByName("Sao Paulo");
                //vm.Images = this.insta.GetMostPopularImages(ViewBag.Token, paris);
                
            }
            catch (Exception ex)
            {
                var error = ex.ToString();                
            }

            return View(vm);
        }

        /// <summary>
        /// Return list of images for JS
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListImages(string cityName)
        {
            // Get most popular images for a city
            City currentCity = this._svc.GetByName(cityName);
            return Json(this.insta.GetMostPopularImages(ViewBag.Token, currentCity), JsonRequestBehavior.AllowGet);
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
            ViewBag.Cities = new SelectList(GetListCity(), "Name", "Name");

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

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns>list of cities</returns>
        private List<City> GetListCity()
        {
            return _svc.GetAllCities();
        }
    }
}
