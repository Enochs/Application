using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Linq;
using System.Collections.Generic;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignList : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary> 
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 结算表
        /// </summary>
        Statement ObjStatementBLL = new Statement();


        /// <summary>
        /// 供应商
        /// </summary>
        Supplier ObjSupplierBLL = new Supplier();


        SupplierType ObjSupplierTypeBLL = new SupplierType();


        /// <summary>
        /// 四大金刚
        /// </summary>
        FourGuardian ObjFourGuardianBLL = new FourGuardian();


        /// <summary>
        /// 成本明细
        /// </summary>
        CostSum ObjCostSumBLL = new CostSum();

        /// <summary>
        /// 前期设计
        /// </summary>
        EarlyDesigner ObjEarlyDesignerBLL = new EarlyDesigner();

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }
        }
        #endregion

        #region 数据绑定
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int EmployeeID = User.Identity.Name.ToInt32();
            if (ObjEmployeeBLL.IsManager(EmployeeID))
            {
                td_Type.Visible = true;
                td_Type1.Visible = true;
            }
            else
            {
                td_Type.Visible = false;
                td_Type1.Visible = false;
            }
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<PMSParameters> ObjparList = new List<PMSParameters>();
            ObjparList.Add("DesignerState", "1,2", NSqlTypes.IN);    //说明已选择策划师
            //根据设计师查询


            if (ddlEmployeeTypes.SelectedValue != "0")
            {
                MyManager.GetEmployeePar(ObjparList, ddlEmployeeTypes.SelectedValue.ToString());
            }
            else
            {
                if (MyManager.SelectedValue.ToInt32() > 0)
                {
                    ObjparList.Add("DesignerEmployee,EmployeeID", MyManager.SelectedValue.ToInt32() + "," + MyManager.SelectedValue.ToInt32(), NSqlTypes.PVP);
                }
                else
                {
                    ObjparList.Add("DesignerEmployee,EmployeeID", User.Identity.Name.ToInt32() + "," + User.Identity.Name.ToInt32(), NSqlTypes.PVP);
                }
            }



            //新人姓名
            ObjparList.Add(!txtBride.Text.Equals(string.Empty), "ContactMan", txtBride.Text.Trim(), NSqlTypes.LIKE);
            //根据酒店查询
            ObjparList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.LIKE);
            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                ObjparList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //婚期

            //完成状态
            if (ddlState.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("DesignerState", ddlState.SelectedValue.ToInt32(), NSqlTypes.Equal);

                ObjparList.Add(DateRanger.IsNotBothEmpty, "PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }
            else
            {
                if (DateRanger.IsNotBothEmpty)
                {
                    ObjparList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                }
                else
                {
                    string StartDate = DateTime.Now.ToShortDateString();
                    string EndDate = "9999-12-31";
                    ObjparList.Add("PartyDate", StartDate + "," + EndDate, NSqlTypes.DateBetween);
                }
            }

            var DataList = ObjQuotedPriceBLL.GetByWhereParameter(ObjparList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();

            DesignUpdate();
        }
        #endregion

        #region 获取报价单ID
        /// <summary>
        /// 根据用户ID获取报价单ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedIDByCustomers(object CustomerID)
        {
            if (CustomerID.ToString() != string.Empty && CustomerID.ToString() != "0")
            {
                return ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 分页
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击查询
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>       
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            HiddenField ObjEmpLoyeeHide;
            HiddenField ObjCustomerHide;

            for (int i = 0; i < repCustomer.Items.Count; i++)
            {
                ObjEmpLoyeeHide = (HiddenField)repCustomer.Items[i].FindControl("hideEmpLoyeeID");
                ObjCustomerHide = (HiddenField)repCustomer.Items[i].FindControl("hideCustomerHide");
                int CustomerID = ObjCustomerHide.Value.ToInt32();
                FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
                if (ObjEmpLoyeeHide.Value.ToInt32() != 0 && ObjQuotedPriceModel != null)
                {
                    int EmployeeID = ObjEmpLoyeeHide.Value.ToInt32();
                    SaveForm(EmployeeID, CustomerID);
                }
            }
            BinderData();
            JavaScriptTools.AlertWindow("保存完毕", Page);

        }
        #endregion

        #region 派给其他人
        /// <summary>
        /// 派给其他人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveOther_Click(object sender, EventArgs e)
        {
            int EmployeeID = hideEmpLoyeeID.Value.ToInt32();
            if (EmployeeID != -1)
            {
                SaveForm(EmployeeID);
                BinderData();
            }
            else
            {
                JavaScriptTools.AlertWindow("请选择设计师", Page);
            }
        }
        #endregion

        #region 改派给自己
        /// <summary>
        /// 派给自己
        /// </summary>    
        protected void btn_Own_Click(object sender, EventArgs e)
        {
            int EmployeeID = User.Identity.Name.ToInt32();

            SaveForm(EmployeeID);
            BinderData();
        }
        #endregion

        #region 改派设计师方法
        /// <summary>
        /// 改派方法
        /// </summary>
        /// <param name="EmployeeID">设计师ID</param>
        public void SaveForm(int EmployeeID, int CustomerID = 0)
        {

            var KeyArry = hideKeyList.Value.Trim(',').Split(',');
            int index = -1;
            foreach (var item in KeyArry)
            {
                index += 1;
                if (EmployeeID != 0)
                {
                    FL_QuotedPrice ObjQuotedPriceModel;
                    if (CustomerID == 0)
                    {
                        ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(item.ToInt32());
                    }
                    else
                    {
                        ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
                    }
                    if (ObjQuotedPriceModel != null)
                    {

                        //前期设计
                        FL_EarlyDesigner EarlyModel = ObjEarlyDesignerBLL.GetByCustomerID(ObjQuotedPriceModel.CustomerID.ToString().ToInt32());

                        //修改成本
                        if (EmployeeID != ObjQuotedPriceModel.DesignerEmployee)     //选择的后期设计和之前的后期设计不是同一个人(是同一个人就不需要操作)
                        {
                            if (EarlyModel == null)             //没有前期设计
                            {
                                ChangeDesigner(EmployeeID, ObjQuotedPriceModel.QuotedID, 2);     //修改成本 结算表
                            }
                            else if (EarlyModel != null)        //有前期设计
                            {
                                if (EmployeeID != EarlyModel.EarlyDesigner)         //改派后的后期设计和前期设计不一样
                                {
                                    if (EarlyModel.EarlyDesigner == ObjQuotedPriceModel.DesignerEmployee)   //改派前  后期设计=前期设计
                                    {
                                        ChangeDesigner(EarlyModel.EarlyDesigner.ToString().ToInt32(), ObjQuotedPriceModel.QuotedID, 1);     //修改成本 结算表(内容改成前期设计  之前的)
                                        SaveDesignCost(EmployeeID, ObjQuotedPriceModel);         //执行设计成本 新增
                                        InsertStatement(EmployeeID, ObjQuotedPriceModel);        //结算表
                                    }
                                    else
                                    {
                                        ChangeDesigner(EmployeeID, ObjQuotedPriceModel.QuotedID, 2);     //修改成本 结算表
                                    }
                                }
                                else if (EmployeeID == EarlyModel.EarlyDesigner)            //改派后的后期设计和前期设计相同
                                {
                                    //1.首先删除 成本表和结算表的设计数据
                                    var CostModels = ObjCostSumBLL.GetByCustomerID(ObjQuotedPriceModel.CustomerID.ToString().ToInt32(), GetEmployeeName(ObjQuotedPriceModel.DesignerEmployee.ToString().ToInt32()), 6);
                                    if (CostModels != null)
                                    {
                                        ObjCostSumBLL.Delete(CostModels);
                                    }

                                    //删除结算表
                                    var StatementModel = ObjStatementBLL.GetByCustomerID(ObjQuotedPriceModel.CustomerID.ToString().ToInt32(), GetEmployeeName(ObjQuotedPriceModel.DesignerEmployee.ToString().ToInt32()), 5);
                                    if (StatementModel != null)
                                    {
                                        ObjStatementBLL.Delete(StatementModel);
                                    }

                                    //修改设计师价格为200   因为前期设计 后期设计是同一个人
                                    FL_CostSum CostModel = ObjCostSumBLL.GetByCustomerID(ObjQuotedPriceModel.CustomerID.ToString().ToInt32(), GetEmployeeName(EmployeeID), 6);
                                    CostModel.Sumtotal = 200;
                                    CostModel.ActualSumTotal = 200;
                                    CostModel.Content = "前期设计/后期设计(执行设计)";
                                    ObjCostSumBLL.Update(CostModel);

                                    //结算表 修改 
                                    FL_Statement StatementsModel = ObjStatementBLL.GetByCustomerID(ObjQuotedPriceModel.CustomerID.ToString().ToInt32(), GetEmployeeName(EmployeeID), 5);
                                    StatementsModel.SumTotal = 200;
                                    StatementsModel.Content = "前期设计/后期设计(执行设计)";
                                    ObjStatementBLL.Update(StatementsModel);
                                }
                            }
                        }

                        ObjQuotedPriceModel.DesignerEmployee = EmployeeID;
                        ObjQuotedPriceModel.DesignCreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                        ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);
                    }

                }
            }
            JavaScriptTools.AlertWindow("保存完毕", Page);

        }
        #endregion

        #region 改派设计师 修改成本
        /// <summary>
        /// 修改成本 Type 1.新增CostSum (改派前  前期=后期  修改价格)   2.纯粹的修改
        /// </summary>  
        public void ChangeDesigner(int DesignEmployee, int QuotedID, int Type)
        {
            var QuotedModel = ObjQuotedPriceBLL.GetByQuotedID(QuotedID.ToString().ToInt32());
            string EmployeeName = GetEmployeeName(QuotedModel.DesignerEmployee);

            FL_CostSum CostModel = ObjCostSumBLL.GetByCustomerID(QuotedModel.CustomerID.ToString().ToInt32(), EmployeeName, 6);
            FL_Statement StatementModel = ObjStatementBLL.GetByCustomerID(QuotedModel.CustomerID.ToString().ToInt32(), EmployeeName, 5);


            if (Type == 1)
            {
                if (CostModel != null)
                {
                    CostModel.Name = GetEmployeeName(DesignEmployee);           //修改成本表
                    CostModel.Content = "前期设计";
                    CostModel.CategoryName = "前期设计";
                    CostModel.Sumtotal = 100;
                    CostModel.ActualSumTotal = 100;
                    ObjCostSumBLL.Update(CostModel);
                }

                if (StatementModel != null)
                {
                    StatementModel.Name = GetEmployeeName(DesignEmployee);              //修改结算表
                    StatementModel.Content = "前期设计";
                    StatementModel.SumTotal = 100;
                    ObjStatementBLL.Update(StatementModel);
                }


            }
            else if (Type == 2)
            {
                if (CostModel != null)
                {
                    CostModel.Name = GetEmployeeName(DesignEmployee);           //修改成本表
                    CostModel.Content = "后期设计(执行设计)";
                    CostModel.CategoryName = "后期设计(执行设计)";
                    ObjCostSumBLL.Update(CostModel);
                }

                if (StatementModel != null)
                {
                    StatementModel.Name = GetEmployeeName(DesignEmployee);              //修改结算表
                    StatementModel.Content = "后期设计(执行设计)";
                    ObjStatementBLL.Update(StatementModel);
                }
            }
        }
        #endregion

        #region 添加设计师成本
        /// <summary>
        /// 添加成本
        /// </summary>
        public void SaveDesignCost(int EmployeeID, FL_QuotedPrice ObjQuotedPriceModel)
        {
            //FL_CostSum CostSum = new FL_CostSum();

            //CostSum.Name = GetEmployeeName(EmployeeID);
            //CostSum.DispatchingID = GetDisID(ObjQuotedPriceModel.CustomerID).ToInt32();
            //CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            //CostSum.ShortCome = "";
            //CostSum.Advance = "";
            //CostSum.OrderID = ObjQuotedPriceModel.OrderID;
            //CostSum.QuotedID = ObjQuotedPriceModel.QuotedID;
            //CostSum.CustomerId = ObjQuotedPriceModel.CustomerID;
            //CostSum.RowType = 6;

            //CostSum.Content = "执行设计(后期设计)";
            //CostSum.CategoryName = "执行设计(后期设计)";

            //CostSum.Sumtotal = 100;
            //CostSum.ActualSumTotal = 100;
            //CostSum.Evaluation = 6;
            //CostSum.EmployeeID = User.Identity.Name.ToInt32();

            //var List = ObjCostSumBLL.GetByChecks(GetEmployeeName(EmployeeID), GetDisID(ObjQuotedPriceModel.CustomerID).ToInt32(), 6);
            //if (List.Count == 0)          //实体为null   没添加过 (就可以新增 添加)  确保不重复添加
            //{
            //    ObjCostSumBLL.Insert(CostSum);
            //}
        }

        #region 获取设计单的各项名称
        /// <summary>
        /// 获取名称
        /// </summary>
        public string GetTitle(int CustomerID, int Supplier)
        {
            HA.PMS.BLLAssmblly.Flow.Designclass ObjDesignclassBLL = new BLLAssmblly.Flow.Designclass();
            var DataList = ObjDesignclassBLL.GetByCustomerId(CustomerID).Where(C => C.Supplier.ToInt32() == Supplier).ToList();
            string titles = "";
            int index = 0;
            foreach (var item in DataList)
            {
                if (DataList.Count == index)
                {
                    titles += item.Title;
                }
                else
                {
                    titles += item.Title + ",";
                    index++;
                }
            }
            return titles;
        }
        #endregion

        #endregion

        //结算表方法
        #region 保存结算表
        public void InsertStatement(int EmployeeID, FL_QuotedPrice ObjQuotedPriceModel)
        {
            int DispatchingID = GetDisID(ObjQuotedPriceModel.CustomerID).ToInt32();
            int CustomerID = ObjQuotedPriceModel.CustomerID.ToString().ToInt32();

            var DatasList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);
            foreach (var item in DatasList)
            {
                #region 结算表

                FL_Statement ObjStatementModel = new FL_Statement();
                #region 判断类别
                switch (item.RowType)
                {
                    case 1:         //供应商
                        var ObjSupplierModel = ObjSupplierBLL.GetByName(item.Name);
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.SupplierID = ObjSupplierModel.SupplierID;
                        if (ObjSupplierModel != null)
                        {
                            ObjStatementModel.TypeID = ObjSupplierModel.CategoryID;
                        }
                        else
                        {
                            foreach (var AllItem in ObjSupplierBLL.GetByAll())
                            {
                                if (item.Name.Contains(AllItem.Name) || item.Name.Contains(AllItem.Name))
                                {
                                    ObjStatementModel.TypeID = AllItem.CategoryID;
                                }
                            }
                        }
                        ObjStatementModel.TypeName = ObjSupplierTypeBLL.GetByID(ObjStatementModel.TypeID).TypeName;
                        ObjStatementModel.RowType = 1;

                        break;
                    case 2:         //库房          //显示  因为没有明确的收款人  结算表不方便结算
                        ObjStatementModel.Name = "库房";
                        ObjStatementModel.SupplierName = "库房";
                        ObjStatementModel.TypeID = -1;
                        ObjStatementModel.TypeName = "库房";
                        ObjStatementModel.RowType = 2;

                        break;
                    case 3:         //新购买
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -5;
                        ObjStatementModel.TypeName = "新购买";
                        ObjStatementModel.RowType = 3;

                        break;
                    case 4:         //四大金刚
                        string name = item.Name.Replace("(预定)", "");
                        var ObjFourGuardianModel = ObjFourGuardianBLL.GetByName(name);
                        if (ObjFourGuardianModel == null)
                        {
                            var ObjEmployeeModel = ObjEmployeeBLL.GetByName(item.Name);
                            if (ObjEmployeeModel != null)
                            {
                                ObjStatementModel.Name = item.Name;
                                ObjStatementModel.SupplierName = item.Name;
                                ObjStatementModel.SupplierID = ObjEmployeeModel.EmployeeID;
                                ObjStatementModel.TypeID = -2;
                                ObjStatementModel.TypeName = "内部人员";
                                ObjStatementModel.RowType = 5;
                            }
                        }
                        if (ObjFourGuardianModel != null)
                        {
                            GuardianType ObjGuardTypeBLL = new GuardianType();

                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = ObjFourGuardianModel.GuardianId;
                            ObjStatementModel.TypeID = ObjFourGuardianModel.GuardianTypeId.ToString().ToInt32();
                            ObjStatementModel.TypeName = ObjGuardTypeBLL.GetByID(ObjFourGuardianModel.GuardianTypeId).TypeName;
                            ObjStatementModel.RowType = 4;
                        }
                        break;
                    case 5:         //人员
                        var ObjEmployeeModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (ObjEmployeeModels != null)
                        {
                            ObjStatementModel.Name = item.Name;
                            ObjStatementModel.SupplierName = item.Name;
                            ObjStatementModel.SupplierID = ObjEmployeeModels.EmployeeID;
                            ObjStatementModel.TypeID = -2;
                            ObjStatementModel.TypeName = "内部人员";
                            ObjStatementModel.RowType = 5;
                        }
                        break;
                    case 6:         //设计师/工程主管
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -3;
                        ObjStatementModel.TypeName = "设计师";
                        var EmployeeModel = ObjEmployeeBLL.GetByName(item.Name);
                        ObjStatementModel.SupplierID = EmployeeModel.EmployeeID;
                        ObjStatementModel.RowType = 5;
                        break;
                    case 7:         //内部人员/四大金刚 (手动添加)
                        var PersonModel = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModel == null)
                        {
                            ObjStatementModel.SupplierID = ObjFourGuardianBLL.GetByName(item.Name).GuardianId;
                            ObjStatementModel.RowType = 4;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = PersonModel.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -4;
                        ObjStatementModel.TypeName = "人员(手动添加)";
                        break;
                    case 8:         //内部人员/供应商 (手动添加)
                        var PersonModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (PersonModels == null)
                        {
                            ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                            ObjStatementModel.RowType = 1;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = PersonModels.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -3;
                        ObjStatementModel.TypeName = "物料 (手动添加)";
                        break;
                    case 9:         //内部人员/供应商 (手动添加)
                        var OtherModels = ObjEmployeeBLL.GetByName(item.Name);
                        if (OtherModels == null)
                        {
                            ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                            ObjStatementModel.RowType = 1;
                        }
                        else
                        {
                            ObjStatementModel.SupplierID = OtherModels.EmployeeID;
                            ObjStatementModel.RowType = 5;
                        }
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -2;
                        ObjStatementModel.TypeName = "其他";
                        break;
                    case 10:         //设计清单(name代表供应商 国色广告)
                        ObjStatementModel.SupplierID = ObjSupplierBLL.GetByName(item.Name).SupplierID;
                        ObjStatementModel.Name = item.Name;
                        ObjStatementModel.SupplierName = item.Name;
                        ObjStatementModel.TypeID = -4;
                        ObjStatementModel.TypeName = "设计清单";
                        ObjStatementModel.RowType = 1;
                        break;
                }
                #endregion

                ObjStatementModel.CustomerID = ObjQuotedPriceModel.CustomerID.ToString().ToInt32();
                ObjStatementModel.CreateEmployee = User.Identity.Name.ToInt32();
                ObjStatementModel.CreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                ObjStatementModel.DispatchingID = DispatchingID;
                ObjStatementModel.OrderId = ObjQuotedPriceModel.OrderID.ToString().ToInt32();
                ObjStatementModel.QuotedId = ObjQuotedPriceModel.QuotedID.ToString().ToInt32();
                ObjStatementModel.Remark = "";
                ObjStatementModel.Finishtation = "";
                ObjStatementModel.SumTotal = item.Sumtotal;
                ObjStatementModel.Content = item.Content;
                ObjStatementModel.PayMent = 0;
                ObjStatementModel.NoPayMent = item.Sumtotal;
                ObjStatementModel.CostSumId = item.CostSumId;
                ObjStatementModel.Year = ObjCustomerBLL.GetByCustomerID(CustomerID).Partydate.Value.Year;
                ObjStatementModel.Month = ObjCustomerBLL.GetByCustomerID(CustomerID).Partydate.Value.Month;


                FL_Statement StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, item.Name);
                if (StatementModel != null)    //已经存在
                {
                    StatementModel.Name = ObjStatementModel.Name;               //名称
                    StatementModel.SupplierID = ObjStatementModel.SupplierID;   //供应商ID
                    StatementModel.Content = ObjStatementModel.Content;         //内容(说明)
                    StatementModel.TypeID = ObjStatementModel.TypeID;           //类型ID
                    StatementModel.TypeName = ObjStatementModel.TypeName;       //类型名称
                    StatementModel.RowType = ObjStatementModel.RowType;         //供应商类别
                    StatementModel.SumTotal = ObjStatementModel.SumTotal;       //金额
                    StatementModel.PayMent = 0;                                 //已付款
                    StatementModel.NoPayMent = ObjStatementModel.NoPayMent;     //未付款
                    ObjStatementBLL.Update(StatementModel);                     //修改更新
                }
                else
                {
                    ObjStatementBLL.Insert(ObjStatementModel);
                }

                #endregion
            }
        }
        #endregion

        #region 根据CustomerID获取DispatchingID
        /// <summary>
        /// 获取DipatchingID
        /// </summary>      
        public string GetDisID(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjDispatchingModel = ObjDispatchingBLL.GetByCustomerID(CustomerID);
            if (ObjDispatchingModel != null)
            {
                return ObjDispatchingModel.DispatchingID.ToString();
            }
            return "";
        }
        #endregion

        #region 修改颜色
        /// <summary>
        /// 根据状态修改颜色
        /// </summary>
        public string ChangeForColor(object Source)
        {
            int DesignState = Source.ToString().ToInt32();
            return DesignState == 2 ? "style ='color:red'" : string.Empty;
        }
        #endregion

        #region 修改颜色
        /// <summary>
        /// 订单金额超过3W 变成蓝色标注
        /// </summary>
        public string ChangeColorForMoney(object Source)
        {
            int QuotedID = Source.ToString().ToInt32();
            var Model = ObjQuotedPriceBLL.GetByID(QuotedID);
            return Model.FinishAmount >= 30000 ? "style ='color:Blue'" : string.Empty;
        }
        #endregion

        #region 只有设计师才可以改派
        /// <summary>
        /// 改派执行设计
        /// </summary>
        public void DesignUpdate()
        {
            //for (int i = 0; i < repCustomer.Items.Count; i++)
            //{
            //    var ObjItem = repCustomer.Items[i];
            //    TextBox txtDesinger = ObjItem.FindControl("txtEmployees") as TextBox;
            //    int CustomerID = (ObjItem.FindControl("hideCustomerHide") as HiddenField).Value.ToString().ToInt32();
            //    var Model = ObjQuotedPriceBLL.GetByCustomerID(CustomerID);
            //    if (Model != null)
            //    {
            //        if (Model.DesignerEmployee != User.Identity.Name.ToInt32())     //登录客户和策划师不是同一个人就不能改派
            //        {
            //            txtDesinger.Enabled = false;
            //        }
            //        else
            //        {
            //            txtDesinger.Enabled = true;
            //        }
            //    }
            //}
        }
        #endregion

        #region 获取QuotedID
        /// <summary>
        /// 根据OrderID获取QuotedID
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetQuotedID(object OrderID)
        {
            return new BLLAssmblly.Flow.QuotedPrice().GetByOrderId(OrderID.ToString().ToInt32()).QuotedID.ToString();
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志的显示
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion

    }
}