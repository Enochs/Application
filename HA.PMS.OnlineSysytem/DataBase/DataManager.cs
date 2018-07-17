
using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem.DataBase
{
    public class DataManager
    {
    //    SqlConnection Objconn = new SqlConnection("Data Source=.;Initial Catalog=PMS_Wedding;User ID=sa;Password=sasa;");

        public void BinderData()
        {
            using (EntityConnection ObjEntityconn = new EntityConnection("name=PMS_WeddingEntities"))
            {
                ObjectContext ObjDataContext = new ObjectContext(ObjEntityconn);
                // strWhere = "it.JION_TIME<datetime'" + t2 + "'";
               // ObjList = ObjDataContext.CreateQuery<T>("Select VALUE c from PMS_WeddingEntities." + ObjEntity.GetType().Name + " as c  " + GetWhere, ObjParameterList).ToList<T>();
            }
        }
     
    }
    
}
