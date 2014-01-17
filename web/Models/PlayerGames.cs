namespace RetrogameWeb.Models
{
    #region Using

    using RetrogameWeb.Data.Entities;
    using System.Collections.Generic;

    #endregion

    public class PlayerGames
    {
        public Player Player { get; set; }

        public List<Game> AvailableGames { get; set; }
    }// class

}// namespace