using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            mvcFile.FileTimeStamp = DateTime.Now.ToUniversalTime();
            action(mvcFile);
            
            ViewDataUploadFileResult status;
            
            var dirInfo = new DirectoryInfo(mvcFile.StorageDirectory);
            var file = mvcFile.File;
            var fileExtension = Path.GetExtension(mvcFile.File.FileName);
            var fileName = Path.GetFileNameWithoutExtension(mvcFile.File.FileName);
            var genName = fileName + "-" + mvcFile.FileTimeStamp.ToFileTime();
            var genFileName = String.IsNullOrEmpty(mvcFile.FileName) ? genName + fileExtension : mvcFile.FileName;// get filename if set
            var fullPath = Path.Combine(mvcFile.StorageDirectory, genFileName);

            try
            {                
                var viewDataUploadFileResult = new ViewDataUploadFileResult()
                {
                    name = file.FileName,
                    SavedFileName = genFileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = mvcFile.UrlPrefix + "/" + genFileName,
                    //delete_url = Url.Action("DeleteFile", new { fileUrl = "/"+storageRoot+"/" + genFileName }),
                    //thumbnail_url = thumbUrl + "?width=100",
                    delete_type = "POST",
                    Title = fileName,

                    //for controller use
                    FullPath = dirInfo.FullName + "/" + genFileName
                };

                //add delete url                           
                mvcFile.AddFileUriParamToDeleteUrl("fileUrl", viewDataUploadFileResult.url);
                viewDataUploadFileResult.delete_url = mvcFile.DeleteUrl;
                

                status = viewDataUploadFileResult;   

                mvcFile.File.SaveAs(fullPath);
            }
            catch (Exception exc)
            {
                if (mvcFile.ThrowExceptions)
                    throw;
                
                status = new ViewDataUploadFileResult()
                             {
                                 error = exc.Message,
                                 name = file.FileName,
                                 size = file.ContentLength,
                                 type = file.ContentType                                 
                             };
            }

            return status;
        } 
    
    
    } 
    
}
