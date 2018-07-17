using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using System;

//销售漏斗参考文档
namespace HA.PMS.BLLAssmblly.Flow
{
    public class OrderReference : ICRUDInterface<FL_OrderReference>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderReference ObjectT)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 根据类型获取
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public FL_OrderReference GetByState(int? State)
        {
            return ObjEntity.FL_OrderReference.FirstOrDefault(C => C.State == State);
        }

        public List<FL_OrderReference> GetByAll()
        {
            throw new NotImplementedException();
        }

        public FL_OrderReference GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderReference> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        public int Insert(FL_OrderReference ObjectT)
        {
         

            var ObjExistModel = ObjEntity.FL_OrderReference.First(C=>C.State==ObjectT.State);
            if (ObjExistModel == null)
            {
                ObjectT.CreateDate = DateTime.Now;
                if (ObjectT != null)
                {
                    ObjEntity.FL_OrderReference.Add(ObjectT);
                    if (ObjEntity.SaveChanges() > 0)
                    {
                        return ObjectT.NodeKeyID;
                    }

                }
            }
            else
            {
                ObjExistModel.StuContent = ObjectT.StuContent;
                ObjExistModel.UpdateDate = DateTime.Now;
              
                Update(ObjExistModel);
            }
            return 0;
        }

        public int Update(FL_OrderReference ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.NodeKeyID;
            }
            return 0;
        }
    }
}
