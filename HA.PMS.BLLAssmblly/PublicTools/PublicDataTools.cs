using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.BLLAssmblly.PublicTools
{
    public static class PublicDataTools<T> where T : class
    {


        /// <summary>
        /// 多条件获取数据
        /// </summary>
        /// <param name="ObjEntity"></param>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        [Obsolete("此方法已经过时，不再使用")]
        public static List<T> GetDataByParameter(T ObjEntity, ObjectParameter[] ObjParameterList)
        {

            return GetDataByPower(ObjEntity, 0, 0, ObjParameterList);
        }

        /// <summary>
        /// 多条件获取数据加数据权限
        /// </summary>
        /// <param name="ObjEntity"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="ChannelID"></param>
        /// <param name="ObjParameterList"></param>
        /// <returns></returns>
        [Obsolete("此方法已经过时，不再使用")]
        public static List<T> GetDataByParameter(T ObjEntity, int? EmployeeID, int? ChannelID, ObjectParameter[] ObjParameterList)
        {
            return GetDataByPower(ObjEntity, EmployeeID, ChannelID, ObjParameterList);

        }

        #region 根据条件 分页查询
        //  GetDataByPower(T ObjEntity, int? EmployeeID, int? ChannelID, ObjectParameter[] ObjParameterList)
        /// <summary>
        /// 根据复杂条件获取数据
        /// </summary>
        /// <param name="ObjEntity">需要的实体</param>
        /// <param name="ObjParameterList">参数集合</param>
        ///OrderByCloumname排序字段
        /// <returns></returns>
        public static List<T> GetDataByWhereParameter(List<PMSParameters> ObjParameterList, string OrderByCloumname, int PageSize, int PageIndex, out int SourceCount, OrderType GroupBy = OrderType.Desc)
        {
            string WhereStr = GetWhere(ObjParameterList);

            string CountSql = " Select count(*) from " + typeof(T).Name + " as c where 1=1 " + WhereStr.Replace("{", "(").Replace("}", ")").Replace("System.Boolean", "bit").Replace("False", "'0'").Replace("True", "'1'");

            //CountSql = CountSql.Replace("System.DateTime", "date");
            CountSql = CountSql.Replace("cast(", "").Replace("as System.DateTime)", "");
            PMS_WeddingEntities ObjContex = new PMS_WeddingEntities();
            var SourctcountResualt = ObjContex.Database.SqlQuery<int>(CountSql, new List<ObjectParameter>().ToArray()).First();
            SourceCount = SourctcountResualt;

            using (
                EntityConnection ObjEntityconn = new EntityConnection("name=PMS_WeddingEntities"))
            {
                List<ObjectParameter> ObjListparmeter = new List<ObjectParameter>();
                ObjectContext ObjDataContext = new ObjectContext(ObjEntityconn);
                PageIndex = PageIndex - 1;
                ObjListparmeter.Add(new ObjectParameter("skip", PageIndex * PageSize));
                ObjListparmeter.Add(new ObjectParameter("limit", PageSize));
                List<T> ObjList = new List<T>();
                string RunSql = "Select VALUE c from PMS_WeddingEntities." + typeof(T).Name + " as c where 1=1 " + WhereStr + " order by c." + OrderByCloumname + " " + GroupBy.ToString() + " SKIP @skip LIMIT @limit ";


                var ObjReturnList = ObjDataContext.CreateQuery<T>(RunSql, ObjListparmeter.ToArray());
                ObjList = ObjReturnList.ToList();
                ObjEntityconn.Close();

                return ObjList;
            }
        }
        #endregion

        #region 构造where语句
        /// <summary>
        /// 构造where语句
        /// </summary>
        /// <param name="ObjParameter"></param>
        /// Needmanager是否需要查询下级
        /// <returns></returns>
        public static string GetWhere(List<PMSParameters> ObjParameterList)
        {
            StringBuilder SqlWhere = new StringBuilder();
            foreach (var ObjParameter in ObjParameterList)
            {
                //如果需要查询我所管理的下级则查询
                if (ObjParameter.IsContainsManagedEmployee)
                {
                    SqlWhere.Append(" and c." + ObjParameter.Name + " " + NSqlTypes.IN.ToString() + Employee.GetMyManagerEmpLoyee(ObjParameter.Value.ToString().ToInt32(), string.Empty));
                    continue;
                }

                switch (ObjParameter.Type)
                {
                    //模糊查询
                    case NSqlTypes.LIKE:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " Like '%" + ObjParameter.Value + "%'");
                        break;
                    case NSqlTypes.DateBetween://时间段之间
                        //if (ObjParameter.Value.ToString().Split(',')[1].ToDateTime().Year == 0001 || ObjParameter.Value.ToString().Split(',')[1].ToDateTime().Year == 9999)
                        //{
                        //    SqlWhere.Append(" and c." + ObjParameter.Name + " >= cast('" + ObjParameter.Value.ToString().Split(',')[0].ToDateTime().ToString("yyyy-MM-dd 00:00:00") +"' as System.DateTime) and c." + ObjParameter.Name + "<=cast('" + ObjParameter.Value.ToString().Split(',')[1].ToDateTime().AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "' as System.DateTime)");
                        //}
                        //else
                        //{
                        SqlWhere.Append(" and c." + ObjParameter.Name + " >= cast('" + ObjParameter.Value.ToString().Split(',')[0].ToDateTime().ToString("yyyy-MM-dd 00:00:00") + "' as System.DateTime) and c." + ObjParameter.Name + "<=cast('" + ObjParameter.Value.ToString().Split(',')[1].ToDateTime().ToString("yyyy-MM-dd 23:59:59") + "' as System.DateTime)");
                        //}
                        break;
                    case NSqlTypes.OrDateBetween://时间段之间

                        SqlWhere.Append(" or c." + ObjParameter.Name + " >= cast('" + ObjParameter.Value.ToString().Split(',')[0].ToDateTime().ToString("yyyy-MM-dd 00:00:00") + "' as System.DateTime) and c." + ObjParameter.Name + "<=cast('" + ObjParameter.Value.ToString().Split(',')[1].ToDateTime().ToString("yyyy-MM-dd 23:59:59") + "' as System.DateTime)");

                        break;
                    case NSqlTypes.DateEquals:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " >= cast('" + ObjParameter.Value.ToString().ToDateTime().ToString("yyyy-MM-dd 00:00:00") + "' as System.DateTime)");
                        break;
                    //大于某个值
                    case NSqlTypes.Greaterthan:
                        if (ObjParameter.Name == "NoPayMent")
                        {
                            SqlWhere.Append(" and c." + ObjParameter.Name + " > " + ObjParameter.Value);
                        }
                        else
                        {
                            SqlWhere.Append(" and c." + ObjParameter.Name + " >= " + ObjParameter.Value);
                        }
                        break;
                    case NSqlTypes.NumLessThan:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " <= " + ObjParameter.Value);
                        break;
                    case NSqlTypes.LessThan:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " <= cast('" + ObjParameter.Value.ToString().ToDateTime().ToString("yyyy-MM-dd 00:00:00") + "' as System.DateTime)");
                        //SqlWhere.Append(" and c." + ObjParameter.Name + " <= " + ObjParameter.Value);
                        break;
                    case NSqlTypes.NumIn:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " in { " + ObjParameter.Value + " }");
                        break;
                    case NSqlTypes.OR:
                        SqlWhere.Append(" or c." + ObjParameter.Name + " = '" + ObjParameter.Value + "'");
                        break;
                    case NSqlTypes.AndLike:
                        SqlWhere.Append(" and (c." + ObjParameter.Name + " like '%" + ObjParameter.Value + "%'");
                        break;
                    case NSqlTypes.OrsLike:     //模式 objParmList.Add("Bride", txtName.Text + ",", NSqlTypes.ORLike);
                        if (ObjParameter.Value.ToString().Contains(","))
                        {
                            SqlWhere.Append(" or c." + ObjParameter.Name + " like '%" + ObjParameter.Value.ToString().Split(',')[0] + "%'");
                        }
                        else
                        {
                            SqlWhere.Append(" or c." + ObjParameter.Name + " like '%" + ObjParameter.Value + "%')");
                        }

                        break;
                    case NSqlTypes.ORLike:
                        SqlWhere.Append(" or c." + ObjParameter.Name + " like '%" + ObjParameter.Value + "%'");
                        break;
                    case NSqlTypes.OrInts:
                        if (ObjParameter.Value.ToString() == "isNulls")        //已完成订单  未评价   已评价 需要
                        {
                            SqlWhere.Append(" or c." + ObjParameter.Name + " = 0)");
                        }
                        else
                        {
                            SqlWhere.Append(" or c." + ObjParameter.Name + " = " + ObjParameter.Value);
                        }
                        break;
                    case NSqlTypes.OTTimerSpan:
                        break;
                    case NSqlTypes.PVP:         //设计单列表使用

                        if (ObjParameter.Value.ToString().Contains(","))
                        {
                            string[] text = ObjParameter.Value.ToString().Split(',');
                            for (int i = 0; i < text.Length; i++)
                            {

                                if (i == text.Length - 1)
                                {
                                        SqlWhere.Append(" or c." + ObjParameter.Name.ToString().Split(',')[i] + " = " + text[i]+")");
                                }
                                else if (i == 0)
                                {
                                    SqlWhere.Append(" and (c." + ObjParameter.Name.ToString().Split(',')[i] + " = " + text[i]);
                                }
                                else
                                {
                                    SqlWhere.Append(" or c." + ObjParameter.Name.ToString().Split(',')[i] + " = " + text[i]);
                                }

                            }

                        }


                        break;
                    case NSqlTypes.IN:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " in { " + ObjParameter.Value + " }");
                        break;
                    case NSqlTypes.OrIn:
                        SqlWhere.Append(" or c." + ObjParameter.Name + " in { " + ObjParameter.Value + " }");
                        break;
                    case NSqlTypes.Equal:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " = " + ObjParameter.Value + "");
                        break;
                    case NSqlTypes.Bit:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " =cast (" + ObjParameter.Value + " as System.Boolean)");
                        break;
                    case NSqlTypes.NotIN:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " not in { " + ObjParameter.Value + " }");
                        break;
                    case NSqlTypes.StringNotIN:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " not in { '" + ObjParameter.Value + " '}");
                        break;
                    case NSqlTypes.StringEquals:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " = '" + ObjParameter.Value + "'");
                        break;
                    case NSqlTypes.IsNull:
                        if (ObjParameter.Value.ToString() == "isNulls")        //已完成订单  未评价   已评价 需要
                        {
                            SqlWhere.Append(" and (c." + ObjParameter.Name + " Is Null");
                        }
                        else
                        {
                            SqlWhere.Append(" and c." + ObjParameter.Name + " Is Null");
                        }
                        break;
                    case NSqlTypes.IsNotNull:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " Is Not Null");
                        break;
                    case NSqlTypes.ColumnOr:
                        SqlWhere.Append(" and (c." + ObjParameter.Name.Split(',')[0] + "=" + ObjParameter.Value + " or c." + ObjParameter.Name.Split(',')[1] + "=" + ObjParameter.Value + ")");
                        break;
                    case NSqlTypes.NumBetween:
                        SqlWhere.Append(" and (c." + ObjParameter.Name + " between " + ObjParameter.Value.ToString().Split(',')[0] + " and " + ObjParameter.Value.ToString().Split(',')[1] + ")");
                        break;
                    case NSqlTypes.NotEquals:
                        SqlWhere.Append(" and c." + ObjParameter.Name + " != " + ObjParameter.Value + " ");
                        break;
                    case NSqlTypes.Split:
                        if (ObjParameter.Value.ToString().Contains(","))
                        {
                            string[] text = ObjParameter.Value.ToString().Split(',');
                            for (int i = 0; i < text.Length; i++)
                            {
                                if (text[text.Length - 1] == "int")
                                {
                                    if (i == 0)
                                    {
                                        if (text[text.Length - 2] == "")    //第一次条件
                                        {
                                            SqlWhere.Append(" and (c." + ObjParameter.Name + " = " + text[i]);
                                        }
                                        else    //第二次或多次累加
                                        {
                                            SqlWhere.Append(" or c." + ObjParameter.Name + " = " + text[i]);
                                        }
                                    }
                                    else if (i == text.Length - 2 && text[i] == "1")
                                    {
                                        SqlWhere.Append(" )");
                                    }

                                }

                                if (text[text.Length - 1] == "string")
                                {
                                    if (i == 0)
                                    {
                                        if (text[text.Length - 2] == "")    //第一次条件
                                        {
                                            SqlWhere.Append(" and (c." + ObjParameter.Name + " ='" + text[i] + "'");
                                        }
                                        else    //第二次或多次累加
                                        {
                                            SqlWhere.Append(" or c." + ObjParameter.Name + " = '" + text[i] + "'");
                                        }
                                    }
                                    else if (i == text.Length - 2 && text[i] == "1")
                                    {
                                        SqlWhere.Append(" )");
                                    }
                                }
                            }
                        }
                        break;
                    case NSqlTypes.OrInt:       //特定判断  只供未评价的订单
                        if (ObjParameter.Value == null)
                        {
                            SqlWhere.Append(" or c." + ObjParameter.Name + " Is Null) ");
                        }
                        else
                        {
                            SqlWhere.Append(" and (c." + ObjParameter.Name + " = " + ObjParameter.Value + " ");
                        }
                        break;
                    case NSqlTypes.SplitEqualString:
                        if (ObjParameter.Value.ToString().Contains(","))
                        {
                            string[] text = ObjParameter.Value.ToString().Split(',');
                            for (int i = 0; i < text.Length; i++)
                            {
                                if (i == 0)
                                {
                                    SqlWhere.Append(" and (c." + ObjParameter.Name + " like '" + text[i] + "'");
                                }
                                else if (i == text.Length - 1)
                                {
                                    SqlWhere.Append(" or c." + ObjParameter.Name + " like '" + text[i] + "')");
                                }
                                else
                                {
                                    SqlWhere.Append(" or c." + ObjParameter.Name + " like '" + text[i] + "'");
                                }
                            }
                        }
                        else
                        {
                            SqlWhere.Append(" and c." + ObjParameter.Name + " like '" + ObjParameter.Value + "'");
                        }
                        break;
                    case NSqlTypes.SplitEqual:
                        if (ObjParameter.Value.ToString().Contains(","))
                        {
                            string[] texts = ObjParameter.Value.ToString().Split(',');
                            for (int i = 0; i < texts.Length; i++)
                            {
                                if (i == 0)
                                {
                                    SqlWhere.Append(" and (c." + ObjParameter.Name + " = " + texts[i] + "");
                                }
                                else if (i == texts.Length - 1 && texts[i] != "")
                                {
                                    SqlWhere.Append(" or c." + ObjParameter.Name + " = " + texts[i] + ")");
                                }
                                else
                                {
                                    SqlWhere.Append(" or c." + ObjParameter.Name + " = " + texts[i] + "");
                                }
                            }
                        }
                        else
                        {
                            SqlWhere.Append(" and c." + ObjParameter.Name + " = " + ObjParameter.Value + "");
                        }
                        break;
                    case NSqlTypes.DeskBetween:
                        if (ObjParameter.Value.ToString().Contains(","))
                        {
                            string[] texts = ObjParameter.Value.ToString().Split(',');
                            for (int i = 0; i < texts.Length; i++)
                            {
                                string[] DeskCount = texts[i].Split('.');
                                if (i == 0)
                                {
                                    SqlWhere.Append(" and (c." + ObjParameter.Name + " between " + DeskCount[0] + " and " + DeskCount[1]);
                                }
                                else if (i == texts.Length - 1)
                                {
                                    SqlWhere.Append(" or c." + ObjParameter.Name + " between " + DeskCount[0] + " and " + DeskCount[1] + ")");
                                }
                                else
                                {
                                    SqlWhere.Append(" or c." + ObjParameter.Name + " between " + DeskCount[0] + " and " + DeskCount[1]);
                                }
                            }
                        }
                        else
                        {
                            string[] DeskCount = ObjParameter.Value.ToString().Split('.');
                            if (DeskCount[0].ToString() == "0")
                            {
                                SqlWhere.Append(" and c." + ObjParameter.Name + " <= " + DeskCount[1] + "");
                            }
                            else if (DeskCount[1].ToString() == "50000")
                            {
                                SqlWhere.Append(" and c." + ObjParameter.Name + " >= " + DeskCount[0] + "");
                            }
                            else
                            {
                                SqlWhere.Append(" and (c." + ObjParameter.Name + " between " + DeskCount[0] + " and " + DeskCount[1] + ")");
                            }
                        }
                        break;
                    case NSqlTypes.SplitContain:
                        if (ObjParameter.Value.ToString().Contains(","))
                        {
                            string[] texts = ObjParameter.Value.ToString().Split(',');
                            for (int i = 0; i < texts.Length; i++)
                            {
                                SqlWhere.Append(" and c." + ObjParameter.Name + " like '%" + texts[i] + "%'");
                            }
                        }
                        break;

                }
            }
            return SqlWhere.ToString();
        }
        #endregion

        #region 过时方法
        /// <summary>
        /// 返回参数
        /// </summary>
        /// <param name="ObjParameter"></param>
        /// <returns></returns>
        [Obsolete("此方法已过时，不再使用")]
        public static string ReturnWhere(ObjectParameter ObjParameter, out string EmpLoyeeID, out string KeyPar)
        {
            if (ObjParameter.Name.Contains("EmpLoyeeID"))
            {
                if (ObjParameter.Value.ToString().Contains(","))
                {
                    EmpLoyeeID = ObjParameter.Value.ToString().Split(',')[0];
                }
                else
                {
                    EmpLoyeeID = ObjParameter.Value.ToString();
                }
            }
            else
            {
                EmpLoyeeID = string.Empty;
            }

            if (ObjParameter.Name.Contains("SerchKeypar"))
            {
                KeyPar = ObjParameter.Value.ToString();
                return string.Empty;
            }
            else
            {
                KeyPar = string.Empty;
            }
            //LIKE查询
            if (ObjParameter.Name.Contains("LIKE"))
            {

                return " c." + ObjParameter.Name.Split('_')[0] + " Like '%" + ObjParameter.Value + "%'";
            }

            //字符串OR 
            if (ObjParameter.Name.Contains("OR"))
            {
                if (ObjParameter.Value.ToString().Split(',')[1] != string.Empty)
                {
                    return " c." + ObjParameter.Name.Split('_')[0] + " ='" + ObjParameter.Value.ToString().Split(',')[0] + "'"
                    + " or c." + ObjParameter.Name.Split('_')[0] + " ='" + ObjParameter.Value.ToString().Split(',')[1] + "'";
                }
                else
                {
                    return " c." + ObjParameter.Name.Split('_')[0] + " ='" + ObjParameter.Value.ToString().Split(',')[0] + "'"
                    + " or c." + ObjParameter.Name.Split('_')[2] + " ='" + ObjParameter.Value.ToString().Split(',')[0] + "'";
                }
            }

            //字段OR 仅两字段
            if (ObjParameter.Name.Contains("PVP"))
            {

                return " (c." + ObjParameter.Name.Split('_')[0] + " =" + ObjParameter.Value.ToString().Split(',')[0]
                + " or c." + ObjParameter.Name.Split('_')[1] + " =" + ObjParameter.Value.ToString().Split(',')[1] + ")";
            }

            //大于某个数字
            if (ObjParameter.Name.Contains("NumGreaterthan"))
            {
                return " c." + ObjParameter.Name.Split('_')[0] + " > " + ObjParameter.Value.ToString();
            }

            //大于小于之间
            if (ObjParameter.Name.Contains("Greaterthan"))
            {
                return " c." + ObjParameter.Name.Split('_')[0] + " >= " + ObjParameter.Value.ToString().Split(',')[0] +
                       " and c." + ObjParameter.Name.Split('_')[0] + "<=" + ObjParameter.Value.ToString().Split(',')[1];
            }
            //数字OR
            if (ObjParameter.Name.Contains("NumOr"))
            {
                var ObjValueList = ObjParameter.Value.ToString().Split(',');
                var ItemName = ObjParameter.Name.Split('_')[0];
                string EsqlWhere = string.Empty;
                foreach (var Objitem in ObjValueList)
                {
                    EsqlWhere += " c." + ObjParameter.Name.Split('_')[0] + " =" + Objitem + " or ";
                }
                EsqlWhere = EsqlWhere.Remove(EsqlWhere.Length - 3);
                EsqlWhere = "( " + EsqlWhere + " ) ";
                return EsqlWhere;
            }

            if (ObjParameter.Name.Contains("between"))
            {
                //判断是Detatime类型 范围
                if (ObjParameter.Value.ToString().Contains("/") || ObjParameter.Value.ToString().Contains("-"))
                {   //cast('1977-11-11' as System.DateTime)")
                    return " c." + ObjParameter.Name.Split('_')[0] + " >= cast('" + ObjParameter.Value.ToString().Split(',')[0].ToDateTime().ToString("yyyy-MM-dd 00:00:00") +
                        "' as System.DateTime) and c." + ObjParameter.Name.Split('_')[0] + "<=cast('" + ObjParameter.Value.ToString().Split(',')[1].ToDateTime().ToString("yyyy-MM-dd 23:59:59") + "' as System.DateTime)";
                }
                else
                {    //数字 范围


                    return " c." + ObjParameter.Name.Split('_')[0] + " BETWEEN " + ObjParameter.Value.ToString().Split(',')[0] + " and " + ObjParameter.Value.ToString().Split(',')[1];
                }
            }



            if (ObjParameter.Name.Contains("OTTimerSpan"))
            {
                //判断是Detatime类型 范围
                string Month = ObjParameter.Value.ToString().Split(',')[0];
                string Day = (int.Parse(ObjParameter.Value.ToString().Split(',')[1]) + 7).ToString();
                string DayStar = (int.Parse(ObjParameter.Value.ToString().Split(',')[1])).ToString();
                //cast('1977-11-11' as System.DateTime)")
                // return " c." + ObjParameter.Name.Split('_')[0] + " >= cast('" + ObjParameter.Value.ToString().Split(',')[0].ToDateTime().ToShortDateString() +
                //     "' as System.DateTime) and c." + ObjParameter.Name.Split('_')[0] + "<=cast('" + ObjParameter.Value.ToString().Split(',')[1].ToDateTime().ToShortDateString() + "' as System.DateTime) or" +
                //" c." + ObjParameter.Name.Split('_')[1] + " >= cast('" + ObjParameter.Value.ToString().Split(',')[2].ToDateTime().ToShortDateString() +
                //     "' as System.DateTime) or c." + ObjParameter.Name.Split('_')[1] + "<=cast('" + ObjParameter.Value.ToString().Split(',')[3].ToDateTime().ToShortDateString() + "' as System.DateTime)";
                return "Month(c." + ObjParameter.Name.Split('_')[0] + ")=" + Month + "" + " or Month(c." + ObjParameter.Name.Split('_')[1] + ")=" + Month + " and (" + "Day(c." + ObjParameter.Name.Split('_')[0] + ")>=" + DayStar + "or Day(c." + ObjParameter.Name.Split('_')[0] + ")<=" + Day + ")" + " and (" + "Day(c." + ObjParameter.Name.Split('_')[1] + ")>=" + DayStar + "or Day(c." + ObjParameter.Name.Split('_')[1] + ")<=" + Day + ")";

            }

            return " c." + ObjParameter.Name + "=@" + ObjParameter.Name;
        }
        #endregion

        #region 过滤数据
        /// <summary>
        /// 根据权过滤数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("请使用 PublicDataTools 中的 GetDataByWhereParameter 方法")]
        public static List<T> GetDataByPower(T ObjEntity, int? EmployeeID, int? ChannelID, ObjectParameter[] ObjParameterList)
        {
            HA.PMS.DataAssmblly.PMS_WeddingEntities ObjDataEntity = new PMS_WeddingEntities();
            string GetWhere = string.Empty;
            if (ObjParameterList.Length > 0)
            {
                GetWhere = "where ";
            }
            else
            {
                List<T> ObjList = new List<T>();
                using (EntityConnection ObjEntityconn = new EntityConnection("name=PMS_WeddingEntities"))
                {
                    ObjectContext ObjDataContext = new ObjectContext(ObjEntityconn);
                    // strWhere = "it.JION_TIME<datetime'" + t2 + "'";
                    ObjList = ObjDataContext.CreateQuery<T>("Select VALUE c from PMS_WeddingEntities." + ObjEntity.GetType().Name + " as c  ", ObjParameterList).ToList<T>();
                }
                return ObjList;
            }
            int i = 1;
            string ParEmpLoyeeID = string.Empty;
            string KeyPar = string.Empty;
            string RealEmpLoyeeID = "0";
            bool NoEmployeeID = false;
            string NonePar = string.Empty;
            foreach (var Objpar in ObjParameterList)
            {
                if (i < ObjParameterList.Length)
                {
                    if (Objpar.Name.Contains("SerchKeypar"))
                    {
                        GetWhere = GetWhere.Replace("EmployeeID", Objpar.Value.ToString());
                        GetWhere = GetWhere.Replace("EmpLoyeeID", Objpar.Value.ToString());
                        NoEmployeeID = true;
                        NonePar = Objpar.Value.ToString();
                    }
                    else
                    {
                        GetWhere += ReturnWhere(Objpar, out ParEmpLoyeeID, out KeyPar) + " and ";
                    }

                }
                else
                {
                    GetWhere += ReturnWhere(Objpar, out ParEmpLoyeeID, out KeyPar);
                }
                if (ParEmpLoyeeID != string.Empty)
                {
                    RealEmpLoyeeID = ParEmpLoyeeID;
                }
                i++;
            }

            if (NoEmployeeID)
            {
                int Index = 0;
                object Value = new object();
                foreach (var Objpar1 in ObjParameterList)
                {
                    if (Objpar1.Name.Contains("EmpLoyeeID") || Objpar1.Name.Contains("EmployeeID"))
                    {
                        Value = Objpar1.Value;
                    }
                    Index++;
                }


                var parList = ObjParameterList.ToList();
                parList.Add(new ObjectParameter(NonePar, Value));
                ObjParameterList = parList.ToArray();
            }

            using (EntityConnection ObjEntityconn = new EntityConnection("name=PMS_WeddingEntities"))
            {
                List<T> ObjList = new List<T>();
                ObjectContext ObjDataContext = new ObjectContext(ObjEntityconn);
                List<int> ObjKeyList = new List<int>();

                //查询本功能模块的数据权限
                //  PMS_WeddingEntities ObjWeddingDataContext = new PMS_WeddingEntities();
                ///定义数据库链接

                ObjEntityconn.Open();
                EmployeeID = int.Parse(RealEmpLoyeeID);
                if (EmployeeID != 0)
                {

                    //var ObjEmpLoyee = ObjWeddingDataContext.Sys_Employee.FirstOrDefault(C => C.EmployeeID == EmployeeID);
                    //   var ObjDepartment = ObjWeddingDataContext.Sys_Department.FirstOrDefault(C => C.DepartmentID == ObjEmpLoyee.DepartmentID);

                    //获取本人兼任的所有部门主管列表
                    var MyDepartmentList = ObjDataEntity.Sys_Department.Where(C => C.DepartmentManager == EmployeeID);
                    if (MyDepartmentList.Count() > 0)
                    {
                        List<Sys_Department> ObjDepartmentList = new List<Sys_Department>();
                        foreach (var ObjDepartment in MyDepartmentList)
                        {
                            ObjDepartmentList.AddRange(ObjDataContext.CreateQuery<Sys_Department>("Select VALUE c from PMS_WeddingEntities.Sys_Department as c where c.SortOrder  Like '%" + ObjDepartment.SortOrder + "%'").ToList<Sys_Department>());
                        }
                        ////猜测是否为部门主管
                        //if (ObjDepartment.DepartmentManager == ObjEmpLoyee.EmployeeID)
                        //{
                        //Department ObjDepartmentBLL=new Department();


                        // var DepartmetnList = ObjDataContext.CreateQuery<Sys_Department>("Select VALUE c from PMS_WeddingEntities.Sys_Department as c where c.SortOrder  Like '%" + ObjDepartment.SortOrder + "%'").ToList<Sys_Department>();

                        Employee ObjEmployeeBLL = new Employee();
                        foreach (var Objitem in ObjDepartmentList)
                        {
                            ObjKeyList.Add(Objitem.DepartmentID);
                        }
                        var ObjEmpLoyeeList = ObjEmployeeBLL.GetByDepartmetnKeysList(ObjKeyList.ToArray());
                        ObjEmpLoyeeList.Add(ObjEmployeeBLL.GetByID(EmployeeID));
                        var NewWhere = string.Empty;
                        int ParIndex = ObjEmpLoyeeList.Count;
                        int ParEmpCount = 0;

                        //拼凑需要的责任人
                        foreach (var Objemployee in ObjEmpLoyeeList)
                        {

                            if (KeyPar == string.Empty)
                            {
                                if (ParEmpCount != 0)
                                {
                                    NewWhere += "  or c.EmployeeID=" + Objemployee.EmployeeID + " ";
                                }
                                else
                                {
                                    NewWhere += "   c.EmployeeID=" + Objemployee.EmployeeID + " ";
                                }
                            }
                            else
                            {
                                if (ParEmpCount != 0)
                                {
                                    NewWhere += "  or c." + KeyPar + "=" + Objemployee.EmployeeID + " ";
                                }
                                else
                                {
                                    NewWhere += "  c." + KeyPar + "=" + Objemployee.EmployeeID + " ";
                                }
                            }
                            ParEmpCount++;
                        }

                        //包括自己的
                        //  NewWhere = NewWhere + " or c.EmployeeID=" + EmployeeID;


                        var ChildenEmpLoyee = string.Empty;
                        //var TrimChar = GetWhere.Substring(GetWhere.Length - 4);
                        //if (TrimChar == "and ")
                        //{
                        //    GetWhere = GetWhere.Substring(0, GetWhere.Length - 4);
                        //}

                        //去掉本人
                        GetWhere = GetWhere.Replace("c.EmpLoyeeID=@EmpLoyeeID and", "");
                        GetWhere = GetWhere.Replace("and c.EmpLoyeeID=@EmpLoyeeID and", "");
                        GetWhere = GetWhere.Replace("and  c.EmpLoyeeID=@EmpLoyeeID", "");

                        GetWhere = GetWhere.Replace("where    and", "where ");

                        if (GetWhere.Trim() == "where")
                        {
                            if (ParEmpCount != 0)
                            {
                                GetWhere = " where (c." + KeyPar + "=@EmpLoyeeID)";
                            }
                            else
                            {
                                GetWhere = " where (c.EmpLoyeeID=@EmpLoyeeID)";
                            }
                        }
                        var TrimChar = GetWhere.Substring(GetWhere.Length - 6);
                        if (TrimChar == "and   ")
                        {
                            GetWhere = GetWhere.Substring(0, GetWhere.Length - 6);
                        }
                        GetWhere = GetWhere.Replace("and    and", " and ");
                        //拼凑OR AND AND优先级高
                        string RunSql = "Select VALUE c from PMS_WeddingEntities." + ObjEntity.GetType().Name + " as c  "
                            + GetWhere + " and (" + NewWhere + ")";

                        RunSql = RunSql.Replace("c.EmpLoyeeID=@EmpLoyeeID and", "");
                        //GetWhere = GetWhere.Replace("and c.EmpLoyeeID=@EmpLoyeeID and", "");
                        //GetWhere = GetWhere.Replace("and  c.EmpLoyeeID=@EmpLoyeeID", "");
                        //GetWhere = GetWhere.Replace("where    and", "where ");
                        var ObjReturnList = ObjDataContext.CreateQuery<T>(RunSql, ObjParameterList);
                        ObjList = ObjReturnList.ToList();
                        ObjEntityconn.Close();
                        return ObjList;
                    }
                    else
                    {
                        if (KeyPar == string.Empty)
                        {
                            GetWhere = GetWhere.Replace("where    and", "where ");
                            GetWhere = GetWhere.Replace("and  and", "and ");

                            ObjList = ObjDataContext.CreateQuery<T>("Select VALUE c from PMS_WeddingEntities." + ObjEntity.GetType().Name + " as c  " + GetWhere, ObjParameterList).ToList<T>();
                            return ObjList;
                        }
                        else
                        {
                            var TrimChar = GetWhere.Substring(GetWhere.Length - 4);
                            if (TrimChar == "and ")
                            {
                                GetWhere = GetWhere.Substring(0, GetWhere.Length - 4);
                            }

                            GetWhere = GetWhere.Replace("EmpLoyeeID", KeyPar);
                            GetWhere = GetWhere.Replace("@" + KeyPar, "@EmpLoyeeID");
                            GetWhere = GetWhere.Replace("where    and", "where ");
                            GetWhere = GetWhere.Replace("and  and", "and ");
                            ObjList = ObjDataContext.CreateQuery<T>("Select VALUE c from PMS_WeddingEntities." + ObjEntity.GetType().Name + " as c  " + GetWhere, ObjParameterList).ToList<T>();
                            ObjEntityconn.Close();

                            return ObjList;
                        }
                    }
                }
                else
                {



                    //特定查询参数
                    if (KeyPar == string.Empty)
                    {
                        GetWhere = GetWhere.Replace("where    and", "where ");
                        GetWhere = GetWhere.Replace("and  and", "and ");
                        ObjList = ObjDataContext.CreateQuery<T>("Select VALUE c from PMS_WeddingEntities." + ObjEntity.GetType().Name + " as c  " + GetWhere, ObjParameterList).ToList<T>();
                        ObjEntityconn.Close();
                        return ObjList;
                    }
                    else
                    {

                        var TrimChar = GetWhere.Substring(GetWhere.Length - 4);
                        if (TrimChar == "and ")
                        {
                            GetWhere = GetWhere.Substring(0, GetWhere.Length - 4);
                        }

                        GetWhere = GetWhere.Replace("EmpLoyeeID", KeyPar);
                        GetWhere = GetWhere.Replace("@" + KeyPar, "@EmpLoyeeID");
                        GetWhere = GetWhere.Replace("where    and", "where ");
                        GetWhere = GetWhere.Replace("and  and", "and ");
                        ObjList = ObjDataContext.CreateQuery<T>("Select VALUE c from PMS_WeddingEntities." + ObjEntity.GetType().Name + " as c  " + GetWhere, ObjParameterList).ToList<T>();
                        ObjEntityconn.Close();
                        return ObjList;
                    }
                }
            }
        }
        #endregion

        #region 不分页查询数据
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="GetWhere">条件</param>
        /// <param name="OrderByColumn">排序字段</param>
        /// <param name="GroupBy">排序方式(倒序 Or 正序)</param>
        /// <returns></returns>
        public static List<T> GetByParameters(string GetWhere, string OrderByColumn, OrderType GroupBy = OrderType.Asc)
        {
            using (EntityConnection ObjEntityconn = new EntityConnection("name=PMS_WeddingEntities"))
            {
                List<ObjectParameter> ObjListparmeter = new List<ObjectParameter>();
                ObjectContext ObjDataContext = new ObjectContext(ObjEntityconn);

                string sql = "Select VALUE c from PMS_WeddingEntities." + typeof(T).Name + " as c where 1=1  " + GetWhere + " order by c." + OrderByColumn + " " + GroupBy.ToString();
                List<T> ObjList = new List<T>();

                var ObjReturnList = ObjDataContext.CreateQuery<T>(sql);
                ObjList = ObjReturnList.ToList();
                ObjEntityconn.Close();
                return ObjList;
            }
        }
        #endregion

        #region 判断该数据是否存在
        /// <summary>
        /// 判断该条数据是否存在
        /// </summary>
        /// <returns></returns>
        public static bool IsExists(List<PMSParameters> ObjParameterList)
        {
            string WhereStr = GetWhere(ObjParameterList);


            string sql = "Select count(*) from " + typeof(T).Name + " as c where 1=1 " + WhereStr;
            PMS_WeddingEntities ObjContex = new PMS_WeddingEntities();
            var SourctcountResualt = ObjContex.Database.SqlQuery<int>(sql, new List<ObjectParameter>().ToArray()).First();
            int Count = SourctcountResualt;
            if (Count > 0)
            {
                return true;            //该条数据已经存在
            }
            else
            {
                return false;           //不存在 就可以新增
            }
        }
        #endregion

        //供应商
        public static List<FD_Supplier> GetSupplierGroup(int PageSize, int CurrentPageIndex, List<PMSParameters> pars, ref int SourceCount)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars);
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            var DataList = ObjEntity.GetStatemetProc(1, WhereStr).ToList();
            //DataList.AddRange(ObjEntity.GetStatemetProc(10, WhereStr).ToList());
            var DLists = (from C in DataList join D in ObjEntity.FD_Supplier on C.SupplierID equals D.SupplierID select D).Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize).ToList();
            SourceCount = DataList.Count;
            return DLists.Where(C => C.IsDelete == false).ToList();
        }
        //四大金刚
        public static List<FD_FourGuardian> GetGuardianGroup(int PageSize, int CurrentPageIndex, List<PMSParameters> pars, ref int SourceCount)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars);
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            var DataList = ObjEntity.GetStatemetProc(4, WhereStr).ToList();
            var DLists = (from C in DataList join D in ObjEntity.FD_FourGuardian on C.SupplierID equals D.GuardianId select D).Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize).ToList();
            SourceCount = DataList.Count;
            return DLists.Where(C => C.IsDelete == false).ToList();
        }

        //内部人员
        public static List<Sys_Employee> GetEmployeeGroup(int PageSize, int CurrentPageIndex, List<PMSParameters> pars, ref int SourceCount)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars) + "and c.SupplierID != 0";
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            var DataList = ObjEntity.GetStatemetProc(5, WhereStr).ToList();
            var DLists = (from C in DataList join D in ObjEntity.Sys_Employee on C.SupplierID equals D.EmployeeID select D).Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize).ToList();
            SourceCount = DataList.Count;
            return DLists.Where(C => C.IsDelete == false).ToList();
        }

        //外部人员
        public static List<FL_Statement> GetOutEmployeeGroup(int PageSize, int CurrentPageIndex, List<PMSParameters> pars, ref int SourceCount)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars) + "and c.SupplierID = 0";
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            var DataList = ObjEntity.GetStatemetProc(5, WhereStr).ToList();
            var DLists = (from C in DataList join D in ObjEntity.FL_Statement on C.SupplierID equals D.SupplierID select D).Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize).ToList();
            SourceCount = DataList.Count;
            return DLists;
        }



        #region 以下方法是计算每页合计
        #endregion

        //供应商
        public static string GetSupplierById(int SupplierID, List<PMSParameters> pars, int Type = 1)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars);
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            string AmountMoney = "0";
            if (Type == 1)      //总金额
            {
                AmountMoney = ObjEntity.GetSuppliersById(SupplierID, 1, WhereStr).ToList().FirstOrDefault().SubTotal.ToString();
            }
            else if (Type == 2)     //已付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(SupplierID, 1, WhereStr).ToList().FirstOrDefault().SubPayMent.ToString();
            }
            else if (Type == 3)     //未付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(SupplierID, 1, WhereStr).ToList().FirstOrDefault().SubNoPayMent.ToString();
            }
            return AmountMoney;
        }

        //四大金刚
        public static string GetGuardianById(int GuardianId, List<PMSParameters> pars, int Type = 1)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars);
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            string AmountMoney = "0";
            if (Type == 1)      //总金额
            {
                AmountMoney = ObjEntity.GetSuppliersById(GuardianId, 4, WhereStr).ToList().FirstOrDefault().SubTotal.ToString();
            }
            else if (Type == 2)     //已付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(GuardianId, 4, WhereStr).ToList().FirstOrDefault().SubPayMent.ToString();
            }
            else if (Type == 3)     //未付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(GuardianId, 4, WhereStr).ToList().FirstOrDefault().SubNoPayMent.ToString();
            }
            return AmountMoney;
        }

        //内部人员
        public static string GetEmployeeById(int EmployeeId, List<PMSParameters> pars, int Type = 1)
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars);
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            string AmountMoney = "0";
            if (Type == 1)      //总金额
            {
                AmountMoney = ObjEntity.GetSuppliersById(EmployeeId, 5, WhereStr).ToList().FirstOrDefault().SubTotal.ToString();
            }
            else if (Type == 2)     //已付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(EmployeeId, 5, WhereStr).ToList().FirstOrDefault().SubPayMent.ToString();
            }
            else if (Type == 3)     //未付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(EmployeeId, 5, WhereStr).ToList().FirstOrDefault().SubNoPayMent.ToString();
            }
            return AmountMoney;
        }

        //外部人员
        public static string GetOutPersonById(int SupplierID, List<PMSParameters> pars, int Type = 1, string name = "")
        {
            PMS_WeddingEntities ObjEntity = new PMS_WeddingEntities();
            string WhereStr = GetWhere(pars);
            if (name != "")
            {
                WhereStr += " and C.Name='" + name + "'";
            }
            WhereStr = WhereStr.Replace("System.DateTime", "date").Replace("c.", "");
            string AmountMoney = "0";
            if (Type == 1)      //总金额
            {
                AmountMoney = ObjEntity.GetSuppliersById(SupplierID, 5, WhereStr).ToList().FirstOrDefault().SubTotal.ToString();
            }
            else if (Type == 2)     //已付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(SupplierID, 5, WhereStr).ToList().FirstOrDefault().SubPayMent.ToString();
            }
            else if (Type == 3)     //未付款
            {
                AmountMoney = ObjEntity.GetSuppliersById(SupplierID, 5, WhereStr).ToList().FirstOrDefault().SubNoPayMent.ToString();
            }
            return AmountMoney;
        }

    }
}
