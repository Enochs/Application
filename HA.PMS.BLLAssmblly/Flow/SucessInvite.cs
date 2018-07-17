
/**
 Version :HaoAi 1.0
 File Name :SucessInvite ； 成功邀约表
 Author:杨洋
 Date:2013.3.23
 Description:成功邀约表 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;



namespace HA.PMS.BLLAssmblly.Flow
{
    public class SucessInvite:ICRUDInterface<FL_SucessInvite>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_SucessInvite ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_SucessInvite.Remove(GetByID(ObjectT.SucessID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }

        public List<FL_SucessInvite> GetByAll()
        {
            return ObjEntity.FL_SucessInvite.ToList();
        }

        public FL_SucessInvite GetByID(int? KeyID)
        {
            return ObjEntity.FL_SucessInvite.FirstOrDefault(C => C.SucessID == KeyID);
        }

        public List<FL_SucessInvite> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_SucessInvite.Count();

            List<FL_SucessInvite> resultList = ObjEntity.FL_SucessInvite
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.SucessID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_SucessInvite>();
            }
            return resultList;
        }

        public int Insert(FL_SucessInvite ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_SucessInvite.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.SucessID;
                }

            }
            return 0;
        }

        public int Update(FL_SucessInvite ObjectT)
        {

            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.SucessID;
            }
            return 0;
        }
    }
}
