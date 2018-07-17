using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Web;


namespace HA.PMS.ToolsLibrary
{
    public class SystemConfig
    {
      
        ///// <summary>
        ///// 根据配置文件设置
        ///// </summary>
        ///// <returns></returns>
        //public static string SetStyleforSystemControlKey(string Key)
        //{
          
        //    string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "AdminPanlWorkArea\\SysConfig.ini";

        //    StreamReader Objreader = new StreamReader(ConfigPath);

        //    string COnfigContent = Objreader.ReadToEnd();

        //    Objreader.Close();

        //    List<string> ObjConfigList = COnfigContent.Split(',').ToList();
        //    string Config = ObjConfigList.FirstOrDefault(C => C.Contains(Key));

        //    if (Config != null)
        //    {

        //        if (Config.Split('=')[1] == "1")
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return "style=\"display: none;\"";
        //        }

        //    }
        //    else
        //    {
        //        return "style=\"display: none;\"";
        //    }

        //}

        ///// <summary>
        ///// 直接移除控件
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="Typers"></param>
        ///// <returns></returns>
        //public static string SetStyleforSystemControlKey(string Key, int Typers)
        //{

        //    string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "AdminPanlWorkArea\\SysConfig.ini";

        //    StreamReader Objreader = new StreamReader(ConfigPath);

        //    string COnfigContent = Objreader.ReadToEnd();

        //    Objreader.Close();

        //    List<string> ObjConfigList = COnfigContent.Split(',').ToList();
        //    string Config = ObjConfigList.FirstOrDefault(C => C.Contains(Key));

        //    if (Config != null)
        //    {

        //        if (Config.Split('=')[1] == "1")
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return "class=\"RemoveClass\"";
        //        }

        //    }
        //    else
        //    {
        //        return "class=\"RemoveClass\"";
        //    }

        //}

        ///// <summary>
        ///// 是否需要这个模块
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <returns></returns>
        //public static bool GetNeedMamagerByKey(string Key)
        //{


        //    string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "AdminPanlWorkArea\\SysConfig.ini";

        //    StreamReader Objreader = new StreamReader(ConfigPath);

        //    string COnfigContent = Objreader.ReadToEnd();

        //    Objreader.Close();

        //    List<string> ObjConfigList = COnfigContent.Split(',').ToList();
        //    string Config = ObjConfigList.FirstOrDefault(C => C.Contains(Key));

        //    if (Config != null)
        //    {

        //        if (Config.Split('=')[1] == "1")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

    }


}
