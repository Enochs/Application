using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_QuotedCatgoryList : SystemPage
    {
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }

        }
        #endregion

        #region 项目  产品的排版 (空格)
        /// <summary>
        /// 排版
        /// </summary>
        public string GetItemNbsp(object ItemLevel)
        {
            if (ItemLevel != null)
            {

                int Count = ItemLevel.ToString().ToInt32();
                string Nbsp = "";
                if (Count == 1)
                {
                    return string.Empty;
                }
                for (int i = 0; i < Count; i++)
                {
                    Nbsp += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                return Nbsp;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            repQuotedCatogry.DataSource = ObjQuotedCatgoryBLL.GetByAll();
            repQuotedCatogry.DataBind();

            GetState();
        }
        #endregion

        #region 绑定事件 排序
        /// <summary>
        /// 绑定事件
        /// </summary>  
        protected void repQuotedCatogry_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int result = 0;
            switch (e.CommandName.ToString())
            {
                case "up":

                    //本级别
                    var ThisModel = ObjQuotedCatgoryBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    if (ThisModel.Parent != 0)
                    {
                        int MinSortOrder = ObjQuotedCatgoryBLL.GetByLevelParent(ThisModel.Parent, 2).Min(C => C.SortOrder).ToInt32();
                        if (ThisModel.SortOrder.ToInt32() == MinSortOrder)     //既然改项目的第一个产品  就说明就不能再向上了
                        {
                            JavaScriptTools.AlertWindow("该产品已经是该项目首项,不能执行此功能", Page);
                        }
                        else
                        {

                            var UpdateModel = ObjQuotedCatgoryBLL.GetByID((repQuotedCatogry.Items[(e.Item.ItemIndex - 1)].FindControl("btnUp") as LinkButton).CommandArgument.ToString().ToInt32());
                            string UpSortorder = UpdateModel.SortOrder;

                            UpdateModel.SortOrder = ThisModel.SortOrder;
                            ObjQuotedCatgoryBLL.Update(UpdateModel);


                            ThisModel.SortOrder = UpSortorder;
                            ObjQuotedCatgoryBLL.Update(ThisModel);
                        }
                    }
                    else if (ThisModel.Parent == 0)
                    {
                        var DataList = ObjQuotedCatgoryBLL.GetByParentID(0);
                        var UpModel = DataList.Where(C => C.SortOrder.ToInt32() < ThisModel.SortOrder.ToInt32()).OrderByDescending(C => C.SortOrder).ToList().Take(1).FirstOrDefault();
                        if (UpModel == null || UpModel == ThisModel)      //既然已经是第一个了  就说明就不能再向上了
                        {
                            JavaScriptTools.AlertWindow("该项目已经是报价单首项,不能执行此功能", Page);
                        }
                        else
                        {
                            string UpSortOrder = UpModel.SortOrder;
                            //修改向上/当前项的产品
                            var DataLists = ObjQuotedCatgoryBLL.GetByParentID(ThisModel.QCKey);
                            foreach (var item in DataLists)
                            {
                                item.SortOrder = item.SortOrder.Replace(ThisModel.SortOrder.ToString(), UpModel.SortOrder);
                                ObjQuotedCatgoryBLL.Update(item);
                            }

                            //修改向下产品
                            var DataListe = ObjQuotedCatgoryBLL.GetByParentID(UpModel.QCKey);
                            foreach (var item in DataListe)
                            {
                                item.SortOrder = item.SortOrder.Replace(UpModel.SortOrder.ToString(), ThisModel.SortOrder);
                                ObjQuotedCatgoryBLL.Update(item);
                            }

                            UpModel.SortOrder = ThisModel.SortOrder;
                            ObjQuotedCatgoryBLL.Update(UpModel);


                            ThisModel.SortOrder = UpSortOrder;
                            ObjQuotedCatgoryBLL.Update(ThisModel);
                        }
                    }


                    break;
                case "down":
                    //本级别
                    var HereModel = ObjQuotedCatgoryBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    if (HereModel.Parent != 0)          //说明属于2级产品
                    {
                        int MaxSortOrder = ObjQuotedCatgoryBLL.GetByLevelParent(HereModel.Parent, 2).Max(C => C.SortOrder).ToInt32();
                        if (HereModel.SortOrder.ToInt32() == MaxSortOrder)     //既然改项目的最后一个产品  就说明就不能再向下了
                        {
                            JavaScriptTools.AlertWindow("该产品已经是该项目最后一项,不能执行此功能", Page);
                        }
                        else
                        {
                            var DownModel = ObjQuotedCatgoryBLL.GetByID((repQuotedCatogry.Items[(e.Item.ItemIndex + 1)].FindControl("btnFlow") as LinkButton).CommandArgument.ToString().ToInt32());
                            string DownSortorder = DownModel.SortOrder;

                            DownModel.SortOrder = HereModel.SortOrder;
                            ObjQuotedCatgoryBLL.Update(DownModel);


                            HereModel.SortOrder = DownSortorder;
                            ObjQuotedCatgoryBLL.Update(HereModel);
                        }
                    }
                    else if (HereModel.Parent == 0)
                    {
                        var DataList = ObjQuotedCatgoryBLL.GetByParentID(0);
                        var DownModel = DataList.Where(C => C.SortOrder.ToInt32() > HereModel.SortOrder.ToInt32()).OrderBy(C => C.SortOrder).ToList().Take(1).FirstOrDefault();
                        if (DownModel == null)      //既然已经是最后一个了  就说明就不能再向下了
                        {
                            JavaScriptTools.AlertWindow("该项目已经是报价单最后一项,不能执行此功能", Page);
                        }
                        else
                        {
                            string DownSortorder = DownModel.SortOrder;

                            //修改向下/当前项的产品
                            var DataLists = ObjQuotedCatgoryBLL.GetByParentID(HereModel.QCKey);
                            foreach (var item in DataLists)
                            {
                                item.SortOrder = item.SortOrder.Replace(HereModel.SortOrder.ToString(), DownModel.SortOrder);
                                ObjQuotedCatgoryBLL.Update(item);
                            }

                            //修改向上产品
                            var DataListe = ObjQuotedCatgoryBLL.GetByParentID(DownModel.QCKey);
                            foreach (var item in DataListe)
                            {
                                item.SortOrder = item.SortOrder.Replace(DownModel.SortOrder.ToString(), HereModel.SortOrder);
                                ObjQuotedCatgoryBLL.Update(item);
                            }

                            DownModel.SortOrder = HereModel.SortOrder;
                            ObjQuotedCatgoryBLL.Update(DownModel);


                            HereModel.SortOrder = DownSortorder;
                            ObjQuotedCatgoryBLL.Update(HereModel);



                        }
                    }
                    break;
                case "Forbidden":     //禁用按钮
                    int QCKey = e.CommandArgument.ToString().ToInt32();
                    var Model = ObjQuotedCatgoryBLL.GetByID(QCKey);
                    Model.IsDelete = true;
                    result = ObjQuotedCatgoryBLL.Update(Model);
                    break;
                case "Start":         //启用按钮
                    int ID = e.CommandArgument.ToString().ToInt32();
                    var QuotedModel = ObjQuotedCatgoryBLL.GetByID(ID);
                    QuotedModel.IsDelete = false;
                    result = ObjQuotedCatgoryBLL.Update(QuotedModel);
                    break;

            }
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("修改成功", Page);
                //Response.Redirect("FD_QuotedCatgoryList.aspx");
                this.Page.Response.Redirect(this.Page.Request.Url.ToString());
            }

            //ObjQuotedCatgoryBLL.DeleteByID(e.CommandArgument.ToString().ToInt32());
            BinderData();
        }
        #endregion

        #region 禁用/启用的显示/隐藏
        /// <summary>
        /// 启用/禁用
        /// </summary>
        public void GetState()
        {
            for (int i = 0; i < repQuotedCatogry.Items.Count; i++)
            {
                var ObjItem = repQuotedCatogry.Items[i];
                LinkButton lbtnForbidden = ObjItem.FindControl("lbtnForbidden") as LinkButton;      //禁用
                LinkButton lbtnStart = ObjItem.FindControl("lbtnStart") as LinkButton;              //启用

                int QCKey = lbtnForbidden.CommandArgument.ToInt32();
                var Model = ObjQuotedCatgoryBLL.GetByID(QCKey);

                if (Model.IsDelete == false)     //启用状态  可以禁用 不能启用
                {
                    lbtnForbidden.Visible = true;
                    lbtnStart.Visible = false;
                }
                else if (Model.IsDelete == true)        //禁用状态  可以启用 不能禁用
                {
                    lbtnForbidden.Visible = false;
                    lbtnStart.Visible = true;
                }
            }
        }
        #endregion
    }
}