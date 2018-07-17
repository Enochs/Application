using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.DataAssmblly;

namespace HA.PMS.BLLAssmblly.Sys
{
    public class SysConfig
    {

        /// <summary>
        /// 编辑
        /// </summary>
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<Sys_Config> Getbyall()
        {
            return ObjEntity.Sys_Config.ToList();
        }

        public List<Sys_Config> GetByType(int? Type)
        {
            return ObjEntity.Sys_Config.Where(C => C.Type == Type).ToList();
        }


        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Sys_Config GetByID(int ID)
        {
            return ObjEntity.Sys_Config.FirstOrDefault(C => C.ConFigID == ID);
        }


        public Sys_Config GetByName(string Name)
        {
            return ObjEntity.Sys_Config.FirstOrDefault(C => C.ConfigName == Name);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int Update(Sys_Config ObjModel)
        {
            return ObjEntity.SaveChanges();
        }


        /// <summary>
        /// 根据关键字获取返回信息 确认此功能是否开启 IsClose=true则表示该功能关闭 反之则开启
        /// 如 关闭了上级订单需要审核功能 则订单需要审核 开启则订单不需要审核
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool GetPowerByKey(string Key)
        {
            return ObjEntity.Sys_Config.Where(C => C.IsClose == false && C.ConfigName == Key).Count() > 0;

        }



        /// <summary>
        /// 根据配置文件设置
        /// </summary>
        /// <returns></returns>
        public string SetStyleforSystemControlKey(string Key)
        {


            if (GetPowerByKey(Key))
            {
                return string.Empty;
            }
            else
            {
                return "style=\"display: none;\"";
            }

        }

        /// <summary>
        /// 直接移除控件
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Typers"></param>
        /// <returns></returns>
        public string SetStyleforSystemControlKey(string Key, int Typers)
        {

            if (GetPowerByKey(Key))
            {
                return string.Empty;
            }
            else
            {
                return "class=\"RemoveClass\"";
            }
        }

        /// <summary>
        /// 是否需要这个模块
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool GetNeedMamagerByKey(string Key)
        {
            return GetPowerByKey(Key);
        }

        /// <summary>
        /// 插入数据。
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public int Insert(Sys_Config Entity)
        {
            if (!object.ReferenceEquals(Entity, null))
            {
                ObjEntity.Sys_Config.Add(Entity);
                ObjEntity.SaveChanges();
                return Entity.ConFigID;
            }
            return -1;
        }

        /// <summary>
        /// 指示功能 自己录入的渠道只有自己和部门主管可以看见 是否开启的状态，true 为开启，false 关闭，若没有该功能，系统自动创建该功能，并可以设置默认状态，默认不开启该功能。
        /// </summary>
        /// <param name="UpdateCustomer">创建人。</param>
        /// <param name="Falg">初始化时的默认状态。true 为打开，false 为关闭。</param>
        /// <returns></returns>
        public bool IsSaleSourcePrivateOpening(int UpdateCustomer, bool Falg = false)
        {
            Sys_Config ObjSysConfigModel = ObjEntity.Sys_Config.Where(C => C.ConfigName.Equals("SaleSourcePrivate")).FirstOrDefault();
            if (!object.ReferenceEquals(ObjSysConfigModel, null))
            {
                return !ObjSysConfigModel.IsClose;
            }
            else
            {
                ObjSysConfigModel = new Sys_Config
                {
                    ConfigMarke = "开启该功能后，自己录入的渠道只有自己和上级可以看见。",
                    ConfigName = "SaleSourcePrivate",
                    IsClose = !Falg,
                    UpdateCustomer = UpdateCustomer,
                    UpdateTime = DateTime.Now
                };
                ObjEntity.Sys_Config.Add(ObjSysConfigModel);
                ObjEntity.SaveChanges();
                return !Falg;
            }
        }

        /// <summary>
        /// 指示功能 临时任务可以同时派给多个人 是否开启的状态，true 为开启，false 关闭，若没有该功能，系统自动创建该功能，并可以设置默认状态，默认不开启该功能。
        /// </summary>
        /// <param name="UpdateCustomer">创建人。</param>
        /// <param name="Falg">初始化时的默认状态。true 为打开，false 为关闭。</param>
        /// <returns></returns>
        public bool IsMissionPackDispatchOpening(int UpdateCustomer, bool Falg = false)
        {
            Sys_Config ObjSysConfigModel = ObjEntity.Sys_Config.Where(C => C.ConfigName.Equals("MissionPackDispatch")).FirstOrDefault();
            if (!object.ReferenceEquals(ObjSysConfigModel, null))
            {
                return !ObjSysConfigModel.IsClose;
            }
            else
            {
                ObjSysConfigModel = new Sys_Config
                {
                    ConfigMarke = "开启该功能后，临时任务可以同时派给多个人。",
                    ConfigName = "MissionPackDispatch",
                    IsClose = !Falg,
                    UpdateCustomer = UpdateCustomer,
                    UpdateTime = DateTime.Now
                };
                ObjEntity.Sys_Config.Add(ObjSysConfigModel);
                ObjEntity.SaveChanges();
                return !Falg;
            }
        }

    }
}
