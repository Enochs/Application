using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLInterface
{

    /// <summary>
    /// 增删改查接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICRUDInterface<T> where T:class
    {

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
       T GetByID(int? KeyID);


        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        List<T> GetByAll();

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        List<T> GetByIndex(int PageSize, int PageIndex, out int SourceCount);


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns>返回Key</returns>
        int Insert(T ObjectT);


        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        int Update(T ObjectT);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        int Delete(T ObjectT);

    }
}
