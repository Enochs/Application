/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:客户满意度管理页面
 History:修改日志

 Author:杨洋
 date:2013.3.25
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    /// <summary>
    /// 比较满意度重复
    /// </summary>
    public class CS_DegreeOfSatisfactionComparer : IEqualityComparer<CS_DegreeOfSatisfaction>
    {

        public bool Equals(CS_DegreeOfSatisfaction x, CS_DegreeOfSatisfaction y)
        {


            if (Object.ReferenceEquals(x, y)) return true;


            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;


            return x.SumDof == y.SumDof;
        }



        public int GetHashCode(CS_DegreeOfSatisfaction c)
        {

            if (Object.ReferenceEquals(c, null)) return 0;


            int hashCustomerStatus = c.SumDof == null ? 0 : c.SumDof.GetHashCode();


            //  int hashProductCode = c.Channel.GetHashCode();


            return hashCustomerStatus;
        }

    }

    public partial class CS_DegreeOfSatisfactionManager : SystemPage
    {
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }

        /// <summary>
        /// 对应的下拉框绑定
        /// </summary>
        protected void DataDropDownList()
        {
            ListItem firstChoose = new ListItem("请选择", "0");
            var query = objDegreeOfSatisfactionBLL.GetByAll();
            int count = query.Distinct(new CS_DegreeOfSatisfactionComparer()).Select(C => C.SumDof).Count();
            ddlSumDof.DataSource = query.Distinct(new CS_DegreeOfSatisfactionComparer()).Select(C => C.SumDof);
            ddlSumDof.DataBind();
            ddlSumDof.Items.Add(firstChoose);
            ddlSumDof.SelectedIndex = ddlSumDof.Items.Count - 1;

        }
        protected void DataBinder()
        {
            //CS_DegreeOfSatisfactionCustomers cS_DegreeOfSatisfactionCustomers =
            //    new CS_DegreeOfSatisfactionCustomers();

            //cS_DegreeOfSatisfactionCustomers.SumDof = ddlSumDof.SelectedItem.Text.ToInt32();
            //List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ////满意度
            //if (ddlSumDof.SelectedValue.ToInt32() != 0)
            //{
            //    ObjParameterList.Add(new ObjectParameter("SumDof", ddlSumDof.SelectedItem.Text.ToInt32()));
            //}

            ////开始时间
            //DateTime startTime = new DateTime();


            //DateTime endTime = DateTime.Now.AddYears(1);
            //if (!string.IsNullOrEmpty(txtPartyDateStar.Text))
            //{
            //    startTime = txtPartyDateStar.Text.ToDateTime();
            //}
            //if (!string.IsNullOrEmpty(txtPartyDateEnd.Text))
            //{
            //    endTime = txtPartyDateEnd.Text.ToDateTime();
            //}
            //ObjParameterList.Add(new ObjectParameter("PartyDate_between", startTime + "," + endTime));

            //#region 分页页码
            //int startIndex = DegreePager.StartRecordIndex;
            //int resourceCount = 0;
            //var query = objDegreeOfSatisfactionBLL.GetbyParameter(ObjParameterList.ToArray(), DegreePager.PageSize, DegreePager.CurrentPageIndex, out resourceCount);
            //DegreePager.RecordCount = resourceCount;

            //rptDegree.DataSource = query;
            //rptDegree.DataBind();


            //#endregion



        }

        protected void rptDegree_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int DofKey = e.CommandArgument.ToString().ToInt32();

                CS_DegreeOfSatisfaction cs_Degree = new CS_DegreeOfSatisfaction();
                cs_Degree.DofKey = DofKey;
                objDegreeOfSatisfactionBLL.Delete(cs_Degree);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }
        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        /// <summary>
        /// 分页页码时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DegreePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
       
        protected void rptDegree_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    string ahtmlStr = "<a href='#' class='btn btn-primary  btn-mini'  onclick=\"{0}\">评价</a>";
            //    CS_DegreeOfSatisfactionCustomers currentObj = e.Item.DataItem as CS_DegreeOfSatisfactionCustomers;
            //    Literal ltlEdit = e.Item.FindControl("ltlEdit") as Literal;
            //    ltlEdit.Text = string.Format(ahtmlStr, "ShowUpdateWindows(" + currentObj.DofKey + ",this);");

            //    //婚期小于当前不能进行评价
            //    if (currentObj.PartyDate > DateTime.Now)
            //    {
            //        ltlEdit.Text = string.Format(ahtmlStr, "alert('对不起，暂时你还不能对该条信息进行评价，因为当前新人婚期还未进行');");
            //    }
            //    //评价过了，也不能重新进行评价
            //    if (currentObj.SumDof > 0)
            //    {
            //        ltlEdit.Text = string.Format(ahtmlStr, "alert('对不起，该条信息已经被评论过了');");
            //    }
            //}
        }

    }
}