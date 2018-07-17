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
using System.Text;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceforMonth : SystemPage
    { /// <summary>
        /// 用户操作
        /// 
        /// </summary> 
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();
        string ColumnName = "PartyDate";


        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney ObjOrderEarnestMoneyBLL = new HA.PMS.BLLAssmblly.FinancialAffairsbll.OrderEarnestMoney();

        HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();

        Dispatching ObjDispatchingBLL = new Dispatching();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                CreateDateRanger.StartText = MonthStart.ToShortDateString();
                CreateDateRanger.EndText = MonthEnd.ToShortDateString();
                BinderData();
                ddlDepartment.BinderByQuoted();
            }

        }




        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            //int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<PMSParameters> paramsList = new List<PMSParameters>();
            int DepartmentID = ddlDepartment.SelectedItem.Value.ToInt32();
            int EmployeeID = MyManager.SelectedValue.ToInt32();

            List<int> Employees = new List<int>();
            if (DepartmentID > 0)
            {
                IEnumerable<int> departmentIDs = new Department().GetbyChildenByDepartmetnID(DepartmentID).Select(C => C.DepartmentID);
                Employees.AddRange(new Employee().Where(C => departmentIDs.Contains(C.DepartmentID)).Select(C => C.EmployeeID).AsEnumerable());
            }
            else if (EmployeeID > 0)
            {
                Employees.Add(EmployeeID);
                Employees.AddRange(new Employee().GetMyManagerEmpLoyee(EmployeeID).Select(C => C.EmployeeID));
            }
            else
            {
                paramsList.Add("OrderEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
            }

            paramsList.Add("ParentQuotedID", 0, NSqlTypes.Equal);
            paramsList.Add("IsDelete", false, NSqlTypes.Bit);


            //ObjParList.Add(new ObjectParameter("CreateDate_between", string.Format("{0},{1}", DateTime.Now.AddDays(-(DateTime.Now.Day-1)).ToString(), DateTime.Now.AddDays((DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - DateTime.Now.Day).ToString())));

            //婚期
            if (PartyDateRanger.IsNotBothEmpty)
            {
                paramsList.Add("PartyDate", PartyDateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumnName = "PartyDate";
            }
            //签到日期
            if (CreateDateRanger.IsNotBothEmpty)
            {
                paramsList.Add("OrderCreateDate", CreateDateRanger.StartoEnd, NSqlTypes.DateBetween);
                ColumnName = "OrderCreateDate";
            }

            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(paramsList, ColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
            GetMoneySumTotal(DataList);
        }

        /// <summary>
        /// 绑定样式
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string SetClass(object QuotedID)
        {

            var ObjQuotedModel = ObjDispatchingBLL.GetByQuotedID(QuotedID.ToString().ToInt32());
            if (ObjQuotedModel != null)
            {
                return string.Empty;
            }
            else
            {
                return "RemoveClass";
            }

        }

        /// <summary>
        /// 根据报价单获取执行单ID
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetDispatchingIDByQuotedID(string QuotedID)
        {

            var ObjQuotedModel = ObjDispatchingBLL.GetByQuotedID(QuotedID.ToString().ToInt32());
            if (ObjQuotedModel != null)
            {
                return ObjQuotedModel.DispatchingID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #region 获取邀约负责人
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
        #endregion

        #region 获取行的状态
        /// <summary>
        /// 状态获取
        /// </summary>   
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
                            return "未提交";
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
        #endregion

        #region 获取已付款
        /// <summary>
        /// 获取已付款
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetQuotedDispatchingFinishMoney(object CustomerID)
        {

            decimal FinishAmount = 0;

            //预付款
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            decimal EarnestMoney = QuotedModel.EarnestMoney.Value;
            FinishAmount += EarnestMoney;

            //获得收款计划的东西
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID);

            foreach (var Objitem in ObjList)
            {
                FinishAmount += Objitem.RealityAmount.Value;
            }

            //定金
            var ObjEorder = ObjOrderEarnestMoneyBLL.GetByOrderID(QuotedModel.OrderID);
            if (ObjEorder != null)
            {
                if (ObjEorder.EarnestMoney.HasValue)
                {
                    FinishAmount += ObjEorder.EarnestMoney.Value;
                }
            }
            return FinishAmount.ToString();

        }
        #endregion

        #region 显示更新  隐藏
        /// <summary>
        /// 显示更新
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string ShowUpdate(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            if (State.ToString() == "True")
            {
                return string.Empty;

            }
            else
            {
                return "style='display:none'";
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
            if (State.ToString() == "False")
            {
                return string.Empty;

            }
            else
            {
                return "style='display:none'";
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
        #endregion

        #region 隐藏提交审核
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
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 合计
        /// <summary>
        /// 总的合计(本日  本月 本年)
        /// </summary>
        /// <param name="DataList"></param>

        public void GetMoneySumTotal(List<View_CustomerQuoted> DataList)
        {
            lblMoneySumToday.Text = DataList.Where(C => C.CreateDate != null && C.CreateDate.Date == DateTime.Now.Date && C.CreateDate.Month == DateTime.Now.Month && C.CreateDate.Year == DateTime.Now.Year).Sum(C => C.FinishAmount).ToString();
            lblMoneySumToMonth.Text = DataList.Where(C => C.CreateDate != null && C.CreateDate.Month == DateTime.Now.Month && C.CreateDate.Year == DateTime.Now.Year).Sum(C => C.FinishAmount).ToString();
            lblMoneySumToYear.Text = DataList.Where(C => C.CreateDate != null && C.CreateDate.Year == DateTime.Now.Year).Sum(C => C.FinishAmount).ToString();
        }
        #endregion

        #region 获取  已收款
        public string GetMoneyByOrderID(object Source)
        {
            int OrderID = Source.ToString().ToInt32();
            //获得收款计划的东西
            var ObjList = ObjQuotedCollectionsPlanBLL.GetByOrderID(OrderID);
            decimal FinishAmount = 0;
            foreach (var Objitem in ObjList)
            {
                FinishAmount += Objitem.RealityAmount.Value;
            }
            return FinishAmount.ToString();

        }
        #endregion

    }
}