using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstStep.Models
{
    /// <summary>
    /// Class for Instagram image properties
    /// </summary>
    public class InstagramImage
    {
        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ownerName"></param>
        /// <param name="date"></param>
        public InstagramImage(string url, string ownerName, string caption, string date)
        {
            this.Url = url;
            this.OwnerName = ownerName;
            this.Caption = caption;
            this.DateUploaded = date;
        }

        /// <summary>
        /// Gets or sets url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets owner name
        /// </summary>
        public string OwnerName { get; set; }


        /// <summary>
        /// Gets or sets caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the date when it was uploaded
        /// </summary>
        public string DateUploaded { get; set; }
    }
}