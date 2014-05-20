namespace RetrogameWeb.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Class containing city properties
    /// </summary>
    [BsonIgnoreExtraElements]
    public class City : MongoEntity
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets latitude
        /// </summary>
        public string Latitude { get; set; }
        
        /// <summary>
        /// Gets or sets longitude
        /// </summary>
        public string Longitude { get; set; }
    }
}
