using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.CS;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace HA.PMS.Pages
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserCheckLoginAttribute : Attribute
    {
        public UserCheckLoginAttribute() 
        {
          
         
            
        }

    
    }
}
