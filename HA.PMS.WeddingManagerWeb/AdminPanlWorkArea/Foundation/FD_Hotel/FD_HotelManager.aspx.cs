using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using System.IO;
using System.Data.Objects;
using System.Text;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel
{
    public partial class FD_HotelManager : HA.PMS.Pages.SystemPage
    {
        Hotel objHotelBLL = new Hotel();
        HotelImg objHotelImgBLL = new HotelImg();
        BanquetHall objBanquetHallBLL = new BanquetHall();
        BanquetHallImg objBanquetHallImgBLL = new BanquetHallImg();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["load"] = "'" + EncodeBase64("/AdminPanlWorkArea/Foundation/FD_Hotel/SaveHotelImgToDB") + "'";
                BinderAreaTags();
                BinderData();
            }
        }

        protected void BinderData()
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(!string.IsNullOrWhiteSpace(txtHotelName.Text), "HotelName_LIKE", txtHotelName.Text.Trim());
            parameters.Add(!string.IsNullOrWhiteSpace(txtArea.Text), "Area_LIKE", txtArea.Text.Trim());
            parameters.Add(ddlStarLevel.SelectedValue.ToInt32() > 0, "StarLevel", ddlStarLevel.SelectedValue.ToInt32());
            int sourceCount = 0;
            var dataSource = objHotelBLL.GetByIndex(HotTelPager.PageSize, HotTelPager.CurrentPageIndex, out sourceCount, parameters);
            HotTelPager.RecordCount = sourceCount;
            rptHotel.DataBind(dataSource);
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

        protected string GetStarLevelText(object StarLevel)
        {
            return objHotelBLL.GetStarLevelText(Convert.ToInt32(StarLevel));
        }

        protected void rptHotel_ItemCommand(object source, RepeaterCommandEventArgs e)
        {


            if (e.CommandName == "Delete")
            {
                int HotelId = e.CommandArgument.ToString().ToInt32();

                //创建酒店实体类
                HA.PMS.DataAssmblly.FD_Hotel fD_Hotel = new HA.PMS.DataAssmblly.FD_Hotel()
                {
                    HotelID = HotelId
                };

                #region 删除酒店对应的图片
                var objResult = objHotelImgBLL.GetByHotelIDAll(HotelId).ToList();
                for (int i = 0; i < objResult.Count; i++)
                {
                    if (File.Exists(Server.MapPath(objResult[i].HotelImagePath)))
                    {
                        File.Delete(Server.MapPath(objResult[i].HotelImagePath));
                    }
                    objHotelImgBLL.Delete(objResult[i]);

                }
                #endregion

                #region 删除酒店对应的宴会厅
                var objResultBanquet = objBanquetHallBLL.GetByHotelIDAll(HotelId).ToList();
                for (int i = 0; i < objResultBanquet.Count; i++)
                {   //查询该宴会厅对应的图片 
                    var currentBanquetHallImg = objBanquetHallImgBLL.GetByBanquetHallIDAll(objResultBanquet[i].BanquetHallID).ToList();
                    for (int j = 0; j < currentBanquetHallImg.Count; j++)
                    {
                        if (File.Exists(Server.MapPath(currentBanquetHallImg[i].BanquetHallPath)))
                        {
                            File.Delete(Server.MapPath(currentBanquetHallImg[i].BanquetHallPath));
                        }
                        //删除单个宴会厅的图片数据
                        objBanquetHallImgBLL.Delete(currentBanquetHallImg[j]);
                    }
                    //删除单个宴会厅
                    objBanquetHallBLL.Delete(objResultBanquet[i]);
                }
                #endregion

                objHotelBLL.Delete(fD_Hotel);
                //删除之后重新绑定数据源
                BinderData();
            }
        }

        protected void HotTelPager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            HotTelPager.CurrentPageIndex = 1;
            BinderData();
        }

        /// <summary>
        /// 设置默认封面图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptHotel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int HotelsID = (e.Item.FindControl("HideHotelID") as HiddenField).Value.ToInt32();
            var Model = objHotelBLL.GetByID(HotelsID);
            if (Model.HotelImagePath == null || Model.HotelImagePath == "")
            {
                var DataList = objHotelImgBLL.GetByHotelIDAll(HotelsID);
                if (DataList.Count > 0)
                {
                    Model.HotelImagePath = DataList.FirstOrDefault().HotelImagePath;
                    objHotelBLL.Update(Model);      //选择封面图片
                }
            }
        }
    }
}