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
        public static List<ViewDataUploadFileResult> StoreFiles(IEnumerable<MvcFileSave> mvcFiles)
        {
            return mvcFiles.Select(x => StoreFile(delegate(MvcFileSave f)
                                                      {
                                                          if (f == null) throw new ArgumentNullException("MvcFileSave");
                                                          f = x;
                                                      })).ToList();
        }


        public static ViewDataUploadFileResult StoreFile(Action<MvcFileSave> action)
        {
            var mvcFile = new MvcFileSave();
            action(mvcFile);
            
            ViewDataUploadFileResult status;
            
            var dirInfo = new DirectoryInfo(mvcFile.StorageDirectory);
            var file = mvcFile.File;
            var fileExtension = Path.GetExtension(mvcFile.File.FileName);
            var fileName = Path.GetFileNameWithoutExtension(mvcFile.File.FileName);
            var genName = fileName + "-" + DateTime.Now.ToFileTimeUtc();
            var genFileName = genName + fileExtension;
            var fullPath = Path.Combine(mvcFile.StorageDirectory, genFileName);

            try
            {
                mvcFile.File.SaveAs(fullPath);

                var viewDataUploadFileResult = new ViewDataUploadFileResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = mvcFile.UrlPrefix + "/" + genFileName,
                    //delete_url = Url.Action("DeleteFile", new { fileUrl = "/"+storageRoot+"/" + genFileName }),
                    //thumbnail_url = thumbUrl + "?width=100",
                    delete_type = "POST",
                    title = fileName,

                    //for controller use
                    fullpath = dirInfo.FullName + "/" + genFileName
                };

                //add delete url                           
                mvcFile.AddFileUriParamToDeleteUrl("fileUrl", viewDataUploadFileResult.url);
                viewDataUploadFileResult.delete_url = mvcFile.DeleteUrl;
                

                status = viewDataUploadFileResult;
            }
            catch (Exception exc)
            {
                status=new ViewDataUploadFileResult()
                {
                    error = "error: " + exc.Message,
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    title = fileName
                };
            }            

            return status;
        } 
    
    
    } 
    
}
