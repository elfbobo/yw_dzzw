using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Yawei.IndustryManagerCore;

namespace Yawei.FacadeCore.Industry
{
    /// <summary>
    /// 初始化信息
    /// </summary>
    public class InitialInfo
    {
        /// <summary>
        /// 获取行业监管责任系统下的部门
        /// </summary>
        /// <returns></returns>
        public static DataSet GetSupUnitName()
        {
            return IMCBaseInfo.GetSupUnitName();
        }

        /// <summary>
        /// 获取项目的建设单位名称
        /// </summary>
        /// <param name="projGuid"></param>
        /// <returns></returns>
        public static string GetProjCstrctUnitName(string projGuid)
        {
            return IMCBaseInfo.GetProjCstrctUnitName(projGuid);
        }

        /// <summary>
        /// 获取项目名称
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static string GetProjNameByGuid(string ProjGuid)
        {
            return IMCBaseInfo.GetProjNameByGuid(ProjGuid);
        }

        /// <summary>
        /// 获取建设过程名称
        /// </summary>
        /// <param name="AppGuid"></param>
        /// <returns></returns>
        public static DataSet GetAppConfigNameByGuid(string AppGuid)
        {
            return IMCBaseInfo.GetAppConfigInfoByGuid(AppGuid);
        }

        /// <summary>
        /// 检查该建设过程是否附带专家评审
        /// </summary>
        /// <param name="AppGuid"></param>
        /// <returns></returns>
        public static bool CheckExportReview(string AppGuid)
        {
            return IMCBaseInfo.CheckIfContainExpertReview(AppGuid);
        }

        /// <summary>
        /// 获取对应手续的批复基本信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="ApprovalGuid">手续主键</param>
        /// <returns>批复基本信息DataTable</returns>
        public static DataTable GetCstrctProcessReply(string ProjGuid, string ApprovalGuid)
        {
            return IMCBaseInfo.GetCstrctProcessReply(ProjGuid, ApprovalGuid);
        }

        /// <summary>
        /// 通过项目主键获取项目信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static DataSet GetProjInfoByGuid(string ProjGuid)
        {
            return IMCBaseInfo.GetProjInfoByGuid(ProjGuid);
        }

        /// <summary>
        /// 根据字典表的主键获取Name
        /// </summary>
        /// <param name="MapGuid">字典表主键</param>
        /// <returns></returns>
        public static string GetMapNameByGuid(string MapGuid)
        {
            return IMCBaseInfo.GetMapNameByGuid(MapGuid);
        }


        /// <summary>
        /// 初始化项目里程碑信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="TopGuid">项目的TopGuid</param>
        /// <param name="NationType">审批类型</param>
        /// <param name="ProjCategory">项目类型</param>
        /// <param name="CityInvest">省级投资</param>
        /// <param name="CenterInvest"></param>
        public static void InitialProjMileStone(string ProjGuid, string TopGuid, string NationType, string ProjCategory, bool CityInvest, bool CenterInvest)
        {
            //审批类型为审批，字典表中审批类的主键为6CDCC6AA-ED28-4616-96D9-6E39229BB956
            int count = IMCBaseInfo.CheckProjMileStone(ProjGuid);
            DataSet DoneStatus = IMCBaseInfo.ExecNonQuery("select Guid,Name,Mark from Sys_Mapping where DirectoryGuid='072C5AC3-F1EE-49B9-B7A2-D8EEF7A6B407'");
            if (count == 0)
            {
                if (NationType == "6CDCC6AA-ED28-4616-96D9-6E39229BB956")//审批类
                {
                    ProjApproveMileStone(ProjGuid, TopGuid, ProjCategory, CityInvest, CenterInvest, DoneStatus);
                }
                else if (NationType == "9D3BEE82-53D9-45E0-8C11-651BE5E754AC")
                {
                    ProjApproveMileStone1(ProjGuid, TopGuid, ProjCategory, CityInvest, CenterInvest, DoneStatus);
                }
                else
                    ProjCheckRecordMileStone(ProjGuid, NationType, ProjCategory, CityInvest, CenterInvest, DoneStatus);//核准备案类
            }

            object obj = IMCBaseInfo.CheckNewMileStone(ProjGuid);
            if(obj!=null && int.Parse(obj.ToString())==0)
                InitMileStoneDoneStatus(ProjGuid, DoneStatus);
        }

