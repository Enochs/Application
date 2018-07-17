using HA.PMS.OnlineSysytem.DataBaseAutoUpdate;
using HA.PMS.OnlineSysytem.Serverice;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.OnlineSysytem.DataBase
{
    public class Mission
    {
        OnlineSysytem.PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public void MissionTimerEnd()
        {
            while (true)
            {
                var ObjList = ObjEntity.FL_MissionDetailed.Where(C => C.IsOver == false && C.Countdown > 0).ToList();
                foreach (var Objitem in ObjList)
                {
                    Objitem.Countdown = Objitem.Countdown - 1;
                    ObjEntity.SaveChanges();
                    System.Threading.Thread.Sleep(150);
                }

                System.Threading.Thread.Sleep(84400000);
            }
             
        }
    }
}
