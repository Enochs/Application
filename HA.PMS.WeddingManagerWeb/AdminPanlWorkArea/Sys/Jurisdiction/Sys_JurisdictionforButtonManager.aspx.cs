using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction
{
    public partial class Sys_JurisdictionforButtonManager : SystemPage
    {
        Controls ObjControlsBLL = new Controls();
        Channel ObjChannel = new Channel();
        JurisdictionforButton ObjJurisdictionforButtonBLL = new JurisdictionforButton();
        UserJurisdiction ObjUserJurisdictionBll = new UserJurisdiction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化绑定信息
                DataBinder();
            }
        }
        /// <summary>
        /// 加载控制信息绑定Repeater控件
        /// </summary>
        private void DataBinder()
        {

            RepControlList.DataSource = ObjControlsBLL.GetByChannel(Request["ChannelID"].ToInt32());
            RepControlList.DataBind();
        }

        /// <summary>
        /// 获取模块名称
        /// </summary>
        /// <returns></returns>
        public string GetChannelNameByID()
        {
            return ObjChannel.GetByID(Request["ChannelID"].ToInt32()).ChannelName;
        }

        /// <summary>
        /// 保存用户的控件授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            CheckBox ObjCheck = new CheckBox();
            HiddenField ObjHide = new HiddenField();
            for (int i = 0; i<RepControlList.Items.Count; i++)
            {
                ObjCheck=(CheckBox)RepControlList.Items[i].FindControl("chkControl");
     
                    ObjHide = (HiddenField)RepControlList.Items[i].FindControl("hideKey");
                    Sys_JurisdictionforButton ObjButtonJs = new Sys_JurisdictionforButton();
                    ObjButtonJs.ChannelID = Request["ChannelID"].ToInt32();
                    ObjButtonJs.ControlID = ObjHide.Value.ToInt32();
                    ObjButtonJs.IsClose = !ObjCheck.Checked;
                    ObjButtonJs.JurisdictionID = ObjUserJurisdictionBll.GetUserJurisdictionByChannelandEmpLoyee(Request["EmployeeID"].ToInt32(), ObjButtonJs.ChannelID).JurisdictionID;
                    ObjJurisdictionforButtonBLL.Insert(ObjButtonJs);
            }
        }
    }
}