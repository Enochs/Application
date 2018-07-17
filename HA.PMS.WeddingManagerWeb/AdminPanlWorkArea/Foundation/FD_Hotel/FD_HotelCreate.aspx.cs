using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Text;
using System.Linq;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class FD_HotelCreate : HA.PMS.Pages.SystemPage
    {
        Hotel objHotelBLL = new Hotel();
        HotelLabel objHotelLabelBLL = new HotelLabel();
        HotelLabelLog objHotelLabelLogBLL = new HotelLabelLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            if (objHotelBLL.GetByName(txtHotelName.Text.ToString()) == null)    //判断酒店不存在  就新增
            {
                //创建酒店实体类
                HA.PMS.DataAssmblly.FD_Hotel fD_Hotel = new HA.PMS.DataAssmblly.FD_Hotel()
                {
                    HotelName = txtHotelName.Text,
                    Area = txtArea.Text,
                    IsDelete = false,
                    DeskCount = txtDeskCount.Text.ToInt32(),
                    Tel = txtTel.Text.Trim(),
                    PriceStar = txtPriceStar.Text.ToDecimal(),
                    PriceEnd = txtPriceEnd.Text.ToDecimal(),
                    Details = txtDetails.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    StarLevel = ddlStarLevel.SelectedValue.ToInt32(),
                    Money1 = txtMoney1.Text,
                    Money2 = txtMoney2.Text,
                    Other = txtOther.Text,
                    HotelType = ddlStarLevel.SelectedItem.Text,
                    EvalScore = txtScore.Text.ToDouble(),
                    Sort = txtSort.Text.ToInt32(),
                    ReCommand = rdoRecommand.SelectedValue.ToInt32(),
                    OnSale = rdoOnSale.SelectedValue.ToInt32()
                };

                int result = objHotelBLL.Insert(fD_Hotel);
                var Model = objHotelBLL.GetByID(result);

                for (int i = 0; i < rptLabelList.Items.Count; i++)
                {
                    CheckBox chSinger = rptLabelList.Items[i].FindControl("chSinger") as CheckBox;
                    HiddenField hfLabelValue = rptLabelList.Items[i].FindControl("hfLabelValue") as HiddenField;
                    if (chSinger.Checked)
                    {
                        int labelId = hfLabelValue.Value.ToInt32();
                        objHotelLabelLogBLL.Insert(new FD_HotelLabelLog()
                        {
                            HotelID = result,
                            LableID = labelId
                        });

                        Model.LabelContent += objHotelLabelBLL.GetByID(labelId) + ",";
                        objHotelBLL.Update(Model);
                    }
                }

                if (result > 0)
                {
                    var HotelModel = objHotelBLL.GetByName(txtHotelName.Text.Trim().ToString());
                    HotelModel.Letter = PinYin.GetFirstLetter(txtHotelName.Text.Trim().ToString()).ToUpper();
                    objHotelBLL.Update(HotelModel);
                    JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

                }
            }
            else
            {
                JavaScriptTools.AlertWindow("酒店已存在,请更换酒店名称", this.Page);
            }
        }

    }
}