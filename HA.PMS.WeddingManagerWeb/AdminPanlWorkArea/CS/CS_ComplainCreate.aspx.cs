/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.24
 Description:投诉意见添加页面
 History:修改日志

 Author:杨洋
 date:2013.3.20
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
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_ComplainCreate : SystemPage
    {
        Complain objComplainBLL = new Complain();
        Customers objCustomersBLL = new Customers();
        HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                phContent.Visible = false;
                int ComplainID = Request.QueryString["ComplainID"].ToInt32();


                //当前传入的url参数进行判断是否是要进行处理投诉
                if (ComplainID != 0)
                {
                    int emlployeeID = User.Identity.Name.ToInt32();

                    CS_Complain comlain = objComplainBLL.GetByID(ComplainID);
                    Employee ObjEmployeeBLL=new Employee();
                    if (emlployeeID == comlain.ComplainEmployeeId)
                    {
                        ddlOrderEmployee.Items.Clear();
                        ddlOrderEmployee.Items.Add(new ListItem(ObjEmployeeBLL.GetByID(emlployeeID).EmployeeName, emlployeeID.ToString()));
                        ddlOrderEmployee.Items[0].Selected = true;
                    }
                    //如果处理人不是当前登陆人，就没有处理权限
                    if (emlployeeID != comlain.ComplainEmployeeId)
                    {
                        foreach (var item in phContent.Controls)
                        {

                            if (item is TextBox)
                            {
                                TextBox txtBox = item as TextBox;
                                txtBox.Enabled = false;
                            }
                            if (item is Button)
                            {
                                Button btn = item as Button;
                                btn.Enabled = false;
                            }

                        }
                        txtReturnContent.Enabled = false;
                        btnExcute.Enabled = false;

                    }
                    //如果是，处理人，也只能添加对该新人的处理结果，其他的就不能进行操作了
                    hfCustomers.Value = comlain.CustomerID + string.Empty;


                    txtComplainContent.Text = comlain.ComplainContent;
                    txtComplainContent.Enabled = false;
                    txtComplainDate.Text = comlain.ComplainDate + string.Empty;
                    txtComplainDate.Enabled = false;
                    txtComplainRemark.Text = comlain.ComplainRemark;
                    txtComplainRemark.Enabled = false;
                    //txtChoosePerson.Text = GetEmployeeName(comlain.ComplainEmployeeId);
                    //txtChoosePerson.Enabled = false;
                    //hiddeEmpLoyeeID.Value = comlain.ComplainEmployeeId + string.Empty;
                    //phChoose.Visible = false;
                    //保存投诉区域隐藏

                    phSaveComplainContent.Visible = false;

                    //投诉处理区域显示
                    phExcuteComplainResult.Visible = true;
                    //搜素新人隐藏
                    phSearch.Visible = false;
                    var singerResult = objCustomersBLL.GetByID(comlain.CustomerID);
                    if (singerResult != null)
                    {
                        phContent.Visible = true;
                        int customerId = singerResult.CustomerID;
                        hfCustomers.Value = customerId + string.Empty;
                        ltlName.Text = singerResult.Bride;
                        ltlPartyDate.Text = GetDateStr(singerResult.PartyDate) + "  " + singerResult.TimeSpans;
                        ltlWineshop.Text = singerResult.Wineshop;
                        ltlCelPhone.Text = singerResult.BrideCellPhone;
                        FL_Invite invites = objInviteBLL.GetByCustomerID(customerId);
                        if (invites != null)
                        {

                            ltlInvitePerson.Text = GetEmployeeName(invites.EmpLoyeeID);

                        }
                        ltlAdviser.Text = GetOrderEmpLoyeeNameByCustomerID(customerId);
                        ltlProgrammer.Text = GetPlannerNameByCustomersId(customerId);





                    }
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            CS_Complain cs_Complain = new CS_Complain();
            cs_Complain.ComplainRemark = txtComplainRemark.Text;
            cs_Complain.CustomerID = hfCustomers.Value.ToInt32();
            cs_Complain.ComplainDate = txtComplainDate.Text.ToDateTime();
            cs_Complain.ComplainContent = txtComplainContent.Text;
            cs_Complain.ComplainEmployeeId = ddlOrderEmployee.SelectedValue.ToInt32();// hiddeEmpLoyeeID.Value.ToInt32();
            cs_Complain.ReturnContent = string.Empty;
            cs_Complain.ReturnDate = DateTime.Now;
            cs_Complain.IsDelete = false;

            if (cs_Complain.ComplainEmployeeId == 0)
            {
                JavaScriptTools.AlertWindow("请你选择处理人", this.Page);
            }
            else
            {
                int result = objComplainBLL.Insert(cs_Complain);
                //根据返回判断添加的状态
                if (result > 0)
                {

                    FL_Message ObjMessageModel = new FL_Message();
                    Message objMessageBLL = new Message();
                    Employee ObjEmployeeBLL = new Employee();
                    ObjMessageModel.EmployeeID = ddlOrderEmployee.SelectedValue.ToInt32();

                    ObjMessageModel.MissionID = 0;
                    ObjMessageModel.IsDelete = false;
                    ObjMessageModel.IsLook = false;
                    ObjMessageModel.Message = "你被" + objComplainBLL.GetByID(cs_Complain.CustomerID) + "投诉了,请在投诉管理模块中处理 录入时间为：" + DateTime.Now;
                    ObjMessageModel.MessAgeTitle = "你被" + objComplainBLL.GetByID(cs_Complain.CustomerID) + "投诉了,请在投诉管理模块中处理";
           
                    ObjMessageModel.CreateEmployeename = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                    objMessageBLL.Insert(ObjMessageModel);


                    ///发消息给他的上级
                    ObjMessageModel = new FL_Message();
                    ObjMessageModel.EmployeeID =ObjEmployeeBLL.GetMineCheckEmployeeID(ddlOrderEmployee.SelectedValue.ToInt32());

                    ObjMessageModel.MissionID = 0;
                    ObjMessageModel.IsDelete = false;
                    ObjMessageModel.IsLook = false;
                    ObjMessageModel.Message = "你的下属被" + objComplainBLL.GetByID(cs_Complain.CustomerID) + "投诉了,请在投诉管理模块中处理 录入时间为：" + DateTime.Now;
                    ObjMessageModel.MessAgeTitle = "你的下属被" + objComplainBLL.GetByID(cs_Complain.CustomerID) + "投诉了,请在投诉管理模块中处理";

                    ObjMessageModel.CreateEmployeename = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                    objMessageBLL.Insert(ObjMessageModel);
                    JavaScriptTools.AlertWindowAndLocation("操作成功", "CS_ComplainManager.aspx?NeedPopu=1", this.Page);
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

                }
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {

            phContent.Visible = true;
            phSearch.Visible = false;
            var query = objCustomersBLL.GetByAll();
            string strubg = txtCustomer.Text;
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                if (query != null)
                {
                    query = query.Where(C => C.Groom == txtCustomer.Text.Trim() || C.Bride == txtCustomer.Text.Trim()).ToList();
                }
            }
            if (!string.IsNullOrEmpty(txtPartyDate.Text))
            {
                if (query != null)
                {
                    query = query.Where(C => C.PartyDate == txtPartyDate.Text.ToDateTime()).ToList();
                }

            }
            if (!string.IsNullOrEmpty(txtWineShop.Text))
            {
                if (query != null)
                {
                    query = query.Where(C => C.Wineshop == txtWineShop.Text.Trim()).ToList();
                }

            }

            if (!string.IsNullOrEmpty(txtCelPhone.Text))
            {
                if (query != null)
                {
                    query = query.Where(C => C.GroomCellPhone == txtCelPhone.Text.Trim() || C.BrideCellPhone == txtCelPhone.Text.Trim()).ToList();
                }

            }

            var singerResult = query.FirstOrDefault();


            if (singerResult != null)
            {

                ddlOrderEmployee.BinderByCustomerID(singerResult.CustomerID);
                

                int customerId = singerResult.CustomerID;
                var excuteCustomer = objComplainBLL.GetByAll().Where(C => C.CustomerID == customerId);
 
                //证明该客户已经被处理了
                if (excuteCustomer.Count() != 0)
                {
                     JavaScriptTools.AlertWindow("该用户已在处理记录当中。", this.Page);
                     phContent.Visible = false;
                     phSearch.Visible = true;
                }
                else
                {


                    hfCustomers.Value = customerId + string.Empty;
                    ltlName.Text = singerResult.Bride;
                    ltlPartyDate.Text = GetDateStr(singerResult.PartyDate) + "  " + singerResult.TimeSpans;
                    ltlWineshop.Text = singerResult.Wineshop;
                    ltlCelPhone.Text = singerResult.BrideCellPhone;
                    FL_Invite invites = objInviteBLL.GetByCustomerID(customerId);
                    if (invites != null)
                    {

                        ltlInvitePerson.Text = GetEmployeeName(invites.EmpLoyeeID);

                    }
                    ltlAdviser.Text = GetOrderEmpLoyeeNameByCustomerID(customerId);
                    ltlProgrammer.Text = GetPlannerNameByCustomersId(customerId);
                }
            }
            else
            {
                phContent.Visible = false;
                phSearch.Visible = true;
                JavaScriptTools.AlertWindow("暂时没有查找该用户", this.Page);
            }
        }
        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExcute_Click(object sender, EventArgs e)
        {
            int ComplainID = Request.QueryString["ComplainID"].ToInt32();
            CS_Complain comlain = objComplainBLL.GetByID(ComplainID);
            comlain.ReturnContent = txtReturnContent.Text;
            int result = objComplainBLL.Update(comlain);
            if (result > 0)
            {
                JavaScriptTools.AlertWindowAndLocation("操作成功", "CS_ComplainManager.aspx?NeedPopu=1", this.Page);
            }
        }
    }
}