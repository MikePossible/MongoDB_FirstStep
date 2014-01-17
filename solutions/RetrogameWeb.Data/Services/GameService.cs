namespace RetrogameWeb.Data.Services
{
    #region Using

    using RetrogameWeb.Data.Entities;
    using MongoDB.Driver.Builders;
    
    using System;
    using System.Collections.Generic;

    #endregion

    public class GameService: EntityService<Game>
    {
        /// <summary>
        /// Gets list of games
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IEnumerable<Game> GetGamesDetails(int limit, int skip)
        {
            var gamesCursor = this.MongoConnectionHandler.MongoCollection.FindAllAs<Game>()
                .SetSortOrder(SortBy<Game>.Descending(g => g.ReleaseDate))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<Game>.Include(g => g.ID, g => g.Name, g => g.ReleaseDate));

            return gamesCursor;
        }// GetGamesDetails(...)

        /// <summary>
        /// Update Game
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(Game entity) { }

    }// class

}// namespace