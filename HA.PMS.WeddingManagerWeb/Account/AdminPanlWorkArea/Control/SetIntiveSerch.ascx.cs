using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SetIntiveSerch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Response.Write(ddlCustomersState1.SelectedValue);
            }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<ObjectParameter> ObjParList = new List<ObjectParameter>();


        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryConditions
        {
            get;
            set;
        }

        /// <summary>
        /// 重写优先执行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            //if (IsPostBack)
            //{
            //    this.ObjParList.Add(new ObjectParameter("ChannelType", ddlChannelType1.SelectedValue.ToInt32()));
            //    //this.ObjParList.Add(new ObjectParameter("PartyDate_between", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime()));
            //    this.ObjParList.Add(new ObjectParameter("State", ddlCustomersState1.SelectedValue.ToInt32()));
            //}
        }





        /// <summary>
        /// 构造参数
        /// </summary>
        public void GetParaList()
        {

            ////渠道类型
            //if (DdlChannelType2.SelectedValue != "-1")
            //{
            //    this.ObjParList.Add(new ObjectParameter("ChannelType", DdlChannelType2.SelectedValue.ToInt32()));
            //}

            //婚期
            if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                this.ObjParList.Add(new ObjectParameter("PartyDate_between", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToDateTime()));
            }
            else
            {
                if (txtStar.Text != string.Empty)
                {
                    this.ObjParList.Add(new ObjectParameter("PartyDate", txtStar.Text.ToDateTime()));
                }
                if (txtEnd.Text != string.Empty)
                {
                    this.ObjParList.Add(new ObjectParameter("PartyDate_between", txtEnd.Text.ToDateTime()));
                }
            }


            //新人状态
            if (ddlCustomersState1.SelectedValue != "-1")
            {
                this.ObjParList.Add(new ObjectParameter("State", ddlCustomersState1.SelectedValue.ToInt32()));
            }

        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }


        /// <summary>
        /// 绑定渠道联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChannelname_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlreferrr.BinderbyChannel(ddlChannelname.SelectedValue.ToInt32());
        }


    }
}