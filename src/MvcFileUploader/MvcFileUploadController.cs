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

        public ActionResult UploadDialog(FileUploadView uploadConfig)
        { 
            return PartialView("_MvcFileUpload", uploadConfig);
        }


        public ActionResult UploadPopup(FileUploadView model)
        {
            var viewModel = new MvcFileUploadModel();
            viewModel.UploadAt(model.UploadUrl)
                .ReturnAt(model.ReturnUrl)
                .WithFileTypes(model.FileTypes)
                .WithMaxFileSize(model.MaxFileSizeInBytes);

            return PartialView("_MvcFileUpload_tmpl", );
        }

    }
}
