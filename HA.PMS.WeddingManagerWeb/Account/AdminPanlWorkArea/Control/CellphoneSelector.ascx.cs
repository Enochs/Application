using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class CellphoneSelector : System.Web.UI.UserControl
    {
        string PhoneType = "";

        private string text;

        public string Text
        {
            get { return txtCellPhone.Text.Trim(); }
            set { text = txtCellPhone.Text.Trim().ToString(); }
        }

        private string title;

        public string Title
        {
            get { return lblTitles.Text.Trim(); }
            set { title = lblTitles.Text.Trim().ToString(); }
        }

        public string selectedValue;
        public string SelectedValue
        {
            get
            {
                return ddlPhoneTypes.SelectedValue;
            }
            set
            {
                ddlPhoneTypes.ClearSelection();
                ddlPhoneTypes.SelectedValue = value;
            }
        }

        public void AppandTo(List<PMSParameters> Sources)
        {
            if (!string.IsNullOrWhiteSpace(txtCellPhone.Text))
            {
                //source.Add(mapping[ddlNameType.SelectedValue], txtName.Text,NSqlTypes.LIKE);
                Sources.Add(PhoneType, txtCellPhone.Text.Trim(), NSqlTypes.LIKE);
            }
        }

        #region 该控件加载
        /// <summary>
        /// 加载事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (ddlPhoneTypes.SelectedValue.ToInt32() == 1)
            {
                PhoneType = "BrideCellPhone";
                txtCellPhone.ToolTip = "新娘联系电话";
            }
            else if (ddlPhoneTypes.SelectedValue.ToInt32() == 2)
            {
                PhoneType = "GroomCellPhone";
                txtCellPhone.ToolTip = "新郎联系电话";
            }

        }
        #endregion
    }
}