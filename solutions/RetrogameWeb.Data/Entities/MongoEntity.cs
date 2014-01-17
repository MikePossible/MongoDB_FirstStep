#region Using

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace RetrogameWeb.Data.Entities
{
    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        public ObjectId ID { get; set; }
    }// class
}// namespace