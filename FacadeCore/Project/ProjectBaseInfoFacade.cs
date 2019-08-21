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
    /// 项目基本信息公共类表现层
    /// </summary>
    public static class ProjectBaseInfoFacade
    {
        /// <summary>
        /// 保存各单位到子表
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="deptGuids">项目主管部门主键集合</param>
        /// <param name="adminGuid">行业主管部门主键集合</param>
        /// <param name="usedGuid">项目使用单位主键集合</param>
        /// <param name="constGuids">项目建设（代建）单位主键集合</param>
        /// <returns></returns>
        public static int ProjRegisterSaveData(string ProjGuid, string deptGuids, string adminGuid, string usedGuid, string constGuids)
        {
            return PorjBaseInfo.ProjRegisterSaveData(ProjGuid, deptGuids, adminGuid, usedGuid, constGuids);
        }

        /// <summary>
        /// 删除各单位子表内容
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static void DeleteProjRegisterDepart(string ProjGuid)
        {
            PorjBaseInfo.DeleteProjRegisterDepart(ProjGuid);
        }

        /// <summary>
        /// 根据项目主键和项目类型取责任体系数据集
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>数据集</returns>
        public static DataSet GetGuidByProjGuidAndDeptType(string ProjGuid)
        {
            return PorjBaseInfo.GetGuidByProjGuidAndDeptType(ProjGuid);
        }

        /// <summary>
        /// 根据项目主键和项目类型取责任体系数据集
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="html1">主管部门html代码</param>
        /// <param name="html2">建设单位html代码</param>
        public static void GetGuidByProjGuidAndDeptType(string ProjGuid,out string html1,out string html2)
        {
            PorjBaseInfo.GetGuidByProjGuidAndDeptType(ProjGuid,out html1,out html2);
        }

        /// <summary>
        /// 责任体系列表删除方法
        /// </summary>
        /// <param name="ProjGuids">项目主键集合</param>
        /// <returns>返回操作影响数（大于零删除成功）-1为未选中操作数据</returns>
        public static string DeleteListData(string ProjGuids)
        {
            return PorjBaseInfo.DeleteListData(ProjGuids);
        }

        /// <summary>
        /// 获取行业类型
        /// </summary>
        /// <param name="value">行业类型编号</param>
        /// <param name="group">行业类型分组</param>
        /// <returns></returns>
        public static string GetDicNameByGroupValue(string value, string group)
        {
            return PorjBaseInfo.GetDicNameByGroupValue(value, group);
        }

        /// <summary>
        /// 将所有行业类别拼成json串
        /// </summary>
        /// <returns></returns>
        public static string GetModuleJson()
        {
            return PorjBaseInfo.GetModuleJson();
        }

        /// <summary>
        /// 返回登记信息中存在日期
        /// </summary>
        /// <param name="Type">新建或列表页</param>
        /// <returns>存在日期字符串</returns>
        public static string GetProjTime(string Type)
        {
            return PorjBaseInfo.GetProjTime(Type);
        }

        /// <summary>
        /// 判断在表中是否存在该项目数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet TableIsData(string tableName, string projGuid)
        {
            return PorjBaseInfo.TableIsData(tableName, projGuid);
        }


        /// <summary>
        /// 执行SQL语句获得结果
        /// </summary>
        /// <param name="Sql">SQL语句</param>
        /// <returns>影响行数</returns>
        public static int RunSqlGetResult(string Sql)
        {
            return PorjBaseInfo.RunSqlGetResult(Sql);
        }

        /// <summary>
        /// 更新登记信息状态 5为中止/恢复后为0，6为撤销
        /// </summary>
        /// <param name="Guid">项目主键</param>
        /// <param name="Type">更新状态类别</param>
        /// <returns>返回执行是否成功</returns>
        public static int UpdateStatusForRegister(string Guid, string Type)
        {
            return PorjBaseInfo.UpdateStatusForRegister(Guid, Type);
        }

        /// <summary>
        /// 通过项目主键返回对应培训登记记录
        /// </summary>
        /// <param name="ProjGuid">项目Guid</param>
        /// <returns>前台html</returns>
        public static string ReturnHtmlByProjGuid(string ProjGuid)
        {
            return PorjBaseInfo.ReturnHtmlByProjGuid(ProjGuid);
        }

        /// <summary>
        /// 获取项目主从关系
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjAffiliation(string ProjGuid)
        {
            return PorjBaseInfo.GetProjAffiliation(ProjGuid);
        }

         /// <summary>
        /// 通过主项目获取子项目
        /// </summary>
        /// <param name="TopGuid"></param>
        /// <returns></returns>
        public static DataSet GetChildProject(string TopGuid)
        {
            return PorjBaseInfo.GetChildProject(TopGuid);
        }

        /// <summary>
        /// 获取主管部门下面有没有对应的独立审批子项目、集中立项可研，子项目独立批复概算的主项目
        /// </summary>
        /// <returns></returns>
        public static int ProjAffiliationCount(string Depart)
        {
            return PorjBaseInfo.ProjAffiliationCount(Depart);
        }
    }
}
