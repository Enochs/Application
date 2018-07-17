
/**
 Version :HaoAi 1.0
 File Name :基础任务表
 Author:杨洋
 Date:2013.3.23
 Description:客户管理 实现ICRUDInterface<T> 接口中的方法
 **/
using System.Collections.Generic;
using System.Linq;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLInterface;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.ToolsLibrary;
using System.IO;
using System.Web;
namespace HA.PMS.BLLAssmblly.Flow
{
    public class MissionManager : ICRUDInterface<FL_MissionManager>
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="CustomerID"></param>
        public void DeleteByCustomerID(int CustomerID)
        {

            string LikeKey = "CustomerID=" + CustomerID;

            //先查询需要删除的详细内容
            var DeleteList = ObjEntity.FL_MissionDetailed.Where(C => C.KeyWords.Contains(LikeKey)).ToList();
            List<int> ObjManagerKey = new List<int>();
            foreach (var Objitem in DeleteList)
            {
                //第二步删除任务相关的文件记录
                var FileList = ObjEntity.FL_MissionFile.Where(C => C.DetailedID == Objitem.DetailedID).ToList();
                foreach (var ObjFile in FileList)
                {
                    //1.删除文件
                    if (File.Exists(HttpContext.Current.Server.MapPath(ObjFile.FileAddress + string.Empty)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(ObjFile.FileAddress));
                    }
                    ObjEntity.FL_MissionFile.Remove(ObjFile);
                    ObjEntity.SaveChanges();

                }
                ObjEntity.SaveChanges();

                //最后开始删除明细任务
                ObjManagerKey.Add(Objitem.MissionID.Value);
                ObjEntity.FL_MissionDetailed.Remove(Objitem);
                ObjEntity.SaveChanges();


            }



            //ObjManagerKey = ObjManagerKey.Distinct().ToList();
            //foreach (var ObjKey in ObjManagerKey)
            //{
            //    var DeleteModel = ObjEntity.FL_MissionManager.FirstOrDefault(C => C.MissionID == ObjKey);
            //    if (DeleteModel != null)
            //    {
            //        ObjEntity.FL_MissionManager.Remove(DeleteModel);
            //        ObjEntity.SaveChanges();
            //    }LikeKey
            //}





        }

        /// <summary>
        /// 根据条件下属任务统计数据
        /// </summary>
        /// <param name="GetWhere"></param>
        /// <returns></returns>
        public List<View_GetMissionSum> GetMissionByWhere(string GetWhere, List<Sys_Employee> ObjEmpLoyeeList)
        {
            string EmpLoyeeIDList = string.Empty;
            if (ObjEmpLoyeeList.Count > 0)
            {
                foreach (var ObjEmployeeItem in ObjEmpLoyeeList)
                {
                    EmpLoyeeIDList += ObjEmployeeItem.EmployeeID + ",";
                }

                EmpLoyeeIDList = EmpLoyeeIDList.Trim(',');

                GetWhere += " and EmployeeID in (" + EmpLoyeeIDList + ")";

                var DataList = ObjEntity.GetMissionSum(GetWhere).ToList();
                return DataList;
            }
            else
            {
                return new List<View_GetMissionSum>();
            }

        }

        /// <summary>
        /// 获取任物数量
        /// </summary>
        /// <returns></returns>
        public MissionSum GetMissionSum(int? EmployeeID, int? MissionState, string GetWhere,int Type)       //Type 1 根据状态查询  0 查询所有总数
        {
            var MissionModel = ObjEntity.GetMissionDetailsCount(EmployeeID, MissionState, GetWhere,Type).FirstOrDefault();
            MissionSum Model = new MissionSum();
            if (MissionModel != null)
            {
                Model.EmployeeID = MissionModel.EmployeeID.ToString().ToInt32();
                Model.MissiionCount = MissionModel.Count.ToString().ToInt32();
                Model.State = MissionModel.MissionState.ToString().ToInt32();
            }
            else
            {
                Model.EmployeeID = EmployeeID.ToString().ToInt32();
                Model.MissiionCount = 0;
                Model.State = MissionState.ToString().ToInt32();
            }

            return Model;
        }


