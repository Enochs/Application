using HA.PMS.Pages;
using System;
using System.Linq;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer
{
    public partial class CustomerImport : SystemPage
    {
        private BLLAssmblly.Sys.Employee employeeBLL = new BLLAssmblly.Sys.Employee();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImportCustomers_Click(object sender, EventArgs e)
        {
            #region 保存文件
            string fileName = uplExcel.FileName;//上传文件的名称（包括扩展名）
            string fileExt = System.IO.Path.GetExtension(uplExcel.FileName);//文件扩展名
            string SavePath = string.Format("{0}{1}", Server.MapPath("~/Files/CustomersImport/"), DateTime.Now.ToString("yyyyMMdd_hhmmss") + fileName);//文件保存全路径
            string[] SurportExt = { ".xls", ".xlsx" };

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(SavePath));

            //文件验证
            if (!uplExcel.HasFile)
            {
                JavaScriptTools.AlertWindow("请先选择文件！", Page); return;
            }
            if (!SurportExt.Contains(fileExt))
            {
                JavaScriptTools.AlertWindow("该文件不是有效的 Excel 文件！Excel 一般扩展名为：xls，xlsx", Page); return;
            }
            //保存在磁盘
            uplExcel.SaveAs(SavePath); 
            #endregion

            #region 读取文件
            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                NPOI.SS.UserModel.IWorkbook workbook = NPOI.SS.UserModel.WorkbookFactory.Create(System.IO.File.OpenRead(SavePath));
                dataTable = new DataCastTool().ToDataTable(workbook.GetSheetAt(0)); ;
            }
            catch (Exception ex)
            {
                JavaScriptTools.AlertWindow("该文件已损坏或不是有效的Excel 97-2003 或 Excel 2007文件请下载最新模版尝试！", Page); return;
            } 
            #endregion
            
            //必填字段列验证
            string[] requiredField = { "*新郎姓名", "*新娘姓名", "*新娘手机", "*新娘生日", "*婚期", "*酒店", "*合同金额" };
            foreach (string field in requiredField)
            {
                if (!dataTable.Columns.Contains(field))
                {
                    JavaScriptTools.AlertWindow("列信息不完整，请下载最新模版！", Page); return;
                }
            }

            //错误信息表
            System.Data.DataTable failedCus = new System.Data.DataTable();
            failedCus.Columns.Add("Index");
            failedCus.Columns.Add("Bride");
            failedCus.Columns.Add("Groom");
            failedCus.Columns.Add("BrideCellPhone");
            failedCus.Columns.Add("PartyDate");
            failedCus.Columns.Add("Wineshop");
            failedCus.Columns.Add("ErrorMsg");

            DataAssmblly.FL_Order OrderModel = new HA.PMS.DataAssmblly.FL_Order();
            DataAssmblly.FL_Customers CustomerModel = new HA.PMS.DataAssmblly.FL_Customers();
            DataAssmblly.FL_QuotedPrice QuotedPriceModel = new HA.PMS.DataAssmblly.FL_QuotedPrice();
            BLLAssmblly.Flow.Customers ObjCustomersBLL = new HA.PMS.BLLAssmblly.Flow.Customers();
            BLLAssmblly.Flow.Order ObjOrderBLL = new HA.PMS.BLLAssmblly.Flow.Order();
            BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                System.Data.DataRow dr = dataTable.Rows[i];

                int CustomerID, OrderID, QuotedID;
                try
                {
                    #region 1.插入新人信息

                    CustomerModel.Groom = GetDataRowString(dr, "*新郎姓名", true);
                    CustomerModel.Bride = GetDataRowString(dr, "*新娘姓名", true);
                    CustomerModel.Operator = GetDataRowString(dr, "经办人姓名");

                    CustomerModel.GroomCellPhone = GetDataRowString(dr, "新郎手机");
                    CustomerModel.BrideCellPhone = GetDataRowString(dr, "*新娘手机", true);
                    if (new HA.PMS.DataAssmblly.PMS_WeddingEntities().FL_Customers.Where(C => C.BrideCellPhone.Equals(CustomerModel.BrideCellPhone)).Count() > 0)
                    {
                        throw new Exception("该会员已存在");
                    }
                    CustomerModel.OperatorPhone = GetDataRowString(dr, "经办人手机");

                    CustomerModel.GroomBirthday = GetDataRowDate(dr, "新郎生日");
                    CustomerModel.BrideBirthday = GetDataRowDate(dr, "*新娘生日", true);
                    CustomerModel.OperatorBrithday = GetDataRowDate(dr, "经办人生日").ToString();

                    CustomerModel.GroomtelPhone = GetDataRowString(dr, "新郎座机");
                    CustomerModel.BridePhone = GetDataRowString(dr, "新娘座机");
                    CustomerModel.OperatorTelPhone = GetDataRowString(dr, "经办人座机");

                    CustomerModel.GroomEmail = GetDataRowString(dr, "新郎邮件");
                    CustomerModel.BrideEmail = GetDataRowString(dr, "新娘邮件");
                    CustomerModel.OperatorEmail = GetDataRowString(dr, "经办人邮件");

                    CustomerModel.GroomteWeixin = GetDataRowString(dr, "新郎微信");
                    CustomerModel.BrideWeiXin = GetDataRowString(dr, "新娘微信");
                    CustomerModel.OperatorWeiXin = GetDataRowString(dr, "经办人微信");

                    CustomerModel.GroomWeiBo = GetDataRowString(dr, "新郎微博");
                    CustomerModel.BrideWeiBo = GetDataRowString(dr, "新娘微博");
                    CustomerModel.OperatorWeiBo = GetDataRowString(dr, "经办人微博");

                    CustomerModel.GroomQQ = GetDataRowString(dr, "新郎QQ");
                    CustomerModel.BrideQQ = GetDataRowString(dr, "新娘QQ");
                    CustomerModel.OperatorQQ = GetDataRowInt(dr, "经办人QQ").ToString();

                    CustomerModel.GroomJob = GetDataRowString(dr, "新郎职业");
                    CustomerModel.BrideJob = GetDataRowString(dr, "新娘职业");
                    CustomerModel.OperatorRelationship = GetDataRowString(dr, "关系");

                    CustomerModel.GroomJobCompany = GetDataRowString(dr, "新郎单位");
                    CustomerModel.BrideJobCompany = GetDataRowString(dr, "新娘单位");
                    CustomerModel.OperatorCompany = GetDataRowString(dr, "经办人单位");

                    CustomerModel.PartyDate = GetDataRowDate(dr, "*婚期", true);
                    CustomerModel.Wineshop = GetDataRowString(dr, "*酒店", true);
                    CustomerModel.TimeSpans = GetDataRowString(dr, "时段");
                    CustomerModel.Other = GetDataRowString(dr, "说明");


                    CustomerModel.Referee = "直接到店";
                    CustomerModel.Channel = "自己到店";
                    CustomerModel.ChannelType = 0;

                    CustomerModel.LikeColor = string.Empty;
                    CustomerModel.Hobbies = string.Empty;
                    CustomerModel.NoTaboos = string.Empty;
                    CustomerModel.ExpectedAtmosphere = string.Empty;
                    CustomerModel.DesiredAppearance = string.Empty;
                    CustomerModel.FormMarriage = string.Empty;
                    CustomerModel.WeddingServices = string.Empty;
                    CustomerModel.ImportantProcess = string.Empty;
                    CustomerModel.Experience = string.Empty;

                    CustomerModel.State = -1;
                    CustomerModel.CustomerStatus = "批量导入";
                    CustomerModel.IsLose = false;
                    CustomerModel.IsDelete = false;
                    CustomerModel.FinishOver = true;
                    CustomerModel.Recorder = User.Identity.Name.ToInt32();
                    CustomerModel.RecorderDate = DateTime.Now;

                    CustomerID = ObjCustomersBLL.Insert(CustomerModel);
                    #endregion
                }
                catch (Exception ex)
                {
                    failedCus.LoadDataRow(GetFailedCustomerMsgRow(dr, i, ex), true);
                    continue;
                }
                try
                {
                    #region 2.插入跟单信息

                    OrderModel.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(CustomerModel.PartyDate.Value);
                    OrderModel.CustomerID = CustomerID;
                    OrderModel.FollowSum = 0;
                    OrderModel.FlowCount = 0;
                    if (GetEmployeeIDByName(GetDataRowString(dr, "*婚礼顾问", true)) > 0)
                    {
                        OrderModel.EmployeeID = GetEmployeeIDByName(GetDataRowString(dr, "*婚礼顾问", true));
                    }
                    else
                    {
                        throw new Exception("婚礼顾问不存在");
                    }
                    OrderModel.EarnestMoney = GetDataRowDecimal(dr, "定金");
                    OrderModel.EarnestFinish = true;
                    OrderModel.LastFollowDate = DateTime.Now;
                    OrderModel.CreateDate = DateTime.Now;

                    OrderID = ObjOrderBLL.Insert(OrderModel);
                    #endregion
                }
                catch (Exception ex)
                {
                    failedCus.LoadDataRow(GetFailedCustomerMsgRow(dr, i, ex), true); 
                    ObjCustomersBLL.Delete(CustomerModel); 
                    continue; 
                }
                try
                {
                    #region 3.插入报价单信息

                    QuotedPriceModel.CustomerID = CustomerID;
                    QuotedPriceModel.OrderID = OrderID;
                    QuotedPriceModel.OrderCoder = OrderModel.OrderCoder;
                    if (GetEmployeeIDByName(GetDataRowString(dr, "*策划师")) > 0)
                    {
                        QuotedPriceModel.EmpLoyeeID = GetEmployeeIDByName(GetDataRowString(dr, "*策划师"));
                    }
                    else
                    {
                        throw new Exception("策划师不存在");
                    }
                    QuotedPriceModel.CategoryName = "开始制作报价单";
                    QuotedPriceModel.FinishAmount = GetDataRowDecimal(dr, "*合同金额", true);
                    QuotedPriceModel.EarnestMoney = GetDataRowDecimal(dr, "首期预付款");
                    QuotedPriceModel.RealAmount = 0;
                    QuotedPriceModel.CheckState = 0;
                    QuotedPriceModel.ChecksEmployee = User.Identity.Name.ToInt32();
                    QuotedPriceModel.ParentQuotedID = 0;
                    QuotedPriceModel.StarDispatching = true;
                    QuotedPriceModel.IsChecks = false;
                    QuotedPriceModel.IsFirstCreate = true;
                    QuotedPriceModel.HaveFile = false;
                    QuotedPriceModel.IsDelete = true;
                    QuotedPriceModel.NextFlowDate = OrderModel.LastFollowDate;
                    QuotedPriceModel.IsDispatching = 6;
                    QuotedPriceModel.CreateDate = DateTime.Now;

                    QuotedID = ObjQuotedPriceBLL.Insert(QuotedPriceModel);
                    #endregion
                }
                catch (Exception ex) 
                {
                    failedCus.LoadDataRow(GetFailedCustomerMsgRow(dr, i, ex), true); 
                    ObjOrderBLL.Delete(OrderModel);
                    ObjCustomersBLL.Delete(CustomerModel);
                    continue;
                }
            }
            if (failedCus.Rows.Count == 0)
            {
                JavaScriptTools.AlertWindow(string.Format("导入成功！共 {0} 位会员", dataTable.Rows.Count), Page);
            }
            else
            {
                rptErrorMsg.DataBind(failedCus);
                JavaScriptTools.AlertWindow(string.Format("已导入 {0} 位会员，失败 {1} 位", dataTable.Rows.Count - failedCus.Rows.Count, failedCus.Rows.Count), Page);
            }
        }

        private DateTime GetDataRowDate(System.Data.DataRow dataRow, string columName, bool IsMust = false)
        {
            DateTime result;
            if (dataRow.Table.Columns.Contains(columName))
            {
                if (!string.IsNullOrWhiteSpace(dataRow[columName].ToString()))
                {
                    if (DateTime.TryParse(dataRow[columName].ToString(), out result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception(string.Format("{0} 格式错误，请参考模版日期格式", columName));
                    }
                }
                else
                {
                    if (IsMust)
                    {
                        throw new Exception(string.Format("{0} 为必填字段，不能为空", columName));
                    }
                    else
                    {
                        return MinDateTime;
                    }
                }
            }
            return MinDateTime;
        }

        private string GetDataRowString(System.Data.DataRow dataRow, string columName, bool IsMust = false)
        {
            if (dataRow.Table.Columns.Contains(columName))
            {
                if (IsMust && string.IsNullOrWhiteSpace(dataRow[columName].ToString()))
                {
                    throw new Exception(string.Format("{0} 为必填字段，不能为空", columName));
                }
                else
                {
                    return dataRow[columName].ToString();
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private int GetDataRowInt(System.Data.DataRow dataRow, string columName, bool IsMust = false)
        {
            int result = 0;
            if (dataRow.Table.Columns.Contains(columName))
            {
                if (!string.IsNullOrWhiteSpace(dataRow[columName].ToString()))
                {
                    if (int.TryParse(dataRow[columName].ToString(), out result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception(string.Format("{0} 格式错误，只能为整数", columName));
                    }
                }
                else
                {
                    if (IsMust)
                    {
                        throw new Exception(string.Format("{0} 为必填字段，不能为空", columName));
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return 0;
            }
        }

        private decimal GetDataRowDecimal(System.Data.DataRow dataRow, string columName, bool IsMust = false)
        {
            decimal result = 0;
            if (dataRow.Table.Columns.Contains(columName))
            {
                if (!string.IsNullOrWhiteSpace(dataRow[columName].ToString()))
                {
                    if (decimal.TryParse(dataRow[columName].ToString(), out result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception(string.Format("{0} 格式错误，只能整数或小数", columName));
                    }
                }
                else
                {
                    if (IsMust)
                    {
                        throw new Exception(string.Format("{0} 为必填字段，不能为空", columName));
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return 0;
            }
        }

        private int GetEmployeeIDByName(string empLoyeeName)
        {
            return employeeBLL.GetByName(empLoyeeName).EmployeeID;
        }

        private object[] GetFailedCustomerMsgRow(System.Data.DataRow dataRow, int index, Exception ex)
        {
            return new object[] {
                    index,
                    GetDataRowString(dataRow, "*新娘姓名"),
                     GetDataRowString(dataRow, "*新郎姓名"),
                     GetDataRowString(dataRow, "*新娘手机"),
                     GetDataRowDate(dataRow, "*婚期"),
                      GetDataRowString(dataRow, "*酒店"),
                     ex.Message
                    };
        }
       
        protected void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            string TemplatePath = Server.MapPath("~/Files/CustomersImport/ImportTemplate.xls");//文件保存目录
            IOTools.DownLoadExcel(TemplatePath, "会员导入模版.xls");
        }
    }
}