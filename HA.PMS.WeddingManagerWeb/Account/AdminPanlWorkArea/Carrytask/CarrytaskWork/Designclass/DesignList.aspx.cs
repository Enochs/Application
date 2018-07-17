using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignList : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary> 
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();


        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }
        }

        /// <summary>
        /// 获取邀约负责人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderEmployee(object CustomerID)
        {
            if (CustomerID != null)
            {
                if (CustomerID.ToString() != string.Empty)
                {
                    Employee ObjEmpLoyeeBLL = new Employee();
                    var ObjIntive = ObjOrderBLL.GetbyCustomerID(CustomerID.ToString().ToInt32());
                    if (ObjIntive != null)
                    {
                        var ObjEmpModel = ObjEmpLoyeeBLL.GetByID(ObjIntive.EmployeeID);
                        if (ObjEmpModel != null)
                        {
                            return ObjEmpModel.EmployeeName;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 获取订单定金
        /// </summary>
        /// <returns></returns>
        public string GetOrderMoney(object OrderID)
        {

            return ObjOrderBLL.GetByID(OrderID.ToString().ToInt32()).EarnestMoney.ToString();
        }

        #region 数据绑定
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int EmployeeID = User.Identity.Name.ToInt32();
            if (ObjEmployeeBLL.IsManager(EmployeeID))
            {
                td_Type.Visible = true;
                td_Type1.Visible = true;
            }
            else
            {
                td_Type.Visible = false;
                td_Type1.Visible = false;
            }
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<PMSParameters> ObjparList = new List<PMSParameters>();
            ObjparList.Add("DesignerState", "1,2", NSqlTypes.IN);    //说明已选择策划师
            //根据设计师查询
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                if (ObjEmployeeBLL.GetByID(EmployeeID).DepartmentID == 32 || ObjEmployeeBLL.GetByID(EmployeeID).DepartmentID == 34)  //执行部 或设计部
                {
                    MyManager.GetEmployeePar(ObjparList, "DesignerEmployee");
                }
                else
                {
                    MyManager.GetEmployeePar(ObjparList, "EmployeeID");
                }
            }
            else
            {
                if (ddlEmployeeTypes.SelectedValue != "0")
                {
                    MyManager.GetEmployeePar(ObjparList, ddlEmployeeTypes.SelectedValue.ToString());
                }
                else
                {
                    if (ObjEmployeeBLL.GetByID(EmployeeID).DepartmentID == 32 || ObjEmployeeBLL.GetByID(EmployeeID).DepartmentID == 34)                    //执行部 或设计部
                    {
                        ObjparList.Add("DesignerEmployee", User.Identity.Name.ToInt32(), NSqlTypes.Equal);
                    }
                    else
                    {
                        ObjparList.Add("EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.Equal);
                    }
                }

            }

            //新人姓名
            ObjparList.Add(!txtBride.Text.Equals(string.Empty), "ContactMan", txtBride.Text.Trim(), NSqlTypes.LIKE);
            //根据酒店查询
            ObjparList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.LIKE);
            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                ObjparList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //婚期

            //完成状态
            if (ddlState.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("DesignerState", ddlState.SelectedValue.ToInt32(), NSqlTypes.Equal);

                ObjparList.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            else
            {
                if (DateRanger.IsNotBothEmpty)
                {
                    ObjparList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                }
                else
                {
                    string StartDate = DateTime.Now.ToShortDateString();
                    string EndDate = "9999-12-31";
                    ObjparList.Add("PartyDate", StartDate + "," + EndDate, NSqlTypes.DateBetween);
                }
            }

            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(ObjparList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }
        #endregion

        #region 获取报价单ID
        /// <summary>
        /// 根据用户ID获取报价单ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedIDByCustomers(object CustomerID)
        {
            if (CustomerID.ToString() != string.Empty && CustomerID.ToString() != "0")
            {
                return ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 分页
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion


        #region 点击查询
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion



        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>       
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            HiddenField ObjEmpLoyeeHide;
            HiddenField ObjCustomerHide;

            for (int i = 0; i < repCustomer.Items.Count; i++)
            {
                ObjEmpLoyeeHide = (HiddenField)repCustomer.Items[i].FindControl("hideEmpLoyeeID");
                ObjCustomerHide = (HiddenField)repCustomer.Items[i].FindControl("hideCustomerHide");
                int CustomerId = ObjCustomerHide.Value.ToInt32();
                FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(ObjCustomerHide.Value.ToInt32());
                if (ObjEmpLoyeeHide.Value.ToInt32() != 0 && ObjQuotedPriceModel != null)
                {
                    ObjQuotedPriceModel.DesignerEmployee = ObjEmpLoyeeHide.Value.ToInt32();
                    ObjQuotedPriceModel.DesignCreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                    //ObjQuotedPriceModel.DesignerState = 1;      //已派设计师
                    ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
                }
            }
            BinderData();
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
                BinderData();
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
            BinderData();
        }
        #endregion

        #region 改派设计师方法
        /// <summary>
        /// 改派方法
        /// </summary>
        /// <param name="EmployeeID">设计师ID</param>
        public void SaveForm(int EmployeeID)
        {

            var KeyArry = hideKeyList.Value.Trim(',').Split(',');
            int index = -1;
            foreach (var item in KeyArry)
            {
                index += 1;
                if (EmployeeID != 0)
                {
                    FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(item.ToInt32());
                    if (ObjQuotedPriceModel != null)
                    {
                        ObjQuotedPriceModel.DesignerEmployee = EmployeeID;
                        ObjQuotedPriceModel.DesignCreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                        //ObjQuotedPriceModel.DesignerState = 1;      //1 .已派设计师 0. null 未派策划师
                        ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
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

        #region 获取QuotedID
        /// <summary>
        /// 根据OrderID获取QuotedID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetQuotedID(object OrderID)
        {
            return new BLLAssmblly.Flow.QuotedPrice().GetByOrderId(OrderID.ToString().ToInt32()).QuotedID.ToString();
        }
        #endregion


        public string GetDisID(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjDispatchingModel = ObjDispatchingBLL.GetByCustomerID(CustomerID);
            if (ObjDispatchingModel != null)
            {
                return ObjDispatchingModel.DispatchingID.ToString();
            }
            return "";
        }

        public string ChangeForColor(object Source)
        {
            int DesignState = Source.ToString().ToInt32();
            return DesignState == 2 ? "style ='color:red'" : string.Empty;
        }
    }
}