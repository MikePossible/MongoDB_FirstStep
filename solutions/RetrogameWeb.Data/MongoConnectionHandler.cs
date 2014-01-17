namespace RetrogameWeb.Data
{
    #region Using

    using System;
    using System.Collections.Generic;

    using MongoDB.Driver;
    using RetrogameWeb.Data.Entities;

    #endregion

    /// <summary>
    /// Connect to the MongoDB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoConnectionHandler<T> where T : IMongoEntity
    {
        private readonly string connectionString = "mongodb://localhost";
        private readonly string databaseName = "retrogramesweb";

        public MongoCollection<T> MongoCollection { get; private set; }

        public MongoConnectionHandler()
        {
            //Gets a thread-safe client object
            var mongoClient = new MongoClient(connectionString);

            //Gets a reference to server object
            var mongoServer = mongoClient.GetServer();

            //Gets a reference to the Db
            var db = mongoServer.GetDatabase(databaseName);

            //Gets a reference to the collection object from the db
            MongoCollection = db.GetCollection<T>(typeof(T).Name.ToLower() + "s");
        }// MongoConnectionHandler(...)

    }// class
}// namespace