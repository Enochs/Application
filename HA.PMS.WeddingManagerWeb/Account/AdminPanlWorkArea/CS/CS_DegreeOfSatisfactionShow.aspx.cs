using HA.PMS.BLLAssmblly.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_DegreeOfSatisfactionShow : SystemPage
    {
        DegreeOfSatisfaction ObjDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["DofKey"].ToInt32() != 0)
                {
                    var DataSource = ObjDegreeOfSatisfactionBLL.GetDegreeOfSatisfactionByDofID(Request["DofKey"].ToInt32());
                    if (DataSource != null)
                    {
                        var ObjUpdateModel = ObjDegreeOfSatisfactionBLL.GetByID(Request["DofKey"].ToInt32());
                        if (DataSource.Count > 0)
                        {
                            lblDegreeResult.Text = DataSource[0].DofContent;
                            lblDofContent.Text = DataSource[0].SumDof;
                            this.repItemList.DataSource = DataSource;
                            this.repItemList.DataBind();

                        }
                        else
                        {
                            var objModel = ObjDegreeOfSatisfactionBLL.GetByID(Request["DofKey"].ToInt32());
                            lblDegreeResult.Text = objModel.DofContent;
                            lblDofContent.Text = objModel.SumDof;
                        }
                    }
                }
            }
        }

        //protected void btnSaveChange_Click(object sender, EventArgs e)
        //{
        //    DegreeOfSatisfaction ObjDegreeOfSatisfactionContentBLL = new DegreeOfSatisfaction();
        //    var ObjUpdateModel = ObjDegreeOfSatisfactionContentBLL.GetByID(Request["DofKey"].ToInt32());
        //    ObjUpdateModel.DegreeResult = txthandle.Text;
        //    ObjDegreeOfSatisfactionContentBLL.Update(ObjUpdateModel);
        //    JavaScriptTools.AlertWindow("保存成功!",Page);
        //}
    }
}