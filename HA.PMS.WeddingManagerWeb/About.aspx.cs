/**
 Version :版本号
 File Name :文件名称
 Author:作者
 Date:生成日期
 Description:模块目的
 History:修改日志
 
 Author:修改人
 date:修改日期
 version:版本号
 description:修改描述
 
 
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.WeddingManagerWeb
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

          //Response.Write("3333".GetStringByIndex(3)+",");
            Response.Redirect("~/Account/login.html");
           
        }
    }
} 