        /// <summary>
        /// 审批类型初始化里程碑信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="TopGuid">项目的TopGuid</param>
        /// <param name="ProjCategory">项目类型</param>
        /// <param name="CityInvest">省级投资</param>
        /// <param name="CenterInvest">中央投资</param>
        ///<param name="DoneStatus"></param>
        static void ProjApproveMileStone(string ProjGuid, string TopGuid, string ProjCategory, bool CityInvest, bool CenterInvest, DataSet DoneStatus)
        {
            DataSet doc = null;
            string DirectoryGuid = "";
            string OthSql = "";
            string ProjAffiliation = "";//项目主从关系


            if (TopGuid != "")
            {
                ProjAffiliation = GetProjAffiliationByGuid(TopGuid);
                if (ProjAffiliation == "6921FF68-A789-4BDD-9AC1-6B8CA13B49C1")//项目主从关系为集中立项可研，子项目独立批复概算
                    OthSql += " and Name not in ('项目建议书','可行性研究报告')";
                if (ProjAffiliation == "FA173C37-BF98-4AAD-9997-817835165E29")//项目主从关系为集中批复立项可研概算，子项目独立概算
                    OthSql += " and Name not in ('项目建议书','可行性研究报告','初步设计')";
            }

            if (CityInvest && (!CenterInvest))
                OthSql += " and Name not in ('资金申请报告')";
            else if (CenterInvest && (!CityInvest))
                OthSql += " and Name not in ('申请省级资金文件')";
            else if (CenterInvest && CityInvest)
                OthSql += "";
            else
                OthSql += " and Name not in ('资金申请报告','申请省级资金文件')";

            #region  审批类非设备采购信息化

            if ((ProjCategory != "190749D5-0526-46FD-96FC-C96281AE926D") && (ProjCategory != "77A6EB3E-D90F-4372-B549-FFD36238B1EA"))
                DirectoryGuid = "95445784-2D6E-47C0-88DA-63A7254FED4C";

            #endregion

            #region  设备采购信息化

            if ((ProjCategory == "190749D5-0526-46FD-96FC-C96281AE926D") || (ProjCategory == "77A6EB3E-D90F-4372-B549-FFD36238B1EA"))
            {
                DirectoryGuid = "C2D82AC2-DBB6-4C58-BC13-8039A62B6335";
                if (ProjCategory == "190749D5-0526-46FD-96FC-C96281AE926D")
                    OthSql += " and Name not in ('软件开发')";

                if (ProjCategory == "77A6EB3E-D90F-4372-B549-FFD36238B1EA")
                    OthSql += " and Name not in ('设备安装')";
            }

            //林业工程(FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6)、城市园林(BD5C451A-025D-45F2-BA9F-CC50DD029301)、水利工程(CDC1A7C3-23BA-4DC3-9B85-D046787441B6)、管线工程（8F8499A3-BAD5-4051-BAB6-290CA5CF9520）
            if ((ProjCategory == "BD5C451A-025D-45F2-BA9F-CC50DD029301") || (ProjCategory == "CDC1A7C3-23BA-4DC3-9B85-D046787441B6") || (ProjCategory == "FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6")) 
            {
                OthSql += " and Name not in ('资金申请报告','基础工程','装修工程','规划验线')";
                //林业工程无设备安装
                if (ProjCategory == "FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6" || ProjCategory == "BD5C451A-025D-45F2-BA9F-CC50DD029301")
                {
                    OthSql += " and Name not in ('设备安装')";
                }
            }
            else if (ProjCategory == "8F8499A3-BAD5-4051-BAB6-290CA5CF9520")
            {
                OthSql += " and Name not in ('基础工程','装修工程','设备安装')";
            }

            doc = IMCBaseInfo.GetMapGuidsByDirectoryGuid(DirectoryGuid, OthSql);
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                    IMCBaseInfo.InitialProjMileStone(ProjGuid, doc.Tables[0].Rows[i]["Guid"].ToString(), doc.Tables[0].Rows[i]["Mark"].ToString());

                //InitMileStoneDoneStatus(ProjGuid, DoneStatus);
            }

