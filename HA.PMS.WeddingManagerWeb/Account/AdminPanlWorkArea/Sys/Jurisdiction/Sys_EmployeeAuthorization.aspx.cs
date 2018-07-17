using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction
{
    public partial class Sys_EmployeeAuthorization : SystemPage
    {
        Channel ObjChannelBLL = new Channel();
        UserJurisdiction ObjUserJurisdBLL = new UserJurisdiction();
        Employee ObjEmpLoyeeBLL = new Employee();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        /// <summary>
        /// 隐藏或者显示控件权限分派
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        public string HideOrShow(object ChannelID)
        {
            if (ObjUserJurisdBLL.GetUserJurisdictionByChannel(Request["EmployeeID"].ToInt32(), ChannelID.ToString().ToInt32()))
            {

                return string.Empty;
            }
            else
            {
                return "style=\"display:none;\"";
            }
        }


        /// <summary>
        /// 绑定栏目菜单
        /// </summary>
        private void BinderData()
        {

            this.RepChannelList.DataSource = ObjUserJurisdBLL.GetEmPloyeeChannel(Request["employeeId"].ToInt32(), 0).OrderBy(C=>C.OrderCode);
            this.RepChannelList.DataBind();
        }


        /// <summary>
        /// 绑定一级栏目下的二级菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepChannelList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater ObjRepeater = (Repeater)e.Item.FindControl("repSecondTree");
            HiddenField ObjHide = (HiddenField)e.Item.FindControl("hideChannel");
            CheckBox chkChannel = (CheckBox)e.Item.FindControl("chkChannel");
            var ObjUserJurisdMdeol = ObjUserJurisdBLL.GetUserJurisdictionByChannelandEmpLoyee(Request["employeeId"].ToInt32(), ObjHide.Value.ToInt32());
            if (ObjUserJurisdMdeol == null)
            {
                chkChannel.Checked = false;
            }
            else
            {
                if (!ObjUserJurisdMdeol.IsClose == true)
                {
                    chkChannel.Checked = true;
                }
            }
            if (ObjRepeater != null && ObjHide != null)
            {
                ObjRepeater.DataSource = ObjUserJurisdBLL.GetEmPloyeeChannel(Request["employeeId"].ToInt32(), ObjHide.Value.ToInt32());
                ObjRepeater.DataBind();
            }
        }



        /// <summary>
        /// 保存用户频道权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateDate_Click(object sender, EventArgs e)
        {
            UserJurisdiction ObjUserJurisdictionBll = new UserJurisdiction();
            Sys_UserJurisdiction ObjModel = new Sys_UserJurisdiction();

            //循环大类
            for (int i = 0; i < RepChannelList.Items.Count; i++)
            {
                CheckBox ObjCheck = (CheckBox)RepChannelList.Items[i].FindControl("chkChannel");
                Repeater ObjRepeater = (Repeater)RepChannelList.Items[i].FindControl("repSecondTree");
                HiddenField ObjFirstHide = (HiddenField)RepChannelList.Items[i].FindControl("hideChannel");
                if (ObjCheck.Checked)
                {

                    if (ObjRepeater.Items.Count > 0)
                    {
                        //循环二级菜单
                        for (int S = 0; S < ObjRepeater.Items.Count; S++)
                        {
                            CheckBox ObjChkSecond = (CheckBox)ObjRepeater.Items[S].FindControl("ChkSecond");
                            HiddenField ObjSeHide = (HiddenField)ObjRepeater.Items[S].FindControl("hideScondChannel");
                            DropDownList ObjDataPower = (DropDownList)ObjRepeater.Items[S].FindControl("ddlDataPower"); 
                            HiddenField ObjChecksEmployee = (HiddenField)ObjRepeater.Items[S].FindControl("hideChecksEmployee");
                            HiddenField ObjDispatching = (HiddenField)ObjRepeater.Items[S].FindControl("hideDispatching");
                            //如果选择 则把IsClose设置为false 表示打开权限
                            if (ObjChkSecond.Checked)
                            {

                                ObjModel = new Sys_UserJurisdiction();
                                ObjModel.EmployeeID = Request["EmployeeID"].ToInt32();
                                ObjModel.ChannelID = ObjSeHide.Value.ToInt32();
                                ObjModel.DataPower = ObjDataPower.SelectedValue.ToInt32();
                                ObjModel.DepartmentID = ObjEmpLoyeeBLL.GetByID(ObjModel.EmployeeID).DepartmentID;
                                ObjModel.DataPowerMd5Key = ObjSeHide.Value + "" + ObjModel.EmployeeID + "" + ObjModel.DataPower + "".Md5forDataRow();
                                ObjModel.IsClose = false;
                                if (ObjChecksEmployee.Value.Trim().ToInt32()>0)
                                {
                                    ObjModel.ChecksEmployee = ObjChecksEmployee.Value.ToInt32();
                                }
                                if (ObjDispatching.Value.Trim().ToInt32() > 0)
                                {
                                    ObjModel.Dispatching = ObjDispatching.Value.ToInt32();
                                }
                                ObjUserJurisdictionBll.Insert(ObjModel);
                            }
                            else
                            {
                                //否则关闭权限
                                ObjModel = new Sys_UserJurisdiction();
                                ObjModel.EmployeeID = Request["EmployeeID"].ToInt32();
                                ObjModel.ChannelID = ObjSeHide.Value.ToInt32();
                                ObjModel.DataPower = ObjDataPower.SelectedValue.ToInt32();
                                ObjModel.DepartmentID = ObjEmpLoyeeBLL.GetByID(ObjModel.EmployeeID).DepartmentID;
                                ObjModel.IsClose = true;
                                if (ObjChecksEmployee.Value.Trim() != string.Empty)
                                {
                                    ObjModel.ChecksEmployee = ObjChecksEmployee.Value.ToInt32();
                                }
                                if (ObjDispatching.Value.Trim() != string.Empty)
                                {
                                    ObjModel.Dispatching = ObjDispatching.Value.ToInt32();
                                }
                                ObjModel.DataPowerMd5Key = ObjSeHide.Value + "" + ObjModel.EmployeeID + "" + ObjModel.DataPower + "".Md5forDataRow();
                                ObjUserJurisdictionBll.Insert(ObjModel);
                            }
                        }
                    }
                    //如果选择 则把IsClose设置为false 表示打开权限
                    ObjModel = new Sys_UserJurisdiction();
                    ObjModel.EmployeeID = Request["EmployeeID"].ToInt32();
                    ObjModel.ChannelID = ObjFirstHide.Value.ToInt32();
                    ObjModel.DataPower = -1;
                    ObjModel.DepartmentID = ObjEmpLoyeeBLL.GetByID(ObjModel.EmployeeID).DepartmentID;
                    ObjModel.DataPowerMd5Key = ObjFirstHide.Value + "" + ObjModel.EmployeeID + "" + ObjModel.DataPower + "".Md5forDataRow();
                    ObjModel.IsClose = false;
                    ObjUserJurisdictionBll.Insert(ObjModel);
                }
                else
                {
                    //如果选择 则把IsClose设置为false 表示打开权限
                    ObjModel = new Sys_UserJurisdiction();
                    ObjModel.EmployeeID = Request["EmployeeID"].ToInt32();
                    ObjModel.ChannelID = ObjFirstHide.Value.ToInt32();
                    ObjModel.DataPower = -1;
                    ObjModel.DepartmentID = ObjEmpLoyeeBLL.GetByID(ObjModel.EmployeeID).DepartmentID;
                    ObjModel.IsClose = true;
                    ObjModel.DataPowerMd5Key = ObjFirstHide.Value + "" + ObjModel.EmployeeID + "" + ObjModel.DataPower + "".Md5forDataRow();
                    ObjUserJurisdictionBll.Insert(ObjModel);
                }

            }
            JavaScriptTools.AlertAndClosefancybox("保存成功!",Page);
        }


        public string GetEmpLoyeeNameByID(object ID)
        {
            if (ID != null)
            {
                return ObjEmpLoyeeBLL.GetByID(ID.ToString().ToInt32()).EmployeeName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 绑定二级菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repSecondTree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CheckBox chkChannel = (CheckBox)e.Item.FindControl("ChkSecond");
            HiddenField ObjHide = (HiddenField)e.Item.FindControl("hideScondChannel");
            var ObjUserJurisdMdeol = ObjUserJurisdBLL.GetUserJurisdictionByChannelandEmpLoyee(Request["employeeId"].ToInt32(), ObjHide.Value.ToInt32());
            try
            {
                if (!ObjUserJurisdMdeol.IsClose == true)
                {
                    chkChannel.Checked = true;
                }
            }
            catch
            {
                chkChannel.Checked = false;
            }
        }
    }
}