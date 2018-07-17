using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.TheStage.CompanyIntroduction
{
    public partial class CompanyDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //    ~/Files/CompanyIntroduction/
            string path = "/Files/CompanyIntroduction/";
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~" + path));


            FileInfo[] currentFileInfo = dirInfo.GetFiles("*", SearchOption.AllDirectories);
            rptLists.DataSource = currentFileInfo;
            rptLists.DataBind();
            
            //   string[] files = DirectoryInfo.GetFiles(Server.MapPath(path));
            
        }
    }
}