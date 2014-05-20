namespace RetrogameWeb.Data.Services
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using RetrogameWeb.Data.Entities;

    #endregion

    /// <summary>
    /// Class of city service
    /// </summary>
    public class CityService : EntityService<City>
    {
        /// <summary>
        /// Returns true if the city is already in the DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Contains(City entity)
        {
            return this.MongoConnectionHandler.MongoCollection.FindAllAs<City>()
                .Any(x => x.Name.ToLower() == entity.Name.ToLower());
        }

        public City GetByName(string name)
        {
            return this.MongoConnectionHandler.MongoCollection.FindAllAs<City>()
                .Where(x => x.Name.ToLower() == name.ToLower())
                .SingleOrDefault();
        }

        /// <summary>
        /// Update City
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(City entity) { }
    }
}
