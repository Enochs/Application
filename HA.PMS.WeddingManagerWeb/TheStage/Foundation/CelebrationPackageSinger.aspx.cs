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


namespace HA.PMS.WeddingManagerWeb.TheStage.Foundation
{
    public partial class CelebrationPackageSinger : System.Web.UI.Page
    {
        CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();
        CelebrationPackageProduct objCelebrationPackageProductBLL = new CelebrationPackageProduct();
        CelebrationPackageImage objCelebrationPackageImageBLL = new CelebrationPackageImage();
        CelebrationPackageStyle objCelebrationPackageStyleBLL = new CelebrationPackageStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int PackageID =Convert.ToInt32( Request.QueryString["PackageID"]);
                LoadPackageDetails(PackageID);
                DataPackageProduct(PackageID);
                LoadPackagerImg(PackageID);
                repeaterBigImgDataBinder(PackageID);
            }
        }

        protected string GetStaticUrl(object source) 
        {
            string newPath = (source + string.Empty).Replace("~","");
            return newPath;
        
        }

        protected string GetPackageTitle(object PackageID)
        {
            FD_CelebrationPackage fD_CelebrationPackage = objCelebrationPackageBLL.GetByID(Convert.ToInt32(PackageID));
            return fD_CelebrationPackage != null ? fD_CelebrationPackage.PackageTitle : string.Empty;
        }

        protected void LoadPackagerImg(int PackageID) 
        {
            rptCeleImgPlay.DataSource = objCelebrationPackageImageBLL.GetByAll().Where(C => C.PackageId == PackageID);
            rptCeleImgPlay.DataBind();
        }
        /// <summary>
        /// 大图区域
        /// </summary>
        /// <param name="PackageID"></param>
        protected void repeaterBigImgDataBinder(int PackageID)
        {
            rptList.DataSource = objCelebrationPackageImageBLL.GetByAll().Where(C => C.PackageId == PackageID);
            rptList.DataBind();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                if (i == 0)
                {
                    HA.PMS.DataAssmblly.FD_CelebrationPackage fd = objCelebrationPackageBLL.GetByID(PackageID);
                    Literal ltlTopTitle = rptList.Items[i].FindControl("ltlTopTitle") as Literal;
                    //Literal ltlPackagePrice = rptList.Items[i].FindControl("ltlPackagePrice") as Literal;
                    Literal ltlPackagePreferentiaPrice = rptList.Items[i].FindControl("ltlPackagePreferentiaPrice") as Literal;

                    Literal ltlPackageStyle = rptList.Items[i].FindControl("ltlPackageStyle") as Literal;
                    ltlTopTitle.Text = fd.PackageTitle;
                    //ltlPackagePrice.Text = fd.PackagePrice + string.Empty;
                    ltlPackagePreferentiaPrice.Text = fd.PackagePreferentiaPrice + string.Empty;
                    ltlPackageStyle.Text = objCelebrationPackageStyleBLL
                        .GetByID(fd.PackageSkip).StyleName;


                }
                else
                {
                    Label lblExplain = rptList.Items[i].FindControl("lblExplain") as Label;
                    lblExplain.Attributes.Add("style", "display:none");
                }
            }
        }
        /// <summary>
        /// 加载套系详情
        /// </summary>
        /// <param name="PackageID"></param>
        protected void LoadPackageDetails(int PackageID) 
        {
            HA.PMS.DataAssmblly.FD_CelebrationPackage fd = objCelebrationPackageBLL.GetByID(PackageID);
            ltlPackPackageTitle.Text = fd.PackageTitle;
            if (fd.PackageDate.HasValue)
            {
                ltlPackPackageDate.Text = fd.PackageDate.Value.ToShortDateString();
            }
        
            ltlPackerDatails.Text = fd.PackageDetails;
            imgPackageImgTop.ImageUrl = fd.PackageImgTop;
            ltlTopNo1.Text = fd.PackageTitle;
        }
        /// <summary>
        /// 加载套系搭档
        /// </summary>
        protected void DataPackageProduct(int PackageID) 
        {
            var query = objCelebrationPackageProductBLL.GetPackageProductByKeysIndex(PackageID);
            if (query != null)
            {
                rptPackage.DataSource = query.ToList(); rptPackage.DataBind();
            }
        }
       
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HA.PMS.DataAssmblly.FD_CelebrationPackage fd_Delebration = e.Item.DataItem as HA.PMS.DataAssmblly.FD_CelebrationPackage;
            
        }
    }
}