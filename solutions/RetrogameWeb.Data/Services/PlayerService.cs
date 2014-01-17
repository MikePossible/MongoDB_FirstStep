namespace RetrogameWeb.Data.Services
{
    #region Using

    using System;

    using Entities;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using System.Collections.Generic;

    #endregion

    public class PlayerService: EntityService<Player>
    {
        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="score"></param>
        public void AddScore(string playerID, Score score)
        {
            var playerObjectID = new ObjectId(playerID);

            var updateResult = this.MongoConnectionHandler.MongoCollection.Update(
                Query<Player>.EQ(p => p.ID, playerObjectID),
                Update<Player>.Push(p => p.Scores, score),
                new MongoUpdateOptions { WriteConcern = WriteConcern.Acknowledged });


            //If issue happens
            if (updateResult.DocumentsAffected == 0) { }
        }// AddScore(...)


        /// <summary>
        /// Gets Player details
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public IEnumerable<Player> GetPlayerDetails(int limit, int skip)
        {
            var playerDetails = this.MongoConnectionHandler.MongoCollection.FindAllAs<Player>()
                                .SetSortOrder(SortBy<Player>.Ascending(p => p.Name))
                                .SetLimit(limit)
                                .SetSkip(skip)
                                .SetFields(Fields<Player>.Include(p => p.ID, p => p.Name));

            return playerDetails;
        }// GetPlayerDetails(...)

        /// <summary>
        /// Update player details
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(Player entity) {
            var updateResult = this.MongoConnectionHandler.MongoCollection.Update(
                Query<Player>.EQ(p => p.ID, entity.ID),
                Update<Player>.Set(p => p.Name, entity.Name)
                    .Set(p => p.Gender, entity.Gender),
                new MongoUpdateOptions { WriteConcern = WriteConcern.Acknowledged });

            //if issue happens
            if (updateResult.DocumentsAffected == 0) { }
        }// Update(...)

    }// class
}// namespace