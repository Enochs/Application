using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.FlowerReport
{
    public partial class FlowerReportPrint : System.Web.UI.Page
    {

        Flower ObjFlowerBLL = new Flower();
        int DispatchingID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            DispatchingID = Request["DispatchingID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var DataList = ObjFlowerBLL.GetByDispatchingID(DispatchingID).Where(C => C.BuyType == Request["buytype"].ToInt32());
            lblSumMoney.Text = DataList.Where(C => C.SaleSumPrice != null).ToList().Sum(C => C.SaleSumPrice.Value).ToString();
            this.repFlowerPlanning.DataBind(DataList);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheetNPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("降雨量日报表");
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("花艺采购单");

            //获取list数据
            List<FL_FlowerCost> listRainInfo = ObjFlowerBLL.GetByDispatchingID(DispatchingID).Where(C => C.BuyType == Request["buytype"].ToInt32()).ToList();
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("鲜花名称");
            row1.CreateCell(1).SetCellValue("采购单价");
            row1.CreateCell(2).SetCellValue("单位");
            row1.CreateCell(3).SetCellValue("数量");
            row1.CreateCell(4).SetCellValue("总成本");
            row1.CreateCell(5).SetCellValue("说明");
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < listRainInfo.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(listRainInfo[i].FLowername);
                rowtemp.CreateCell(1).SetCellValue(listRainInfo[i].CostPrice.ToString());
                rowtemp.CreateCell(2).SetCellValue(listRainInfo[i].Unite);
                rowtemp.CreateCell(3).SetCellValue(listRainInfo[i].Quantity.ToString());
                rowtemp.CreateCell(4).SetCellValue(listRainInfo[i].CostSumPrice.ToString());
                //用GetWether方法进行数据转换
                rowtemp.CreateCell(5).SetCellValue(listRainInfo[i].Node);
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
        }
    }
}