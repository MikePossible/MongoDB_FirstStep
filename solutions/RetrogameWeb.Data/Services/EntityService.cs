namespace RetrogameWeb.Data.Services
{
    #region Using

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using Entities;

    #endregion

    public abstract class EntityService<T>: IEntityService<T> where T : IMongoEntity
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        protected EntityService()
        {
            MongoConnectionHandler = new MongoConnectionHandler<T>();
        }// EntityService()

        /// <summary>
        /// Create Method
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(T entity)
        {
            //Save this entity in Safe Mode
            var result = this.MongoConnectionHandler.MongoCollection.Save(
                entity,
                new MongoInsertOptions { WriteConcern = WriteConcern.Acknowledged });

            
            //If issue happened then fire in the hall!!!
            if (!result.Ok) { }            

        }// Create(...)
        
        /// <summary>
        /// Delete Method
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(string id)
        {
            var result = this.MongoConnectionHandler.MongoCollection.Remove(
                Query<T>.EQ(e => e.ID,
                                new ObjectId(id)),
                            RemoveFlags.None,
                            WriteConcern.Acknowledged);

            //If issue happened then fire in the hall!!!
            if (!result.Ok) { }
                
        }// Delete(...)

        /// <summary>
        /// Gets By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetByID(string id)
        {
            var entityQuery = Query<T>.EQ(e => e.ID, new ObjectId(id));
            return this.MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
        }// GetByID(...)

        /// <summary>
        /// Update Method
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Update(T entity);

    }// class

}// namespace
