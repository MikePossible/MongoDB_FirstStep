namespace RetrogameWeb.Data.Entities
{
    #region Using

    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    #endregion

    [BsonIgnoreExtraElements]
    public class Score
    {
        public ObjectId GameID { get; set; }
        public string GameName { get; set; }
        public int ScoreValue { get; set; }
        public DateTime ScoreDateTime { get; set; }
    }// class

}// namespace
