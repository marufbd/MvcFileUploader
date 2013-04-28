using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcFileUploader.Models
{
    public class FileUploadView
    {
        public FileUploadView()
        {
            PostValuesWithUpload=new Dictionary<string, string>();
            UI = UploadUI.Bootstrap;
        }

        //public int EntityId { get; set; }
        public string ReturnUrl { get; set; }
        public string FileTypes { get; set; } // jquery-image-upload format expression string
        public long MaxFileSizeInBytes { get; set; } // in bytes

        public string UploadUrl { get; set; }

        public bool IsDialog { get; set; }

        public UploadUI UI { get; set; }

        public IDictionary<string, string> PostValuesWithUpload { get; set; } 
    }

    public enum UploadUI
    {
        Bootstrap,
        JQueryUI
    }
}


