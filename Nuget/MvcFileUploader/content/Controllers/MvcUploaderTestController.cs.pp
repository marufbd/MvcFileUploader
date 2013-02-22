using System.Collections.Generic;
using System.Web.Mvc;
using MvcFileUploader;
using MvcFileUploader.Models;

namespace $rootnamespace$.Controllers
{
    public class MvcUploaderTestController : Controller
    {
        //
        // GET: /MvcUploaderTest/Demo

        public ActionResult Demo()
        {
            return View();
        }

        public ActionResult UploadFile(int? entityId) // optionally receive values specified with Html helper
        {
            // here we can send in some extra info to be included with the delete url 
            var statuses=new List<ViewDataUploadFileResult>();
            for (var i = 0; i < Request.Files.Count; i++ )
            {
                var st = FileSaver.StoreFile(x=>
                                                 {
                                                     x.File = Request.Files[i];
                                                     //note how we are adding an additional value to be posted with delete request
                                                     //and giving it the same value posted with upload
                                                     x.DeleteUrl = Url.Action("DeleteFile", new {entityId = entityId});
                                                     x.StorageDirectory = Server.MapPath("~/Content/uploads");
                                                     x.UrlPrefix = "/Content/uploads";
                                                 });

                statuses.Add(st);
            }             

            //statuses contains all the uploaded files details (if error occurs the check error property is not null or empty)
            //todo: add additional code to generate thumbnail for videos, associate files with entities etc
            
            //adding thumbnail url for jquery file upload javascript plugin
            //statuses.ForEach(x=>x.thumbnail_url=x.url+"?width=80&height=80"); // uses ImageResizer httpmodule to resize images from this url


            return Json(statuses);
        }



        //here i am receving the extra info injected
        public ActionResult DeleteFile(int? entityId, string fileUrl)
        {
            var filePath = Server.MapPath("~" + fileUrl);

            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            return new HttpStatusCodeResult(200); // trigger success
        }

    }
}
