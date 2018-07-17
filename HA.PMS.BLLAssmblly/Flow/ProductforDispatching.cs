using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;
using HA.PMS.ToolsLibrary;
//产品级派工

namespace HA.PMS.BLLAssmblly.Flow
{
    public class ProductforDispatching : ICRUDInterface<FL_ProductforDispatching>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        public List<FL_ProductforDispatching> GetProductByType(int? DispatchingID, int? RowType)
        {
            return GetProductByType(DispatchingID, RowType, 1);
        }

        /// <summary>
        /// 获取非空非库房供应商
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetSupplierByNotNullOrWareHouse(int? KeyID)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == KeyID && C.SupplierName != null && C.SupplierName != "库房").ToList();
        }
        /// <summary>
        /// 获取派工单下的产品
        /// </summary>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetProductByType(int? DispatchingID, int? RowType, int? Productproperty)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.RowType == RowType && C.DispatchingID == DispatchingID && C.ItemLevel == 3).ToList();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="SuppName"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetProductBySupplierName(string SuppName)
        {
            Dispatching ObjDispatchingBLL = new Dispatching();


            return ObjEntity.FL_ProductforDispatching.Where(C => C.SupplierName == SuppName && C.IsDelete == false).ToList();

        }

        public List<FL_ProductforDispatching> GetDataByParameter(List<PMSParameters> pars, string OrderByCloumname, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FL_ProductforDispatching>.GetDataByWhereParameter(pars, OrderByCloumname, PageSize, PageIndex, out SourceCount).ToList();
        }



        /// <summary>
        /// 根据OrderID组获取数据
        /// </summary>
        /// <returns></returns>
        public int GetUseCountByDispatchingKeyList(int[] ObjKeyList, int ProductID)
        {
            var ObjList = (from C in ObjEntity.FL_ProductforDispatching
                           where C.ProductID == ProductID && (ObjKeyList).Contains(C.DispatchingID)
                           select C).ToList();
            if (ObjList.Count > 0)
            {
                return ObjList.Sum(C => C.Quantity);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 婚期当天占用情况
        /// </summary>
        /// <returns></returns>
        public int UseforCustomerorTimerSpan(int ProductID, DateTime PartyDate, string TimerSpan)
        {

            //先取得当天结婚的新娘订单ID
            var ObjCustomerList = ObjEntity.View_Dispatching.Where(C => C.PartyDate == PartyDate && C.TimeSpans == TimerSpan);
            List<int> ObjOrderKeyList = new List<int>();
            foreach (var ObjItem in ObjCustomerList)
            {
                ObjOrderKeyList.Add(ObjItem.CustomerID);
            }

            var UseCount = GetUseCountByDispatchingKeyList(ObjOrderKeyList.ToArray(), ProductID);




            var WareHouseProductID = ObjEntity.FD_AllProducts.FirstOrDefault(C => C.Keys == ProductID && C.Type == 2).KindID;
            var HouseProduct = ObjEntity.FD_StorehouseSourceProduct.FirstOrDefault(C => C.SourceProductId == WareHouseProductID);
            if (HouseProduct != null)
            {

                return (HouseProduct.SourceCount.Value - UseCount);
            }
            else
            {
                return 0;
            }
            ////Customers ObjCustomersBLL = new Customers();
            //ObjEntity.FL_Customers.Where(

        }
        /// <summary>
        /// 获取某供应商的产品
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="RowType"></param>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="Productproperty"></param>
        /// <param name="SuppName"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetProductBySupplierName(int? DispatchingID, int? RowType, int? EmpLoyeeID, int? Productproperty, string SuppName)
        {
            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjDisModel = ObjDispatchingBLL.GetByID(DispatchingID);
            if (ObjDisModel.EmployeeID == EmpLoyeeID)
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.Productproperty == Productproperty && C.DispatchingID == DispatchingID && C.RowType == RowType && C.SupplierName == SuppName && C.ItemLevel == 3).ToList();
            }
            else
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.Productproperty == Productproperty && C.DispatchingID == DispatchingID && C.EmployeeID == EmpLoyeeID && C.SupplierName == SuppName && C.ItemLevel == 3).ToList();
            }
        }

        public List<FL_ProductforDispatching> GetProductBySupplierName(int? DispatchingID, int? RowType, int? Productproperty, string SuppName)
        {
            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjDisModel = ObjDispatchingBLL.GetByID(DispatchingID);

            return ObjEntity.FL_ProductforDispatching.Where(C => C.Productproperty == Productproperty && C.DispatchingID == DispatchingID && C.SupplierName == SuppName && C.ItemLevel == 3).ToList();

        }

        /// <summary>
        /// 获取某人的责任产品 总派人无限制
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="RowType"></param>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="Productproperty"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetProductByType(int? DispatchingID, int? RowType, int? EmpLoyeeID, int? Productproperty)
        {

            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjDisModel = ObjDispatchingBLL.GetByID(DispatchingID);
            if (ObjDisModel.EmployeeID == EmpLoyeeID)
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.Productproperty == Productproperty && C.DispatchingID == DispatchingID && C.RowType == RowType && C.ItemLevel == 3).ToList();
            }
            else
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.Productproperty == Productproperty && C.DispatchingID == DispatchingID && C.EmployeeID == EmpLoyeeID && C.RowType == RowType && C.ItemLevel == 3).ToList();
            }
        }


        /// <summary>
        /// 获取责任人相关的项目或类别
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="itemLevel"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByEmloyeeID(int? DispatchingID, int? EmpLoyeeID, int? itemLevel)
        {

            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.EmployeeID == EmpLoyeeID && C.ItemLevel == itemLevel).ToList();
        }

        /// <summary>
        /// 根据ID组
        /// </summary>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByKeysList(int[] ObjKeyList)
        {
            return (from C in ObjEntity.FL_ProductforDispatching
                    where (ObjKeyList).Contains(C.ProductID.Value)
                    select C).ToList();
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="ObjKeyList"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByCatogryList(int[] ObjKeyList, int? DispatchingID)
        {
            return (from C in ObjEntity.FL_ProductforDispatching
                    where (ObjKeyList).Contains(C.CategoryID.Value) && C.DispatchingID == DispatchingID
                    select C).ToList();
        }

        /// <summary>
        /// 获取一级类别
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetFirstByEmloyeeID(int? DispatchingID, int? EmpLoyeeID)
        {

            var ObjModelList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.EmployeeID == EmpLoyeeID && C.ItemLevel == 1).ToList();
            if (ObjModelList.Count == 0)
            {
                var ObjSecondModelList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.EmployeeID == EmpLoyeeID && C.ItemLevel == 2).ToList();
                foreach (var ObjItem in ObjSecondModelList)
                {
                    ObjModelList.AddRange(ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.CategoryID == ObjItem.ParentCategoryID));
                }
                return ObjModelList;
            }
            else
            {
                return ObjModelList;
            }

        }


        /// <summary>
        /// 根据类型获取
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="RowType"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetProductByProductproperty(int? DispatchingID, int? ItemLevel, int? Productproperty, bool NeedChilder)
        {
            if (NeedChilder)
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.ItemLevel == ItemLevel && C.Productproperty == Productproperty && (C.DispatchingID == DispatchingID || C.ParentCategoryID == DispatchingID)).ToList();
            }
            else
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.ItemLevel == ItemLevel && C.Productproperty == Productproperty && C.DispatchingID == DispatchingID).ToList();
            }
        }

        /// <summary>
        /// 获取用户下的派工单
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByEmpLoyeeID(int? EmpLoyeeID, int? CategID)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.CategoryID == CategID && C.EmployeeID == EmpLoyeeID).ToList();
        }


        /// <summary>
        /// 获取用户的派工执行表 
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="CategID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByEmpLoyeeID(int? EmpLoyeeID, bool? IsGet)
        {
            List<ObjectParameter> ObjList = new List<System.Data.Objects.ObjectParameter>();
            ObjList.Add(new ObjectParameter("EmpLoyeeID", EmpLoyeeID));
            ObjList.Add(new ObjectParameter("IsGet", IsGet));

            //var DataList = new List<FL_ProductforDispatching>();


            var ObjDataList = PublicDataTools<FL_ProductforDispatching>.GetDataByParameter(new FL_ProductforDispatching(), ObjList.ToArray());
            return ObjDataList.Distinct(new KeyClassEquers()).ToList();
        }

        public FL_ProductforDispatching GetProductByProductId(int productId)
        {
            return ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.ProductID == productId);

        }


        public List<FL_ProductforDispatching> GetProductsByCustomerIDAndRowType(int CustomerID, int RowType)
        {
            IEnumerable<int> DispatchingIDs = ObjEntity.FL_Dispatching.Where(C => C.CustomerID == CustomerID).Select(C => C.DispatchingID);
            return ObjEntity.FL_ProductforDispatching.Where(C => DispatchingIDs.Contains(C.DispatchingID) && C.RowType == RowType).ToList();
        }

        public List<FL_ProductforDispatching> GetMarkProductsByCustomerID(int CustomerID)
        {
            IEnumerable<int> DispatchingIDs = ObjEntity.FL_Dispatching.Where(C => C.CustomerID == CustomerID).Select(C => C.DispatchingID);
            return ObjEntity.FL_ProductforDispatching.Where(C => DispatchingIDs.Contains(C.DispatchingID) && C.RowType == 2 && C.Productproperty.Value == 1 && C.ItemLevel == 3).ToList();
        }

        [Obsolete("OrderID 有时为空，不能完全获取到，请用 GetProductsByDispatchingIDAndRowType，或修改其算法")]
        public List<FL_ProductforDispatching> GetProductsByOrderIDAndRowType(int OrderID, int RowType)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.OrderID == OrderID && C.RowType == RowType).ToList();
        }

        public IEnumerable<FL_ProductforDispatching> GetProductsByDispatchingIDAndRowType(int DispatchingID, int RowType)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.RowType == RowType);
        }

        /// <summary>
        /// 获取用户的派工执行表 
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="CategID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetProductforDispatchingByMine(int? EmpLoyeeID, int? DispatchingID, int ItemLevel, out List<int> PatentCatogryList)
        {
            List<ObjectParameter> ObjList = new List<System.Data.Objects.ObjectParameter>();
            PatentCatogryList = new List<int>();
            ObjList.Add(new ObjectParameter("EmpLoyeeID", EmpLoyeeID));
            // ObjList.Add(new ObjectParameter("IsGet", IsGet));
            ObjList.Add(new ObjectParameter("DispatchingID", DispatchingID));
            ObjList.Add(new ObjectParameter("ItemLevel", ItemLevel));
            //var DataList = new List<FL_ProductforDispatching>();
            var ObjDataList = PublicDataTools<FL_ProductforDispatching>.GetDataByParameter(new FL_ProductforDispatching(), ObjList.ToArray());
            if (ItemLevel != 1)
            {
                foreach (var Objitem in ObjDataList)
                {
                    PatentCatogryList.Add(Objitem.ParentCategoryID.Value);
                }

            }
            else
            {
                PatentCatogryList = new List<int>();
            }



            return ObjDataList.Distinct(new KeyClassEquers()).ToList();
        }


        // public List<FL_ProductforDispatching> GetProductforDispatchingByMine(int? EmpLoyeeID,  int? DispatchingID,int ItemLevel)


        /// <summary>
        /// 删除物料
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_ProductforDispatching ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_ProductforDispatching.Remove(GetByID(ObjectT.ProeuctKey));
                int result = ObjEntity.SaveChanges();
                return result;
            }
            return 0;
        }


        /// <summary>
        /// 获取所有三级派工单
        /// </summary>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByAll()
        {
            return ObjEntity.FL_ProductforDispatching.ToList();
        }

        public int GetEmployeeIDByOrderID(int? OrderID)
        {

            var ObjDataList = ObjEntity.FL_ProductforDispatching.Where(C => C.OrderID == OrderID && C.EmployeeID != null).ToList().FirstOrDefault();
            return ObjDataList.EmployeeID.ToString().ToInt32();
        }


        /// <summary>
        /// 获取所有的需要填写成本的人员名单
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<int> GetEmpLoyeeKeyListDispatchingID(int? DispatchingID)
        {

            var ObjDataList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.EmployeeID != null).ToList();
            ObjDataList.AddRange(ObjEntity.FL_ProductforDispatching.Where(C => C.ParentDispatchingID == DispatchingID && C.EmployeeID != null).ToList());
            List<int> ObjEmployeeKeyList = new List<int>();
            foreach (var Objitem in ObjDataList)
            {
                ObjEmployeeKeyList.Add(Objitem.EmployeeID.Value);
            }
            return ObjEmployeeKeyList;
        }

        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <returns></returns>
        public List<int> GetSupperKeyListByDispatchingID(int? DispatchingID)
        {
            AllProducts ObjAllProductsBLL = new AllProducts();
            var ObjProductList = this.GetByDispatchingID(DispatchingID, 3).ToList();
            ObjProductList.AddRange(GetByParentDispatchingID(DispatchingID, 3).ToList());
            List<int> ObjProductKey = new List<int>();
            foreach (var Objitem in ObjProductList)
            {
                if (Objitem.ProductID != null)
                {
                    ObjProductKey.Add(Objitem.ProductID.Value);
                }
            }

            List<int> ObjSupperKeyList = new List<int>();
            var ObjSuppLierKeyList = ObjAllProductsBLL.GetGetSupplierProduct(ObjProductKey.ToArray());
            foreach (var ObjSupperModel in ObjSuppLierKeyList)
            {
                ObjSupperKeyList.Add(ObjSupperModel.SupplierID);
            }

            return ObjSupperKeyList;
        }


        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_ProductforDispatching GetByID(int? KeyID)
        {
            return ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.ProeuctKey == KeyID);
        }


        /// <summary>
        /// 根据名称获取派工项目
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public FL_ProductforDispatching GetByName(string ItemName)
        {
            return ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.ServiceContent == ItemName);
        }


        /// <summary>
        /// 根据名称 DispatchingID 供应商获取派工项目
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public FL_ProductforDispatching GetByNameDispachingID(int DispachingID, string SupplierName, string ItemName)
        {
            return ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.DispatchingID == DispachingID && C.SupplierName == SupplierName && C.ServiceContent == ItemName && C.ItemLevel == 3);
        }

        public List<FL_ProductforDispatching> GetByDispatchingID(int? DispatchingID)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID).ToList();
        }

        /// <summary>
        /// 根据总派和层级获取
        /// </summary>
        /// <param name="Classfiction">就是WorkType</param>
        /// <param name="SupplierName">供应商</param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByDispatchingID(int? DispatchingID, int? Level, string Classfiction = "", string SupplierName = "")
        {
            if (Classfiction != "" && SupplierName == "")
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == Level && C.Classification == Classfiction).ToList();
            }
            else if (Classfiction == "" && SupplierName != "")
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == Level && C.SupplierName == SupplierName).ToList();
            }
            else if (Classfiction != "" && SupplierName != "")
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == Level && C.Classification == Classfiction && C.SupplierName == SupplierName).ToList();
            }
            else
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == Level).ToList();
            }
        }

        /// <summary>
        /// 是否获取
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByDispatchingID(int? DispatchingID, int? Level, bool NeedChilder)
        {
            if (NeedChilder)
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID || C.ParentDispatchingID == DispatchingID).ToList();
            }
            else
            {
                return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == Level).ToList();
            }
        }

        //(C.DispatchingID == DispatchingID||C.ParentCategoryID==DispatchingID)
        /// <summary>
        /// 根据总派和层级获取
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByParentDispatchingID(int? DispatchingID, int? Level)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.ParentDispatchingID == DispatchingID && C.ItemLevel == Level).ToList();
        }

        /// <summary>
        /// 根据父级ID获取
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// 
        /// <param name="ParetnCategoryID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByParentCatageID(int? DispatchingID, int? ParentCategoryID, int? Level)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ParentCategoryID == ParentCategoryID && C.ItemLevel == Level).ToList();
        }


        public List<FL_ProductforDispatching> GetByParentCatageID(int? DispatchingID, int? ParentCategoryID)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ParentCategoryID == ParentCategoryID).ToList();
        }

        /// <summary>
        /// 根据类型ID获取单条
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="Level"></param>
        /// <param name="Catageid"></param>
        /// <returns></returns>
        public FL_ProductforDispatching GetOnlyByCatageID(int? DispatchingID, int? CategoryID, int? Level, int? IsFirstMake = 0)
        {
            return ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.DispatchingID == DispatchingID && C.ItemLevel == Level && C.CategoryID == CategoryID);
        }


        /// <summary>
        /// 根据类型获取多条 一般是三级
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByDispatchingID(int? DispatchingID, int CategoryID, int? Level)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == Level && C.CategoryID == CategoryID).ToList();
        }

        /// <summary>
        /// 根据报价单ID和派工ID获取
        /// </summary>
        /// <param name="KindID"></param>
        /// <param name="DisID"></param>
        /// <returns></returns>
        public FL_ProductforDispatching GetByQuotedIDandProductID(int? KindID, int? DispatchingID)
        {
            return ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.ProductID == KindID && C.DispatchingID == DispatchingID);
        }



        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="DisID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByCategoryID(int? DispatchingID, int? CategoryID, int? Level)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.CategoryID == CategoryID && C.ItemLevel == Level).ToList();
        }


        /// <summary>
        /// 获取跟本人相关的所有玩意
        /// </summary>
        /// <param name="EmpLoyee"></param>
        /// <param name="DispatchingID"></param>
        /// <param name="ItemLevel"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByMineProductall(int? EmployeeID, int? DispatchingID, int ItemLevel, out int MineState)
        {

            var ObjModelList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID).ToList();
            var ObjReturnDataList = ObjModelList;
            MineState = 1;

            if (ItemLevel == 1)
            {
                List<int> ObjKeyList = new List<int>();

                foreach (var ObjItem in ObjModelList)
                {
                    if (ObjItem.ParentCategoryID != 0)
                    {
                        ObjKeyList.Add(ObjItem.ParentCategoryID.Value);
                    }
                    else
                    {
                        ObjKeyList.Add(ObjItem.CategoryID.Value);
                    }
                }
                ObjReturnDataList = this.GetByCatogryList(ObjKeyList.ToArray(), DispatchingID).Where(C => C.ItemLevel == 1).ToList();
                //&& (C.TaskEmpLoyee == EmployeeID || C.EmployeeID == EmployeeID || C.DesignEmployee == EmployeeID)
                return ObjReturnDataList;
            }

            if (ItemLevel == 2)
            {
                ObjReturnDataList = ObjReturnDataList.Where(C => C.ItemLevel == 2).ToList();
                return ObjReturnDataList;
            }

            if (ItemLevel == 3)
            {
                ObjReturnDataList = ObjReturnDataList.Where(C => C.ItemLevel == 3).ToList();
                return ObjReturnDataList;
            }
            return ObjReturnDataList;

            //if (ItemLevel == 2)
            //{
            //    if (ObjModelList.Where(C => C.ItemLevel == 3).Count() == 0)
            //    {
            //        ObjReturnDataList.AddRange(ObjEntity.FL_ProductforDispatching.Where(C => C.ItemLevel == 2 && C.DispatchingID == DispatchingID && C.TaskEmpLoyee == EmployeeID || C.EmployeeID == EmployeeID));
            //    }

            //    MineState = 2;
            //    return ObjReturnDataList.Where(C => C.ItemLevel == 2).ToList();
            //}

            //if (ItemLevel == 3)
            //{
            //    if (ObjModelList.Where(C => C.ItemLevel == 3 && C.TaskEmpLoyee == EmployeeID || C.EmployeeID == EmployeeID).Count() == 0)
            //    {
            //        MineState = 3;
            //        return new List<FL_ProductforDispatching>();
            //    }

            //    MineState = 3;
            //    //ObjReturnDataList.AddRange(ObjEntity.FL_ProductforDispatching.Where(C => C.ItemLevel == 3 && C.DispatchingID == DispatchingID));
            //    return ObjReturnDataList.Where(C => C.ItemLevel == 3).ToList();
            //}

            //MineState = 3;
            //return new List<FL_ProductforDispatching>();

        }


        public List<FL_ProductforDispatching> GetByMineCatogry(int? CatogryID, int? EmployeeID, int? DispatchingID, int ItemLevel, out int MineState)
        {

            var ObjModelList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == ItemLevel && C.CategoryID == CatogryID).ToList();
            ObjModelList = ObjModelList.Where(C => C.EmployeeID == EmployeeID || C.TaskEmpLoyee == EmployeeID).ToList();
            var ObjReturnDataList = ObjModelList;
            MineState = 1;
            return ObjReturnDataList;


        }



        public List<FL_ProductforDispatching> GetByMineParetnCatogry(int? CatogryID, int? EmployeeID, int? DispatchingID, int ItemLevel, out int MineState, bool Need)
        {

            var ObjModelList = ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.ItemLevel == ItemLevel && C.ParentCategoryID == CatogryID && (C.EmployeeID == EmployeeID || C.TaskEmpLoyee == EmployeeID)).ToList();

            //if (!Need)
            //{
            //    ObjModelList = ObjModelList.Where(C => C.EmployeeID == EmployeeID || C.TaskEmpLoyee == EmployeeID).ToList();
            //}

            var ObjReturnDataList = ObjModelList;
            MineState = 1;
            return ObjReturnDataList;


        }

        public List<FL_ProductforDispatching> GetByCategoryID(int? DispatchingID, int? CategoryID, int? Level, int? EmpLoyeeID)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.DispatchingID == DispatchingID && C.CategoryID == CategoryID && C.ItemLevel == Level && C.EmployeeID == EmpLoyeeID).ToList();
        }

        /// <summary>
        /// 获取单条产品派工
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="ProductID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public FL_ProductforDispatching GetOnlyByProductID(int? DispatchingID, int? ProductID, int? Level)
        {

            return ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.DispatchingID == DispatchingID && C.ProductID == ProductID && C.ItemLevel == Level);
        }


        /// <summary>
        /// 添加三级派工方法
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_ProductforDispatching ObjectT)
        {
            if (ObjectT != null)
            {

                //var ExistModel = ObjEntity.FL_ProductforDispatching.FirstOrDefault(C => C.DispatchingID == ObjectT.DispatchingID && C.ProductID == ObjectT.ProductID);
                //if (ExistModel == null)
                //{
                ObjEntity.FL_ProductforDispatching.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.DispatchingID;
                }
                //}
                //else
                //{
                //    ExistModel.IsDelete = false;
                //    ExistModel.Unit = ObjectT.Unit;
                //    ExistModel.UnitPrice = ObjectT.UnitPrice;
                //    ExistModel.Quantity = ObjectT.Quantity;
                //    ExistModel.Subtotal = ObjectT.Subtotal;
                //    ExistModel.Remark = ObjectT.Remark;
                //    ExistModel.Requirement = ObjectT.Requirement;
                //    ExistModel.ServiceContent = ObjectT.ServiceContent;
                //    this.Update(ExistModel);
                //}
                return 0;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新所有责任人
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="EmpLoyeeID"></param>
        public void UpdateforDispatchingID(int? DispatchingID, int? EmpLoyeeID)
        {
            var ObjDataList = new List<FL_ProductforDispatching>();
            ObjDataList.AddRange(this.GetByDispatchingID(DispatchingID, 1));
            ObjDataList.AddRange(this.GetByDispatchingID(DispatchingID, 2));
            ObjDataList.AddRange(this.GetByDispatchingID(DispatchingID, 3));
            foreach (var Objitem in ObjDataList)
            {
                Objitem.EmployeeID = EmpLoyeeID;
                this.Update(Objitem);
            }
            //List<System.Data.Objects.ObjectParameter> ObjList= new List<System.Data.Objects.ObjectParameter>();
            //ObjList.Add(new System.Data.Objects.ObjectParameter("DispatchingID", DispatchingID));
            //PublicDataTools<FL_ProductforDispatching>.UpdateforEntity(new FL_ProductforDispatching(), ObjList.ToArray());
        }


        /// <summary>
        /// 更新所有责任人
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <param name="EmpLoyeeID"></param>
        public void UpdateIsGetforDispatchingID(int? DispatchingID, int? EmpLoyeeID)
        {
            var ObjDataList = new List<FL_ProductforDispatching>();
            ObjDataList.AddRange(this.GetByEmloyeeID(DispatchingID, EmpLoyeeID, 1));
            ObjDataList.AddRange(this.GetByEmloyeeID(DispatchingID, EmpLoyeeID, 2));
            ObjDataList.AddRange(this.GetByEmloyeeID(DispatchingID, EmpLoyeeID, 3));
            foreach (var Objitem in ObjDataList)
            {
                Objitem.IsGet = false;
                this.Update(Objitem);
            }
            //List<System.Data.Objects.ObjectParameter> ObjList= new List<System.Data.Objects.ObjectParameter>();
            //ObjList.Add(new System.Data.Objects.ObjectParameter("DispatchingID", DispatchingID));
            //PublicDataTools<FL_ProductforDispatching>.UpdateforEntity(new FL_ProductforDispatching(), ObjList.ToArray());
        }

        /// <summary>
        /// 修改物料
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_ProductforDispatching ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.ProeuctKey;
            }
            return 0;
        }

        public class KeyClassEquers : IEqualityComparer<FL_ProductforDispatching>
        {
            public bool Equals(FL_ProductforDispatching x, FL_ProductforDispatching y)
            {
                if (x.EmployeeID == y.EmployeeID && x.DispatchingID == y.DispatchingID)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(FL_ProductforDispatching obj)
            {
                return 0;
            }
        }


        #region

        //[执行的产品](所有)
        public List<FL_DispatchAllProducts> GetDispatchingProducts<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters)
        {
            parameters.Add(new ObjectParameter("ItemLevel", 3));
            List<FL_DispatchAllProducts> query = PublicDataTools<FL_DispatchAllProducts>.GetDataByParameter(new FL_DispatchAllProducts(), parameters.ToArray());
            totalCount = query.Count();
            return isAsc ? PageDataTools<FL_DispatchAllProducts>.GetPagedData(query.OrderBy(keySelector), pageSize, pageIndex).ToList() :
               PageDataTools<FL_DispatchAllProducts>.GetPagedData(query.OrderByDescending(keySelector), pageSize, pageIndex).ToList();
        }
        //[执行的产品](库房)
        public List<FL_DispatchAllProducts> GetDispatchingSHProducts<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters)
        {
            parameters.Add(new ObjectParameter("RowType", 2));
            return GetDispatchingProducts(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters);
        }
        //[执行的产品](库房)(指定新人)
        public List<FL_DispatchAllProducts> GetDispatchingSHProductsByCustomerIDs<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters, List<int> customerids)
        {
            parameters.Add(new ObjectParameter("Expr6_NumOr", string.Join(",", customerids)));
            return GetDispatchingSHProducts(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters);
        }
        //[执行的产品](库房)(婚期范围)
        public List<FL_DispatchAllProducts> GetDispatchingSHProductsByPartyDate<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters, DateTime start, DateTime end)
        {
            List<int> customerids = ObjEntity.FL_Customers.Where(C => C.PartyDate >= start && C.PartyDate <= end).Select(C => C.CustomerID).ToList();
            return GetDispatchingSHProductsByCustomerIDs(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters, customerids);
        }
        //[执行的产品](库房)(婚期范围)(一次性)
        public List<FL_DispatchAllProducts> GetDispatchingSHDisposibleProductsByPartyDate<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters, DateTime start, DateTime end)
        {
            List<int> productids = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDisposible == true).Select(C => C.SourceProductId).ToList();
            parameters.Add(new ObjectParameter("KindID_NumOr", string.Join(",", productids)));
            return GetDispatchingSHProductsByPartyDate(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters, start, end);
        }

        //[执行的产品](库房)(婚期范围)(非一次性)
        public List<FL_DispatchAllProducts> GetDispatchingSHNonIsDisposibleProductsByPartyDate<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters, DateTime start, DateTime end)
        {
            List<int> productids = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDisposible == false).Select(C => C.SourceProductId).ToList();
            parameters.Add(new ObjectParameter("KindID_NumOr", string.Join(",", productids)));
            return GetDispatchingSHProductsByPartyDate(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters, start, end);
        }


        //[执行的产品](库房)（指定新人）(一次性)
        public List<FL_DispatchAllProducts> GetDispatchingSHDisposibleProductsByCustomerIDs(List<int> customerids)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            List<int> productids = ObjEntity.FD_StorehouseSourceProduct.Where(C => C.IsDisposible == true).Select(C => C.SourceProductId).ToList();
            if (customerids.Count > 0 && productids.Count > 0)
            {
                parameters.Add(new ObjectParameter("KindID_NumOr", string.Join(",", productids)));
                parameters.Add(new ObjectParameter("Expr6_NumOr", string.Join(",", customerids)));
                parameters.Add(new ObjectParameter("RowType", 2));
                parameters.Add(new ObjectParameter("ItemLevel", 3));
                return PublicDataTools<FL_DispatchAllProducts>.GetDataByParameter(new FL_DispatchAllProducts(), parameters.ToArray());
            }
            return Enumerable.Empty<FL_DispatchAllProducts>().ToList();
        }


        //[执行的产品](供应商)
        public List<FL_DispatchAllProducts> GetSupplierProducts<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters)
        {
            parameters.Add(new ObjectParameter("RowType", 1));
            return GetDispatchingProducts(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters);
        }
        //[执行的产品](供应商)(指定新人)
        public List<FL_DispatchAllProducts> GetSupplierProductsByCustomerIDs<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters, List<int> customerids)
        {
            parameters.Add(new ObjectParameter("Expr6_NumOr", string.Join(",", customerids)));
            return GetSupplierProducts(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters);
        }
        //[执行的产品](供应商)(婚期范围)
        public List<FL_DispatchAllProducts> GetSupplierProductsByPartyDate<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters, DateTime start, DateTime end)
        {
            List<int> customerids = ObjEntity.FL_Customers.Where(C => C.PartyDate >= start && C.PartyDate <= end).Select(C => C.CustomerID).ToList();
            return GetSupplierProductsByCustomerIDs(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters, customerids);
        }
        //[执行的产品](供应商)(月分)
        public List<FL_DispatchAllProducts> GetSupplierProductsByMonthOfYear<S>(int pageSize, int pageIndex, out int totalCount, Func<FL_DispatchAllProducts, S> keySelector, bool isAsc, List<ObjectParameter> parameters, int year, int month)
        {
            DateTime start = new DateTime(year, month, 1);
            DateTime end = start.AddMonths(1);
            List<int> customerids = ObjEntity.FL_Customers.Where(C => C.PartyDate >= start && C.PartyDate < end).Select(C => C.CustomerID).ToList();
            return GetSupplierProductsByCustomerIDs(pageSize, pageIndex, out totalCount, keySelector, isAsc, parameters, customerids);
        }

        public List<FL_DispatchAllProducts> GetSupplierProducts(int supplierid)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("RowType", 1));
            parameters.Add(new ObjectParameter("ItemLevel", 3));
            return PublicDataTools<FL_DispatchAllProducts>.GetDataByParameter(new FL_DispatchAllProducts(), parameters.ToArray());
        }

        #endregion



        public List<FL_ProductforDispatching> GetForSupplierName(string SupplierName, string Classfiction, int DispatchingID, int level = 1)
        {
            if (level == 1)
            {
                if (Classfiction == "")
                {
                    return ObjEntity.FL_ProductforDispatching.Where(C => C.SupplierName == SupplierName && C.DispatchingID == DispatchingID && C.IsGet == true).ToList();
                }
                else
                {
                    return ObjEntity.FL_ProductforDispatching.Where(C => C.SupplierName == SupplierName && C.DispatchingID == DispatchingID && C.Classification.Contains(Classfiction) && C.IsGet == true).ToList();
                }
            }
            else
            {
                if (Classfiction == "")
                {
                    return ObjEntity.FL_ProductforDispatching.Where(C => C.SupplierName == SupplierName && C.DispatchingID == DispatchingID && C.ItemLevel == level && C.IsGet == true).ToList();
                }
                else
                {
                    return ObjEntity.FL_ProductforDispatching.Where(C => C.SupplierName == SupplierName && C.DispatchingID == DispatchingID && C.Classification.Contains(Classfiction) && C.ItemLevel == level && C.IsGet == true).ToList();
                }
            }
        }


        /// <summary>
        /// 根据总成本Id查找
        /// </summary>
        /// <param name="CostSumId"></param>
        /// <returns></returns>
        public FL_ProductforDispatching GetByCostSumId(int CostSumId)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.CostSumId == CostSumId).FirstOrDefault();
        }


        /// <summary>
        /// 根据ID获取派工任务
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public List<FL_ProductforDispatching> GetByOrderID(int OrderID)
        {
            return ObjEntity.FL_ProductforDispatching.Where(C => C.OrderID == OrderID).ToList();
        }
    }
}
