using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;

namespace HA.PMS.Pages
{
    /// <summary>
    /// 页面相关工具 如：页面按钮权限查找
    /// </summary>
    public static class SysTemPageTools
    {

        public static void HideControlByUser(int UserID)
        {
 
        }



        /// <summary>
        /// 获取本页访问权限
        /// </summary>
        public static void EndPageByPower(this System.Web.UI.Page ObjPage,string ChannelType)
        {
            UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
            var ObjJurisdictionModel=ObjUserJurisdictionBLL.GetUserJurisdictionByChanneltype(ObjPage.User.Identity.Name.ToInt32(), ChannelType);
            if (ObjJurisdictionModel == null)
            {
                ObjPage.Response.End();
            }
        }
    }
}
