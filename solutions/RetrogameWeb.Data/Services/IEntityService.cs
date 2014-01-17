namespace RetrogameWeb.Data.Services
{
    #region Using

    using Entities;

    #endregion

    public interface IEntityService<T> where T: IMongoEntity
    {
        void Create(T entity);
        void Delete(string id);
        T GetByID(string id);
        void Update(T entity);
    }// interface
}// namespace