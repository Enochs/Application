using System;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Collections.Generic;
using System.Text;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class FD_HotelUpdate : HA.PMS.Pages.SystemPage
    {
        Hotel objHotelBLL = new Hotel();
        HotelLabel objHotelLabelBLL = new HotelLabel();
        HotelLabelLog objHotelLabelLogBLL = new HotelLabelLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int HotelId = Request.QueryString["HotelId"].ToInt32();
                HA.PMS.DataAssmblly.FD_Hotel fD_Hotel = objHotelBLL.GetByID(HotelId);
                txtHotelName.Text = fD_Hotel.HotelName;
                txtArea.Text = fD_Hotel.Area;
                txtDeskCount.Text = fD_Hotel.DeskCount + string.Empty;
                txtDetails.Text = fD_Hotel.Details;
                txtPriceEnd.Text = fD_Hotel.PriceEnd + string.Empty;
                txtPriceStar.Text = fD_Hotel.PriceStar + string.Empty;
                txtAddress.Text = fD_Hotel.Address;
                txtTel.Text = fD_Hotel.Tel;
                txtAddress1.Text = fD_Hotel.Address1;
                ddlStarLevel.SelectedItem.Text = fD_Hotel.HotelType == null ? "五星级酒店" : fD_Hotel.HotelType.ToString();
                //ddlStarLevel.SelectedItem.Value = fD_Hotel.StarLevel.ToString();
                txtScore.Text = fD_Hotel.EvalScore.ToString();
                txtOther.Text = fD_Hotel.Other;
                txtMoney1.Text = fD_Hotel.Money1;
                txtMoney2.Text = fD_Hotel.Money2;
                txtSort.Text = fD_Hotel.Sort.ToString();
                rdoOnSale.SelectedValue = fD_Hotel.OnSale.ToString();
                rdoRecommand.SelectedValue = fD_Hotel.ReCommand.ToString();
                ListItem selectitem = ddlStarLevel.Items.FindByValue(Convert.ToString(fD_Hotel.StarLevel));
                if (selectitem != null)
                {
                    ddlStarLevel.ClearSelection();
                    selectitem.Selected = true;
                }
                rptLabelList.DataSource = objHotelLabelBLL.GetByAll();
                rptLabelList.DataBind();

                BinderAreaTags();
            }
        }

        protected void BinderAreaTags()
        {
            List<string> arealist = objHotelBLL.GetAreaList();
            StringBuilder areatags = new StringBuilder();
            areatags.Append("[");
            foreach (var C in arealist)
            {
                areatags.AppendFormat("\"{0}\",", C);
            }
            string result = string.Concat(areatags.ToString().Trim(','), "]");
            ViewState["areatags"] = result;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int HotelId = Request.QueryString["HotelId"].ToInt32();
            HA.PMS.DataAssmblly.FD_Hotel fD_Hotel = objHotelBLL.GetByID(HotelId);
            if (objHotelBLL.GetByName(txtHotelName.Text.ToString()) == null || objHotelBLL.GetByName(txtHotelName.Text.ToString()).HotelName == fD_Hotel.HotelName)    //更改了名称 判断酒店不存在可以修改 或者 酒店名称没变 也可修改
            {
                fD_Hotel.HotelName = txtHotelName.Text;
                fD_Hotel.Area = txtArea.Text;
                fD_Hotel.IsDelete = false;
                fD_Hotel.Address = txtAddress.Text;
                fD_Hotel.DeskCount = txtDeskCount.Text.ToInt32();
                fD_Hotel.Tel = txtTel.Text.Trim();
                fD_Hotel.PriceStar = txtPriceStar.Text.ToDecimal();
                fD_Hotel.PriceEnd = txtPriceEnd.Text.ToDecimal();
                fD_Hotel.StarLevel = ddlStarLevel.SelectedValue.ToInt32();
                fD_Hotel.HotelType = ddlStarLevel.SelectedItem.Text.ToString();
                fD_Hotel.EvalScore = txtScore.Text.ToString().ToDouble();
                fD_Hotel.Details = txtDetails.Text.Trim();
                fD_Hotel.Other = txtOther.Text;
                fD_Hotel.Money1 = txtMoney1.Text;
                fD_Hotel.Money2 = txtMoney2.Text;
                fD_Hotel.Sort = txtSort.Text.ToInt32();
                fD_Hotel.ReCommand = rdoRecommand.SelectedValue.ToInt32();
                fD_Hotel.OnSale = rdoOnSale.SelectedValue.ToInt32();
                fD_Hotel.LabelContent = "";
                int result = objHotelBLL.Update(fD_Hotel);

                //先删除所有标签，再添加标签，已减少循环
                objHotelLabelLogBLL.DeleteByHotelID(HotelId);
                int index = 0;
                for (int i = 0; i < rptLabelList.Items.Count; i++)
                {
                    CheckBox chSinger = rptLabelList.Items[i].FindControl("chSinger") as CheckBox;
                    HiddenField hfLabelValue = rptLabelList.Items[i].FindControl("hfLabelValue") as HiddenField;
                    if (chSinger.Checked)
                    {
                        int LableID = hfLabelValue.Value.ToInt32();

                        objHotelLabelLogBLL.Insert(new FD_HotelLabelLog()
                        {
                            HotelID = HotelId,
                            LableID = LableID
                        });
                        fD_Hotel.LabelContent += objHotelLabelBLL.GetByID(LableID).HotelLabelName + ",";
                        objHotelBLL.Update(fD_Hotel);
                    }
                }

                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

                }
            }
            else
            {
                JavaScriptTools.AlertWindow("酒店已存在,请更换酒店名称", this.Page);
            }


        }

        protected void rptLabelList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FD_HotelLabel ObjHotelLabelModel = (FD_HotelLabel)e.Item.DataItem;

            int HotelID = Request.QueryString["HotelId"].ToInt32();
            var query = objHotelLabelLogBLL.GetByAll().Where(C => C.LableID == ObjHotelLabelModel.HotelLabelID && HotelID == C.HotelID);
            if (query != null && query.Count() > 0)
            {
                ((CheckBox)e.Item.FindControl("chSinger")).Checked = true;
            }
            else
            {
                ((CheckBox)e.Item.FindControl("chSinger")).Checked = false;
            }
        }
    }
}