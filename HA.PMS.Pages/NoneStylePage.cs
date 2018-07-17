using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.CS;

namespace HA.PMS.Pages
{
    public class NoneStylePage: System.Web.UI.Page
    {
        UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();


        /// <summary>
        /// 用户控件权限
        /// </summary>
        JurisdictionforButton ObjJurisdictionforButtonBLL = new JurisdictionforButton();


        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();
        Department ObjDepartmentBLL = new Department();
        /// <summary>
        /// 构造函数  验证用户是否对此模块有访问权限
        /// </summary>
        public NoneStylePage()
        {

            //int EmpLoyeeID = User.Identity.Name.ToInt32();
         
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


        /// <summary>
        /// 邀约人
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetInviteName(object source)
        {
            if (source != null)
            {

                HA.PMS.BLLAssmblly.Flow.Invite objInviteBLL = new BLLAssmblly.Flow.Invite();
                FL_Invite ints = objInviteBLL.GetByAll()
                    .Where(C => C.CustomerID == (int)source).FirstOrDefault();
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
                return quotedPrice.AggregateAmount + string.Empty;
            }
            return "";
        }

        protected string GetProfitMarginByCustomerID(object source)
        {
            //// GetCostAll()
            //Customers objCustomersBLL = new Customers();
            //CustomerCost cost = objCustomersBLL.GetCostAll().Where(C => C.CustomerID == (int)source).FirstOrDefault();
            //if (cost != null)
            //{
            //    return cost.ProfitMargin.Value + string.Empty;
            //}
            return string.Empty;
        }

        /// <summary>
        /// 返回策划师名字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetPlannerName(object source)
        {
            QuotedPrice ObjQuotedPriceBLL = new QuotedPrice();
            return GetEmployeeName(ObjQuotedPriceBLL.GetByID(source.ToString().ToInt32()).EmpLoyeeID);
            //Order objOrderBLL = new Order();
            //int Planner = (source + string.Empty).ToInt32();
            //FL_QuotedPriceEmployee plannerObj = objOrderBLL.GetByOrderId(Planner);
            //if (plannerObj != null)
            //{
            //    return plannerObj.EmployeeName;
            //}
            //else
            //{
            //    return "";
            //}


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


        /// <summary>
        /// 获取日期格式
        /// </summary>
        /// <param name="ObjDatetime"></param>
        /// <returns></returns>
        public string GetLongDateString(object ObjDatetime)
        {

            if (ObjDatetime != null)
            {
                return ObjDatetime.ToString().ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
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
    }
}
