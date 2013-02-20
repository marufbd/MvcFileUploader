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
