using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcFileUploader.Models
{
    public interface IMvcFileUpload
    {
        //builder methods
        IMvcFileUpload UploadAt(string url);
        IMvcFileUpload ReturnAt(string url);
        IMvcFileUpload WithFileTypes(string fileTypes);
        IMvcFileUpload WithMaxFileSize(long sizeInBytes);

        IMvcFileUpload AddFormField(string fieldName, string fieldValue);

        //render
        IMvcFileUpload ExcludeSharedScript();
        IHtmlString RenderInline();
        IHtmlString RenderInline(string template);
        IHtmlString RenderPopup(string labelTxt, string dataTarget, object htmlAttributes = null);
    }
}
