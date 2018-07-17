using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceFileCheckList : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new BLLAssmblly.Flow.Invite();
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
        public string GetInviteEmployee(object CustomerID)
        {
            if (CustomerID != null)
            {
                if (CustomerID.ToString() != string.Empty)
                {
                    Employee ObjEmpLoyeeBLL = new Employee();
                    var ObjIntive = ObjInviteBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
                    if (ObjIntive != null)
                    {
                        var ObjEmpModel = ObjEmpLoyeeBLL.GetByID(ObjIntive.EmpLoyeeID);
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
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            ObjParList.Add(new ObjectParameter("FileCheck", 2));

            
            var DataList = ObjQuotedPriceBLL.GetCustomerQuotedByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, ObjParList);
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Checks")
            {
                var ObjUpdateModel=ObjQuotedPriceBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjUpdateModel.FileCheck = 3;
                ObjQuotedPriceBLL.Update(ObjUpdateModel);
            }
        }
    }
}