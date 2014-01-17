namespace RetrogameWeb.Data.Entities
{
    #region Using

    using System;
    using System.Collections.Generic;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;


    #endregion

    [BsonIgnoreExtraElements]
    public class Player
    {
        //Constructor
        public Player()
        {
            Scores = new List<Score>();
        }// Player(...)


        #region Properties

        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Gender Gender { get; set; }

        public List<Score> Scores { get; set; }

        #endregion
    }// class

}// namespace