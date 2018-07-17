using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using System.IO;
using System.Data.Objects;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.SysReport
{
    public partial class CarrytaskUseTeamReport : SystemPage
    {
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        Dispatching ObjDispatchingBLL = new Dispatching();
        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();
        int DispatchingID = 0;
        int CustomerID = 0;
        Customers ObjCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            if (!IsPostBack)
            {

                CustomerID = Request["CustomerID"].ToInt32();


            }
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
                var ObjAllProductsModel = ObjAllProductsBLL.GetByID(Key.ToString().ToInt32());
                if (ObjAllProductsModel != null)
                {
                    return ObjAllProductsBLL.GetByID(Key.ToString().ToInt32()).ProductName;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var ObjProdustList = new List<FL_ProductforDispatching>();
            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add("PartyDate_between", txtPartyDay.Text.ToDateTime() + "," + txtPartyDay.Text.ToDateTime());
            int SourceCount = 0;
            var ObjDisList = ObjDispatchingBLL.GetDispatchingCustomersByWhere(100000, 0, out SourceCount, User.Identity.Name.ToInt32(), paramsList);
            foreach (var Objitem in ObjDisList)
            {
                ObjProdustList.AddRange(ObjProductforDispatchingBLL.GetProductByProductproperty(Objitem.DispatchingID, 3, 0, false));
            }


            repProductList.DataSource = ObjProdustList;
            repProductList.DataBind();
        }

        /// <summary>
        /// 保存完成时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repProductList.Items.Count; i++)
            {
                //((TextBox)repProductList.Items[i].FindControl("txtFinishDate"));
            }
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExporttoExcel_Click(object sender, EventArgs e)
        {
            var ObjProdustList = ObjProductforDispatchingBLL.GetProductByProductproperty(DispatchingID, 3, 0, false);

            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/TeamModel.xml"));
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();


            string ObjTempletContent = Objreader.ReadToEnd();
            Objreader.Close();
            if (ObjProdustList.Count > 0)
            {
                foreach (var ObjDataItem in ObjProdustList)
                {
                    ObjDataString.Append("<Row ss:Height=\"15\">\r\n");

                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.ParentCategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.CategoryName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.ServiceContent + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Requirement + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.UnitPrice + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Quantity + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Subtotal + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + GetEmployeeName(ObjDataItem.EmployeeID) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell ss:StyleID=\"s46\"><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                    ObjDataString.Append("</Row>");
                }
            }
            ObjTempletContent = ObjTempletContent.Replace("<=Title>", "新购入");

            ObjTempletContent = ObjTempletContent.Replace("<=Source>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}