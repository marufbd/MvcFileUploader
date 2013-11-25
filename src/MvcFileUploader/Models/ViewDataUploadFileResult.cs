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
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteType { get; set; }

        //for use from any controller and/or views
        public string FullPath { get; set; }
        public string SavedFileName { get; set; }
        //public ErrorType? ErrorType { get; set; }

        //for storing
        public string Title { get; set; }
    }
}
