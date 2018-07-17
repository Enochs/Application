using System;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignclassforWarehouse : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.Designclass ObjDesignclassBLL = new BLLAssmblly.Flow.Designclass();

        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();


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


        #region 数据绑定
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<PMSParameters> ObjparList = new List<PMSParameters>();
            ObjparList.Add("DesignerState", 2, NSqlTypes.Equal);    //说明已选择策划师
            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(ObjparList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();

        }
        #endregion






        /// <summary>
        /// 分页打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();

        }

        protected void RepDesignlist_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            var ObjUpdateModel = ObjDesignclassBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            if (ObjUpdateModel != null)
            {
                ObjUpdateModel.State = 2;
                ObjUpdateModel.UpdateDate = DateTime.Now;
                ObjUpdateModel.RealQuantity = (e.Item.FindControl("txtRealQuantity") as TextBox).Text.ToInt32();
                if (ObjUpdateModel.RealQuantity <= 0)
                {
                    JavaScriptTools.AlertWindow("确认到货数量不能为空!", Page);
                    return;
                }
                ObjDesignclassBLL.Update(ObjUpdateModel);


                JavaScriptTools.AlertWindowAndLocation("确认到货成功!", Request.Url.ToString(), Page);
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
    }
}