/*
 财务管理
 * 责任人：黄晓可
 * 时间爱你20130916
 
 */

using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class Cost : ICRUDInterface<FL_Cost>
    {
        //
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 获取成交率平均数
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public decimal GetAvgProfitMargin(int Year, int Month)
        {

            var SourceCount = ObjEntity.FL_Cost.Count(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month);
            var SumProfitMargin = ObjEntity.FL_Cost.Where(C => C.ProfitMargin != null).Sum(C => C.ProfitMargin);


            if (SourceCount > 0 && SumProfitMargin != null)
            {
                return (SumProfitMargin / SourceCount).Value;
            }
            else
            {
                return 0;
            }
        }



        /// <summary>
        /// 删除成本明晰
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Cost ObjectT)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页获财务主体
        /// </summary>
        /// <returns></returns>
        public List<View_GetCost> GetCostByWhere(int PageSize, int PageIndex, out int SourceCount, int? EmployeeID, List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_GetCost>.GetDataByParameter(new View_GetCost(), ObjParList.ToArray()).OrderByDescending(C => C.DispatchingID).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_GetCost>();
            }
            return PageDataTools<View_GetCost>.AddtoPageSize(DataSource);
        }


        public List<View_GetCost> GetDataByWhereParameter(List<PMSParameters> ObjParmList, string OrdreByColumname, int pagesize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_GetCost>.GetDataByWhereParameter(ObjParmList, OrdreByColumname, pagesize, PageIndex, out SourceCount);

        }





        /// <summary>
        /// 根据客户获取
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public FL_Cost GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_Cost.FirstOrDefault(c => c.CustomerID == CustomerID);
        }

        public List<FL_Cost> GetByAll()
        {
            return ObjEntity.FL_Cost.ToList();
        }

        public FL_Cost GetByID(int? KeyID)
        {
            return ObjEntity.FL_Cost.FirstOrDefault(c => c.CostKey == KeyID);
        }



        public FL_Cost GetByOrderID(int? KeyID)
        {
            return ObjEntity.FL_Cost.FirstOrDefault(C => C.OrderID == KeyID);
        }

        public List<FL_Cost> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加财务成本明细主体
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Cost ObjectT)
        {
            var ObjModel = ObjEntity.FL_Cost.FirstOrDefault(C => C.OrderID == ObjectT.OrderID);
            if (ObjModel == null)
            {
                ObjectT.IsLock = false;
                ObjEntity.FL_Cost.Add(ObjectT);
                ObjEntity.SaveChanges();
                return ObjectT.CostKey;
            }
            else
            {
                return ObjModel.CostKey;
            }
        }

        public int Update(FL_Cost ObjectT)
        {
            ObjEntity.SaveChanges();
            return ObjectT.CostKey;
        }
    }
}
