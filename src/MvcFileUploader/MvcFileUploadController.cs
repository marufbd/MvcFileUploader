/*
 * MvcFileUploader utility
 * https://github.com/marufbd/MvcFileUploader
 *
 * Copyright 2015, Maruf Rahman
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

using System.Collections.Generic;
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
