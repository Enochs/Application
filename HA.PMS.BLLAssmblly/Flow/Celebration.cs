using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
//临时注释 FL_Celebration 以后完善 庆典项目表 
//根据报价单派工生成一个庆典项目

namespace HA.PMS.BLLAssmblly.Flow
{

    public class Celebration:ICRUDInterface<FL_Celebration>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 删除庆典项目
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Celebration ObjectT)
        {
            var UpdateModel=this.GetByID(ObjectT.CelebrationID);
            UpdateModel.IsDelete = true;
            ObjEntity.SaveChanges();
            return UpdateModel.CelebrationID;
        }


        /// <summary>
        /// 删除相关信息
        /// </summary>
        /// <param name="OrderID"></param>
        public void DeleteByOrderID(int OrderID)
        {
            var ObjList = ObjEntity.FL_Celebration.Where(C => C.OrderID == OrderID).ToList();
            foreach (var Objitem in ObjList)
            {

                var DisList= ObjEntity.FL_Dispatching.Where(C=>C.OrderID==OrderID).ToList();
                foreach (var ObjDis in DisList)
                {
                    var ObjitemList= ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == ObjDis.DispatchingID).ToList();
                    foreach (var objProduct in ObjitemList)
                    {
                        ObjEntity.FL_ProductforDispatching.Remove(objProduct);
                        ObjEntity.SaveChanges();
                    }

                    var TeamList= ObjEntity.FL_DispatchingEmployeeManager.Where(C => C.DispatchingID == ObjDis.DispatchingID).ToList();
                    foreach (var ObjTeam in TeamList)
                    {
                        ObjEntity.FL_DispatchingEmployeeManager.Remove(ObjTeam);
                        ObjEntity.SaveChanges();
                        
                    }

                    ObjEntity.FL_Dispatching.Remove(ObjDis);
                    ObjEntity.SaveChanges();
                }
                var ObjCelList= ObjEntity.FL_CelebrationProductItem.Where(C=>C.CelebrationID==Objitem.CelebrationID).ToList();



                foreach (var ObjCelItem in ObjCelList)
                {
                    ObjEntity.FL_CelebrationProductItem.Remove(ObjCelItem);
                    ObjEntity.SaveChanges();
                    
                }
                ObjEntity.FL_Celebration.Remove(Objitem);
                ObjEntity.SaveChanges();
            }
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<FL_Celebration> GetByAll()
        {
            return new List<FL_Celebration>();
            //return ObjEntity.FL_Celebration.Where(C=>C.IsChecks==false).ToList();
        }


        /// <summary>
        /// 根据id返回
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Celebration GetByID(int? KeyID)
        {
            return ObjEntity.FL_Celebration.FirstOrDefault(C => C.CelebrationID==KeyID);
        }

        /// <summary>
        /// 根据id返回
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Celebration GetByCustomerID(int? CustomerID)
        {
            return ObjEntity.FL_Celebration.FirstOrDefault(C => C.CustomerID == CustomerID);
        }



        /// <summary>
        /// 根据报价单获取
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public FL_Celebration GetByQuotedID(int? QuotedID)
        {
            return ObjEntity.FL_Celebration.FirstOrDefault(C => C.QuotedID == QuotedID);
        }


        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public bool IsExistByQuotedID(int? QuotedID)
        {
            var ObjCelebrationModel=ObjEntity.FL_Celebration.FirstOrDefault(C => C.QuotedID == QuotedID);
            if (ObjCelebrationModel != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

 


        /// <summary>
        /// 分页获取项目
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_Celebration> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 创建庆典项目
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Celebration ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Celebration.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.CelebrationID;
                }
            }
            return 0;
        }


        /// <summary>
        /// 更新庆典项目表
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_Celebration ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.CelebrationID;
            }
            return 0;
        }
    }
}
