using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.SystemConfig
{
    public partial class QuotedPrintTitleSet :SystemPage
    {
        TitleNode ObjTitleNodeBLL = new TitleNode();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ObjItem= ObjTitleNodeBLL.Getbyall();
                if (ObjItem != null)
                {
                    txtTop.Text = ObjItem.NodeTop;
                    txtDown.Text = ObjItem.NodeButtom;
                }
            }
        }

   

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var ObjItem = ObjTitleNodeBLL.Getbyall();
            if (ObjItem != null)
            {
                ObjItem.NodeButtom = txtDown.Text;
                ObjItem.NodeTop = txtTop.Text;
                ObjTitleNodeBLL.Update(ObjItem);
            }
            else
            {
                ObjItem = new DataAssmblly.Sys_TitleNode();
                ObjItem.NodeButtom = txtDown.Text;
                ObjItem.NodeTop = txtTop.Text;
                ObjTitleNodeBLL.Update(ObjItem);
            }
        }
    }
}