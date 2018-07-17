using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.FD
{
    public class LoseContent:ICRUDInterface<FD_LoseContent>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FD_LoseContent ObjectT)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取所有流失原因
        /// </summary>
        /// <returns></returns>
        public List<FD_LoseContent> GetByAll()
        {

            return ObjEntity.FD_LoseContent.ToList();
        }


        /// <summary>
        /// 根据类型获取
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<FD_LoseContent> GetByType(int? Type)
        {

            return ObjEntity.FD_LoseContent.Where(C => C.Type == Type).ToList();
        }


        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_LoseContent GetByID(int? KeyID)
        {
           return ObjEntity.FD_LoseContent.FirstOrDefault(C=>C.ContentID==KeyID);

        }


        

        public List<FD_LoseContent> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加流失原因
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FD_LoseContent ObjectT)
        {
            ObjEntity.FD_LoseContent.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.ContentID;
        }


        /// <summary>
        /// 修改流失原因
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FD_LoseContent ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
            }
            else
            {
                return ObjectT.ContentID;
            }
        }
    }
}
