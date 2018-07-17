#region 业务逻辑层抽象基类
/*
 */
#endregion

using System.Linq;

namespace HA.PMS.BLLAssmblly
{
    public class BaseRepository<T> : HA.PMS.BLLInterface.IEFRepository<T> where T : class
    {
        public static readonly PMS.DataAssmblly.PMS_WeddingEntities DbContext;

        static BaseRepository()
        {
            DbContext = new DataAssmblly.PMS_WeddingEntities();
        }

        public T AddEntity(T entity)
        {
            DbContext.Entry<T>(entity).State = System.Data.EntityState.Added;
            DbContext.SaveChanges();
            return entity;
        }

        public bool ModifyEntity(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = System.Data.EntityState.Modified;
            return DbContext.SaveChanges() > 0;
        }

        public bool UpdateEntity(T entity, System.Action<System.Data.Entity.Infrastructure.DbEntityEntry<T>> entityStateAction)
        {
            DbContext.Set<T>().Attach(entity);
            entityStateAction.Invoke(DbContext.Entry<T>(entity));
            return DbContext.SaveChanges() > 0;
        }

        public bool DeleteEntity(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = System.Data.EntityState.Deleted;
            return DbContext.SaveChanges() > 0;
        }

        public IQueryable<T> LoadEntities()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> LoadEntities(System.Func<T, bool> predicate)
        {
            return DbContext.Set<T>().Where<T>(predicate).AsQueryable();
        }

        public IQueryable<T> LoadPagedEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Func<T, S> keySelector, bool isAsc, System.Func<T, bool> predicate)
        {
            IQueryable<T> queryEntites = DbContext.Set<T>().Where<T>(predicate).AsQueryable();
            totalCount = queryEntites.Count();
            return isAsc ? queryEntites.OrderBy<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable() :
                queryEntites.OrderByDescending<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
        }

        public IQueryable<T> LoadPagedEntities<S>(int pageSize, int pageIndex, out int totalCount, System.Func<T, S> keySelector, bool isAsc, string sql, params System.Data.Objects.ObjectParameter[] parameters)
        {
            IQueryable<T> queryEntites = DbContext.Database.SqlQuery<T>(sql, parameters).AsQueryable();
            totalCount = queryEntites.Count();
            return isAsc ? queryEntites.OrderBy<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable() :
                queryEntites.OrderByDescending<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
        }

        public IQueryable<T> PagedEntities<S>(IQueryable<T> entites,int pageSize, int pageIndex)
        {
            return entites.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
        }
        
        public IQueryable<T> SqlQuery(string sql, params System.Data.Objects.ObjectParameter[] parameters)
        {
            return DbContext.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
