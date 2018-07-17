using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace HA.PMS.ToolsLibrary
{
    public static class StringControl
    {
        /// <summary>
        /// 尝试将字符串转换为数字如字符串非法则返回0
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static int ToInt32(this string Value)
        {
            int IntValue=0;
            int.TryParse(Value, out IntValue);
            return IntValue;
            
        }


        /// <summary>
        /// 尝试将字符串转换为十进制数如字符串非法则返回0
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string Value)
        {
            decimal ReturnValue = 0;
            decimal.TryParse(Value, out ReturnValue);
            return ReturnValue;
        }

        /// <summary>
        /// 尝试将字符串转换为十进制数如字符串非法则返回0
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static double ToDouble(this string Value)
        {
            double ReturnValue = 0;
            double.TryParse(Value, out ReturnValue);
            return ReturnValue;
        }



        /// <summary>
        /// 尝试将字符串转换为时间格式 如果字符串非法则返回最小时间
        /// </summary>
        /// <param name="Value"></param>
        public static DateTime ToDateTime(this string Value)
        {
            if(string.IsNullOrEmpty(Value))
            {
             return DateTime.Now;
            }
            DateTime ReturnValue =DateTime.Now;
            DateTime.TryParse(Value, out ReturnValue);
            return ReturnValue;
        }

 

        /// <summary>
        /// 尝试将字符串转换GUID 如果不能转换则返回Guid.Empty
        /// </summary>
        /// <param name="Value"></param>
        public static Guid ToGuid(this string Value)
        {
            Guid ReturnValue = Guid.Empty;
            Guid.TryParse(Value, out ReturnValue);
            return ReturnValue;
        }


        /// <summary>
        /// 转换字符串为BOOL类型
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool ToBool(this string Value)
        {
            bool ReturnValue =false;
            bool.TryParse(Value, out ReturnValue);
            return ReturnValue;
        }

        /// <summary>
        /// 根据下标获取前N位字符串
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static string GetStringByLeftIndex(this string Value, int Index)
        {
            if (Value.Length <= Index)
            {
                return Value.Substring(0,Index-1);
            }
            else
            {

                return Value;
            }
        }

        /// <summary>
        /// 根据下标获取后N位
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static string GetStringByRightIndex(this string Value, int Index)
        {
            if (Value.Length <= Index)
            {
                return Value.Substring(Index, Index - 1);
            }
            else
            {

                return Value;
            }
        }



        /// <summary>
        /// 将字符串转换成完整TXT
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToTxt(this string s)
        {
            return s.Replace("\r\n", "</br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
        }


        public static bool IsNotNullContains(this string s,string ClassFire)
        {
            if (s == null)
            {
                return false;
            }
            else
            {
                if (!s.Contains(ClassFire))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        ///<summary>
        ///把一个字符串截取成数据返回
        ///</summary>
        ///<param name="s">目标字符串</param>
        ///<param name="sep">分割标示符</param>
        ///<returns>返回一个分割出来的数组</returns>
        public static string[] ToArray(this string s, string sep)
        {
            int sepLen = sep.Length;
            ArrayList arr = new ArrayList();
            string item;
            int pos;

            if (sep != "")
            {
                while (true)
                {
                    pos = s.IndexOf(sep);
                    if (pos > 0)
                    {
                        item = s.Substring(0, pos);
                        arr.Add(item);
                        if (sepLen + pos >= s.Length) break;
                        s = s.Substring(sepLen + pos, s.Length - sepLen - pos);
                    }
                    else if (pos == 0)
                    {
                        if (sepLen >= s.Length) break;
                        s = s.Substring(sepLen, s.Length - sepLen);
                    }
                    else
                    {
                        if (s.Length > 0) arr.Add(s);
                        break;
                    }
                }
            }
            else
            {
                char[] arr_str = s.ToCharArray();
                for (int i = 0; i < arr_str.GetLength(0); i++)
                {
                    arr.Add(arr_str[i].ToString());
                }
            }

            string[] result = new string[arr.Count];
            arr.CopyTo(result);
            return result;
        }

        /// <summary>
        /// 将字符串转化成Byte[]
        /// </summary>
        public static Byte[] ToByteArray(this string s)
        {
            return Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(s)));
        }

        /// <summary>
        /// 将Byte[]转化成字符串
        /// </summary>
        public static string ToByteString(this byte[] s)
        {
            return BitConverter.ToString(s);
        }

        /// <summary>
        /// 对字符串进行MD5加密
        /// </summary>
        public static string MD5Hash(this string Value)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(Value, "MD5");
        }



        /// <summary>
        /// 生成数据MD5 防止手动录入数据
        /// 
        /// </summary>
        /// <param name="StarKey"></param>
        /// <param name="EndKey"></param>
        /// <returns></returns>
        public static string Md5forDataRow(this string StarKey)
        {

            return FormsAuthentication.HashPasswordForStoringInConfigFile(StarKey + GetSoftKey(), "MD5");

        }


        /// <summary>
        /// 获取软件序列号
        /// </summary>
        /// <returns></returns>
        public static string GetSoftKey()
        {
            return "HAOA2013";
        }

    }
}
