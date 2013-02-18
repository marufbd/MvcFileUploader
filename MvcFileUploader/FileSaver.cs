using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcFileUploader.Models;

namespace MvcFileUploader
{
    public class FileSaver
    {
        public static List<ViewDataUploadFileResult> StoreWholeFile(HttpRequestBase request, string storageRoot, string urlPrefix, string deleteUri=null, object deleteUriRouteValues=null)
        {
            var statuses = new List<ViewDataUploadFileResult>();

            var dirInfo = new DirectoryInfo(storageRoot);

            for (int i = 0; i < request.Files.Count; i++)
            {
                //bool error = false; // indicates any error happened on processing

                var file = request.Files[i];

                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var genName = fileName + "-" + DateTime.Now.ToFileTimeUtc();
                var genFileName = genName + fileExtension;
                var fullPath = Path.Combine(storageRoot, genFileName);

                try
                {
                    file.SaveAs(fullPath);                    

                    statuses.Add(new ViewDataUploadFileResult()
                    {
                        name = file.FileName,
                        size = file.ContentLength,
                        type = file.ContentType,
                        url = urlPrefix +"/"+ genFileName,
                        //delete_url = Url.Action("DeleteFile", new { fileUrl = "/"+storageRoot+"/" + genFileName }),
                        //thumbnail_url = thumbUrl + "?width=100",
                        delete_type = "POST",
                        title = fileName,

                        //for controller use
                        fullpath = dirInfo.FullName + "/" + genFileName
                    });
                }
                catch (Exception exc)
                {
                    statuses.Add(new ViewDataUploadFileResult()
                                 {
                                     error = "error: "+exc.Message,
                                     name = file.FileName,
                                     size = file.ContentLength,
                                     type = file.ContentType,
                                     title = fileName
                                 });                    
                } 
            }


            //add delete_urls
            if(deleteUri!=null)
            {
                var uri = deleteUri;
                foreach (var status in statuses)
                {
                    uri += "?fileUri=" + status.url;
                    
                    if(deleteUriRouteValues!=null)
                        deleteUriRouteValues.GetType().GetProperties().ToList().ForEach(
                            x => uri += "&" + x.Name + "=" + x.GetValue(deleteUriRouteValues, null).ToString());

                    status.delete_url = uri;
                } 
            }            

            return statuses;
        } 

    }

    
}
