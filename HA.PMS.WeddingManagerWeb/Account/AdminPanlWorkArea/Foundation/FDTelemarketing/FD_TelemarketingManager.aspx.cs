/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.14
 Description:客户基本信息管理页面
 History:修改日志
 （客户电话营销）
 Author:杨洋
 date:2013.3.14
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
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;
using System.Collections;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/bb338049.aspx
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
    public class FL_CustomersCustomerStatusComparer : IEqualityComparer<FL_Customers>
    {

        public bool Equals(FL_Customers x, FL_Customers y)
        {


            if (Object.ReferenceEquals(x, y)) return true;


            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;


            return x.CustomerStatus == y.CustomerStatus;
        }



        public int GetHashCode(FL_Customers c)
        {

            if (Object.ReferenceEquals(c, null)) return 0;


            int hashCustomerStatus = c.CustomerStatus == null ? 0 : c.CustomerStatus.GetHashCode();


            //  int hashProductCode = c.Channel.GetHashCode();


            return hashCustomerStatus;
        }

    }


    public partial class FD_TelemarketingManager : SystemPage
    {
        /// <summary>
        /// 电话营销
        /// </summary>
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        Customers objCustomersBLL = new Customers();
        ChannelType objChannelTypeBLL = new ChannelType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load加载时绑定数据源
                DataDropDownList();
                DataBinder();
               
            }
        }
 
        protected void DataDropDownList() 
        {
            var query = objCustomersBLL.GetByAll();
            
            //渠道名称绑定
            ddlChanneName.DataSource = query.Where(C=>!string.IsNullOrEmpty(C.Channel))
                .Distinct(new FL_CustomersChannelComparer()).Select(C=>C.Channel);
            //ddlChanneName.DataTextField = "Channel";
            ddlChanneName.DataBind();
            ddlChanneName.Items.Add(new ListItem("请选择", "0"));
            ddlChanneName.SelectedIndex = ddlChanneName.Items.Count - 1;
            //渠道类型绑定
            ddlChanneType.DataSource = objChannelTypeBLL.GetByAll();
            ddlChanneType.DataTextField = "ChannelTypeName";
            ddlChanneType.DataValueField = "ChannelTypeId";
            ddlChanneType.DataBind();
            ddlChanneType.Items.Add(new ListItem("请选择", "0"));
            ddlChanneType.SelectedIndex = ddlChanneType.Items.Count - 1;
            //推荐人绑定
            ddlReferee.DataSource = query;
            ddlReferee.DataTextField = "Referee";
            ddlReferee.DataBind();
            ddlReferee.Items.Add(new ListItem("请选择", "0"));
            ddlReferee.SelectedIndex = ddlReferee.Items.Count - 1;
            //新人状态ddlCustomerStatus
         
        }
        /// <summary>
        /// 绑定数据到Repeater控件中
        /// </summary>
        private void DataBinder()
        {
            #region 相关的查询
            FL_Customers fl_Customers = new FL_Customers();

            fl_Customers.Channel = ddlChanneName.SelectedItem.Text;
            fl_Customers.ChannelType = ddlChanneType.SelectedValue.ToInt32();
            fl_Customers.Referee = ddlReferee.SelectedItem.Text;
            fl_Customers.State = ddlCustomerStatus.SelectedValue.ToInt32();
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            if (ddlChanneType.SelectedValue.ToInt32()!=0)
            {
                ObjParameterList.Add(new ObjectParameter("ChannelType", fl_Customers.ChannelType));
            }
            
            if (ddlChanneName.SelectedItem.Text!="请选择")
            {
                  ObjParameterList.Add(new ObjectParameter("Channel", fl_Customers.Channel));
            }
            if (ddlReferee.SelectedItem.Text != "请选择")
            {
                ObjParameterList.Add(new ObjectParameter("Referee", fl_Customers.Referee));
            }
            if (ddlCustomerStatus.SelectedItem.Text != "请选择")
            {
                ObjParameterList.Add(new ObjectParameter("State", fl_Customers.State));
            }

            ObjParameterList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            ObjParameterList.Add(new ObjectParameter("SerchKeypar", "CreateEmpLoyee"));
            //开始时间
            DateTime startTime=new DateTime ();
            //如果没有选择结束时间就默认是当前时间
            DateTime endTime=DateTime.Now;
            if (!string.IsNullOrEmpty(txtRecorderDateStar.Text))
            {
                startTime = txtRecorderDateStar.Text.ToDateTime();
            }
            if (!string.IsNullOrEmpty(txtRecorderDateEnd.Text))
            {
                 endTime = txtRecorderDateEnd.Text.ToDateTime();
            }
            ObjParameterList.Add(new ObjectParameter("RecorderDate_between", startTime + "," + endTime));
            #endregion

            #region 分页页码
            int startIndex = TelemarketingPager.StartRecordIndex;
            int resourceCount = 0;
            var query = ObjTelemarketingBLL.GetTelmarketingCustomersByParameter(TelemarketingPager.PageSize, TelemarketingPager.CurrentPageIndex, out resourceCount, ObjParameterList.ToArray());
            TelemarketingPager.RecordCount = resourceCount;

            rptTelemarketingManager.DataSource = query;
            rptTelemarketingManager.DataBind();
            
            #endregion


        }

 
        /// <summary>
        /// 删除ItemCommand事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptTelemarketingManager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int CustomerID = e.CommandArgument.ToString().ToInt32();
                //创建客户基本类
                FL_Customers fL_Customers = new FL_Customers()
                {
                    CustomerID = CustomerID
                };
                objCustomersBLL.Delete(fL_Customers);
                //删除之后重新绑定数据源
                DataBinder();
            }

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

            DataBinder();
        }
 
        protected void TelemarketingPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
    }
}