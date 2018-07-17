
/**
 Version :HaoAi 1.0
 File Name :客户满意度调查类
 Author:杨洋
 Date:2013.4.11
 Description:处理内容表 实现ICRUDInterface<T> 接口中的方法
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.CS
{
    public class DegreeOfSatisfactionContent:ICRUDInterface<CS_DegreeOfSatisfactionContent>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CS_DegreeOfSatisfactionContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_DegreeOfSatisfactionContent.FirstOrDefault(
                 C => C.TypeKey == ObjectT.TypeKey);
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<CS_DegreeOfSatisfactionContent> GetByAll()
        {
            return ObjEntity.CS_DegreeOfSatisfactionContent.ToList();
        }

        public CS_DegreeOfSatisfactionContent GetByID(int? KeyID)
        {
            return ObjEntity.CS_DegreeOfSatisfactionContent.FirstOrDefault(C => C.TypeKey == KeyID);
        }

        public List<CS_DegreeOfSatisfactionContent> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CS_DegreeOfSatisfactionContent.Count();

            List<CS_DegreeOfSatisfactionContent> resultList = ObjEntity.CS_DegreeOfSatisfactionContent
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.TypeKey)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CS_DegreeOfSatisfactionContent>();
            }
            return resultList;
        }

        public int Insert(CS_DegreeOfSatisfactionContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CS_DegreeOfSatisfactionContent.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.TypeKey;
                }

            }
            return 0;
        }

        public int Update(CS_DegreeOfSatisfactionContent ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.TypeKey;
            }
            return 0;
        }
    }
}
