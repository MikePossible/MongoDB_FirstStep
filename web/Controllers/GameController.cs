namespace RetrogameWeb.Controllers
{
    #region Using

    using RetrogameWeb.Data.Entities;
    using RetrogameWeb.Data.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;


    #endregion

    public class GameController : Controller
    {
        //Get: /Game/Create
        public ActionResult Create()
        {
            return View(new Game()
            {
                ReleaseDate = DateTime.Today,
                Played = false
            });
        }// Create(...)

        //Post: /Game/Create
        [HttpPost]
        public ActionResult Create(Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var gameService = new GameService();
                    gameService.Create(game);

                    return RedirectToAction("Index");
                }// if
            }
            catch
            {
                
            }// try..catch

            return View();
        }// Create(...)

        // GET: /Game/
        public ActionResult Index()
        {
            var gameService = new GameService();
            var gamesDetails = gameService.GetGamesDetails(100, 0);

            return View(gamesDetails);
        }// Index

    }// class

}// namespace