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
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.CA;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Master
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        Customers objCustomerBLL = new Customers();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        Notice ObjNoticeBLL = new Notice();

        // public string url { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            hideFormKey.Value = form1.ClientID;
            if (!IsPostBack)
            {
                if (Request.Cookies["HAEmployeeID"] != null)
                {
                    hideSubmitKey.Value = Request.Cookies["HAEmployeeID"].Value;
                    txtCurrentLocation.Text = new Employee().GetCurrentLocation(Convert.ToInt32(hideSubmitKey.Value));
                }

                DataBinder();
                //搜素框提供的具体只想URL参数
                //ViewState["url"] = hfUrl.Value;
            }
        }
        #region 公司销售目标和公告相关的数据查询
        /// <summary>
        /// 公司销售目标 公司公告的数据绑定
        /// </summary>
        protected void DataBinder()
        {
            //int employeeId = 0;
            ////访问cookie
            //if (Request.Cookies["userName"] != null)
            //{   //发送留言者
            //    employeeId = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            //}
            //#region 销售目标
            //DateTime dt = DateTime.Now;
            //DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);   //本 季度初 
            //DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);   //本 季度末 
            //startQuarter = new DateTime(startQuarter.Year, startQuarter.Month, startQuarter.Day);
            //endQuarter = new DateTime(endQuarter.Year, endQuarter.Month, endQuarter.Day, 23, 59, 59);
            //var currentTarget = objMyGoalTargetBLL.GetByAll().Where(C => C.CreateEmployeeId == employeeId);
            //var currentMonth = currentTarget.Where(C => C.CreateTime.Value.Month == dt.Month
            //    &&C.CreateTime.Value.Year==dt.Year).FirstOrDefault();
            ////当前月目标
            //if (currentMonth!=null)
            //{
            //    ltlTargetMonth.Text =Convert.ToInt32( currentMonth.TargetValue.Value)+string.Empty;
            //}
            //else
            //{
            //    ltlTargetMonth.Text = "0";
            //}
            //var currentQuarter = currentTarget.Where(C => C.CreateTime.Value >= startQuarter
            //    && C.CreateTime.Value <= endQuarter);
            ////当前季度目标
            //if (currentQuarter.Count()>0)
            //{
            //    ltlTargetQuarter.Text =Convert.ToInt32(  currentQuarter.Sum(C => C.TargetValue.Value) )+ string.Empty;
            //}
            //else
            //{
            //    ltlTargetQuarter.Text = "0";
            //}
            //var currentYear = currentTarget.Where(C => C.CreateTime.Value.Year == dt.Year);
            ////当前年目标
            //if (currentYear.Count()>0)
            //{
            //    ltlTargetYear.Text=Convert.ToInt32( currentYear.Sum(C => C.TargetValue.Value)) + string.Empty;
            //}
            //else
            //{
            //    ltlTargetYear.Text = "0";
            //}


        #endregion
            #region 公司制度
            rptNotice.DataSource = ObjNoticeBLL.Getbytop(3);
            rptNotice.DataBind();
            #endregion
        }
        /// <summary>
        /// 截取部分较长的内容
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetSubStr(object source)
        {
            string str = source + string.Empty;
            string newStr = "";
            if (str.Length > 6)
            {
                newStr = "<span title='" + str + "'>" + str.Substring(0, 5) + "....</span>";
            }
            else
            {

                newStr = str;
            }
            return newStr;
        }
        /// <summary>
        /// 格式化字符串为时间格式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetTimeStrFormat(object source)
        {
            return (source + string.Empty).ToDateTime().ToString("yyyy年MM月dd日");
        }
        protected void rptNotice_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //第一项时默认为第一跳公告为空
                if (e.Item.ItemIndex == 0)
                {
                    Literal ltlNewState = e.Item.FindControl("ltlNewState") as Literal;
                    ltlNewState.Text = "<span class='date badge badge-important'>新</span>";
                }
            }
        }


        /// <summary>
        /// 根据客户姓名查询之后，进行相应的跳转页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbtnQuery_Click(object sender, EventArgs e)
        {
            FL_Customers fl_Customers = new FL_Customers();

            //fl_Customers.Groom = txtCustomer.Text;
            //fl_Customers.Bride = txtCustomer.Text;
            //或者男，或者女
            fl_Customers.Groom = hfTxtValue.Value;
            fl_Customers.Bride = hfTxtValue.Value;
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            ObjParameterList.Add(new ObjectParameter("Groom_OR_Bride", fl_Customers.Groom + "," + fl_Customers.Bride));
            var query = objCustomerBLL.GetbyParameter(ObjParameterList.ToArray());
            int resultCount = query.Count;
            //搜素之后对应js代码 通过返回的count个数然后再对应的格式化填充对应的url地址信息
            string jsCodeStr = "$('#popuSerach').attr('href','{0}');$('#popuSerach').fancybox().trigger('click')";

            //如果有记录则进行正在邀约
            if (resultCount > 0)
            {
                jsCodeStr = string.Format(jsCodeStr, "/AdminPanlWorkArea/Invite/Customer/OngoingInvite.aspx?ChannelID=16&customerId="
                + query.FirstOrDefault().CustomerID);
            }
            else
            {
                //如果没有记录，则进入录入信息 此时新人状态就是邀约成功            
                jsCodeStr = string.Format(jsCodeStr, "/AdminPanlWorkArea/Foundation/FDTelemarketing/FD_TelemarketingCreate.aspx?InviteSucess="
                    + (int)CustomerStates.InviteSucess);
            }
            //注册对应js脚本，然后进行弹出框
            JavaScriptTools.RegisterJsCodeSource(jsCodeStr, this.Page);
        }

        protected void logoffsystem_ServerClick(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            HttpCookie cok = Request.Cookies["LoginCookie"];
            if (cok != null)
            {
                cok.Expires = DateTime.Now.AddDays(-1);
                Response.AppendCookie(cok);
            }
            Response.Write("<script>alert('退出系统成功');window.parent.document.location ='/Account/LoginOut.aspx';</script>");
        }

        protected void btnSaveLocation_Click(object sender, EventArgs e)
        {
            int flag = new Employee().SetCurrentLocation(Convert.ToInt32(hideSubmitKey.Value), txtCurrentLocation.Text);
            if (flag > 0)
            {
                txtCurrentLocation.BorderColor = System.Drawing.Color.Green;
            }
            else if (flag == 0)
            {
                txtCurrentLocation.BorderColor = System.Drawing.Color.Yellow;
            }
            else
            {
                txtCurrentLocation.BorderColor = System.Drawing.Color.Red;
            }
        }
    }
}