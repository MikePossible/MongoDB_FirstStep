namespace RetrogameWeb.Controllers
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using RetrogameWeb.Data.Entities;
    using RetrogameWeb.Data.Services;
    using Models;
    using MongoDB.Bson;

    #endregion

    public class PlayerController : Controller
    {
        #region AddScore

        //Get: /Player/AddScore
        public ActionResult AddScore(string playerID, string gameID, string gameName)
        {
            var playerService = new PlayerService();
            var score = new Score { 
                GameID = new ObjectId(gameID),
                GameName = gameName,
                ScoreDateTime = DateTime.Now,
                ScoreValue = new Random().Next(0, Int32.MaxValue)
            };

            playerService.AddScore(playerID, score);

            return RedirectToAction("Details", new { id = playerID });
        }// AddScore(...)

        #endregion

        #region Create

        //Get: /Player/Create
        public ActionResult Create() { return View(new Player()); }

        //Post: /Player/Create
        [HttpPost]
        public ActionResult Create(Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var playerService = new PlayerService();
                    playerService.Create(player);

                    return RedirectToAction("Index");
                }// if
            }
            catch { }// try..catch

            return View();
        }// Create(...)

        #endregion

        #region Delete

        //Get: /Player/Delete/38493843948
        public ActionResult Delete(string id)
        {
            var playerService = new PlayerService();
            var player = playerService.GetByID(id);

            return View(player);
        }// Delete(...)

        //Post: /Player/Delete/38493843948
        [HttpPost]
        public ActionResult Delete(Player player)
        {

            try
            {
                var playerService = new PlayerService();
                playerService.Delete(player.ID.ToString());

                return RedirectToAction("Index");
            }
            catch { }

            return View();
        }// Delete(...)

        #endregion

        //Get: /Player/Details/38493843948
        public ActionResult Details(string id)
        {
            var playerService = new PlayerService();
            var player = playerService.GetByID(id);

            return View(player);
        }// Details(...)

        #region Edit

        //Get: /Player/Edit/38493843948
        public ActionResult Edit(string id)
        {
            var playerService = new PlayerService();
            var player = playerService.GetByID(id);

            return View(player);
        }// Edit(...)

        //Post: /Player/Edit/38493843948
        [HttpPost]
        public ActionResult Edit(Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var playerService = new PlayerService();
                    playerService.Update(player);

                    return RedirectToAction("Index");
                }// if
            }
            catch { }

            return View();
        }// Edit(...)

        #endregion

        // GET: /Player/
        public ActionResult Index()
        {
            var playerService = new PlayerService();
            var playerDetails = playerService.GetPlayerDetails(100, 0);            

            return View(playerDetails);
        }// Index

        //Get: /Player/PlayGames/38493843948
        public ActionResult PlayGames(string id)
        {
            var playerService = new PlayerService();
            var player = playerService.GetByID(id);
            var gameService = new GameService();
            var availableGames = gameService.GetGamesDetails(100, 0);

            var playerGames = new PlayerGames()
            {
                Player = player,
                AvailableGames = new List<Game>(availableGames)
            };

            return View(playerGames);
        }// PlayGames(...)

    }// class

}// namespace
