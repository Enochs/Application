using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.CA;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class CA_CompanySaleRateConfig : SystemPage
    {

        TargetTypeRateValue objTargetTypeRateValueBLL = new TargetTypeRateValue();
        TargetType objTargetTypeBLL = new TargetType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }


        protected void DataBinder()
        {
            rptRate.DataSource = objTargetTypeBLL.GetByAll().Where(C => C.TargetType.Value == 1);

            rptRate.DataBind();

        }

        protected void rptRate_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int TargetTypeId = e.CommandArgument.ToString().ToInt32();
            TextBox txtRateValue = e.Item.FindControl("txtRateValue") as TextBox;
            int userId = User.Identity.Name.ToInt32();
            int result = 0;
            decimal rateValue = txtRateValue.Text.ToDecimal();
            if (e.CommandName == "Oper")
            {

                if (rateValue == 0)
                {
                    JavaScriptTools.AlertWindow("请你输入有效的质量率", this.Page);
                }
                else
                {


                    var query = objTargetTypeRateValueBLL.GetByAll().OrderByDescending(C=>C.CreateTime)
                        .FirstOrDefault(C => C.TargetTypeId == TargetTypeId);
                    if (query != null)
                    {
                        //修改
                        query.RateValue = rateValue;
                        
                        query.CreateEmployeeId = userId;
                        query.TargetTypeId = TargetTypeId;
                        //如果此时是当前月份有的话，就覆盖当前月份的，此时为修改操作
                        if ( query.CreateTime.Value.Month==DateTime.Now.Month)
                        {
                            result = objTargetTypeRateValueBLL.Update(query);
                        }
                        else
                        {
                            query.CreateTime = DateTime.Now;
                            //如果不等于当前月的话，就是添加操作
                            result = objTargetTypeRateValueBLL.Insert(query);
                        }
                        
                      

                    }
                    else
                    {
                        //新增
                        query = new CA_TargetTypeRateValue();
                        query.RateValue = rateValue;
                        query.CreateTime = DateTime.Now;
                        query.TargetTypeId = TargetTypeId;
                        query.CreateEmployeeId = userId;
                        result = objTargetTypeRateValueBLL.Insert(query);
                    }
                    if (result > 0)
                    {
                        JavaScriptTools.AlertWindow("保存成功!", this.Page);
                        DataBinder();
                    }
                }
            }
        }

        protected void rptRate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CA_TargetType current = e.Item.DataItem as CA_TargetType;
            if (current != null)
            {
                int TargetTypeId = current.TargetTypeId;
                var query = objTargetTypeRateValueBLL.GetByAll().OrderByDescending(C => C.CreateTime)
                    .FirstOrDefault(C => C.TargetTypeId == TargetTypeId);
                if (query != null)
                {
                    TextBox txtRateValue = e.Item.FindControl("txtRateValue") as TextBox;
                    txtRateValue.Text = query.RateValue + string.Empty;
                }
            }
        }

    }
}