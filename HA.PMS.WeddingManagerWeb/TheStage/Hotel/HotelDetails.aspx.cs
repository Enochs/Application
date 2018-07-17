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
namespace HA.PMS.WeddingManagerWeb.TheStage.Hotel
{
    public partial class HotelDetails : System.Web.UI.Page
    {
        BLLAssmblly.FD.Hotel objHotelBLL = new BLLAssmblly.FD.Hotel();
        HotelImg objHotelImgBLL = new HotelImg();
        HotelLabelLog objHotelLabelLogBLL = new HotelLabelLog();
        HotelLabel objHotelLabelBLL = new HotelLabel();
        BanquetHall objBanquetHallBLL = new BanquetHall();
        BanquetHallImg objBanquetHallImgLL = new BanquetHallImg();
        int HotelID;

        #region 页面初始化
        /// <summary>
        /// 对页面进行初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            HotelID = Request.QueryString["HotelID"].ToInt32();
            if (!IsPostBack)
            {

                FD_Hotel singerQuery = objHotelBLL.GetByID(HotelID);
                lblHotelName.Text = singerQuery.HotelName;

                imgHotel.Src = singerQuery.HotelImagePath;
                lblHotelType.Text = singerQuery.HotelType;
                lblPriceStar.Text = singerQuery.PriceStar + string.Empty;
                lblPriceEnd.Text = singerQuery.PriceEnd + string.Empty;
                lblDeskCount.Text = singerQuery.DeskCount + string.Empty;
                lblAddress.Text = singerQuery.Area + " " + singerQuery.Address;

                #region 标签
                rptLabel.DataSource = objHotelLabelLogBLL.GetByHotelIDAll(HotelID);
                rptLabel.DataBind();
                #endregion

                lblTel.Text = singerQuery.Tel;
                lblHotelDetails.Text = singerQuery.Details;
                lblScore.Text = singerQuery.EvalScore.ToString().Length == 1 ? singerQuery.EvalScore.ToString() + ".0" : singerQuery.EvalScore.ToString();
                ViewState["HotelImagePath"] = singerQuery.HotelImagePath;       //获取酒店封面图片
                GetImgScore(singerQuery.EvalScore);      //获取评价

                var BanquetHallList = objBanquetHallBLL.GetByHotelIDAll(singerQuery.HotelID);
                if (BanquetHallList != null && BanquetHallList.Count > 0)
                {
                    dlBanquetHall.DataBind(BanquetHallList);
                }

                //酒店图片列表
                var ImageList = objHotelImgBLL.GetByHotelIDAll(HotelID);
                dlImages.DataSource = ImageList;
                dlImages.DataBind();

            }
        }
        #endregion

        #region 获取宴会厅 图片地址方法
        /// <summary>
        /// 获取图片地址(宴会厅)
        /// </summary>
        protected string GetBanquetHallPath(object BanquetHallID)
        {
            var query = objBanquetHallImgLL.GetByBanquetHallIDAll(BanquetHallID.To<Int32>(0)).FirstOrDefault();
            return object.ReferenceEquals(query, null) ? string.Empty : query.BanquetHallPath;
        }
        #endregion

        #region 获取标签
        /// <summary>
        /// 获取酒店的标签
        /// </summary>
        public string GetLabelByID(object Source)
        {
            int LabelID = Source.ToString().ToInt32();
            var HotelLabel = objHotelLabelBLL.GetByID(LabelID);
            return HotelLabel.HotelLabelName + "  ";
        }
        #endregion

        #region 点击登陆
        /// <summary>
        /// 登陆功能
        /// </summary>  
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            Employee ObjEmployee = new Employee();
            //login 首页导航，不加入验证中

            if (string.IsNullOrEmpty(txtLoginName.Text) || string.IsNullOrEmpty(txtpassword.Text))
            {
                Response.Redirect("/Account/AdminWorklogin.html");
            }
            int userId = 0;
            if (Request.Cookies["userName"] != null)
            {
                userId = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            }
            var ObjSysEmpLoyee = ObjEmployee.EmpLoyeeLogin(txtLoginName.Text.Trim(), txtpassword.Text.Trim().MD5Hash());
            if (ObjSysEmpLoyee != null)
            {
                Response.Cookies["userName"].Value = ObjSysEmpLoyee.EmployeeID + string.Empty;
                Response.Cookies["HAEmployeeID"].Value = ObjSysEmpLoyee.EmployeeID + string.Empty;
                Response.Cookies["userName"].Expires = DateTime.Now.AddMinutes(15);
                System.Web.Security.FormsAuthentication.SetAuthCookie(ObjSysEmpLoyee.EmployeeID.ToString(), false);
                Response.Redirect("/AdminPanlWorkArea/AdminMain.aspx");
            }
            else
            {
                Response.Redirect("/Account/AdminWorklogin.html?error=true");
            }
        }
        #endregion

        #region 获取评分 星星显示
        /// <summary>
        /// 显示星星
        /// </summary>
        public void GetImgScore(object Source)
        {
            if (Source != null)
            {
                decimal EvalScore = Source.ToString().ToDecimal();

                if (EvalScore.ToString().Contains("."))
                {
                    string[] score = EvalScore.ToString().Split('.');
                    imgScore.Src = "images/" + score[0] + "part.png";
                }
                else
                {
                    if (EvalScore == 0)
                    {
                        imgScore.Visible = false;
                    }
                    else
                    {
                        imgScore.Src = "images/" + EvalScore.ToString() + ".png";
                    }
                }
            }
        }
        #endregion

        #region 酒店图片列表 显示大图
        /// <summary>
        /// 绑定完成(宴会厅)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected string GetImgPath(object source)
        {
            string newPath = (source + string.Empty);
            return newPath.Replace("~", "/.");
        }
        #endregion


    }
}