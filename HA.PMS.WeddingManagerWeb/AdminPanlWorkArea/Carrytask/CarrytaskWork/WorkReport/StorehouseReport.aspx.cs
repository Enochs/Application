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
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport
{
    public partial class StorehouseReport : SystemPage
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
   
            ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
 
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByType(DispatchingID, 2,EmployeeID).Where(C=>C.SupplierName=="库房");

            repWareHouseList.DataSource = ObjProdustList;
            repWareHouseList.DataBind();
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