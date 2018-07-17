
/**
 Version :HaoAi 1.0
 File Name :客户投诉类
 Author:杨洋
 Date:2013.3.24
 Description:客户投诉类 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
namespace HA.PMS.BLLAssmblly.CS
{
    public class Complain : ICRUDInterface<CS_Complain>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();



        /// <summary>
        /// 获取当期投诉量
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public int GetComplainSumByDate(int Year,int Month)
        {

            return ObjEntity.CS_Complain.Count(C=>C.ComplainDate.Value.Year==Year&&C.ComplainDate.Value.Month==Month);

        }


        public int Delete(CS_Complain ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_Complain.FirstOrDefault(
                 C => C.ComplainID == ObjectT.ComplainID).IsDelete = true;
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<CS_Complain> GetByAll()
        {
            return ObjEntity.CS_Complain.Where(C => C.IsDelete == false).ToList();
        }

        public CS_Complain GetByID(int? KeyID)
        {
            return ObjEntity.CS_Complain.FirstOrDefault(C => C.ComplainID == KeyID);
        }

        public List<CS_Complain> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CS_Complain.Count();

            List<CS_Complain> resultList = ObjEntity.CS_Complain
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.ComplainID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_Complain>();
            }
            return resultList;
        }
        /// <summary>
        /// 参数化查询并分页
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="isAll">是否是全部</param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<ComplainCustomer> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, bool? isAll, out int SourceCount)
        {
            var query = new List<ComplainCustomer>();

            if (isAll.HasValue)
            {



                //为true的是代表全部查询
                if (isAll.Value)
                {
                    //已经受理的了
                    query = PublicDataTools<ComplainCustomer>.GetDataByParameter(new ComplainCustomer(), ObjParameterList).Where(C => !string.IsNullOrEmpty(C.ReturnContent)).ToList();
                }
                else
                {
                    //false 没有受理了
                    query = PublicDataTools<ComplainCustomer>.GetDataByParameter(new ComplainCustomer(), ObjParameterList).Where(C => string.IsNullOrEmpty(C.ReturnContent)).ToList();
                }

            }
            else
            {
               // 为null的是代表全部查询
                query = PublicDataTools<ComplainCustomer>.GetDataByParameter(new ComplainCustomer(), ObjParameterList);
                
            }
            SourceCount = query.Count();

            List<ComplainCustomer> resultList = query.OrderByDescending(C => C.ComplainID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count() == 0)
            {
                resultList = new List<ComplainCustomer>();
            }
            return resultList;

        }


        /// <summary>
        /// 参数化查询并分页
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_CSDispatching> GetCSDispatchingbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<View_CSDispatching>.GetDataByParameter(new View_CSDispatching(), ObjParameterList);
            SourceCount = query.Count();
            List<View_CSDispatching> resultList = query.OrderByDescending(C => C.ComplainID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();
            if (query.Count == 0)
            {
                resultList = new List<View_CSDispatching>();
            }
            return resultList;
        }
        //ComplainCustomer
        public List<ComplainCustomer> GetbyParameter(ObjectParameter[] ObjParameterList)
        {
            var query = PublicDataTools<ComplainCustomer>.GetDataByParameter(new ComplainCustomer(), ObjParameterList);

            return query.ToList();

        }
        
      
        public int Insert(CS_Complain ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_Complain.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.ComplainID;
                }

            }
            return 0;
        }

        public int Update(CS_Complain ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ComplainID;
            }
            return 0;
        }

    }
}
