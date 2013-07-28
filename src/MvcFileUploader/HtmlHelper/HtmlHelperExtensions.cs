using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcFileUploader.Models;

namespace MvcFileUploader.HtmlHelper
{
    public static class HtmlHelperExtensions
    {
        public static IMvcFileUploadModelBuilder MvcFileUpload<TModel>(this HtmlHelper<TModel> helper)
        {
            return new MvcFileUploadModelBuilder(helper);
        }
    }
}
