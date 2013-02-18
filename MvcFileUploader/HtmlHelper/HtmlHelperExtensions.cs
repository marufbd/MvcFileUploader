using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcFileUploader.HtmlHelper
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// MVCs the upload button.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="fileTypes">The file types.</param>
        /// <param name="maxFileSize">Size of the max file in bytes.</param>
        /// <param name="labelText">Label for link </param>
        /// <param name="dataTarget">Target modal div with # prefixed </param>
        /// <param name="uploadUrl">The upload URL.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="htmlAttributes">html attributes for link </param>
        /// <returns></returns>
        public static IHtmlString MvcUploadButton<TModel>(this HtmlHelper<TModel> helper, string labelText, string dataTarget, string uploadUrl, string returnUrl, string fileTypes = @"/(\.|\/)(jpe?g|png)$/i", uint maxFileSize=5000000, object htmlAttributes=null)
        {
            var tag = new TagBuilder("a");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            
            tag.Attributes.Add("href",
                               urlHelper.Action("UploadDialog", "MvcFileUpload",
                                                new
                                                    {
                                                        UploadUrl = uploadUrl,
                                                        ReturnUrl = String.IsNullOrEmpty(returnUrl)?helper.ViewContext.RequestContext.HttpContext.Request.Url.ToString():returnUrl,
                                                        MaxFileSizeInBytes = maxFileSize,
                                                        FileTypes = fileTypes
                                                    }));

            tag.Attributes.Add("role", "button");
            tag.Attributes.Add("data-toggle", "modal");
            tag.Attributes.Add("data-target", dataTarget);
            

            tag.InnerHtml = labelText;

            if (htmlAttributes != null)
                htmlAttributes.GetType().GetProperties().ToList().ForEach(p=>tag.Attributes.Add(p.Name, p.GetValue(htmlAttributes, null).ToString()));

            return new MvcHtmlString(tag.ToString());
        }
    }
}
