#region 业务逻辑层实现类
/*
 */
#endregion

using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace HA.PMS.BLLAssmblly
{
    public sealed partial class PMSRepositoy<T> : BaseRepository<T> where T : class,new()
    {
        /// <summary>
        /// 带自定义参数格式的分页。
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="keySelector"></param>
        /// <param name="isAsc"></param>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> LoadPagedEntities<S>(int pageSize, int pageIndex, out int totalCount, Func<T, S> keySelector, bool isAsc, Func<T, bool> predicate, IEnumerable<ObjectParameter> parameters)
        {
            IEnumerable<T> query = HA.PMS.BLLAssmblly.PublicTools.PublicDataTools<T>.GetDataByParameter(new T(), parameters.ToArray()).Where(predicate);
            totalCount = query.Count();
            return isAsc ? query.OrderBy<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize) :
                query.OrderByDescending<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
        }

        /// <summary>
        /// 带自定义参数格式的分页。
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="keySelector"></param>
        /// <param name="isAsc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> LoadPagedEntities<S>(int pageSize, int pageIndex, out int totalCount, Func<T, S> keySelector, bool isAsc, IEnumerable<ObjectParameter> parameters)
        {
            IEnumerable<T> query = HA.PMS.BLLAssmblly.PublicTools.PublicDataTools<T>.GetDataByParameter(new T(), parameters.ToArray());
            totalCount = query.Count();
            return isAsc ? query.OrderBy<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize) :
                query.OrderByDescending<T, S>(keySelector).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
        }
    }
}
