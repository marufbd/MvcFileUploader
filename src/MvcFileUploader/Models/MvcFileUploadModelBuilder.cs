/*
 * MvcFileUploader utility
 * https://github.com/marufbd/MvcFileUploader
 *
 * Copyright 2015, Maruf Rahman
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace MvcFileUploader.Models
{
    using System.Web.Mvc;

    public class MvcFileUploadModelBuilder : IMvcFileUploadModelBuilder
    {
        private readonly HtmlHelper _helper;

        private string _returnUrl = "#";
        private string _fileTypes = @"/(\.|\/)(.+)$/i"; // jquery-image-upload format expression string
        private long _maxFileSizeInBytes = 10485760; // in bytes, default 10MB
        private int? _maxNoOfFiles = null;
        private string _uploadUrl = "/upload"; 
        private string _renderType = "link";
        private string _template = "_MvcFileUpload";
        private bool _disableImagePreview = false;

        private bool _includeScriptAndTemplate = true;


        private UploadUI _uiStyle = UploadUI.Bootstrap;
        private string _popupLabelTxt="Upload"; 

        //post additional values        
        private readonly IDictionary<string, string> _postValuesWithUpload=new Dictionary<string, string>();


        private FileUploadViewModel GetViewModel()
        {
            return new FileUploadViewModel()
                       {
                           FileTypes = _fileTypes,
                           MaxFileSizeInBytes = _maxFileSizeInBytes,
                           MaxNumberOfFiles = _maxNoOfFiles,
                           DisableImagePreview = _disableImagePreview,
                           UploadUrl = _uploadUrl,
                           UIStyle = _uiStyle,
                           ReturnUrl = _returnUrl??"#",
                           RenderSharedScript = _includeScriptAndTemplate,
                           PostValuesWithUpload = _postValuesWithUpload                           
                       };
        }

        private dynamic GetUrlPostModel()
        {
            return new 
            {
                FileTypes = _fileTypes,
                MaxFileSizeInBytes = _maxFileSizeInBytes,
                MaxNumberOfFiles=_maxNoOfFiles,
                DisableImagePreview=_disableImagePreview,
                UploadUrl = _uploadUrl,
                UIStyle = _uiStyle,
                ReturnUrl = _returnUrl ?? "#",
                RenderSharedScript = _includeScriptAndTemplate,                
                ShowPopUpClose = "link".Equals(_renderType) && _returnUrl != null
            };
        }
        
        public MvcFileUploadModelBuilder(HtmlHelper helper)
        {
            _helper = helper; 
        }


        public IMvcFileUploadModelBuilder UploadAt(string url)
        {
            _uploadUrl = url;

            return this;
        }

        public IMvcFileUploadModelBuilder ReturnAt(string url)
        {
            _returnUrl = url;

            return this;
        }

        public IMvcFileUploadModelBuilder WithFileTypes(string fileTypes)
        {
            _fileTypes = fileTypes;

            return this;
        }

        public IMvcFileUploadModelBuilder WithMaxFileSize(long sizeInBytes)
        {
            _maxFileSizeInBytes = sizeInBytes;

            return this;
        }

        public IMvcFileUploadModelBuilder MaxNumberOfFiles(int maxNoOfFiles)
        {
            _maxNoOfFiles = maxNoOfFiles;

            return this;
        }

        public IMvcFileUploadModelBuilder DisableImagePreview()
        {
            _disableImagePreview = true;

            return this;
        }

        public IMvcFileUploadModelBuilder AddFormField(string fieldName, string fieldValue)
        {
            _postValuesWithUpload.Add(fieldName, fieldValue);
            return this;
        }


        public IMvcFileUploadModelBuilder Template(string template)
        {
            _template = template;
            return this;
        }

        public IMvcFileUploadModelBuilder UIStyle(UploadUI ui)
        {
            _uiStyle = ui;
            return this;
        }

        /// <summary>
        /// Excludes the shared script. 
        /// Should be called when rendering two inline upload widget
        /// </summary>
        /// <returns></returns>
        public IMvcFileUploadModelBuilder ExcludeSharedScript()
        {
            _includeScriptAndTemplate = false;

            return this;
        }





        //rendering
        public IHtmlString RenderInline()
        {
            _renderType = "inline";
            return _helper.Partial(_template, GetViewModel());
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

            
            var tag = new TagBuilder("a");
            var urlHelper = new UrlHelper(_helper.ViewContext.RequestContext);
                        
            var linkUrl = urlHelper.Action("UploadDialog", "MvcFileUpload", GetUrlPostModel());
            
            //binding the dictionary with post
            if(_postValuesWithUpload.Count>0)
            {
                int idx = 0;
                foreach (var postVal in _postValuesWithUpload)
                {
                    linkUrl += String.Format("&postValues{1}.Key={0}", HttpUtility.UrlEncode(postVal.Key), HttpUtility.UrlEncode("[" + idx + "]"));
                    linkUrl += String.Format("&postValues{1}.Value={0}", HttpUtility.UrlEncode(postVal.Value), HttpUtility.UrlEncode("[" + idx + "]"));
                    idx++;
                } 
            }
            else
            {
                linkUrl += String.Format("&postValues[0].Key=NoKeys&postValues[0].Value=");
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