            #endregion
        }

        /// <summary>
        /// 只审批初步设计类型初始化里程碑信息
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="TopGuid">项目的TopGuid</param>
        /// <param name="ProjCategory">项目类型</param>
        /// <param name="CityInvest">省级投资</param>
        /// <param name="CenterInvest">中央投资</param>
        ///<param name="DoneStatus"></param>
        static void ProjApproveMileStone1(string ProjGuid, string TopGuid, string ProjCategory, bool CityInvest, bool CenterInvest, DataSet DoneStatus)
        {
            DataSet doc = null;
            string DirectoryGuid = "";
            string OthSql = " and Name not in ('项目建议书','可行性研究报告')";
            string ProjAffiliation = "";//项目主从关系


            if (TopGuid != "")
            {
                ProjAffiliation = GetProjAffiliationByGuid(TopGuid);
                if (ProjAffiliation == "6921FF68-A789-4BDD-9AC1-6B8CA13B49C1")//项目主从关系为集中立项可研，子项目独立批复概算
                    OthSql += " and Name not in ('项目建议书','可行性研究报告')";
                if (ProjAffiliation == "FA173C37-BF98-4AAD-9997-817835165E29")//项目主从关系为集中批复立项可研概算，子项目独立概算
                    OthSql += " and Name not in ('项目建议书','可行性研究报告','初步设计')";
            }

            if (CityInvest && (!CenterInvest))
                OthSql += " and Name not in ('资金申请报告')";
            else if (CenterInvest && (!CityInvest))
                OthSql += " and Name not in ('申请省级资金文件')";
            else if (CenterInvest && CityInvest)
                OthSql += "";
            else
                OthSql += " and Name not in ('资金申请报告','申请省级资金文件')";

            #region  审批类非设备采购信息化

            if ((ProjCategory != "190749D5-0526-46FD-96FC-C96281AE926D") && (ProjCategory != "77A6EB3E-D90F-4372-B549-FFD36238B1EA"))
                DirectoryGuid = "95445784-2D6E-47C0-88DA-63A7254FED4C";

            #endregion

            #region  设备采购信息化

            if ((ProjCategory == "190749D5-0526-46FD-96FC-C96281AE926D") || (ProjCategory == "77A6EB3E-D90F-4372-B549-FFD36238B1EA"))
            {
                DirectoryGuid = "C2D82AC2-DBB6-4C58-BC13-8039A62B6335";
                if (ProjCategory == "190749D5-0526-46FD-96FC-C96281AE926D")
                    OthSql += " and Name not in ('软件开发')";

                if (ProjCategory == "77A6EB3E-D90F-4372-B549-FFD36238B1EA")
                    OthSql += " and Name not in ('设备安装')";
            }

            //林业工程(FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6)、城市园林(BD5C451A-025D-45F2-BA9F-CC50DD029301)、水利工程(CDC1A7C3-23BA-4DC3-9B85-D046787441B6)
            if ((ProjCategory == "BD5C451A-025D-45F2-BA9F-CC50DD029301") || (ProjCategory == "CDC1A7C3-23BA-4DC3-9B85-D046787441B6") || (ProjCategory == "FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6"))
            {
                OthSql += " and Name not in ('资金申请报告','基础工程','装修工程','规划验线')";
                //林业工程无设备安装
                if (ProjCategory == "FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6" || ProjCategory == "BD5C451A-025D-45F2-BA9F-CC50DD029301")
                {
                    OthSql += " and Name not in ('设备安装')";
                }
            }
            else if (ProjCategory == "8F8499A3-BAD5-4051-BAB6-290CA5CF9520")
            {
                OthSql += " and Name not in ('基础工程','装修工程','设备安装')";
            }

            doc = IMCBaseInfo.GetMapGuidsByDirectoryGuid(DirectoryGuid, OthSql);
            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                    IMCBaseInfo.InitialProjMileStone(ProjGuid, doc.Tables[0].Rows[i]["Guid"].ToString(), doc.Tables[0].Rows[i]["Mark"].ToString());

                //InitMileStoneDoneStatus(ProjGuid, DoneStatus);
            }

