using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;
using System.Data;
using System.Data.Objects;
namespace HA.PMS.Pages
{
    public class SystemPageDataTools
    {
        /// <summary>
        /// 根据权获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IQueryable<T> GetDataByPower<T>(T obj) where T : class
        {
         

            ObjectContext ObjDataContext = new ObjectContext("");
            ObjectParameter ObjParameter = new ObjectParameter("id", 1);
         
            return ObjDataContext.CreateQuery<T>("  ", ObjParameter);

            //ObjectParameter op = new ObjectParameter("id", 1);
            //oc.CreateQuery<生成EntityFramework的类>("select value c from Model1Container.CategoryTypeSet as c where c.Id=@id", op);
        }
    }

}
