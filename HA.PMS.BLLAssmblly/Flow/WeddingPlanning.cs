using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;


namespace HA.PMS.BLLAssmblly.Flow
{
    public class WeddingPlanning : ICRUDInterface<FL_WeddingPlanning>
    {
        //操作
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_WeddingPlanning ObjectT)
        {
            var ObjUpdateModel = GetByID(ObjectT.PlanningID);
            ObjEntity.FL_WeddingPlanning.Remove(ObjUpdateModel);

            return ObjEntity.SaveChanges();
        }

        public List<FL_WeddingPlanning> GetByAll()
        {
            return ObjEntity.FL_WeddingPlanning.ToList();
        }

        /// <summary>
        /// 根据ORDERID获取数据
        /// </summary>
        /// <returns></returns>
        public List<FL_WeddingPlanning> GetByOrderID(int? OrderID)
        {
            return ObjEntity.FL_WeddingPlanning.Where(C => C.OrderID == OrderID).ToList();
        }

        /// <summary>
        /// 根据订单获取数据
        /// </summary>
        /// <returns></returns>
        public List<FL_WeddingPlanning> GetByEmpLoyeeIDandOrderID(int? OrderID, int? EmpLoyeeID)
        {
            return ObjEntity.FL_WeddingPlanning.Where(C => C.OrderID == OrderID && C.CreateEmpLoyee == EmpLoyeeID).ToList();
        }


        public FL_WeddingPlanning GetByID(int? KeyID)
        {
            return ObjEntity.FL_WeddingPlanning.FirstOrDefault(C => C.PlanningID == KeyID);
        }

        public List<FL_WeddingPlanning> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页 包含OrderId查询
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_WeddingPlanning> GetByOrderIdIndex(int orderId, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = ObjEntity.FL_WeddingPlanning.Where(C => C.OrderID == orderId && C.IsDelete == false).ToList();
            SourceCount = query.Count();

            List<FL_WeddingPlanning> resultList = query
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.PlanningID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_WeddingPlanning>();
            }
            return resultList;
        }


        /// <summary>
        /// 分页 包含OrderId查询
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_WeddingPlanning> GetByOrderIdIndex(int orderId, int PageSize, int PageIndex, string State, out int SourceCount)
        {
            var query = ObjEntity.FL_WeddingPlanning.Where(C => C.OrderID == orderId && C.IsDelete == false).ToList();
            SourceCount = query.Count();

            List<FL_WeddingPlanning> resultList = query
                //进行排序功能操作，不然系统会抛出异常
                   .Where(C => C.State == State).OrderByDescending(C => C.PlanningID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).OrderBy(C => C.State).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_WeddingPlanning>();
            }
            return resultList;
        }
        /// <summary>
        /// 添加婚礼统筹
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_WeddingPlanning ObjectT)
        {
            var ObjExistModel = ObjEntity.FL_WeddingPlanning.FirstOrDefault(C => C.EmpLoyeeID == ObjectT.EmpLoyeeID && C.OrderID == ObjectT.OrderID && C.CategoryID == ObjectT.CategoryID && C.ParentCategoryID == ObjectT.ParentCategoryID);
            if (ObjExistModel == null)
            {
                ObjectT.IsDelete = false;
                ObjEntity.FL_WeddingPlanning.Add(ObjectT);
                ObjEntity.SaveChanges();
                return ObjectT.PlanningID;
            }
            else
            {
                ObjExistModel.PlanFinishDate = ObjectT.PlanFinishDate;
                ObjExistModel.Remark = ObjectT.Remark;
                ObjExistModel.Requirement = ObjectT.Requirement;
                ObjExistModel.ServiceContent = ObjectT.ServiceContent;
                ObjExistModel.EmpLoyeeID = ObjectT.EmpLoyeeID;
                ObjEntity.SaveChanges();
                return ObjExistModel.PlanningID;
            }

        }

        public int Update(FL_WeddingPlanning ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.PlanningID;
        }
    }
}