        public List<View_GetMissionSum> GetMissionByWhere(string GetWhere)
        {
            return ObjEntity.GetMissionSum(GetWhere).ToList();
        }


        /// <summary>
        /// 根据EmpLoyeeID获取数据
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public View_GetMissionSum GetMissionByEmpLoyeeID(int EmpLoyeeID)
        {
            return ObjEntity.View_GetMissionSum.FirstOrDefault(C => C.EmpLoyeeID == EmpLoyeeID);
        }



        /// <summary>
        /// 删除任务主体
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Delete(FL_MissionManager ObjectT)
        {
            if (ObjectT != null)
            {
                ObjEntity.FL_MissionManager.Remove(GetByID(ObjectT.MissionID));
                return ObjEntity.SaveChanges();
            }
            return 0;
        }


        /// <summary>
        /// 获取所有任务主体
        /// </summary>
        /// <returns></returns>
        public List<FL_MissionManager> GetByAll()
        {
            return ObjEntity.FL_MissionManager.ToList();
        }


        /// <summary>
        /// 根据主键获取单条
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public FL_MissionManager GetByID(int? KeyID)
        {
            return ObjEntity.FL_MissionManager.FirstOrDefault(C => C.MissionID == KeyID);
        }


        /// <summary>
        /// 分页获取所有的任务主体
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SourceCount"></param>
        /// <returns></returns>
        public List<FL_MissionManager> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            SourceCount = ObjEntity.FL_Message.Count();

            List<FL_MissionManager> resultList = ObjEntity.FL_MissionManager
                //进行排序功能操作，不然系统会抛出异常
                   .OrderByDescending(C => C.MissionID)
                   .Skip(PageSize * (PageIndex - 1)).Take(PageSize).OrderByDescending(C => C.CreateDate).ToList();

