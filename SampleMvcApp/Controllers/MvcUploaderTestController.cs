using System.Collections.Generic;
using System.Web.Mvc;
using MvcFileUploader;
using MvcFileUploader.Models;

namespace SampleMvcApp.Controllers
{
    public class MvcUploaderTestController : Controller
    {
        //
        // GET: /MvcUploaderTest/Demo

        public ActionResult Demo()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            // here we can send in some extra info to be included with the delete url
            List<ViewDataUploadFileResult> statuses = FileSaver.StoreWholeFile(Request, Server.MapPath("~/Content/uploads"), "/Content/uploads",
                                                                               Url.Action("DeleteFile"), new {entityId = 123});

            JsonResult result = Json(statuses); 

            //statuses contains all the uploaded files details (if error occurs the check error property is not null or empty)
            //todo: add additional code to generate thumbnail for videos, associate files with entities etc

            return result;
        }



        //here i am receving the extra info injected
        public ActionResult DeleteFile(int entityId, string fileUri)
        {
            var filePath = Server.MapPath("~" + fileUri);

            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            return new HttpStatusCodeResult(200); // trigger success
        }

    }
}
