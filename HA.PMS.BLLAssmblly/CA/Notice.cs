using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
namespace HA.PMS.BLLAssmblly.CA
{
    public class Notice:ICRUDInterface<CA_Notice>
    {
         PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(CA_Notice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_Notice.Remove(GetByID(ObjectT.NoticeKey));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<CA_Notice> GetByAll()
        {
            return ObjEntity.CA_Notice.ToList();
        }


        /// <summary>
        /// 返回指定数量的
        /// </summary>
        /// <param name="TopSize"></param>
        /// <returns></returns>
        public List<CA_Notice> Getbytop(int? TopSize)
        {
            return ObjEntity.CA_Notice.OrderByDescending(C => C.CreateDate).Take(TopSize.Value).ToList();
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public CA_Notice GetByID(int? KeyID)
        {
            return ObjEntity.CA_Notice.FirstOrDefault(C=>C.NoticeKey==KeyID);
        }

        public List<CA_Notice> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.CA_Notice.Count();

            List<CA_Notice> resultList = ObjEntity.CA_Notice
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.NoticeKey)
                   .Skip(PageSize * (PageIndex-1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<CA_Notice>();
            }
            return resultList;
        }
        public int Insert(CA_Notice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.CA_Notice.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.NoticeKey;
                }

            }
            return 0;
        }

        public int Update(CA_Notice ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.NoticeKey;
            }
            return 0;
        }
        
    }
}
