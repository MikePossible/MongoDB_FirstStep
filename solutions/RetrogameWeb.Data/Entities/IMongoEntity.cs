namespace RetrogameWeb.Data.Entities
{
    #region Using

    using System;
    using MongoDB.Bson;

    #endregion

    public interface IMongoEntity
    {
        ObjectId ID { get; set; }
    }// interface
}// namespace