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

                // Get most popular images
                lst = insta.GetMostPopularImages(ViewBag.Token);

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
            ViewData["AuthLink"] = insta.Authenticate();            

            return View();
        }// Index()
        

    }// class
}// namespace
