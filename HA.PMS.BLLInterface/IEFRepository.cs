#region 数据操作接口（增加、删除、修改、查询）
/*基于 EntityFramework
 */
#endregion

using System.Linq;

namespace HA.PMS.BLLInterface
{
    public interface IEFRepository<T>
    {
        T AddEntity(T entity);

        bool ModifyEntity(T entity);

        bool DeleteEntity(T entity);

        IQueryable<T> LoadEntities();

        IQueryable<T> LoadEntities(System.Func<T, bool> predicate);

        IQueryable<T> LoadPagedEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Func<T, S> keySelector, bool isAsc, System.Func<T, bool> predicate);

        IQueryable<T> LoadPagedEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Func<T, S> keySelector, bool isAsc, string sql, params System.Data.Objects.ObjectParameter[] parameters);

        IQueryable<T> SqlQuery(string sql, params System.Data.Objects.ObjectParameter[] parameters);

        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}