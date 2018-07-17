using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.FD
{

    public class QuotedProduct : ICRUDInterface<FD_QuotedProduct>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
        public int Delete(FD_QuotedProduct ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FD_QuotedProduct> GetByAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据类型ID获取所包含的产品
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public FD_QuotedProduct GetByQcKey(int Key)
        {
            var ObjModel = ObjEntity.FD_QuotedProduct.FirstOrDefault(C => C.QCKey == Key);
            if (ObjModel == null)
            {
                ObjModel = new FD_QuotedProduct();
                ObjModel.QCKey = Key;
                ObjModel.Keys = string.Empty;
                Insert(ObjModel);
                return ObjEntity.FD_QuotedProduct.FirstOrDefault(C => C.QCKey == Key);
            }
            else
            {
                return ObjModel;
            }
        }

        public FD_QuotedProduct GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }


        public List<FD_QuotedProduct> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        public int Insert(FD_QuotedProduct ObjectT)
        {

            ObjEntity.FD_QuotedProduct.Add(ObjectT);

            return ObjEntity.SaveChanges();
        }

        public int Update(FD_QuotedProduct ObjectT)
        {
            return ObjEntity.SaveChanges();
        }
    }
}
