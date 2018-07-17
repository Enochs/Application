using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.CS;
using System.IO;
using System.Web;
using HA.PMS.BLLAssmblly.FinancialAffairsbll;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.CustomerSystem;
using HHLWedding.BLLAssmbly;

namespace HA.PMS.Pages
{
    /// <summary>
    /// 系统页
    /// </summary>
    public class SystemPage : System.Web.UI.Page
    {
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();

        Report objReportBLL = new Report();

        Order objOrderBLL = new Order();
        /// <summary>
        /// 用户控件权限
        /// </summary>
        JurisdictionforButton ObjJurisdictionforButtonBLL = new JurisdictionforButton();
        Customers objCustomersBLLs = new Customers();

        Employee ObjEmployeeBLL = new Employee();

        Invite ObjEnviteBll = new Invite();

        CelebrationPackage ObjCeleBrationBLL = new CelebrationPackage();


        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();

        Department ObjDepartmentBLL = new Department();


        /// <summary>
        /// 服务类型
        /// </summary>
        Member ObjMemberBLL = new Member();



        /// <summary>
        /// 系统配置
        /// </summary>
        SysConfig ObjConfigBLL = new SysConfig();

        /// <summary>
        /// 客户
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.Customers ObjCustomerBLL = new BLLAssmblly.Flow.Customers();

        /// <summary>
        /// 构造函数  验证用户是否对此模块有访问权限
        /// </summary>
        public SystemPage()
        {

            int EmpLoyeeID = User.Identity.Name.ToInt32();
            this.StyleSheetTheme = "Default";

            //HASystem ObjHASystemBLL = new HASystem();
            //System.IO.StreamReader objReader = new System.IO.StreamReader(Server.MapPath("/SN.txt"));
            //var DISDID = objReader.ReadToEnd();
            //if (DISDID == "")
            //{
            //    System.Web.HttpContext.Current.Response.End();
            //}
            //if (ObjHASystemBLL.GetByaall(DISDID) != "Sucess")
            //{
            //    objReader.Close();
            //    System.Web.HttpContext.Current.Response.End();
            //}
            //else
            //{
            //    while (true)
            //    {
            //        System.Web.HttpContext.Current.Response.Write("啊");
            //    }
            //}

            //if (System.Web.HttpContext.Current.Request["NeedPopu"] != null)
            //{
            //    this.MasterPageFile = "~/AdminPanlWorkArea/Master/PopuMaster.Master";
            //}
            //if (System.Web.HttpContext.Current.Request["ChannelID"] != null)
            //{
            //    //判断是否有权限访问此页面
            //    if (IsHavePower(System.Web.HttpContext.Current.Request["ChannelID"].ToInt32()))
            //    {
            //        if (!ObjUserJurisdictionBLL.GetUserJurisdictionByChannel(EmpLoyeeID, System.Web.HttpContext.Current.Request["ChannelID"].ToInt32()))
            //        {
            //            System.Web.HttpContext.Current.Response.End();
            //        }

            //    }
            //    else
            //    {
            //        System.Web.HttpContext.Current.Response.End();
            //    }
            //}
            //else
            //{

            //   // System.Web.HttpContext.Current.Response.End();
            //}


        }


