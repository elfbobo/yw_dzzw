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
    /// 机场项目办理计划
    /// </summary>
    public class JCBuildProcedureInfo
    {
        static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// 根据投资类型和审批类型获取相对应的主要审批手续
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static DataSet GetMainByType( string ProjGuid)
        {
              string MainSql = "";
            //string ProjAffiliationSql = "";//获取该项目的项目主从关系
            //DataSet AffiliationDs = new DataSet();//项目主从关系
            string ProjAffiliation = BuildProcedureInfo.GetProjAffiliation(ProjGuid);

            //判断该项目是主项目还是子项目
            int count =BuildProcedureInfo.GetProjTop(ProjGuid);
            //如果是主项目
            if (count == 0)
            {
                // //判断主从关系是否是集中批复立项可研概算，子项目独立概算
                if (ProjAffiliation == "集中批复立项可研概算")
                {
                    //只显示集中批复立项可研概算
                    MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('1','2','3','4','5','6','7','10','12') ORDER BY convert(INT, Code)";
                }
            }
            //如果是子项目
            else
            {
                // //判断主从关系是否是集中批复立项可研概算，子项目独立概算
                if (ProjAffiliation == "集中批复立项可研概算")
                {
                    //根据投资类型查询出相对应的审批手续                
                    //加入集中批复立项可研概算

                        MainSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type='主要审批手续' AND Code IN ('11') ORDER BY OrderCode";               
                }
            }
            DataSet MainDs = GetDatabase().ExecuteDataSet(MainSql);
            return MainDs;
        }


        /// <summary>
        /// 主项目的主从关系是否是集中立项可研，子项目独立批复概算，显示的子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetJzOther(string ProjGuid)
        {
            string jzOhterSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type!='主要审批手续' AND Type IN ('土地审批','环境影响评价审批','规划审批','节能审查','海域使用审批','建设审批','市政审批','招标或政府采购审批','其他审批') AND Name NOT IN ('开工规划验线','掘路') ORDER BY Type,convert(INT, Code);SELECT DISTINCT Type,TypeCode FROM Busi_ApprovalConfig WHERE Type!='主要审批手续' AND Type IN ('土地审批','环境影响评价审批','规划审批','节能审查','海域使用审批','建设审批','招标或政府采购审批','市政审批','其他审批') order by TypeCode";
            DataSet JzOtherDs = GetDatabase().ExecuteDataSet(jzOhterSql);

            return JzOtherDs;
        }

        /// <summary>
        /// 子项目的子手续
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetZother(string ProjGuid) 
        {
            string ZOhterSql = "SELECT * FROM Busi_ApprovalConfig WHERE Type!='主要审批手续'  AND Name IN ('开工规划验线','掘路')  ORDER BY Type,convert(INT, Code);SELECT DISTINCT Type,TypeCode FROM Busi_ApprovalConfig WHERE Type!='主要审批手续' AND Type  IN ('规划审批','建设审批') order by TypeCode";
            DataSet ZOtherDs = GetDatabase().ExecuteDataSet(ZOhterSql);

            return ZOtherDs;
        }

        /// <summary>
        /// 获取机场项目的标记
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetProjType(string ProjGuid)
        {
            string typeSql = "SELECT ProjType FROM Busi_ProjRegister WHERE Guid='" + ProjGuid + "' ";
            string projType = GetDatabase().ExecuteScalar(typeSql).ToString();

            return projType;
        }
    }

}
