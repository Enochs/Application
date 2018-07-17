/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.16
 Description:新人一周内记录
 History:修改日志
 
 Author:杨洋
 date:2013.4.16
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
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.EditoerLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit
{
    public partial class FL_CustomerReturnVisitManagerTswk :SystemPage
    {
        //订单
        Order objOrderBLL = new Order();

        //客户
        Customers objCustomenrBLL = new Customers();

        //用户
        Employee objEmployeeBLL = new Employee();

        //回访
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }


        
   

        /// <summary>
        /// 绑定一周内的回访
        /// </summary>
        protected void DataBinder()
        {

            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            ObjParameterList.Add(new ObjectParameter("ReurnState", 1));
            if (!string.IsNullOrEmpty(txtGroom.Text))
            {
                ObjParameterList.Add(new ObjectParameter("Bride_LIKE", txtGroom.Text));
            }
            if (!string.IsNullOrEmpty(txtGroomCellPhone.Text))
            {
                ObjParameterList.Add(new ObjectParameter("BrideCellPhone", txtGroomCellPhone.Text));
            }
            if (!string.IsNullOrEmpty(txtPartDateStar.Text) && !string.IsNullOrEmpty(txtPartDateEnd.Text))
            {
                ObjParameterList.Add(new ObjectParameter("PartyDate_between", txtPartDateStar.Text + "," + txtPartDateEnd.Text));

            }

            //if (ddlHotel.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("Wineshop", ddlHotel.SelectedItem.Text));
            //}

            #region 分页页码
            int startIndex = ReturnPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objCustomerReturnVisitBLL.GetbyCustomerReturnVisitParameter(ObjParameterList.ToArray(), ReturnPager.PageSize, ReturnPager.CurrentPageIndex, out resourceCount);
            ReturnPager.RecordCount = resourceCount;
            rptReturn.DataSource = query;
            rptReturn.DataBind();

            #endregion
        }

        /// <summary>
        /// 获取预定时间
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderDate(object CustomerID)
        {
            var ObjOrderModel = objOrderBLL.GetbyCustomerID(CustomerID.ToString().ToInt32());
            if (ObjOrderModel != null)
            {
                if (ObjOrderModel.LastFollowDate.HasValue)
                {
                    return ObjOrderModel.LastFollowDate.Value.ToShortDateString();
                }
                return string.Empty;
            }
            else
            {
                return "暂无到店记录";
            }
        }
 
        protected void ReturnPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }



        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptReturn_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                HiddenField hfReturnId = e.Item.FindControl("hfReturnId") as HiddenField;
                CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
                FL_CustomerReturnVisit visit = objCustomerReturnVisitBLL.GetByID(hfReturnId.Value.ToInt32());
                ddlOrderEmployee ddlOrderEmployee = e.Item.FindControl("ddlOrderEmployee") as ddlOrderEmployee;
                TextBox txtReturnDate = e.Item.FindControl("txtReturnDate") as TextBox;
                visit.ReturnDate = txtReturnDate.Text.ToDateTime();
                TextBox txtReturnRecoder = e.Item.FindControl("txtReturnRecoder") as TextBox;
                visit.ReturnRecoder = txtReturnRecoder.Text;
                visit.ReturnEmployee = ddlOrderEmployee.SelectedValue.ToInt32();
                visit.ReurnState = 2;
                visit.ReturnResult = (e.Item.FindControl("ddlReturnResult") as DropDownList).SelectedItem.Text;
                int result = objCustomerReturnVisitBLL.Update(visit);
                if (result > 0)
                {
                    if (visit.ReturnResult == "不满意" || visit.ReturnResult == "一般")
                    {
                        //先发消息给本人
                        FL_Message ObjMessageModel = new FL_Message();
                        Message objMessageBLL = new Message();
                        Employee ObjEmployeeBLL = new Employee();

                        //警告处理
                        WarningMessage ObjWarningMessageBLL = new WarningMessage();
                        ObjMessageModel.EmployeeID = ddlOrderEmployee.SelectedValue.ToInt32();

                        ObjMessageModel.MissionID = 0;
                        ObjMessageModel.IsDelete = false;
                        ObjMessageModel.IsLook = false;
                        ObjMessageModel.Message = "新人不满意 录入时间为：" + DateTime.Now;
                        ObjMessageModel.MessAgeTitle = "回访不满意消息";
                        ObjMessageModel.KeyWords = "/AdminPanlWorkArea/Flows/Customer/ReturnVisit/FL_CustomerReturnVisitManager.aspx?CustomerID=" + visit.CustomerId;
                        ObjMessageModel.CreateEmployeename = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                        objMessageBLL.Insert(ObjMessageModel);
                        //然后对他进行警告

                        //发警告信息给他
    
                      
                        FL_WarningMessage ObjWareMessage = new FL_WarningMessage();
                  
                        ObjWareMessage.CreateDate = DateTime.Now;
                        ObjWareMessage.EmpLoyeeID = ObjMessageModel.EmployeeID;
                        ObjWareMessage.IsLook = false;
                        ObjWareMessage.MessAgeTitle = "新人不满意";
                        ObjWareMessage.ResualtAddress = string.Empty;
                        ObjWareMessage.managerEmployee = ObjEmployeeBLL.GetMineCheckEmployeeID(ObjMessageModel.EmployeeID);
                        ObjWareMessage.FinishKey = visit.ReturnId;
                        ObjWareMessage.State = 3;
                        ObjWareMessage.CustomerID = visit.CustomerId;
                        ObjWarningMessageBLL.Insert(ObjWareMessage);
            
                    
                        //获取上级信息
                        ObjMessageModel = new FL_Message();
                        ObjMessageModel.EmployeeID = objEmployeeBLL.GetMineCheckEmployeeID(User.Identity.Name.ToInt32());

                        ObjMessageModel.MissionID = 0;
                        ObjMessageModel.IsDelete = false;
                        ObjMessageModel.IsLook = false;
                        ObjMessageModel.Message = "新人不满意 录入时间为：" + DateTime.Now;
                        ObjMessageModel.MessAgeTitle = "你的下属：有新人回访不满意";
                        ObjMessageModel.KeyWords = "/AdminPanlWorkArea/Flows/Customer/ReturnVisit/FL_CustomerReturnVisitManager.aspx?CustomerID=" + visit.CustomerId;
                        ObjMessageModel.CreateEmployeename = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                        objMessageBLL.Insert(ObjMessageModel);

                    }
                    JavaScriptTools.AlertWindow("操作成功", this.Page);
                    DataBinder();
                }
            }
        }

        protected void rptReturn_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            GetCustomerReturnVisit current = e.Item.DataItem as GetCustomerReturnVisit;
            DropDownList ddlReturnResult = e.Item.FindControl("ddlReturnResult") as DropDownList;
            ddlOrderEmployee ddlOrderEmployee = e.Item.FindControl("ddlOrderEmployee") as ddlOrderEmployee;
            ddlOrderEmployee.BinderByCustomerID(current.CustomerID);
            
            ListItem listItem= ddlReturnResult.Items.FindByText(current.ReturnResult);
            if (listItem!=null)
            {
                listItem.Selected = true;
            }
            
        }
    }
}