using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Flow
{
    /// <summary>
    /// 执行表图片上传与查看
    /// </summary>
    public class DispatchingImage : ICRUDInterface<FL_DispatchingImage>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
       
        public int Delete(FL_DispatchingImage ObjectT)
        {
            ObjEntity.FL_DispatchingImage.Remove(GetByID(ObjectT.ImageID));
            ObjEntity.SaveChanges();
            return ObjectT.ImageID;
        }


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<FL_DispatchingImage> GetByAll()
        {
            return ObjEntity.FL_DispatchingImage.ToList();
        }


        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="KindID"></param>
        /// <returns></returns>
        public List<FL_DispatchingImage> GetByKind(int DispatchingID,int KindID)
        {

            return ObjEntity.FL_DispatchingImage.Where(C => C.KindID == KindID && C.DispatchingID == DispatchingID).ToList();
        }

 
        /// <summary>
        ///根据执行单获取
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_DispatchingImage> GetByDispatchingID(int? DispatchingID)
        {
            return ObjEntity.FL_DispatchingImage.Where(C => C.DispatchingID == DispatchingID).ToList();
        }




        public List<FL_DispatchingImage> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_DispatchingImage ObjectT)
        {
            ObjEntity.FL_DispatchingImage.Add(ObjectT);
    
            ObjEntity.SaveChanges();
            return ObjectT.ImageID;
        }

        public int Update(FL_DispatchingImage ObjectT)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 根据主键ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_DispatchingImage GetByID(int? KeyID)
        {
            return ObjEntity.FL_DispatchingImage.FirstOrDefault(C=>C.ImageID==KeyID);
        }
    }
}
