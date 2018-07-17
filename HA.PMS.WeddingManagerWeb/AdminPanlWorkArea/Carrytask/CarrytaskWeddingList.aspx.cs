using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using System.Data.Objects;
using System.Web.UI.HtmlControls;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskWeddingList : SystemPage
    {
        //策划报价
        BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        //内部员工
        Employee ObjEmployeeBLL = new Employee();

        //前期设计
        EarlyDesigner ObjEarlyBLL = new EarlyDesigner();


        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary> 
        protected void BinderData(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.Button)
            {
                switch (((System.Web.UI.WebControls.Button)sender).ID.ToLower())
                {
                    case "btnquery": CtrPageIndex.CurrentPageIndex = 1; break;
                }
            }
            List<PMSParameters> objParmList = new List<PMSParameters>();


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;

            objParmList.Add("ParentQuotedID", 0, NSqlTypes.Equal);
            //objParmList.Add("IsChecks", true, NSqlTypes.Equal);
            objParmList.Add("State", "7,10,20,29", NSqlTypes.NotIN);

            //新人姓名
            CstmNameSelector.AppandTo(objParmList);
            ////婚期
            if (PartyDateRanger.IsNotBothEmpty)
            {
                objParmList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            ////酒店
            if (ddlHotel.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.StringEquals);
            }
            //objParmList.Add("MissionManagerEmployee,EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.ColumnOr);
            MyManager.GetEmployeePar(objParmList, "EmployeeID");

            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }


            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            repCustomer.DataBind(DataList);
            CtrPageIndex.RecordCount = SourceCount;
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>       
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            HiddenField ObjEmpLoyeeHide = null;
            HiddenField ObjCustomerHide = null;
            HiddenField ObjOrderHide = null;
            int EmployeeID = 0;
            for (int i = 0; i < repCustomer.Items.Count; i++)
            {
                ObjEmpLoyeeHide = (HiddenField)repCustomer.Items[i].FindControl("hideEmpLoyeeID");
                ObjCustomerHide = (HiddenField)repCustomer.Items[i].FindControl("hideCustomerHide");
                ObjOrderHide = (HiddenField)repCustomer.Items[i].FindControl("HideOrderID");
                int CustomerId = ObjCustomerHide.Value.ToInt32();
                FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(ObjCustomerHide.Value.ToInt32());
                TextBox txtSaveDate = repCustomer.Items[i].FindControl("txtPlanDate") as TextBox;

                if (txtSaveDate.Text == "" && ObjEmpLoyeeHide.Value != "")      //必须同时选择设计师和计划完成时间
                {
                    JavaScriptTools.AlertWindow("请选择计划完成时间", Page);
                    return;
                }
                else if (txtSaveDate.Text != "" && ObjEmpLoyeeHide.Value == "")
                {
                    TextBox txtEmployeeName = repCustomer.Items[i].FindControl("txtEmployees") as TextBox;
                    if (txtEmployeeName.Text == "")
                    {
                        JavaScriptTools.AlertWindow("请选择设计师", Page);
                        return;
                    }
                    if (ObjQuotedPriceModel.PlanFinishDate != null && ObjQuotedPriceModel.PlanFinishDate != txtSaveDate.Text.ToString().ToDateTime())
                    {//修改时间
                        ObjQuotedPriceModel.PlanFinishDate = txtSaveDate.Text.ToString().ToDateTime();      //保存计划完成时间
                        ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
                    }
                    if (txtEmployeeName.Text != "" || txtSaveDate.Text != "")
                    {
                        ObjQuotedPriceModel.PlanFinishDate = txtSaveDate.Text.ToString().ToDateTime();      //保存计划完成时间
                        ObjQuotedPriceModel.DesignerEmployee = ObjEmployeeBLL.GetByName(txtEmployeeName.Text.ToString()).EmployeeID;
                        //ObjQuotedPriceModel.DesignerState = 1;      //已派设计师  (状态)
                        ObjQuotedPriceModel.IsReceive = 0;
                        ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
                        EmployeeID = ObjQuotedPriceModel.DesignerEmployee.ToString().ToInt32();
                        MissionManager ObjMissManagerBLL = new MissionManager();

                        ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedPriceModel.CustomerID.ToString().ToInt32(), 1, (int)MissionTypes.Design, DateTime.Now, EmployeeID, "?OrderID=" + ObjOrderHide.Value + "&IsFinish=4&QuotedID=" + ObjQuotedPriceModel.QuotedID + "&CustomerID=" + ObjQuotedPriceModel.CustomerID.ToString().ToInt32() + "&Type=1", MissionChannel.Quoted, User.Identity.Name.ToInt32(), ObjOrderHide.Value.ToInt32(), "", "", "", "设计单制作");
                    }
                }

                if (ObjEmpLoyeeHide.Value.ToInt32() != 0 && txtSaveDate.Text != "")
                {
                    ObjQuotedPriceModel.PlanFinishDate = txtSaveDate.Text.ToString().ToDateTime();      //保存计划完成时间
                    ObjQuotedPriceModel.DesignerEmployee = ObjEmpLoyeeHide.Value.ToInt32();
                    //ObjQuotedPriceModel.DesignerState = 1;      //已派设计师  (状态)
                    ObjQuotedPriceModel.IsReceive = 0;
                    ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
                    EmployeeID = ObjQuotedPriceModel.DesignerEmployee.ToString().ToInt32();
                    MissionManager ObjMissManagerBLL = new MissionManager();

                    ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedPriceModel.CustomerID.ToString().ToInt32(), 1, (int)MissionTypes.Design, DateTime.Now, EmployeeID, "?OrderID=" + ObjOrderHide.Value + "&IsFinish=4&QuotedID=" + ObjQuotedPriceModel.QuotedID + "&CustomerID=" + ObjQuotedPriceModel.CustomerID.ToString().ToInt32() + "&Type=1", MissionChannel.Quoted, User.Identity.Name.ToInt32(), ObjOrderHide.Value.ToInt32(), "", "", "", "设计单制作");
                }


            }

            BinderData(sender, e);
            JavaScriptTools.AlertWindow("保存完毕", Page);

        }
        #endregion

        #region 派给其他人
        /// <summary>
        /// 派给其他人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveOther_Click(object sender, EventArgs e)
        {
            int EmployeeID = hideEmpLoyeeID.Value.ToInt32();
            if (EmployeeID != -1)
            {
                SaveForm(EmployeeID);
                BinderData(sender, e);
            }
            else
            {
                JavaScriptTools.AlertWindow("请选择设计师", Page);
            }
        }
        #endregion

        #region 改派给自己
        /// <summary>
        /// 派给自己
        /// </summary>    
        protected void btn_Own_Click(object sender, EventArgs e)
        {
            int EmployeeID = User.Identity.Name.ToInt32();

            SaveForm(EmployeeID);
            BinderData(sender, e);
        }
        #endregion

        #region 改派设计师方法
        /// <summary>
        /// 改派方法
        /// </summary>
        /// <param name="EmployeeID">设计师ID</param>
        public void SaveForm(int EmployeeID)
        {
            HiddenField ObjEmpLoyeeHide = null;
            HiddenField ObjCustomerHide = null;
            HiddenField ObjOrderHide = null;
            int EmployeeIDs = 0;
            var KeyArry = hideKeyList.Value.Trim(',').Split(',');
            int index = -1;
            foreach (var item in KeyArry)
            {
                index += 1;
                if (EmployeeID != 0)
                {
                    FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(item.ToInt32());
                    string PlanDate = "";
                    string[] strDates = null;
                    if (ObjQuotedPriceModel != null)
                    {
                        for (int i = 0; i < repCustomer.Items.Count; i++)
                        {
                            ObjEmpLoyeeHide = (HiddenField)repCustomer.Items[i].FindControl("hideEmpLoyeeID");
                            ObjCustomerHide = (HiddenField)repCustomer.Items[i].FindControl("hideCustomerHide");
                            ObjOrderHide = (HiddenField)repCustomer.Items[i].FindControl("HideOrderID");

                            TextBox txtSaveDate = (TextBox)repCustomer.Items[i].FindControl("txtPlanDate");

                            if (txtSaveDate.Text == "" && ObjEmpLoyeeHide.Value != "")          //必须同时选择设计师和计划完成时间
                            {
                                JavaScriptTools.AlertWindow("请选择计划完成时间", Page);
                                return;
                            }
                            else if (txtSaveDate.Text != "" && ObjEmpLoyeeHide.Value == "")
                            {
                                //TextBox txtEmployeeName = repCustomer.Items[i].FindControl("txtEmployees") as TextBox;
                                //if (txtEmployeeName.Text == "")
                                //{
                                PlanDate += txtSaveDate.Text.ToString() + ",";
                                strDates = PlanDate.Split(',');
                                //}
                            }
                        }
                        ObjQuotedPriceModel.PlanFinishDate = strDates[index].ToDateTime();
                        ObjQuotedPriceModel.DesignerEmployee = EmployeeID;
                        //ObjQuotedPriceModel.DesignerState = 1;      //1 .已派设计师 0. null 未派策划师
                        ObjQuotedPriceModel.IsReceive = 0;
                        ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
                        EmployeeIDs = ObjQuotedPriceModel.DesignerEmployee.ToString().ToInt32();
                        MissionManager ObjMissManagerBLL = new MissionManager();

                        ObjMissManagerBLL.WeddingMissionCreate(ObjQuotedPriceModel.CustomerID.ToString().ToInt32(), 1, (int)MissionTypes.Design, DateTime.Now, EmployeeIDs, "?OrderID=" + ObjOrderHide.Value + "&IsFinish=4&QuotedID=" + ObjQuotedPriceModel.QuotedID + "&CustomerID=" + ObjQuotedPriceModel.CustomerID.ToString().ToInt32() + "&Type=1", MissionChannel.Quoted, User.Identity.Name.ToInt32(), ObjOrderHide.Value.ToInt32(), "", "", "", "设计单制作");
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("请选择设计师", Page);
                        return;
                    }
                }

            }
            JavaScriptTools.AlertWindow("保存完毕", Page);

        }
        #endregion

        #region 获取前期设计
        /// <summary>
        /// 获取前期设计师
        /// </summary>  
        public string GetEarlyEmployee(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjEarlyBLL.GetByCustomerID(CustomerID);
            if (Model != null)
            {
                return GetEmployeeName(Model.EarlyDesigner);
            }
            else
            {
                return "无";
            }
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志显示
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion

    }
}