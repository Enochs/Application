using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Flow
{
    /// <summary>
    /// 四大金刚于订单关联
    /// </summary>
    public class OrderGuardian : ICRUDInterface<FL_OrderGuardian>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderGuardian ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderGuardian> GetByAll()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 根据订单ID获取
        /// </summary>
        /// <returns></returns>
        public List<FL_OrderGuardian> GetByOrder(int? OrderID)
        {
            return ObjEntity.FL_OrderGuardian.Where(C => C.OrderID == OrderID).ToList();
        }



        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_OrderGuardian GetByID(int? KeyID)
        {
            return ObjEntity.FL_OrderGuardian.FirstOrDefault(C => C.KeyforGudn == KeyID);
        }



        /// <summary>
        /// 获取订单下的
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_OrderGuardian> GetByOrderID(int? OrderID)
        {
            return ObjEntity.FL_OrderGuardian.Where(C => C.OrderID == OrderID).ToList();
        }

        public List<FL_OrderGuardian> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加关系
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_OrderGuardian ObjectT)
        {
            var ExistModel = ObjEntity.FL_OrderGuardian.FirstOrDefault(C => C.GuardianId == ObjectT.GuardianId && ObjectT.EmpLoyee == ObjectT.EmpLoyee);
            if (ExistModel == null)
            {
                ObjectT.CreateDate = DateTime.Now;
                ObjectT.Isdelete = false;
                ObjEntity.FL_OrderGuardian.Add(ObjectT);

                ObjEntity.SaveChanges();
            }
            else
            {
                ExistModel.Isdelete = false;
                ExistModel.OrderID = ObjectT.OrderID;

                ObjEntity.SaveChanges();
            }
            return ObjectT.KeyforGudn;
        }

        public int Update(FL_OrderGuardian ObjectT)
        {
            throw new NotImplementedException();
        }
    }
}
