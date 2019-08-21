using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.ProjectCore;

namespace Yawei.FacadeCore.Project
{
    /// <summary>
    /// 建设程序公共类表现层
    /// </summary>
    public class BuildProcedureInfoFacade
    {

        /// <summary>
        /// 判断项目是主项目还是子项目
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static int GetProjTop(string ProjGuid)
        {
            return BuildProcedureInfo.GetProjTop(ProjGuid);
        }

        /// <summary>
        /// 获取该项目的项目主从关系
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetProjAffiliation(string ProjGuid)
        {
            return BuildProcedureInfo.GetProjAffiliation(ProjGuid);
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
            return BuildProcedureInfo.GetMainByType(NationType, InvestType, ProjGuid);
        }


        /// <summary>
        /// 获取其他审批手续
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static DataSet GetOther(string ProjGuid)
        {
            return BuildProcedureInfo.GetOther(ProjGuid);
        }

        /// <summary>
        /// 保存办理手续
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static void Save(DataSet ds)
        {
            BuildProcedureInfo.Save(ds);
        }

        /// <summary>
        /// 获取该项目的主要手续
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjProcedure(string projGuid)
        {
            return BuildProcedureInfo.GetProjMainProcedure(projGuid);
        }

        /// <summary>
        /// 获取该项目的子手续
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjChildProcedure(string projGuid)
        {
            return BuildProcedureInfo.GetProjChildProcedure(projGuid);
        }

        /// <summary>
        /// 获取该项目的其他手续类型
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjOtherConfig(string projGuid)
        {
            return BuildProcedureInfo.GetProjOtherConfig(projGuid);
        }

        /// <summary>
        /// 获取该项目的其他手续
        /// </summary>
        /// <param name="projGuid"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static DataSet GetProjOtherProcedure(string projGuid, string Type)
        {
            return BuildProcedureInfo.GetProjOtherProcedure(projGuid, Type);
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
            BuildProcedureInfo.GetSelectedApproveByProjGuid(ProjGuid, out MainAppList, out ApproveList, out ApproveTypeList, out topProj);
        }

        /// <summary>
        /// 判断该项目选中的手续是否在建设过程中已填
        /// </summary>
        /// <param name="ProjGuid">项目Guid</param>
        /// <param name="ApprovalGuid">审批手续Guid</param>
        /// <returns></returns>
        public static bool isProjProcedureByCstrctProcess(string ProjGuid, string ApprovalGuid)
        {
            return BuildProcedureInfo.isProjProcedureByCstrctProcess(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 保存子手续计划时间
        /// </summary>
        /// <param name="datas">子手续计划时间数据集</param>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>返回影响行数</returns>
        public static int SaveProcedureChild(string datas, string ProjGuid)
        {
            return BuildProcedureInfo.SaveProcedureChild(datas, ProjGuid);
        }

        /// <summary>
        /// 获取子手续计划时间
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>DataTable数据集</returns>
        public static DataTable GetProcedureChildData(string ProjGuid)
        {
            return BuildProcedureInfo.GetProcedureChildData(ProjGuid);
        }
    }
}
