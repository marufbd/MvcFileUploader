using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace MvcFileUploader.Models
{
    using System.Web.Mvc;

    public class MvcFileUploadModel : IMvcFileUpload
    {
        private readonly HtmlHelper _helper;

        private string _returnUrl = "/";
        private string _fileTypes = ""; // jquery-image-upload format expression string
        private long _maxFileSizeInBytes = 10485760; // in bytes, default 10MB
        private string _uploadUrl = "/upload";        
        private string _renderType = "link";
        private string _template = "_MvcFileUpload";

        private bool _includeScriptAndTemplate = true;


        private UploadUI _uiStyle = UploadUI.Bootstrap;
        private string _popupLabelTxt="Upload";
        private object _popupElementHtmlAttributes = null;

        //post additional values        
        public IDictionary<string, string> PostValuesWithUpload { get; set; } 



        public string UploadUrl { get { return _uploadUrl; } }
        public string ReturnUrl { get { return _returnUrl??"#"; } }
        public long MaxFileSizeInBytes { get { return _maxFileSizeInBytes; } }
        public string FileTypes { get { return _fileTypes; } }
        public UploadUI UIStyle { get { return _uiStyle; }}
        public bool RenderSharedScript { get { return _includeScriptAndTemplate; } }
        public bool ShowPopUpClose
        {
            get { return "link".Equals(_renderType) && _returnUrl != null; }
        } 


        public MvcFileUploadModel(HtmlHelper helper)
        {
            _helper = helper;
            PostValuesWithUpload=new Dictionary<string, string>();
        }              

       
        public IMvcFileUpload UploadAt(string url)
        {
            _uploadUrl = url;

            return this;
        }

        public IMvcFileUpload ReturnAt(string url)
        {
            _returnUrl = url;

            return this;
        }

        public IMvcFileUpload WithFileTypes(string fileTypes)
        {
            _fileTypes = fileTypes;

            return this;
        }

        public IMvcFileUpload WithMaxFileSize(long sizeInBytes)
        {
            _maxFileSizeInBytes = sizeInBytes;

            return this;
        }

        public IMvcFileUpload AddFormField(string fieldName, string fieldValue)
        {
            PostValuesWithUpload.Add(fieldName, fieldValue);
            return this;
        }


        /// <summary>
        /// Excludes the shared script. 
        /// Should be called when rendering two inline upload widget
        /// </summary>
        /// <returns></returns>
        public IMvcFileUpload ExcludeSharedScript()
        {
            _includeScriptAndTemplate = false;

            return this;
        }

        public IHtmlString RenderInline()
        {
            _renderType = "inline";
            return _helper.Partial(_template, this);
        }

        public IHtmlString RenderInline(string template)
        {
            _template = template;
            return RenderInline();
        }

        public IHtmlString RenderPopup(string labelTxt, string dataTarget, object htmlAttributes = null)
        {
            _includeScriptAndTemplate = true;
            _renderType = "link";
            _popupLabelTxt = labelTxt;
            _popupElementHtmlAttributes = htmlAttributes;

            
            var tag = new TagBuilder("a");
            var urlHelper = new UrlHelper(_helper.ViewContext.RequestContext);
            var linkUrl = urlHelper.Action("UploadPopup", "MvcFileUpload", new {
                                                                                    UploadUrl = UploadUrl,
                                                                                    ReturnUrl = ReturnUrl,
                                                                                    MaxFileSizeInBytes = MaxFileSizeInBytes,
                                                                                    FileTypes = FileTypes
                                                                                });

            int i = 0;
            foreach (var postVal in PostValuesWithUpload)
            {                    
                linkUrl += String.Format("&PostValuesWithUpload[{0}].Key={1}", i, HttpUtility.UrlEncode(postVal.Key));
                linkUrl += String.Format("&PostValuesWithUpload[{0}].Value={1}", i, HttpUtility.UrlEncode(postVal.Value));
                i++;
            }
            


            tag.Attributes.Add("href", linkUrl);

            tag.Attributes.Add("role", "button");
            tag.Attributes.Add("data-toggle", "modal");
            tag.Attributes.Add("data-target", dataTarget);


            tag.InnerHtml = _popupLabelTxt;

            if (htmlAttributes != null)
                htmlAttributes.GetType().GetProperties().ToList().ForEach(p => tag.Attributes.Add(p.Name, p.GetValue(htmlAttributes, null).ToString()));

            return new MvcHtmlString(tag.ToString());
        }
    }
}