namespace RetrogameWeb.Data.Entities
{

    #region Using

    using System;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    #endregion

    [BsonIgnoreExtraElements]
    public class Game : MongoEntity
    {
        //Constructor
        public Game()
        {
            Categories = new List<string>();
        }// Game(...)

        #region Properties

        public string Name { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime ReleaseDate { get; set; }

        public List<string> Categories { get; set; }

        public bool Played { get; set; }

        #endregion
    }// class
}// namespace