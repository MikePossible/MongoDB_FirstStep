namespace RetrogameWeb.Data.Entities
{
    #region Using

    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    #endregion

    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        public ObjectId ID { get; set; }
    }// class
}// namespace