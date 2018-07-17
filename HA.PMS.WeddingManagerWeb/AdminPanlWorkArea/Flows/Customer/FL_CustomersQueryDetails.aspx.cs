/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.10
 Description:新人明细表
 History:修改日志

 Author:杨洋
 date:2013.4.10
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer
{
    public partial class FL_CustomersQueryDetails : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.QuotedPrice objQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        Customers objCustomersBLL = new Customers();
        HA.PMS.BLLAssmblly.Flow.Telemarketing objTelemarketingBLL = new BLLAssmblly.Flow.Telemarketing();
        Employee objEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList();
                DataBinder();
            }
        }
       
       
        /// <summary>
        /// 返回要约人姓名
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetEmployeeIdByName(object source)
        {
            int CustomerID = (source + string.Empty).ToInt32();
            FL_Telemarketing mark = objTelemarketingBLL.GetByAll().Where(C => C.CustomerID == CustomerID).FirstOrDefault();
            if (mark != null)
            {
                int empId = mark.EmployeeID.Value;
                Sys_Employee emps = objEmployeeBLL.GetByID(empId);
                if (emps != null)
                {
                    return emps.EmployeeName;
                }
            }
            return "";

        }
        /// <summary>
        /// 通过新人ID，查处对应的邀约次数
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetInviteCount(object source)
        {

            int customersId = (source + string.Empty).ToInt32();
            HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
            FL_Invite invite = objInviteBLL.GetByAll().Where(C => C.CustomerID == customersId).FirstOrDefault();
            if (invite != null)
            {
                InvtieContent objInviteContentBLL = new InvtieContent();
                return objInviteContentBLL.GetByAll().Where(C => C.InviteID == invite.InviteID).Count() + string.Empty;
            }
            return "";
        }
        protected void DataBinder()
        {
            //#region 相关的查询
           
            ////渠道类别
            //customerOrderEmployee.ChannelType = ddlFLChannelType.SelectedValue.ToInt32();

            //customerOrderEmployee.Channel = ddlChannelName.SelectedItem.Text;
            ////客户状态
            //customerOrderEmployee.CustomerStatus = ddlCustomerStatus.SelectedItem.Text;
            //List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            //if (ddlTactcontacts1.SelectedIndex != 0)
            //{
            //    //根据选择的的渠道的Value值，来确定此时用户选择了哪个渠道几的联系人
            //    switch (ddlTactcontacts1.SelectedValue)
            //    {
            //        case "1":
            //            ObjParameterList.Add(new ObjectParameter("Tactcontacts1", ddlTactcontacts1.SelectedItem.Text));
            //            break;
            //        case "2":
            //            ObjParameterList.Add(new ObjectParameter("Tactcontacts2", ddlTactcontacts1.SelectedItem.Text));
            //            break;
            //        case "3":
            //            ObjParameterList.Add(new ObjectParameter("Tactcontacts3", ddlTactcontacts1.SelectedItem.Text));
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //if (ddlFLChannelType.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("ChannelType", customerOrderEmployee.ChannelType));
            //}

            //if (ddlChannelName.SelectedItem.Text != "请选择")
            //{
            //    ObjParameterList.Add(new ObjectParameter("Channel", customerOrderEmployee.Channel));
            //}

            //if (ddlCustomerStatus.SelectedItem.Text != "请选择")
            //{
            //    ObjParameterList.Add(new ObjectParameter("CustomerStatus", customerOrderEmployee.CustomerStatus));
            //}
            ////开始时间
            //DateTime startTime = new DateTime();
            ////如果没有选择结束时间就默认是当前时间
            //DateTime endTime = DateTime.Now;
            ////如果此时选择的时间查询类别不是第一项的话，就根据时间类别进行时间段查询
            //if (ddlTimeChoose.SelectedItem.Text != "请选择")
            //{

            //    if (!string.IsNullOrEmpty(txtStar.Text))
            //    {
            //        startTime = txtStar.Text.ToDateTime();
            //    }

            //    if (!string.IsNullOrEmpty(txtEnd.Text))
            //    {
            //        endTime = txtEnd.Text.ToDateTime();
            //    }

            //    ObjParameterList.Add(new ObjectParameter(ddlTimeChoose.SelectedValue + "_between", startTime + "," + endTime));
            //}


            //#endregion

            //#region 分页页码
            //int startIndex = QueryDetailsPager.StartRecordIndex;
            //int resourceCount = 0;
            //var listResultContent = objCustomersBLL.GetbyFLCustomerOrderEmployeeParameter(ObjParameterList.ToArray(), QueryDetailsPager.PageSize, QueryDetailsPager.CurrentPageIndex, out resourceCount);
            //QueryDetailsPager.RecordCount = resourceCount;

            //rptQueryDetails.DataSource = listResultContent;
            //rptQueryDetails.DataBind();

            //#endregion

        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetChannelTypeName(object source)
        {
            ChannelType objChannelTypeBLL = new ChannelType();
            int ChannelTypeId = (source + string.Empty).ToInt32();
            FD_ChannelType channelType = objChannelTypeBLL.GetByID(ChannelTypeId);

            if (channelType != null)
            {
                return channelType.ChannelTypeName;
            }
            return "";
        }

        protected void DropDownList()
        {

            ////渠道类型
            //ListItem firstChoose = new ListItem("请选择", "0");
            //ddlFLChannelType.Items.Add(firstChoose);
            //ddlFLChannelType.SelectedIndex = ddlFLChannelType.Items.Count - 1;
            ////客户状态
            //var query = objCustomersBLL.GetByAll();
            //ddlCustomerStatus.DataSource = query.Where(C=>!string.IsNullOrEmpty(C.CustomerStatus))
            //    .Distinct(new FL_CustomersCustomerStatusComparer()).Select(C => C.CustomerStatus);
            //ddlCustomerStatus.DataBind();
            //ddlCustomerStatus.Items.Add(firstChoose);
            //ddlCustomerStatus.SelectedIndex = ddlCustomerStatus.Items.Count - 1;
            ////
            //ddlChannelName.DataSource = query.Where(C => !string.IsNullOrEmpty(C.Channel))
            //    .Distinct(new FL_CustomersChannelComparer()).Select(C => C.Channel);
            //ddlChannelName.DataBind();
            //ddlChannelName.Items.Add(firstChoose);

            //#region 添加所有渠道联系人
            //SaleSources objSaleSourcesBLL = new SaleSources();
            //var querySale = objSaleSourcesBLL.GetByAll().Distinct();
            //ListItem Tactcontacts;
            //foreach (var item in querySale)
            //{
            //    Tactcontacts = new ListItem(item.Tactcontacts1, "1");
            //    ddlTactcontacts1.Items.Add(Tactcontacts);
            //    Tactcontacts = new ListItem(item.Tactcontacts2, "2");
            //    ddlTactcontacts1.Items.Add(Tactcontacts);
            //    Tactcontacts = new ListItem(item.Tactcontacts3, "3");
            //    ddlTactcontacts1.Items.Add(Tactcontacts);
            //}
            //ddlTactcontacts1.Items.Add(firstChoose);
            //ddlTactcontacts1.SelectedIndex = ddlTactcontacts1.Items.Count - 1;
            //#endregion




        }


        protected void QueryDetailsPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}