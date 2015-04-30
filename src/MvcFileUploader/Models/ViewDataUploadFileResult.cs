/*
 * MvcFileUploader utility
 * https://github.com/marufbd/MvcFileUploader
 *
 * Copyright 2015, Maruf Rahman
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

using System;

namespace MvcFileUploader.Models
{
    /// <summary>
    /// The status Json object for each file returned to blueimp uploader
    /// with some additional properties for use in server controller
    /// </summary>
    public class ViewDataUploadFileResult
    {
        private string _error;

        //for JQuery file upload
        /// <summary>
        /// Gets or sets the error. If neither null nor empty indicates something wrong happened
        /// </summary>
        /// <value>
        /// The error text.
        /// </value>
        public string error
        {
            get { return _error; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _error = value;
                    deleteUrl = String.Empty;
                    thumbnailUrl = String.Empty;
                    url = String.Empty;
                }

            }
        }

        public string name { get; set; }
        /// <summary>
        /// Gets or sets the size of the file in bytes
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteType { get; set; }

        
        /// <summary>
        /// Gets or sets the full path. for use from any controller and/or views. 
        /// Should be removed in the returned json to client
        /// </summary>
        /// <value>
        /// The full path.
        /// </value>
        public string FullPath { get; set; }
        public string SavedFileName { get; set; }
        //public ErrorType? ErrorType { get; set; }

        //for storing
        public string Title { get; set; }
    }
}
