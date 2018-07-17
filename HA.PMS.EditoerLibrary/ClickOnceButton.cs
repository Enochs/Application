using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace HA.PMS.EditoerLibrary
{
    /// <summary>
    /// 表示一个防止重复提交的按钮。当用户单击按钮以后，该按钮变灰，不能再次单击，直到重新加载页面或者跳转。
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ClickOnceButton runat=server></{0}:ClickOnceButton>")]
    public class ClickOnceButton : System.Web.UI.WebControls.Button
    {
        /// <summary>
        /// 默认的构造函数。
        /// </summary>
        public ClickOnceButton()
        {
            this.ViewState["afterSubmitText"] = "正在提交，请稍候...";
            base.Text = "ClickOnceButton";
            this.ViewState["showMessageBox"] = false;
            this.ViewState["warningText"] = "确定要提交吗？";
        }

        /// <summary>
        /// 获取或设置单击按钮后，按钮上所显示的文本。
        /// </summary>
        [Bindable(true),
        Category("Appearance"),
        DefaultValue("正在提交，请稍候..."),
        Description("指示单击提交后，按钮上所显示的文本。")]
        public string AfterSubmitText
        {
            get
            {
                string afterSubmitText = (string)this.ViewState["afterSubmitText"];
                if (afterSubmitText != null)
                {
                    return afterSubmitText;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["afterSubmitText"] = value;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        Description("指示是否要显示一个提示框。")]
        public bool ShowMessageBox
        {
            get
            {
                return (bool)this.ViewState["showMessageBox"];
            }
            set
            {
                this.ViewState["showMessageBox"] = value;
            }
        }


        [Bindable(true),
        Category("Appearance"),
        DefaultValue("确定要提交吗？"),
        Description("指示提示框内所包含的内容。")]
        public string WarningText
        {
            get
            {
                return (string)this.ViewState["warningText"];
            }
            set
            {
                this.ViewState["warningText"] = value;
            }
        }

        /// <summary>
        /// AddAttributesToRender
        /// </summary>
        /// <param name="writer">HtmlTextWriter</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            System.Text.StringBuilder ClientSideEventReference = new System.Text.StringBuilder();

            if (((this.Page != null) && this.CausesValidation) && (this.Page.Validators.Count > 0))
            {
                ClientSideEventReference.Append("if (typeof(Page_ClientValidate) == 'function'){if (Page_ClientValidate() == false){return false;}}");
            }
            //ShowMessageBox?
            if (this.ShowMessageBox)
            {
                ClientSideEventReference.Append("if (!confirm('" + this.WarningText + "')){return false}");
            }
            ClientSideEventReference.AppendFormat("this.value = '{0}';", (string)this.ViewState["afterSubmitText"]);
            ClientSideEventReference.Append("this.disabled = true;");
            ClientSideEventReference.Append(this.Page.ClientScript.GetPostBackEventReference(this, string.Empty));


            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, ClientSideEventReference.ToString(), true);
            base.AddAttributesToRender(writer);
        }
    }
}