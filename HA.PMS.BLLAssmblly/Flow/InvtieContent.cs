
/**
 Version :HaoAi 1.0
 File Name :Customers
 Author:黄晓可
 Date:2013.3.17
 Description:客户邀请沟通记录 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class InvtieContent : ICRUDInterface<FL_InvtieContent>
    {
        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Delete(FL_InvtieContent ObjectT)
        {

            ObjEntity.FL_InvtieContent.Remove(ObjectT);
            ObjEntity.SaveChanges();
            return 0;
        }

        public List<FL_InvtieContent> GetByAll()
        {
            return ObjEntity.FL_InvtieContent.ToList();
        }

        public FL_InvtieContent GetByID(int? KeyID)
        {
            return ObjEntity.FL_InvtieContent.FirstOrDefault(C => C.ContentID == KeyID);
        }


        /// <summary>
        /// 根据客ID获取沟通记录主体
        /// </summary>
        /// <returns></returns>
        public List<FL_InvtieContent> GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_InvtieContent.Where(C => C.CustomerID == CustomerID).ToList();
        }

        /// <summary>
        /// 根据主体ID获取沟通记录
        /// </summary>
        /// <returns></returns>
        public List<FL_InvtieContent> GetByInviteID(int InviteID)
        {
            return ObjEntity.FL_InvtieContent.Where(C => C.InviteID == InviteID).ToList();
        }

        /// <summary>
        /// 分页获取沟通记录
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<FL_InvtieContent> GetByIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {
            PageIndex = PageIndex - 1;

            var DataSource = PublicDataTools<FL_InvtieContent>.GetDataByParameter(new FL_InvtieContent(), ObjParList.ToArray()).OrderBy(C => C.CommunicationTime).ToList();

            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();
            if (SourceCount == 0)
            {
                DataSource = new List<FL_InvtieContent>();
            }
            return DataSource;
        }
        /// <summary>
        /// 添加邀约沟通信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_InvtieContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_InvtieContent.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ContentID;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 修改邀约沟通记录
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_InvtieContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ContentID;
            }
            return 0;
        }


        public List<FL_InvtieContent> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// 删除邀约跟踪信息
        /// </summary>
        /// <param name="CustomerID"></param>
        public void DeleteByCustomerID(int CustomerID)
        {

            //删除 邀约模块数据
            Invite ObjInviteBLL = new Invite();
            int InviteID = 0;
            var ObjInviteModel = ObjInviteBLL.GetByCustomerID(CustomerID);
            if (ObjInviteModel != null)
            {
                InviteID = ObjInviteModel.InviteID;
                //邀约跟踪信息
                var ObjInvtieContentList = ObjEntity.FL_InvtieContent.Where(C => C.InviteID == ObjInviteModel.InviteID || C.CustomerID == CustomerID).ToList();
                if (ObjInvtieContentList != null)
                {
                    foreach (var Item in ObjInvtieContentList)
                    {
                        ObjEntity.FL_InvtieContent.Remove(Item);
                    }
                    ObjEntity.SaveChanges();
                }
                ObjInviteBLL.Delete(ObjInviteModel);
            }

            //删除跟单模块信息
            Order ObjOrderBLL = new Order();
            int OrderID = 0;
            var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (ObjOrderModel != null)
            {
                OrderID = ObjOrderModel.OrderID;
                //跟踪信息
                var ObjOrderDetailsList = ObjEntity.FL_OrderDetails.Where(C => C.OrderID == ObjOrderModel.OrderID || C.CustomerID == CustomerID).ToList();
                if (ObjOrderDetailsList != null)
                {
                    foreach (var Item in ObjOrderDetailsList)
                    {
                        ObjEntity.FL_OrderDetails.Remove(Item);
                    }
                    ObjEntity.SaveChanges();
                }
                ObjOrderBLL.Delete(ObjOrderModel);
            }

             //删除婚礼统筹
            WeddingPlanning ObjWeddingPlanningBLL = new WeddingPlanning();
            var ObjWeddingPlanningList = ObjWeddingPlanningBLL.GetByOrderID(OrderID);
            List<int> PlanningIDs = new List<int>();
            if (ObjWeddingPlanningList != null && ObjWeddingPlanningList.Count > 0)
            {
                foreach (var Item in ObjWeddingPlanningList)
                {
                    PlanningIDs.Add(Item.PlanningID);
                    //婚礼统筹文件
                    var ObjWeddingPlanFileList = ObjEntity.FL_WeddingPlanFile.Where(C => C.PlanningID.Equals(Item.PlanningID));
                    if (ObjWeddingPlanFileList != null && ObjWeddingPlanFileList.Count() > 0)
                    {
                        foreach (var Item2 in ObjWeddingPlanFileList)
                        {
                            //1.删除硬盘文件
                            //2.删除数据库记录
                            ObjEntity.FL_WeddingPlanFile.Remove(Item2);
                        }
                        ObjEntity.SaveChanges();
                    }
                    ObjWeddingPlanningBLL.Delete(Item);
                }
                ObjEntity.SaveChanges();
            }


            //删除报价单主体
            QuotedPrice ObjQuotedPriceBLL = new QuotedPrice();
            var ObjQuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (ObjQuotedModel != null)
            {
                //删除报价单详细项目
                var ObjQuotedPriceItemsList = ObjEntity.FL_QuotedPriceItems.Where(C => C.QuotedID == ObjQuotedModel.QuotedID).ToList();
                if (ObjQuotedPriceItemsList != null)
                {
                    foreach (var Item in ObjQuotedPriceItemsList)
                    {
                        ObjEntity.FL_QuotedPriceItems.Remove(Item);
                    }
                    ObjEntity.SaveChanges();
                }
                
                //删除提案 图片
                var ObjQuotedPricefileManagerList = ObjEntity.FL_QuotedPricefileManager.Where(C => C.QuotedID == ObjQuotedModel.QuotedID).ToList();
                if (ObjQuotedPricefileManagerList != null)
                {
                    foreach (var Item in ObjQuotedPricefileManagerList)
                    {
                        ObjEntity.FL_QuotedPricefileManager.Remove(Item);
                    }
                    ObjEntity.SaveChanges();
                }

                ObjQuotedPriceBLL.Delete(ObjQuotedModel);
            }

            //删除执行表
            var ObjDispatchingList = ObjEntity.FL_Dispatching.Where(C => C.CustomerID == CustomerID).ToList();
            if (ObjDispatchingList!=null)
            {
                foreach (var Item in ObjDispatchingList)
                {
                    //执行状态
                    var ObjDispatchingStateList = ObjEntity.FL_DispatchingState.Where(C => C.DispatchingID == Item.DispatchingID).ToList();
                    if (ObjDispatchingStateList != null)
                    {
                        foreach (var ObjState in ObjDispatchingStateList)
                        {
                            ObjEntity.FL_DispatchingState.Remove(ObjState);
                        }
                        ObjEntity.SaveChanges();
                    }

                    //分配的产品
                    var ObjProductforDispatchingList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == Item.DispatchingID).ToList();
                    if (ObjProductforDispatchingList != null)
                    {
                        foreach (var ObjProductItem in ObjProductforDispatchingList)
                        {
                            ObjEntity.FL_ProductforDispatching.Remove(ObjProductItem);
                        }
                        ObjEntity.SaveChanges();
                    }
                    ObjEntity.FL_Dispatching.Remove(Item);
                }
                ObjEntity.SaveChanges();
            }

            //返利核算
            HA.PMS.BLLAssmblly.FD.PayNeedRabate ObjPayNeedRabateBLL = new FD.PayNeedRabate();
            var ObjPayNeedRabateModel = ObjPayNeedRabateBLL.GetByCustomersID(CustomerID);
            if (ObjPayNeedRabateModel != null)
            {
                ObjPayNeedRabateBLL.Delete(ObjPayNeedRabateModel);
            }
            
            Customers ObjCustomersBLL = new Customers();
            ObjCustomersBLL.Delete(ObjCustomersBLL.GetByID(CustomerID));

        }
    }
}
