using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.PublicTools;

//目标基础表
namespace HA.PMS.BLLAssmblly.SysTarget
{
    public class Target:ICRUDInterface<FL_Target>
    {


        PMS_WeddingEntities Objentity = new PMS_WeddingEntities();


        public int Delete(FL_Target ObjectT)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 获取本人的目标
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public List<FL_Target> GetEmployeeTarget(List<int> EmployeeID, out  List<FL_FinishTargetSum> ObjSource,int Type)
        {
            List<FL_Target> ObjReturnList = new List<FL_Target>();
            UserJurisdiction ObjChannelBLL = new UserJurisdiction();
            List<FL_FinishTargetSum> ObjOutList = new List<FL_FinishTargetSum>();
            var NeedTarget = Objentity.FL_Target.Where(C=>C.TargetType==Type).ToList();
            foreach (var ObjEmployeeKey in EmployeeID)
            {
                var ChannelList = ObjChannelBLL.GetEmPloyeeChannel(ObjEmployeeKey,0);
                ChannelList = ChannelList.Where(C=>C.IsClose==false).ToList();
                foreach (var Objitem in ChannelList)
                {
                    var ObjModel = NeedTarget.Where(C => C.ChannelID == Objitem.ChannelID).ToList();
                    if (ObjModel.Count>0)
                    {
                        foreach (var ObjTargetModel in ObjModel)
                        {
                            ObjReturnList.Add(ObjTargetModel);
                            ObjOutList.Add(new FL_FinishTargetSum() { TargetTitle = ObjTargetModel.TargetTitle, TargetID = ObjTargetModel.TargetID, EmployeeID = ObjEmployeeKey });
                        }
                    }
                }
            }

 
            ObjSource = ObjOutList;
            return ObjReturnList;
        }

        /// <summary>
        /// 获取全部 不分页
        /// </summary>
        /// <returns></returns>
        public List<FL_Target> GetByAll()
        {
            return Objentity.FL_Target.ToList();
        }


        public List<FL_Target> GetByType(int TargetType)
        {
            return Objentity.FL_Target.Where(C => C.TargetType == TargetType).ToList();
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_Target GetByID(int? KeyID)
        {
            return Objentity.FL_Target.FirstOrDefault(C=>C.TargetID==KeyID);
        }

        public List<FL_Target> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加一条
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_Target ObjectT)
        {
            Objentity.FL_Target.Add(ObjectT);
            return Objentity.SaveChanges();
        }


        /// <summary>
        /// 修改一条
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_Target ObjectT)
        {
            Objentity.SaveChanges();
            return ObjectT.TargetID;
        }

        public List<FL_Target> GetDataByParameter(List<PMSParameters> ObjParameterList, string OrderByCloumname, int PageSize, int PageIndex, ref int SourceCount)
        {
            return PublicDataTools<FL_Target>.GetDataByWhereParameter(ObjParameterList, OrderByCloumname, PageSize, PageIndex, out SourceCount);
        }
    }
}