            if (resultList.Count == 0)
            {
                resultList = new List<FL_MissionManager>();
            }
            return resultList;
        }



        /// <summary>
        /// 添加一条任务主体
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_MissionManager ObjectT)
        {
            if (ObjectT != null)
            {
                if (ObjectT.IsCheck == null)
                {

                    ObjectT.IsCheck = false;
                }

                ObjectT.CheckContent = string.Empty;
                ObjEntity.FL_MissionManager.Add(ObjectT);
                if (ObjEntity.SaveChanges() > 0)
                {
                    return ObjectT.MissionID;
                }

            }
            return 0;
        }



        /// <summary>
        /// 分页获取派工
        /// </summary>
        /// <returns></returns>
        public List<FL_MissionManager> GetMissionManagerByWhere(int PageSize, int PageIndex, out int SourceCount, List<ObjectParameter> ObjParList)
        {
            PageIndex = PageIndex - 1;
            var DataSource = PublicDataTools<FL_MissionManager>.GetDataByParameter(new FL_MissionManager(), ObjParList.ToArray()).OrderByDescending(C => C.CreateDate).ToList();
            SourceCount = DataSource.Count;
            DataSource = DataSource.Skip(PageSize * PageIndex).Take(PageSize).ToList();

            if (SourceCount == 0)
            {
                DataSource = new List<FL_MissionManager>();
            }
            return PageDataTools<FL_MissionManager>.AddtoPageSize(DataSource);
        }


        public List<FL_MissionManager> GetAllMissionByParameter(List<PMSParameters> pars, string SortName, int PageSize, int PageIndex, out int SourceCount)
        {
            return PublicDataTools<FL_MissionManager>.GetDataByWhereParameter(pars, SortName, PageSize, PageIndex, out SourceCount);
        }


        /// <summary>
        /// 修改任务主体
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_MissionManager ObjectT)
        {
            if (ObjectT != null)
            {
                return ObjEntity.SaveChanges();
                //return ObjectT.MissionID;
            }
            return 0;
        }



        /// <summary>
        /// 是否需要添加计划
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="MissionType"></param>
        /// <param name="Timer"></param>
        /// <param name="EmpLoyeeID"></param>
        /// <param name="TyperName"></param>
        /// <returns></returns>
        public int CheckInsert(int Type, int MissionType, DateTime Timer, int EmpLoyeeID, out string TyperName)
        {
            return CheckInsert(string.Empty, Type, MissionType, Timer, EmpLoyeeID, out  TyperName);
        }

        /// <summary>
        /// 验证是否需要创建包
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="MissionType"></param>
        /// <param name="Timer"></param>
        public int CheckInsert(string Title, int Type, int MissionType, DateTime Timer, int EmpLoyeeID, out string TyperName)
        {

            string OutTyper = string.Empty;
            switch (MissionType)
            {
                case 1:
                    OutTyper = "电话营销";
                    break;
                case 2:
                    OutTyper = "邀约";
                    break;
                case 3:
                    OutTyper = "跟单";
                    break;
                case 4:
                    OutTyper = "报价";
                    break;
                case 5:
                    OutTyper = "制作执行明细";
                    break;
                case 6:
                    OutTyper = "总派工";
                    break;
                case 7:
                    OutTyper = "分工执行";
                    break;
                case 8:
                    OutTyper = "庆典";
                    break;
                case -4:
                    OutTyper = "审核报价单";
                    break;
                case -8:
                    OutTyper = "制作婚礼统筹表";
                    break;
                case -5:
                    OutTyper = "制作收款计划";
                    break;
                case 11:
                    OutTyper = "设计单";
                    break;
                default:
                    OutTyper = "计划任务";
                    break;
            }
            DateTime ObjTimerSpan = DateTime.Parse(Timer.ToShortDateString());
            var ObjExistModel = ObjEntity.FL_MissionManager.FirstOrDefault(C => C.Type == 1 && C.MissionType == MissionType && C.CreateDate == ObjTimerSpan && C.EmployeeID == EmpLoyeeID && C.CheckState == 1);
            if (ObjExistModel != null)
            {
                TyperName = OutTyper;
                return ObjExistModel.MissionID;
            }
            else
            {


                FL_MissionManager ObjMissionManagerModel = new FL_MissionManager();
                Department ObjDepartmentBLL = new Department();
                Employee ObjEmployeeBLL = new Employee();
                ObjMissionManagerModel.CreateDate = System.DateTime.Now;
                ObjMissionManagerModel.DepartmentID = ObjEmployeeBLL.GetByID(EmpLoyeeID).DepartmentID;
                ObjMissionManagerModel.EmployeeID = EmpLoyeeID;
                //if (ObjEmployeeBLL.IsManager(EmpLoyeeID))
                //{
                //    ObjMissionManagerModel.CheckEmpLoyeeID = ObjDepartmentBLL.GetByID(ObjMissionManagerModel.DepartmentID).DepartmentManager;
                //    ObjMissionManagerModel.IsCheck = true;
                //    ObjMissionManagerModel.CheckState = 3;
                //}
                //else
                //{
                //最顶级不需要审核
                ObjMissionManagerModel.CheckEmpLoyeeID = ObjEmployeeBLL.GetMineCheckEmployeeID(EmpLoyeeID);
                if (ObjMissionManagerModel.CheckEmpLoyeeID == 0)
                {
                    ObjMissionManagerModel.IsCheck = true;
                    ObjMissionManagerModel.CheckState = 3;
                }
                else
                {
                    //非最顶级审核
                    ObjMissionManagerModel.IsCheck = true;
                    ObjMissionManagerModel.CheckState = 1;
                }
                //}
                ObjMissionManagerModel.IsAppraise = false;
                ObjMissionManagerModel.IsDelete = false;
                ObjMissionManagerModel.Type = Type;
                ObjMissionManagerModel.MissionType = MissionType;
                if (Title == string.Empty)
                {
                    ObjMissionManagerModel.MissionTitle = OutTyper + "任务";
                }
                else
                {
                    ObjMissionManagerModel.MissionTitle = Title;
                }
                TyperName = OutTyper;
                return this.Insert(ObjMissionManagerModel);

            }
        }


        ///// <summary>
        ///// 直接创建详情 不创建主体
        ///// </summary>
        ///// <param name="TypeName"></param>
        ///// <param name="StarDate"></param>
        ///// <param name="EmployeeID"></param>
        ///// <param name="keyWords"></param>
        ///// <param name="ChannelType"></param>
        ///// <returns></returns>
        //public int WeddingMissionCreate(string TypeName, DateTime StarDate, int EmployeeID, string keyWords, string ChannelType)
        //{ 
        //    WeddingMissionCreate(string.Empty,  TypeName,  StarDate,  EmployeeID,  keyWords,  ChannelType);
        //}

        //public int WeddingMissionCreate(int,int Type, int MissionType, DateTime StarDate, int EmployeeID, string keyWords, string ChannelType, int CreateEmpLoyee, int Key, string WorkNode, string FinishNode, string FileAddress,string Title)
        //{
        //    return WeddingMissionCreate(0, Type, MissionType, StarDate, EmployeeID, keyWords, ChannelType, CreateEmpLoyee, Key, WorkNode, FinishNode, FileAddress,Title);
        //}

        public int WeddingMissionCreate(int Type, int MissionType, DateTime StarDate, int EmployeeID, string keyWords, string ChannelType, int CreateEmpLoyee, int Key, string WorkNode, string FinishNode, string FileAddress)
        {
            return WeddingMissionCreate(0, Type, MissionType, StarDate, EmployeeID, keyWords, ChannelType, CreateEmpLoyee, Key, WorkNode, FinishNode, FileAddress, string.Empty);
        }

        public int WeddingMissionCreate(int Type, int MissionType, DateTime StarDate, int EmployeeID, string keyWords, string ChannelType, int CreateEmpLoyee, int Key, string WorkNode, string FinishNode)
        {
            return WeddingMissionCreate(0, Type, MissionType, StarDate, EmployeeID, keyWords, ChannelType, CreateEmpLoyee, Key, WorkNode, FinishNode, string.Empty, string.Empty);
        }

        public int WeddingMissionCreate(int Type, int MissionType, DateTime StarDate, int EmployeeID, string keyWords, string ChannelType, int CreateEmpLoyee, int Key)
        {
            return WeddingMissionCreate(0, Type, MissionType, StarDate, EmployeeID, keyWords, ChannelType, CreateEmpLoyee, Key, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public int WeddingMissionCreate(int CustomerID, int Type, int MissionType, DateTime StarDate, int EmployeeID, string keyWords, string ChannelType, int CreateEmpLoyee, int Key)
        {
            return WeddingMissionCreate(CustomerID, Type, MissionType, StarDate, EmployeeID, keyWords, ChannelType, CreateEmpLoyee, Key, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// 庆典相关任务创建
        /// </summary>
        /// <returns></returns>
        public int WeddingMissionCreate(int CustomerID, int Type, int MissionType, DateTime FinishDate, int EmployeeID, string keyWords, string ChannelType, int CreateEmpLoyee, int Key, string WorkNode, string FinishNode, string FileAddress, string Titles)
        {
            string Title = string.Empty;
            var MissionID = this.CheckInsert(Type, MissionType, DateTime.Now, EmployeeID, out Title);
            FL_MissionDetailed ObjModel = new FL_MissionDetailed();
            Channel ObjChannelBLL = new Channel();
            MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();
            Employee ObjEmployeeBLL = new Employee();
            ObjModel = ObjEntity.FL_MissionDetailed.FirstOrDefault(C => C.FinishKey == Key && C.MissionType == MissionType && C.KeyWords == keyWords);

            if (ObjModel == null)
            {
                ObjModel = new FL_MissionDetailed();
                Customers ObjCustomersBLL = new Customers();
                var ObjChannelModel = ObjChannelBLL.GetbyClassType(ChannelType);
                ObjModel.KeyWords = keyWords;

                ObjModel.StarDate = DateTime.Now;
                ObjModel.CreateDate = DateTime.Now;
                ObjModel.TypeName = Title;
                ObjModel.FinishStandard = FinishNode;
                ObjModel.WorkNode = WorkNode;
                ObjModel.MissionID = MissionID;
                ObjModel.ChecksState = 3;
                ObjModel.Type = Type;
                ObjModel.FinishDate = null;
                ObjModel.PlanDate = DateTime.Now.Date.AddDays(3);
                ObjModel.AppraiseLevel = -1;

                ObjModel.MissionState = 0;
                if (Type == 1)
                {
                    //var NewWords = keyWords.Replace("?","");
                    //var Arry = NewWords.Split('&');
                    //foreach (var Objitem in Arry)
                    //{
                    //    if (Objitem.Contains("CustomerID"))
                    //    { 

                    //    }
                    //}
                    if (CustomerID != 0)
                    {

                        ObjModel.MissionName = ObjCustomersBLL.GetByID(CustomerID).Bride + "的" + Title;
                        //ObjModel.FinishDate = ObjCustomersBLL.GetByID(CustomerID).PartyDate;
                    }
                    //DateTime.Now.ToShortDateString() + "-" + Title + "(" + (ObjEntity.FL_MissionDetailed.Count(C => C.MissionType == MissionType && C.EmpLoyeeID == EmployeeID) + 1) + ")";
                    ObjModel.Countdown = 3;
                    ObjModel.Emergency = "重要不紧急";

                }
                else
                {
                    if (CustomerID != 0)
                    {

                        ObjModel.MissionName = ObjCustomersBLL.GetByID(CustomerID).Bride + "的" + Title;
                        if (Titles != string.Empty)
                        {
                            ObjModel.MissionName = ObjCustomersBLL.GetByID(CustomerID).Bride + "的" + Title + Titles;
                        }
                        ObjModel.Countdown = 3;
                        ObjModel.Emergency = "重要不紧急";
                        //ObjModel.FinishDate = ObjCustomersBLL.GetByID(CustomerID).PartyDate;
                    }
                    //DateTime.Now.ToShortDateString() + "-" + Title + "(" + (ObjEntity.FL_MissionDetailed.Count(C => C.MissionType == MissionType && C.EmpLoyeeID == EmployeeID) + 1) + ")";
                    //ObjModel.Countdown = 7;
                    //ObjModel.Emergency = "重要不紧急";
                }
                ObjModel.IsDelete = false;
                ObjModel.IsOver = false;
                ObjModel.IsLook = true;
                ObjModel.FinishKey = Key;
                ObjModel.ChannelID = ObjChannelModel.ChannelID;
                ObjModel.EmpLoyeeID = EmployeeID;
                ObjModel.MissionType = MissionType;
                ObjModel.CreateEmployeeID = CreateEmpLoyee;
                ObjModel.CreateEmployeeName = ObjEmployeeBLL.GetByID(ObjModel.CreateEmployeeID).EmployeeName;
                ObjModel.ChecksEmployee = CreateEmpLoyee;
                ObjModel.CreateDate = DateTime.Now;
                ObjMissionDetailedBLL.Insert(ObjModel);
                if (FileAddress != string.Empty)
                {
                    MissionFile ObjMissionFile = new MissionFile();
                    FL_MissionFile ObjMissionFileModel = new FL_MissionFile();
                    ObjMissionFileModel.DetailedID = ObjModel.DetailedID;
                    ObjMissionFileModel.EmpLoyeeID = EmployeeID;
                    ObjMissionFileModel.FileAddress = FileAddress;
                    ObjMissionFileModel.FileName = "婚礼统筹图片";

                    ObjMissionFileModel.IsFinish = false;

                    ObjMissionFileModel.MissionID = 0;
                    ObjMissionFile.Insert(ObjMissionFileModel);
                }

                return ObjModel.DetailedID;

            }
            else
            {
                ObjModel = ObjEntity.FL_MissionDetailed.FirstOrDefault(C => C.FinishKey == Key && C.MissionType == MissionType);
                ObjModel = ObjMissionDetailedBLL.GetByID(ObjModel.DetailedID);
                ObjModel.EmpLoyeeID = EmployeeID;
                ObjModel.CreateEmployeeID = CreateEmpLoyee;
                ObjModel.CreateDate = DateTime.Now;
                ObjModel.PlanDate = FinishDate;
                ObjModel.MissionState = 0;
                ObjModel.AppraiseLevel = -1;
                ObjMissionDetailedBLL.Update(ObjModel);
                return ObjModel.DetailedID;
            }

            #region 注释
            //FL_MissionDetailed ObjModel = new FL_MissionDetailed();
            //Channel ObjChannelBLL = new Channel();
            //Employee EmployeeBLL = new Employee();
            //MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();
            //var ObjChannelModel = ObjChannelBLL.GetbyClassType(ChannelType);
            //var ExistMissionModel = ObjEntity.FL_MissionManager.FirstOrDefault(C => C.EmployeeID == EmployeeID && C.CreateDate == StarDate);
            //if (ExistMissionModel == null)
            //{

            //    var ObjMissIonModel = new FL_MissionManager();
            //    ObjMissIonModel.EmployeeID = EmployeeID;
            //    ObjMissIonModel.CreateDate = StarDate;
            //    ObjMissIonModel.IsDelete = false;
            //    ObjMissIonModel.MissionTitle = Title;
            //    ObjMissIonModel.CreateDate = StarDate;
            //    ObjMissIonModel.DepartmentID = EmployeeBLL.GetByID(ObjModel.EmpLoyeeID).DepartmentID;
            //    this.Insert(ObjMissIonModel);
            //    if (ObjChannelModel != null)
            //    {
            //        ObjMissionManagerModel.CreateDate = System.DateTime.Now;
            //        ObjMissionManagerModel.EmployeeID = EmployeeBLL.GetByID(ObjModel.EmpLoyeeID).DepartmentID;
            //        ObjMissionManagerModel.IsDelete = false;
            //        ObjMissionManagerModel.MissionTitle = Title;
            //        this.Insert(ObjMissionManagerModel);
            //        ObjModel.KeyWords = keyWords;
            //        ObjModel.MissionName = Title;
            //        ObjModel.StarDate = DateTime.Now;
            //        ObjModel.TypeName = TypeName;
            //        ObjModel.FinishStandard = TypeName;
            //        ObjModel.WorkNode = TypeName;
            //        ObjModel.MissionID = ObjMissIonModel.MissionID;
            //        ObjModel.ChecksState = 3;
            //        ObjModel.Type = 2;
            //        ObjModel.IsDelete = false;
            //        ObjModel.ChannelID = ObjChannelModel.ChannelID;
            //        ObjMissionDetailedBLL.Insert(ObjModel);
            //        return ObjModel.DetailedID;
            //    }
            //}
            //else
            //{
            //    if (ObjChannelModel != null)
            //    {
            //        ObjMissionManagerModel.CreateDate = System.DateTime.Now;
            //        ObjMissionManagerModel.EmployeeID = EmployeeBLL.GetByID(ObjModel.EmpLoyeeID).DepartmentID;
            //        ObjMissionManagerModel.IsDelete = false;
            //        ObjMissionManagerModel.MissionTitle = Title;
            //        this.Insert(ObjMissionManagerModel);
            //        ObjModel.KeyWords = keyWords;
            //        ObjModel.MissionName = Title;
            //        ObjModel.StarDate = DateTime.Now;
            //        ObjModel.TypeName = TypeName;
            //        ObjModel.FinishStandard = TypeName;
            //        ObjModel.WorkNode = TypeName;
            //        ObjModel.MissionID = ExistMissionModel.MissionID;
            //        ObjModel.ChecksState = 3;
            //        ObjModel.Type = 2;
            //        ObjModel.ChannelID = ObjChannelModel.ChannelID;
            //        ObjMissionDetailedBLL.Insert(ObjModel);
            //        return ObjModel.DetailedID;
            //    }
            //}
            //return 0;
            #endregion
        }
    }
    public class MissionSum
    {
        public int EmployeeID { get; set; }
        public int MissiionCount { get; set; }
        public int State { get; set; }
    }
}
