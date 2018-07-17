
/**
 Version :HaoAi 1.0
 File Name :Customers
 Author:黄晓可
 Date:2013.3.13
 Description:客户管理 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using System.Linq.Expressions;
using System;
using System.Data.Objects;
using System.Data.EntityClient;
using HA.PMS.BLLAssmblly.PublicTools;
using System.IO;
using System.Web;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.ToolsLibrary;
using System.Data.SqlClient;
using HA.PMS.BLLAssmblly.CS;
namespace HA.PMS.BLLAssmblly.Flow
{
    #region
    public class QueryData
    {
        public IQueryable AddPropertyMatch(IQueryable query, string prop, object value)
        {

            ParameterExpression p = Expression.Parameter(query.ElementType, "p");

            LambdaExpression pred = Expression.Lambda(

                Expression.Equal(

                      Expression.PropertyOrField(p, prop),

                      Expression.Constant(value)

                      ), p
           );


            return query.Provider.CreateQuery(

                   Expression.Call(typeof(Queryable), "Where", new Type[] { query.ElementType }, query.Expression, pred)

                   );
        }

        public IQueryable AddPropertyMatches(IQueryable query, Dictionary<string, object> matches)
        {

            foreach (var kv in matches)
            {

                query = AddPropertyMatch(query, kv.Key, kv.Value);

            }

            return query;

        }

    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }


    }

    #endregion

    public class Customers : ICRUDInterface<FL_Customers>
    {


        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 获取平均成交率
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetAvgSucess(int Year, int Month)
        {
            var ObjCustomerCount = ObjEntity.FL_Customers.Count(C => C.IsDelete == false && C.RecorderDate.Value.Year == Year && C.RecorderDate.Value.Month == Month);
            var ObjCustomerSucessCount = ObjEntity.FL_Customers.Count(C => C.IsDelete == false && C.State >= 13 && C.State <= 29 && C.RecorderDate.Value.Year == Year && C.RecorderDate.Value.Month == Month);
            if (ObjCustomerCount > 0)
            {
                return (ObjCustomerSucessCount / ObjCustomerCount).ToString("0.00");
            }
            else
            {
                return "0";
            }


        }


        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Customers ObjectT)
        {
            if (ObjectT != null)
            {
                //删除 FL_Telemarketing
                var ObjTelemarketingModel = ObjEntity.FL_Telemarketing.FirstOrDefault(C => C.CustomerID == ObjectT.CustomerID);
                if (ObjTelemarketingModel != null)
                {
                    ObjEntity.FL_Telemarketing.Remove(ObjTelemarketingModel);
                    ObjEntity.SaveChanges();
                }

                //删除 满意度调查模块数据
                var ObjDegreeOfSatisfactionModel = ObjEntity.CS_DegreeOfSatisfaction.FirstOrDefault(C => C.CustomerID == ObjectT.CustomerID);
                if (ObjDegreeOfSatisfactionModel != null)
                {
                    var ObjDegreeOfSatisfactionContentList = ObjEntity.CS_DegreeOfSatisfactionContent.Where(C => C.DofKey == ObjDegreeOfSatisfactionModel.DofKey);
                    if (ObjDegreeOfSatisfactionContentList != null)
                    {
                        //满意度content
                        foreach (var Item in ObjDegreeOfSatisfactionContentList)
                        {
                            ObjEntity.CS_DegreeOfSatisfactionContent.Remove(Item);
                        }
                        ObjEntity.SaveChanges();
                    }

                    ObjEntity.CS_DegreeOfSatisfaction.Remove(ObjDegreeOfSatisfactionModel);
                    ObjEntity.SaveChanges();
                }



                //删除 新人回访
                var ObjCustomerReturnVisitBLL = new CustomerReturnVisit();
                var ObjCustomerReturnVisitList = ObjCustomerReturnVisitBLL.GetByAll().Where(C => C.CustomerId.Equals(ObjectT.CustomerID)).ToList();
                if (ObjCustomerReturnVisitList != null && ObjCustomerReturnVisitList.Count > 0)
                {
                    foreach (var Item in ObjCustomerReturnVisitList)
                    {
                        ObjCustomerReturnVisitBLL.Delete(Item);
                    }
                }

                //删除 新人满意度
                var ObjDegreeBLL = new DegreeOfSatisfaction();
                var ObjDegreeSatisfactionList = ObjDegreeBLL.GetByAll().Where(C => C.CustomerID == ObjectT.CustomerID).ToList();
                if (ObjDegreeSatisfactionList != null && ObjDegreeSatisfactionList.Count > 0)
                {
                    foreach (var Item in ObjDegreeSatisfactionList)
                    {
                        ObjDegreeBLL.Delete(Item);
                    }
                }

                //删除 取件模块数据
                var ObjTakeDiskModel = ObjEntity.CS_TakeDisk.FirstOrDefault(C => C.CustomerID == ObjectT.CustomerID);
                if (ObjTakeDiskModel != null)
                {
                    ObjEntity.CS_TakeDisk.Remove(ObjTakeDiskModel);
                    ObjEntity.SaveChanges();
                }
                //删除邀请内容
                var ObjInviteContentModel = ObjEntity.FL_InvtieContent.Where(C => C.CustomerID == ObjectT.CustomerID).ToList();
                if (ObjInviteContentModel.Count > 0)
                {
                    foreach (var item in ObjInviteContentModel)
                    {
                        ObjEntity.FL_InvtieContent.Remove(item);
                        ObjEntity.SaveChanges();
                    }
                }

                //删除 邀请
                var ObjInviteModel = ObjEntity.FL_Invite.FirstOrDefault(C => C.CustomerID == ObjectT.CustomerID);
                if (ObjInviteModel != null)
                {
                    ObjEntity.FL_Invite.Remove(ObjInviteModel);
                    ObjEntity.SaveChanges();
                }

                //删除 报价单


                var ObjQuotedPriceModel = ObjEntity.FL_QuotedPrice.Where(C => C.CustomerID == ObjectT.CustomerID).ToList();
                int QuotedId = 0;
                if (ObjQuotedPriceModel.Count > 0)
                {
                    foreach (var item in ObjQuotedPriceModel)
                    {
                        QuotedId = item.QuotedID;
                        int EmployeeID = item.EmpLoyeeID.ToString().ToInt32();
                        var ObjQuotedPriceItemModel = ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedId).ToList();
                        if (ObjQuotedPriceItemModel.Count > 0)
                        {
                            foreach (var it in ObjQuotedPriceItemModel)
                            {
                                ObjEntity.FL_QuotedPriceItems.Remove(it);
                                ObjEntity.SaveChanges();
                            }
                        }


                        if (item.ParentQuotedID == 0)
                        {
                            var ObjQuotedCollectionMOdel = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.QuotedID == QuotedId).ToList();

                            decimal SumMoney = ObjQuotedCollectionMOdel.Where(C => C.CreateEmpLoyee == EmployeeID).Sum(C => C.RealityAmount).ToString().ToDecimal();


                            var TargetModel = ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.TargetTitle == "现金流" && C.Year == DateTime.Now.Year && C.EmployeeID == EmployeeID);
                            if (TargetModel != null)
                            {
                                if (DateTime.Now.Month == 1)
                                {
                                    TargetModel.MonthFinsh1 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 2)
                                {
                                    TargetModel.MonthFinish2 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 3)
                                {
                                    TargetModel.MonthFinish3 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 4)
                                {
                                    TargetModel.MonthFinish4 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 5)
                                {
                                    TargetModel.MonthFinish5 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 6)
                                {
                                    TargetModel.MonthFinish6 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 7)
                                {
                                    TargetModel.MonthFinish7 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 8)
                                {
                                    TargetModel.MonthFinish8 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 9)
                                {
                                    TargetModel.MonthFinish9 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 10)
                                {
                                    TargetModel.MonthFinish10 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 11)
                                {
                                    TargetModel.MonthFinish11 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                                else if (DateTime.Now.Month == 12)
                                {
                                    TargetModel.MonthFinish12 -= SumMoney;
                                    ObjEntity.SaveChanges();
                                }
                            }


                            if (ObjQuotedCollectionMOdel.Count > 0)
                            {
                                foreach (var items in ObjQuotedCollectionMOdel)
                                {
                                    ObjEntity.FL_QuotedCollectionsPlan.Remove(items);
                                    ObjEntity.SaveChanges();
                                }
                            }
                        }

                        var ObjQuotedPriceFileModel = ObjEntity.FL_QuotedPricefileManager.Where(C => C.QuotedID == QuotedId).ToList();
                        if (ObjQuotedPriceFileModel.Count > 0)
                        {
                            foreach (var items in ObjQuotedPriceFileModel)
                            {
                                ObjEntity.FL_QuotedPricefileManager.Remove(items);
                                ObjEntity.SaveChanges();
                            }
                        }
                        ObjEntity.FL_QuotedPrice.Remove(item);
                        ObjEntity.SaveChanges();
                    }

                }

                //删除订单
                var ObjOrderDetailsModel = ObjEntity.FL_OrderDetails.Where(C => C.CustomerID == ObjectT.CustomerID).ToList();
                if (ObjOrderDetailsModel.Count > 0)
                {
                    foreach (var items in ObjOrderDetailsModel)
                    {
                        ObjEntity.FL_OrderDetails.Remove(items);
                        ObjEntity.SaveChanges();
                    }
                }


                var ObjOrderModel = ObjEntity.FL_Order.Where(C => C.CustomerID == ObjectT.CustomerID).ToList();
                if (ObjOrderModel.Count > 0)
                {
                    foreach (var items in ObjOrderModel)
                    {
                        var ObjOrderEarnesMoneyModel = ObjEntity.FL_OrderEarnestMoney.Where(C => C.OrderID == items.OrderID).ToList();
                        if (ObjOrderEarnesMoneyModel.Count > 0)
                        {
                            foreach (var item in ObjOrderEarnesMoneyModel)
                            {
                                ObjEntity.FL_OrderEarnestMoney.Remove(item);
                                ObjEntity.SaveChanges();
                            }
                        }
                        var ObjCelebrationModel = ObjEntity.FL_Celebration.Where(C => C.OrderID == items.OrderID).ToList();
                        if (ObjCelebrationModel.Count > 0)
                        {
                            foreach (var item in ObjCelebrationModel)
                            {
                                var ObjDispatchingModel = ObjEntity.FL_Dispatching.Where(C => C.CelebrationID == item.CelebrationID).ToList();
                                if (ObjDispatchingModel.Count > 0)
                                {
                                    foreach (var it in ObjDispatchingModel)
                                    {
                                        var ObjProdcutForDispatchingModel = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == it.DispatchingID).ToList();
                                        if (ObjProdcutForDispatchingModel.Count > 0)
                                        {
                                            foreach (var its in ObjProdcutForDispatchingModel)
                                            {
                                                ObjEntity.FL_ProductforDispatching.Remove(its);
                                                ObjEntity.SaveChanges();
                                            }
                                        }
                                        ObjEntity.FL_Dispatching.Remove(it);
                                        ObjEntity.SaveChanges();
                                    }
                                }
                                var ObjCelebrationProductItemModel = ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == item.CelebrationID).ToList();
                                if (ObjCelebrationProductItemModel.Count > 0)
                                {
                                    foreach (var ite in ObjCelebrationProductItemModel)
                                    {
                                        ObjEntity.FL_CelebrationProductItem.Remove(ite);
                                        ObjEntity.SaveChanges();
                                    }
                                }

                                ObjEntity.FL_Celebration.Remove(item);
                                ObjEntity.SaveChanges();
                            }
                        }


                        ObjEntity.FL_Order.Remove(items);
                        ObjEntity.SaveChanges();
                    }
                }



                //删除 新人基本信息
                var ObjCustomersModel = ObjEntity.FL_Customers.FirstOrDefault(C => C.CustomerID == ObjectT.CustomerID);
                if (ObjCustomersModel != null)
                {
                    ObjEntity.FL_Customers.Remove(ObjEntity.FL_Customers.FirstOrDefault(C => C.CustomerID == ObjectT.CustomerID));
                    return ObjEntity.SaveChanges();
                }




            }
            return 0;
        }

        /// <summary>
        /// 删除有关新人的所有信息。（删除报价单  但不能删除新人的信息）
        /// </summary>
        /// <param name="CustomerID"></param>
        public void Remove(int CustomerID)
        {
            #region 执行状态、执行产品、图片附件、执行团队、执行明细、执行总表

            //执行明细
            List<FL_Dispatching> fL_DispatchingList = ObjEntity.FL_Dispatching.Where(C => C.CustomerID.Value == CustomerID).ToList();
            if (fL_DispatchingList != null)
            {
                for (int i = 0; i < fL_DispatchingList.Count; i++)
                {
                    int DispatchingID = fL_DispatchingList[i].DispatchingID;
                    //执行状态
                    List<FL_DispatchingState> ObjDispatchingStateList = ObjEntity.FL_DispatchingState.Where(C => C.DispatchingID == DispatchingID).ToList();
                    if (ObjDispatchingStateList != null)
                    {
                        for (int j = 0; j < ObjDispatchingStateList.Count; j++)
                        {
                            ObjEntity.FL_DispatchingState.Remove(ObjDispatchingStateList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //执行产品
                    List<FL_ProductforDispatching> fL_ProductforDispatchingList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID).ToList();
                    if (fL_ProductforDispatchingList != null)
                    {
                        for (int j = 0; j < fL_ProductforDispatchingList.Count; j++)
                        {
                            ObjEntity.FL_ProductforDispatching.Remove(fL_ProductforDispatchingList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //图片附件
                    List<FL_DispatchingImage> fL_DispatchingImageList = ObjEntity.FL_DispatchingImage.Where(C => C.DispatchingID == DispatchingID).ToList();
                    if (fL_DispatchingImageList != null)
                    {
                        for (int j = 0; j < fL_DispatchingImageList.Count; j++)
                        {
                            //1.删除文件
                            if (File.Exists(HttpContext.Current.Server.MapPath(fL_DispatchingImageList[i].FileAddress + string.Empty)))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(fL_DispatchingImageList[i].FileAddress));
                            }
                            //2.删除数据库记录
                            ObjEntity.FL_DispatchingImage.Remove(fL_DispatchingImageList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //执行团队
                    List<FL_DispatchingEmployeeManager> fL_DispatchingEmployeeManagerList = ObjEntity.FL_DispatchingEmployeeManager.Where(C => C.DispatchingID == DispatchingID).ToList();
                    if (fL_DispatchingEmployeeManagerList != null)
                    {
                        for (int j = 0; j < fL_DispatchingEmployeeManagerList.Count; j++)
                        {
                            ObjEntity.FL_DispatchingEmployeeManager.Remove(fL_DispatchingEmployeeManagerList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }
                    ObjEntity.FL_Dispatching.Remove(fL_DispatchingList[i]);
                }

                //执行总表
                List<FL_Celebration> fL_CelebrationList = ObjEntity.FL_Celebration.Where(C => C.CustomerID == CustomerID).ToList();
                if (fL_CelebrationList != null)
                {
                    for (int i = 0; i < fL_CelebrationList.Count; i++)
                    {
                        int CelebrationID = fL_CelebrationList[i].CelebrationID;
                        List<FL_CelebrationProductItem> fL_CelebrationProductItemList = ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == CelebrationID).ToList();
                        if (fL_CelebrationProductItemList != null)
                        {
                            for (int j = 0; j < fL_CelebrationProductItemList.Count; j++)
                            {
                                List<FL_Dispatching> FL_DispatchingList = ObjEntity.FL_Dispatching.Where(C => C.CelebrationID == CelebrationID).ToList();
                                if (FL_DispatchingList.Count > 0)
                                {
                                    foreach (var item in FL_DispatchingList)
                                    {
                                        ObjEntity.FL_Dispatching.Remove(item);
                                        ObjEntity.SaveChanges();
                                    }
                                }
                                ObjEntity.FL_CelebrationProductItem.Remove(fL_CelebrationProductItemList[j]);
                                ObjEntity.SaveChanges();
                            }

                        }
                        ObjEntity.FL_Celebration.Remove(fL_CelebrationList[i]);
                    }
                    ObjEntity.SaveChanges();
                }
            }

            #endregion

            #region 报价单图片附件、报价单提案、收款计划、报价单

            QuotedPrice ObjQuotedPriceBLL = new QuotedPrice();
            List<FL_QuotedPrice> fL_QuotedPriceList = ObjEntity.FL_QuotedPrice.Where(C => C.CustomerID.Value.Equals(CustomerID)).ToList();
            if (fL_QuotedPriceList != null)
            {
                for (int i = 0; i < fL_QuotedPriceList.Count; i++)
                {
                    int QuotedID = fL_QuotedPriceList[i].QuotedID;
                    int EmployeeID = fL_QuotedPriceList[0].EmpLoyeeID.ToString().ToInt32();
                    //报价单详细
                    List<FL_QuotedPriceItems> fL_QuotedPriceItemsList = ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == QuotedID).ToList();
                    if (fL_QuotedPriceItemsList != null && fL_QuotedPriceItemsList.Count > 0)
                    {
                        for (int j = 0; j < fL_QuotedPriceItemsList.Count; j++)
                        {
                            ObjEntity.FL_QuotedPriceItems.Remove(fL_QuotedPriceItemsList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //图片附件
                    List<FL_QuotedImage> fL_QuotedImagList = ObjEntity.FL_QuotedImage.Where(C => C.QuotedID == QuotedID).ToList();
                    if (fL_QuotedImagList != null && fL_QuotedImagList.Count > 0)
                    {
                        for (int j = 0; j < fL_QuotedImagList.Count; j++)
                        {
                            //1.删除文件
                            if (File.Exists(HttpContext.Current.Server.MapPath(fL_QuotedImagList[j].ImageUrl + string.Empty)))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(fL_QuotedImagList[j].ImageUrl));
                            }
                            //2.删除数据库记录
                            ObjEntity.FL_QuotedImage.Remove(fL_QuotedImagList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //提案
                    List<FL_QuotedPricefileManager> fL_QuotedPricefileManagerList = ObjEntity.FL_QuotedPricefileManager.Where(C => C.QuotedID == QuotedID).ToList();
                    if (fL_QuotedPricefileManagerList != null && fL_QuotedPricefileManagerList.Count > 0)
                    {
                        for (int j = 0; j < fL_QuotedPricefileManagerList.Count; j++)
                        {
                            //1.删除文件
                            if (File.Exists(HttpContext.Current.Server.MapPath(fL_QuotedPricefileManagerList[j].FileAddress + string.Empty)))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(fL_QuotedPricefileManagerList[j].FileAddress));
                            }
                            //2.删除数据库记录
                            ObjEntity.FL_QuotedPricefileManager.Remove(fL_QuotedPricefileManagerList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //头部报表
                    //收款计划
                    List<FL_QuotedCollectionsPlan> fL_QuotedCollectionsPlanList = ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.QuotedID.Value == QuotedID).ToList();
                    decimal SumMoney = fL_QuotedCollectionsPlanList.Where(C => C.CreateEmpLoyee == EmployeeID).Sum(C => C.RealityAmount).ToString().ToDecimal();


                    var TargetModel = ObjEntity.FL_FinishTargetSum.FirstOrDefault(C => C.TargetTitle == "现金流" && C.Year == DateTime.Now.Year && C.EmployeeID == EmployeeID);
                    if (TargetModel != null)
                    {
                        if (DateTime.Now.Month == 1)
                        {
                            TargetModel.MonthFinsh1 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 2)
                        {
                            TargetModel.MonthFinish2 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 3)
                        {
                            TargetModel.MonthFinish3 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 4)
                        {
                            TargetModel.MonthFinish4 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 5)
                        {
                            TargetModel.MonthFinish5 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 6)
                        {
                            TargetModel.MonthFinish6 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 7)
                        {
                            TargetModel.MonthFinish7 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 8)
                        {
                            TargetModel.MonthFinish8 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 9)
                        {
                            TargetModel.MonthFinish9 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 10)
                        {
                            TargetModel.MonthFinish10 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 11)
                        {
                            TargetModel.MonthFinish11 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                        else if (DateTime.Now.Month == 12)
                        {
                            TargetModel.MonthFinish12 -= SumMoney;
                            ObjEntity.SaveChanges();
                        }
                    }






                    if (fL_QuotedCollectionsPlanList != null && fL_QuotedCollectionsPlanList.Count > 0)
                    {
                        for (int j = 0; j < fL_QuotedCollectionsPlanList.Count; j++)
                        {
                            ObjEntity.FL_QuotedCollectionsPlan.Remove(fL_QuotedCollectionsPlanList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //报价单
                    ObjEntity.FL_QuotedPrice.Remove(fL_QuotedPriceList[i]);
                }
                ObjEntity.SaveChanges();
            }
            #endregion

            #region 跟单详细、订单评价、定金金额、合同总额、财务、婚礼统筹、取件、订单


            List<FL_Order> fL_OrderList = ObjEntity.FL_Order.Where(C => C.CustomerID.Value == CustomerID).ToList();
            if (fL_OrderList != null)
            {
                for (int i = 0; i < fL_OrderList.Count; i++)
                {
                    int OrderID = fL_OrderList[i].OrderID;
                    //跟单详细
                    List<FL_OrderDetails> fL_OrderDetailsList = ObjEntity.FL_OrderDetails.Where(C => C.OrderID.Value == OrderID || C.CustomerID.Value == CustomerID).ToList();
                    if (fL_OrderDetailsList != null)
                    {
                        for (int j = 0; j < fL_OrderDetailsList.Count; j++)
                        {
                            ObjEntity.FL_OrderDetails.Remove(fL_OrderDetailsList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }
                    //订单评价
                    List<FL_OrderAppraise> fL_fL_OrderAppraiseList = ObjEntity.FL_OrderAppraise.Where(C => C.OrderID.Value == OrderID).ToList();
                    if (fL_fL_OrderAppraiseList != null)
                    {
                        for (int j = 0; j < fL_fL_OrderAppraiseList.Count; j++)
                        {
                            ObjEntity.FL_OrderAppraise.Remove(fL_fL_OrderAppraiseList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //定金
                    List<FL_OrderEarnestMoney> fL_OrderEarnestMoneyList = ObjEntity.FL_OrderEarnestMoney.Where(C => C.OrderID == OrderID).ToList();
                    if (fL_OrderEarnestMoneyList != null)
                    {
                        for (int j = 0; j < fL_OrderEarnestMoneyList.Count; j++)
                        {
                            ObjEntity.FL_OrderEarnestMoney.Remove(fL_OrderEarnestMoneyList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //合同
                    List<FL_OrderfinalCost> fL_OrderfinalCostList = ObjEntity.FL_OrderfinalCost.Where(C => C.CustomerID.Value == CustomerID).ToList();
                    if (fL_OrderfinalCostList != null)
                    {
                        for (int j = 0; j < fL_OrderfinalCostList.Count; j++)
                        {
                            ObjEntity.FL_OrderfinalCost.Remove(fL_OrderfinalCostList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }
                    //财务
                    List<FL_Cost> fL_CostList = ObjEntity.FL_Cost.Where(C => C.OrderID.Value == OrderID || C.CustomerID.Value == CustomerID).ToList();
                    if (fL_CostList != null)
                    {
                        for (int j = 0; j < fL_CostList.Count; j++)
                        {
                            ObjEntity.FL_Cost.Remove(fL_CostList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //婚礼统筹
                    List<FL_WeddingPlanning> fL_WeddingPlanningList = ObjEntity.FL_WeddingPlanning.Where(C => C.OrderID.Value == OrderID).ToList();
                    if (fL_WeddingPlanningList != null && fL_WeddingPlanningList.Count > 0)
                    {
                        for (int j = 0; j < fL_WeddingPlanningList.Count; j++)
                        {
                            int PlanningID = fL_WeddingPlanningList[j].PlanningID;
                            //婚礼统筹文件
                            List<FL_WeddingPlanFile> fL_WeddingPlanFileList = ObjEntity.FL_WeddingPlanFile.Where(C => C.PlanningID == PlanningID).ToList();
                            if (fL_WeddingPlanFileList != null && fL_WeddingPlanFileList.Count > 0)
                            {
                                for (int k = 0; k < fL_WeddingPlanFileList.Count; k++)
                                {
                                    //1.删除文件
                                    if (File.Exists(HttpContext.Current.Server.MapPath(fL_WeddingPlanFileList[i].FileAddress + string.Empty)))
                                    {
                                        File.Delete(HttpContext.Current.Server.MapPath(fL_WeddingPlanFileList[i].FileAddress));
                                    }
                                    //2.删除数据库记录
                                    ObjEntity.FL_WeddingPlanFile.Remove(fL_WeddingPlanFileList[k]);
                                }
                                ObjEntity.SaveChanges();
                            }
                            ObjEntity.FL_WeddingPlanning.Remove(fL_WeddingPlanningList[i]);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //取件
                    List<CS_TakeDisk> cS_TakeDiskList = ObjEntity.CS_TakeDisk.Where(C => C.OrderID == OrderID || C.CustomerID == CustomerID).ToList();
                    if (cS_TakeDiskList != null)
                    {
                        for (int j = 0; j < cS_TakeDiskList.Count; j++)
                        {
                            ObjEntity.CS_TakeDisk.Remove(cS_TakeDiskList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }
                    ////执行明细
                    //List<FL_Celebration> fL_CelebrationList = ObjEntity.FL_Celebration.Where(C => C.OrderID == OrderID || C.CustomerID == CustomerID).ToList();
                    //if (fL_CelebrationList != null)
                    //{
                    //    for (int j = 0; j < fL_CelebrationList.Count; j++)
                    //    {
                    //        int CelebrationID = fL_CelebrationList[j].CelebrationID;
                    //        List<FL_CelebrationProductItem> fL_CelebrationProductItemList = ObjEntity.FL_CelebrationProductItem.Where(C => C.CelebrationID == CelebrationID).ToList();
                    //        if (fL_CelebrationProductItemList != null)
                    //        {
                    //            for (int k = 0; k < fL_CelebrationProductItemList.Count; k++)
                    //            {
                    //                ObjEntity.FL_CelebrationProductItem.Remove(fL_CelebrationProductItemList[k]);
                    //            }
                    //            ObjEntity.SaveChanges();
                    //        }
                    //        ObjEntity.FL_Celebration.Remove(fL_CelebrationList[j]);
                    //    }
                    //    ObjEntity.SaveChanges();
                    //}

                    //订单
                    ObjEntity.FL_Order.Remove(fL_OrderList[i]);
                }
                ObjEntity.SaveChanges();
            }
            #endregion

            #region 邀约、电话销售

            //邀约
            List<FL_Invite> fL_InviteList = ObjEntity.FL_Invite.Where(C => C.CustomerID == CustomerID).ToList();
            if (fL_InviteList != null)
            {
                for (int i = 0; i < fL_InviteList.Count; i++)
                {
                    int InviteID = fL_InviteList[i].InviteID;
                    //邀约详细
                    List<FL_InvtieContent> fL_InvtieContentList = ObjEntity.FL_InvtieContent.Where(C => C.InviteID.Value == InviteID || C.CustomerID.Value == CustomerID).ToList();
                    if (fL_InvtieContentList != null)
                    {
                        for (int j = 0; j < fL_InvtieContentList.Count; j++)
                        {
                            ObjEntity.FL_InvtieContent.Remove(fL_InvtieContentList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }
                    ObjEntity.FL_Invite.Remove(fL_InviteList[i]);
                }
                ObjEntity.SaveChanges();
            }

            //电话销售
            List<FL_Telemarketing> fL_TelemarketingList = ObjEntity.FL_Telemarketing.Where(C => C.CustomerID == CustomerID).ToList();
            if (fL_TelemarketingList != null)
            {
                for (int i = 0; i < fL_TelemarketingList.Count; i++)
                {
                    ObjEntity.FL_Telemarketing.Remove(fL_TelemarketingList[i]);
                }
                ObjEntity.SaveChanges();
            }
            #endregion

            #region 任务
            new MissionManager().DeleteByCustomerID(CustomerID);
            #endregion


            #region 成本表  结算表
            //成本表
            List<FL_CostSum> fl_costSumList = ObjEntity.FL_CostSum.Where(C => C.CustomerId == CustomerID).ToList();
            if (fl_costSumList.Count > 0)
            {
                for (int i = 0; i < fl_costSumList.Count; i++)
                {
                    ObjEntity.FL_CostSum.Remove(fl_costSumList[i]);
                }
                ObjEntity.SaveChanges();
            }

            //结算表
            List<FL_Statement> fl_statementlist = ObjEntity.FL_Statement.Where(C => C.CustomerID == CustomerID).ToList();
            if (fl_costSumList.Count > 0)
            {
                for (int i = 0; i < fl_statementlist.Count; i++)
                {
                    ObjEntity.FL_Statement.Remove(fl_statementlist[i]);
                }
                ObjEntity.SaveChanges();
            }

            #endregion

            #region 返利核算、警告消息、投诉、满意度调查、新人回访、会员服务、新人信息

            //返利核算
            List<FD_PayNeedRabate> payNeedRabateList = ObjEntity.FD_PayNeedRabate.Where(C => C.CustomerID.Value == CustomerID).ToList();
            if (payNeedRabateList != null)
            {
                for (int i = 0; i < payNeedRabateList.Count; i++)
                {
                    ObjEntity.FD_PayNeedRabate.Remove(payNeedRabateList[i]);
                }
                ObjEntity.SaveChanges();
            }

            //警告消息
            List<FL_WarningMessage> fL_WarningMessageList = ObjEntity.FL_WarningMessage.Where(C => C.CustomerID.Value == CustomerID).ToList();
            if (fL_WarningMessageList != null)
            {
                for (int i = 0; i < fL_WarningMessageList.Count; i++)
                {
                    ObjEntity.FL_WarningMessage.Remove(fL_WarningMessageList[i]);
                }
                ObjEntity.SaveChanges();
            }
            //投诉
            List<CS_Complain> cS_ComplainList = ObjEntity.CS_Complain.Where(C => C.CustomerID.Value == CustomerID).ToList();
            if (cS_ComplainList != null)
            {
                for (int i = 0; i < cS_ComplainList.Count; i++)
                {
                    ObjEntity.CS_Complain.Remove(cS_ComplainList[i]);
                }
                ObjEntity.SaveChanges();
            }

            //满意度调查
            List<CS_DegreeOfSatisfaction> cS_DegreeOfSatisfactionList = ObjEntity.CS_DegreeOfSatisfaction.Where(C => C.CustomerID.Value == CustomerID).ToList();
            if (cS_DegreeOfSatisfactionList != null)
            {
                for (int i = 0; i < cS_DegreeOfSatisfactionList.Count; i++)
                {
                    int DofKey = cS_DegreeOfSatisfactionList[i].DofKey;
                    //满意度调查内容
                    List<CS_DegreeOfSatisfactionContent> cS_DegreeOfSatisfactionContentList = ObjEntity.CS_DegreeOfSatisfactionContent.Where(C => C.DofKey == DofKey).ToList();
                    if (cS_DegreeOfSatisfactionContentList != null)
                    {
                        for (int j = 0; j < cS_DegreeOfSatisfactionContentList.Count; j++)
                        {
                            ObjEntity.CS_DegreeOfSatisfactionContent.Remove(cS_DegreeOfSatisfactionContentList[j]);
                        }
                        ObjEntity.SaveChanges();
                    }
                    ObjEntity.CS_DegreeOfSatisfaction.Remove(cS_DegreeOfSatisfactionList[i]);
                }
                ObjEntity.SaveChanges();
            }

            //新人回访
            List<FL_CustomerReturnVisit> customerReturnVisitList = ObjEntity.FL_CustomerReturnVisit.Where(C => C.CustomerId == CustomerID).ToList();
            if (customerReturnVisitList != null)
            {
                for (int i = 0; i < customerReturnVisitList.Count; i++)
                {
                    ObjEntity.FL_CustomerReturnVisit.Remove(customerReturnVisitList[i]);
                }
                ObjEntity.SaveChanges();
            }

            //会员服务
            List<CS_Member> cS_MemberList = ObjEntity.CS_Member.Where(C => C.CustomerID == CustomerID).ToList();
            if (cS_MemberList != null)
            {
                for (int i = 0; i < cS_MemberList.Count; i++)
                {
                    ObjEntity.CS_Member.Remove(cS_MemberList[i]);
                }
                ObjEntity.SaveChanges();
            }

            //成本表
            List<FL_CostSum> CostSumList = ObjEntity.FL_CostSum.Where(C => C.CustomerId == CustomerID).ToList();
            if (CostSumList.Count > 0)
            {
                for (int i = 0; i < CostSumList.Count; i++)
                {

                    ObjEntity.FL_CostSum.Remove(CostSumList[i]);
                }
                ObjEntity.SaveChanges();
            }


            //结算表
            List<FL_Statement> StatementList = ObjEntity.FL_Statement.Where(C => C.CustomerID == CustomerID).ToList();
            if (CostSumList.Count > 0)
            {
                for (int i = 0; i < StatementList.Count; i++)
                {

                    ObjEntity.FL_Statement.Remove(StatementList[i]);
                }
                ObjEntity.SaveChanges();
            }


            //新人信息
            //FL_Customers fL_Customers = ObjEntity.FL_Customers.Where(C => C.CustomerID == CustomerID).FirstOrDefault();
            //if (fL_Customers != null)
            //{
            //    ObjEntity.FL_Customers.Remove(fL_Customers);
            //}
            //ObjEntity.SaveChanges();

            //删除消息和警告消息
            Message ObjMessageBLL = new Message();
            ObjMessageBLL.DeleteByCustomerID(CustomerID);
            #endregion
        }



        //public List<CustomerCost> GetCostAll() 
        //{
        //    return ObjEntity.CustomerCost.ToList();
        //}
        ///// <summary>
        ///// 利润率
        ///// </summary>
        ///// <param name="ObjParameterList"></param>
        ///// <returns></returns>
        //public List<CustomerCost> GetbyCustomerCostParameter(ObjectParameter[] ObjParameterList)
        //{
        //    var query = PublicDataTools<CustomerCost>.GetDataByParameter(new CustomerCost(), ObjParameterList);

        //    return query.ToList();
        //}

        /// <summary>
        /// 根据参数组获取新人信息
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public FL_Customers GetOnlybyParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<FL_Customers>.GetDataByParameter(new FL_Customers(), ObjParameterList);

            return query.FirstOrDefault();
        }

        public FL_Customers GetDataByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {
            var DataList = PublicDataTools<FL_Customers>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

            return DataList.FirstOrDefault();
        }


        /// <summary>
        /// 根据名字或者电话查询客户
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public FL_Customers GetOnlyByNameOrPhone(string Name, string Phone)
        {
            var ObjListModel = ObjEntity.FL_Customers.Where(C => C.Bride == Name || C.Groom == Name || C.Operator == Name);
            var OnlyModel = ObjListModel.FirstOrDefault(C => C.BrideCellPhone == Phone || C.GroomCellPhone == Phone || C.OperatorPhone == Phone);
            if (OnlyModel != null)
            {
                return OnlyModel;
            }
            else
            {
                return new FL_Customers();
            }
        }

        /// <summary>
        /// 根据电话查询
        /// </summary>
        public FL_Customers GetOnlyByPhone(string Phone)
        {
            var OnlyModel = ObjEntity.FL_Customers.FirstOrDefault(C => C.BrideCellPhone == Phone || C.GroomCellPhone == Phone || C.OperatorPhone == Phone);
            if (OnlyModel != null)
            {
                return OnlyModel;
            }
            else
            {
                return null;
            }
        }

        public List<FL_Cost> GetCustomerCostAll()
        {
            return ObjEntity.FL_Cost.ToList();
        }


        /// <summary>
        /// 客户明细查询方法
        /// </summary>
        /// <param name="listCustomerId"></param>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        [Obsolete("请使用GetByWhereParameter")]
        public List<FL_Customers> GetbyParameter(List<int> listCustomerId, ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {


            List<FL_Customers> list = GetCurrentbyParameter(listCustomerId, ObjParameterList);
            SourceCount = list.Count();

            List<FL_Customers> resultList = list.OrderByDescending(C => C.CustomerID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (list.Count == 0)
            {
                resultList = new List<FL_Customers>();
            }
            return resultList;
        }



        public List<View_SSCustomer> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            var ReturnList = PublicDataTools<View_SSCustomer>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);
            return ReturnList;

        }

        /// <summary>
        /// 返回根据参数含支付时间的参数返回当前新人
        /// </summary>
        /// <param name="listCustomerId"></param>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<FL_Customers> GetCurrentbyParameter(List<int> listCustomerId, ObjectParameter[] ObjParameterList)
        {
            var query = GetAllByParameter(ObjParameterList);

            List<FL_Customers> list = new List<FL_Customers>();
            FL_Customers customer;
            foreach (var cid in listCustomerId)
            {
                customer = query.Where(C => C.CustomerID == cid).FirstOrDefault();
                if (customer != null)
                {
                    list.Add(customer);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据参入参数查询所有客户相关的信息
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<FL_Customers> GetAllByParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<FL_Customers>.GetDataByParameter(new FL_Customers(), ObjParameterList)
              .Where(C => C.IsDelete == false);
            return query.ToList();
        }


        /// <summary>
        /// 根据条件查询新人档案
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<FL_Customers> GetBirthCustomer()
        {
            DateTime EndDate = DateTime.Now;
            int EndDay = EndDate.Day + 7;
            int StarDay = DateTime.Now.Day;
            int EndMonth = EndDate.Month;
            var GroomList = (from S in ObjEntity.FL_Customers
                             where S.GroomBirthday != null && S.GroomBirthday.Value.Month == EndMonth
                             select S).ToList();
            GroomList = GroomList.Where(C => C.GroomBirthday.Value.Day <= EndDay && C.GroomBirthday.Value.Day >= StarDay).ToList();
            foreach (var Objitem in GroomList)
            {
                Objitem.Bride = "新郎：" + Objitem.Groom;
                Objitem.BrideBirthday = Objitem.GroomBirthday;
            }


            var BrideList = (from S in ObjEntity.FL_Customers
                             where S.BrideBirthday != null && S.BrideBirthday.Value.Month == EndMonth
                             select S).ToList();
            BrideList = BrideList.Where(C => C.BrideBirthday.Value.Day <= EndDay && C.BrideBirthday.Value.Day >= StarDay).ToList();
            GroomList.AddRange(BrideList);
            return GroomList;

            //    ObjEntity.View_SSCustomer.Where(C=>C.BrideBirthday!=null||C.GroomBirthday!=null).

        }


        /// <summary>
        /// 根据婚期查询
        /// </summary>
        public List<View_CustomerQuoted> GetBirthCustomers(List<View_CustomerQuoted> DataList, int StartMonth, int EndMonth)
        {

            //int StartMonth = StartDate.Month;
            //int EndMonth = EndDate.Month;

            var GroomList = (from S in DataList
                             where S.GroomBirthday != null && S.GroomBirthday.Value.Month >= StartMonth && S.GroomBirthday.Value.Month <= EndMonth
                             select S).ToList();
            var BrideList = (from S in DataList
                             where S.BrideBirthday != null && S.BrideBirthday.Value.Month >= StartMonth && S.BrideBirthday.Value.Month <= EndMonth
                             select S).ToList();
            GroomList.AddRange(BrideList);
            return GroomList;
        }


        public List<FL_Customers> GetBrideBirthCustomer()
        {
            DateTime EndDate = DateTime.Now;
            int EndDay = EndDate.Day + 7;
            ObjEntity.Dispose();
            ObjEntity = new PMS_WeddingEntities();
            var BrideList = (from S in ObjEntity.FL_Customers
                             where System.Data.Objects.EntityFunctions.DiffMonths(EndDate, S.BrideBirthday) == 0 && S.BrideBirthday != null
                             select S).ToList();
            BrideList = BrideList.Where(C => C.BrideBirthday.Value.Day <= EndDay).ToList();
            return BrideList;

            //    ObjEntity.View_SSCustomer.Where(C=>C.BrideBirthday!=null||C.GroomBirthday!=null).

        }


        /// <summary>
        /// 根据条件查询新人档案
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<View_SSCustomer> GetCustomerByPmsParameter(List<PMSParameters> ObjParameterList)
        {
            int SourceCOunt = 0;
            return PublicDataTools<View_SSCustomer>.GetDataByWhereParameter(ObjParameterList, "CustomerID", 10000000, 0, out SourceCOunt);

        }

        public List<FL_Customers> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FL_Customers>.GetDataByParameter(new FL_Customers(), ObjParameterList);
            SourceCount = query.Count();

            List<FL_Customers> resultList = query.OrderByDescending(C => C.CustomerID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FL_Customers>();
            }
            return resultList;



        }





        /// <summary>
        /// 获取已经开始销售跟单的用户
        /// </summary>
        /// <returns></returns>
        public List<View_GetOrderCustomers> GetOrderCustomers(int PageSize, int PageIndex, out int SourceCount, CustomerStates States)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.View_GetOrderCustomers.Count(C => C.State == (int)States);

            List<View_GetOrderCustomers> resultList = ObjEntity.View_GetOrderCustomers.
                Where(C => C.State == (int)States)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_GetOrderCustomers>();
            }
            return resultList;
        }

        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <returns></returns>
        public List<FL_Customers> GetByAll()
        {
            var DataList = ObjEntity.FL_Customers.Where(C => C.IsDelete == false).ToList();
            return DataList;
        }


        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <returns></returns>
        public List<View_SSCustomer> GetAllViewCusomter()
        {
            var DataList = ObjEntity.View_SSCustomer.Where(C => C.IsDelete == false).ToList();
            return DataList;
        }


        /// <summary>
        /// 根据客户ID获取客户
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Customers GetByID(int? KeyID)
        {
            return ObjEntity.FL_Customers.FirstOrDefault(C => C.CustomerID == KeyID);
        }

        public List<FL_Customers> GestByID(int? KeyID)
        {
            return ObjEntity.FL_Customers.Where(C => C.CustomerID == KeyID).ToList();
        }


        /// <summary>
        /// 分页获取客户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_Customers> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_Customers.Count();

            List<FL_Customers> resultList = ObjEntity.FL_Customers
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_Customers>();
            }
            return resultList;
        }

        public List<FL_Customers> GetByIndex(int PageSize, int PageIndex, out int SourceCount, ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<FL_Customers>.GetDataByParameter(new FL_Customers(), ObjParameterList);
            SourceCount = query.Count();

            List<FL_Customers> resultList = query
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_Customers>();
            }
            return resultList;
        }



        /// <summary>
        /// 分页获取成功邀请的电话营销用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetTelmarketingCustomers> GetTelCustomerByStateIndex(int PageSize, int PageIndex, out int SourceCount, CustomerStates States)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.FL_Customers.Count(C => C.State == (int)States);

            List<View_GetTelmarketingCustomers> resultList = ObjEntity.View_GetTelmarketingCustomers.
                Where(C => C.State == (int)States)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_GetTelmarketingCustomers>();
            }
            return resultList;
        }

        /// <summary>
        /// 获取正在邀约的客户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="States"></param>
        /// <returns></returns>
        public List<View_GetInviteCustomers> GetInviteCustomerByStateIndex(int PageSize, int PageIndex, out int SourceCount, CustomerStates States)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.FL_Customers.Count(C => C.State == (int)States);

            List<View_GetInviteCustomers> resultList = ObjEntity.View_GetInviteCustomers.
                Where(C => C.State == (int)States)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_GetInviteCustomers>();
            }
            return resultList;
        }




        /// <summary>
        /// 获取正在制作报价单的客户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="States"></param>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetCustomerQuotedByStateIndex(int PageSize, int PageIndex, out int SourceCount, CustomerStates States)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.FL_Customers.Count(C => C.State == (int)States);

            List<View_CustomerQuoted> resultList = ObjEntity.View_CustomerQuoted.
                Where(C => C.State == (int)States)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_CustomerQuoted>();
            }
            return resultList;
        }

        /// <summary>
        /// 根据审核状态获取正在制作报价单的客户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="States"></param>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetCustomerQuotedByCheckStateIndex(int PageSize, int PageIndex, out int SourceCount, int States, int CheckEmpLoyeeID)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.FL_Customers.Count(C => C.State == (int)States);

            List<View_CustomerQuoted> resultList = ObjEntity.View_CustomerQuoted.
                Where(C => C.CheckState == States && C.ChecksEmployee == CheckEmpLoyeeID)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_CustomerQuoted>();
            }
            return resultList;
        }


        /// <summary>
        /// 获取责任人的报价单列表
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="States"></param>
        /// <returns></returns>
        public List<View_CustomerQuoted> GetCustomerQuotedByEmpLoyeeID(int PageSize, int PageIndex, out int SourceCount, int EmpLoyeeID)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.FL_Customers.Count(C => C.State == 3);

            List<View_CustomerQuoted> resultList = ObjEntity.View_CustomerQuoted.
                Where(C => C.CheckState == 3 && C.EmpLoyeeID == EmpLoyeeID)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_CustomerQuoted>();
            }
            return resultList;
        }
        /// <summary>
        /// 分页按状态获取客户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_Customers> GetCustomerByStateIndex(int PageSize, int PageIndex, out int SourceCount, CustomerStates States)
        {
            PageIndex = PageIndex - 1;

            SourceCount = ObjEntity.FL_Customers.Count(C => C.State == (int)States);

            List<FL_Customers> resultList = ObjEntity.FL_Customers.
                Where(C => C.State == (int)States)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.CustomerID)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_Customers>();
            }
            return resultList;
        }


        /// <summary>
        /// 添加录入客户信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Customers ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Customers.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.CustomerID;
                }

            }
            return 0;
        }
        /// <summary>
        /// 根据新郎的姓名进行查询
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int GetExistCustomers(FL_Customers ObjectT)
        {
            if (ObjectT != null)
            {
                var query = from m in ObjEntity.FL_Customers
                            where m.Groom == ObjectT.Groom ||
                                  m.GroomCellPhone == ObjectT.GroomCellPhone
                            select m;
                return query.Count();
            }
            return 0;
        }

        /// <summary>
        /// 修改客户基本信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_Customers ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.CustomerID;
            }
            return 0;
        }


        /// <summary>
        /// 根据姓名查询客户是否存在
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<FL_Customers> GetbyParameter(ObjectParameter[] ObjParameterList)
        {
            return PublicDataTools<FL_Customers>.GetDataByParameter(new FL_Customers(), ObjParameterList);
        }


        public IEnumerable<FL_Customers> Where(Func<FL_Customers, bool> predicate)
        {
            return ObjEntity.FL_Customers.Where(predicate);
        }


        #region 基于 Repositoy

        /// <summary>
        /// 根据婚期返回新人 ID 集合。
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<int> GetCustomerIDsByPartyDate(DateTime start, DateTime end)
        {
            return ObjEntity.FL_Customers.Where(C => C.PartyDate >= start && C.PartyDate <= end).Select(C => C.CustomerID).ToList();
        }

        public List<int> GetCustomerIDsByMonthOfYear(int year, int month)
        {
            DateTime start = new DateTime(year, month, 1);
            DateTime end = new DateTime(year, month, 1).AddMonths(1);
            return ObjEntity.FL_Customers.Where(C => C.PartyDate >= start && C.PartyDate < end).Select(C => C.CustomerID).ToList();
        }

        /// <summary>
        /// 根据新人 ID 返回新人婚期。
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public DateTime? GetPartyDate(object customerid)
        {
            int customerID = Convert.ToInt32(customerid);
            FL_Customers fL_Customers = ObjEntity.FL_Customers.Where(C => C.CustomerID == customerID).FirstOrDefault();
            return fL_Customers != null ? fL_Customers.PartyDate : null;
        }

        public bool IsPartyDateOver(int customerid)
        {
            DateTime today = DateTime.Now.Date;
            FL_Customers fL_Customers = ObjEntity.FL_Customers.Where(C => C.CustomerID == customerid).FirstOrDefault();
            return fL_Customers != null ? Convert.ToDateTime(fL_Customers.PartyDate) < today : true;
        }

        #endregion


        DBHelper db = new DBHelper();
        public int UpdateCustomer(FL_Customers Customer)
        {
            //SqlParameter[] pars = new SqlParameter[]
            //{
            //    new SqlParameter("@FinishOver",Customer.FinishOver),
            //    new SqlParameter("@State",Customer.State),
            //    new SqlParameter("@CustomerID",Customer.State),
            //};
            //string sql = "Update FL_Customers set FinishiOver=@FinishOver,State=@State where CustomerID=@CustomerID";
            int finishOver = 0;
            if (Customer.FinishOver == true)
            {
                finishOver = 1;
            }
            else if (Customer.FinishOver == false)
            {
                finishOver = 0;
            }
            string sql = string.Format("Update FL_Customers set FinishOver={0},State={1} where CustomerID={2}", finishOver, Customer.State, Customer.CustomerID);
            int result = db.ExcuteNonQuery(sql);
            return result;

        }

        /// <summary>
        /// 获取婚期已过的所有客户  修改状态
        /// </summary>
        /// <returns></returns>
        public List<FL_Customers> GetByPartyDateAll()
        {
            var DataList = ObjEntity.FL_Customers.Where(C => C.IsDelete == false && C.State != 206 && C.State != 29).ToList();
            return DataList;
        }

        public List<View_SSCustomer> GetAllByPartyDate(DateTime PartyDate)
        {
            return ObjEntity.View_SSCustomer.Where(C => C.Partydate == PartyDate).ToList();
        }


        /// <summary>
        /// 根据客户ID获取客户的信息
        /// </summary>
        public View_SSCustomer GetByCustomerID(int CustomerID)
        {
            return ObjEntity.View_SSCustomer.FirstOrDefault(C => C.CustomerID == CustomerID);
        }

        public List<View_CustomerQuoted> GetByAllView()
        {
            return null;
        }


        /// <summary>
        /// 成本明细 获取订单金额
        /// </summary>
        /// <returns></returns>
        public string GetOrderSum(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)
            {
                return (from C in ObjEntity.FL_QuotedPrice
                        join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                        where D.FinishOver == true && D.State == 206 && (D.Partydate >= Start && D.Partydate <= End)
                        select C).Sum(C => C.FinishAmount).ToString();
            }
            else            //查询某员工的订单金额
            {
                return (from C in ObjEntity.FL_QuotedPrice
                        join D in ObjEntity.View_SSCustomer on C.CustomerID equals D.CustomerID
                        where D.FinishOver == true && D.State == 206 && C.EmpLoyeeID == EmployeeID && (D.Partydate >= Start && D.Partydate <= End)
                        select C).Sum(C => C.FinishAmount).ToString();
            }

        }


        /// <summary>
        /// 成本明细 获取成本
        /// </summary>
        /// <returns></returns>
        public string GetCostSum(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)
            {
                return (from C in ObjEntity.FL_CostSum
                        join D in ObjEntity.View_SSCustomer on C.CustomerId equals D.CustomerID
                        where D.FinishOver == true && D.State == 206 && (D.Partydate >= Start && D.Partydate <= End)
                        select C).Sum(C => C.ActualSumTotal).ToString();
            }
            else
            {
                return (from C in ObjEntity.FL_CostSum
                        join D in ObjEntity.View_SSCustomer on C.CustomerId equals D.CustomerID
                        where D.FinishOver == true && D.State == 206 && D.QuotedEmployee == EmployeeID && (D.Partydate >= Start && D.Partydate <= End)
                        select C).Sum(C => C.ActualSumTotal).ToString();
            }
        }



        #region 获取新录入
        /// <summary>
        /// 新录入
        /// </summary>
        public int GetNumByToday(int EmployeeID, DateTime Start, DateTime End)
        {
            return ObjEntity.FL_Customers.Where(C => C.Recorder == EmployeeID && C.RecorderDate >= Start && C.RecorderDate <= End).ToList().Count;
        }

        public int GetSumByToday(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.FL_Customers.Where(C => C.RecorderDate >= Start && C.RecorderDate <= End).ToList().Count;
            }
            else
            {
                return ObjEntity.FL_Customers.Where(C => C.Recorder == EmployeeID && C.RecorderDate >= Start && C.RecorderDate <= End).ToList().Count;
            }
        }
        #endregion

        #region 获取电销量
        /// <summary>
        /// 获取电销量
        /// </summary>
        public int GetInviteNumByToday(int EmployeeID, DateTime Start, DateTime End)
        {
            return ObjEntity.FL_Invite.Where(C => C.EmpLoyeeID == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End).ToList().Count; ;
        }

        public int GetInviteSumByToday(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.FL_Invite.Where(C => C.LastFollowDate >= Start && C.LastFollowDate <= End).ToList().Count;
            }
            else
            {
                return ObjEntity.FL_Invite.Where(C => C.EmpLoyeeID == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End).ToList().Count; ;
            }
        }
        #endregion

        #region 获取流失 邀约成功量(邀约)
        /// <summary>
        /// 获取流失量(邀约中) 邀约成功
        /// </summary>
        public int GetInviteNumByToday(int EmployeeID, DateTime Start, DateTime End, int State)
        {
            return ObjEntity.View_SSCustomer.Where(C => C.InviteEmployee == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End && C.State == State).ToList().Count; ;
        }

        public int GetInviteSumByTodays(DateTime Start, DateTime End, int State, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.View_SSCustomer.Where(C => C.LastFollowDate >= Start && C.LastFollowDate <= End && C.State == State).ToList().Count; ;
            }
            else
            {
                return ObjEntity.View_SSCustomer.Where(C => C.InviteEmployee == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End && C.State == State).ToList().Count; ;
            }

        }
        #endregion

        #region 获取跟单量
        /// <summary>
        /// 获取跟单量
        /// </summary>
        public int GetOrderNumByToday(int EmployeeID, DateTime Start, DateTime End)
        {
            return ObjEntity.FL_Order.Where(C => C.EmployeeID == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End).ToList().Count; ;
        }

        public int GetOrderSumByToday(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.FL_Order.Where(C => C.LastFollowDate >= Start && C.LastFollowDate <= End).ToList().Count; ;
            }
            else
            {
                return ObjEntity.FL_Order.Where(C => C.EmployeeID == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End).ToList().Count; ;
            }
        }
        #endregion

        #region 获取流失量(跟单) 成功预订
        /// <summary>
        /// 获取流失量(跟单) 成功预订
        /// </summary>
        public int GetOrderNumByToday(int EmployeeID, DateTime Start, DateTime End, int State)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.OrderEmployee == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End && C.State == State).ToList().Count;
        }

        public int GetOrderSumByToday(DateTime Start, DateTime End, int State, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.View_CustomerQuoted.Where(C => C.LastFollowDate >= Start && C.LastFollowDate <= End && C.State == State).ToList().Count;
            }
            else
            {
                return ObjEntity.View_CustomerQuoted.Where(C => C.OrderEmployee == EmployeeID && C.LastFollowDate >= Start && C.LastFollowDate <= End && C.State == State).ToList().Count;
            }
        }
        #endregion

        #region 获取确认签约量
        /// <summary>
        /// 获获取确认签约量
        /// </summary>
        public int GetQuotedNumByToday(int EmployeeID, DateTime Start, DateTime End)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.QuotedEmployee == EmployeeID && C.QuotedDateSucessDate >= Start && C.QuotedDateSucessDate <= End && C.State == 15).ToList().Count;
        }

        public int GetQuotedSumByToday(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.View_CustomerQuoted.Where(C => C.QuotedDateSucessDate >= Start && C.QuotedDateSucessDate <= End).ToList().Count;
            }
            else
            {
                return ObjEntity.View_CustomerQuoted.Where(C => C.QuotedEmployee == EmployeeID && C.QuotedDateSucessDate >= Start && C.QuotedDateSucessDate <= End).ToList().Count;
            }
        }
        #endregion

        #region 获取现金流
        /// <summary>
        /// 获获取确认签约量
        /// </summary>
        public decimal GetFinishAmountByToday(int EmployeeID, DateTime Start, DateTime End)
        {
            return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CreateEmpLoyee == EmployeeID && C.CollectionTime >= Start && C.CollectionTime <= End).Sum(C => C.RealityAmount).ToString().ToDecimal();
        }

        public decimal GetFinishAmountSumByToday(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CollectionTime >= Start && C.CollectionTime <= End).Sum(C => C.RealityAmount).ToString().ToDecimal();
            }
            else
            {
                return ObjEntity.FL_QuotedCollectionsPlan.Where(C => C.CreateEmpLoyee == EmployeeID && C.CollectionTime >= Start && C.CollectionTime <= End).Sum(C => C.RealityAmount).ToString().ToDecimal();
            }
        }
        #endregion

        #region 获取订单金额
        /// <summary>
        /// 获取订单金额
        /// </summary>
        public decimal GetOrderAmountByToday(int EmployeeID, DateTime Start, DateTime End)
        {
            return ObjEntity.View_CustomerQuoted.Where(C => C.QuotedEmployee == EmployeeID && C.QuotedDateSucessDate >= Start && C.QuotedDateSucessDate <= End).Sum(C => C.FinishAmount).ToString().ToDecimal();
        }

        public decimal GetOrderAmountSumByToday(DateTime Start, DateTime End, int EmployeeID = 0)
        {
            if (EmployeeID == 0)        //没有选择责任人
            {
                return ObjEntity.View_CustomerQuoted.Where(C => C.QuotedDateSucessDate >= Start && C.QuotedDateSucessDate <= End).Sum(C => C.FinishAmount).ToString().ToDecimal();
            }
            else
            {
                return ObjEntity.View_CustomerQuoted.Where(C => C.QuotedEmployee == EmployeeID && C.QuotedDateSucessDate >= Start && C.QuotedDateSucessDate <= End).Sum(C => C.FinishAmount).ToString().ToDecimal();
            }
        }
        #endregion

        #region 获取所有邀约客户
        /// <summary>
        /// 获取邀约所有客户（每日汇总） 时间没用
        /// </summary> 
        public int GetInviteSumTotal(int EmployeeID, DateTime Start, DateTime End)
        {
            int Sum = ObjEntity.View_SSCustomer.Where(C => C.InviteEmployee == EmployeeID && (C.State == 3 || C.State == 5)).ToList().Count;
            return Sum;
        }
        #endregion

        #region 获取所有跟单客户
        /// <summary>
        /// 获取所有跟单客户（每日汇总） 时间没用
        /// </summary>
        public int GetOrderSumTotal(int EmployeeID, DateTime Start, DateTime End)
        {
            int Sum = ObjEntity.View_SSCustomer.Where(C => C.OrderEmployee == EmployeeID && (C.State == 202 || C.State == 203 || C.State == 205 || C.State == 8 || C.State == 9)).ToList().Count;
            return Sum;
        }
        #endregion


    }

}
