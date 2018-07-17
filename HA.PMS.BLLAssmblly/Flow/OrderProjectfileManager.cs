using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using System.Data.Objects;

namespace HA.PMS.BLLAssmblly.Flow
{
    public class OrderProjectfileManager:ICRUDInterface<FL_OrderProjectfileManager>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FL_OrderProjectfileManager ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderProjectfileManager> GetByAll()
        {
            throw new NotImplementedException();
        }

        public FL_OrderProjectfileManager GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }

        public List<FL_OrderProjectfileManager> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<FL_OrderProjectfileManager> GetOrderProjectfileManagerIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;

            var DataSource = PublicDataTools<FL_OrderProjectfileManager>.GetDataByParameter(new FL_OrderProjectfileManager(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<FL_OrderProjectfileManager>();
            }
            return PageDataTools<FL_OrderProjectfileManager>.AddtoPageSize(DataSource);

        }

        /// <summary>
        /// 添加初案
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_OrderProjectfileManager ObjectT)
        {
            ObjEntity.FL_OrderProjectfileManager.Add(ObjectT);
            ObjEntity.SaveChanges();
            return ObjectT.FileID;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_OrderProjectfileManager ObjectT)
        {
            throw new NotImplementedException();
        }
    }
}
