using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HA.PMS.ToolsLibrary
{
    public class CKEditorTool:CKEditor.NET.CKEditorControl
    {
        public CKEditorTool()
        {
            this.BasePath = "~/Scripts/ckeditor/";

            this.Width = Unit.Parse("100%");
            this.Height = 300; 
        }
        //BasePath="~/Scripts/ckeditor/"
    }
}
