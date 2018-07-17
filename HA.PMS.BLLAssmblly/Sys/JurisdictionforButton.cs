
/**
 Version :HaoAi 1.0
 File Name :Customers
 Author:黄晓可
 Date:2013.3.15
 Description:用户控件权限管理 实现ICRUDInterface<T> 接口中的方法
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;
using System.Data.SqlClient;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class JurisdictionforButton : ICRUDInterface<Sys_JurisdictionforButton>
    {

        /// <summary>
        /// EF操作实例化
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 删除用户的控件权限
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(Sys_JurisdictionforButton ObjectT)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 根据控件ID删除权限（实际为有权限删除此表数据）
        /// </summary>
        /// <param name="ControlKey"></param>
        /// <returns></returns>
        public int DeleteByContorlKey(int ControlKey, int JurisdictionID)
        {
 
             ObjEntity.Sys_JurisdictionforButton.Remove(ObjEntity.Sys_JurisdictionforButton.FirstOrDefault(C=>C.ControlID==ControlKey&&C.JurisdictionID==JurisdictionID));
             return ObjEntity.SaveChanges();
        }


        /// <summary>
        ///根据权限ID删除用户的控件权限 
        /// </summary>
        /// <param name="JurisdictionID"></param>
        /// <returns></returns>
        public int DeleteByJurisdictionID(int? JurisdictionID)
        {
            int i = 0;
            foreach (var ObjItem in ObjEntity.Sys_JurisdictionforButton.Where(C => C.JurisdictionID == JurisdictionID))
            {
                ObjEntity.Sys_JurisdictionforButton.Remove(ObjItem);
                i=ObjEntity.SaveChanges();
            }
            return i;
 
            //ObjEntity.Sys_JurisdictionforButton.Remove(ObjEntity.Sys_JurisdictionforButton.FirstOrDefault(C => C.JurisdictionID));
        }


        /// <summary>
        /// 获取频道下的人员按钮级权限
        /// </summary>
        /// <param name="Employeeid"></param>
        /// <param name="GetType"></param>
        /// <returns></returns>
        public List<Sys_JurisdictionforButton> GetByChannel(int? Employeeid,string GetType)
        {

            UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
            var JurisdID = ObjUserJurisdictionBLL.GetUserJurisdictionByChanneltype(Employeeid,GetType).JurisdictionID;
            return ObjEntity.Sys_JurisdictionforButton.Where(C => C.JurisdictionID == JurisdID&&C.IsClose==true).ToList();
        }

        /// <summary>
        /// 初始化用户控件权限
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public bool StarJurisdictionforButton(int? EmployeeID)
        {
            Controls ObjControlsBLL = new Controls();
            var ObjControlList=ObjControlsBLL.GetByAll();
            Sys_JurisdictionforButton ObjJurisdictionforButtonModel;
            UserJurisdiction ObjUserJurisdictionBLL = new UserJurisdiction();
            foreach (var ObjControl in ObjControlList)
            {
                ObjJurisdictionforButtonModel = new Sys_JurisdictionforButton();
                //ObjJurisdictionforButtonModel.ButtonKey = ObjControl.ConrolKey;
                ObjJurisdictionforButtonModel.ChannelID = ObjControl.ChannelID;
                ObjJurisdictionforButtonModel.ControlID = ObjControl.ControlID;
                ObjJurisdictionforButtonModel.IsClose = true;
                ObjJurisdictionforButtonModel.JurisdictionID = ObjUserJurisdictionBLL.GetUserJurisdictionByChannelandEmpLoyee(EmployeeID,ObjControl.ChannelID).JurisdictionID;
                this.Insert(ObjJurisdictionforButtonModel);

            }
            return true;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Sys_JurisdictionforButton> GetByAll()
        {
            throw new NotImplementedException();
        }

        public Sys_JurisdictionforButton GetByID(int? KeyID)
        {
            throw new NotImplementedException();
        }

        public List<Sys_JurisdictionforButton> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加用户控件权限 先通过权限ID判断有无此权限有则跳出
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(Sys_JurisdictionforButton ObjectT)
        {
            var CheckModel = ObjEntity.Sys_JurisdictionforButton.FirstOrDefault(C=>C.JurisdictionID==ObjectT.JurisdictionID&&C.ControlID==ObjectT.ControlID);
            if (CheckModel == null)
            {
                ObjEntity.Sys_JurisdictionforButton.Add(ObjectT);
                return ObjEntity.SaveChanges();
            }
            else
            {
                CheckModel.IsClose = ObjectT.IsClose;
                ObjEntity.SaveChanges();
            }
            return 0;
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(Sys_JurisdictionforButton ObjectT)
        {
            throw new NotImplementedException();
        }
    }
}
