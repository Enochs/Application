
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.20
 Description:制作套系报价单页面
 History:修改日志

 Author:杨洋
 date:2013.3.20
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
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Web.Script.Serialization;
using System.Text;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage
{
    //制作套系报价单
    public partial class FD_CelebrationPackageMakeQuotedPrice : SystemPage
    {
        Category objCategoryBLL = new Category();
        Productcs objProductsBLL = new Productcs();
        //制作套系报价单 数据操作类
        CelebrationPackageMakeQuotedPrice ObjCelebrationPackageMakeQuotedPriceBLL = new CelebrationPackageMakeQuotedPrice();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 根据选择产品ID 回传之后代码区域
            //注意这里不能写在if(!ispostback)里面是因为 TrackViewState()方法将被触发去跟踪这些动态创建的控件，
            //才能保存它们的视图装填
            //在选择产品界面之后回传过来的产品ID集合
            string productIds = Request.QueryString["productIds"];
            if (productIds != null)
            {
                GetCallBackLoadDropDown();
                string[] ids = productIds.Split(',');

                ViewState["ids"] = ids;
                LoadOtherOption(ids);
            }
            #endregion
            if (!IsPostBack)
            {


                DataBinderDropDown(ddlCategoryProduct, 0);


            }
        }

        //protected override void OnInit(EventArgs e)
        //{


        //}
        /// <summary>
        /// 返回选择产品之后加载对应之前的选择类别
        /// </summary>
        protected void GetCallBackLoadDropDown()
        {
            //第一级项目 
            string cidFirst = Request.QueryString["cidFirst"];
            //第一级项类别
            string cidSecond = Request.QueryString["cidSecond"];
            if (cidFirst != null)
            {
                DataBinderDropDown(ddlCategoryProduct, 0);
                ddlCategoryProduct.Items.FindByValue(cidFirst).Selected = true;
                lblProductName.Text = ddlCategoryProduct.SelectedItem.Text;
            }
            if (cidSecond != null)
            {
                DataBinderDropDown(ddlCategorySecond, cidFirst.ToInt32());
                ddlCategorySecond.Items.FindByValue(cidSecond).Selected = true;

                lblCategoryName.Text = ddlCategorySecond.SelectedItem.Text;
            }
        }

        protected void DataBinderDropDown(DropDownList drop, int parentId)
        {
            drop.DataSource = objCategoryBLL.GetByAll().Where(C => C.ParentID == parentId).ToList();
            drop.DataTextField = "CategoryName";
            drop.DataValueField = "CategoryID";
            drop.DataBind();

        }

        protected void btnProductSave_Click(object sender, EventArgs e)
        {
            lblProductName.Text = ddlCategoryProduct.SelectedItem.Text;
            int parentId = ddlCategoryProduct.SelectedValue.ToInt32();
            DataBinderDropDown(ddlCategorySecond, parentId);
        }

        protected void btnCategorySave_Click(object sender, EventArgs e)
        {
            if (ddlCategorySecond.Items.Count > 0)
            {
                lblCategoryName.Text = ddlCategorySecond.SelectedItem.Text;
            }
            else
            {
                JavaScriptTools.AlertWindow("对不起,没有找到要添加的类别", this.Page);
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] ids = ViewState["ids"] as string[];
            //对应长传图片的保存地址
            string imgPath = "~/Files/CelebrationPackageMakeQuotedPrice/";
            HA.PMS.DataAssmblly.FD_CelebrationPackageMakeQuotedPrice fd_Cele = null;
            int result = 0;
            if (ids!=null)
            {


                for (int i = 0; i < ids.Length; i++)
                {

                    fd_Cele = new DataAssmblly.FD_CelebrationPackageMakeQuotedPrice();
                    fd_Cele.ProductId = ids[i].ToInt32();
                    fd_Cele.ProductPrice = (pnlProductPrice.FindControl("txtProductPrice" + i) as TextBox).Text.ToInt32();
                    fd_Cele.ProductService = (pnlProductName.Controls[i] as Label).Text;
                    fd_Cele.CategoryProjectId = ddlCategoryProduct.SelectedValue.ToInt32();
                    fd_Cele.CategorySecondId = ddlCategorySecond.SelectedValue.ToInt32();
                    fd_Cele.SmallSum = (pnlSmallSum.Controls[i] as TextBox).Text.ToInt32();
                    fd_Cele.IsDelete = false;
                    fd_Cele.Count = (pnlCount.Controls[i] as TextBox).Text.ToInt32();
                    fd_Cele.Explain = (pnlExplain.Controls[i] as TextBox).Text;
                    fd_Cele.PackageID = 1;
                    fd_Cele.SpecificRequirements = (pnlSpecificRequirements.Controls[i] as TextBox).Text;
                    #region 开始上传图片
                    string fileExt = "";
                    FileUpload fuLoadFile = pnlImage.Controls[i] as FileUpload;
                    if (fuLoadFile.HasFile)
                    {

                        fileExt = System.IO.Path.GetExtension(fuLoadFile.FileName);

                        if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".pnl")
                        {

                            fd_Cele.ImageUrl = imgPath + fuLoadFile.FileName;
                            string rootImgPath = Server.MapPath(imgPath) + fuLoadFile.FileName;
                            fuLoadFile.SaveAs(rootImgPath);

                        }
                    }
                    #endregion

                    result = ObjCelebrationPackageMakeQuotedPriceBLL.Insert(fd_Cele);

                }
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("添加成功", this.Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("对不起,请你先选择产品", this.Page);
            }
        }
        /// <summary>
        /// 返回选择产品之后 提供对应填写内容
        /// </summary>
        /// <param name="count">选择产品的个数</param>
        protected void LoadOtherOption(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                #region 产品服务



                Label lblProductService = new Label();
                lblProductService.ID = "lblProductService" + i;
                lblProductService.Text = objProductsBLL.GetByID(str[i].ToInt32()).ProductName;

                pnlProductName.Controls.Add(lblProductService);


                #endregion
                #region 具体要求
                TextBox txtSpecificRequirements = new TextBox();
                txtSpecificRequirements.ID = "txtSpecificRequirements" + i;
                txtSpecificRequirements.Width = 60;
                pnlSpecificRequirements.Controls.Add(txtSpecificRequirements);

                #endregion

                #region 图片上传
                FileUpload ImageUrl = new FileUpload();
                ImageUrl.ID = "ImageUrl" + i;

                ImageUrl.Width = 70;

                pnlImage.Controls.Add(ImageUrl);

                #endregion

                #region 单价
                TextBox txtProductPrice = new TextBox();
                txtProductPrice.ID = "txtProductPrice" + i;
                txtProductPrice.Width = 60;

                pnlProductPrice.Controls.Add(txtProductPrice);

                #endregion

                #region 数量
                TextBox txtCount = new TextBox();
                txtCount.ID = "txtCount" + i;

                txtCount.Width = 60;
                pnlCount.Controls.Add(txtCount);

                #endregion

                #region 小计
                TextBox txtSmallSum = new TextBox();
                txtSmallSum.ID = "txtSmallSum" + i;
                txtSmallSum.Width = 60;
                pnlSmallSum.Controls.Add(txtSmallSum);

                #endregion

                #region 备注
                TextBox txtExplain = new TextBox();
                txtExplain.ID = "txtExplain" + i;
                txtExplain.Width = 60;
                pnlExplain.Controls.Add(txtExplain);

                #endregion
            }
        }



    }
}