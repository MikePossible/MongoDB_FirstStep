#region Using

using System;
using MongoDB.Bson;

#endregion

namespace RetrogameWeb.Data.Entities
{
    public interface IMongoEntity
    {
        ObjectId ID { get; set; }
    }// interface
}// namespace