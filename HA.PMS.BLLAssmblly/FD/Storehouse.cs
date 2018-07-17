

/**
 Version :HaoAi 1.0
 File Name :库房 （多库房管理）
 Author:黄晓可
 Date:2013.9.11
 Description:渠道供应商表 实现ICRUDInterface<T> 接口中的方法
 * 
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;

namespace HA.PMS.BLLAssmblly.FD
{
    public class Storehouse : ICRUDInterface<FD_Storehouse>
    {
       PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_Storehouse ObjectT)
        {
            //if (ObjectT != null)
            //{
            //    FD_Storehouse objFD_Storehouse = GetByID(ObjectT.StorehouseID);

            //    objFD_Storehouse.IsDelete = true;
            //    return ObjEntity.SaveChanges();

            //}
            return 0;
        }



        /// <summary>
        /// 获取我需要的HouseID
        /// </summary>
        /// <returns></returns>
        public int GetMyneedHouseID(int EmpLoyeeID)
        {
            var HouseManagerList = ObjEntity.FD_Storehouse.ToList();
            //获得本人部门
            var EmployeeModel= ObjEntity.Sys_Employee.FirstOrDefault(C=>C.EmployeeID==EmpLoyeeID);
            var EmployeeDepartment = ObjEntity.Sys_Department.FirstOrDefault(C => C.DepartmentID == EmployeeModel.DepartmentID);
            foreach (var Objitem in HouseManagerList)
            {
                //循环取得本人的部门权限
                  var Department = ObjEntity.Sys_Department.FirstOrDefault(C=>C.DepartmentID== Objitem.DepartmentID);
                  if (Department.SortOrder.Contains(EmployeeDepartment.SortOrder))
                  {
                      return Objitem.StorehouseID;
                  }
            }
            return 1;
        
        }
       /// <summary>
       /// 获取库管所管理的库房ID
       /// </summary>
       /// <param name="EmployeeID"></param>
       /// <returns></returns>
        public int GetKeyByManager(int EmployeeID)
        {

            //开放多库房功能就返回实际值
            //不开房就返回1
            var ObjModel= ObjEntity.FD_Storehouse.FirstOrDefault(C => C.EmpLoyeeID == EmployeeID);
            if (ObjModel != null)
            {
                return ObjModel.StorehouseID;

            }
            else
            {
                return 1;
            }
        }


        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        public List<FD_Storehouse> GetByAll()
        {
            return ObjEntity.FD_Storehouse.ToList();
        }
        /// <summary>
        /// 返回使用产品表中产品明细
        /// </summary>
        /// <param name="ObjParameterList"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_DispatchAllProducts> GetbyFL_DispatchAllProductsParameter(ObjectParameter[] ObjParameterList, int PageSize, int PageIndex, out int SourceCount)
        {
            var query = PublicDataTools<FL_DispatchAllProducts>.GetDataByParameter(new FL_DispatchAllProducts(), ObjParameterList);
            SourceCount = query.Count();

            List<FL_DispatchAllProducts> resultList = query.OrderByDescending(C => C.Keys)
              .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (query.Count == 0)
            {
                resultList = new List<FL_DispatchAllProducts>();
            }
            return resultList;
        }
       

        /// <summary>
        /// 获取库房根据ID 当没有的时候返回默认库房
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FD_Storehouse GetByID(int? KeyID)
        {
            
            var ObjModel= ObjEntity.FD_Storehouse.FirstOrDefault(C => C.StorehouseID == KeyID);
            if (ObjModel != null)
            {
                return ObjModel;
            }
            else
            {
                return new FD_Storehouse() { HouseName="默认库房"};
            }
        }


       /// <summary>
       /// 库房排序
       /// </summary>
       /// <param name="PageSize"></param>
       /// <param name="PageIndex"></param>
       /// <param name="SourceCount"></param>
       /// <returns></returns>
        public List<FD_Storehouse> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FD_Storehouse.Count();

            List<FD_Storehouse> resultList = ObjEntity.FD_Storehouse
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.StorehouseID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FD_Storehouse>();
            }
            return resultList;
        }
      
        

       /// <summary>
       /// 添加一个库房
       /// </summary>
       /// <param name="ObjectT"></param>
       /// <returns></returns>
        public int Insert(FD_Storehouse ObjectT)
        {
            //FD_AllProducts allProduct = new FD_AllProducts();
            //allProduct.Count = ObjectT.TotalQuantity;
            //allProduct.Data = ObjectT.Image;//这里虽然是图片，但是资料含图片和其他的文件
         
            //allProduct.IsDelete = false;
            //allProduct.KindID = ObjectT.StorehouseID;
            //allProduct.ProductCategory = ObjectT.CategoryParent;
            //allProduct.ProjectCategory = ObjectT.CategoryID;
            //allProduct.PurchasePrice = ObjectT.PurchasePrice;
            //allProduct.SalePrice = ObjectT.SaleOrice;
            //allProduct.Specifications = ObjectT.Specifications;
            //allProduct.ProductName = new Productcs().GetByID(ObjectT.ProductId).ProductName;
            //allProduct.Type = 2;//库房
            //allProduct.Unit = ObjectT.Unit;

            //ObjEntity.FD_AllProducts.Add(allProduct);
            if (ObjectT != null)
            {
                ObjEntity.FD_Storehouse.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.StorehouseID;
                }

            }
            return 0;
        }
        

       //修改一个库房
        public int Update(FD_Storehouse ObjectT)
        {
            //FD_AllProducts allProduct = new FD_AllProducts();
            //allProduct.Count = ObjectT.TotalQuantity;
            //allProduct.Data = ObjectT.Image;//这里虽然是图片，但是资料含图片和其他的文件

            //allProduct.IsDelete = false;
            //allProduct.KindID = ObjectT.StorehouseID;
            //allProduct.ProductCategory = ObjectT.CategoryParent;
            //allProduct.ProjectCategory = ObjectT.CategoryID;
            //allProduct.PurchasePrice = ObjectT.PurchasePrice;
            //allProduct.SalePrice = ObjectT.SaleOrice;
            //allProduct.Specifications = ObjectT.Specifications;
            //allProduct.ProductName = new Productcs().GetByID(ObjectT.ProductId).ProductName;
            //allProduct.Type = 2;//库房
            //allProduct.Unit = ObjectT.Unit;
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.StorehouseID;
            }
            return 0;
        }


        #region 基于 Repositoy
        
        #endregion
    }
}
