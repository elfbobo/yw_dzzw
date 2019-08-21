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
    /// 项目基本信息公共类
    /// </summary>
    public class PorjBaseInfo
    {
        /// <summary>
        /// 保存各单位到子表
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="deptGuids">项目主管部门主键集合</param>
        /// <param name="adminGuid">行业主管部门主键集合</param>
        /// <param name="usedGuid">项目使用单位主键集合</param>
        /// <param name="constGuids">项目建设（代建）单位主键集合</param>
        public static int ProjRegisterSaveData(string ProjGuid, string deptGuids, string adminGuid, string usedGuid, string constGuids)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = string.Empty;
            if (deptGuids != "")
            {
                string[] list = deptGuids.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    sql += string.Format("insert into Busi_ProjRegisterDeparts (Guid,ProjGuid,DeptGuid,DeptType,SysStatus,Status) values ('" + Guid.NewGuid().ToString() + "','{0}','{1}','Department',0,0);", ProjGuid, list[i]);
                }
            }
            if (adminGuid != "")
            {
                string[] list = adminGuid.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    sql += string.Format("insert into Busi_ProjRegisterDeparts (Guid,ProjGuid,DeptGuid,DeptType,SysStatus,Status) values ('" + Guid.NewGuid().ToString() + "','{0}','{1}','AdminName',0,0);", ProjGuid, list[i]);
                }
            }
            if (usedGuid != "")
            {
                string[] list = usedGuid.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    sql += string.Format("insert into Busi_ProjRegisterDeparts (Guid,ProjGuid,DeptGuid,DeptType,SysStatus,Status) values ('" + Guid.NewGuid().ToString() + "','{0}','{1}','UsedUnit',0,0);", ProjGuid, list[i]);
                }
            }
            if (constGuids != "")
            {
                string[] list = constGuids.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    sql += string.Format("insert into Busi_ProjRegisterDeparts (Guid,ProjGuid,DeptGuid,DeptType,SysStatus,Status) values ('" + Guid.NewGuid().ToString() + "','{0}','{1}','ConstructionUnitName',0,0);", ProjGuid, list[i]);
                }
            }
            if (sql != string.Empty)
            {
                sql = string.Format("delete Busi_ProjRegisterDeparts where ProjGuid='{0}';" + sql, ProjGuid);
                db.ExecuteNonQuery(sql);
                //更新成功
                return 1;
            }
            else
            {
                //更新失败
                return 0;
            }
        }

        /// <summary>
        /// 删除各单位子表内容
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static void DeleteProjRegisterDepart(string ProjGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            db.ExecuteNonQuery(string.Format("delete Busi_ProjRegisterDeparts where ProjGuid='{0}'", ProjGuid));
        }

        /// <summary>
        /// 根据项目主键和项目类型取责任体系数据集
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns>数据集</returns>
        public static DataSet GetGuidByProjGuidAndDeptType(string ProjGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from Busi_Responsibility where ProjGuid='{0}' and DepartType='1' and SysStatus<>-1;select * from Busi_Responsibility where ProjGuid='{1}' and DepartType='2' and SysStatus<>-1", ProjGuid, ProjGuid));
            return ds;
        }

        /// <summary>
        /// 根据项目主键和项目类型取责任体系数据集
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="html1">主管部门html代码</param>
        /// <param name="html2">建设单位html代码</param>
        public static void GetGuidByProjGuidAndDeptType(string ProjGuid, out string html1, out string html2)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(string.Format("select * from Busi_Responsibility where ProjGuid='{0}' and DepartType='1' and SysStatus<>-1;select * from Busi_Responsibility where ProjGuid='{1}' and DepartType='2' and SysStatus<>-1", ProjGuid, ProjGuid));
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            html1 = "<table class='table' cellpadding='0' cellspacing='0'>";
            html2 = "<table class='table' cellpadding='0' cellspacing='0'>";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i != 0)
                {
                    html1 += "<tr><td class='TdLabel' colspan='4'></td></tr>";
                }
                html1 += "<tr>";
                html1 += "<td class='TdLabel' style='width: 10%'>部门名称</td>";
                html1 += "<td class='TdContent' colspan='3'>" + dt1.Rows[i]["Dept"] + "</td>";
                html1 += "</tr>";
                html1 += "<tr>";
                html1 += "<td class='TdLabel' style='width: 10%'>部门领导</td>";
                html1 += "<td class='TdContent'>" + dt1.Rows[i]["Leader"] + "</td>";
                html1 += "<td class='TdLabel' style='width: 10%'>部门领导电话</td>";
                html1 += "<td class='TdContent'>" + dt1.Rows[i]["LeaderPhone"] + "</td>";
                html1 += "</tr>";
                html1 += "<tr>";
                html1 += "<td class='TdLabel' style='width: 10%'>负责人</td>";
                html1 += "<td class='TdContent'>" + dt1.Rows[i]["MainPerson"] + "</td>";
                html1 += "<td class='TdLabel' style='width: 10%'>电话</td>";
                html1 += "<td class='TdContent'>" + dt1.Rows[i]["MainPhone"] + "</td>";
                html1 += "</tr>";
                html1 += "<tr>";
                html1 += "<td class='TdLabel' style='width: 10%'>责任人</td>";
                html1 += "<td class='TdContent'>" + dt1.Rows[i]["MainUser"] + "</td>";
                html1 += "<td class='TdLabel' style='width: 10%'>责任人电话</td>";
                html1 += "<td class='TdContent'>" + dt1.Rows[i]["MainUserPhone"] + "</td>";
                html1 += "</tr>";
                html1 += "<tr>";
                html1 += "<td class='TdLabel' style='width: 10%'>内部主管处室<br />或部门</td>";
                html1 += "<td class='TdContent'>" + dt1.Rows[i]["MainDept"] + "</td>";
                html1 += "<td class='TdLabel' style='width: 10%'></td>";
                html1 += "<td class='TdContent'></td>";
                html1 += "</tr>";
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (i != 0)
                {
                    html2 += "<tr><td class='TdLabel' colspan='4'></td></tr>";
                }
                html2 += "<tr>";
                html2 += "<td class='TdLabel' style='width: 10%'>部门名称</td>";
                html2 += "<td class='TdContent' colspan='3'>" + dt2.Rows[i]["Dept"] + "</td>";
                html2 += "</tr>";
                html2 += "<tr>";
                html2 += "<td class='TdLabel' style='width: 10%'>部门领导</td>";
                html2 += "<td class='TdContent'>" + dt2.Rows[i]["Leader"] + "</td>";
                html2 += "<td class='TdLabel' style='width: 10%'>部门领导电话</td>";
                html2 += "<td class='TdContent'>" + dt2.Rows[i]["LeaderPhone"] + "</td>";
                html2 += "</tr>";
                html2 += "<tr>";
                html2 += "<td class='TdLabel' style='width: 10%'>负责人</td>";
                html2 += "<td class='TdContent'>" + dt2.Rows[i]["MainPerson"] + "</td>";
                html2 += "<td class='TdLabel' style='width: 10%'>电话</td>";
                html2 += "<td class='TdContent'>" + dt2.Rows[i]["MainPhone"] + "</td>";
                html2 += "</tr>";
                html2 += "<tr>";
                html2 += "<td class='TdLabel' style='width: 10%'>责任人</td>";
                html2 += "<td class='TdContent'>" + dt2.Rows[i]["MainUser"] + "</td>";
                html2 += "<td class='TdLabel' style='width: 10%'>责任人电话</td>";
                html2 += "<td class='TdContent'>" + dt2.Rows[i]["MainUserPhone"] + "</td>";
                html2 += "</tr>";
                html2 += "<tr>";
                html2 += "<td class='TdLabel' style='width: 10%'>内部主管处室<br />或部门</td>";
                html2 += "<td class='TdContent'>" + dt2.Rows[i]["MainDept"] + "</td>";
                html2 += "<td class='TdLabel' style='width: 10%'></td>";
                html2 += "<td class='TdContent'></td>";
                html2 += "</tr>";
            }
            html1 += "</table>";
            html2 += "</table>";
        }

        /// <summary>
        /// 责任体系列表删除方法
        /// </summary>
        /// <param name="ProjGuids">项目主键集合</param>
        /// <returns>返回操作影响数（大于零删除成功）-1为未选中操作数据</returns>
        public static string DeleteListData(string ProjGuids)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string[] list = ProjGuids.Split(',');
            string sql = string.Empty;
            if (ProjGuids.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    sql += string.Format("update Busi_Responsibility set SysStatus=-1 where ProjGuid='{0}';", list[i]);
                }
            }
            else
            {
                return "-1";
            }
            string result = db.ExecuteNonQuery(sql).ToString();
            return result;
        }

        /// <summary>
        /// 获取行业类型
        /// </summary>
        /// <param name="value">行业类型编号</param>
        /// <param name="group">行业类型分组</param>
        /// <returns></returns>
        public static string GetDicNameByGroupValue(string value, string group)
        {
            if (value != "")
            {
                Database db = DatabaseFactory.CreateDatabase();
                return db.ExecuteScalar("select name from Busi_IndustryType where [Value]='" + value + "' and [Group]='" + group + "'").ToString();
            }
            return "";
        }

        /// <summary>
        /// 将所有行业类别拼成json串
        /// </summary>
        /// <returns></returns>
        public static string GetModuleJson()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet dsProjType = db.ExecuteDataSet("SELECT VALUE,Name FROM Busi_IndustryType where [GROUP]='行业类别' AND VALUE LIKE '[A-Z]' ORDER BY VALUE ASC");
            DataSet ds = db.ExecuteDataSet("select * from Busi_IndustryType where [GROUP]='行业类别'");
            string json = "";

            DataRow[] drArr = null;
            for (int i = 0; i < dsProjType.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    json += ",";
                DataRow dr = dsProjType.Tables[0].Rows[i];
                drArr = ds.Tables[0].Select("VALUE LIKE '" + dr["VALUE"].ToString() + "%' and VALUE<>'" + dr["VALUE"].ToString() + "'");
                json += "{name:'" + dr["Name"].ToString() + "'";
                if (drArr.Length > 0)
                {
                    json += ",children:[";
                    for (int j = 0; j < drArr.Length; j++)
                    {
                        if (j > 0)
                            json += ",";
                        json += "{id:'" + drArr[j]["Value"].ToString() + "',name:'" + drArr[j]["Name"].ToString() + "'}";
                    }
                    json += "]";
                }
                json += "}";
            }

            return "[" + json + "]";
        }

        /// <summary>
        /// 返回登记信息中存在日期
        /// </summary>
        /// <param name="Type">新建或列表页</param>
        /// <returns>存在日期字符串</returns>
        public static string GetProjTime(string Type)
        {
            string times = string.Empty;
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet("select distinct ProjCheckYear from Busi_ProjRegister order by ProjCheckYear").Tables[0];
            string DateTimes = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Select())
                {
                    if (dr["ProjCheckYear"].ToString() != "")
                    {
                        if (Type == "Create")
                        {
                            if (Convert.ToInt32(dr["ProjCheckYear"].ToString().Substring(0, 4)) < DateTime.Now.Year && Convert.ToInt32(dr["ProjCheckYear"].ToString().Substring(4, 2)) < DateTime.Now.Month)
                            {
                                DateTimes += dr["ProjCheckYear"].ToString() + ",";
                            }
                        }
                        else if (Type == "List")
                        {
                            DateTimes += dr["ProjCheckYear"].ToString() + ",";
                        }
                    }
                }
                DateTimes = DateTimes.TrimEnd(',');
            }
            return DateTimes;
        }

        /// <summary>
        /// 判断在表中是否存在该项目数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static DataSet TableIsData(string tableName, string projGuid)
        {
            string sql = "";
            if (tableName == "V_Proj_ProjProcedureCount")
            {
                sql = "select * from " + tableName + " where Guid='" + projGuid + "' and sysstatus<>-1";
            }
            else if (tableName == "Busi_ProjRegister")
            {
                sql = "select * from " + tableName + " where Guid='" + projGuid + "' and sysstatus<>-1";
            }
            else
            {
                sql = "select * from " + tableName + " where ProjGuid='" + projGuid + "' and sysstatus<>-1";
            }
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(sql);
            return ds;
        }


        /// <summary>
        /// 执行SQL语句获得结果
        /// </summary>
        /// <param name="Sql">SQL语句</param>
        /// <returns>影响行数</returns>
        public static int RunSqlGetResult(string Sql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int result = db.ExecuteNonQuery(Sql);
            return result;
        }

        /// <summary>
        /// 更新登记信息状态 5为中止/恢复后为0，6为撤销,7为缓建
        /// </summary>
        /// <param name="Guid">项目主键</param>
        /// <param name="Type">更新状态类别</param>
        /// <returns>返回执行是否成功</returns>
        public static int UpdateStatusForRegister(string Guid, string Type)
        {
            Database db = DatabaseFactory.CreateDatabase();
            int result = 0;
            //if (Type == "5")
            //{
            //    result = db.ExecuteNonQuery(string.Format("update Busi_ProjRegister set Status='{0}',SuspensionTime='{1}' where Guid='{2}'", Type, DateTime.Now, Guid));
            //}
            //else if (Type == "6")
            //{
            //    result = db.ExecuteNonQuery(string.Format("update Busi_ProjRegister set Status='{0}',CancelTime='{1}' where Guid='{2}'", Type, DateTime.Now, Guid));
            //}
            //else
            //{
            //    result = db.ExecuteNonQuery(string.Format("update Busi_ProjRegister set Status='{0}' where Guid='{1}'", Type, Guid));
            //}

            if (Type == "5")
            {
                result = db.ExecuteNonQuery(string.Format("update Busi_ProjRegister set Status='{0}',OptDate='{1}' where Guid='{2}'", Type, DateTime.Now, Guid));
            }
            else if (Type == "6")
            {
                result = db.ExecuteNonQuery(string.Format("update Busi_ProjRegister set Status='{0}',OptDate='{1}' where Guid='{2}'", Type, DateTime.Now, Guid));
            }
            else if (Type == "7")
            {
                result = db.ExecuteNonQuery(string.Format("update Busi_ProjRegister set Status='{0}',OptDate='{1}' where Guid='{2}'", Type, DateTime.Now, Guid));
            }
            else
            {
                result = db.ExecuteNonQuery(string.Format("update Busi_ProjRegister set Status='{0}' where Guid='{1}'", Type, Guid));
            }

            return result;
        }

        /// <summary>
        /// 通过项目主键返回对应培训登记记录
        /// </summary>
        /// <param name="ProjGuid">项目Guid</param>
        /// <returns>DataTable</returns>
        public static string ReturnHtmlByProjGuid(string ProjGuid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteDataSet(string.Format("select * from Busi_Training where charindex('{0}',ProjectGuid)>0 order by DeptType desc", ProjGuid)).Tables[0];
            string html = "<table class='table' cellpadding='0' cellspacing='0'>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    html += "<tr><td class='TdLabel' colspan='4'></td></tr>";
                }
                html += "<tr>";
                html += "<td class='TdLabel'>单位类型</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["DeptType"] + "</td>";
                html += "<td class='TdLabel'>培训批次</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["TrainingBatch"] + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td class='TdLabel'>姓名</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["Name"] + "</td>";
                html += "<td class='TdLabel'>电话</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["Phone"] + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td class='TdLabel'>单位</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["DeptName"] + "</td>";
                html += "<td class='TdLabel'>培训时间</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["TrainTime"].ToString().Split(' ')[0] + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td class='TdLabel'>金宏名称</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["JHOAName"] + "</td>";
                html += "<td class='TdLabel'>外网名称</td>";
                html += "<td class='TdContent'>" + dt.Rows[i]["WEBName"] + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        /// <summary>
        /// 获取项目主从关系
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjAffiliation(string ProjGuid) 
        {
            string AffiliationSql = "SELECT ProjName,departName=( SELECT TOP  1 Name FROM Sys_UserGroups WHERE charindex(Guid,Busi_ProjRegister.DepartmentGuid)>0), ProjAffiliation FROM Busi_ProjRegister WHERE (TopGuid IS NULL OR TopGuid='') AND SysStatus<>-1 AND Guid='" + ProjGuid + "'";
            Database db = DatabaseFactory.CreateDatabase();

            return db.ExecuteDataSet(AffiliationSql);
        }

        /// <summary>
        /// 通过主项目获取子项目
        /// </summary>
        /// <param name="TopGuid"></param>
        /// <returns></returns>
        public static DataSet GetChildProject(string TopGuid) 
        {
            string sql = "SELECT Guid FROM Busi_ProjRegister WHERE TopGuid='" + TopGuid + "' AND SysStatus<>-1 AND Status in (0,1)";
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(sql);
        }

        /// <summary>
        /// 获取主管部门下面有没有对应的独立审批子项目、集中立项可研，子项目独立批复概算的主项目
        /// </summary>
        /// <returns></returns>
        public static int ProjAffiliationCount(string Depart)
        {
            string sql = "SELECT count(*) FROM Busi_ProjRegister WHERE DepartmentGuid='9BD9BA2A-FDD6-45D3-BD58-B6F41323BB27'AND ProjAffiliation IN  ('782B4E4D-BFAE-4034-96A4-BD18F95A3C61','6921FF68-A789-4BDD-9AC1-6B8CA13B49C1') AND SysStatus<>-1 AND Status IN (0,1)";
            Database db = DatabaseFactory.CreateDatabase();
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }

    }
}
