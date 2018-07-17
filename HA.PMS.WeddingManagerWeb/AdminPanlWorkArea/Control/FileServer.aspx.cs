using HA.PMS.DataAssmblly;
using System;
using System.IO;
using System.Web;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class FileServer : System.Web.UI.Page
    {


        bool IsExists = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------------------------
            //组件设置a.MD5File为2，3时 的实例代码
            if (Request.QueryString["access2008_cmd"] != null && Request.QueryString["access2008_cmd"] == "2")//服务器提交MD5验证后的文件信息进行验证
            {

                Response.Write("0");//返回命令  0 = 开始上传文件， 2 = 不上传文件，前台直接显示上传完成
                Response.End();
            }
            else if (Request.QueryString["access2008_cmd"] != null && Request.QueryString["access2008_cmd"] == "3") //服务器提交文件信息进行验证
            {
                Response.Write("1");//返回命令 0 = 开始上传文件,1 = 提交MD5验证后的文件信息进行验证, 2 = 不上传文件，前台直接显示上传完成
                Response.End();
            }
            //---------------------------------------------------------------------------------------------

            if (Request.Files["Filedata"] != null)//判断是否有文件上传上来
            {
                SaveFiles(System.Configuration.ConfigurationManager.AppSettings["FilesTemporary"]);
                Response.End();
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="url">保存路径,填写相对路径</param>
        /// <returns></returns>
        private void SaveFiles(string url)
        {
            ///'遍历File表单元素
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string NewfileName = string.Empty;
            ///'检查文件扩展名字
            //HttpPostedFile postedFile = files[iFile];
            HttpPostedFile postedFile = Request.Files["Filedata"]; //得到要上传文件
            string fileName, fileExtension, filesize, cadFileNameList;

            string URIAddress = string.Empty;

            cadFileNameList = string.Empty;
            fileName = System.IO.Path.GetFileName(postedFile.FileName.ToString()); //Guid.NewGuid().ToString() +  //得到文件名

            //            fileName = Guid.NewGuid().ToString() + System.IO.Path.GetFileName(postedFile.FileName.ToString()); //得到文件名
            filesize = System.IO.Path.GetFileName(postedFile.ContentLength.ToString()); //得到文件大小

            string PureRoute = string.Empty;
            if (fileName != "")
            {
                string ServerFloder = string.Empty;
                fileExtension = System.IO.Path.GetExtension(fileName);//'获取扩展名

                if (Request["Typer"].ToInt32() == 9)
                {
                    URIAddress = System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"] + Request.Cookies["FinishFloder"].Value + "/" + Request["FileName"].ToString() + "/";
                    PureRoute = URIAddress;
                }
                else if (Request["Typer"].ToInt32() == 10)
                {
                    URIAddress = System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"] + Request.Cookies["FinishFloder"].Value + "/" + Request["SystemTitle"].ToString() + "/";
                    PureRoute = URIAddress;
                }
                else
                {
                    URIAddress = System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"] + Request.Cookies["FinishFloder"].Value + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                }
                ServerFloder = Server.MapPath(URIAddress);
                string Floders = System.Web.HttpContext.Current.Request.MapPath(url);


                //注意：可能要修改你的文件夹的匿名写入权限。

                //文件夹 不存在  就新建文件夹
                if (!Directory.Exists(ServerFloder))
                {
                    Directory.CreateDirectory(ServerFloder);
                }

                if (!Directory.Exists(Floders))
                {
                    Directory.CreateDirectory(Floders);
                }

                postedFile.SaveAs(Floders + fileName);

                if (Request["Typer"].ToInt32() == 13)
                {

                    HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
                    Customers ObjCustomersBLL = new Customers();
                    var Model = ObjCustomersBLL.GetByID(ObjQuotedPriceBLL.GetByQuotedID(Request["QuotedID"].ToInt32()).CustomerID);

                    NewfileName = Model.Bride + "-" + Model.Groom + fileExtension;               //好评   名称为空   用新人的名字命名
                }
                else
                {
                    NewfileName = Guid.NewGuid().ToString() + fileExtension;// System.IO.Path.GetFileName(postedFile.FileName.ToString());
                }


                string NewName = (ServerFloder + NewfileName).ToString();
                if (File.Exists(NewName))
                {
                    File.Delete(NewName);
                }

                File.Move(Floders + fileName, ServerFloder + NewfileName);
                URIAddress = URIAddress + NewfileName;


            }

            SavetoDataBase(URIAddress, fileName, PureRoute);
        }



        /// <summary>
        /// 保存到数据库
        /// </summary>
        private void SavetoDataBase(string Address, string Filename, string PureRoute)
        {
            int DataBaseType = int.Parse(Request.QueryString["Typer"]);
            HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
            var FileAddress = Address;
            FL_QuotedPricefileManager ObjFileModel = new FL_QuotedPricefileManager();
            switch (DataBaseType)
            {
                case 1://报价单
                    ObjFileModel.CreateDate = DateTime.Now;
                    ObjFileModel.FileAddress = FileAddress;
                    ObjFileModel.Filename = Filename;
                    ObjFileModel.QuotedID = Request["QuotedID"].ToInt32();
                    ObjFileModel.SortOrder = 1;
                    ObjFileModel.KindID = Request["Kind"].ToInt32();
                    ObjFileModel.Type = 1;
                    ObjQuotedPriceBLL.QuotedPricefileManagerInsert(ObjFileModel);
                    break;
                case 2://报价单提案
                    ObjFileModel.CreateDate = DateTime.Now;
                    ObjFileModel.FileAddress = FileAddress;
                    ObjFileModel.Filename = Filename;
                    ObjFileModel.QuotedID = Request["QuotedID"].ToInt32();
                    ObjFileModel.SortOrder = 1;
                    ObjFileModel.KindID = 0;//Request["Kind"].ToInt32();
                    ObjFileModel.Type = 2;
                    ObjQuotedPriceBLL.QuotedPricefileManagerInsert(ObjFileModel);

                    var ObjQuotedModel = ObjQuotedPriceBLL.GetByID(ObjFileModel.QuotedID);
                    ObjQuotedModel.FileCheck = 2;
                    ObjQuotedPriceBLL.Update(ObjQuotedModel);

                    break;
                case 3:
                    MissionFile ObjMissionFile = new MissionFile();
                    FL_MissionFile ObjMissionFileModel = new FL_MissionFile();
                    ObjMissionFileModel.DetailedID = Request["DetailedID"].ToInt32();
                    ObjMissionFileModel.EmpLoyeeID = User.Identity.Name.ToInt32();
                    ObjMissionFileModel.FileAddress = FileAddress;
                    ObjMissionFileModel.FileName = Filename;
                    if (Request["FinishType"] == "1")
                    {
                        ObjMissionFileModel.IsFinish = true;
                    }
                    else
                    {

                        ObjMissionFileModel.IsFinish = false;
                    }
                    ObjMissionFileModel.MissionID = Request["MissionID"].ToInt32();
                    ObjMissionFile.Insert(ObjMissionFileModel);
                    //DetailedID
                    break;
                case 4:
                    Orderfile ObjOrderfileBLL = new Orderfile();
                    FL_Orderfile ObjOrderfileModel = new FL_Orderfile();
                    Order ObjOrderBLL = new Order();
                    ObjOrderfileModel.OrderID = Request["OrderID"].ToInt32();
                    ObjOrderfileModel.Filename = Filename;
                    ObjOrderfileModel.FIleAddress = FileAddress;
                    ObjOrderfileModel.CreateDate = DateTime.Now;
                    ObjOrderfileModel.EmployeeID = User.Identity.Name.ToInt32(); ;

                    ObjOrderfileBLL.Insert(ObjOrderfileModel);
                    var ObjUpdateModel = ObjOrderBLL.GetByID(ObjOrderfileModel.OrderID);
                    ObjUpdateModel.FileCheck = false;
                    ObjOrderBLL.Update(ObjUpdateModel);

                    break;

                case 5:
                    //执行表图片
                    DispatchingImage ObjDispatchingImageBLL = new DispatchingImage();
                    FL_DispatchingImage ObjDispatchingImageModel = new FL_DispatchingImage();

                    ObjDispatchingImageModel.DispatchingID = Request["DispatchingID"].ToInt32();
                    ObjDispatchingImageModel.Filename = Filename;
                    ObjDispatchingImageModel.FileAddress = FileAddress;
                    ObjDispatchingImageModel.CreateDate = DateTime.Now;
                    ObjDispatchingImageModel.KindID = Request["Kind"].ToInt32();
                    ObjDispatchingImageModel.SortOrder = 0;
                    ObjDispatchingImageModel.Type = 0;


                    ObjDispatchingImageBLL.Insert(ObjDispatchingImageModel);

                    break;

                case 6:
                    //婚礼统筹文件
                    WeddingPlanFile ObjWeddingPlanFileBLL = new WeddingPlanFile();
                    FL_WeddingPlanFile ObjWeddingPlanFileModel = new FL_WeddingPlanFile();

                    ObjWeddingPlanFileModel.PlanningID = Request["PlanningID"].ToInt32();
                    ObjWeddingPlanFileModel.FileName = Filename;
                    ObjWeddingPlanFileModel.FileAddress = FileAddress;
                    ObjWeddingPlanFileModel.CreateDate = DateTime.Now;
                    ObjWeddingPlanFileModel.FileType = Request["Kind"].ToInt32();

                    ObjWeddingPlanFileBLL.Insert(ObjWeddingPlanFileModel);

                    break;
                case 7:
                    //FD_QuotedCatgory ObjQuotedCatogryModel = new FD_QuotedCatgory();
                    QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();
                    var ObjQcModel = ObjQuotedCatgoryBLL.GetByID(Request["QcKey"].ToInt32());

                    ObjQcModel.MediaFileList += "☆" + Address + "★" + Filename;

                    ObjQuotedCatgoryBLL.Update(ObjQcModel);

                    break;
                case 8:
                    //婚礼统筹文件
                    DesignUpload ObjDesignUploadBLL = new DesignUpload();
                    FL_DesignUpload ObjDesignUploadModel = new FL_DesignUpload();

                    ObjDesignUploadModel.CreateDate = DateTime.Now.ToString().ToDateTime();
                    ObjDesignUploadModel.FileName = Filename;
                    ObjDesignUploadModel.FileAddress = FileAddress;
                    ObjDesignUploadModel.Type = Request["Type"].ToInt32();
                    ObjDesignUploadModel.DesignId = Request["DesignClassID"].ToInt32();
                    ObjDesignUploadModel.OrderId = Request["OrderID"].ToInt32();
                    ObjDesignUploadModel.Remark = "";
                    ObjDesignUploadBLL.Insert(ObjDesignUploadModel);
                    break;
                case 9:
                    //公司文件资料
                    CompanyFile ObjFileBLL = new CompanyFile();
                    CA_CompanyFile FileModels = ObjFileBLL.GetByID(Request["FileID"].ToInt32());        //修改纯路径(文件夹路径  删除时 所需要)
                    if (FileModels.PureRoute == null)           //文件夹路径为null时  需要赋值
                    {
                        FileModels.PureRoute = System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"] + Request.Cookies["FinishFloder"].Value + "/" + Request["FileName"].ToString();
                        ObjFileBLL.Update(FileModels);
                    }

                    CA_CompanyFile FileModel = ObjFileBLL.GetByName(Filename, Request["FileID"].ToInt32());
                    if (FileModel == null)          //不存在 新增
                    {
                        FileModel = new CA_CompanyFile();
                        FileModel.FileName = Filename;
                        FileModel.FileURL = FileAddress;
                        FileModel.ItemLevel = 2;
                        FileModel.ParentFileId = Request["FileID"].ToInt32();
                        FileModel.IsDelete = false;
                        FileModel.PureRoute = PureRoute.ToString();
                        FileModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                        FileModel.CreateEmployee = User.Identity.Name.ToInt32();
                        FileModel.Remark = "";
                        ObjFileBLL.Insert(FileModel);
                    }
                    else        //已存在  先删除真实文件 再保存  修改
                    {
                        File.Delete(Server.MapPath(FileModel.FileURL));      //上传新的文件  命名重复  覆盖(实际执行的删除操作)
                        FileModel.FileURL = FileAddress;
                        FileModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                        ObjFileBLL.Update(FileModel);
                    }
                    break;
                case 10:
                    //公司制度
                    CompanySystem ObjSystemBLL = new CompanySystem();
                    CA_CompanySystem SystemModels = ObjSystemBLL.GetByID(Request["SystemID"].ToInt32());        //修改纯路径(文件夹路径  删除时 所需要)
                    if (SystemModels.SystemPureRoute == "" || SystemModels.SystemPureRoute == null)           //文件夹路径为null时  需要赋值
                    {
                        SystemModels.SystemPureRoute = System.Configuration.ConfigurationManager.AppSettings["FilesServerFloder"] + Request.Cookies["FinishFloder"].Value + "/" + Request["SystemTitle"].ToString();
                        ObjSystemBLL.Update(SystemModels);
                    }

                    CA_CompanySystem SystemModel = ObjSystemBLL.GetByName(Filename, Request["SystemID"].ToInt32());
                    if (SystemModel == null)          //不存在 新增
                    {
                        SystemModel = new CA_CompanySystem();
                        SystemModel.SystemTitle = Filename;
                        SystemModel.SystemURL = FileAddress;
                        SystemModel.ParentID = Request["SystemID"].ToInt32();
                        SystemModel.IsDelete = false;
                        SystemModel.SystemPureRoute = PureRoute.ToString();
                        SystemModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                        SystemModel.CreateEmployee = User.Identity.Name.ToInt32();
                        SystemModel.Type = Request["FileType"].ToInt32();
                        SystemModel.Remark = "";
                        ObjSystemBLL.Insert(SystemModel);
                    }
                    else        //已存在  先删除真实文件 再保存  修改
                    {
                        File.Delete(Server.MapPath(SystemModel.SystemURL));      //上传新的文件  命名重复 覆盖(实际执行的删除操作) 
                        SystemModel.SystemURL = FileAddress;
                        SystemModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
                        ObjSystemBLL.Update(SystemModel);
                    }
                    break;
                case 11:
                    //个人档案
                    EmployeeDataFile ObjDataFileBLL = new EmployeeDataFile();
                    sys_EmployeeDataFile DataModel = new sys_EmployeeDataFile();
                    DataModel.DataName = Filename;
                    DataModel.DataUrl = FileAddress;
                    DataModel.CreateEmployee = User.Identity.Name.ToInt32();
                    DataModel.CreateDate = DateTime.Now.ToString().ToDateTime();
                    DataModel.EmployeeID = Request["EmployeeID"].ToInt32();
                    ObjDataFileBLL.Insert(DataModel);
                    break;

                case 12:
                    //Bug文件
                    BugFile ObjBugFileBLL = new BugFile();
                    sys_BugFile BugModel = new sys_BugFile();
                    BugModel.FileName = Filename;
                    BugModel.FileUrl = FileAddress;
                    BugModel.FilePureRoute = PureRoute;
                    BugModel.BugID = Request["BugID"].ToInt32();
                    BugModel.CreateEmployee = User.Identity.Name.ToInt32();
                    BugModel.CreateDate = DateTime.Now;
                    BugModel.IsDelete = false;
                    ObjBugFileBLL.Insert(BugModel);
                    break;
                case 13:
                    //上传好评
                    int QuotedID = Request["QuotedID"].ToInt32();
                    var QuoedModel = ObjQuotedPriceBLL.GetByID(QuotedID);
                    HA.PMS.Pages.SystemPage ObjSystem = new Pages.SystemPage();

                    string Name = ObjSystem.GetCustomerName(QuoedModel.CustomerID);

                    Customers ObjCustomerBLL = new Customers();
                    var CustomerModel = ObjCustomerBLL.GetByID(QuoedModel.CustomerID);
                    QuoedModel.Praise = FileAddress;
                    ObjQuotedPriceBLL.Update(QuoedModel);
                    break;
                default: break;

            }
        }

    }
}