using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class MyCarryTask : System.Web.UI.UserControl
    {
        int DispatchingID = 0;
        int EmployeeID = 0;

        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            EmployeeID = Request.Cookies["HAEmployeeID"].Value.ToInt32();
            if (!IsPostBack)
            {

                BinderData();
            }
        }

        public class KeyClassEquers : IEqualityComparer<KeyClass>
        {

            public bool Equals(KeyClass x, KeyClass y)
            {
                if (x.Key == y.Key && x.KeyName == y.KeyName)
                    return true;
                else
                    return false;
            }
            public int GetHashCode(KeyClass obj)
            {
                return 0;
            }


        }
        public class KeyClass
        {
            public int Key { get; set; }

            public string KeyName { get; set; }
        }


        /// <summary>
        /// 根据类型ID获取类型名称
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetProductByID(object Key)
        {
            if (Key != null)
            {
                return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 根据类型ID获取类型名称
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetCategoryByID(object Key)
        {
            if (Key != null)
            {
                return OjbCategoryBLL.GetByID(Key.ToString().ToInt32()).CategoryName;
            }
            else
            {
                return string.Empty;
            }
        }



        public string GetBorderStyle(object IsNewAdd)
        {
            if (IsNewAdd != null)
            {
                if (IsNewAdd.ToString() != string.Empty && IsNewAdd.ToString() == "True")
                {
                    return "style='background-color:#c7face;'";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 绑定供应商数据
        /// </summary>
        private void BinderData()
        {
            ///获取派工单产品
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
            //所有产品操作
            AllProducts ObjAllProductsBLL = new AllProducts();
            var ObjProductList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 1, EmployeeID);
            List<int> ProductKeyList = new List<int>();
            //获取所有产品ID 用于查询供应商关联
            foreach (var Objitem in ObjProductList)
            {
                if (Objitem.ProductID != null)
                {
                    ProductKeyList.Add(Objitem.ProductID.Value);
                }
            }

            var ObjSupplierProductList = ObjAllProductsBLL.GetGetSupplierProduct(ProductKeyList.ToArray());
            List<KeyClass> ObjSupplierKeyList = new List<KeyClass>();
            //获取所有供应商
            foreach (var ObjSupplieritem in ObjSupplierProductList)
            {
                ObjSupplierKeyList.Add(new KeyClass() { Key = ObjSupplieritem.SupplierID, KeyName = ObjSupplieritem.Name });
            }
            ObjSupplierKeyList = ObjSupplierKeyList.Distinct(new KeyClassEquers()).ToList();


            this.repTypeList.DataSource = ObjSupplierKeyList;
            this.repTypeList.DataBind();


            for (int i = 0; i < repTypeList.Items.Count; i++)
            {
                HiddenField objHideKey = repTypeList.Items[i].FindControl("hideKey") as HiddenField;
                if (objHideKey != null)
                {
                    Repeater ObjRep = repTypeList.Items[i].FindControl("repProductList") as Repeater;

                    var ALLKeys = ObjSupplierProductList.Where(C => C.SupplierID == objHideKey.Value.ToInt32());
                    List<int> ObjLiseKey = new List<int>();
                    foreach (var ObjKey in ALLKeys)
                    {
                        ObjLiseKey.Add(ObjKey.Keys);
                    }

                    ObjRep.DataSource = ObjProductforDispatchingBLL.GetByKeysList(ObjLiseKey.ToArray());
                    ObjRep.DataBind();
                }
            }
            // //循环供应商绑定
            //foreach (var SupplierKey in ObjSupplierKeyList)
            //{ 

            //}

            //取得库房
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2,EmployeeID);

            repWareHouseList.DataSource = ObjProdustList;
            repWareHouseList.DataBind();
        }


        /// <summary>
        /// 绑定供应商供应明细
        /// </summary>
        /// <param name="KindKey"></param>
        /// <param name="Type"></param>
        private void BinderBySupplierID(int? KindKey, int? Type)
        {



            this.repTypeList.DataBind();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 根据供应商 绑定产品列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repTypeList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}