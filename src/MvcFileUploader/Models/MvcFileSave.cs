using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcFileUploader.Models
{
    public class MvcFileSave
    {
        public HttpPostedFileBase File { get; set; }

        public string StorageDirectory { get; set; }

        public string UrlPrefix { get; set; }

        public string DeleteUrl { get; set; }

        public DateTime FileTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the file to be saved if not set, <see cref="FileSaver"/> will generate a filename suffixed with filetimestamp.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="FileSaver"/> exceptions should be thrown or set any exception message in the error property which is default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if <see cref="FileSaver"/> should throw exceptions; otherwise, <c>false</c>.
        /// </value>
        public bool ThrowExceptions { get; set; }

        public void AddFileUriParamToDeleteUrl(string paramName, string fileUrl)
        {
            
            if (!String.IsNullOrEmpty(this.DeleteUrl))
            {
                // means has query
                if (DeleteUrl.Contains("?") && !DeleteUrl.Contains("&"+paramName))
                {
                    this.DeleteUrl += String.Format("&{0}={1}", paramName, fileUrl);
                }
                else
                {
                    this.DeleteUrl += String.Format("?{0}={1}", paramName, fileUrl);
                }                    
            }                            
                        
        }
    }
}
