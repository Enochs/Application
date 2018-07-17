#region 业务逻辑层工厂类
/*若模型（数据表）固定，应修改为静态方法以提升效率。
 */
#endregion

using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System;
using System.Linq;

namespace HA.PMS.BLLAssmblly
{
    public sealed class RepositoryFactory
    {
        private static System.Data.Entity.DbContext _dbContext;

        public static System.Data.Entity.DbContext DbContext
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }

        public static IEFRepository<T> CreateRepository<T>() where T : class,new()
        {
            //if (_dbContext == null)
            //{
            //    _dbContext = new PMS_WeddingEntities();
            //}
            //return new Repositoy<T>(_dbContext);
            return new Repositoy<T>();
        }
    }
}
