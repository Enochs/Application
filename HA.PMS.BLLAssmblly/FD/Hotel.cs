
/**
 Version :HaoAi 1.0
 File Name :Hotel
 Author:杨洋
 Date:2013.3.15
 Description:酒店管理 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using HA.PMS;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;

namespace HA.PMS.BLLAssmblly.FD
{
    public class Hotel : ICRUDInterface<FD_Hotel>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_Hotel ObjectT)
        {
            if (ObjectT != null)
            {
                FD_Hotel objHotel = GetByID(ObjectT.HotelID);

                objHotel.IsDelete = true;
                return ObjEntity.SaveChanges();

            }
            return 0;
        }

        public List<FD_Hotel> GetByAll()
        {
            return ObjEntity.FD_Hotel.Where(C => C.IsDelete == false).ToList();
        }

        public FD_Hotel GetByID(int? KeyID)
        {
            return ObjEntity.FD_Hotel.FirstOrDefault(C => C.HotelID == KeyID);
        }

        public List<View_Hotels> GetbyParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<View_Hotels>.GetDataByParameter(new View_Hotels(), ObjParameterList);
            SourceCount = query.Where(C => C.IsDelete == false).Count();

            List<View_Hotels> resultList = query.Where(C => C.IsDelete == false).OrderByDescending(C => C.HotelID)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<View_Hotels>();
            }
            return resultList;

        }

        public List<FD_Hotel> GetWhereByParameter(List<PMSParameters> pars, string OrderByCoulumnName, int PageSize, int PageIndex, out int SourceCount,OrderType Order)
        {
            return PublicDataTools<FD_Hotel>.GetDataByWhereParameter(pars, OrderByCoulumnName, PageSize, PageIndex, out SourceCount, Order);

        }

        public List<FD_Hotel> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_Hotel.Where(C => C.IsDelete == false).Count();

            List<FD_Hotel> resultList = ObjEntity.FD_Hotel.Where(C => C.IsDelete == false)
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.HotelID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).Where(C => C.IsDelete == false).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_Hotel>();
            }
            return resultList;
        }

        public int Insert(FD_Hotel ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FD_Hotel.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.HotelID;
                }

            }
            return 0;
        }

        public int Update(FD_Hotel ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.HotelID;
            }
            return 0;
        }

        public string GetStarLevelText(int StarLevel)
        {
            switch (StarLevel)
            {
                case 1: return "一星级";
                case 2: return "二星级";
                case 3: return "三星级";
                case 4: return "四星级";
                case 5: return "五星级";
                case 6: return "其他";
                default: return string.Empty;
            }
        }

        public List<string> GetAreaList()
        {
            return ObjEntity.FD_Hotel.Where(C => C.IsDelete == false).Select(C => C.Area.Trim()).Distinct().ToList();
        }

        public List<FD_Hotel> GetByIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> parameters)
        {
            if (parameters == null)
            {
                parameters = new List<ObjectParameter>();
            }
            var query = PublicDataTools<FD_Hotel>.GetDataByParameter(new FD_Hotel(), parameters.ToArray()).Where(C => C.IsDelete == false);
            SourceCount = query.Count();
            return query.OrderByDescending(C => C.HotelID).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();
        }

        /// <summary>
        /// 根据酒店名称 获取酒店详细信息
        /// </summary>
        /// <param name="HotelName"></param>
        /// <returns></returns>
        public FD_Hotel GetByName(string HotelName)
        {
            return ObjEntity.FD_Hotel.FirstOrDefault(C => C.HotelName == HotelName);
        }
    }
}
