using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Navi.Kernel.Example.Web
{
    public partial class page_example_webexcel : System.Web.UI.Page
    {
        /// <summary>
        /// (WebMethod方法)获取Xls文件内容
        /// </summary>
        /// <param name="as_xlsfilename"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string GetStringExcelContentByFileName(string as_xlsfilename)
        {
            string s_content = string.Empty;
            System.Xml.XmlDocument xmldoc_file = new System.Xml.XmlDocument();

            string s_xlsfilepath = System.Web.HttpContext.Current.Server.MapPath("~/UpLoad/" + as_xlsfilename);            
            xmldoc_file.Load(s_xlsfilepath);
            if (xmldoc_file != null)
            {
                s_content = xmldoc_file.OuterXml;
            }

            return s_content;

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
