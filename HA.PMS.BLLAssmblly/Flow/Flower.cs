using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HA.PMS.BLLAssmblly.Flow
{
    public class Flower : ICRUDInterface<FL_FlowerCost>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_FlowerCost ObjectT)
        {
            ObjEntity.FL_FlowerCost.Remove(ObjectT);
            return ObjEntity.SaveChanges();
        }

        public List<FL_FlowerCost> GetByAll()
        {
            return ObjEntity.FL_FlowerCost.ToList();
        }



        /// <summary>
        /// 根据订单获取花艺数据
        /// </summary>
        /// <param name="DispatchingID"></param>
        /// <returns></returns>
        public List<FL_FlowerCost> GetByDispatchingID(int DispatchingID)
        {
            return ObjEntity.FL_FlowerCost.Where(C => C.DispatchingID == DispatchingID && C.BuyType != null).ToList();
        }


        public FL_FlowerCost GetByID(int? KeyID)
        {
            return ObjEntity.FL_FlowerCost.FirstOrDefault(C => C.Flowerkey == KeyID);
        }

        public List<FL_FlowerCost> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        public int Insert(FL_FlowerCost ObjectT)
        {
            List<PMSParameters> pars = new List<PMSParameters>();
            pars.Add("FLowername", ObjectT.FLowername.ToString(), NSqlTypes.StringEquals);
            pars.Add("DispatchingID", ObjectT.DispatchingID.ToString(), NSqlTypes.Equal);

            if (PublicDataTools<FL_FlowerCost>.IsExists(pars) == true)      //已经存在  该项 不能新增
            {
                return 0;
            }
            else
            {
                ObjEntity.FL_FlowerCost.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }

        }

        public int Update(FL_FlowerCost ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
    }
}