        /// <summary>
        /// 获取余款数据
        /// </summary>
        /// <param name="QuotedID"></param>
        /// <returns></returns>
        public string GetOverFinishMoney(object CustomerID)
        {

            /// <summary>
            /// 收款计划
            /// </summary>
            int CID = CustomerID.ToString().ToInt32();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();
            decimal AllMoney = 0;
            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CID);
            string Balance = "";
            if (QuotedModel != null)
            {

                AllMoney = QuotedModel.FinishAmount.HasValue ? QuotedModel.FinishAmount.Value : 0;
                decimal FinishMoney = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID).Sum(C => C.RealityAmount).ToString().ToDecimal();
                Balance = (AllMoney - FinishMoney).ToString();
            }
            return Balance;

        }


        public string SetStyleforSystemControlKey(string Key)
        {
            return ObjConfigBLL.SetStyleforSystemControlKey(Key);
        }

        public string SetStyleforSystemControlKey(string Key, int Type)
        {
            return ObjConfigBLL.SetStyleforSystemControlKey(Key, Type);
        }

        public bool GetNeedMamagerByKey(string Key)
        {
            return ObjConfigBLL.GetNeedMamagerByKey(Key);
        }

        public string GetMissionState(object IsLook, object IsOver)
        {
            if (IsLook.ToString().ToBool() && IsOver.ToString().ToBool())
            {
                return "已完结";
            }

            if (IsLook.ToString().ToBool() && !IsOver.ToString().ToBool())
            {
                return "已查看";
            }

            if (!IsLook.ToString().ToBool() && !IsOver.ToString().ToBool())
            {
                return "未查看";
            }

            return "未查看";
        }

        /// <summary> 
        /// 将字符串使用base64算法加密 
        /// </summary> 
        /// <param name="code_type">编码类型（编码名称） 
        /// * 代码页 名称 
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2" 
        /// * 1201 "UTF-16BE"或"unicodeFFFE" 
        /// * 1252 "windows-1252" 
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7" 
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8" 
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991" 
        /// * 54936 "GB18030"    
        /// </param> 
        /// <param name="code">待加密的字符串</param> 
        /// <returns>加密后的字符串</returns> 
        public string EncodeBase64(string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(code);  //将一组字符编码为一个字节序列. 
            try
            {
                encode = Convert.ToBase64String(bytes);  //将8位无符号整数数组的子集转换为其等效的,以64为基的数字编码的字符串形式. 
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        /// <summary>
        /// 获取流失详细原因
        /// </summary>
        /// <param name="ContentKey"></param>
        /// <returns></returns>
        public string GetLoseContentDetails(object ContentKey)
        {
            if (ContentKey != null)
            {
                int orderId = ContentKey.ToString().ToInt32();
                return ObjOrderBLL.GetLoseContentByOrderID(orderId);

            }
            return "";
        }
        /// <summary> 
        /// 将字符串使用base64算法解密 
        /// </summary> 
        /// <param name="code_type">编码类型</param> 
        /// <param name="code">已用base64算法加密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public string DecodeBase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);  //将2进制编码转换为8位无符号整数数组. 
            try
            {
                decode = Encoding.GetEncoding("utf-8").GetString(bytes);  //将指定字节数组中的一个字节序列解码为一个字符串。 
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        public int GetLoginUserID()
        {

            return User.Identity.Name.ToInt32();
        }


        #region 获取页面状态方法
        /// <summary>
        /// 获取上传提案
        /// </summary>
        /// <returns></returns>
        public string GetFileDate(object QuotedID)
        {
            if (QuotedID != null)
            {
                var ObjQuotedFileModel = ObjQuotedPriceBLL.GetQuotedPricefileByQuotedID(QuotedID.ToString().ToInt32(), 2);
                if (ObjQuotedFileModel.Count > 0)
                {
                    return ObjQuotedFileModel.First().CreateDate.Value.ToShortDateString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public int GetQuotedIDByCustomerID(object source)
        {
            int customerId = (source.ToString()).ToInt32();
            FL_QuotedPrice queryOrder = ObjQuotedPriceBLL.GetByCustomerID(customerId);
            if (queryOrder != null)
            {
                return queryOrder.QuotedID;
            }

            return 0;
        }

        /// <summary>
        /// 根据客户ID返回订单号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetOrderCoderByCustomerId(object source)
        {
            int customerId = (source.ToString()).ToInt32();
            FL_Order queryOrder = objOrderBLL.GetbyCustomerID(customerId);
            if (queryOrder != null)
            {
                return queryOrder.OrderCoder;
            }

            return string.Empty;

        }
        /// <summary>
        /// 根据客户ID返回订单ID
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public int GetOrderIdByCustomerID(object source)
        {
            int customerId = (source.ToString()).ToInt32();
            FL_Order queryOrder = objOrderBLL.GetbyCustomerID(customerId);
            if (queryOrder != null)
            {
                return queryOrder.OrderID;
            }

            return 0;
        }


        /// <summary>
        /// 获取婚礼顾问
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetOrderEmpLoyeeNameByCustomerID(object Source)
        {
            int customerId = (Source.ToString()).ToInt32();
            FL_Order queryOrder = objOrderBLL.GetbyCustomerID(customerId);
            if (queryOrder != null)
            {
                return GetEmployeeName(queryOrder.EmployeeID);
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取新娘新明
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetBrideByCustomerId(object source)
        {
            int customerId = (source.ToString()).ToInt32();
            FL_Customers objCustomer = objCustomersBLLs.GetByID(customerId);
            if (objCustomer != null)
            {
                return objCustomer.Bride;
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据渠道名称返回渠道详细页的链接
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetChannelHref(object source)
        {
            SaleSources objSaleSourcesBLL = new SaleSources();
            if (source != null)
            {


                HA.PMS.DataAssmblly.FD_SaleSources sale = objSaleSourcesBLL.GetByName(source.ToString().Trim());

                if (sale != null)
                {
                    return "<a href='/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SaleSourcesDetails.aspx?sourceID=" +

                    sale.SourceID + "' target='_blank'>" + source.ToString() + "</a>";

                }
            }
            return "";
        }

        public string GetOrderEmpLoyeeName(object OrderID)
        {
            try
            {
                if (OrderID != null)
                {
                    if (OrderID.ToString() != string.Empty)
                    {
                        var ObjModel = ObjEmployeeBLL.GetByID(ObjOrderBLL.GetByID(OrderID.ToString().ToInt32()).EmployeeID);
                        if (ObjModel != null)
                        {
                            return ObjModel.EmployeeName;
                        }
                    }
                }
                else
                {
                    return string.Empty;
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 根据orderId，返回订单号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetOrderCodeByOrderId(object source)
        {
            int orderId = (source + string.Empty).ToInt32();
            FL_Order order = objOrderBLL.GetByID(orderId);
            if (order != null)
            {
                return order.OrderCoder;
            }
            return string.Empty;
        }


        public string GetOrderCodeByCustomerID(object source)
        {
            int orderId = (source + string.Empty).ToInt32();
            FL_Order order = objOrderBLL.GetbyCustomerID(orderId);
            if (order != null)
            {
                return order.OrderCoder;
            }
            return string.Empty;
        }
        /// <summary>   
        /// 
        /// </summary>   
        /// <param name="filesize">文件的大小,传入的是一个bytes为单位的参数</param>   
        /// <returns>格式化后的值</returns>   
        public string FormatFileSize(long filesize)
        {
            if (filesize < 0)
            {
                throw new ArgumentOutOfRangeException("filesize");
            }
            else if (filesize >= 1024 * 1024 * 1024) //文件大小大于或等于1024MB   
            {
                return string.Format("{0:0.00}GB", (double)filesize / (1024 * 1024 * 1024));
            }
            else if (filesize >= 1024 * 1024) //文件大小大于或等于1024KB   
            {
                return string.Format("{0:0.00}MB", (double)filesize / (1024 * 1024));
            }
            else if (filesize >= 1024) //文件大小大于等于1024bytes   
            {
                return string.Format("{0:0.00}KB", (double)filesize / 1024);
            }
            else
            {
                return string.Format("{0:0.00}BY", filesize);
            }
        }
        /// <summary>
        /// 获取报价单责任人名字
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetQuotedEmpLoyeeName(object OrderID)
        {
            try
            {
                if (OrderID != null)
                {
                    if (OrderID.ToString() != string.Empty)
                    {
                        var ObjModel = ObjEmployeeBLL.GetByID(ObjQuotedPrice.GetByOrderId(OrderID.ToString().ToInt32()).EmpLoyeeID);
                        if (ObjModel != null)
                        {
                            return ObjModel.EmployeeName;
                        }
                    }
                }
                else
                {
                    return string.Empty;
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }


        public string GetQuotedEmpLoyeeID(object OrderID)
        {
            try
            {
                if (OrderID != null)
                {
                    if (OrderID.ToString() != string.Empty)
                    {
                        var ObjModel = ObjEmployeeBLL.GetByID(ObjQuotedPrice.GetByOrderId(OrderID.ToString().ToInt32()).EmpLoyeeID);
                        if (ObjModel != null)
                        {
                            return ObjModel.EmployeeID.ToString();
                        }
                    }
                }
                else
                {
                    return string.Empty;
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion


        #region 用于折线单位换算
        /// <summary>
        /// 百分比
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected string GetDoubleFormat(double number)
        {

            string ReturnValue = Math.Round(number, 2) * 100 + "%";
            if (ReturnValue.Contains("非数字"))
            {
                return "0%";
            }
            else
            {
                return ReturnValue;
            }


        }


        /// <summary>
        /// 锁定所有项目
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public string GetRemoveClassByOrderID(object OrderID)
        {
            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjModel = ObjDispatchingBLL.GetByOrder(OrderID.ToString().ToInt32());
            if (ObjModel != null)
            {
                if (ObjModel.Isover.Value)
                {
                    return "RemoveClass";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }

        }



        /// <summary>
        /// 获取派工人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetDispatchingEmpLoyee(object CustomerID)
        {
            Dispatching ObjDispatchingBLL = new Dispatching();
            var ObjModel = ObjDispatchingBLL.GetDispatchingByCustomerID(CustomerID.ToString().ToInt32());
            if (ObjModel != null)
            {
                return GetEmployeeName(ObjModel.EmployeeID);
            }
            else
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetSubString(string source)
        {
            return source.Substring(0, source.ToString().LastIndexOf(','));
        }
        protected string GetDoubleFormat2(double number)
        {
            return Math.Round(number, 2) * 100 + string.Empty;


        }
        #endregion
        /// <summary>
        /// 更改double格式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetMoneyDouble(object source)
        {
            return Convert.ToDouble(source) + string.Empty;
        }
        /// <summary>
        /// 计划到店时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetToStrose(object source)
        {
            int customersId = (source + string.Empty).ToInt32();
            Invite ObjInviteBLL = new Invite();
            FL_Invite orders = ObjInviteBLL.GetByCustomerID(customersId);
            if (orders != null)
            {
                return GetDateStr(orders.ComeDate);
            }
            else
            {
                FL_Order ObjOrderModel = new Order().GetbyCustomerID(customersId);
                if (ObjOrderModel != null)
                {
                    return GetDateStr(ObjOrderModel.CreateDate);
                }
            }
            return string.Empty;

        }


        #region 获取页面方法1
        /// <summary>
        /// 根据客户ID返回门店名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetStoreHouseByCustomerId(object source)
        {
            int customersId = (source + string.Empty).ToInt32();

            Invite objInviteBLL = new Invite();
            FL_Invite invite = objInviteBLL.GetByCustomerID(customersId);
            if (invite != null)
            {
                Department objDepartmentBLL = new Department();
                Sys_Department depart = objDepartmentBLL.GetByID(invite.ToDepartMent);
                if (depart != null)
                {
                    return depart.DepartmentName;
                }

            }
            else
            {
                var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(customersId);
                if (ObjOrderModel != null)
                {
                    Department objDepartmentBLL = new Department();

                    Sys_Department depart = objDepartmentBLL.GetByID(ObjEmployeeBLL.GetByID(ObjOrderModel.EmployeeID).DepartmentID);
                    if (depart != null)
                    {
                        return depart.DepartmentName;
                    }
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据客户ID返回对应的策划师名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetPlannerNameByCustomersId(object source)
        {
            int customersId = (source + string.Empty).ToInt32();


            var invite = ObjQuotedPrice.GetByCustomerID(customersId);
            if (invite != null)
            {
                return GetEmployeeName(invite.EmpLoyeeID);
            }
            return string.Empty;

        }
        /// <summary>
        /// 查询对应的邀约负责人 新人对应的邀约人
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetInviteName(object source)
        {
            if (source != null)
            {

                HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
                FL_Invite ints = objInviteBLL.GetByCustomerID((int)source);

                if (ints != null)
                {
                    return GetEmployeeName(ints.EmpLoyeeID);
                }
            }
            return "暂无";
        }
        /// <summary>
        /// 获取类别名
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetCategoryName(object source)
        {
            Category objCategoryBLL = new Category();
            if (source != null)
            {
                FD_Category category = objCategoryBLL.GetByID((source + string.Empty).ToInt32());
                if (category != null)
                {
                    return category.CategoryName;
                }

            }
            return "";


        }


        /// <summary>
        /// 获取类别名
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetQuotedCategoryName(object source)
        {
            QuotedCatgory objCategoryBLL = new QuotedCatgory();
            if (source != null)
            {
                FD_QuotedCatgory category = objCategoryBLL.GetByID((source + string.Empty).ToInt32());
                if (category != null)
                {
                    return category.Title;
                }

            }
            return "";


        }
        /// <summary>
        /// 获取用户的部门名称
        /// </summary>
        /// <param name="EmpLoyeeKey"></param>
        /// <returns></returns>
        public string GetDepartmentByEnpLoyeeID(object EmpLoyeeKey)
        {
            if (EmpLoyeeKey != null)
            {
                var ObjEmpLoyeeModel = ObjEmployeeBLL.GetByID(EmpLoyeeKey.ToString().ToInt32());
                if (ObjEmpLoyeeModel != null)
                {
                    return ObjDepartmentBLL.GetByID(ObjEmpLoyeeModel.DepartmentID).DepartmentName;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取用户的部门名称
        /// </summary>
        /// <param name="EmpLoyeeKey"></param>
        /// <returns></returns>
        public string GetDepartmentNameByID(object Key)
        {
            if (Key != null)
            {
                var ObjDepartmentModel = ObjDepartmentBLL.GetByID(Key.ToString().ToInt32());
                if (ObjDepartmentModel != null)
                {
                    return ObjDepartmentModel.DepartmentName;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetCustomerStateStr(object source)
        {
            if (source != null)
            {
                var ValueList = Enum.GetValues(typeof(CustomerStates));
                foreach (var ObjItem in ValueList)
                {
                    if ((int)ObjItem == (int)source)
                    {
                        return CustomerState.GetEnumDescription(ObjItem);
                    }
                }

            }
            return "";
        }



        /// <summary>
        /// 获取计划名称前缀
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetMissiontypeName(object source)
        {
            string OutTyper = string.Empty;
            switch (source.ToString().ToInt32())
            {
                case 1:
                    OutTyper = "电话营销";
                    break;
                case 2:
                    OutTyper = "邀约";
                    break;
                case 3:
                    OutTyper = "跟单";
                    break;
                case 4:
                    OutTyper = "报价";
                    break;
                case 5:
                    OutTyper = "制作执行明细";
                    break;
                case 6:
                    OutTyper = "总派工";
                    break;
                case 7:
                    OutTyper = "分工执行";
                    break;
                case 8:
                    OutTyper = "婚礼统筹";
                    break;
                case 60:
                    OutTyper = "临时任务";
                    break;
                case 61:
                    OutTyper = "周计划";
                    break;
                case 62:
                    OutTyper = "月计划";
                    break;
                case 63:
                    OutTyper = "季度计划";
                    break;
                case 64:
                    OutTyper = "年度计划";
                    break;
                case 65:
                    OutTyper = "半年计划";
                    break;
                default:
                    OutTyper = "计划任务";
                    break;
            }
            return OutTyper;
        }

        /// <summary>
        /// 返回销售跟单人
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetAdviserOrderByCustomer(object source)
        {
            HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
            int customerId = (source + string.Empty).ToInt32();
            FL_Invite invites = objInviteBLL.GetByCustomerID(customerId);
            if (invites != null)
            {

                return GetEmployeeName(invites.OrderEmpLoyeeID);

            }
            return string.Empty;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetCustomerStateNameByValue(object value)
        {
            if (value != null)
            {
                var ValueList = Enum.GetValues(typeof(CustomerStates));
                foreach (var ObjItem in ValueList)
                {
                    if ((int)ObjItem == (int)value)
                    {
                        return CustomerState.GetEnumDescription(ObjItem);
                    }
                }

            }
            return "";
        }
        /// <summary>
        /// 通过客户ID返回策划师
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetPlannerIDByCustomerID(object source)
        {
            int CustomerId = (int)source;
            return GetEmployeeName(objOrderBLL.GetbyCustomerID(CustomerId).Planner);
        }
        /// <summary>
        /// 销售跟单人
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetSaleEmployeeByCustomerID(object source)
        {
            int CustomerId = (int)source;
            var objResult = objOrderBLL.GetbyCustomerID(CustomerId);
            if (objResult != null)
            {
                return GetEmployeeName(objResult.EmployeeID);
            }
            return string.Empty;

        }
        //PlanComeDate
        /// <summary>
        /// 返回预定时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetPlanComeDateByCustomerID(object source)
        {
            int CustomerId = (int)source;
            FL_Order orders = objOrderBLL.GetbyCustomerID(CustomerId);
            if (orders != null)
            {
                return GetDateStr(orders.PlanComeDate);
            }
            return "";
        }

        /// <summary>
        /// 员工名
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetEmployeeName(object source)
        {
            int employeeId = (source + string.Empty).ToInt32();
            Employee objEmployeeBLL = new Employee();
            Sys_Employee emp = objEmployeeBLL.GetByID(employeeId);

            if (emp != null)
            {
                return emp.EmployeeName;
            }
            return "";
        }



        /// <summary>
        /// 截取时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDateStr(object ObjDatetime)
        {

            if (ObjDatetime != null)
            {
                return ObjDatetime.ToString().ToDateTime().ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 根据客户ID返回定金
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetEarnestMoneyByCustomerID(object source)
        {
            Order objOrderBLL = new Order();
            FL_Order ordes = objOrderBLL.GetbyCustomerID((int)source);
            if (ordes != null)
            {
                return ordes.EarnestMoney + string.Empty;
            }
            return "";

        }
        /// <summary>
        /// 返回总金额
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetAggregateAmount(object source)
        {
            QuotedPrice objQuotedPriceBLL = new QuotedPrice();
            int CustomerID = (source + string.Empty).ToInt32();
            FL_QuotedPrice quotedPrice = objQuotedPriceBLL.GetByCustomerID(CustomerID);
            if (quotedPrice != null)
            {
                return quotedPrice.FinishAmount + string.Empty;
            }
            return "暂无数据";
        }


        /// <summary>
        ///获取已收款
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetFinishMoney(object CustomerID)
        {
            /// <summary>
            /// 收款计划
            /// </summary>
            int CID = CustomerID.ToString().ToInt32();
            HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new HA.PMS.BLLAssmblly.Flow.QuotedCollectionsPlan();

            var QuotedModel = ObjQuotedPriceBLL.GetByCustomerID(CID);
            if (QuotedModel != null)
            {
                var FinishMoney = ObjQuotedCollectionsPlanBLL.GetByOrderID(QuotedModel.OrderID).Sum(C => C.RealityAmount);
                return (FinishMoney).ToString();
            }
            return "0";
        }



        protected string GetProfitMarginByCustomerID(object source)
        {
            CostSum ObjCostSumBLL = new CostSum();
            decimal CostMoney = ObjCostSumBLL.GetByAll().Sum(C => C.ActualSumTotal).ToString().ToDecimal().ToString("f2").ToDecimal();
            decimal FinishMoney = GetAggregateAmount(source).ToDecimal();
            if (FinishMoney > 0)
            {
                return ((FinishMoney - CostMoney) / FinishMoney).ToString().ToDecimal().ToString("0.00%");
            }
            return string.Empty;
        }

        /// <summary>
        /// 通过员工ID返回策划师名字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetPlannerName(object source)
        {
            //QuotedPrice ObjQuotedPriceBLL = new QuotedPrice();
            //return GetEmployeeName(ObjQuotedPriceBLL.GetByID(source.ToString().ToInt32()).EmpLoyeeID);
            return string.Empty;
        }


        public string GetQuotedEmployee(object CustomerID)
        {
            var ObjModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
            if (ObjModel != null)
            {
                return ObjEmployeeBLL.GetByID(ObjModel.EmpLoyeeID).EmployeeName;
            }
            else
            {
                return "暂无";
            }
        }
        /// <summary>
        /// 根据新人ID查询出该新人的满意度
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetReturnResultByCustomerId(object source)
        {

            return string.Empty;
        }
        /// <summary>
        /// 根据新人ID查询出对应投诉内容
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetComplainByCustomerId(object source)
        {
            int customerId = (int)source;
            Complain objComplainBLL = new Complain();
            CS_Complain complain = objComplainBLL.GetByAll().Where(C => C.CustomerID == customerId).FirstOrDefault();
            if (complain != null)
            {
                return complain.ComplainContent;
            }
            return "";

        }

        /// <summary>
        /// 根据用户ID返回相关联的满意度
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDegreeByCustomerID(object source)
        {
            //int customerId = (int)source;
            //DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
            //CS_DegreeOfSatisfaction entity = objDegreeOfSatisfactionBLL.GetByAll().Where(C => C.CustomerID == customerId).FirstOrDefault();
            //if (entity != null)
            //{
            //    return entity.SumDof.Value + string.Empty;
            //}
            return "";
        }
        /// <summary>
        /// 返回婚礼顾问
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetAdviserNameByEmployeeID(object source)
        {
            Employee objEmployeeBLL = new Employee();
            int employeeId = (source + string.Empty).ToInt32();

            Sys_Employee emp = objEmployeeBLL.GetByID(employeeId);
            if (emp != null)
            {
                return emp.EmployeeName;
            }
            return "";
        }
        /// <summary>
        /// 重新定义母板页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            if (System.Web.HttpContext.Current.Request["NeedPopu"] != null)
            {
                this.MasterPageFile = "~/AdminPanlWorkArea/Master/PopuMaster.Master";
            }
            base.OnPreInit(e);
        }
        /// <summary>
        /// 渠道类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetChannelTypeName(object source)
        {
            ChannelType objChannelTypeBLL = new ChannelType();
            int ChannelTypeId = (source + string.Empty).ToInt32();
            FD_ChannelType channelType = objChannelTypeBLL.GetByID(ChannelTypeId);

            if (channelType != null)
            {
                return channelType.ChannelTypeName;
            }
            return "";
        }


        /// <summary>
        /// 获取短日期格式
        /// </summary>
        /// <param name="ObjDatetime"></param>
        /// <returns></returns>
        public string GetShortDateString(object ObjDatetime)
        {

            if (ObjDatetime != null)
            {
                return ObjDatetime.ToString().ToDateTime().ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }
        ///// <summary>
        ///// 隐藏页面控件
        ///// </summary>
        ///// <param name="PageType"></param>
        //public void ChecksPageControl(string PageType,System.Web.UI.Page ObjPage)
        //{
        //    //var ObjControlList=ObjJurisdictionforButtonBLL.GetByChannel(User.Identity.Name.ToInt32(), PageType);
        //    //int Index = 0;
        //    //string openScript = string.Empty;
        //    //Controls ObjControls = new Controls();
        //    //foreach(var Objitem in ObjControlList)
        //    //{
        //    //    openScript += "$(#\"" +ObjControls.GetByID(Objitem.ControlID).ConrolKey + "\").hide();\n\t ";
        //    //    System.Web.UI.ClientScriptManager OjbClientScript = ObjPage.ClientScript;
        //    //    OjbClientScript.RegisterStartupScript(ObjPage.GetType(), "hideControl" + Index, openScript, true);
        //    //    Index++;
        //    //}

        //    //string openScript = "\n\t ";
        //    //openScript = openScript + "alert('A');\n\t";
        //    //System.Web.UI.ClientScriptManager OjbClientScript = ObjPage.ClientScript;
        //    //OjbClientScript.RegisterStartupScript(ObjPage.GetType(), "AlertAndClosefancybox", openScript, true);
        //}


        /// <summary>
        /// 验证此模块
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        private bool IsHavePower(int ChannelID)
        {
            return true;
        }


        #endregion


        /// <summary>
        /// 锁定今年的服务
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns></returns>
        public string LockMemberByYear(object DateTime, object CustomerID, int Type)
        {

            if (DateTime != null)
            {

                var Year = DateTime.ToString().ToDateTime().Year;
                var Checks = ObjMemberBLL.CheckMemberByYear(CustomerID.ToString().ToInt32(), Year, Type);
                if (!Checks)
                {
                    return " style='display:none;'";
                }
                else
                {
                    return string.Empty;
                }


            }
            else
            {
                return " style='display:none;'";
            }
        }


        /// <summary>
        /// 显示新郎新娘名字
        /// </summary>
        /// <param name="bride"></param>
        /// <param name="groom"></param>
        /// <returns></returns>
        public string ShowCstmName(object bride, object groom, object OldBride)
        {

            if (OldBride.ToString() != string.Empty)
            {
                return string.Format("{0}\\{1}", OldBride, groom).Trim('\\');
            }
            else
            {
                if (bride != null)
                {
                    return bride.ToString();
                }
                else
                {
                    return string.Format("{0}\\{1}", OldBride, groom).Trim('\\');
                }
            }

        }



        /// <summary>
        /// 显示新郎新娘名字
        /// </summary>
        /// <param name="bride"></param>
        /// <param name="groom"></param>
        /// <returns></returns>
        public string ShowCstmName(object bride, object groom)
        {
            if (bride.ToString() != string.Empty || groom.ToString() != string.Empty)
            {
                string names = string.Format("{0}\\{1}", bride, groom).Trim('\\');
                if (names.Length >= 8)
                {
                    return names.Substring(0, 8).ToString();
                }
                else
                {
                    return names.ToString();
                }
            }
            else if (bride.ToString() == string.Empty || groom.ToString() == string.Empty)
            {
                return "无/无";
            }
            else
            {
                return "无/无";
            }

        }

        #region System.DateTime

        /// <summary>
        /// 返回短日期，若比默认最小日期小，则返回空
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public string ShowShortDate(object datetime)
        {
            if (!object.ReferenceEquals(datetime, null))
            {
                DateTime result;
                if (DateTime.TryParse(datetime.ToString(), out result))
                {
                    if (DateTime.Compare(result, MinDateTime) > 0)
                    {
                        return result.ToShortDateString();
                    }
                }
            }
            return string.Empty;
        }

        public string ShowPartyDate(object datetime)
        {
            if (!object.ReferenceEquals(datetime, null))
            {
                DateTime result;
                if (DateTime.TryParse(datetime.ToString(), out result))
                {
                    if (DateTime.Compare(result, DateTime.Parse("2000-01-01")) >= 0)
                    {
                        return result.ToShortDateString();
                    }
                }
            }
            return "婚期未确定";
        }


        /// <summary>
        /// 输入起始值和结束值，返回满足在最小值和最大值范围之间的子集合的起始和结束值的数组。T[0] 为最小值，T[1] 为最大值。
        /// </summary>
        /// <typeparam name="T">要比较数据的值类型。</typeparam>
        /// <param name="start">输入的搜索范围起始值。</param>
        /// <param name="end">输入的搜索范围结束值。</param>
        /// <param name="min">范围的最小值。</param>
        /// <param name="max">范围的最大值。</param>
        /// <returns></returns>
        public T[] CompareTo<T>(T start, T end, T min, T max) where T : struct
        {
            T[] result = new T[2];
            result[0] = typeof(T).GetMethod("CompareTo", new Type[] { typeof(T) }).Invoke(start, new object[] { min }).Equals(1) ? start : min;
            result[1] = typeof(T).GetMethod("CompareTo", new Type[] { typeof(T) }).Invoke(end, new object[] { max }).Equals(1) ? max : end;
            return result;
        }


        /// <summary>
        /// 默认最小日期。
        /// </summary>
        public System.DateTime MinDateTime
        {
            get { return System.DateTime.Parse("1753-01-01 00:00:00"); }
        }

        /// <summary>
        /// 默认最大日期。
        /// </summary>
        public System.DateTime MaxDateTime
        {
            get { return System.DateTime.Parse("9999-12-31 23:59:59"); }
        }

        /// <summary>
        /// 本周一（周一算第一天，周日算最后一天）
        /// </summary>
        public System.DateTime Monday
        {
            get
            {
                return System.DateTime.Today.AddDays(1 - Convert.ToInt32(System.DateTime.Today.DayOfWeek.ToString("d")));
            }
        }

        /// <summary>
        /// 本周日（周一算第一天，周日算最后一天）
        /// </summary>
        public System.DateTime Sunday
        {
            get
            {
                return System.DateTime.Today.AddDays(7 - Convert.ToInt32(System.DateTime.Today.DayOfWeek.ToString("d")));
            }
        }

        /// <summary>
        /// 下周一（周一算第一天，周日算最后一天）
        /// </summary>
        public System.DateTime NextMonday
        {
            get
            {
                return Monday.AddDays(7);
            }
        }

        /// <summary>
        /// 下周日（周一算第一天，周日算最后一天）
        /// </summary>
        public System.DateTime NextSunday
        {
            get
            {
                return Sunday.AddDays(7);
            }
        }

        /// <summary>
        /// 本月初
        /// </summary>
        public System.DateTime MonthStart
        {
            get
            {
                return System.DateTime.Today.AddDays(1 - System.DateTime.Today.Day);
            }
        }

        /// <summary>
        /// 本月末
        /// </summary>
        public System.DateTime MonthEnd
        {
            get
            {
                return System.DateTime.Today.AddDays(1 - System.DateTime.Today.Day).AddMonths(1).AddDays(-1);
            }
        }

        /// <summary>
        /// 下月初
        /// </summary>
        public System.DateTime NextMonthStart
        {
            get
            {
                return System.DateTime.Today.AddDays(1 - System.DateTime.Today.Day).AddMonths(1);
            }
        }

        /// <summary>
        /// 下月末
        /// </summary>
        public System.DateTime NextMonthEnd
        {
            get
            {
                return System.DateTime.Today.AddDays(1 - System.DateTime.Today.Day).AddMonths(2).AddDays(-1);
            }
        }

        /// <summary>
        /// 上个月（一个月前）
        /// </summary>
        public System.DateTime LastMonth
        {
            get
            {
                return System.DateTime.Today.AddMonths(-1);
            }
        }

        /// <summary>
        /// 下个月（一个月后）
        /// </summary>
        public System.DateTime NextMonth
        {
            get
            {
                return System.DateTime.Today.AddMonths(1);
            }
        }

        /// <summary>
        /// 下一周（七天后）
        /// </summary>
        public System.DateTime NextWeek
        {
            get
            {
                return System.DateTime.Today.AddDays(7);
            }
        }

        /// <summary>
        /// 上一周（七天前）
        /// </summary>
        public System.DateTime LastWeek
        {
            get
            {
                return System.DateTime.Today.AddDays(-7);
            }
        }
        #endregion

        /// <summary>
        /// 将 source 剪切为短字符串。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="width">保留的字符长度。</param>
        /// <returns></returns>
        public string ToInLine(object source, int width = 10)
        {
            string tmp = Convert.ToString(source);
            if (tmp.Length > width)
            {
                return tmp.Substring(0, width - 1) + "...";
            }
            return tmp;
        }

        public string ShowHotelName(object hotelName)
        {
            if (Convert.ToString(hotelName) != string.Empty)
            {
                return hotelName.ToString();
            }
            return "未选择";
        }

        /// <summary>
        /// 返回沟通次数
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public string GetFollowCount(object CustomerId)
        {
            int id = Convert.ToInt32(CustomerId);
            FL_Invite invite = ObjEnviteBll.GetByCustomerID(id);
            if (invite != null)
            {
                return invite.FllowCount.ToString();
            }
            else
            {
                return "0";
            }
        }


        public string GetLastFollowdDate(object CustomerId)
        {
            int id = Convert.ToInt32(CustomerId);
            FL_Invite invite = ObjEnviteBll.GetByCustomerID(id);
            if (invite != null)
            {
                return invite.LastFollowDate.ToString().ToDateTime().ToShortDateString();
            }
            else
            {
                return "无";
            }

        }

        /// <summary>
        /// 获取客户状态
        /// </summary>
        /// <returns></returns>
        public string GetCustomerState(object CustomerID)
        {
            int CID = CustomerID.ToString().ToInt32();
            FL_Customers Customer = ObjCustomerBLL.GetByID(CustomerID.ToString().ToInt32());
            if (Customer != null)
            {
                return GetCustomerStateStr(Customer.State);
            }
            else
            {
                return "无";
            }
        }

        /// <summary>
        /// 返回本月完成率
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetCompletionrate(FL_FinishTargetSum FinishiTargetSum, int Month)
        {
            if (FinishiTargetSum.MonthPlan1 == 0 || FinishiTargetSum.MonthPlan2 == 0 || FinishiTargetSum.MonthPlan3 == 0 || FinishiTargetSum.MonthPlan4 == 0 || FinishiTargetSum.MonthPlan2 == 5 || FinishiTargetSum.MonthPlan3 == 6 || FinishiTargetSum.MonthPlan7 == 0 || FinishiTargetSum.MonthPlan2 == 8 || FinishiTargetSum.MonthPlan3 == 9 || FinishiTargetSum.MonthPlan10 == 0 || FinishiTargetSum.MonthPlan2 == 11 || FinishiTargetSum.MonthPlan3 == 12)
            {
                return "0";
            }
            else
            {
                switch (Month)
                {
                    case 1:
                        return ((FinishiTargetSum.MonthFinsh1 / FinishiTargetSum.MonthPlan1).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 2:
                        return ((FinishiTargetSum.MonthFinish2 / FinishiTargetSum.MonthPlan2).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 3:
                        return ((FinishiTargetSum.MonthFinish3 / FinishiTargetSum.MonthPlan3).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 4:
                        return ((FinishiTargetSum.MonthFinish4 / FinishiTargetSum.MonthPlan4).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 5:
                        return ((FinishiTargetSum.MonthFinish5 / FinishiTargetSum.MonthPlan5).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 6:
                        return ((FinishiTargetSum.MonthFinish6 / FinishiTargetSum.MonthPlan6).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 7:
                        return ((FinishiTargetSum.MonthFinish7 / FinishiTargetSum.MonthPlan7).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 8:
                        return ((FinishiTargetSum.MonthFinish8 / FinishiTargetSum.MonthPlan8).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 9:
                        return ((FinishiTargetSum.MonthFinish9 / FinishiTargetSum.MonthPlan9).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 10:
                        return ((FinishiTargetSum.MonthFinish10 / FinishiTargetSum.MonthPlan10).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 11:
                        return ((FinishiTargetSum.MonthFinish11 / FinishiTargetSum.MonthPlan11).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                    case 12:
                        return ((FinishiTargetSum.MonthFinish12 / FinishiTargetSum.MonthPlan12).ToString("0.00").ToInt32() * 100).ToString();
                        break;
                }
            }
            return "";
        }

        #region 获取套餐名称
        /// <summary>
        /// 获取套餐名称
        /// </summary>
        /// <returns></returns>
        public string GetByPackgetID(object source)
        {
            int PackageID = source.ToString().ToInt32();
            FD_CelebrationPackage Cele = ObjCeleBrationBLL.GetByID(PackageID);
            if (Cele != null)
            {
                return Cele.PackageTitle.ToString();
            }
            return "";
        }
        #endregion


        WeddingSceneEvaluationResult ObjEvaluationBLL = new WeddingSceneEvaluationResult();

        #region 获取 总体评价
        public string GetNameByEvaulationId(object source)
        {
            if (source == null)
            {
                source = 6;
            }
            int EvaulationId = source.ToString().ToInt32();
            var ObjEvaluationModel = ObjEvaluationBLL.GetByID(EvaulationId);
            if (ObjEvaluationModel != null)
            {
                string EvaulationName = ObjEvaluationModel.EvaluationName == "--请选择--" ? "未评价" : ObjEvaluationModel.EvaluationName;

                return EvaulationName;
            }

            return "";

        }

        #endregion

        DegreeOfSatisfaction ObjDegreeBLL = new DegreeOfSatisfaction();

        #region 获取 客户满意度
        public string GetSacNameByCustomernId(object source)
        {

            int CustomerID = source.ToString().ToInt32();
            var ObjCustomerModel = ObjDegreeBLL.GetByCustomerID(CustomerID);
            if (ObjCustomerModel != null)
            {
                string EvaulationName = ObjCustomerModel.SumDof.ToString() == "" ? "未评价满意度" : ObjCustomerModel.SumDof.ToString();

                return EvaulationName;
            }

            return "未评价满意度";
        }

        public int GetDofKeyByCustomernId(object source)
        {
            int CustomerID = source.ToString().ToInt32();
            var ObjCustomerModel = ObjDegreeBLL.GetByCustomerID(CustomerID);
            if (ObjCustomerModel != null)
            {
                return ObjCustomerModel.DofKey;
            }
            return 0;

        }
        #endregion

        public string GetOrderDate(object source)
        {
            int CustomerId = source.ToString().ToInt32();
            var ObjCustomerModel = objReportBLL.GetByCustomerID(CustomerId, User.Identity.Name.ToInt32());
            if (ObjCustomerModel != null)
            {
                return ObjCustomerModel.OrderSucessDate.ToString();
            }
            return "";
        }


        public int GetQuotedEmployeesID(object CustomerID)
        {

            var ObjModel = ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
            //var ObjModel = ObjCustomerBLL.GetByCustomerID(CustomerID.ToString().ToInt32());
            if (ObjModel != null)
            {
                return ObjModel.EmpLoyeeID.ToString().ToInt32();
            }
            return 0;

        }

        public string GetCustomerName(object source)
        {
            if (source != null)
            {
                int CustomerId = source.ToString().ToInt32();
                var ObjCustomerModel = ObjCustomerBLL.GetByID(CustomerId);
                if (ObjCustomerModel != null)
                {
                    string bride = ObjCustomerModel.Bride == "" ? "无" : ObjCustomerModel.Bride;
                    string groom = ObjCustomerModel.Groom == "" ? "无" : ObjCustomerModel.Groom;
                    return bride + "/" + groom;
                }
            }
            return "无/无";
        }

        public string GetPartyDate(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjCustomerBLL.GetByID(CustomerID);
            if (Model != null)
            {
                return Model.PartyDate.ToString().ToDateTime().ToShortDateString();
            }
            return "";
        }

        //获取是否已经内部评价   0 null 都是未评价  1 是已评价
        public int GetEvalState(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjCustomerBLL.GetByID(CustomerID);
            if (Model != null)
            {
                return Model.EvalState.ToString().ToInt32();
            }
            return 0;           //默认为0
        }

        #region 获取邀约类型名称
        /// <summary>
        /// 获取邀约类型名称    2018-04-23
        /// </summary>
        public string GetApplyName(object applyType)
        {
            string name = "无";
            if (applyType != null)
            {
                if (!string.IsNullOrEmpty(applyType.ToString()))
                {
                    int Type = applyType.ToString().ToInt32();
                    BaseService<FD_ApplyType> ObjApplyTypeBLL = new BaseService<FD_ApplyType>();
                    var m_applyType = ObjApplyTypeBLL.GetById(Type);
                    if (m_applyType != null)
                    {
                        name = m_applyType.ApplyName;
                    }
                }
            }
            return name;
        }
        #endregion

    }
}
