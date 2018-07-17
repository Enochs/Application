
/**
 Version :HaoAi 1.0
 File Name :Customers
 Author:黄晓可
 Date:2013.3.17
 Description:客户邀请 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLAssmblly.Sys;
using System;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class Invite : ICRUDInterface<FL_Invite>
    {
        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 获取邀约统计
        /// </summary>
        /// <param name="ObjParList">参数列表</param>
        /// <param name="Type">类型1客源量 2订单金额 3有效信息量 4邀约中的数量 5邀约成功数 6流失数</param>
        /// <returns></returns>
        public string GetInviteSumTotal(List<ObjectParameter> ObjParList, int Type)
        {

            switch (Type)
            {
                case 1:
                    return PublicDataTools<View_GetTelmarketingCustomers>.GetDataByParameter(new View_GetTelmarketingCustomers(), ObjParList.ToArray()).Count.ToString();
                    break;
                case 2:
                    return PublicDataTools<View_GetInviteCompletetoEnd>.GetDataByParameter(new View_GetInviteCompletetoEnd(), ObjParList.ToArray()).Sum(C => C.FinishAmount).ToString();
                    break;
                case 3:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Count.ToString();
                    break;
                case 4:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Count.ToString();
                    break;
                case 5:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Where(C => C.State >= 6 && C.State < 29).Count().ToString();
                    break;
                case 6:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Count.ToString();
                    break;
                case 7:
                    return PublicDataTools<View_GetTelmarketingCustomers>.GetDataByParameter(new View_GetTelmarketingCustomers(), ObjParList.ToArray()).Count.ToString();
                    break;
                case 8:
                    return PublicDataTools<FL_Invite>.GetDataByParameter(new FL_Invite(), ObjParList.ToArray()).Count.ToString();
                    break;
            }
            return string.Empty;
        }


        /// <summary>
        /// 根据年月获取客源量
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns>1客源量 2有效量 3邀约中 4邀约成功量 5流失 6未邀约 7订单总金额</returns>
        public string GetInviteSumByDatetime(List<ObjectParameter> ObjParList, int Year, int Month, int Type)
        {
            switch (Type)
            {
                case 1:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month).Count().ToString();
                    break;
                case 2:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.State > 3).Count().ToString();

                    //ObjEntity.View_GetInviteCustomers.Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month&&C.State>3).Count().ToString();
                    break;
                case 3:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.State == 5).Count().ToString();

                    //ObjEntity.View_GetInviteCustomers.Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.State ==5).Count().ToString();
                    break;
                case 4:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.State > 6 && C.State != 29).Count().ToString();
                    break;
                case 5:
                    return PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.State == 29).Count().ToString();
                    break;
                case 6:

                    return PublicDataTools<View_GetTelmarketingCustomers>.GetDataByParameter(new View_GetTelmarketingCustomers(), ObjParList.ToArray()).Where(C => C.CreateDate.Value.Year == Year && C.CreateDate.Value.Month == Month && C.State == 3).Count().ToString();
                    break;
                case 7:
                    break;
            }

            return string.Empty;
        }

        ///// <summary>
        ///// 根据时间段获取客源量
        ///// </summary>
        ///// <param name="Star"></param>
        ///// <param name="End"></param>
        ///// <returns></returns>
        //public string GetInviteCountByDateTime(DateTime Star, DateTime End)
        //{
        //    return ObjEntity.FL_Invite.Count(C => C.CreateDate >= Star && C.CreateDate <= End).ToString();


        //}


        ///// <summary>
        ///// 获取订单金额
        ///// </summary>
        ///// <param name="Star"></param>
        ///// <param name="End"></param>
        ///// <returns></returns>
        //public string GetQuotedMoneyByInvite(DateTime Star, DateTime End)
        //{

        //    return ObjEntity.View_GetInviteCompletetoEnd.Where(C => C.CreateDate >= Star && C.CreateDate <= End).Sum(C=>C.FinishAmount).ToString();

        //}



        /// <summary>
        /// 获取有效信息量
        /// </summary>
        /// <returns></returns>
        public string GetEffectiveInvite(DateTime Star, DateTime End)
        {
            return ObjEntity.View_GetInviteCustomers.Count(C => C.CreateDate >= Star && C.CreateDate <= End).ToString();
        }


        /// <summary>
        /// 获取邀约中的数量
        /// </summary>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public string GetStarInviteCount(DateTime Star, DateTime End, int State)
        {
            return ObjEntity.View_GetInviteCustomers.Count(C => C.CreateDate >= Star && C.CreateDate <= End && C.State >= State && C.State <= 29).ToString();
        }




        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_Invite ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Invite.Remove(GetByID(ObjectT.InviteID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }


        /// <summary>
        /// 获取搜友邀约信息
        /// </summary>
        /// <returns></returns>
        public List<FL_Invite> GetByAll()
        {
            return ObjEntity.FL_Invite.ToList();
        }


        /// <summary>
        /// 获取邀约流失原因
        /// </summary>
        /// <param name="InviteID"></param>
        /// <returns></returns>
        public string GetLoseContentByInviteID(int? InviteID)
        {
            var ObjModel = ObjEntity.FL_InvtieContent.FirstOrDefault(C => C.InviteID == InviteID && C.State == "流失");
            if (ObjModel != null)
            {
                return ObjModel.LoseContent;
            }
            else
            {
                return "未填写";
            }
        }



        /// <summary>
        /// 根据客ID获取沟通记录主体
        /// </summary>
        /// <returns></returns>
        public FL_Invite GetByCustomerID(int CustomerID)
        {
            return ObjEntity.FL_Invite.FirstOrDefault(C => C.CustomerID == CustomerID);
        }


        /// <summary>
        /// 根据ID获取邀约信息
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Invite GetByID(int? KeyID)
        {
            return ObjEntity.FL_Invite.FirstOrDefault(C => C.InviteID == KeyID);
        }


        /// <summary>
        /// 分页不需条件获取所有邀约记录
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_Invite> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// 根据条件获取邀约用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetTelmarketingCustomers> GetTelCustomerByStateIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;

            var DataSource = PublicDataTools<View_GetTelmarketingCustomers>.GetDataByParameter(new View_GetTelmarketingCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).OrderByDescending(C => C.CreateDate).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<View_GetTelmarketingCustomers>();
            }
            return PageDataTools<View_GetTelmarketingCustomers>.AddtoPageSize(DataSource);

        }

        /// <summary>
        /// 历史查询
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<View_GetInviteCustomers> GetInviteCustomerAllByParameter()
        {
            var DataSource = ObjEntity.View_GetInviteCustomers.Where(C => C.IsDelete == false).OrderByDescending(C => C.LastFollowDate).ToList();


            return DataSource;
        }

        /// <summary>
        /// 获取邀约负责人
        /// </summary>
        /// <returns></returns>
        public string GetIntiveEmployeeName(int? CustomerID)
        {
            Employee ObjEmployeeBLL = new Employee();

            var ObjModel = this.GetByCustomerID(CustomerID.Value);
            if (ObjModel != null)
            {
                var ObjCustomerModel = ObjEmployeeBLL.GetByID(ObjModel.EmpLoyeeID);
                if (ObjCustomerModel != null)
                {
                    if (ObjCustomerModel.EmployeeName.Length < 3)
                    {
                        return ObjCustomerModel.EmployeeName + "&nbsp;&nbsp;";
                    }
                    else
                    {
                        return ObjCustomerModel.EmployeeName;
                    }
                }
                else
                {
                    return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
            }
            else
            {
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }
        }
        /// <summary>
        /// 提供统计分析当期功能数据
        /// </summary>
        /// <param name="isAdd"></param>
        /// <param name="customerList"></param>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<View_GetInviteCustomers> GetInviteCustomerByParameter(bool isAdd, List<int> customerList, List<ObjectParameter> ObjParList)
        {
            var DataSource = PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.LastFollowDate).ToList();
            List<View_GetInviteCustomers> list = new List<View_GetInviteCustomers>();
            View_GetInviteCustomers customer;
            if (isAdd)
            {
                foreach (var cid in customerList)
                {
                    customer = DataSource.Where(C => C.CustomerID == cid).FirstOrDefault();
                    if (customer != null)
                    {
                        list.Add(customer);
                    }
                }
            }
            else
            {
                list = DataSource;
            }
            return list;
        }
        /// <summary>
        /// 邀约新人查询
        /// </summary>
        /// <param name="isAdd">是否加入了 接收时间的条件，如果加入则为true，相反</param>
        /// <param name="customerList">加入之后接收时间的条件，参入对应时间段的参数</param>
        /// <param name="ObjParList">参数对象</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetInviteCustomers> GetInviteCustomerByStateIndex(bool isAdd, List<int> customerList, List<ObjectParameter> ObjParList, int PageSize, int PageIndex, out int SourceCount)
        {
            var DataSource = PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.LastFollowDate).ToList();
            List<View_GetInviteCustomers> list = new List<View_GetInviteCustomers>();
            View_GetInviteCustomers customer;
            if (isAdd)
            {
                foreach (var cid in customerList)
                {
                    customer = DataSource.Where(C => C.CustomerID == cid).FirstOrDefault();
                    if (customer != null)
                    {
                        list.Add(customer);
                    }
                }
            }
            else
            {
                list = DataSource;
            }

            PageIndex = PageIndex - 1;

            var objResult = list.Skip(PageSize * PageIndex).Take(PageSize).ToList();
            SourceCount = DataSource.Count;
            if (SourceCount == 0)
            {
                objResult = new List<View_GetInviteCustomers>();
            }
            return PageDataTools<View_GetInviteCustomers>.AddtoPageSize(objResult);

        }



        /// <summary>
        /// 根据条件获取渠道邀约用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetTelmarketingCustomers> GetTelCustomerByWhere(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<View_GetTelmarketingCustomers>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount);

        }

        /// <summary>
        /// 分页获取邀约数据
        /// </summary>
        /// <param name="ObjParList">参数</param>
        /// <param name="OrdreByColumname">排序字段</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="SourceCount">总行数</param>
        /// <returns>邀约集合</returns>
        public List<View_GetInviteCustomers> GetByWhereParameter(List<PMSParameters> ObjParList, string OrdreByColumname, int PageSize, int PageIndex, out int SourceCount)
        {

            return PublicDataTools<View_GetInviteCustomers>.GetDataByWhereParameter(ObjParList, OrdreByColumname, PageSize, PageIndex, out SourceCount, OrderType.Asc);

        }

        /// <summary>
        /// 不含分页的条件获取邀约用户
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<View_GetInviteCustomers> GetInviteCustomerByStateIndex(List<ObjectParameter> ObjParList)
        {


            var DataSource = PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.LastFollowDate).ToList();


            if (DataSource.Count == 0)
            {
                DataSource = new List<View_GetInviteCustomers>();
            }
            //return PageDataTools<View_GetInviteCustomers>.AddtoPageSize(DataSource);
            return DataSource;
        }
        /// <summary>
        /// 根据条件获取邀约用户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<View_GetInviteCustomers> GetInviteCustomerByStateIndex(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {

            PageIndex = PageIndex - 1;

            var DataSource = PublicDataTools<View_GetInviteCustomers>.GetDataByParameter(new View_GetInviteCustomers(), ObjParList.ToArray()).OrderByDescending(C => C.LastFollowDate).ToList();
            SourceCount = DataSource.Count;

            if (PageIndex >= 0)
            {

                DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).OrderByDescending(C => C.LastFollowDate).ToList();
            }

            if (SourceCount == 0)
            {
                DataSource = new List<View_GetInviteCustomers>();
            }
            //return PageDataTools<View_GetInviteCustomers>.AddtoPageSize(DataSource);
            return DataSource;
        }

        /// <summary>
        /// 添加邀约信息
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Invite ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_Invite.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.InviteID;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public int Update(FL_Invite ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.SaveChanges();
                return ObjectT.InviteID;
            }
            return 0;
        }

        public List<FL_Invite> Where(System.Linq.Expressions.Expression<Func<FL_Invite, bool>> predicate)
        {
            return ObjEntity.FL_Invite.Where(predicate).ToList();
        }

        public string GetInviteSum(IEnumerable<View_GetInviteCustomers> source, InviteSumTypes type)
        {
            switch (type)
            {
                //客源量
                case InviteSumTypes.TotalInviteCount: return source.Count().ToString();
                //有效量
                case InviteSumTypes.ActualInviteCount: return source.Count(C => C.State > 5).ToString();
                //邀约中量
                case InviteSumTypes.InvitingCount: return source.Count(C => C.State >= 13 && C.State < 29).ToString();
                //邀约成功量
                case InviteSumTypes.SuccessInviteCount: return source.Count(C => C.State >= 13 && C.State < 29).ToString();
                //流失量
                case InviteSumTypes.LoseCount: return source.Count(C => C.State == 29 || C.State == 300).ToString();
                //未邀约量
                case InviteSumTypes.NotInviteCount: return source.Count(C => C.State == 29 || C.State == 300).ToString();
                //订单总额
                case InviteSumTypes.TotalFinishAmount: return "0";
                default: return string.Empty;
            }
        }

        public string GetInviteSum(IEnumerable<View_GetInviteCustomers> source, InviteSumTypes type, int year, int month)
        {
            switch (type)
            {
                //客源量
                case InviteSumTypes.TotalInviteCount: return source.Count(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month).ToString();
                //有效量
                case InviteSumTypes.ActualInviteCount: return source.Count(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month && C.State > 5).ToString();
                //邀约中量
                case InviteSumTypes.InvitingCount: return source.Count(C => C.State >= 13 && C.State < 29).ToString();
                //邀约成功量
                case InviteSumTypes.SuccessInviteCount: return source.Count(C => C.CreateDate.Value.Year == year && C.CreateDate.Value.Month == month && C.State >= 13 && C.State < 29).ToString();
                //流失量
                case InviteSumTypes.LoseCount: return source.Count(C => C.State == 29 || C.State == 300).ToString();
                //未邀约量
                case InviteSumTypes.NotInviteCount: return source.Count(C => C.State == 29 || C.State == 300).ToString();
                //订单总额
                case InviteSumTypes.TotalFinishAmount: return "0";
                default: return string.Empty;
            }
        }
    }
}
