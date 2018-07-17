using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class DispatchingEmployeeManager
    {

        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        /// <summary>
        /// 根据项目获取人员统筹
        /// </summary>
        /// <returns></returns>
        public List<FL_DispatchingEmployeeManager> GetEmpLoyeeByDispachingID(int? DispachingID)
        {

            return ObjEntity.FL_DispatchingEmployeeManager.Where(C => C.DispatchingID == DispachingID).ToList();

        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_DispatchingEmployeeManager ObjectT)
        {

            ObjEntity.FL_DispatchingEmployeeManager.Add(ObjectT);
            return ObjEntity.SaveChanges();

        }

        public int Update(FL_DispatchingEmployeeManager ObjectT)
        {

            //ObjEntity.FL_DispatchingEmployeeManager.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.DeJey;
        }

        public int Delete(FL_DispatchingEmployeeManager ObjectT)
        {

            ObjEntity.FL_DispatchingEmployeeManager.Remove(ObjEntity.FL_DispatchingEmployeeManager.FirstOrDefault(C => C.DeJey == ObjectT.DeJey));
            ObjEntity.SaveChanges();
            return 1;
        }
    }
}
