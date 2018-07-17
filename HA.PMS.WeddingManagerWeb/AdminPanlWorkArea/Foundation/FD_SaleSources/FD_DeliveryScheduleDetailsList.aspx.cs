using System;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Flow;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Text;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_DeliveryScheduleDetailsList : HA.PMS.Pages.SystemPage
    {
        /// <summary>
        /// 供应商
        /// </summary>
        Supplier ObjSupplierBLL = new Supplier();
        /// <summary>
        /// 四大金刚
        /// </summary>
        FourGuardian ObjGuardianBLL = new FourGuardian();
        /// <summary>
        /// 内部人员
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        QuotedPriceSchedule ObjScheduleBLL = new QuotedPriceSchedule();
        Statement ObjStatementBLL = new Statement();

        string StartDate = "1970-01-01";
        string EndDate = "9999-12-31";
        int resourceCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderSupplierType();
                BinderGuardianType();
                BinderData(sender, e);
            }
        }

        #region 供应商 四大金刚绑定
        /// <summary>
        /// 绑定供应商类型
        /// </summary>
        protected void BinderSupplierType()
        {
            ddlSupplierType.DataSource = new SupplierType().GetByAll();
            ddlSupplierType.DataTextField = "TypeName";
            ddlSupplierType.DataValueField = "SupplierTypeId";
            ddlSupplierType.DataBind();
            ddlSupplierType.Items.Insert(0, "请选择");
        }

        /// <summary>
        /// 四大金刚
        /// </summary>
        public void BinderGuardianType()
        {
            var DataList = new GuardianType().GetByAll();
            ddlGuardianType.DataSource = DataList;
            ddlGuardianType.DataTextField = "TypeName";
            ddlGuardianType.DataValueField = "TypeId";
            ddlGuardianType.DataBind();
            ddlGuardianType.Items.Insert(0, "请选择");
        }
        #endregion

        List<FD_Supplier> SupplyDataList = null;
        List<HA.PMS.DataAssmblly.FD_FourGuardian> GuardianDataList = null;
        List<Sys_Employee> EmployeeDataList = null;
        List<FL_Statement> StatementDataList = null;

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BinderData(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.Button)
            {
                switch (((System.Web.UI.WebControls.Button)sender).ID.ToLower())
                {
                    case "btnquery": DeliveryPager.CurrentPageIndex = 1; break;
                }
            }
            if (rdoType.SelectedValue.ToInt32() == 1)           //供应商   
            {
                div_Supplier.Visible = true;
                div_Guardian.Visible = false;
                div_Employee.Visible = false;
                div_OutPerson.Visible = false;
                List<PMSParameters> pars = new List<PMSParameters>();
                pars.Add(ddlSupplierType.SelectedValue.ToInt32() > 0, "TypeId", ddlSupplierType.SelectedValue.ToInt32(), NSqlTypes.Equal);
                pars.Add(txtSupplierName.Text.Trim() != string.Empty, "Name", txtSupplierName.Text.Trim(), NSqlTypes.LIKE);
                pars.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                var DataList = ObjSupplierBLL.GetSupplierGroup(DeliveryPager.PageSize, DeliveryPager.CurrentPageIndex, pars, ref resourceCount);
                rptTestSupplier.DataBind(DataList);

                SupplyDataList = ObjSupplierBLL.GetSupplierGroup(1000, 1, pars, ref resourceCount);
                DeliveryPager.RecordCount = resourceCount;
                GetAllSum(pars, 1);             //本期合计
            }
            else if (rdoType.SelectedValue.ToInt32() == 2)          //四大金刚
            {
                div_Supplier.Visible = false;
                div_Guardian.Visible = true;
                div_Employee.Visible = false;
                div_OutPerson.Visible = false;
                List<PMSParameters> pars = new List<PMSParameters>();
                pars.Add(ddlGuardianType.SelectedValue.ToInt32() > 0, "TypeId", ddlGuardianType.SelectedValue.ToInt32(), NSqlTypes.Equal);
                pars.Add(txtGuardianName.Text.Trim() != string.Empty, "Name", txtGuardianName.Text.Trim(), NSqlTypes.LIKE);
                pars.Add(DateRangers.IsNotBothEmpty, "PartyDate", DateRangers.StartoEnd, NSqlTypes.DateBetween);

                var DataList = ObjGuardianBLL.GetGuardianGroup(DeliveryPager.PageSize, DeliveryPager.CurrentPageIndex, pars, ref resourceCount);
                repGuardian.DataBind(DataList);

                GuardianDataList = ObjGuardianBLL.GetGuardianGroup(1000, 1, pars, ref resourceCount);
                DeliveryPager.RecordCount = resourceCount;
                GetAllSum(pars, 4);              //本期合计
            }
            else if (rdoType.SelectedValue.ToInt32() == 3)          //内部人员
            {
                div_Supplier.Visible = false;
                div_Guardian.Visible = false;
                div_Employee.Visible = true;
                div_OutPerson.Visible = false;
                List<PMSParameters> pars = new List<PMSParameters>();
                pars.Add(txtEmployeeName.Text.Trim() != string.Empty, "Name", txtEmployeeName.Text.Trim(), NSqlTypes.LIKE);
                pars.Add(DateRanger1.IsNotBothEmpty, "PartyDate", DateRanger1.StartoEnd, NSqlTypes.DateBetween);
                var DataList = ObjEmployeeBLL.GetEmployeeGroup(DeliveryPager.PageSize, DeliveryPager.CurrentPageIndex, pars, ref resourceCount);
                rptEmployee.DataBind(DataList);

                EmployeeDataList = ObjEmployeeBLL.GetEmployeeGroup(1000, 1, pars, ref resourceCount);
                DeliveryPager.RecordCount = resourceCount;
                GetAllSum(pars, 5);     //本期合计
            }
            else if (rdoType.SelectedValue.ToInt32() == 4)          //外部人员
            {
                div_Supplier.Visible = false;
                div_Guardian.Visible = false;
                div_Employee.Visible = false;
                div_OutPerson.Visible = true;
                List<PMSParameters> pars = new List<PMSParameters>();
                pars.Add(txtOutPersonName.Text.Trim() != string.Empty, "Name", txtOutPersonName.Text.Trim(), NSqlTypes.LIKE);
                pars.Add(DateRanger2.IsNotBothEmpty, "PartyDate", DateRanger2.StartoEnd, NSqlTypes.DateBetween);
                var DataList = ObjEmployeeBLL.GetOutEmployeeGroup(DeliveryPager.PageSize, DeliveryPager.CurrentPageIndex, pars, ref resourceCount);
                retOutPerson.DataBind(DataList);

                StatementDataList = ObjEmployeeBLL.GetOutEmployeeGroup(1000, 1, pars, ref resourceCount);
                DeliveryPager.RecordCount = resourceCount;
                GetAllSum(pars, 0);     //本期合计
            }


            lblSupplierSum.Text = "当前总共" + resourceCount + "条数据";
        }
        #endregion

        #region 获取供应商类别
        /// <summary>
        /// 获取供应商产品类别名称
        /// </summary>
        /// <param name="SupplierTypeId"></param>
        /// <returns></returns>
        protected string GetSupplierTypeName(object SupplierTypeId)
        {
            FD_SupplierType fD_SupplierType = new SupplierType().GetByID(Convert.ToInt32(SupplierTypeId));
            return fD_SupplierType != null ? fD_SupplierType.TypeName : string.Empty;
        }
        #endregion

        #region 获取四大金刚类别
        /// <summary>
        /// 获取供应商产品类别名称
        /// </summary>
        /// <param name="SupplierTypeId"></param>
        /// <returns></returns>
        protected string GetGuardianTypeName(object GuardianTypeId)
        {
            FD_GuardianType GuardianTypes = new GuardianType().GetByID(Convert.ToInt32(GuardianTypeId));
            return GuardianTypes != null ? GuardianTypes.TypeName : string.Empty;
        }
        #endregion

        #region 获取供应商各种价格
        /// <summary>
        /// 获取价格
        /// </summary>
        /// <returns></returns>   
        public string GetSupplierByName(object Source, object Source1)
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            int SupplierId = Source.ToString().ToInt32();
            int Type = Source1.ToString().ToInt32();
            //供应商
            pars.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

            return ObjSupplierBLL.GetSuppierById(SupplierId, pars, Type);
        }
        #endregion

        #region 获取四大金刚各种价格
        /// <summary>
        /// 获取价格
        /// </summary>
        /// <returns></returns>   
        public string GetGuardianByName(object Source, object Source1)
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            int GuardianId = Source.ToString().ToInt32();
            int Type = Source1.ToString().ToInt32();
            //四大金刚
            pars.Add(DateRangers.IsNotBothEmpty, "PartyDate", DateRangers.StartoEnd, NSqlTypes.DateBetween);

            return ObjGuardianBLL.GetGuardianById(GuardianId, pars, Type);
        }
        #endregion

        #region 获取内部人员各种价格
        /// <summary>
        /// 获取价格
        /// </summary>
        /// <returns></returns>   
        public string GetEmployeeByName(object Source, object Source1)
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            int EmployeeId = Source.ToString().ToInt32();
            int Type = Source1.ToString().ToInt32();
            //内部人员
            pars.Add(DateRangers.IsNotBothEmpty, "PartyDate", DateRangers.StartoEnd, NSqlTypes.DateBetween);

            return ObjEmployeeBLL.GetEmployeeById(EmployeeId, pars, Type);
        }
        #endregion

        #region 选择变化事件 类型 四大金刚还是供应商
        /// <summary>
        /// 类型选择
        /// </summary>      
        protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData(sender, e);
        }
        #endregion

        #region 获取供货次数
        /// <summary>
        /// 供货次数
        /// </summary>
        public string GetSupplyCount(object Source, object Source1)
        {
            int RowType = Source1.ToString().ToInt32();
            if (RowType == 1)        //供应商
            {
                if (DateRanger.IsNotBothEmpty)
                {
                    string[] date = DateRanger.StartoEnd.Split(',');
                    StartDate = date[0].ToString();
                    EndDate = date[1].ToString();
                }
            }
            else if (RowType == 4)          //四大金刚
            {
                if (DateRangers.IsNotBothEmpty)
                {
                    string[] date = DateRangers.StartoEnd.Split(',');
                    StartDate = date[0].ToString();
                    EndDate = date[1].ToString();
                }
            }
            else if (RowType == 5)          //四大金刚
            {
                if (DateRanger1.IsNotBothEmpty)
                {
                    string[] date = DateRanger1.StartoEnd.Split(',');
                    StartDate = date[0].ToString();
                    EndDate = date[1].ToString();
                }
            }
            int SupplierID = Source.ToString().ToInt32();
            return ObjStatementBLL.GetBySupplierID(SupplierID, RowType, StartDate.ToDateTime(), EndDate.ToDateTime());
        }
        #endregion

        #region 隐藏查看预定按钮
        /// <summary>
        /// 隐藏
        /// </summary>
        public string IsShowOrHide(object Source)
        {
            int GuardianId = Source.ToString().ToInt32();
            var DataList = ObjScheduleBLL.GetByGuardianID(GuardianId);
            if (DataList.Count > 0)
            {
                return "style='display:block;width:72px;height:20px;'";
            }
            else
            {
                return "style='display:none'";
            }
        }
        #endregion

        #region 点击导出
        /// <summary>
        /// 导出功能
        /// </summary>
        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            LinkButton btn = (sender as LinkButton);
            if (btn.ID == "lbtnExport")
            {
                StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SupplyModel.xml"));

                string ObjTempletContent = Objreader.ReadToEnd();
                System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
                Objreader.Close();

                BinderData(sender, e);

                foreach (var ObjDataItem in SupplyDataList)
                {
                    ObjDataString.Append("<Row>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetSupplierTypeName(ObjDataItem.CategoryID) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Name + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Linkman + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.CellPhone + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.AccountInformation + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetSupplyCount(ObjDataItem.SupplierID, 1) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.ErrorCount + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetSupplierByName(ObjDataItem.SupplierID, 1) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetSupplierByName(ObjDataItem.SupplierID, 2) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetSupplierByName(ObjDataItem.SupplierID, 3) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + "" + "</Data></Cell>\r\n");
                    ObjDataString.Append("</Row>\r\n");
                }
                ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
                IOTools.DownLoadByString(ObjTempletContent, "xls");
            }
            else if (btn.ID == "lbtnExports")
            {
                StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/SupplyModel.xml"));

                string ObjTempletContent = Objreader.ReadToEnd();
                System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
                Objreader.Close();

                BinderData(sender, e);

                foreach (var ObjDataItem in GuardianDataList)
                {
                    ObjDataString.Append("<Row>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetGuardianTypeName(ObjDataItem.GuardianTypeId) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.GuardianName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.GuardianName + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.CellPhone + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.AccountInformation + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetSupplyCount(ObjDataItem.GuardianId, 4) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.ErrorCount + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetGuardianByName(ObjDataItem.GuardianId, 1) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetGuardianByName(ObjDataItem.GuardianId, 2) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetGuardianByName(ObjDataItem.GuardianId, 3) + "</Data></Cell>\r\n");
                    ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + "" + "</Data></Cell>\r\n");
                    ObjDataString.Append("</Row>\r\n");
                }
                ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
                IOTools.DownLoadByString(ObjTempletContent, "xls");
            }
        }
        #endregion

        #region 点击打印
        /// <summary>
        /// 打印功能
        /// </summary> 
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        #endregion

        decimal SumTotal = 0;       //应付款
        decimal PayMent = 0;        //已付款
        decimal NoPayMent = 0;      //未付款
        decimal PrePayMent = 0;     //预付款

        #region 绑定完成事件  本页合计
        /// <summary>
        /// 本页合计
        /// </summary>     
        protected void rptTestSupplier_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater repeater = sender as Repeater;
            if (repeater.ID == "rptTestSupplier")
            {
                FD_Supplier DataModel = (FD_Supplier)e.Item.DataItem;
                SumTotal += GetSupplierByName(DataModel.SupplierID, 1).ToDecimal();
                PayMent += GetSupplierByName(DataModel.SupplierID, 2).ToDecimal();
                NoPayMent += GetSupplierByName(DataModel.SupplierID, 3).ToDecimal();
                lblSumTotl.Text = SumTotal.ToString("f2");
                lblPayMent.Text = PayMent.ToString("f2");
                lblNoPayMent.Text = NoPayMent.ToString("f2");
            }
            else if (repeater.ID == "repGuardian")
            {
                HA.PMS.DataAssmblly.FD_FourGuardian DataModel = (HA.PMS.DataAssmblly.FD_FourGuardian)e.Item.DataItem;
                SumTotal += GetGuardianByName(DataModel.GuardianId, 1).ToDecimal();
                PayMent += GetGuardianByName(DataModel.GuardianId, 2).ToDecimal();
                NoPayMent += GetGuardianByName(DataModel.GuardianId, 3).ToDecimal();
                lblGuardPageSumTotal.Text = SumTotal.ToString("f2");
                lblGuardPagePayMent.Text = PayMent.ToString("f2");
                lblGuardPageNoPayMent.Text = NoPayMent.ToString("f2");
            }
            else if (repeater.ID == "rptEmployee")
            {
                Sys_Employee DataModel = (Sys_Employee)e.Item.DataItem;
                SumTotal += GetEmployeeByName(DataModel.EmployeeID, 1).ToDecimal();
                PayMent += GetEmployeeByName(DataModel.EmployeeID, 2).ToDecimal();
                NoPayMent += GetEmployeeByName(DataModel.EmployeeID, 3).ToDecimal();
                lblEmpPageSumTotal.Text = SumTotal.ToString("f2");
                lblEmpPagePayMent.Text = PayMent.ToString("f2");
                lblEmpPageNoPayMent.Text = NoPayMent.ToString("f2");
            }
            else if (repeater.ID == "retOutPerson")
            {
                FL_Statement DataModel = (FL_Statement)e.Item.DataItem;
                SumTotal += DataModel.SumTotal.ToString().ToDecimal();
                PayMent += DataModel.PayMent.ToString().ToDecimal();
                PrePayMent += DataModel.PrePayMent.ToString().ToDecimal();
                NoPayMent += DataModel.NoPayMent.ToString().ToDecimal();
                lblOutPageSumTotal.Text = SumTotal.ToString("f2");
                lblOutPageOutPayMent.Text = (PayMent.ToString().ToDecimal() + PrePayMent.ToString().ToDecimal()).ToString().ToDecimal().ToString("f2");
                lblOutPageNoPayMent.Text = NoPayMent.ToString("f2");
            }

        }
        #endregion

        #region 本期合计
        public void GetAllSum(List<PMSParameters> pars, int Type)
        {
            if (Type == 1)          //供应商
            {
                lblAllSumtotal.Text = ObjSupplierBLL.GetAllMoneySum(pars, 1, "供应商").ToDecimal().ToString("f2");
                lblAllPayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 2, "供应商").ToDecimal().ToString("f2");
                lblAllNoPayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 3, "供应商").ToDecimal().ToString("f2");
            }
            else if (Type == 4)     //四大金刚
            {
                lblGuardSumTotal.Text = ObjSupplierBLL.GetAllMoneySum(pars, 1, "四大金刚").ToDecimal().ToString("f2");
                lblGuardPayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 2, "四大金刚").ToDecimal().ToString("f2");
                lblGuardNoPayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 3, "四大金刚").ToDecimal().ToString("f2");
            }
            else if (Type == 5)     //内部人员
            {
                lblEmployeeSumTotal.Text = ObjSupplierBLL.GetAllMoneySum(pars, 1, "内部人员").ToDecimal().ToString("f2");
                lblEmployeePayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 2, "内部人员").ToDecimal().ToString("f2");
                lblEmployeeNoPayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 3, "内部人员").ToDecimal().ToString("f2");
            }
            else if (Type == 0)     //外部人员
            {
                lblOutSumTotal.Text = ObjSupplierBLL.GetAllMoneySum(pars, 1, "外部人员").ToDecimal().ToString("f2");
                lblOutPayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 2, "外部人员").ToDecimal().ToString("f2");
                lblOutNoPayMent.Text = ObjSupplierBLL.GetAllMoneySum(pars, 3, "外部人员").ToDecimal().ToString("f2");
            }
        }
        #endregion

        #region 获取银行信息
        /// <summary>
        /// 获取银行信息
        /// </summary>
        /// <param name="Type">1.代表 银行名称  2.代表银行帐号</param>
        public string GetBankInfo(object Source, object Source1, int Type)
        {
            string bankname = "";
            string bankcard = "";
            int RowType = 0;
            int ID = 0;
            if (Source != null)
            {
                RowType = Source.ToString().ToInt32();          //1.供应商    4.四大金刚    5.内部人员
            }

            if (Source1 != null)
            {
                ID = Source1.ToString().ToInt32();                //供应商ID SupplierID
            }

            if (Type == 1)
            {
                if (RowType == 1)
                {
                    var Model = ObjSupplierBLL.GetByID(ID);
                    if (Model != null)
                    {
                        bankname = Model.BankName != null ? Model.BankName.Trim().ToString() : "";
                        bankcard = Model.AccountInformation != null ? Model.AccountInformation.Trim().ToString() : "";
                    }
                }
                else if (RowType == 4)
                {
                    var Model = ObjGuardianBLL.GetByID(ID);
                    if (Model != null)
                    {
                        bankname = Model.BankName != null ? Model.BankName.Trim().ToString() : "";
                        bankcard = Model.BankName != null ? Model.AccountInformation.Trim().ToString() : "";
                    }
                }
                else if (RowType == 5)
                {
                    var Model = ObjEmployeeBLL.GetByID(ID);
                    if (Model != null)
                    {
                        bankname = Model.BankName != null ? Model.BankName.Trim().ToString() : "";
                        bankcard = Model.BankCard != null ? Model.BankCard.Trim().ToString() : "";
                    }
                }
            }


            if (bankname != "" && bankcard != "")
            {
                return bankname + ":" + bankcard;
            }
            else if (bankname != "" && bankcard == "")
            {
                return bankname + ":空";
            } if (bankname == "" && bankcard != "")
            {
                return bankcard;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 获取查询的时间段
        /// <summary>
        /// 获取
        /// </summary> 
        public string GetStartToEnd()
        {
            return DateRanger.StartoEnd.ToString();
        }
        #endregion

    }
}