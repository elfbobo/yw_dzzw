using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.ProjectCore
{
    /// <summary>
    /// 建设程序管理类
    /// </summary>
    public class BuildProcedureInfo
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// 判断项目是主项目还是子项目
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static int GetProjTop(string ProjGuid)
        {
            string ProjTopSql = "select count(TopGuid) from Busi_ProjRegister where Guid='" + ProjGuid + "'";
            int count = Convert.ToInt32(GetDatabase().ExecuteScalar(ProjTopSql));
            return count;
        }

        /// <summary>
        /// 获取该项目的项目主从关系
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetProjAffiliation(string ProjGuid)
        {
            string ProjAffiliationSql = "";
            DataSet AffiliationDs = new DataSet();
            //判断该项目是主项目还是子项目
            int count = GetProjTop(ProjGuid);

            if (count == 0)
            {
                ProjAffiliationSql = "select ProjAffiliation from  Busi_ProjRegister where  Guid='" + ProjGuid + "'";
                AffiliationDs = GetDatabase().ExecuteDataSet(ProjAffiliationSql);
            }
            else
            {
                ProjAffiliationSql = "SELECT ProjAffiliation FROM Busi_ProjRegister WHERE Guid in (SELECT TopGuid FROM Busi_ProjRegister WHERE Guid='" + ProjGuid + "')";
                AffiliationDs = GetDatabase().ExecuteDataSet(ProjAffiliationSql);
            }
            if (AffiliationDs.Tables[0].Rows.Count > 0)
            {
                if (AffiliationDs.Tables[0].Rows[0]["ProjAffiliation"].ToString() == "6921FF68-A789-4BDD-9AC1-6B8CA13B49C1")
                {
                    return "集中立项可研";
                }
                else if (AffiliationDs.Tables[0].Rows[0]["ProjAffiliation"].ToString() == "FA173C37-BF98-4AAD-9997-817835165E29")
                {
                    return "集中批复立项可研概算";
                }
                else if (AffiliationDs.Tables[0].Rows[0]["ProjAffiliation"].ToString() == "CC4B768A-2A2E-4EA0-B3EA-1C6E2765C221")
                {
                    return "无子项目";
                }
                else if (AffiliationDs.Tables[0].Rows[0]["ProjAffiliation"].ToString() == "782B4E4D-BFAE-4034-96A4-BD18F95A3C61")
                {
                    return "独立审批子项目";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据投资类型和审批类型获取相对应的主要审批手续
        /// </summary>
        /// <param name="NationType">审批类型</param>
        /// <param name="InvestType">投资类型</param>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static DataSet GetMainByType(string NationType, string InvestType, string ProjGuid)
        {
            string MainSql = "";
            //string ProjAffiliationSql = "";//获取该项目的项目主从关系
            //DataSet AffiliationDs = new DataSet();//项目主从关系
            string ProjAffiliation = GetProjAffiliation(ProjGuid);

            //判断该项目是主项目还是子项目
            int count = GetProjTop(ProjGuid);
            //如果是主项目
            if (count == 0)
            {
                if (ProjAffiliation != "")
                {
                    //ProjAffiliation = AffiliationDs.Tables[0].Rows[0]["ProjAffiliation"].ToString();
                    //判断主从关系是否是集中立项可研，子项目独立批复概算
                    if (ProjAffiliation == "集中立项可研")
                    {
                        //只显示集中立项可研
                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4') ORDER BY convert(INT, Code)";
                    }
                    // //判断主从关系是否是集中批复立项可研概算，子项目独立概算
                    //else if (ProjAffiliation == "集中批复立项可研概算")
                    //{
                    //    //只显示集中批复立项可研概算
                    //    MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','7','14') ORDER BY convert(INT, Code)";
                    //}
                    else if (ProjAffiliation == "独立审批子项目")
                    {
                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE 1=2";
                    }
                    else
                    {
                        //判断审批类型
                        if (NationType.Contains("6CDCC6AA-ED28-4616-96D9-6E39229BB956")) //全审批
                        {
                            //根据投资类型查询出相对应的审批手续

                            //判断投资类型是否是中央投资类型
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','11','12') ORDER BY convert(INT, Code)";
                            }
                        }
                        else if (NationType.Contains("9D3BEE82-53D9-45E0-8C11-651BE5E754AC"))//只审批初步设计
                        {
                            //根据投资类型查询出相对应的审批手续

                            //判断投资类型是否是中央投资类型
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','11','12') ORDER BY convert(INT, Code)";
                            }
                        }
                        else if (NationType.Contains("A1816C6C-4433-4123-B7A4-EA166D2649A0"))//核准
                        {
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','8','10','11','12') ORDER BY convert(INT, Code)";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','8','11','12') ORDER BY convert(INT, Code)";
                            }
                        }
                        else if (NationType.Contains("F8913E85-1F23-4E9B-BF6E-594B9E070CA5"))//备案
                        {
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','9','10','11','12') ORDER BY convert(INT, Code)";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','9','11','12') ORDER BY convert(INT, Code)";
                            }
                        }
                        else
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code =''";
                        }
                    }
                }
                else
                {
                    //判断审批类型
                    if (NationType.Contains("6CDCC6AA-ED28-4616-96D9-6E39229BB956")) //审批
                    {
                        //根据投资类型查询出相对应的审批手续

                        //判断投资类型是否是中央投资类型
                        if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                        }
                        else
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','11','12') ORDER BY convert(INT, Code)";
                        }
                    }
                    else if (NationType.Contains("9D3BEE82-53D9-45E0-8C11-651BE5E754AC"))//只审批初步设计
                    {
                        //根据投资类型查询出相对应的审批手续

                        //判断投资类型是否是中央投资类型
                        if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                        }
                        else
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','11','12') ORDER BY convert(INT, Code)";
                        }
                    }
                    else if (NationType.Contains("A1816C6C-4433-4123-B7A4-EA166D2649A0"))//核准
                    {
                        if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','8','10','11','12') ORDER BY convert(INT, Code)";
                        }
                        else
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','8','11','12') ORDER BY convert(INT, Code)";
                        }
                    }
                    else if (NationType.Contains("F8913E85-1F23-4E9B-BF6E-594B9E070CA5"))//备案
                    {
                        if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','9','10','11','12') ORDER BY convert(INT, Code)";
                        }
                        else
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','9','11','12') ORDER BY convert(INT, Code)";
                        }
                    }
                    else
                    {
                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code =''";
                    }
                }
            }
            //如果是子项目
            else
            {
                //判断审批类型
                if (NationType.Contains("6CDCC6AA-ED28-4616-96D9-6E39229BB956")) //审批
                {
                    //AffiliationDs = GetDatabase().ExecuteDataSet(ProjAffiliationSql);
                    if (ProjAffiliation != "")
                    {
                        //判断主从关系是否是集中立项可研，子项目独立批复概算
                        if (ProjAffiliation == "集中立项可研")
                        {

                            //根据投资类型查询出相对应的审批手续
                            //判断投资类型是否是中央投资类型
                            //加入集中立项可研
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('5','6','7','10','11','12','13') ORDER BY OrderCode";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('5','6','7','11','12','13') ORDER BY OrderCode";
                            }
                        }
                        // //判断主从关系是否是集中批复立项可研概算，子项目独立概算
                        //else if (ProjAffiliation == "集中批复立项可研概算")
                        //{
                        //    //根据投资类型查询出相对应的审批手续
                        //    //判断投资类型是否是中央投资类型
                        //    //加入集中批复立项可研概算
                        //    if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        //    {
                        //        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('5','6','7','10','11','12','14') ORDER BY OrderCode";
                        //    }
                        //    else
                        //    {
                        //        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('5','6','7','11','12','14') ORDER BY OrderCode";
                        //    }
                        //}
                        else
                        {
                            //根据投资类型查询出相对应的审批手续
                            //判断投资类型是否是中央投资类型
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','11','12') ORDER BY convert(INT, Code)";
                            }
                        }
                    }
                    else
                    {
                        //根据投资类型查询出相对应的审批手续
                        //判断投资类型是否是中央投资类型
                        if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                        }
                        else
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','11','12') ORDER BY convert(INT, Code)";
                        }
                    }


                }
                else if (NationType.Contains("9D3BEE82-53D9-45E0-8C11-651BE5E754AC"))//只审批初步设计
                {
                    //AffiliationDs = GetDatabase().ExecuteDataSet(ProjAffiliationSql);
                    if (ProjAffiliation != "")
                    {
                        //判断主从关系是否是集中立项可研，子项目独立批复概算
                        if (ProjAffiliation == "集中立项可研")
                        {

                            //根据投资类型查询出相对应的审批手续
                            //判断投资类型是否是中央投资类型
                            //加入集中立项可研
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('6','7','10','11','12','13') ORDER BY OrderCode";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('6','7','11','12','13') ORDER BY OrderCode";
                            }
                        }
                        // //判断主从关系是否是集中批复立项可研概算，子项目独立概算
                        //else if (ProjAffiliation == "集中批复立项可研概算")
                        //{
                        //    //根据投资类型查询出相对应的审批手续
                        //    //判断投资类型是否是中央投资类型
                        //    //加入集中批复立项可研概算
                        //    if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        //    {
                        //        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('5','6','7','10','11','12','14') ORDER BY OrderCode";
                        //    }
                        //    else
                        //    {
                        //        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('5','6','7','11','12','14') ORDER BY OrderCode";
                        //    }
                        //}
                        else
                        {
                            //根据投资类型查询出相对应的审批手续
                            //判断投资类型是否是中央投资类型
                            if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                            }
                            else
                            {
                                MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','11','12') ORDER BY convert(INT, Code)";
                            }
                        }
                    }
                    else
                    {
                        //根据投资类型查询出相对应的审批手续
                        //判断投资类型是否是中央投资类型
                        if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','10','11','12') ORDER BY convert(INT, Code)";
                        }
                        else
                        {
                            MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','5','6','7','11','12') ORDER BY convert(INT, Code)";
                        }
                    }
                }
                else if (NationType.Contains("A1816C6C-4433-4123-B7A4-EA166D2649A0"))//核准
                {
                    //判断投资类型是否是中央投资类型
                    if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                    {
                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','6','8','10','11','12') ORDER BY convert(INT, Code)";
                    }
                    else
                    {
                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','6','8','11','12') ORDER BY convert(INT, Code)";
                    }
                }
                else if (NationType.Contains("F8913E85-1F23-4E9B-BF6E-594B9E070CA5"))//备案
                {
                    //判断投资类型是否是中央投资类型
                    if (InvestType == "8E4AEDEA-8EEF-469D-980B-97F9275C6507")
                    {
                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','6','9','10','11','12') ORDER BY convert(INT, Code)";
                    }
                    else
                    {
                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','6','9','11','12') ORDER BY convert(INT, Code)";
                    }
                }
                else
                {
                    MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code =''";
                }
            }


            DataSet MainDs = GetDatabase().ExecuteDataSet(MainSql);


            return MainDs;
        }

        /// <summary>
        /// 获取其他审批手续
        /// </summary>
        /// <returns></returns>
        public static DataSet GetOther(string ProjGuid)
        {
            string OtherSql = "";
            DataSet OtherDs = new DataSet();
            //判断该项目是主项目还是子项目
            int count = GetProjTop(ProjGuid);
            string ProjAffiliation = GetProjAffiliation(ProjGuid);
            if (ProjAffiliation != "集中立项可研")
            {
                if (count != 0 || ProjAffiliation == "无子项目")
                {
                    OtherSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type!='主要审批手续' ORDER BY Type,convert(INT, Code);SELECT DISTINCT Type,TypeCode FROM Busi_ApprovalConfig WHERE Type!='主要审批手续' order by TypeCode";
                    OtherDs = GetDatabase().ExecuteDataSet(OtherSql);
                }
            }
            else
            {
                if (count == 0)//主项目
                {
                    OtherSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type!='主要审批手续' AND Name  IN ('规划设计条件','民用建筑节能强制性标准审查','非民用建筑合理用能审查','水土保持') ORDER BY Type,convert(INT, Code);SELECT DISTINCT Type,TypeCode FROM Busi_ApprovalConfig WHERE Type!='主要审批手续'  AND Type IN ('规划审批','节能审查','其他审批') order by TypeCode";
                    OtherDs = GetDatabase().ExecuteDataSet(OtherSql);
                }
                else//子项目
                {
                    OtherSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type!='主要审批手续'  AND Name NOT  IN ('规划设计条件','民用建筑节能强制性标准审查','非民用建筑合理用能审查','水土保持') ORDER BY Type,convert(INT, Code);SELECT DISTINCT Type,TypeCode FROM Busi_ApprovalConfig WHERE Type!='主要审批手续'  AND Type NOT  IN ('节能审查') order by TypeCode";

                    OtherDs = GetDatabase().ExecuteDataSet(OtherSql);
                }
            }

            return OtherDs;
        }

        /// <summary>
        /// 保存办理手续
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static void Save(DataSet ds)
        {
            GetDatabase().UpdateDataSet(ds);

        }

        /// <summary>
        /// 获取该项目的主要手续
        /// </summary>
        /// <param name="projGuid">项目Guid</param>
        /// <returns></returns>
        public static DataSet GetProjMainProcedure(string projGuid)
        {
            DataSet MainDs = GetDatabase().ExecuteDataSet("SELECT * FROM V_Procedure_Config WHERE ProjGuid='" + projGuid + "' and TypeCode=0  ORDER BY OrderCode ,TypeCode");
            return MainDs;
        }

        /// <summary>
        /// 获取该项目的子手续
        /// </summary>
        /// <param name="projGuid">项目Guid</param>
        /// <returns></returns>
        public static DataSet GetProjChildProcedure(string projGuid)
        {
            DataSet ChildDs = GetDatabase().ExecuteDataSet("SELECT * FROM V_ProcedureChild_Config WHERE ProjGuid='" + projGuid + "' ORDER BY OrderCode ,TypeCode");
            return ChildDs;
        }

        /// <summary>
        /// 获取该项目其他手续的类型
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjOtherConfig(string projGuid)
        {
            DataSet OtherConfigDs = GetDatabase().ExecuteDataSet("SELECT DISTINCT Type ,TypeCode FROM V_Procedure_Config  WHERE Typecode!=0 and  ProjGuid='" + projGuid + "' ORDER BY TypeCode");
            return OtherConfigDs;
        }


        /// <summary>
        /// 获取该项目的其他手续
        /// </summary>
        /// <param name="projGuid">项目Guid</param>
        /// <param name="Type">手续类型</param>
        /// <returns></returns>
        public static DataSet GetProjOtherProcedure(string projGuid, string Type)
        {
            DataSet OtherDs = GetDatabase().ExecuteDataSet("SELECT * FROM V_Procedure_Config WHERE ProjGuid='" + projGuid + "' and Type='" + Type + "'  ORDER BY convert(INT, Code),TypeCode");
            return OtherDs;
        }

        /// <summary>
        /// 通过项目主键获取设置的手续
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="MainAppList">主手续字符串</param>
        /// <param name="ApproveList">手续字符串</param>
        /// <param name="ApproveTypeList">手续类别字符串</param>
        /// <param name="topProj">如果是子项目返回主项目手续</param>
        public static void GetSelectedApproveByProjGuid(string ProjGuid, out string MainAppList, out string ApproveList, out string ApproveTypeList, out string topProj)
        {
            //0填报了 1审核 2退回
            Database db = DatabaseFactory.CreateDatabase();
            //DataSet ds = db.ExecuteDataSet(string.Format("select distinct type from Busi_ApprovalConfig where Guid in (select ApprovalGuid from Busi_ProjProcedure where ApprovalGuid in (select Guid from Busi_ApprovalConfig) and ProjGuid='{0}') and SysStatus<>-1;select * from Busi_ApprovalConfig where Guid in (select ApprovalGuid from Busi_ProjProcedure where ApprovalGuid in (select Guid from Busi_ApprovalConfig) and ProjGuid='{1}') and SysStatus<>-1 order by OrderCode asc;"
            //     + "select * from Busi_ProjProposal where ProjGuid='{2}' and SysStatus<>-1;select * from Busi_ProjFsbtyStudy where ProjGuid='{3}' and SysStatus<>-1;select * from Busi_ProjInitialDesign where ProjGuid='{4}' and SysStatus<>-1;select * from Busi_ProjButget where ProjGuid='{5}' and SysStatus<>-1;select * from Busi_LicenseReply where ProjGuid='{6}' and SysStatus<>-1;select * from Busi_CstrctProcessCompile where ProjGuid='{7}' and SysStatus<>-1;"
            //     + "select * from Busi_ProjRegister where SysStatus<>-1;select * from Busi_LicenseReply where SysStatus<>-1;select * from Busi_CstrctProcess where SysStatus<>-1;select * from Busi_AppReportDoc where ProjGuid='{6}' and SysStatus<>-1"
            //     , ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid));
            DataSet ds = db.ExecuteDataSet(string.Format("select distinct type from Busi_ApprovalConfig where Guid in (select ApprovalGuid from Busi_ProjProcedure where ApprovalGuid in (select Guid from Busi_ApprovalConfig) and ProjGuid='{0}') and SysStatus<>-1;select * from Busi_ApprovalConfig where Guid in (select ApprovalGuid from Busi_ProjProcedure where ApprovalGuid in (select Guid from Busi_ApprovalConfig) and ProjGuid='{1}') and SysStatus<>-1 order by OrderCode asc;"
                 + "select * from Busi_ProjProposal where ProjGuid='{2}' and SysStatus<>-1;select * from Busi_ProjFsbtyStudy where ProjGuid='{3}' and SysStatus<>-1;select * from Busi_ProjInitialDesign where ProjGuid='{4}' and SysStatus<>-1;select * from Busi_ProjButget where ProjGuid='{5}' and SysStatus<>-1;select * from Busi_LicenseReply where ProjGuid='{6}' and SysStatus<>-1;select * from Busi_CstrctProcessCompile where ProjGuid='{7}' and SysStatus<>-1;"
                 + "select * from Busi_ProjRegister where SysStatus<>-1;select * from Busi_LicenseReply where SysStatus<>-1;select * from View_CstrctProcessAllData order by status desc;select * from Busi_AppReportDoc where ProjGuid='{6}' and SysStatus<>-1;"
                 + "select * from Busi_ProjInviteApproveMain where SysStatus<>-1;"
                 , ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid, ProjGuid));
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            DataTable ProjProposal = ds.Tables[2];//项目建议书批复
            DataTable ProjFsbtyStudy = ds.Tables[3];//可行性研究批复
            DataTable ProjInitialDesign = ds.Tables[4];//初步设计批复
            DataTable ProjButget = ds.Tables[5];//项目预算批复
            DataTable LicenseReply = ds.Tables[6];//证照批复
            DataTable CstrctProcess = ds.Tables[7];//审批信息
            DataTable ProjRegister = ds.Tables[8];
            DataTable AllLicenseReply = ds.Tables[9];//所有证照批复
            DataTable AllCstrctProcess = ds.Tables[10];//所有审批信息
            DataTable AppReportDoc = ds.Tables[11];//项目备案申请文件证照批复
            DataTable InviteApproveMain = ds.Tables[12];//特殊手续（工程招标方式审批，政府采购方式审批）
            DataRow[] prodr = ProjRegister.Select(string.Format("Guid='{0}'", ProjGuid));
            topProj = "";
            if (prodr.Count() > 0)
            {
                string topguid = prodr[0]["TopGuid"].ToString();
                if (topguid.ToString() != "" && topguid != null)
                {
                    DataRow[] topdr = ProjRegister.Select(string.Format("Guid='{0}'", topguid));
                    if (topdr.Count() > 0)
                    {
                        string ProjAffiliation = topdr[0]["ProjAffiliation"].ToString();
                        string topProcess = string.Empty;
                        switch (ProjAffiliation)
                        {
                            //集中立项可研，子项目独立批复概算
                            case "6921FF68-A789-4BDD-9AC1-6B8CA13B49C1":
                                DataRow[] cp1 = AllCstrctProcess.Select(string.Format("ProjGuid='{0}' and ApprovalGuid in ('F12CD147-DB2A-4904-968B-997BC853C148','A942FCA7-EF43-4BCB-B476-388ED2D89273','1FAADF76-A228-4272-8B92-420BEE102043')", topguid));
                                string topProcess1 = string.Empty;
                                string topProcess2 = string.Empty;
                                string topProcess3 = string.Empty;
                                if (cp1.Length > 0)
                                {
                                    for (int i = 0; i < cp1.Length; i++)
                                    {
                                        switch (cp1[i]["ApprovalGuid"].ToString())
                                        {
                                            case "F12CD147-DB2A-4904-968B-997BC853C148":
                                                switch (cp1[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProcess1 = "green^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                    case "2":
                                                        topProcess1 = "red^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                    default:
                                                        topProcess1 = "blue^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                }
                                                break;
                                            case "A942FCA7-EF43-4BCB-B476-388ED2D89273":
                                                switch (cp1[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProcess2 = "green^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                    case "2":
                                                        topProcess2 = "red^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                    default:
                                                        topProcess2 = "blue^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                }
                                                break;
                                            case "1FAADF76-A228-4272-8B92-420BEE102043":
                                                switch (cp1[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProcess3 = "green^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                    case "2":
                                                        topProcess3 = "red^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                    default:
                                                        topProcess3 = "blue^" + cp1[0]["Guid"].ToString();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    if (topProcess1 == string.Empty)
                                        topProcess1 = "black^";
                                    if (topProcess2 == string.Empty)
                                        topProcess2 = "black^";
                                    if (topProcess3 == string.Empty)
                                        topProcess3 = "black^";

                                    topProcess = topProcess1 + "," + topProcess2 + "," + topProcess3;
                                }
                                else
                                {
                                    topProcess1 = "black^";
                                    topProcess2 = "black^";
                                    topProcess3 = "black^";
                                }

                                string topProj1 = string.Empty;
                                string topProj2 = string.Empty;
                                string topProj3 = string.Empty;
                                DataRow[] lr1 = AllLicenseReply.Select(string.Format("ProjGuid='{0}' and ApprovalGuid in ('F12CD147-DB2A-4904-968B-997BC853C148','A942FCA7-EF43-4BCB-B476-388ED2D89273','1FAADF76-A228-4272-8B92-420BEE102043')", topguid));
                                if (lr1.Length > 0)
                                {
                                    for (int i = 0; i < lr1.Length; i++)
                                    {
                                        switch (cp1[i]["ApprovalGuid"].ToString())
                                        {
                                            case "F12CD147-DB2A-4904-968B-997BC853C148":
                                                switch (lr1[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@green@" + topProcess1 + "@" + topguid;
                                                        break;
                                                    case "2":
                                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@red@" + topProcess1 + "@" + topguid;
                                                        break;
                                                    default:
                                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@blue@" + topProcess1 + "@" + topguid;
                                                        break;
                                                }
                                                break;
                                            case "A942FCA7-EF43-4BCB-B476-388ED2D89273":
                                                switch (lr1[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@green@" + topProcess2 + "@" + topguid;
                                                        break;
                                                    case "2":
                                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@red@" + topProcess2 + "@" + topguid;
                                                        break;
                                                    default:
                                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@blue@" + topProcess2 + "@" + topguid;
                                                        break;
                                                }
                                                break;
                                            case "1FAADF76-A228-4272-8B92-420BEE102043":
                                                switch (lr1[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProj3 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@green@" + topProcess3 + "@" + topguid;
                                                        break;
                                                    case "2":
                                                        topProj3 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@red@" + topProcess3 + "@" + topguid;
                                                        break;
                                                    default:
                                                        topProj3 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@blue@" + topProcess3 + "@" + topguid;
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    if (topProj1 == string.Empty)
                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@black@" + topProcess1 + "@" + topguid;
                                    if (topProj2 == string.Empty)
                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@black@" + topProcess2 + "@" + topguid;
                                    if (topProj3 == string.Empty)
                                        topProj3 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@black@" + topProcess3 + "@" + topguid;

                                    topProj = topProj1 + "," + topProj2 + "," + topProj3;
                                }
                                else
                                {
                                    topProj = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@black@" + topProcess1 + "@" + topguid + ",项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@black@" + topProcess2 + "@" + topguid + ",项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@black@" + topProcess3 + "@" + topguid;
                                }
                                break;
                            //集中批复立项可研概算，子项目独立概算
                            case "FA173C37-BF98-4AAD-9997-817835165E29":
                                DataRow[] cp2 = AllCstrctProcess.Select(string.Format("ProjGuid='{0}' and ApprovalGuid in ('F12CD147-DB2A-4904-968B-997BC853C148','A942FCA7-EF43-4BCB-B476-388ED2D89273','A79D200D-0496-451B-A28A-05C8D24FC39E','1FAADF76-A228-4272-8B92-420BEE102043')", topguid));
                                topProcess1 = string.Empty;
                                topProcess2 = string.Empty;
                                topProcess3 = string.Empty;
                                string topProcess4 = string.Empty;
                                if (cp2.Length > 0)
                                {
                                    for (int i = 0; i < cp2.Length; i++)
                                    {
                                        switch (cp2[i]["ApprovalGuid"].ToString())
                                        {
                                            case "F12CD147-DB2A-4904-968B-997BC853C148":
                                                switch (cp2[0]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProcess1 = "green^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    case "2":
                                                        topProcess1 = "red^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    default:
                                                        topProcess1 = "blue^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                }
                                                break;
                                            case "A942FCA7-EF43-4BCB-B476-388ED2D89273":
                                                switch (cp2[0]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProcess2 = "green^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    case "2":
                                                        topProcess2 = "red^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    default:
                                                        topProcess2 = "blue^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                }
                                                break;
                                            case "A79D200D-0496-451B-A28A-05C8D24FC39E":
                                                switch (cp2[0]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProcess3 = "green^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    case "2":
                                                        topProcess3 = "red^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    default:
                                                        topProcess3 = "blue^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                }
                                                break;
                                            case "1FAADF76-A228-4272-8B92-420BEE102043":
                                                switch (cp2[0]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProcess4 = "green^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    case "2":
                                                        topProcess4 = "red^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                    default:
                                                        topProcess4 = "blue^" + cp2[0]["Guid"].ToString();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    if (topProcess1 == string.Empty)
                                        topProcess1 = "black^";
                                    if (topProcess2 == string.Empty)
                                        topProcess2 = "black^";
                                    if (topProcess3 == string.Empty)
                                        topProcess3 = "black^";
                                    if (topProcess4 == string.Empty)
                                        topProcess4 = "black^";

                                    topProcess = topProcess1 + "," + topProcess2 + "," + topProcess3 + "," + topProcess4;
                                }
                                else
                                {
                                    topProcess1 = "black^";
                                    topProcess2 = "black^";
                                    topProcess3 = "black^";
                                }
                                topProj1 = string.Empty;
                                topProj2 = string.Empty;
                                topProj3 = string.Empty;
                                string topProj4 = string.Empty;
                                DataRow[] lr2 = AllLicenseReply.Select(string.Format("ProjGuid='{0}' and  ApprovalGuid in ('F12CD147-DB2A-4904-968B-997BC853C148','A942FCA7-EF43-4BCB-B476-388ED2D89273','A79D200D-0496-451B-A28A-05C8D24FC39E','1FAADF76-A228-4272-8B92-420BEE102043')", topguid, "F12CD147-DB2A-4904-968B-997BC853C122"));
                                if (lr2.Length > 0)
                                {
                                    for (int i = 0; i < lr2.Length; i++)
                                    {
                                        switch (cp2[i]["ApprovalGuid"].ToString())
                                        {
                                            case "F12CD147-DB2A-4904-968B-997BC853C148":
                                                switch (lr2[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@green@" + topProcess1 + "@" + topguid;
                                                        break;
                                                    case "2":
                                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@red@" + topProcess1 + "@" + topguid;
                                                        break;
                                                    default:
                                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@blue@" + topProcess1 + "@" + topguid;
                                                        break;
                                                }
                                                break;
                                            case "A942FCA7-EF43-4BCB-B476-388ED2D89273":
                                                switch (lr2[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@green@" + topProcess2 + "@" + topguid;
                                                        break;
                                                    case "2":
                                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@red@" + topProcess2 + "@" + topguid;
                                                        break;
                                                    default:
                                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@blue@" + topProcess2 + "@" + topguid;
                                                        break;
                                                }
                                                break;
                                            case "A79D200D-0496-451B-A28A-05C8D24FC39E":
                                                switch (lr2[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProj3 = "项目初步设计及概算@A79D200D-0496-451B-A28A-05C8D24FC39E@green@" + topProcess3 + "@" + topguid;
                                                        break;
                                                    case "2":
                                                        topProj3 = "项目初步设计及概算@A79D200D-0496-451B-A28A-05C8D24FC39E@red@" + topProcess3 + "@" + topguid;
                                                        break;
                                                    default:
                                                        topProj3 = "项目初步设计及概算@A79D200D-0496-451B-A28A-05C8D24FC39E@blue@" + topProcess3 + "@" + topguid;
                                                        break;
                                                }
                                                break;
                                            case "1FAADF76-A228-4272-8B92-420BEE102043":
                                                switch (lr2[i]["Status"].ToString())
                                                {
                                                    case "1":
                                                        topProj4 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@green@" + topProcess4 + "@" + topguid;
                                                        break;
                                                    case "2":
                                                        topProj4 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@red@" + topProcess4 + "@" + topguid;
                                                        break;
                                                    default:
                                                        topProj4 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@blue@" + topProcess4 + "@" + topguid;
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    if (topProj1 == string.Empty)
                                        topProj1 = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@black@" + topProcess1 + "@" + topguid;
                                    if (topProj2 == string.Empty)
                                        topProj2 = "项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@black@" + topProcess2 + "@" + topguid;
                                    if (topProj3 == string.Empty)
                                        topProj3 = "项目初步设计及概算@A79D200D-0496-451B-A28A-05C8D24FC39E@black@" + topProcess3 + "@" + topguid;
                                    if (topProj4 == string.Empty)
                                        topProj4 = "项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@black@" + topProcess4 + "@" + topguid;

                                    topProj = topProj1 + "," + topProj2 + "," + topProj3 + "," + topProj4;
                                }
                                else
                                {
                                    topProj = "项目建议书@F12CD147-DB2A-4904-968B-997BC853C142@black@" + topProcess1 + "@" + topguid + ",项目可行性研究报告@A942FCA7-EF43-4BCB-B476-388ED2D89273@black@" + topProcess2 + "@" + topguid + ",项目初步设计及概算@A79D200D-0496-451B-A28A-05C8D24FC39E@green@" + topProcess3 + "@" + topguid + ",项目预算审批@1FAADF76-A228-4272-8B92-420BEE102043@black@" + topProcess4 + "@" + topguid;
                                }
                                break;
                        }
                    }
                }
            }
            MainAppList = string.Empty;
            ApproveList = string.Empty;
            ApproveTypeList = string.Empty;
            string ProcessList = string.Empty;
            string name = string.Empty;
            DataRow[] dr;
            foreach (DataRow dr1 in dt1.Select())
            {
                foreach (DataRow dr2 in dt2.Select("type='" + dr1["Type"].ToString() + "'"))
                {
                    //string tcode = " is null";
                    //string orcode = " is null";
                    //if (dr2["TypeCode"] != DBNull.Value)
                    //{
                    //    tcode = "=" + Convert.ToInt32(dr2["TypeCode"]);
                    //}
                    //if (dr2["OrderCode"] != DBNull.Value)
                    //{
                    //    orcode = "=" + Convert.ToInt32(dr2["OrderCode"]);
                    //}
                    if (dr2["Type"].ToString() == "主要审批手续")
                    {
                        name = ApprovalGetNameByGuid(dr2["Guid"].ToString());
                        dr = CstrctProcess.Select(string.Format("ApprovalGuid='{0}'", dr2["Guid"].ToString()));
                        if (dr.Count() > 0)
                        {
                            switch (dr[0]["Status"].ToString())
                            {
                                case "1":
                                    ProcessList = "green^" + dr[0]["Guid"].ToString();
                                    break;
                                case "2":
                                    ProcessList = "red^" + dr[0]["Guid"].ToString();
                                    break;
                                default:
                                    ProcessList = "blue^" + dr[0]["Guid"].ToString();
                                    break;
                            }
                        }
                        else
                        {
                            ProcessList = "black^";
                        }

                        if (name == "项目建议书")
                        {
                            dr = ProjProposal.Select();
                            if (dr.Count() > 0)
                            {
                                switch (dr[0]["Status"].ToString())
                                {
                                    case "1":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@green@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    case "2":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@red@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    default:
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@blue@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                }
                            }
                            else
                            {
                                MainAppList += name + "@" + dr2["Guid"].ToString() + "@black@" + ProcessList + "@,";
                            }
                        }
                        else if (name == "项目可行性研究报告")
                        {
                            dr = ProjFsbtyStudy.Select();
                            if (dr.Count() > 0)
                            {
                                switch (dr[0]["Status"].ToString())
                                {
                                    case "1":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@green@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    case "2":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@red@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    default:
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@blue@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                }
                            }
                            else
                            {
                                MainAppList += name + "@" + dr2["Guid"].ToString() + "@black@" + ProcessList + "@,";
                            }
                        }
                        else if (name == "项目初步设计及概算")
                        {
                            dr = ProjInitialDesign.Select();
                            if (dr.Count() > 0)
                            {
                                switch (dr[0]["Status"].ToString())
                                {
                                    case "1":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@green@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    case "2":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@red@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    default:
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@blue@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                }
                            }
                            else
                            {
                                MainAppList += name + "@" + dr2["Guid"].ToString() + "@black@" + ProcessList + "@,";
                            }
                        }
                        else if (name == "项目预算审批")
                        {
                            dr = ProjButget.Select();
                            if (dr.Count() > 0)
                            {
                                switch (dr[0]["Status"].ToString())
                                {
                                    case "1":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@green@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    case "2":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@red@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    default:
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@blue@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                }
                            }
                            else
                            {
                                MainAppList += name + "@" + dr2["Guid"].ToString() + "@black@" + ProcessList + "@,";
                            }
                        }
                        else if (name == "项目备案申请文件" || name == "项目核准申请报告")
                        {
                            dr = AppReportDoc.Select();
                            if (dr.Count() > 0)
                            {
                                switch (dr[0]["Status"].ToString())
                                {
                                    case "1":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@green@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    case "2":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@red@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    default:
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@blue@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                }
                            }
                            else
                            {
                                MainAppList += name + "@" + dr2["Guid"].ToString() + "@black@" + ProcessList + "@,";
                            }
                        }
                        else
                        {
                            dr = LicenseReply.Select(string.Format("ApprovalGuid='{0}'", dr2["Guid"].ToString()));
                            if (dr.Count() > 0)
                            {
                                switch (dr[0]["Status"].ToString())
                                {
                                    case "1":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@green@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    case "2":
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@red@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                    default:
                                        MainAppList += name + "@" + dr2["Guid"].ToString() + "@blue@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                        break;
                                }
                            }
                            else
                            {
                                MainAppList += name + "@" + dr2["Guid"].ToString() + "@black@" + ProcessList + "@,";
                            }
                        }
                    }
                    else if (dr2["Guid"].ToString() == "8A223E20-6687-4515-A4E1-AD117A3A48C0" || dr2["Guid"].ToString() == "BFDF4241-8965-4062-BF91-C79E766893C3")
                    {
                        name = ApprovalGetNameByGuid(dr2["Guid"].ToString());
                        DataRow[] drs = InviteApproveMain.Select("ApproveGuid='" + dr2["Guid"].ToString() + "' and ProjGuid='" + ProjGuid + "'");
                        if (drs.Count() > 0)
                        {
                            switch (drs[0]["Status"].ToString())
                            {
                                case "1":
                                    ApproveList += name + "@" + dr2["Guid"].ToString() + "@green@" + drs[0]["Guid"].ToString() + ",";
                                    break;
                                case "2":
                                    ApproveList += name + "@" + dr2["Guid"].ToString() + "@red@" + drs[0]["Guid"].ToString() + ",";
                                    break;
                                default:
                                    ApproveList += name + "@" + dr2["Guid"].ToString() + "@blue@" + drs[0]["Guid"].ToString() + ",";
                                    break;
                            }
                        }
                        else
                            ApproveList += name + "@" + dr2["Guid"].ToString() + "@black@,";
                    }
                    else
                    {
                        name = ApprovalGetNameByGuid(dr2["Guid"].ToString());
                        dr = CstrctProcess.Select(string.Format("ApprovalGuid='{0}'", dr2["Guid"].ToString()));
                        if (dr.Count() > 0)
                        {
                            switch (dr[0]["Status"].ToString())
                            {
                                case "1":
                                    ProcessList = "green^" + dr[0]["Guid"].ToString();
                                    break;
                                case "2":
                                    ProcessList = "red^" + dr[0]["Guid"].ToString();
                                    break;
                                default:
                                    ProcessList = "blue^" + dr[0]["Guid"].ToString();
                                    break;
                            }
                        }
                        else
                        {
                            ProcessList = "black^";
                        }
                        if (LicenseReply.Select(string.Format("ApprovalGuid='{0}'", dr2["Guid"].ToString())).Count() > 0)
                        {
                            dr = LicenseReply.Select(string.Format("ApprovalGuid='{0}'", dr2["Guid"].ToString()));
                            switch (dr[0]["Status"].ToString())
                            {
                                case "1":
                                    ApproveList += name + "@" + dr2["Guid"].ToString() + "@green@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                    break;
                                case "2":
                                    ApproveList += name + "@" + dr2["Guid"].ToString() + "@red@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                    break;
                                default:
                                    ApproveList += name + "@" + dr2["Guid"].ToString() + "@blue@" + ProcessList + "@" + dr[0]["Guid"].ToString() + ",";
                                    break;
                            }
                        }
                        else
                        {
                            ApproveList += name + "@" + dr2["Guid"].ToString() + "@black@" + ProcessList + "@,";
                        }
                    }
                }
                ApproveList = ApproveList.TrimEnd(',');
                ApproveList = ApproveList + "&";
                if (dr1["Type"].ToString() != "主要审批手续")
                {
                    ApproveTypeList += dr1["Type"].ToString() + ",";
                }
            }
            ProcessList = ProcessList.TrimEnd(',');
            MainAppList = MainAppList.TrimEnd(',');
            ApproveList = ApproveList.TrimEnd('&');
            ApproveTypeList = ApproveTypeList.TrimEnd(',');
        }

        /// <summary>
        /// 通过手续主键获得手续名称
        /// </summary>
        /// <param name="Guid">手续主键</param>
        /// <returns>手续名称</returns>
        public static string ApprovalGetNameByGuid(string Guid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string Name = db.ExecuteScalar(string.Format("select Name from Busi_ApprovalConfig where Guid='{0}'", Guid)).ToString();
            return Name;
        }

        /// <summary>
        /// 判断该项目选中的手续是否在建设过程中已填
        /// </summary>
        /// <param name="ProjGuid">项目Guid</param>
        /// <param name="ApprovalGuid">审批手续Guid</param>
        /// <returns></returns>
        public static bool isProjProcedureByCstrctProcess(string ProjGuid, string ApprovalGuid)
        {
            bool isCstrctProcess = false;
            int i = Convert.ToInt32(GetDatabase().ExecuteScalar("select count(*) from Busi_CstrctProcess where ProjGuid='" + ProjGuid + "' and ApprovalGuid='" + ApprovalGuid + "' and SysStatus<>-1"));

            if (i > 0)
            {
                isCstrctProcess = true;
            }

            return isCstrctProcess;
        }

        /// <summary>
        /// 保存子手续计划时间
        /// </summary>
        /// <param name="datas">子手续计划时间数据集</param>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>返回影响行数</returns>
        public static int SaveProcedureChild(string datas, string ProjGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int result = 0;
            string sql = string.Format("delete Busi_ProjProcedureChild where ProjGuid='{0}';", ProjGuid);
            string[] list = datas.TrimEnd('@').Split('@');
            if (datas != "")
            {
                for (int i = 0; i < list.Length; i++)
                {
                    string[] data = list[i].Split(',');
                    sql += string.Format("insert into Busi_ProjProcedureChild (Guid,ProjGuid,MApprovalGuid,CProcedureType,CPlanStartDate,CPlanEndDate,Status,SysStatus)" +
                        " values ('{0}','{1}','{2}','{3}','{4}','{5}',0,0);", Guid.NewGuid().ToString(), ProjGuid, data[0], data[1], data[2], data[3]);
                }
                result = db.ExecuteNonQuery(sql);
            }
            return result;
        }

        /// <summary>
        /// 获取子手续计划时间
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>DataTable数据集</returns>
        public static DataTable GetProcedureChildData(string ProjGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select * from Busi_ProjProcedureChild where ProjGuid='{0}';", ProjGuid)).Tables[0];
            return dt;
        }
    }
}
