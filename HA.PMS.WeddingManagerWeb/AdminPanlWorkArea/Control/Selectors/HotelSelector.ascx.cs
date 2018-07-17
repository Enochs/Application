using System;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.Selectors
{
    public partial class HotelSelector : System.Web.UI.UserControl
    {
        public bool Required { get; set; }

        public string Title { get; set; }

        public int HotelID
        {
            get
            {
                return DdlHotel.SelectedIndex > 0 ? Convert.ToInt32(DdlHotel.SelectedValue) : -1;
            }
            set
            {
                System.Web.UI.WebControls.ListItem listItem = DdlHotel.Items.FindByValue(value.ToString());
                if (listItem != null)
                {
                    DdlHotel.ClearSelection();
                    listItem.Selected = true;
                }
            }
        }

        public string HotelName
        {
            get
            {
                return DdlHotel.SelectedIndex > 0 ? DdlHotel.SelectedItem.Text : string.Empty;
            }
            set
            {
                System.Web.UI.WebControls.ListItem listItem = DdlHotel.Items.FindByText(value);
                if (listItem != null)
                {
                    DdlHotel.ClearSelection();
                    listItem.Selected = true;
                }
            }
        }

        public bool Enabled
        {
            set
            {
                DdlHotel.Enabled = value;
            }
        }

        public string CssClass
        {
            get
            {
                return DdlHotel.CssClass;
            }
            set
            {
                DdlHotel.CssClass = value;
            }
        }

        public string Style
        {
            set
            {
                DdlHotel.Attributes.Add("style", value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                var dataSource = new BLLAssmblly.FD.Hotel().GetByAll();

                if (!Required)
                {
                    dataSource.Insert(0, new DataAssmblly.FD_Hotel() { HotelID = 0, HotelName = string.Empty });
                }

                LblTitle.Text = Title;
                DdlHotel.DataSource = dataSource;
                DdlHotel.DataTextField = "HotelName";
                DdlHotel.DataValueField = "HotelID";
                DdlHotel.CssClass = CssClass;
                DdlHotel.DataBind();
            }
            base.OnInit(e);
        }
    }
}