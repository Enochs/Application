using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class CstmNameSelector : System.Web.UI.UserControl
    {
        string NameType = "";
        private System.Collections.Generic.SortedList<string, string> mapping = new SortedList<string, string>();

        public System.Collections.Generic.SortedList<string, string> Mapping
        {
            get { return mapping; }
            set { mapping = value; }
        }

        /// <summary>
        /// 获取或设置文本框中的内容。
        /// </summary>
        public string Text
        {
            get
            {
                return txtName.Text.Trim();
            }
            set
            {
                txtName.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        public bool Enable 
        {
            set
            {
                ddlNameType.Enabled = txtName.Enabled = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return txtName.MaxLength;
            }
            set
            {
                txtName.MaxLength = value;
            }
        }

        /// <summary>
        /// 获取或设置列表控件中选定的值。1 表示新郎，2 表示新娘
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return ddlNameType.SelectedValue;
            }
            set
            {
                ddlNameType.ClearSelection();
                ddlNameType.SelectedValue = value;
            }
        }

        /// <summary>
        /// 获取或设置列表控件中选定项的索引。
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return ddlNameType.SelectedIndex;
            }
            set
            {
                ddlNameType.ClearSelection();
                ddlNameType.SelectedIndex = value;
            }
        }

        public void AddItem(string text, string value)
        {
            ddlNameType.Items.Add(new ListItem(text, value));
            Mapping.Add(value, text);
        }

        public void RemoveItem(string value)
        {
            ListItem item = ddlNameType.Items.FindByValue(value);
            if (!object.ReferenceEquals(item,null))
            {
                ddlNameType.Items.Remove(item);
                Mapping.Remove(value);
            }
        }


        //public List<ObjectParameter> GetEmployeePar(List<ObjectParameter> ObjKeyParList)
        //{

        //    //是否按照责任人查
        //    if (EmployeeID != 0)
        //    {
        //        ObjKeyParList.Add(new System.Data.Objects.ObjectParameter("EmployeeID", EmployeeID));
        //    }
        //    else
        //    {
        //        ObjKeyParList.Add(new System.Data.Objects.ObjectParameter("EmpLoyeeID", int.Parse(Request.Cookies["HAEmployeeID"].Value)));
        //    }
        //    return ObjKeyParList;
        //}
        public void AppandTo(List<PMSParameters> source)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                //source.Add(mapping[ddlNameType.SelectedValue], txtName.Text,NSqlTypes.LIKE);
                source.Add(NameType, txtName.Text, NSqlTypes.LIKE);
            }
        }

        public void AppandTo(List<ObjectParameter> source)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                //source.Add(mapping[ddlNameType.SelectedValue], txtName.Text,NSqlTypes.LIKE);
                source.Add(NameType, txtName.Text, NSqlTypes.LIKE);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Mapping.Add("1", "Bride_LIKE");
            //Mapping.Add("2", "Groom_LIKE");
            if (ddlNameType.SelectedValue == "1")
            {
                NameType = "Bride";
            }
            else
            {
                NameType = "Groom";
            }
        }
    }
}