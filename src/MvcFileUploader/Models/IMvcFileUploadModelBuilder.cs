/*
 * MvcFileUploader utility
 * https://github.com/marufbd/MvcFileUploader
 *
 * Copyright 2015, Maruf Rahman
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

using System.Web;

namespace MvcFileUploader.Models
{
    public interface IMvcFileUploadModelBuilder
    {
        //builder methods
        IMvcFileUploadModelBuilder UploadAt(string url);
        IMvcFileUploadModelBuilder ReturnAt(string url);
        IMvcFileUploadModelBuilder WithFileTypes(string fileTypes);
        IMvcFileUploadModelBuilder WithMaxFileSize(long sizeInBytes);
        IMvcFileUploadModelBuilder MaxNumberOfFiles(int maxNoOfFiles);
        IMvcFileUploadModelBuilder DisableImagePreview();

        IMvcFileUploadModelBuilder AddFormField(string fieldName, string fieldValue);
        IMvcFileUploadModelBuilder Template(string template);
        IMvcFileUploadModelBuilder UIStyle(UploadUI ui);

        //render
        IMvcFileUploadModelBuilder ExcludeSharedScript();
        IHtmlString RenderInline();
        IHtmlString RenderInline(string template);
        IHtmlString RenderPopup(string labelTxt, string dataTarget, object htmlAttributes = null);
    }
}