            #endregion
        }

        static void InitMileStoneDoneStatus(string ProjGuid, DataSet doc)
        {
            for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                IMCBaseInfo.InitialProjMileStone(ProjGuid, doc.Tables[0].Rows[i]["Guid"].ToString(), doc.Tables[0].Rows[i]["Mark"].ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <param name="NationType">审批类型</param>
        /// <param name="ProjCategory">项目类型</param>
        /// <param name="CityInvest">省级投资</param>
        /// <param name="CenterInvest">中央投资</param>
        ///<param name="DoneStatus"></param>
        static void ProjCheckRecordMileStone(string ProjGuid, string NationType, string ProjCategory, bool CityInvest, bool CenterInvest,DataSet DoneStatus)
        {
            //核准 A1816C6C-4433-4123-B7A4-EA166D2649A0   备案 F8913E85-1F23-4E9B-BF6E-594B9E070CA5
            string DirectoryGuid = "";
            DataSet doc = null;
            string OthSql = "";
            if (NationType == "A1816C6C-4433-4123-B7A4-EA166D2649A0")
                OthSql += " and Name not in ('项目备案申请')";
            if (NationType == "F8913E85-1F23-4E9B-BF6E-594B9E070CA5")
                OthSql += " and Name not in ('项目核准申请报告')";

            if (CityInvest && (!CenterInvest))
                OthSql += " and Name not in ('资金申请报告')";
            else if (CenterInvest && (!CityInvest))
                OthSql += " and Name not in ('申请省级资金文件')";
            else if (CenterInvest && CityInvest)
                OthSql += "";
            else
                OthSql += " and Name not in ('资金申请报告','申请省级资金文件')";

            #region  核准备案类非设备采购信息化

            if ((ProjCategory != "190749D5-0526-46FD-96FC-C96281AE926D") && (ProjCategory != "77A6EB3E-D90F-4372-B549-FFD36238B1EA"))
                DirectoryGuid = "C607D211-DFDB-4AAB-8351-16C1FE4BD17C";

            #endregion

            #region  核准备案类设备采购信息化

            if ((ProjCategory == "190749D5-0526-46FD-96FC-C96281AE926D") || (ProjCategory == "77A6EB3E-D90F-4372-B549-FFD36238B1EA"))
            {
                DirectoryGuid = "F5820CBC-8011-4643-A668-F8DF7083BB4F";

                if (ProjCategory == "190749D5-0526-46FD-96FC-C96281AE926D")
                    OthSql += " and Name not in ('软件开发')";

                if (ProjCategory == "77A6EB3E-D90F-4372-B549-FFD36238B1EA")
                    OthSql += " and Name not in ('设备安装')";
            }

            //林业工程(FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6)、城市园林(BD5C451A-025D-45F2-BA9F-CC50DD029301)、水利工程(CDC1A7C3-23BA-4DC3-9B85-D046787441B6)
            if ((ProjCategory == "BD5C451A-025D-45F2-BA9F-CC50DD029301") || (ProjCategory == "CDC1A7C3-23BA-4DC3-9B85-D046787441B6") || (ProjCategory == "FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6"))
            {
                OthSql += " and Name not in ('资金申请报告','基础工程','装修工程','规划验线')";
                //林业工程无设备安装
                if (ProjCategory == "FE0E9C66-62D6-4109-9EB3-5167DB3EDBC6" || ProjCategory == "BD5C451A-025D-45F2-BA9F-CC50DD029301")
                {
                    OthSql += " and Name not in ('设备安装')";
                }
            }
            else if (ProjCategory == "8F8499A3-BAD5-4051-BAB6-290CA5CF9520")
            {
                OthSql += " and Name not in ('基础工程','装修工程','设备安装')";
            }

            doc = IMCBaseInfo.GetMapGuidsByDirectoryGuid(DirectoryGuid, OthSql);

            if (doc != null && doc.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < doc.Tables[0].Rows.Count; i++)
                    IMCBaseInfo.InitialProjMileStone(ProjGuid, doc.Tables[0].Rows[i][0].ToString(), doc.Tables[0].Rows[i]["Mark"].ToString());

                //InitMileStoneDoneStatus(ProjGuid, DoneStatus);
            }

            #endregion
        }

        /// <summary>
        /// 获取项目的里程碑信息
        /// </summary>
        /// <param name="ProjGuid"></param>
        /// <returns></returns>
        public static DataSet GetProjMileStone(string ProjGuid)
        {
            return IMCBaseInfo.GetProjMileStone(ProjGuid);
        }

        /// <summary>
        /// 设置项目里程碑的在办时间、办结时间
        /// </summary>
        /// <param name="MileStoneGuid">里程碑主键</param>
        /// <param name="Column">用于区分是在办时间还是办结时间</param>
        public static void InitialMileStoneDate(string MileStoneGuid, string Column)
        {
            IMCBaseInfo.InitialMileStoneDate(MileStoneGuid, Column);
        }
        /// <summary>
        /// 设置项目里程碑的开工时间、竣工时间
        /// </summary>
        /// <param name="MileStoneGuid">里程碑主键</param>
        /// <param name="Column">用于区分是在办时间还是办结时间</param>
        /// <param name="WorkTime">开工竣工时间</param>
        public static void InitialMileStoneDate(string MileStoneGuid, string Column,string WorkTime)
        {
            IMCBaseInfo.InitialMileStoneDate(MileStoneGuid, Column, WorkTime);
        }


        /// <summary>
        /// 获取Busi_ApprovalConfig的所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAppConfigData()
        {
            return IMCBaseInfo.GetAppConfigData();
        }

        /// <summary>
        /// 根据Busi_ApprovalConfig的主键获取Name
        /// </summary>
        /// <param name="AppGuid"></param>
        /// <returns></returns>
        public static string GetApprovalConfigNameByGuid(string AppGuid)
        {
            return IMCBaseInfo.GetAppConfigNameByGuid(AppGuid);
        }

        /// <summary>
        /// 通过项目主键获取项目的TopGuid
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static string GetProjTopGuid(string ProjGuid)
        {
            return IMCBaseInfo.GetProjTopGuid(ProjGuid);
        }

        /// <summary>
        /// 通过项目主键获取项目的项目主从关系
        /// </summary>
        /// <param name="ProjGuid">项目主键</param>
        /// <returns></returns>
        public static string GetProjAffiliationByGuid(string ProjGuid)
        {
            return IMCBaseInfo.GetProjAffiliationByGuid(ProjGuid);
        }


        /// <summary>
        /// 初始化年下拉框
        /// </summary>
        /// <param name="ddl">控件名称</param>
        /// <param name="HasEmpty">是否添加第一个为空的选项</param>
        public static void InitDropDownControl(DropDownList ddl, bool HasEmpty)
        {
            int NowYear = DateTime.Now.Year;
            if (HasEmpty)
                ddl.Items.Add(new ListItem("", ""));
            for (int i = NowYear; i > (NowYear - 10); i--)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        /// <summary>
        /// 通过预算下达文号找到下达的项目
        /// </summary>
        /// <param name="FileNo"></param>
        /// <returns></returns>
        public static DataSet GetProjNameByFileNo(string FileNo)
        {
            return IMCBaseInfo.GetProjNameByFileNo(FileNo);
        }
    }
}
