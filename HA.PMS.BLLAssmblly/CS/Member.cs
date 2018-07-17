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

namespace HA.PMS.BLLAssmblly.CS
{
    public class Member : ICRUDInterface<CS_Member>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();



        /// <summary>
        /// 获取计划完成的服务方式
        /// </summary>
        /// <returns></returns>
        public List<View_GetOtherMember> GetOtherMember(int PageSize, int PageIndex, out int SourceCount,  List<ObjectParameter> ObjParList)
        {
            var query = PublicDataTools<View_GetOtherMember>.GetDataByParameter(new View_GetOtherMember(), ObjParList.ToArray());
            SourceCount = query.Count();

            List<View_GetOtherMember> resultList = query
                //根据时间排序
                   .OrderByDescending(C => C.CreateDate)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_GetOtherMember>();
            }
            return resultList;
        }


        public string GetMemberTypeByCustomerID(int CustomerID)
        {
            var ObjModel = ObjEntity.CS_Member.FirstOrDefault(C=>C.CustomerID==CustomerID);
            if (ObjModel == null)
            {
                return "";
            }
            else
            {
                return ObjModel.ServiceType;
            }
        }



        /// <summary>
        /// 根据客户ID获取服务记录
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public CS_Member GetByCustomerID(int CustomerID)
        {

            var ObjModel = ObjEntity.CS_Member.FirstOrDefault(C => C.CustomerID == CustomerID);
            if (ObjModel == null)
            {
                return new CS_Member();
            }
            else
            {
                return ObjModel;
            }
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(CS_Member ObjectT)
        {
            ObjEntity.CS_Member.Remove(ObjectT);
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 获取素所有
        /// </summary>
        /// <returns></returns>
        public List<CS_Member> GetByAll()
        {
            return ObjEntity.CS_Member.ToList();
        }


        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public CS_Member GetByID(int? KeyID)
        {
            return ObjEntity.CS_Member.FirstOrDefault(C => C.MemberID == KeyID);
        }


        /// <summary>
        /// 暂无用处
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<CS_Member> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加庆典服务
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(CS_Member ObjectT)
        {
            ObjEntity.CS_Member.Add(ObjectT);
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 更新服务内容
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(CS_Member ObjectT)
        {
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 获取周年庆 生日庆详细信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetMember> GetMemberListByIndex(int PageSize, int PageIndex, out int SourceCount)
        {

            SourceCount = ObjEntity.View_GetMember.Count();

            List<View_GetMember> resultList = ObjEntity.View_GetMember
                //根据时间排序
                   .OrderByDescending(C => C.CreateDate)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<View_GetMember>();
            }
            return resultList;
        }

        public List<View_GetMember> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            var ReturnList = PublicDataTools<View_GetMember>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);
            return ReturnList;
        }


        /// <summary>
        /// 根据年和客户验证服务
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="Year"></param>
        /// <returns>False为今年已经服务,True今年还未服务</returns>
        public bool CheckMemberByYear(int CustomerID, int Year,int Type)
        {

            var ObjModel = ObjEntity.CS_Member.FirstOrDefault(C => C.CustomerID == CustomerID && C.CreateDate.Year == Year && C.Type == Type);
            if (ObjModel != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
