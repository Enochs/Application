using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
//订单评价信息

namespace HA.PMS.BLLAssmblly.Flow
{
    public class OrderAppraise:ICRUDInterface<FL_OrderAppraise>
    {
        PMS_WeddingEntities Objentity = new PMS_WeddingEntities();

        public int Delete(FL_OrderAppraise ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderAppraise> GetByAll()
        {
            return Objentity.FL_OrderAppraise.ToList();
        }


        /// <summary>
        /// 根据订单号获取数据
        /// </summary>
        /// <returns></returns>
        public List<FL_OrderAppraise> GetByOrder(int? OrderID)
        {
          return  Objentity.FL_OrderAppraise.Where(C => C.OrderID == OrderID).ToList();
        }


        public FL_OrderAppraise GetByID(int? KeyID)
        {
            return Objentity.FL_OrderAppraise.FirstOrDefault(C => C.AppraiseID == KeyID);
        }


        public FL_OrderAppraise GetByEmployeeID(int? EmployeeID,int? OrderID)
        {
            return Objentity.FL_OrderAppraise.FirstOrDefault(C => C.KindID == EmployeeID && C.OrderID == OrderID&&C.Type==1);
        }

        public List<FL_OrderAppraise> GetAllBySupplierName(string AppraiseTitle) 
        {

            return Objentity.FL_OrderAppraise.Where(C => C.AppraiseTitle == AppraiseTitle&&C.Type==3).ToList();
        }

        public List<FL_OrderAppraise> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FL_OrderAppraise>.GetDataByParameter(new FL_OrderAppraise(), ObjParameterList);
            SourceCount = query.Count();

            List<FL_OrderAppraise> resultList = query.OrderByDescending(C => C.AppraiseID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FL_OrderAppraise>();
            }
            return resultList;
        }

        public List<FL_OrderAppraise> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加评论信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_OrderAppraise ObjectT)
        {
            //var ObjExistModel=Objentity.FL_OrderAppraise.FirstOrDefault(C=>C.KindID==ObjectT.KindID&&C.Type==ObjectT.Type&&C.OrderID==ObjectT.OrderID);
            //if (ObjExistModel != null)
            //{
            //    ObjExistModel.Point = ObjectT.Point;
            //    ObjExistModel.Remark = ObjectT.Remark;
            //    ObjExistModel.Suggest=ObjectT.Suggest;
            //    Objentity.SaveChanges();
            //    return ObjExistModel.AppraiseID;
            //}
            //else
            //{
                Objentity.FL_OrderAppraise.Add(ObjectT);
                Objentity.SaveChanges();
                return ObjectT.AppraiseID;
            //}
        }



        /// <summary>
        /// 获取供应商差错次数
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public string GetSupplierErroSum(int SupplierID,DateTime? TimerStar,DateTime? TimerEnd)
        {
            if (TimerStar != null && TimerEnd != null)
            {
                return Objentity.FL_OrderAppraise.Count(C => C.CreateDate >= TimerStar && C.CreateDate <= TimerEnd && C.SupplierID == SupplierID && C.ErroState == 0).ToString();
            }
            else
            {
                return Objentity.FL_OrderAppraise.Count(C => C.SupplierID == SupplierID && C.ErroState == 0).ToString();
            }

        }


        /// <summary>
        /// 获取供货次数
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <param name="TimerStar">为null查询全部</param>
        /// <param name="TimerEnd"></param>
        /// <returns></returns>
        public string GetSupplierProductSum(int SupplierID, DateTime? TimerStar, DateTime? TimerEnd)
        {
            if (TimerStar != null && TimerEnd != null)
            {
                return Objentity.FL_OrderAppraise.Count(C => C.CreateDate >= TimerStar && C.CreateDate <= TimerEnd && C.SupplierID == SupplierID).ToString();
            }
            else
            {
                return Objentity.FL_OrderAppraise.Count(C => C.SupplierID == SupplierID).ToString();
            }
        }



        /// <summary>
        /// 计算总体满意度
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public string GetPoint(int SupplierID)
        {
            var ObjList1 = Objentity.FL_OrderAppraise.Count(C => C.SupplierID == SupplierID&&C.Point==4);
            var ObjList2 = Objentity.FL_OrderAppraise.Count(C => C.SupplierID == SupplierID);
            if (ObjList2 >0)
            {
                return (ObjList1 / ObjList2).ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_OrderAppraise ObjectT)
        {
            Objentity.SaveChanges();
            return ObjectT.AppraiseID;
        }
    }
}
