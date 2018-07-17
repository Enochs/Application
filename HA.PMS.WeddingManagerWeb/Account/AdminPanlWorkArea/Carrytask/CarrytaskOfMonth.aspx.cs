using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskOfMonth : SystemPage
    {/// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 派工操作
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();
        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();

        Employee ObjEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
                DepartmentDropdownList1.BinderByQuoted();

                hideIsmanager.Value = ObjEmployeeBLL.IsManager(User.Identity.Name.ToString().ToInt32()).ToString();
            }
        }

        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();

            string DataTimerStar = DateTime.Today.ToString();
            string DateTimerEnd = DateTime.Today.AddDays(30).ToString();
            objParmList.Add("PartyDate", DataTimerStar + "," + DateTimerEnd, NSqlTypes.DateBetween);


            objParmList.Add(DateRanger.IsNotBothEmpty, "CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);



            if (DepartmentDropdownList1.SelectedItem.Value.ToInt32() > 0)       //选择了部门
            {
                if (MyManager.SelectedValue.ToInt32() > 0)
                {
                    objParmList.Add("QuotedEmpLoyee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
                }
                else
                {
                    string keys = "";
                    Department ObjDepartmentBLL = new Department();
                    var DataLists = ObjEmployeeBLL.GetMyManagerEmpLoyee(ObjDepartmentBLL.GetByID(DepartmentDropdownList1.SelectedValue.ToInt32()).DepartmentManager);
                    int index = 0;
                    foreach (var item in DataLists)
                    {
                        index += 1;
                        if (index == DataLists.Count)
                        {
                            keys += item.EmployeeID;
                        }
                        else
                        {
                            keys += item.EmployeeID + ",";
                        }
                    }
                    objParmList.Add("QuotedEmpLoyee", keys, NSqlTypes.IN);
                }
            }
            else
            {
                if (ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeTypeID > 2)        //没有选择部门 包括也没选择策划师
                {
                    MyManager.GetEmployeePar(objParmList, "QuotedEmpLoyee");
                }
 
                if (MyManager.SelectedValue.ToInt32() > 0)      //没有选择部门 但选择了策划师个人
                {
                    objParmList.Add("QuotedEmpLoyee", MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
                }
            }


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            repCustomer.DataBind(new BLLAssmblly.Flow.Dispatching().GetDispatchingPageByWhere(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount));
            CtrPageIndex.RecordCount = SourceCount;
        }


        /// <summary>
        /// 根据用户ID获取报价单ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedIDByCustomers(object CustomerID)
        {
            return ObjQuotedPrice.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
        }

        public string GetFinishMoney(object OrderID)
        {
            return ObjQuotedPrice.GetByOrderId(OrderID.ToString().ToInt32()).FinishAmount.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }


        /// <summary>
        /// 应收款 总金额-已收款
        /// </summary>
        public string GetMoney(object source)
        {
            int CustomerID = source.ToString().ToInt32();
            FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPrice.GetByCustomerID(CustomerID);
            QuotedCollectionsPlan ObjCollectionPlanBLL = new QuotedCollectionsPlan();
            List<FL_QuotedCollectionsPlan> DataList = ObjCollectionPlanBLL.GetByOrderID(ObjQuotedPriceModel.OrderID);
            double TotalSum = ObjQuotedPriceModel.AggregateAmount.ToString().ToDouble();
            double CostSum = DataList.Sum(C => C.RealityAmount).ToString().ToDouble();
            return (TotalSum - CostSum).ToString("f2");
        }

    }
}