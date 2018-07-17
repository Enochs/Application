using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_QuotedCatgoryProductCreateUpdate : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int SourceProductId = Request.QueryString["SourceProductId"].ToInt32();
                DataDropDownList();
                if (SourceProductId > 0)
                {
                    FD_StorehouseSourceProduct fd_Storehouse = objStorehouseSourceProductBLL.GetByID(SourceProductId);
                    if (fd_Storehouse != null)
                    {
                        txtSaleOrice.Text = fd_Storehouse.SaleOrice + string.Empty;
                        txtRemark.Text = fd_Storehouse.Remark;
                        txtUnit.Text = fd_Storehouse.Unit + string.Empty;
                        txtSpecifications.Text = fd_Storehouse.Specifications;

                        txtSourceProductName.Text = fd_Storehouse.SourceProductName;
                        txtPurchasePrice.Text = fd_Storehouse.PurchasePrice.ToString();



                        fd_Storehouse.Specifications = txtSpecifications.Text;


                        AllProducts ObjAllProductsBLL = new AllProducts();
                        var ClassModel = ObjAllProductsBLL.GetVPByKind(9, fd_Storehouse.SourceProductId);
                        if (ClassModel != null)
                        {
                            var KeyList = ClassModel.Classification.Trim(',').Split(',');

                            foreach (var Key in KeyList)
                            {
                                if (Key != string.Empty)
                                {
                                    CheckBoxList1.Items.FindByText(Key).Selected = true;
                                }
                            }
                        }
                    }
                }
            }



            if (Request["Qckey"].ToInt32() > 0)
            {
                int key = Request["ProjectCategory"].ToInt32();
                if (key == 0)
                {
                    var CatogryModel = ObjQuotedCatgoryBLL.GetByID(Request["QcKey"].ToInt32());

                    ListItem ddlCategoryInit = ddlParentCatogry.Items.FindByValue(CatogryModel.Parent + string.Empty);
                    if (ddlCategoryInit != null)
                    {
                        ddlCategoryInit.Selected = true;
                    }
                    ddlParentCatogry_SelectedIndexChanged(sender, e);
                    ListItem ddlProjectInit = ddlSecondCatgory.Items.FindByValue(Request["QcKey"]);
                    if (ddlProjectInit != null)
                    {
                        ddlProjectInit.Selected = true;
                    }
                    ddlParentCatogry.Enabled = false;
                    ddlSecondCatgory.Enabled = false;
                }
            }
        }
        #endregion

        #region 顶级 父级绑定
        /// <summary>
        /// 绑定父极
        /// </summary>
        protected void DataDropDownList()
        {
            int key = Request["ProductCategory"].ToInt32();
            ddlParentCatogry.DataSource = ObjQuotedCatgoryBLL.GetByParentID(0);
            ddlParentCatogry.DataTextField = "Title";
            ddlParentCatogry.DataValueField = "QcKey";
            ddlParentCatogry.DataBind();
            if (key > 0)
            {
                ddlParentCatogry.Items.FindByValue(key.ToString()).Selected = true;
            }

            SecondBind(key);

        }
        #endregion

        #region 二级 父级绑定
        /// <summary>
        /// 根据父极绑定
        /// </summary>
        protected void ddlParentCatogry_SelectedIndexChanged(object sender, EventArgs e)
        {
            SecondBind(ddlParentCatogry.SelectedValue.ToInt32());

        }
        #endregion

        #region 二级绑定
        /// <summary>
        /// 二级绑定
        /// </summary>
        public void SecondBind(int PrarentCategory)
        {
            int key = Request["ProjectCategory"].ToInt32();
            var source = ObjQuotedCatgoryBLL.GetByParentID(PrarentCategory); ;
            ddlSecondCatgory.DataSource = source;
            ddlSecondCatgory.DataTextField = "Title";
            ddlSecondCatgory.DataValueField = "QcKey";
            ddlSecondCatgory.DataBind();
            if (key > 0)
            {
                ddlSecondCatgory.Items.FindByValue(key.ToString()).Selected = true;
            }
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存修改或者新建
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (ddlSecondCatgory.SelectedValue.ToInt32() == 0)
            {
                JavaScriptTools.AlertWindow("请选择产品项目!", Page);
                return;
            }
            int SourceProductId = Request.QueryString["SourceProductId"].ToInt32();
            FD_StorehouseSourceProduct fd_Storehouse = new FD_StorehouseSourceProduct();
            if (SourceProductId > 0)
            {

                fd_Storehouse = objStorehouseSourceProductBLL.GetByID(SourceProductId);

            }
            fd_Storehouse.IsDelete = false;

            fd_Storehouse.SourceProductName = txtSourceProductName.Text;

            fd_Storehouse.PutStoreDate = DateTime.Now;
            fd_Storehouse.ProductCategory = ddlParentCatogry.SelectedValue.ToInt32();
            fd_Storehouse.ProductProject = ddlSecondCatgory.SelectedValue.ToInt32();
            fd_Storehouse.Specifications = txtSpecifications.Text;
            fd_Storehouse.Remark = txtRemark.Text;
            fd_Storehouse.PurchasePrice = txtPurchasePrice.Text.ToString() == "" ? 0 : txtPurchasePrice.Text.ToInt32();
            fd_Storehouse.SaleOrice = txtSaleOrice.Text.ToDecimal();
            fd_Storehouse.Unit = txtUnit.Text;
            fd_Storehouse.Position = string.Empty;

            string ClassType = string.Empty;
            foreach (ListItem Objitem in CheckBoxList1.Items)
            {
                if (Objitem.Selected)
                {
                    ClassType += Objitem.Value + ",";
                }
            }
            fd_Storehouse.Unit += "VirtualProduct" + "①" + ClassType.TrimEnd(',');
            string savaPath = "~/Files/Storehouse/";
            string fileExt = "";


            fd_Storehouse.ProductState = "编辑";
            if (SourceProductId > 0)
            {
                objStorehouseSourceProductBLL.Update(fd_Storehouse);
            }
            else
            {
                objStorehouseSourceProductBLL.Insert(fd_Storehouse);
            }
            //根据返回判断添加的状态

            JavaScriptTools.AlertAndClosefancybox("操作完毕;", Page);

        }
        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            string Property = "";
            if (rdohstoPakge.Checked)
            {
                Property += rdohstoPakge.Text;
            }

            HandleModel.HandleContent = "基础信息设置-报价单类别项目 添加单个产品,产品名称:" + txtSourceProductName.Text.Trim().ToString() +
                "产品类别:" + ddlParentCatogry.SelectedItem.Text + "产品项目:" + ddlSecondCatgory.SelectedItem.Text + "产品属性:" + Property;

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 13;     //基础信息设置
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}