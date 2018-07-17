using System;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class YearSelector : System.Web.UI.UserControl
    {
        public DateTime Start
        {
            get
            {
                return new DateTime(Convert.ToInt32(txtYear.Text), 1, 1);
            }
        }
        public DateTime End
        {
            get
            {
                return new DateTime(Convert.ToInt32(txtYear.Text), 12, 31);
            }
        }

        public string Text
        {
            get
            {
                return txtYear.Text;
            }
            set
            {
                txtYear.Text = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                txtYear.Text = DateTime.Now.Year.ToString();
            }
            base.OnInit(e);
        }
    }
}