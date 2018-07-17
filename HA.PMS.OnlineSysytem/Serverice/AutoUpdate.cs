using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace HA.PMS.OnlineSysytem.Serverice
{
    public class AutoUpdate
    {
        PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();


        public void CreateSince()
        {

            var ObjModel = ObjEntity.Sys_Employee.FirstOrDefault(C => C.LoginName == "admin");

            if (ObjModel == null)
            {
                ObjEntity.Sys_EmployeeType.Add(new Sys_EmployeeType()
                {
                    Type = "系统管理员",
                    IsDelete = false,
                    EmployeeId = 1,
                    CreateTime = DateTime.Now
                });
                ObjEntity.SaveChanges();


                ObjEntity.Sys_Workgroups.Add(new Sys_Workgroups()
                {
                    GroupName = "系统管理部"



                });

                ObjEntity.SaveChanges();


                ObjEntity.Sys_EmployeeJob.Add(new Sys_EmployeeJob()
                {
                    Jobname = "系统管理员",
                    IsDelete = false,
                    createTime = DateTime.Now,
                    EmployeeId = 1
                });

                ObjEntity.SaveChanges();


                ObjEntity.Sys_Department.Add(new Sys_Department()
                {
                    DepartmentName = "系统管理部",
                    IsDelete = false,
                    createTime = DateTime.Now,
                    Parent = 0,
                    DepartmentManager = 1,
                    Brand = "1",
                    ItemLevel = 1,
                    DataSource = 1,
                    EmployeeID = 1,


                });

                ObjEntity.SaveChanges();


                ObjEntity.Sys_Employee.Add(new Sys_Employee()
                {
                    JobID = 1,
                    DepartmentID = 1,
                    GroupID = 1,
                    EmployeeTypeID = 1,
                    EmployeeName = "admin",
                    PassWord = "E10ADC3949BA59ABBE56E057F20F883E",
                    CreateDate = DateTime.Now,
                    LoginYear = DateTime.Now.Year,
                    LoginMonth = DateTime.Now.Month,
                    LoginDay = DateTime.Now.DayOfYear,
                    Sex = 1,
                    BornDate = DateTime.Now,
                    IsClose = false,
                    Employeekey = "2013",
                    TelPhone = "2013",
                    QQ = "2013",
                    Email = "2013",
                    PlanChecks = 2013,
                    Coach = 2013,
                    Look = 2013,
                    IsDelete = false,
                    LoginName = "admin"
                });

                ObjEntity.SaveChanges();


                string Floder = Environment.CurrentDirectory;

                //System.IO.StreamReader ObjChannelReader = new StreamReader(Floder + "\\ChannelData.txt");
                //string AChannel = string.Empty;
                //while (!string.IsNullOrEmpty((AChannel = ObjChannelReader.ReadLine())))
                //{
                //    var Arry = AChannel.Trim().Split('$');
                //    ObjEntity.Sys_Channel.Add(new Sys_Channel() { ChannelID = int.Parse(Arry[0]), Parent = int.Parse(Arry[1]), ChannelName = Arry[2], ChannelAddress = Arry[3], ChannelGetType = Arry[4], StyleSheethem = Arry[5], IsMenu = bool.Parse(Arry[6]), CreateDate = DateTime.Now, IsPublic = bool.Parse(Arry[7]) });
                //    ObjEntity.SaveChanges();
                //    AChannel = string.Empty;
                //}

                //注入频道
                //2$频道管理$/AdminPanlWorkArea/Sys/Jurisdiction/Sys_ChannelManager.aspx$2013/7/22$49$True,Sys_ChannelManager$频道管理0$False




                //辅助文件
                //ObjEntity.FL_OrderReference.Add(new FL_OrderReference() { CreateDate = DateTime.Now, CreateEmployeeID = 1, State = 200, StuContent = "", UpdateDate = DateTime.Now });
                //ObjEntity.SaveChanges();
                ObjEntity.FL_OrderReference.Add(new FL_OrderReference() { CreateDate = DateTime.Now, CreateEmployeeID = 1, State = 200, StuContent = "", UpdateDate = DateTime.Now });
                ObjEntity.SaveChanges();
                ObjEntity.FL_OrderReference.Add(new FL_OrderReference() { CreateDate = DateTime.Now, CreateEmployeeID = 1, State = 201, StuContent = "", UpdateDate = DateTime.Now });
                ObjEntity.SaveChanges();
                ObjEntity.FL_OrderReference.Add(new FL_OrderReference() { CreateDate = DateTime.Now, CreateEmployeeID = 1, State = 202, StuContent = "", UpdateDate = DateTime.Now });
                ObjEntity.SaveChanges();
                ObjEntity.FL_OrderReference.Add(new FL_OrderReference() { CreateDate = DateTime.Now, CreateEmployeeID = 1, State = 203, StuContent = "", UpdateDate = DateTime.Now });
                ObjEntity.SaveChanges();

                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 29, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 186, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 187, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 189, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 190, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 191, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 192, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 193, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 194, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 195, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 196, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 199, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 200, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();
                //ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 201, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                //ObjEntity.SaveChanges();

                ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 2, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                ObjEntity.SaveChanges();

                ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 6, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                ObjEntity.SaveChanges();

                ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 8, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                ObjEntity.SaveChanges();

                ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 9, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                ObjEntity.SaveChanges();

                ObjEntity.Sys_UserJurisdiction.Add(new Sys_UserJurisdiction() { ChannelID = 49, EmployeeID = 1, DepartmentID = 1, DataPower = 1, DataPowerMd5Key = string.Empty, IsClose = false });
                ObjEntity.SaveChanges();


                //目标
                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 43, ChannelName = "渠道管理", TargetTitle = "有效信息数", TargetType = 1, Unite = "个", Remark = "渠道来源的非无效信息数量" });
                ObjEntity.SaveChanges();



                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 43, ChannelName = "渠道管理", TargetTitle = "客源有效率", TargetType = 2, Unite = "%", Remark = "客源有效率 基本可以确定为邀约中" });
                ObjEntity.SaveChanges();



                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 45, ChannelName = "邀约新人", TargetTitle = "邀约新人", TargetType = 1, Unite = "个", Remark = "客源有效率 基本可以确定为邀约中" });
                ObjEntity.SaveChanges();


                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 29, ChannelName = "邀约分派", TargetTitle = "邀约成功率", TargetType = 2, Unite = "%", Remark = "邀约分派" });
                ObjEntity.SaveChanges();


                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 46, ChannelName = "销售跟单", TargetTitle = "成功预定数", TargetType = 1, Unite = "", Remark = "成功预定数" });
                ObjEntity.SaveChanges();


                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 46, ChannelName = "渠道管理", TargetTitle = "成功预订率", TargetType = 2, Unite = "个", Remark = "成功预订率" });
                ObjEntity.SaveChanges();


                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 47, ChannelName = "策划报价", TargetTitle = "当期执行订单总额", TargetType = 1, Unite = "元", Remark = "当期执行订单总额" });
                ObjEntity.SaveChanges();

                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 47, ChannelName = "策划报价", TargetTitle = "毛利率", TargetType = 2, Unite = "%", Remark = "毛利率" });
                ObjEntity.SaveChanges();


                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 47, ChannelName = "策划报价", TargetTitle = "当期新增订单总额", TargetType = 1, Unite = "个", Remark = "当期新增订单总额" });
                ObjEntity.SaveChanges();

                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 48, ChannelName = "订单管理", TargetTitle = "执行订单数", TargetType = 1, Unite = "个", Remark = "" });
                ObjEntity.SaveChanges();

                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 48, ChannelName = "订单管理", TargetTitle = "满意度", TargetType = 2, Unite = "%", Remark = "" });
                ObjEntity.SaveChanges();


                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 48, ChannelName = "订单管理", TargetTitle = "投诉率", TargetType = 2, Unite = "%", Remark = "" });
                ObjEntity.SaveChanges();

                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 116, ChannelName = "新人满意度调查", TargetTitle = "执行订单总数", TargetType = 1, Unite = "个", Remark = "" });
                ObjEntity.SaveChanges();

                ObjEntity.FL_Target.Add(new FL_Target() { ChannelID = 175, ChannelName = "财务管理", TargetTitle = "订单毛利率", TargetType = 1, Unite = "%", Remark = "" });
                ObjEntity.SaveChanges();
            }



            //StreamReader ObjReader = new StreamReader(Floder + "\\ChannelData.txt");
            //string Line=string.Empty;
            //while (!string.IsNullOrEmpty((Line = ObjReader.ReadLine())))
            //{
            //    var ObjLineArry=Line.Split('$');
            //    ObjEntity.Sys_Channel.Add(new Sys_Channel()
            //    {
            //        ChannelID = int.Parse(ObjLineArry[0]),
            //        ChannelName = ObjLineArry[1],
            //        ChannelAddress = ObjLineArry[2],
            //        CreateDate = DateTime.Now,
            //        Parent = int.Parse(ObjLineArry[4]),
            //        IsMenu=bool.Parse(ObjLineArry[5]),
            //        ChannelGetType=ObjLineArry[6],
            //        StyleSheethem=ObjLineArry[7],
            //        OrderCode=int.Parse(ObjLineArry[8]),
            //        IsPublic=false

            //    });

            //}

            // ObjReader.Close();

            //            DepartmentID	DepartmentName	Parent	SortOrder	createTime	EmployeeID	IsDelete	DepartmentManager	Brand	ItemLevel	DataSource
            //1	系统管理	0	NULL	2013-06-01 00:00:00.000	1	0	1	1	1	1
        }
    }
}
