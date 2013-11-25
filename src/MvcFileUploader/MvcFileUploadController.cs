using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcFileUploader.Models;

namespace MvcFileUploader
{
    public class MvcFileUploadController : Controller
    {
        private string StorageRoot
        {
            get { return Server.MapPath("~/Uploads/"); }
        }

        public ActionResult UploadDialog(FileUploadViewModel model, IDictionary<string, string> postValues)
        {
            if (postValues!=null && postValues.ContainsKey("NoKeys"))
                postValues.Clear();

            model.PostValuesWithUpload = postValues;
            model.IsDialog = true;

            return PartialView("_MvcFileUpload", model);
        }
    }
}
