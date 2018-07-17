/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.1
 Description:客户流失界面
 History:修改日志

 Author:杨洋
 Date:2013.4.1
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer
{
    /// <summary>
    /// 排除流失原因 重复
    /// </summary>
    public class FL_CustomersReasonsComparer : IEqualityComparer<FL_Customers>
    {

        public bool Equals(FL_Customers x, FL_Customers y)
        {


            if (Object.ReferenceEquals(x, y)) return true;


            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;


            return x.Reasons == y.Reasons;
        }



        public int GetHashCode(FL_Customers c)
        {

            if (Object.ReferenceEquals(c, null)) return 0;


            int hashReasons = c.Reasons == null ? 0 : c.Reasons.GetHashCode();


            //  int hashProductCode = c.Channel.GetHashCode();


            return hashReasons;
        }

    }

    /// <summary> 
    /// 排除 渠道类型 重复
    /// </summary>
    public class FL_CustomersChannelComparer : IEqualityComparer<FL_Customers>
    {

        public bool Equals(FL_Customers x, FL_Customers y)
        {


            if (Object.ReferenceEquals(x, y)) return true;


            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;


            return x.Channel == y.Channel;
        }



        public int GetHashCode(FL_Customers c)
        {

            if (Object.ReferenceEquals(c, null)) return 0;


            int hashChannel = c.Channel == null ? 0 : c.Channel.GetHashCode();


            //  int hashProductCode = c.Channel.GetHashCode();


            return hashChannel;
        }

    }
    public partial class FL_TelemarkingRunOff : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }
        protected string GetDateStr(object source)
        {
            return Convert.ToDateTime(source).ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void DataBinder()
        {
            //FL_CustomersOrder fL_CustomersOrder = new FL_CustomersOrder();
            //fL_CustomersOrder.Channel = ddlChannel.Text;
            //fL_CustomersOrder.Reasons = ddlReasons.Text;
            //fL_CustomersOrder.IsLose = true;

            //List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ////查询已经流失的
            //ObjParameterList.Add(new ObjectParameter("IsLose", fL_CustomersOrder.IsLose));
            //if (ddlReasons.SelectedItem.Text != "请选择")
            //{
            //    ObjParameterList.Add(new ObjectParameter("Reasons", fL_CustomersOrder.Reasons));
            //}

            //if (ddlChannel.SelectedItem.Text != "请选择")
            //{
            //    ObjParameterList.Add(new ObjectParameter("Channel", fL_CustomersOrder.Channel));
            //}

            ////开始时间
            //DateTime startTime = new DateTime();
            ////如果没有选择结束时间就默认是当前时间

            //DateTime endTime = DateTime.Now;
            
            //if (!string.IsNullOrEmpty(txtStar.Text))
            //{
            //    startTime = txtStar.Text.ToDateTime();
            //}
            //if (!string.IsNullOrEmpty(txtEnd.Text))
            //{
            //    endTime = txtEnd.Text.ToDateTime();
            //}
            //ObjParameterList.Add(new ObjectParameter("RecorderDate_between", startTime + "," + endTime));

            //int sourceCount = 0;
            //#region 分页页码
            //int startIndex = CustomerPager.StartRecordIndex;
            //int resourceCount = 0;


            //var query = objCustomersBLL.GetbyFL_CustomersOrderParameter(ObjParameterList.ToArray(),
            //CustomerPager.PageSize, CustomerPager.CurrentPageIndex, out sourceCount);
            //CustomerPager.RecordCount = resourceCount;
            //rptCustomers.DataSource = query;
            //rptCustomers.DataBind();
           

        }
        /// <summary>
        /// 下拉框
        /// </summary>
        protected void DataDropDownList()
        {
            var query = objCustomersBLL.GetByAll();

            ddlReasons.DataSource = query.Where(C=>!string.IsNullOrEmpty(C.Reasons))
                .Distinct(new FL_CustomersReasonsComparer()).Select(C => C.Reasons);
            ddlReasons.DataBind();


            ddlReasons.Items.Add(new ListItem("请选择", "0"));
            ddlReasons.SelectedIndex = ddlReasons.Items.Count - 1;

            ddlChannel.DataSource = query.Where(C => !string.IsNullOrEmpty(C.Channel))
                .Distinct(new FL_CustomersChannelComparer()).Select(C => C.Channel);
            ddlChannel.DataBind();

            ddlChannel.Items.Add(new ListItem("请选择", "0"));
            ddlChannel.SelectedIndex = ddlChannel.Items.Count - 1;

        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void CustomerPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}