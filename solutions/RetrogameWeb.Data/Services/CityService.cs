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

        /// <summary>
        /// Gets a city by name
        /// </summary>
        /// <param name="name">city name</param>
        /// <returns>a city</returns>
        public City GetByName(string name)
        {
            return this.MongoConnectionHandler.MongoCollection.FindAllAs<City>()
                .Where(x => x.Name.ToLower() == name.ToLower())
                .SingleOrDefault();
        }

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns>list of city</returns>
        public List<City> GetAllCities()
        {
            return this.MongoConnectionHandler.MongoCollection.FindAllAs<City>()
                .ToList();
        }

        /// <summary>
        /// Update City
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(City entity) { }
    }
}
