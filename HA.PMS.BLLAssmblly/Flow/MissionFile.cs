using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class MissionFile:ICRUDInterface<FL_MissionFile>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 删除任务文件
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_MissionFile ObjectT)
        {
            
            ObjEntity.FL_MissionFile.Remove(this.GetByID(ObjectT.FileID));
            ObjEntity.SaveChanges();
            return ObjectT.DetailedID;
        }

        public List<FL_MissionFile> GetByAll()
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 获取任务下的文件
        /// </summary>
        /// <param name="DetailedID"></param>
        /// <returns></returns>
        public List<FL_MissionFile> GetByMission(int? DetailedID)
        {
            return ObjEntity.FL_MissionFile.Where(C=>C.DetailedID==DetailedID).ToList();
        }



        /// <summary>
        /// 根据ID获取单个文件
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_MissionFile GetByID(int? KeyID)
        {

            return ObjEntity.FL_MissionFile.FirstOrDefault(C=>C.FileID==KeyID);
        }

        public List<FL_MissionFile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加任务文件
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_MissionFile ObjectT)
        {
            ObjectT.CreateDate = DateTime.Now;
            ObjEntity.FL_MissionFile.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_MissionFile ObjectT)
        {
            throw new NotImplementedException();
        }
    }
}
