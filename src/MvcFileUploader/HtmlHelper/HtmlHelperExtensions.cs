/*
 * MvcFileUploader utility
 * https://github.com/marufbd/MvcFileUploader
 *
 * Copyright 2015, Maruf Rahman
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

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
