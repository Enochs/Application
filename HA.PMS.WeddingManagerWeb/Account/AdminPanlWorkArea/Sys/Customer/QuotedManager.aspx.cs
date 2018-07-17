using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer
{
    public partial class QuotedManager : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary> 
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();


        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        string Sortname = "PartyDate";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        public string GetRowState(object QuotedID)
        {
            if (QuotedID != null)
            {
                var ObjQuotedFileModel = ObjQuotedPriceBLL.GetByID(QuotedID.ToString().ToInt32());
                if (ObjQuotedFileModel != null)
                {
                    switch (ObjQuotedFileModel.CheckState)
                    {
                        case null:
                            return "新接到";
                            break;
                        case 1:
                            return "已经提交";
                            break;
                        case 2:
                            return "审核中 ";
                            break;
                        case 3:
                            return "通过";
                            break;
                        default:
                            return "";
                            break;
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// 获取预算或者定金
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderMoney(object CustomerID, int Type)
        {
            if (Type == 1)
            {
                var ObjIntive = ObjOrderBLL.GetbyCustomerID(CustomerID.ToString().ToInt32());
                if (ObjIntive != null)
                {
                    return ObjIntive.EarnestMoney.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                Customers ObjCustomersBLL = new Customers();
                return ObjCustomersBLL.GetByID(CustomerID.ToString().ToInt32()).PartyBudget.ToString();
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
        /// 隐藏
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideCreate(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            else
            {
                if (State.ToString() == "1" || State.ToString() == "0")
                {
                    return string.Empty;
                }
                else
                {
                    return "style='display:none'";
                }
            }
        }

        /// <summary>
        /// 显示更新
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string ShowUpdate(object State)
        {
            if (State == null)
            {
                return "style='display:none';";
            }
            if (State.ToString() == "3")
            {
                return string.Empty;
            }
            else
            {
                return "style='display:none';";
            }
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideDis(object State, object Dispatching)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            if (State.ToString() == "True")
            {
                return "style='display:none'";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 隐藏提交审核
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideChecks(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            else
            {
                if (State.ToString().ToInt32() == 3)
                {
                    return string.Empty;

                }
                else
                {
                    return "style='display:none'";
                }
            }
        }
        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var ObjParList = new List<PMSParameters>();

            ObjParList.Add("ParentQuotedID", 0, NSqlTypes.Equal);       //避免有重复的数据
            ObjParList.Add("Expr1", false, NSqlTypes.Bit);

            //新人姓名
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Bride", txtBride.Text.Trim(), NSqlTypes.LIKE);
            ObjParList.Add(!string.IsNullOrWhiteSpace(txtBride.Text), "Groom", txtBride.Text.Trim(), NSqlTypes.ORLike);
            //婚期
            ObjParList.Add(PartyDateRanger.IsNotBothEmpty, "PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
            //联系电话
            ObjParList.Add(txtCellPhone.Text.Trim() != string.Empty, "GroomCellPhone", txtCellPhone.Text.Trim().ToString(), NSqlTypes.StringEquals);
            ObjParList.Add(txtCellPhone.Text.Trim() != string.Empty, "BrideCellPhone", txtCellPhone.Text.Trim().ToString(), NSqlTypes.OR);
            ObjParList.Add(txtCellPhone.Text.Trim() != string.Empty, "OperatorPhone", txtCellPhone.Text.Trim().ToString(), NSqlTypes.OR);
            //酒店
            ObjParList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text.Trim(), NSqlTypes.LIKE);

            var DataList = ObjQuotedPriceBLL.GetDataByParameter(ObjParList, Sortname, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }
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

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }


        /// <summary>
        /// 开始删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //var ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(e.CommandArgument.ToString().ToInt32());

            //if (ObjQuotedPriceModel != null)
            //{
            //    new QuotedPriceItems().DeleteByQuotedID(ObjQuotedPriceModel.QuotedID);

            //    foreach (var Item in ObjQuotedPriceBLL.GetByAll().Where(C => C.CustomerID == ObjQuotedPriceModel.CustomerID))
            //    {
            //        ObjQuotedPriceBLL.Delete(Item);
            //    }
            //}
            new BLLAssmblly.Flow.Customers().Remove(e.CommandArgument.ToString().ToInt32());

            BinderData();
            JavaScriptTools.AlertWindow("删除成功", Page);

        }

    }
}