using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseProductCreate : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategorySerch.ClearSelection();
                ddlCategorySerch.Items.Add(new ListItem("请选择", "-1"));
                ddlCategorySerch.Items[ddlCategorySerch.Items.Count - 1].Selected = true;
                DataDropDownList(ddlProjectSerch, ddlCategorySerch.SelectedValue.ToInt32(), true);

                ddlCategory.ClearSelection();
                DataDropDownList(ddlProject, ddlCategory.SelectedValue.ToInt32(), false);

                BinderData(sender, e);
            }
            else
            {
                // Page.ClientScript.RegisterStartupScript(Page.GetType(), "bb", "alert('b')", true);

            }
        }


        protected void ClearSave()
        {
            txtSourceProductName.Text =
                txtSpecifications.Text =
                txtPurchasePrice.Text =
                txtSaleOrice.Text =
                txtSourceCount.Text =
                txtUnit.Text =
                txtRemark.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl">要绑定的控件</param>
        /// <param name="ParentID"></param>
        /// <param name="IsAddSelectAll">是否添加“请选择”项到最后并选择。</param>
        protected void DataDropDownList(EditoerLibrary.CategoryDropDownList ddl, int ParentID, bool IsAddSelectAll)
        {
            ddl.Items.Clear();
            ddl.ParentID = ParentID;
            ddl.BinderByparent();
            if (IsAddSelectAll)
            {
                ddl.ClearSelection();
                ddl.Items.Add(new ListItem("请选择", "-1"));
                ddl.Items[ddl.Items.Count - 1].Selected = true;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add(ddlProjectSerch.SelectedValue.ToInt32() > 0, "Type", 2);
            objParmList.Add(!string.IsNullOrWhiteSpace(txtProductName.Text), "ProductName", txtProductName.Text.Trim(), NSqlTypes.LIKE);
            objParmList.Add(ddlCategorySerch.SelectedValue.ToInt32() > 0, "ProductCategory", ddlCategorySerch.SelectedValue.ToInt32());
            objParmList.Add(ddlProjectSerch.SelectedValue.ToInt32() > 0, "ProjectCategory", ddlProjectSerch.SelectedValue.ToInt32());

            if (Request["Vtype"].ToInt32() > 0)
            {
                objParmList.Add("VirtualType", 9);
            }
            else
            {
                //objParmList.Add("VirtualType", null, NSqlTypes.IsNull);
                objParmList.Add("Type", 2);

            }


            int resourceCount = 0;
            var query = objStorehouseSourceProductBLL.GetByWhereParameter(objParmList, "Keys", StorePager.PageSize, StorePager.CurrentPageIndex, out resourceCount);
            StorePager.RecordCount = resourceCount;
            rptStorehouse.DataBind(query);


            //var objParmList = new List<PMSParameters>();
            //objParmList.Add("State", (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingQuotedPrice + "," + (int)CustomerStates.DoingChecksQuotedPrice + "," + (int)CustomerStates.CheckQuotedPrice + "," + (int)CustomerStates.StarCarrytask + "," + (int)CustomerStates.DoingCarrytask, NSqlTypes.IN);

            //MyManager.GetEmployeePar(objParmList);
            //objParmList.Add("LastFollowDate", MainDateRanger.Start.ToString() + "," + MainDateRanger.End.ToString(), NSqlTypes.DateBetween);
            //objParmList.Add("PartyDate", MainDateRanger.Start.ToString() + "," + MainDateRanger.End.ToString(), NSqlTypes.DateBetween);

            //int startIndex = CtrPageIndex.StartRecordIndex;
            //int SourceCount = 0;
            //var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            //CtrPageIndex.RecordCount = SourceCount;
            //repCustomer.DataSource = DataList;
            //repCustomer.DataBind();
        }

        /// <summary>
        /// 保存库房产品数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Storehouse ObjStorehouseBLL = new Storehouse();
            //判断是否为本库房库管
            int HouseID = ObjStorehouseBLL.GetKeyByManager(User.Identity.Name.ToInt32());
            if (HouseID < 0)
            {
                JavaScriptTools.AlertWindow("你不是库管", Page);
                return;
            }


            if (ddlDisposible.SelectedItem == null)
            {
                JavaScriptTools.AlertWindow("请选择是否为一次性物品", Page);
                return;
            }
            FD_StorehouseSourceProduct fd_Storehouse = new FD_StorehouseSourceProduct();
            fd_Storehouse.IsDelete = false;
            //产品状态
            fd_Storehouse.ProductState = "编辑";

            fd_Storehouse.SourceProductName = txtSourceProductName.Text;
            // fd_Storehouse.PutStoreDate = txtPutStoreDates.Text.ToDateTime();
            fd_Storehouse.PutStoreDate = DateTime.Now;
            fd_Storehouse.ProductCategory = ddlCategory.SelectedValue.ToInt32();
            fd_Storehouse.ProductProject = ddlProject.SelectedValue.ToInt32();
            fd_Storehouse.Specifications = txtSpecifications.Text;
            fd_Storehouse.Remark = txtRemark.Text;
            fd_Storehouse.PurchasePrice = txtPurchasePrice.Text.ToDecimal();
            fd_Storehouse.SaleOrice = txtSaleOrice.Text.ToDecimal();
            fd_Storehouse.SourceCount = txtSourceCount.Text.ToInt32();
            fd_Storehouse.Unit = txtUnit.Text;
            fd_Storehouse.OperCount = 0;

            if (ddlDisposible.SelectedItem.Text == "虚拟")
            {
                fd_Storehouse.Unit += "VirtualProduct";
            }

            //所属库房
            fd_Storehouse.StorehouseID = HouseID;

            //所属仓位
            fd_Storehouse.Position = txtPosition.Text;
            //产品属性
            if (ddlDisposible.SelectedItem.Text == "一次性")
            {
                fd_Storehouse.IsDisposible = true;
            }
            else
            {
                fd_Storehouse.IsDisposible = false;
            }
            //这里库房只有一个，所以默认为 库房ID 为 0
            fd_Storehouse.StorehouseSourceID = 0;
            string savaPath = "~/Files/Storehouse/";
            string fileExt = "";
            bool isOk = true;
            if (flieUp.HasFile)
            {

                fileExt = System.IO.Path.GetExtension(flieUp.FileName).ToLower();
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {

                    fd_Storehouse.Data = savaPath + DateTime.Now.ToFileTimeUtc() + fileExt;
                    if (!Directory.Exists(Server.MapPath(savaPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(savaPath));
                    }
                    flieUp.SaveAs(Server.MapPath(fd_Storehouse.Data));
                }
                else
                {
                    isOk = false;
                    JavaScriptTools.AlertWindow("请你上传的图片只能是jpeg png jpg gif bmp", this.Page);

                }
            }

            if (ddlProject.Items.Count == 0)
            {
                isOk = false;
                JavaScriptTools.AlertWindow("项目不能为空", this.Page);
            }
            if (isOk)
            {
                int result = objStorehouseSourceProductBLL.Insert(fd_Storehouse);
                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("添加成功", this.Page);
                    BinderData(sender, e);
                    ClearSave();
                }
                else
                {
                    JavaScriptTools.AlertWindow("添加失败,请重新尝试", this.Page);

                }
            }

        }

        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDataDisPlay(object source)
        {
            if (source.ToString().Length > 0)
            {

                string filePath = "../../.." + source.ToString().ToLower().Replace("~", "");
                string fileExt = Path.GetExtension(filePath);
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {
                    return "<a class='grouped_elements'   href='#' rel='group1'><img style='width:100px; height:70px;' alt='' src='" + filePath + "' /> </a>";
                }


            }
            return "";
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList(ddlProject, ddlCategory.SelectedValue.ToInt32(), false);
        }
        /// <summary>
        /// 单个文件进行下载
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        public void DownLoad(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            //防止中文出现乱码
            string filename = HttpUtility.UrlEncode(fileInfo.Name);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.WriteFile(path);

            Response.End();
        }

        protected void rptStorehouse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            FD_StorehouseSourceProduct storehouse = objStorehouseSourceProductBLL.GetByID((e.Item.DataItem as FD_AllProducts).KindID);
            if (storehouse != null)
            {
                LinkButton linkButton = (LinkButton)e.Item.FindControl("lkbtnDownLoad");
                Image imgStore = (Image)e.Item.FindControl("imgStore");
                if (!string.IsNullOrEmpty(storehouse.Data))
                {
                    string filePath = "../../.." + storehouse.Data.ToLower().Replace("~", "");
                    string fileExt = Path.GetExtension(filePath);

                    if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                    {
                        imgStore.Visible = true;
                        linkButton.Visible = false;
                    }
                    else
                    {
                        linkButton.Text = Path.GetFileName(storehouse.Data);
                        imgStore.Visible = false;
                        linkButton.Visible = true;
                    }
                }
                else
                {
                    imgStore.Visible = false;
                    linkButton.Visible = false;
                }
            }

        }

        protected void rptStorehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DownLoad")
            {
                string filePath = objStorehouseSourceProductBLL.GetByID((e.CommandArgument + string.Empty)
                .ToInt32()).Data;
                filePath = Server.MapPath(filePath);
                DownLoad(filePath);
            }
            else
            {
                FD_StorehouseSourceProduct fd = objStorehouseSourceProductBLL.GetByID((e.CommandArgument + string.Empty).ToInt32());

                if (File.Exists(Server.MapPath(fd.Data)))
                {
                    File.Delete(Server.MapPath(fd.Data));
                }
                int result = objStorehouseSourceProductBLL.Delete(fd);

                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", this.Page);
                    BinderData(source, e);

                }
                else
                {
                    JavaScriptTools.AlertWindow("删除失败,请重新尝试", this.Page);

                }
            }
        }


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/ProductList.xml"));

            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();


            var objParmList = new List<PMSParameters>();
            objParmList.Add(ddlProjectSerch.SelectedValue.ToInt32() > 0, "Type", 2);
            objParmList.Add(!string.IsNullOrWhiteSpace(txtProductName.Text), "ProductName", txtProductName.Text.Trim(), NSqlTypes.LIKE);
            objParmList.Add(ddlCategorySerch.SelectedValue.ToInt32() > 0, "ProductCategory", ddlCategorySerch.SelectedValue.ToInt32());
            objParmList.Add(ddlProjectSerch.SelectedValue.ToInt32() > 0, "ProjectCategory", ddlProjectSerch.SelectedValue.ToInt32());

            if (Request["Vtype"].ToInt32() > 0)
            {
                objParmList.Add("VirtualType", 9);
            }
            else
            {
                objParmList.Add("VirtualType", null, NSqlTypes.IsNull);

            }


            int resourceCount = 0;
            var ItemList = objStorehouseSourceProductBLL.GetByWhereParameter(objParmList, "Keys", 15000, StorePager.CurrentPageIndex, out resourceCount);

            foreach (var ObjDataItem in ItemList)
            {

                ObjDataString.Append("<Row>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.ProductName + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetCategoryName(ObjDataItem.ProductCategory) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetCategoryName(ObjDataItem.Productproperty) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Specifications + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.PurchasePrice + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.SalePrice + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Count + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Unit + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Position + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                ObjDataString.Append("</Row>\r\n");

            }
            ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");



        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            StorePager.CurrentPageIndex = 1;
            BinderData(sender, e);
        }


        /// <summary>
        /// 根据类别绑定项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCategorySerch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList(ddlProjectSerch, ddlCategorySerch.SelectedValue.ToInt32(), true);
        }

        protected void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        protected string GetIsDisposibleStatu(object IsDisposible)
        {
            string temp = string.Empty;
            if (IsDisposible != null)
            {
                temp = IsDisposible.ToString().ToLower();
                if (temp.Equals("true"))
                { return "是"; }
                else { return "否"; }
            }
            else { return string.Empty; }
        }

    }
}