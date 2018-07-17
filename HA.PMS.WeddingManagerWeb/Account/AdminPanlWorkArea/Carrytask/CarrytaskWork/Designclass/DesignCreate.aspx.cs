using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using System.IO;
using System.Net;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignCreate : SystemPage
    {
        int QuotedID = 0;
        int CustomerID = 0;
        int Type = 0;
        HA.PMS.BLLAssmblly.Flow.Designclass ObjDesignclassBLL = new BLLAssmblly.Flow.Designclass();
        HA.PMS.BLLAssmblly.FD.Material ObjMaterialBLL = new BLLAssmblly.FD.Material();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        DesignThemes ObjDesignThemesBLL = new DesignThemes();

        Supplier objSupplierBLL = new Supplier();

        Material objMaterial = new Material();

        /// <summary> 
        /// 
        /// </summary>
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 图片
        /// </summary>
        DesignUpload ObjDesignUploadBLL = new DesignUpload();

        /// <summary>
        /// 所有产品
        /// </summary>
        AllProducts ObjAllProductsBLL = new AllProducts();

        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomerBLL = new Customers();


        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            Type = Request["Type"].ToInt32();
            if (Type == 2)
            {
                btnSaveConfirm.Visible = false;
            }
            if (QuotedID > 0)
            {
                if (!IsPostBack)
                {
                    DDLBind();
                    BinderData();

                    FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                    txtEmpLoyee.Text = ObjQuotedPriceModel.DesignerEmployee == null ? GetEmployeeName(4) : GetEmployeeName(ObjQuotedPriceModel.DesignerEmployee.ToString());
                    txtPlanDate.Text = ObjQuotedPriceModel.PlanFinishDate == null ? "" : ObjQuotedPriceModel.PlanFinishDate.ToString().ToDateTime().ToShortDateString();
                    var DataList = ObjDesignclassBLL.GetByQuotedIDs(QuotedID);
                    if (Type == 2)      //策划师界面  下派设计师
                    {
                        if (User.Identity.Name.ToInt32() != ObjQuotedPriceModel.EmpLoyeeID || ObjQuotedPriceModel.DesignerState != 0)  //当前登陆人不是策划师  只能查看 已选择设计师/或者已经确定下单 就不能再修改 
                        {
                            btnSaveConfirm.Visible = false;
                            td_Dispatching.Visible = false;
                            RepDesignlist.Visible = false;
                            tr_TextBoxInsert.Visible = false;
                            th_Save.Visible = false;
                            btnSaveConfirm.Visible = false;
                            btnPrint2.Visible = false;
                            BtnExportImage.Visible = false;
                            this.RepDesignListShow.DataBind(DataList);
                            this.RepDesignlist.DataBind(null);
                        }
                    }
                    else if (Type == 1)     //设计师界面 下派设计单
                    {
                        if (User.Identity.Name.ToInt32() != ObjQuotedPriceModel.DesignerEmployee)  //当前登陆人不是设计师  只能查看 
                        {
                            btnSaveConfirm.Visible = false;
                            td_Dispatching.Visible = false;
                            RepDesignlist.Visible = false;
                            tr_TextBoxInsert.Visible = false;
                            th_Save.Visible = false;
                            btnSaveConfirm.Visible = false;
                            btnPrint2.Visible = false;
                            BtnExportImage.Visible = false;
                            this.RepDesignListShow.DataBind(DataList);
                            this.RepDesignlist.DataBind(null);
                        }
                        else
                        {
                            btnBackUp.Visible = true;
                        }
                    }

                }
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var QuotedPriceModel = ObjQuotedPriceBLL.GetByQuotedID(QuotedID);
            lblDesignEmployee.Text = QuotedPriceModel.DesignerEmployee.ToString() == null ? "" : GetEmployeeName(QuotedPriceModel.DesignerEmployee);
            lblPlanFinishDate.Text = QuotedPriceModel.PlanFinishDate.ToString().ToDateTime().ToShortDateString() == null ? "" : QuotedPriceModel.PlanFinishDate.ToString().ToDateTime().ToShortDateString();
            FL_DesignThemes ObjThemeModel = ObjDesignThemesBLL.GetByCustomerID(Request["CustomerID"].ToInt32());

            #region 主题
            if (ObjThemeModel == null)
            {
                txtThemes.Text = string.Empty;
                txtColorTone.Text = string.Empty;
                txThemeStyle.Text = string.Empty;
            }
            else if (ObjThemeModel != null)
            {
                txtThemes.Text = ObjThemeModel.Themes;
                txtColorTone.Text = ObjThemeModel.ColorTone;
                txThemeStyle.Text = ObjThemeModel.DesignStyle;
            }
            #endregion

            if (Type == 2 && QuotedPriceModel.DesignerEmployee == User.Identity.Name.ToInt32())     // 已接受
            {
                QuotedPriceModel.IsReceive = 1;
                ObjQuotedPriceBLL.Update(QuotedPriceModel);
            }


            bool IsShow = false;
            var DataList = ObjDesignclassBLL.GetByQuotedIDs(QuotedID);

            if (DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    if (item.State == 1)        //已经确认下单
                    {
                        IsShow = true;
                    }
                }
            }
            if (User.Identity.Name.ToInt32() != QuotedPriceModel.DesignerEmployee && User.Identity.Name.ToInt32() != QuotedPriceModel.EmpLoyeeID)  //当前登陆人不是设计师  只能查看 
            {
                btnSaveConfirm.Visible = false;
                td_Dispatching.Visible = false;
                RepDesignlist.Visible = false;
                tr_TextBoxInsert.Visible = false;
                th_Save.Visible = false;
                btnSaveConfirm.Visible = false;
                btnPrint2.Visible = false;
                BtnExportImage.Visible = false;
                btn_SaveThemes.Visible = false;
                this.RepDesignListShow.DataBind(DataList);
                this.RepDesignlist.DataBind(null);
            }
            else            //策划师或设计师是本人
            {
                if (IsShow == false)            //还未下单 绑定可修改列表 可以修改
                {
                    td_Dispatching.Visible = false;
                    if (Type == 2)
                    {
                        td_Dispatching.Visible = true;
                    }
                    RepDesignlist.Visible = true;
                    tr_TextBoxInsert.Visible = true;
                    th_Save.Visible = true;
                    btnSaveConfirm.Visible = true;
                    btnExport.Visible = true;
                    btnPrint2.Visible = false;
                    this.RepDesignListShow.DataBind(null);
                    this.RepDesignlist.DataBind(DataList);
                }
                else if (IsShow == true)     //已下单 不可修改
                {
                    td_Dispatching.Visible = false;
                    RepDesignlist.Visible = false;
                    tr_TextBoxInsert.Visible = false;
                    th_Save.Visible = false;
                    btnSaveConfirm.Visible = false;
                    btnExport.Visible = true;
                    btnPrint2.Visible = true;
                    this.RepDesignListShow.DataBind(DataList);
                    this.RepDesignlist.DataBind(null);
                }
            }


            if (Type == 2)
            {
                btnSaveConfirm.Visible = false;
            }
            if (Type == 1)
            {
                btn_SaveThemes.Visible = false;
            }

        }
        #endregion

        #region 下拉框绑定
        /// <summary>
        /// 下拉框绑定数据
        /// </summary>
        private void DDLBind()
        {
            //绑定材质
            List<FD_Material> objMaterialModel = ObjMaterialBLL.GetByAll();
            ddlMaterial.DataSource = objMaterialModel;
            ddlMaterial.DataTextField = "MaterialName";
            ddlMaterial.DataValueField = "MaterialId";
            ddlMaterial.DataBind();
            FD_Material material = ObjMaterialBLL.GetByID(ddlMaterial.SelectedValue.ToInt32());
            if (objMaterialModel.Count() == 0)
            {
                txtPurchasePrice.Text = "";
            }
            else
            {
                txtPurchasePrice.Text = material.MaterialUnitPrice.ToString();
            }

            //绑定供应商
            ddlSupplierName.DataSource = objSupplierBLL.GetByCategoryId(5);
            ddlSupplierName.DataTextField = "Name";
            ddlSupplierName.DataValueField = "SupplierID";
            ddlSupplierName.DataBind();
            ddlSupplierName.Items.Add(new ListItem("请选择", "0"));
            ddlSupplierName.SelectedValue = "166";
        }
        #endregion

        #region 新增设计清单
        /// <summary>
        ///添加喷绘设计 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            int SupplierId = 1;
            if (ddlSupplierName.SelectedValue.ToInt32() == 0)
            {
                SupplierId = 1;
            }
            else
            {
                SupplierId = ddlSupplierName.SelectedValue.ToInt32();
            }

            ObjDesignclassBLL.Insert(new FL_Designclass()
            {
                Title = txtTitle.Text,
                Node = txtNode.Text,
                PurchaseQuantity = txtPurchaseQuantity.Text.ToInt32(),
                PurchasePrice = txtPurchasePrice.Text.ToDecimal(),
                RealQuantity = 0,
                Unit = txtUnit.Text,
                Supplier = SupplierId.ToString(),
                TotalPrice = txtPurchaseQuantity.Text.ToInt32() * txtPurchasePrice.Text.ToDecimal(),
                CreateDate = DateTime.Now,
                CreateEmployee = User.Identity.Name.ToInt32(),
                QuotedID = QuotedID,
                CustomerID = Request["CustomerID"].ToInt32(),
                State = 0,
                Material = ddlMaterial.SelectedValue,
                Spec = txtSpec.Text

            });
            Response.Redirect(Request.Url.ToString());
            Type = Request["Type"].ToInt32();
            if (Type == 2)
            {
                btnSaveConfirm.Visible = false;
            }
        }
        #endregion

        #region 绑定 做事件(删除 修改)
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RepDesignlist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    ObjDesignclassBLL.Delete(ObjDesignclassBLL.GetByID(e.CommandArgument.ToString().ToInt32()));
                    Response.Redirect(Request.Url.ToString());
                    break;
                case "Edit":
                    var UpdateModel = ObjDesignclassBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    UpdateModel.Material = (e.Item.FindControl("ddlMaterial") as DropDownList).SelectedValue;       //材质
                    UpdateModel.Supplier = (e.Item.FindControl("ddlSupplier") as DropDownList).SelectedValue;       //供应商
                    UpdateModel.Unit = (e.Item.FindControl("txtUnit") as TextBox).Text.ToString();
                    UpdateModel.Spec = (e.Item.FindControl("txtSpec") as TextBox).Text;
                    UpdateModel.PurchaseQuantity = (e.Item.FindControl("txtPurchaseQuantity") as TextBox).Text.ToInt32();
                    UpdateModel.PurchasePrice = (e.Item.FindControl("txtPurchasePrice") as TextBox).Text.ToDecimal();
                    UpdateModel.TotalPrice = UpdateModel.PurchaseQuantity * UpdateModel.PurchasePrice;
                    ObjDesignclassBLL.Update(UpdateModel);
                    Response.Redirect(Request.Url.ToString());
                    break;
                //case "Check":
                //    var UpdateModel1 = ObjDesignclassBLL.GetByID(e.CommandArgument.ToString().ToInt32());

                //    UpdateModel1.Supplier = (e.Item.FindControl("txtSupplier") as TextBox).Text;
                //    //if ((e.Item.FindControl("txtTotalPrice") as TextBox).Text != "" || (e.Item.FindControl("txtTotalPrice") as TextBox).Text != null)
                //    //{
                //    //    UpdateModel1.TotalPrice = Convert.ToDecimal((e.Item.FindControl("txtTotalPrice") as TextBox).Text);
                //    //}
                //    UpdateModel1.State = 1;
                //    UpdateModel1.Material = (e.Item.FindControl("txtMaterial") as TextBox).Text;
                //    ObjDesignclassBLL.Update(UpdateModel1);
                //    JavaScriptTools.AlertWindow("下单成功",Page);
                //    Response.Redirect(Request.Url.ToString());
                //    break;
            }


        }
        #endregion

        #region 选择供应商
        /// <summary>
        /// 保存指定供应商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavesupperSave_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertWindow("我们都有一个家", Page);
            SetRowType(1);
        }
        #endregion

        #region 指定供应商
        /// <summary>
        /// 指定供应商
        /// </summary>
        /// <param name="Type"></param>
        private void SetRowType(int Type)
        {

            //int CategoryId = hideCategoryID.Value.ToInt32();
            //int PriceKey = hidePriceKey.Value.ToInt32();
            //JavaScriptTools.AlertWindow(CategoryId.ToString() + "___" + PriceKey.ToString(), Page);
        }
        #endregion

        #region 点击确认下单
        /// <summary>
        /// 确认保存 下单
        /// </summary>
        protected void btnSaveConfirm_Click(object sender, EventArgs e)
        {
            if (ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()) == null)
            {
                JavaScriptTools.AlertWindow("请将该设计单的主题信息填写完整", Page);
                return;
            }

            for (int i = 0; i < RepDesignlist.Items.Count; i++)
            {
                var ObjItem = RepDesignlist.Items[i];
                HiddenField HideDesign = ObjItem.FindControl("HideDesignId") as HiddenField;
                var ObjDesignModel = ObjDesignclassBLL.GetByID(HideDesign.Value.ToInt32());
                if (ObjDesignModel != null)
                {
                    ObjDesignModel.State = 1;
                    ObjDesignModel.Material = (ObjItem.FindControl("ddlMaterial") as DropDownList).SelectedValue;       //材质
                    ObjDesignModel.Spec = (ObjItem.FindControl("txtSpec") as TextBox).Text;
                    ObjDesignModel.Unit = (ObjItem.FindControl("txtUnit") as TextBox).Text;
                    ObjDesignModel.PurchaseQuantity = (ObjItem.FindControl("txtPurchaseQuantity") as TextBox).Text.ToInt32();
                    ObjDesignModel.PurchasePrice = (ObjItem.FindControl("txtPurchasePrice") as TextBox).Text.ToDecimal();
                    ObjDesignModel.TotalPrice = ObjDesignModel.PurchaseQuantity * ObjDesignModel.PurchasePrice;
                    ObjDesignModel.Supplier = (ObjItem.FindControl("ddlSupplier") as DropDownList).SelectedValue;       //供应商
                    ObjDesignclassBLL.Update(ObjDesignModel);
                }
            }

            FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(Request["QuotedId"].ToInt32());
            ObjQuotedPriceModel.DesignerState = 2;
            ObjQuotedPriceModel.AutualFinishDate = DateTime.Now.ToString().ToDateTime();
            ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);

            //操作日志
            CreateHandle(2);
            BinderData();
            JavaScriptTools.AlertWindow("下单成功", Page);

        }
        #endregion

        #region 材质选择变化事件
        /// <summary>
        /// 更改单价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FD_Material> objMaterialModel = ObjMaterialBLL.GetByAll();

            FD_Material material = ObjMaterialBLL.GetByID(ddlMaterial.SelectedValue.ToInt32());
            if (objMaterialModel.Count() == 0)
            {
                txtPurchasePrice.Text = "";
            }
            else
            {
                txtPurchasePrice.Text = material.MaterialUnitPrice.ToString();
            }
        }
        #endregion

        #region 绑定完成事件
        /// <summary>
        /// 完成
        /// </summary>
        protected void RepDesignListShow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList ddlMaterial = e.Item.FindControl("ddlMaterial") as DropDownList;   //材质
            Label lblMaterial = e.Item.FindControl("lblMaterial") as Label;

            DropDownList ddlSupplier = e.Item.FindControl("ddlSupplier") as DropDownList;   //供应商
            Label lblSupplier = e.Item.FindControl("lblSupplier") as Label;

            lblMaterial.Text = ddlMaterial.SelectedItem.Text;
            lblSupplier.Text = ddlSupplier.SelectedItem.Text;
        }
        #endregion

        #region 导出 Excel
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            ////获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            //Response.Buffer = true;
            ////获取或设置输出流的HTTP字符集
            //Response.Charset = "GB2312";
            ////将HTTP头添加到输出流
            //Response.AppendHeader("Content-Disposition", "attachment;filename=PriceManage" + DateTime.Now.Date.ToString("yyyyMMdd") + ".xls");
            ////获取或设置输出流的HTTP字符集
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            ////获取或设置输出流的HTTP MIME类型
            //Response.ContentType = "application/ms-excel";
            //System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //this.RepDesignListShow.RenderControl(htw);
            //string html = sw.ToString().Trim();
            //Response.Output.Write(html);
            //Response.Flush();
            //Response.End();

        }
        #endregion

        #region 导出图片
        /// <summary>
        /// 导出图片
        /// </summary>
        protected void BtnExportImage_Click(object sender, EventArgs e)
        {
            //DesignUpload ObjDesignUploadBLL = new DesignUpload();
            //FL_DesignUpload ObjDesignUploadModel = new FL_DesignUpload();
            //var DataList = ObjDesignUploadBLL.GetByOrderID(Request["OrderID"].ToInt32());
            //for (int i = 0; i < DataList.Count; i++)
            //{
            //    var ObjUploadModel = ObjDesignUploadBLL.GetByID(DataList[0].DuId);
            //    //1.获取文件虚拟路径
            //    string fileLocalPath = GetServerPath(ObjUploadModel.FileAddress).Replace("//", "/");
            //    Uri url = new Uri(GetServerPath(ObjUploadModel.FileAddress).ToString());
            //    //2.下载
            //    WebClient wc = new WebClient();
            //    try
            //    {
            //        wc.DownloadFile(Server.MapPath(fileLocalPath.ToString()), ObjUploadModel.FileName);
            //    }
            //    finally
            //    {
            //        if (wc != null)
            //        {
            //            wc.Dispose();
            //        }
            //    }
            //}
            //string strtxtPath = "D://Mailer";
            //string strzipPath = "D://Mailer.zip";
            //System.Diagnostics.Process Process1 = new System.Diagnostics.Process();
            //Process1.StartInfo.FileName = "Winrar.exe";
            //Process1.StartInfo.CreateNoWindow = true;

            ////// 1  
            //////压缩c:/freezip/free.txt(即文件夹及其下文件freezip/free.txt)  
            //////到c:/freezip/free.rar  
            //strzipPath = "E://Mailer.rar";//默认压缩方式为 .rar  
            //Process1.StartInfo.Arguments = " a -r /" + strzipPath + "/" + strtxtPath;

        }
        #endregion

        protected string GetServerPath(object source)
        {
            //获取程序根目录
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());
            string imagesurl = (source + string.Empty).Replace(tmpRootDir, "");
            return imagesurl = "/" + imagesurl.Replace(@"\", @"/");
        }

        #region  判断类型  隐藏 显示
        protected string StatuHideViewInviteInfo()
        {
            return GetTypes() == false ? "style='display:none'" : string.Empty;
        }

        protected string StatuHideViewInviteInfos()
        {
            return GetTypes() == true ? "style='display:none'" : string.Empty;
        }
        #endregion

        #region 获取类型
        public bool GetTypes()
        {
            Type = Request["Type"].ToInt32();
            if (Type == 2)      //统筹师
            {
                return true;
            }
            else if (Type == 1)     //设计师
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 点击导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        protected void Button2_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/DesignClass.xml"));

            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();

            var DataList = ObjDesignclassBLL.GetByQuotedIDs(QuotedID);

            foreach (var ObjDataItem in DataList)
            {
                string MatrialName = objMaterial.GetByID(ObjDataItem.Material.ToInt32()).MaterialName.ToString();
                string SupplierName = objSupplierBLL.GetByID(ObjDataItem.Supplier.ToInt32()).Name.ToString();

                ObjDataString.Append("<Row>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Title + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Node + "</Data></Cell>\r\n"); //
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + MatrialName + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.PurchasePrice + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Unit + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.PurchaseQuantity + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Spec + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + SupplierName + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.TotalPrice + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + "" + "</Data></Cell>\r\n");
                ObjDataString.Append("</Row>\r\n");
            }
            ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }
        #endregion


        #region 绑定完成事件 显示图片 参考图 Type 2  效果图 Type 1
        /// <summary>
        /// 图片先试试
        /// </summary>
        protected void RepDesignlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            ///参考图
            Repeater repImages = e.Item.FindControl("repShowImg") as Repeater;
            int DesignClassID = (e.Item.FindControl("HideDesignId") as HiddenField).Value.ToInt32();
            repImages.DataSource = ObjDesignUploadBLL.GetByDesignClassID(DesignClassID, 2);
            repImages.DataBind();


            ///效果图
            Repeater repShowResultImg = e.Item.FindControl("repShowResultImg") as Repeater;
            repShowResultImg.DataSource = ObjDesignUploadBLL.GetByDesignClassID(DesignClassID, 1);
            repShowResultImg.DataBind();

        }
        #endregion

        #region 点击保存事件
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btn_SaveThemes_Click(object sender, EventArgs e)
        {
            FL_DesignThemes ObjThemeModel = ObjDesignThemesBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            int result = 0;
            if (ObjThemeModel == null)      //不存在 为null  就新增
            {
                ObjThemeModel = new FL_DesignThemes();
                ObjThemeModel.Themes = txtThemes.Text;
                ObjThemeModel.ColorTone = txtColorTone.Text;
                ObjThemeModel.DesignStyle = txThemeStyle.Text;
                ObjThemeModel.CustomerID = Request["CustomerID"].ToInt32();
                ObjThemeModel.QuotedID = QuotedID;
                ObjThemeModel.CreateDate = DateTime.Now.ToString().ToDateTime();
                result = ObjDesignThemesBLL.Insert(ObjThemeModel);


            }
            else if (ObjThemeModel != null)     //否则就修改
            {
                ObjThemeModel.Themes = txtThemes.Text;
                ObjThemeModel.ColorTone = txtColorTone.Text;
                ObjThemeModel.DesignStyle = txThemeStyle.Text;
                result = ObjDesignThemesBLL.Update(ObjThemeModel);
            }
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("操作成功", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("操作失败,请稍后再试...", Page);
            }

            BinderData();
        }
        #endregion

        #region 头部主题隐藏和显示
        /// <summary>
        /// 是否隐藏 Type  1 设计师 可以填写   2 策划师没有权限填写
        /// </summary>
        /// <returns></returns>
        public string ShowOrHide()
        {
            if ((ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).EmpLoyeeID != User.Identity.Name.ToInt32()) || Type == 1 && (ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).DesignerEmployee != User.Identity.Name.ToInt32()))
            {
                return "style='display:none;'";
            }
            //else if (Type == 1 && (ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()).DesignerEmployee != User.Identity.Name.ToInt32()))
            //{
            //    return "style='display:none;'";
            //    //return "style='display:none;'";
            //}
            return "";
        }
        #endregion

        #region 确认下派
        /// <summary>
        /// 下派设计单 选择设计师
        /// </summary>
        protected void btnDispatchingConfirm_Click(object sender, EventArgs e)
        {

            int EmployeeID = hideEmpLoyeeID.Value.ToInt32();
            if (EmployeeID == -1 && txtEmpLoyee.Text != "")
            {
                Employee ObjEmployeeBLL = new Employee();
                EmployeeID = ObjEmployeeBLL.GetByName(txtEmpLoyee.Text.Trim().ToString()).EmployeeID;
            }
            FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            if (txtPlanDate.Text == "")
            {
                ObjQuotedPriceModel.PlanFinishDate = DateTime.Today.AddDays(7);
            }
            else
            {
                ObjQuotedPriceModel.PlanFinishDate = txtPlanDate.Text.ToDateTime();
            }
            ObjQuotedPriceModel.DesignerEmployee = EmployeeID;      //选择设计师
            ObjQuotedPriceModel.WorkCreateDate = DateTime.Now.ToShortDateString().ToDateTime();      //选派设计师时间
            ObjQuotedPriceModel.DesignerState = 1;                  //1 .已派设计师 0. null 未派策划师
            ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);

            //操作日志
            CreateHandle(1);
            JavaScriptTools.AlertWindowAndLocation("下派设计师成功", Page.Request.Url.ToString(), Page);
        }

        #endregion


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int Type)
        {
            int EmployeeID = hideEmpLoyeeID.Value.ToInt32();
            if (EmployeeID == -1 && txtEmpLoyee.Text != "")
            {
                Employee ObjEmployeeBLL = new Employee();
                EmployeeID = ObjEmployeeBLL.GetByName(txtEmpLoyee.Text.Trim().ToString()).EmployeeID;
            }

            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            if (Type == 1)
            {
                HandleModel.HandleContent = "婚礼统筹-设计单派单,客户姓名:" + Model.Bride + "/" + Model.Groom + ",选择设计师：" + ObjEmployeeBLL.GetByID(EmployeeID).EmployeeName;
            }
            else if (Type == 2)
            {
                HandleModel.HandleContent = "婚礼统筹-设计单下单,客户姓名:" + Model.Bride + "/" + Model.Groom + ",设计师：" + ObjEmployeeBLL.GetByID(EmployeeID).EmployeeName + ",成功下单";
            }
            else if (Type == 3)
            {
                HandleModel.HandleContent = "婚礼统筹-设计单打回,客户姓名:" + Model.Bride + "/" + Model.Groom + ",设计师：" + ObjEmployeeBLL.GetByID(EmployeeID).EmployeeName + ",成功打回设计单";
            }

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 5;     //婚礼统筹
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion

        #region 点击打回设计单
        /// <summary>
        /// 打回设计单
        /// </summary>
        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            int OrderID = Request["OrderID"].ToString().ToInt32();
            int QuotedID = Request["QuotedID"].ToString().ToInt32();
            int CustomerID = Request["CustomerID"].ToString().ToInt32();
            FL_QuotedPrice ObjQuotedPriceModel = ObjQuotedPriceBLL.GetByID(QuotedID);
            ObjQuotedPriceModel.DesignerState = 0;                  //1 .已派设计师 0. null 未派策划师
            ObjQuotedPriceBLL.Update(ObjQuotedPriceModel);

            //操作日志
            CreateHandle(3);

            string url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignCreate.aspx?OrderID=" + OrderID + "&IsFinish=4&QuotedID=" + QuotedID + "&CustomerID=" + CustomerID + "&Type=2";
            JavaScriptTools.AlertWindowAndLocation("成功打回设计单", url, Page);
        }
        #endregion
    }
}