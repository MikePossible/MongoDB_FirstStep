using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstStep.Models.ViewModel
{
    public class CallBackVM
    {
        /// <summary>
        /// Gets or sets code returned by insta
        /// </summary>
        public string InstagramCode { get; set; }

        /// <summary>
        /// Gets or sets authorization token
        /// </summary>
        public string AuthorizationToken { get; set; }
        
        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets cities
        /// </summary>
        public SelectList Cities { get; set; }

        /// <summary>
        /// Gets or sets images
        /// </summary>
        public List<InstagramImage> Images { get; set; }
    }
}