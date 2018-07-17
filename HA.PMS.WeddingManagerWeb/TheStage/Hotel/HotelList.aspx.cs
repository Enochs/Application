using HA.PMS.BLLAssmblly.CA;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.TheStage.Hotel
{
    public partial class HotelList : SystemPage
    {
        HA.PMS.BLLAssmblly.FD.Hotel ObjHotelBLL = new BLLAssmblly.FD.Hotel();
        HA.PMS.BLLAssmblly.FD.HotelImg ObjHotelImg = new BLLAssmblly.FD.HotelImg();
        BanquetHall ObjHallBLL = new BanquetHall();
        Distrinct ObjDistrinctBLL = new Distrinct();
        HotelLabel objHotelLabelBLL = new HotelLabel();
        HotelLabelLog ObjLogBLL = new HotelLabelLog();

        int SourceCount = 0;
        static string OrderByColumnName = "Sort";
        static OrderType Order;
        public static string sort = "Asc";


        #region 页面初始化

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["sort"] = OrderByColumnName;
            if (!IsPostBack)
            {
                CheckBoxBind(9, 50);
                BinderData();

            }
        }
        #endregion

        #region 数据绑定

        public void BinderData()
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            string Distrinct = GetChkByType(1);         //区域
            string DeskCount = GetChkByType(2);         //桌数
            string Type = GetChkByType(3);              //酒店类型
            string Price = GetChkByType(4);             //价格
            string Label = GetChkByType(5);             //价格

            if (Distrinct != "")
            {
                pars.Add("Area", Distrinct, NSqlTypes.SplitEqualString);
            }
            if (DeskCount != "")
            {
                pars.Add("DeskCount", DeskCount, NSqlTypes.DeskBetween);
            }
            if (Type != "")
            {
                pars.Add("HotelType", Type, NSqlTypes.SplitEqualString);
            }
            if (Price != "")
            {
                pars.Add("PriceStar", Price, NSqlTypes.DeskBetween);
            }

            if (ChkReCommand.Checked)
            {
                pars.Add("ReCommand", 1, NSqlTypes.Equal);
            }

            if (ChkOnSale.Checked)
            {
                pars.Add("OnSale", 1, NSqlTypes.Equal);
            }
            if (Label != "")
            {
                pars.Add("LabelContent", Label, NSqlTypes.SplitContain);
            }


            pars.Add(txtHotelName.Text.Trim().ToString() != string.Empty, "HotelName", txtHotelName.Text.ToString(), NSqlTypes.LIKE);   //酒店名称

            pars.Add("IsDelete", false, NSqlTypes.Bit);
            var DataList = ObjHotelBLL.GetWhereByParameter(pars, OrderByColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, Order);
            CtrPageIndex.RecordCount = SourceCount;

            rptHotel.DataSource = DataList;
            rptHotel.DataBind();



        }
        #endregion

        #region 地区绑定

        public void CheckBoxBind(int PageSize, int SPageSize)
        {
            string OrderByName = "ID";
            int Sum = 0;
            List<PMSParameters> pars = new List<PMSParameters>();
            var DataList = ObjDistrinctBLL.GetDataByParameter(pars, OrderByName, PageSize, 1, out Sum, OrderType.Asc);

            ChkDistrinct.DataSource = DataList;
            ChkDistrinct.DataTextField = "DistrinctName";
            ChkDistrinct.DataValueField = "ID";
            ChkDistrinct.DataBind();

            var DataLists = ObjDistrinctBLL.GetDataByParameter(pars, OrderByName, SPageSize, 1, out Sum, OrderType.Asc);
            var ModelList = DataLists.Skip(9).Take(41);
            ChkDistrincts.DataSource = ModelList;
            ChkDistrincts.DataTextField = "DistrinctName";
            ChkDistrincts.DataValueField = "ID";
            ChkDistrincts.DataBind();


            var LabelList = objHotelLabelBLL.GetDataByParameter(pars, "HotelLabelID", PageSize, 1, out Sum);
            ChkLabel.DataSource = LabelList;
            ChkLabel.DataTextField = "HotelLabelName";
            ChkLabel.DataValueField = "HotelLabelID";
            ChkLabel.DataBind();

            var LabelLists = objHotelLabelBLL.GetDataByParameter(pars, "HotelLabelID", SPageSize, 1, out Sum);
            var ModelsList = LabelLists.Skip(9).Take(41);
            ChkLabels.DataSource = ModelsList;
            ChkLabels.DataTextField = "HotelLabelName";
            ChkLabels.DataValueField = "HotelLabelID";
            ChkLabels.DataBind();

        }
        #endregion

        #region 分页事件


        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {

            BinderData();
        }
        #endregion

        #region 点击查询

        protected void btnGet_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            CtrPageIndex.CurrentPageIndex = 1;
            if (btn.ID == "btnConfirm")
            {
                BinderData();
            }
            else if (btn.ID == "btnReset")
            {
                Response.Redirect("HotelList.aspx");
            }



        }
        #endregion

        #region 获取CheckBox

        public string GetChkByType(int Type)
        {
            string chkSelect = "";
            if (Type == 1)          //区域
            {
                for (int i = 0; i < ChkDistrinct.Items.Count; i++)
                {
                    if (ChkDistrinct.Items[i].Selected == true)
                    {
                        chkSelect += ChkDistrinct.Items[i].Text + ",";
                    }
                }
                for (int i = 0; i < ChkDistrincts.Items.Count; i++)
                {
                    if (ChkDistrincts.Items[i].Selected == true)
                    {
                        chkSelect += ChkDistrincts.Items[i].Text + ",";
                    }
                }
                if (chkSelect != "")
                {
                    chkSelect = chkSelect.Substring(0, chkSelect.Length - 1);
                }
            }
            else if (Type == 2)            //桌数   
            {
                for (int i = 0; i < ChkDeskCount.Items.Count; i++)
                {
                    if (ChkDeskCount.Items[i].Selected == true)
                    {
                        chkSelect += ChkDeskCount.Items[i].Value + ",";
                    }
                }
                if (chkSelect != "")
                {
                    chkSelect = chkSelect.Substring(0, chkSelect.Length - 1);
                }
            }
            else if (Type == 3)         //酒店类型
            {
                for (int i = 0; i < ChkType.Items.Count; i++)
                {
                    if (ChkType.Items[i].Selected == true)
                    {
                        chkSelect += ChkType.Items[i].Text + ",";
                    }
                }
                if (chkSelect != "")
                {
                    chkSelect = chkSelect.Substring(0, chkSelect.Length - 1);
                }
            }
            else if (Type == 4)            //价格   
            {
                for (int i = 0; i < ChkPrice.Items.Count; i++)
                {
                    if (ChkPrice.Items[i].Selected == true)
                    {
                        chkSelect += ChkPrice.Items[i].Value + ",";
                    }
                }
                if (chkSelect != "")
                {
                    chkSelect = chkSelect.Substring(0, chkSelect.Length - 1);
                }
            }
            else if (Type == 5)          //区域
            {
                for (int i = 0; i < ChkLabel.Items.Count; i++)
                {
                    if (ChkLabel.Items[i].Selected == true)
                    {
                        chkSelect += ChkLabel.Items[i].Text + ",";
                    }
                }
                for (int i = 0; i < ChkLabels.Items.Count; i++)
                {
                    if (ChkLabels.Items[i].Selected == true)
                    {
                        chkSelect += ChkLabels.Items[i].Text + ",";
                    }
                }
                if (chkSelect != "")
                {
                    chkSelect = chkSelect.Substring(0, chkSelect.Length - 1);
                }
            }
            return chkSelect;
        }
        #endregion

        #region 外层酒店 绑定完成事件 宴会厅

        protected void rptHotel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptHall = e.Item.FindControl("rptBanquetHall") as Repeater;
            int HotelID = (e.Item.FindControl("HideHotelID") as HiddenField).Value.ToInt32();

            var DataList = ObjHallBLL.GetByHotelIDAll(HotelID);
            rptHall.DataBind(DataList.Take(3).ToList());

            Label lbtnLook = (e.Item.FindControl("lblLook")) as Label;
            if (DataList.Count >= 3)
            {
                lbtnLook.Text = "查看全部" + DataList.Count + "个宴会厅";
            }
            else if (DataList.Count == 0)
            {
                lbtnLook.Text = "";
            }
            else if (DataList.Count <= 3 && DataList.Count >= 1)
            {
                lbtnLook.Text = "查看宴会厅";
            }
        }
        #endregion

        #region 排序方式 价格 桌数

        protected void lbtnSort_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (sender as LinkButton);
            if (lbtn.ID == "lbtnSort")
            {
                OrderByColumnName = "Sort";
                //this.Page.Response.Redirect(this.Page.Request.Url.ToString());
            }
            else if (lbtn.ID == "lbtnDeskSort")
            {
                OrderByColumnName = "DeskCount";
            }
            else if (lbtn.ID == "lbtnPriceSort")
            {
                OrderByColumnName = "PriceStar";
            }

            if (sort == "Asc")
            {
                Order = OrderType.Asc;
                sort = "Desc";
            }
            else if (sort == "Desc")
            {
                Order = OrderType.Desc;
                sort = "Asc";
            }
            BinderData();



        }
        #endregion

        #region 获取评分 星星显示
        /// <summary>
        /// 显示星星
        /// </summary>
        public string GetImgScore(object Source)
        {
            if (Source != null)
            {
                decimal EvalScore = Source.ToString().ToDecimal();

                if (EvalScore.ToString().Contains("."))
                {
                    string[] score = EvalScore.ToString().Split('.');
                    return "images/" + score[0] + "part.png";
                }
                else
                {
                    if (EvalScore.ToString() == "0")
                    {
                        return "";
                    }
                    else
                    {
                        return "images/" + EvalScore.ToString() + ".png";
                    }
                }
            }

            return "images/3.png";
        }
        #endregion

        #region 选中变化 是否推荐  有优惠
        /// <summary>
        /// 选中变化事件
        /// </summary>
        protected void ChkReward_CheckedChanged(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }
        #endregion

        #region 推荐  优惠图标的显示. 隐藏
        /// <summary>
        /// 显示 隐藏图标
        /// </summary>
        public string IsShow(object Source, object Source1)
        {
            if (Source != null)
            {

                int Type = Source1.ToString().ToInt32();

                if (Type == 1)          //推荐
                {
                    if (Source1 != null)
                    {
                        int ReCommand = Source.ToString().ToInt32();
                        if (ReCommand == 1)
                        {
                            return "style='display:block;'";
                        }
                        else if (ReCommand == 2)
                        {
                            return "style='display:none;'";
                        }
                    }
                    else
                    {
                        return "style='display:none;'";
                    }
                }

                else if (Type == 2)         //优惠
                {
                    if (Source1 != null)
                    {
                        int OnSale = Source.ToString().ToInt32();
                        if (OnSale == 1)
                        {
                            return "style='display:block;'";
                        }
                        else if (OnSale == 2)
                        {
                            return "style='display:none;'";
                        }
                    }
                    else
                    {
                        return "style='display:none;'";
                    }
                }
            }
            return "style='display:none;'";
        }
        #endregion

        public string GetVisible(object Source)
        {
            int HotelID = Source.ToString().ToInt32();
            var DataList = ObjHallBLL.GetByHotelIDAll(HotelID);
            if (DataList.Count == 3)
            {
                return string.Empty;
            }
            else
            {
                return "style='display:none;'";
            }
        }


    }
}