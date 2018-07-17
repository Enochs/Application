using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class QuotedCollectionsPlan : ICRUDInterface<FL_QuotedCollectionsPlan>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        Employee ObjEmployeeBLL = new Employee();


        public class CustomerCost
        {
            public string ContarctName
            {
                get;
                set;
            }

            public string PartyDate
            {
                get;
                set;
            }

            public string CostMoney
            {
                get;
                set;
            }

            public string CostDate
            {
                get;
                set;
            }

            public string Phone
            {
                get;
                set;
            }

            public string Node
            { get; set; }
        }

        /// <summary>
        /// 查询收款明细
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetCostPlan> GetCostPlanByWhere(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_GetCostPlan>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);
        }


        public List<View_GetCostPlan> GetByPlanID(int? PlanID)
        {
            return ObjEntity.View_GetCostPlan.Where(C => C.PlanID == PlanID).ToList();
        }

        /// <summary>
        /// 查询收款明细
        /// </summary>
        /// <param name="EmployeeID">责任人</param>
        /// <param name="CostDateStar">收款日期开始</param>
        /// <param name="CostDateEnd">收款日期结束</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        //public List<CustomerCost> GetCustomerCostByEmployeeID(int EmployeeID,bool HaveManagerEmployee,string Name, DateTime CostDateStar, DateTime CostDateEnd, int PageIndex, int PageSize, out int SourceCount)
        //{
        //    //是否需要获取主管和主管下的员工
        //    var EmployeeList = new List<Sys_Employee>();
        //    List<int> objEmployeeKeyList = new List<int>();
        //    if (HaveManagerEmployee)
        //    {
        //        EmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(EmployeeID);

        //        foreach (var ObjItem in EmployeeList)
        //        {
        //            objEmployeeKeyList.Add(ObjItem.EmployeeID);
        //        }

        //        objEmployeeKeyList.Add(EmployeeID);
        //    }
        //    else
        //    {
        //        objEmployeeKeyList.Add(EmployeeID);
        //    }

        //    var OutSourceCount=0;
        //    if (Name != string.Empty)
        //    {
        //        OutSourceCount = (from C in ObjEntity.FL_QuotedCollectionsPlan
        //                          where objEmployeeKeyList.Contains(C.CreateEmpLoyee.Value) && C.CreateDate >= CostDateStar && C.CreateDate <= CostDateEnd
        //                          select C
        //                      ).Count();
        //    }

        //    SourceCount = OutSourceCount;
        //    var CostList = (from C in ObjEntity.FL_QuotedCollectionsPlan
        //                    where objEmployeeKeyList.Contains(C.CreateEmpLoyee.Value) && C.CreateDate >= CostDateStar && C.CreateDate <= CostDateEnd
        //                    select C
        //                      ).OrderBy(C=>C.CreateDate).Skip((PageSize-1) * PageIndex).Take(PageSize).ToList();
        //    List<CustomerCost> ObjCostList = new List<CustomerCost>();
        //    QuotedPrice ObjQuotedPriceBLL = new QuotedPrice();
        //    HA.PMS.BLLAssmblly.Report.Report ObjReportBLL = new Report.Report();
        //    foreach (var ObjItm in CostList)
        //    {
        //        var QuotedModel = ObjQuotedPriceBLL.GetByID(ObjItm.QuotedID);
        //        var SSModel = ObjReportBLL.GetByCustomerID(QuotedModel.CustomerID.Value, EmployeeID);

        //        ObjCostList.Add(new CustomerCost()
        //        {
        //            CostDate = ObjItm.CreateDate.ToString(),
        //            CostMoney = ObjItm.Amountmoney.ToString(),
        //            ContarctName = SSModel.ContactMan,
        //            PartyDate = SSModel.Partydate.ToString(),
        //            Phone = SSModel.ContactPhone,
        //            Node=ObjItm.Node
        //        });
        //    }
        //    return ObjCostList;
        //}

        /// <summary>
        /// 获取当期流水 实际上是已收款
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetMoneySumBYYearMOnth(int DepartmentID, int EmployeeID, DateTime Star, DateTime End)
        {

            var SumQuotedPrice =
             from O in ObjEntity.FL_QuotedCollectionsPlan
             from Q in ObjEntity.View_GetQuotedCostPlan
             where O.QuotedID == Q.QuotedID
             && O.CollectionTime >= Star && O.CollectionTime <= End && O.RealityAmount != null

             select O.RealityAmount;

            return SumQuotedPrice.ToList().Sum(C => C.Value);
            //var ObjList = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CollectionTime.Value.Year == Year && C.CollectionTime.Value.Month == Month);
            //if (ObjList.Count() > 0)
            //{
            //    return ObjList.Sum(C => C.RealityAmount).Value;
            //}
            //else
            //{
            //    return 0;
            //}
        }


        /// <summary>
        /// 当期应收款
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetYinShouMoneySumBYYearMOnth(int DepartmentID, int EmployeeID, DateTime Star, DateTime End)
        {


            //已收
            var SaveMoney =
             from O in ObjEntity.FL_QuotedCollectionsPlan
             from Q in ObjEntity.View_GetQuotedCostPlan
             where O.QuotedID == Q.QuotedID
             && O.CollectionTime >= Star && O.CollectionTime <= End && O.RealityAmount != null

             select O.RealityAmount;


            var PlanMoney =
                 from O in ObjEntity.FL_QuotedCollectionsPlan
                 from Q in ObjEntity.View_GetQuotedCostPlan
                 where O.QuotedID == Q.QuotedID
                 && O.CollectionTime >= Star && O.CollectionTime <= End && O.RealityAmount != null

                 select O.Amountmoney;

            return (PlanMoney.ToList().Sum(C => C.Value)) - (SaveMoney.ToList().Sum(C => C.Value));
            //return PlanMoney.ToList().Sum(C => C.Value);

            //var ObjList = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.RowLock == false);
            //if (ObjList.Count() > 0)
            //{
            //    //var YiShoulist = ObjList.Where(C=>C.RealityAmount!=null);
            //    //if (YiShoulist.Count() > 0)
            //    //{
            //    //    return (ObjList.Sum(C => C.Amountmoney).Value - YiShoulist.Sum(C=>C.RealityAmount).Value);
            //    //}
            //    return ObjList.Sum(C => C.Amountmoney).Value;
            //}
            //else
            //{
            //    return 0;
            //}
        }

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(FL_QuotedCollectionsPlan ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_QuotedCollectionsPlan.Remove(GetByID(ObjectT.PlanID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }
        #endregion

        public List<FL_QuotedCollectionsPlan> GetByAll()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取订单下的收款计划
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_QuotedCollectionsPlan> GetByOrderID(int? OrderID)
        {
            return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.OrderID == OrderID).ToList();
        }


        /// <summary>
        /// 获取订单下的第一次收款 (定金)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public FL_QuotedCollectionsPlan GetOrderDate(int? OrderID)
        {
            return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.OrderID == OrderID).OrderBy(c=>c.CreateDate).ToList().FirstOrDefault();
        }

        /// <summary>
        /// 获取各种说明的收款(实际给退款用的)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_QuotedCollectionsPlan> GetByNodeOrder(int? OrderID, string Node)
        {
            return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.OrderID == OrderID && C.Node == Node).ToList();
        }


        /// <summary>
        /// 根据条件获取邀约用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_QuotedCollectionsPlan> GetQuotedCollectionsPlanByIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;

            var DataSource = PublicDataTools<FL_QuotedCollectionsPlan>.GetDataByParameter(new FL_QuotedCollectionsPlan(), ObjParList.ToArray());
            SourceCount = DataSource.Count;

            if (PageIndex >= 0)
            {

                DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();
            }

            if (SourceCount == 0)
            {
                DataSource = new List<FL_QuotedCollectionsPlan>();
            }
            //return PageDataTools<View_GetInviteCustomers>.AddtoPageSize(DataSource);
            return DataSource;
        }

        /// <summary>
        /// 获取ID数据
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_QuotedCollectionsPlan GetByID(int? KeyID)
        {
            return ObjEntity.FL_QuotedCollectionsPlan.FirstOrDefault(C => C.PlanID == KeyID);
        }

        public List<FL_QuotedCollectionsPlan> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_QuotedCollectionsPlan ObjectT)
        {
            ObjEntity.FL_QuotedCollectionsPlan.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.PlanID;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_QuotedCollectionsPlan ObjectT)
        {

            ObjEntity.SaveChanges();
            return ObjectT.PlanID;
        }

        public List<View_GetCostPlan> GetAllCostPlan()
        {
            return ObjEntity.View_GetCostPlan.ToList();
        }


        /// <summary>
        /// 获取现金流  头部显示
        /// </summary>
        /// <returns></returns>
        public decimal GetRealityAmountSum(int OrderID, int Month)
        {
            var QuotedColletionPlanList = GetByOrderID(OrderID);
            string ReailityMoneySum = QuotedColletionPlanList.Where(C => C.CollectionTime.Value.Month == Month && C.State == 0).Sum(C => C.RealityAmount).ToString();
            decimal sum = Convert.ToDecimal(ReailityMoneySum.ToString());
            return sum;
        }


        #region 获取所有收款
        public string GetAllFinishAmount(List<View_CustomerQuoted> DataList)
        {
            var List = (from C in DataList join D in ObjEntity.FL_QuotedCollectionsPlan on C.OrderID equals D.OrderID select D).ToList();
            return List.Sum(C => C.RealityAmount).ToString();
        }
        #endregion


        public List<View_GetMinDateCollectionPlan> GetMinByAll()
        {
            return ObjEntity.View_GetMinDateCollectionPlan.ToList();
        }
    }
}
