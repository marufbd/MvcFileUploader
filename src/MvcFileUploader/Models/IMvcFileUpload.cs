using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        IMvcFileUploadModelBuilder AddFormField(string fieldName, string fieldValue);

        //render
        IMvcFileUploadModelBuilder ExcludeSharedScript();
        IHtmlString RenderInline();
        IHtmlString RenderInline(string template);
        IHtmlString RenderPopup(string labelTxt, string dataTarget, object htmlAttributes = null);
    }
}
