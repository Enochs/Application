using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wuqi.Webdiyer;

namespace HA.PMS.ToolsLibrary
{
    public class AspNetPagerTool : AspNetPager
    {
        public AspNetPagerTool()
        {
            this.PageSize = 10;
            this.AlwaysShow = true;
            //this.CustomInfoHTML = "Page  <font color=\"red\"><b>" + this.CurrentPageIndex + "</b></font> of  " + this.PageCount;
            //this.CustomInfoHTML += "&nbsp;&nbsp;Orders " + this.StartRecordIndex + "-" + this.EndRecordIndex;
            //this.CustomInfoHTML = "当前第" + this.CurrentPageIndex + "/" + this.PageCount + "页 共" + RecordCount + "条记录 每页" + PageSize + "条";
            this.PrevPageText = "上一页";
            this.NextPageText = "下一页";
            this.FirstPageText = "首页";
            this.LastPageText = "末页";

            this.SubmitButtonClass = "btn btn-danger";
            this.CssClass = "paginator";
            this.CustomInfoHTML = "当前第" + this.CurrentPageIndex + "/" + this.PageCount + "页 共" + RecordCount + "条记录 每页" + PageSize + "条";
        }
    }
}
