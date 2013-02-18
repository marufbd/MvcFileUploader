using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcFileUploader.Models
{
    public class FileUploadConfig
    {
        //public int EntityId { get; set; }
        public string ReturnUrl { get; set; }
        public string FileTypes { get; set; } // jquery-image-upload format expression string
        public long MaxFileSizeInBytes { get; set; } // in bytes

        public string UploadUrl { get; set; }
    }
}
