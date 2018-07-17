using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.BLLAssmblly.FD
{
    public class PayNeedRabate:ICRUDInterface<FD_PayNeedRabate>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();



        /// <summary>
        /// 当期渠道费用
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetQudaoMoneySumBYYearMOnth(int Year, int Month)
        {

            var ObjList = ObjEntity.FD_PayNeedRabate.Where(C => C.PayDate.Value.Year == Year && C.PayDate.Value.Month == Month);
            if (ObjList.Count() > 0)
            {
                var YiShoulist = ObjList.Where(C => C.PayMoney != null);

                //if (YiShoulist.Count() > 0)
                //{
                //    return (ObjList.Sum(C => C.Amountmoney).Value - YiShoulist.Sum(C => C.RealityAmount).Value);
                //}
                return YiShoulist.Sum(C => C.PayMoney).Value.ToString("0.00");
            }
            else
            {
                return "0";
            }
        }



        public int Delete(FD_PayNeedRabate ObjectT)
        {
            if (ObjectT != null)
            {
                FD_PayNeedRabate objPayNeedRabate = GetByID(ObjectT.Id);

                objPayNeedRabate.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }


        /// <summary>
        /// 根据客户ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_PayNeedRabate GetByCustomersID(int? CustomerID)
        {
            return ObjEntity.FD_PayNeedRabate.FirstOrDefault(C => C.CustomerID == CustomerID);
        }

        public List<FD_PayNeedRabate> GetByAll()
        {
            return ObjEntity.FD_PayNeedRabate.Where(C => C.IsDelete == false).ToList();
        }

        public FD_PayNeedRabate GetByID(int? KeyID)
        {
            return ObjEntity.FD_PayNeedRabate.FirstOrDefault(C => C.Id == KeyID);
        }

        public List<FD_PayNeedRabate> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_PayNeedRabate.Where(C => C.IsDelete == false).Count();

            List<FD_PayNeedRabate> resultList = ObjEntity.FD_PayNeedRabate.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.PartyDay)
                   .Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_PayNeedRabate>();
            }
            return resultList;
        }

        /// <summary>
        /// 取得返利详细列表 根据条件
        /// </summary>
        /// <param name="ObjParameterList">参数列表</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        [Obsolete("请使用GetPayNeedRabateByWhere")]
        public List<FD_PayNeedRabate> GetByParaandIndex(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_PayNeedRabate>.GetDataByParameter(new FD_PayNeedRabate(), ObjParameterList);
            SourceCount = query.Where(C => C.IsDelete == false).Count();

            List<FD_PayNeedRabate> resultList = query.Where(C => C.IsDelete == false).OrderByDescending(C => C.SourceID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_PayNeedRabate>();
            }
            return resultList;
        }


        /// <summary>
        /// 分页获取返利明细
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <param name="OrdreByColumname"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_PayNeedRabate> GetPayNeedRabateByWhere(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FD_PayNeedRabate>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }  
        /// <summary>
        /// 不带分页 取得返利详细列表 根据条件
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<FD_PayNeedRabate> GetByParaandIndex(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<FD_PayNeedRabate>.GetDataByParameter(new FD_PayNeedRabate(), ObjParameterList);

            return query;
        }
        /// <summary>
        /// 查询统计所哟的
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        public List<FD_PayNeedRabate> GetByAll(ObjectParameter[] ObjParameterList)
        {
          
            var query = PublicDataTools<FD_PayNeedRabate>.GetDataByParameter(new FD_PayNeedRabate(), ObjParameterList);
            return query;
        }

        /// <summary>
        /// 返利查询
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FD_PayNeedSales> GetFD_SupplierProductQuerybyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FD_PayNeedSales>.GetDataByParameter(new FD_PayNeedSales(), ObjParameterList);
            SourceCount = query.Where(C => C.IsDelete == false).Count();

            List<FD_PayNeedSales> resultList = query.Where(C => C.IsDelete == false).OrderByDescending(C => C.SourceID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FD_PayNeedSales>();
            }
            return resultList;
        }

        public int Insert(FD_PayNeedRabate ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_PayNeedRabate.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.Id;
                }

            }
            return 0;
        }

        public int Update(FD_PayNeedRabate ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.Id;
            }
            return 0;
        }
    }
}
