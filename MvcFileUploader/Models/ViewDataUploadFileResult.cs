using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcFileUploader.Models
{
    public class ViewDataUploadFileResult
    {
        //for JQuery file upload
        public string error { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string delete_url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_type { get; set; }

        //for use from any controller 
        public string fullpath { get; set; }

        //for storing
        public string title { get; set; }
    }
}
