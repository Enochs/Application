using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class TitleNode
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public void Update(Sys_TitleNode ObjModel)
        {
            if (ObjEntity.Sys_TitleNode.Count() > 0)
            {

                
                ObjEntity.SaveChanges();
            }
            else
            {
                ObjEntity.Sys_TitleNode.Add(ObjModel);
                ObjEntity.SaveChanges();
            }
        }


        public Sys_TitleNode Getbyall()
        {
            return ObjEntity.Sys_TitleNode.FirstOrDefault();
        }
    }
}
