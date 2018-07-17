using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesUpdate : HA.PMS.Pages.NoneStylePage
    {
        SaleSources ObjSaleSourcesBLL = new SaleSources();
        ChannelType ObjChannelTypeBLL = new ChannelType();
        Employee objEmployeeBLL = new Employee();

        protected bool IsSaleSourcePrivateOpening = false;   //指示自己录入的渠道只有自己和主管可以看见功能是否处于开启状态

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 自己录入的渠道只有自己和主管可以看见功能。

            IsSaleSourcePrivateOpening = new SysConfig().IsSaleSourcePrivateOpening(User.Identity.Name.ToInt32(), false);
            //设置控件权限
            if (IsSaleSourcePrivateOpening)
            {
                ddlMaintenanceEmployee.Enabled = false;
                ddlProlongationEmployee.Enabled = false;
            }

            #endregion

            if (!IsPostBack)
            {
                DataBinder();
                int SourceID = Request.QueryString["SourceID"].ToInt32();
                //初始化之前没有修改的信息
                GetSaleSources(SourceID);

            }
        }

        protected void DataBinder()
        {
            ddlChannelType.DataSource = ObjChannelTypeBLL.GetByAll();
            ddlChannelType.DataValueField = "ChannelTypeId";
            ddlChannelType.DataTextField = "ChannelTypeName";
            ddlChannelType.DataBind();

            var employeeList = objEmployeeBLL.GetByAll();
            //拓展人绑定
            ddlMaintenanceEmployee.DataSource = employeeList;
            ddlMaintenanceEmployee.DataValueField = "EmployeeID";
            ddlMaintenanceEmployee.DataTextField = "EmployeeName";
            ddlMaintenanceEmployee.DataBind();
            //拓展人绑定
            ddlProlongationEmployee.DataSource = employeeList;
            ddlProlongationEmployee.DataValueField = "EmployeeID";
            ddlProlongationEmployee.DataTextField = "EmployeeName";
            ddlProlongationEmployee.DataBind();
        }

        protected void GetSaleSources(int sourcesId)
        {
            HA.PMS.DataAssmblly.FD_SaleSources fd_SaleSources = ObjSaleSourcesBLL.GetByID(sourcesId);
            txtSourcename.Text = fd_SaleSources.Sourcename;


            txtProlongationDate.Text = GetShortDateString(fd_SaleSources.ProlongationDate.ToString());
            //拓展人 
            ListItem prolongionEmployee = ddlProlongationEmployee.Items.
            FindByValue(fd_SaleSources.ProlongationEmployee + string.Empty);
            if (prolongionEmployee != null)
            {
                prolongionEmployee.Selected = true;
            }
            //维护人
            ListItem maintenanceEmployee = ddlMaintenanceEmployee.Items.
            FindByValue(fd_SaleSources.MaintenanceEmployee + string.Empty);
            if (maintenanceEmployee != null)
            {
                maintenanceEmployee.Selected = true;
            }

            ddlChannelType.Items.FindByValue(fd_SaleSources.ChannelTypeId.ToString()).Selected = true;


            txtAddress.Text = fd_SaleSources.Address;
            txtSynopsis.Text = fd_SaleSources.Synopsis;
            txtPreferentialpolicy.Text = fd_SaleSources.Preferentialpolicy;
            txtRebatepolicy.Text = fd_SaleSources.Rebatepolicy;
            string needRabate = fd_SaleSources.NeedRebate == true ? "0" : "1";
            ddlNeedRebate.Items.FindByValue(needRabate).Selected = true;


            txtTactcontacts1.Text = fd_SaleSources.Tactcontacts1;
            txtTactcontactsType1.Text = fd_SaleSources.TactcontactsType1;
            txtTactcontactsJob1.Text = fd_SaleSources.TactcontactsJob1;
            txtTactcontactsPhone1.Text = fd_SaleSources.TactcontactsPhone1;
            txtEmail1.Text = fd_SaleSources.Email1;
            txtQQ1.Text = fd_SaleSources.QQ1;
            txtWeibo1.Text = fd_SaleSources.Weibo1;
            txtWenXin1.Text = fd_SaleSources.WenXin1;

            txtTactcontacts2.Text = fd_SaleSources.Tactcontacts2;
            txtTactcontactsType2.Text = fd_SaleSources.TactcontactsType2;
            txtTactcontactsJob2.Text = fd_SaleSources.TactcontactsJob2;
            txtTactcontactsPhone2.Text = fd_SaleSources.TactcontactsPhone2;
            txtEmail2.Text = fd_SaleSources.Email2;
            txtQQ2.Text = fd_SaleSources.QQ2;
            txtWeibo2.Text = fd_SaleSources.Weibo2;
            txtWenXin2.Text = fd_SaleSources.WenXin2;
            txtBankName.Text = fd_SaleSources.BankName;
            txtBankCard.Text = fd_SaleSources.BankCard;

            txtTactcontacts3.Text = fd_SaleSources.Tactcontacts3;
            txtTactcontactsType3.Text = fd_SaleSources.TactcontactsType3;
            txtTactcontactsJob3.Text = fd_SaleSources.TactcontactsJob3;
            txtTactcontactsPhone3.Text = fd_SaleSources.TactcontactsPhone3;
            txtEmail3.Text = fd_SaleSources.Email3;
            txtQQ3.Text = fd_SaleSources.QQ3;
            txtWeibo3.Text = fd_SaleSources.Weibo3;
            txtWenXin3.Text = fd_SaleSources.WenXin3;


            txtNode1.Text = fd_SaleSources.Node1;
            txtNode2.Text = fd_SaleSources.Node2;
            txtNode3.Text = fd_SaleSources.Node3;



        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int SourceID = Request.QueryString["SourceID"].ToInt32();
            HA.PMS.DataAssmblly.FD_SaleSources fd_SaleSources = ObjSaleSourcesBLL.GetByID(SourceID);
            fd_SaleSources.Sourcename = txtSourcename.Text;


            fd_SaleSources.ProlongationDate = txtProlongationDate.Text.ToDateTime();
            fd_SaleSources.ProlongationEmployee = ddlProlongationEmployee.SelectedValue.ToInt32();
            fd_SaleSources.MaintenanceEmployee = ddlMaintenanceEmployee.SelectedValue.ToInt32();
            fd_SaleSources.ChannelTypeId = ddlChannelType.SelectedValue.ToInt32();
            fd_SaleSources.Address = txtAddress.Text;
            fd_SaleSources.Synopsis = txtSynopsis.Text;
            fd_SaleSources.Preferentialpolicy = txtPreferentialpolicy.Text;
            fd_SaleSources.Rebatepolicy = txtRebatepolicy.Text;
            fd_SaleSources.NeedRebate = ddlNeedRebate.SelectedValue == "0" ? true : false;


            fd_SaleSources.Tactcontacts1 = txtTactcontacts1.Text;
            fd_SaleSources.TactcontactsType1 = txtTactcontactsType1.Text;
            fd_SaleSources.TactcontactsJob1 = txtTactcontactsJob1.Text;
            fd_SaleSources.TactcontactsPhone1 = txtTactcontactsPhone1.Text;
            fd_SaleSources.Email1 = txtEmail1.Text;
            fd_SaleSources.QQ1 = txtQQ1.Text;
            fd_SaleSources.Weibo1 = txtWeibo1.Text;
            fd_SaleSources.WenXin1 = txtWenXin1.Text;
            fd_SaleSources.BankName = txtBankName.Text;
            fd_SaleSources.BankCard = txtBankCard.Text;



            fd_SaleSources.Tactcontacts2 = txtTactcontacts2.Text;
            fd_SaleSources.TactcontactsType2 = txtTactcontactsType2.Text;
            fd_SaleSources.TactcontactsJob2 = txtTactcontactsJob2.Text;
            fd_SaleSources.TactcontactsPhone2 = txtTactcontactsPhone2.Text;
            fd_SaleSources.Email2 = txtEmail2.Text;
            fd_SaleSources.QQ2 = txtQQ2.Text;
            fd_SaleSources.Weibo2 = txtWeibo2.Text;
            fd_SaleSources.WenXin2 = txtWenXin2.Text;


            fd_SaleSources.Tactcontacts3 = txtTactcontacts3.Text;
            fd_SaleSources.TactcontactsType3 = txtTactcontactsType3.Text;
            fd_SaleSources.TactcontactsJob3 = txtTactcontactsJob3.Text;
            fd_SaleSources.TactcontactsPhone3 = txtTactcontactsPhone3.Text;
            fd_SaleSources.Email3 = txtEmail3.Text;
            fd_SaleSources.QQ3 = txtQQ3.Text;
            fd_SaleSources.Weibo3 = txtWeibo3.Text;
            fd_SaleSources.WenXin3 = txtWenXin3.Text;
            fd_SaleSources.IsDelete = false;


            fd_SaleSources.Node1 = txtNode1.Text;
            fd_SaleSources.Node2 = txtNode2.Text;
            fd_SaleSources.Node3 = txtNode3.Text;
            int result = ObjSaleSourcesBLL.Update(fd_SaleSources);
            if (result > 0)
            {
                JavaScriptTools.AlertWindowAndLocation("修改成功", "FD_SaleSourcesManager.aspx?NeedPopu=1", this.Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("修改失败,请重新尝试", this.Page);

            }
        }
    }
}