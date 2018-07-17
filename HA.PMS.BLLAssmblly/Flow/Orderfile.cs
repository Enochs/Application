using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class Orderfile:ICRUDInterface<FL_Orderfile>
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public int Delete(FL_Orderfile ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_Orderfile> GetByAll()
        {
            throw new NotImplementedException();
        }

        public FL_Orderfile GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }

        public List<FL_Orderfile> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 上传添加初案
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Orderfile ObjectT)
        {
            ObjEntity.FL_Orderfile.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }



        /// <summary>
        /// 根据订单ID获取数据
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_Orderfile> GetByOrderID(int? OrderID)
        {
            return ObjEntity.FL_Orderfile.Where(C => C.OrderID == OrderID).ToList();
            
        }
        public int Update(FL_Orderfile ObjectT)
        {
            throw new NotImplementedException();
        }
    }
}
