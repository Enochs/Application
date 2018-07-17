/**
 * 临时注释 订单跟踪记录管理
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class OrderDetails:ICRUDInterface<FL_OrderDetails>
    {
        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderDetails ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_OrderDetails.Remove(ObjectT);
                ObjEntity.SaveChanges();
                return 0;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 获取所有跟单跟踪记录
        /// </summary>
        /// <returns></returns>
        public List<FL_OrderDetails> GetByAll()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 根据客户ID获取相印的跟踪记录
        /// </summary>
        /// <returns></returns>
        public List<FL_OrderDetails> GetByCustomerID(int? CustomerID)
        {
            return ObjEntity.FL_OrderDetails.Where(C => C.CustomerID == CustomerID).ToList();
        }

        /// <summary>
        /// 根据Key获取跟踪信息
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_OrderDetails GetByID(int? KeyID)
        {
            return ObjEntity.FL_OrderDetails.FirstOrDefault(C=>C.DetailID==KeyID);
        }



        /// <summary>
        /// 无条件分页获取
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_OrderDetails> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException(); 
        }


        /// <summary>
        /// 分页获取沟通记录
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<FL_OrderDetails> GetByIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {
            PageIndex = PageIndex - 1;

            var DataSource = PublicDataTools<FL_OrderDetails>.GetDataByParameter(new FL_OrderDetails(), ObjParList.ToArray()).OrderBy(C => C.FollowDate).ToList();

            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();
            if (SourceCount == 0)
            {
                DataSource = new List<FL_OrderDetails>();
            }
            return DataSource;
        }


        /// <summary>
        /// 添加客户跟踪记录
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_OrderDetails ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_OrderDetails.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DetailID;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 修改客户跟踪记录
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_OrderDetails ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.DetailID;
            }
            return 0;
        }
    }
}
