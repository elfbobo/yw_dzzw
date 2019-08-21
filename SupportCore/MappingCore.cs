using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;

namespace Yawei.SupportCore
{
    public class MappingCore
    {
        public static string Json = string.Empty;
        static Database db = DatabaseFactory.CreateDatabase();
        public static string GetTreeJson()
        {
            Json = "";
            db = DatabaseFactory.CreateDatabase();
            DataSet MappingDirectory = db.ExecuteDataSet("select * from Sys_Mapping");
            if (MappingDirectory.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < MappingDirectory.Tables[0].Rows.Count; i++)
                {
                    if (MappingDirectory.Tables[0].Rows[i]["DirectoryGuid"].ToString() == "")
                    {
                        Json += "{id:'" + MappingDirectory.Tables[0].Rows[i]["Guid"] + "',name:'" + MappingDirectory.Tables[0].Rows[i]["Name"] + "',pid:'19ce7196-04ef-4f6a-893e-2981127d9bb5'},";
                    }
                    else
                    {
                        Json += "{id:'" + MappingDirectory.Tables[0].Rows[i]["Guid"] + "',name:'" + MappingDirectory.Tables[0].Rows[i]["Name"] + "',pid:'" + MappingDirectory.Tables[0].Rows[i]["DirectoryGuid"] + "'},";
                    }
                }
            }
            Json += "{id:'19ce7196-04ef-4f6a-893e-2981127d9bb5',name:'映射目录'}";
            Json = "[" + Json + "]";
            return Json;
        }

        public static string GetNameRoute(string TreeGuid)
        {
            Json = "";
            db = DatabaseFactory.CreateDatabase();
            DataSet MappingData = db.ExecuteDataSet("select * from Sys_Mapping");
            if (TreeGuid != "")
            {
                if (TreeGuid == "19ce7196-04ef-4f6a-893e-2981127d9bb5")
                {
                    return "0";
                }
                else
                {
                    if (MappingData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < MappingData.Tables[0].Select("DirectoryGuid='" + TreeGuid + "'").Length; i++)
                        {
                            Json += MappingData.Tables[0].Select("DirectoryGuid='" + TreeGuid + "'")[i]["Name"] + "," + MappingData.Tables[0].Select("DirectoryGuid='" + TreeGuid + "'")[i]["Substance"] + ";";
                        }
                        Json = Json.TrimEnd(';');
                    }
                    else
                    {
                        Json = "";
                    }
                    return Json;
                }
            }
            else
            {
                return "0";
            }
        }

        public static int TreeSave(string aimId, string aimPid, string aimName, string Status)
        {
            db = DatabaseFactory.CreateDatabase();
            if (Status == "")
            {
                int count = Convert.ToInt32(db.ExecuteScalar("select count(*) from Sys_Mapping where Name='" + aimName + "' and DirectoryGuid='" + aimPid + "'"));
                if (count == 0)
                {
                    db.ExecuteNonQuery("update Sys_Mapping set Name='" + aimName + "' where Guid='" + aimId + "'");
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else if (Status == "create")
            {
                int count = Convert.ToInt32(db.ExecuteScalar("select count(*) from Sys_Mapping where Name='" + aimName + "' and DirectoryGuid='" + aimPid + "'"));
                if (count == 0)
                {
                    db.ExecuteNonQuery("insert into Sys_Mapping(Guid,Name,Substance,DirectoryGuid,Mark) values('" + Guid.NewGuid() + "','" + aimName + "','','" + aimPid + "','0')");
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 0;
            }
        }

        public static int NewData(string Name, string aimId, string aimPid, string newCode, string newSubstance)
        {
            db = DatabaseFactory.CreateDatabase();
            DataSet MappingData = db.ExecuteDataSet("select * from Sys_Mapping");
            if (MappingData.Tables[0].Select("DirectoryGuid='" + aimId + "'").Length > 0)
            {
                if (newCode != "" && newSubstance != "")
                {
                    newCode = newCode.TrimEnd(',');
                    newSubstance = newSubstance.TrimEnd(',');
                    db.ExecuteNonQuery("delete Sys_Mapping where DirectoryGuid='" + aimId + "'");
                    string Sql = string.Empty;
                    for (int i = 0; i < newCode.Split(',').Length; i++)
                    {
                        Sql += "insert into Sys_Mapping(Guid,DirectoryGuid,Name,Substance,Mark) values('" + Guid.NewGuid() + "','" + aimId + "','" + newCode.Split(',')[i] + "','" + newSubstance.Split(',')[i] + "','0');";
                    }
                    db.ExecuteNonQuery(Sql);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                string Sql = string.Empty;
                for (int i = 0; i < newCode.TrimEnd(',').Split(',').Length; i++)
                {
                    Sql += "insert into Sys_Mapping(Guid,DirectoryGuid,Name,Substance,Mark) values('" + Guid.NewGuid() + "','" + aimId + "','" + newCode.Split(',')[i] + "','" + newSubstance.Split(',')[i] + "','0');";
                }
                db.ExecuteNonQuery(Sql);
                return 1;
            }
        }

        public static int TreeDel(string TreeGuid)
        {
            int result = 0;
            if (TreeGuid != "")
            {
                string Sql = string.Empty;
                Sql += "delete from Sys_Mapping where Guid='" + TreeGuid + "';";
                result = db.ExecuteNonQuery(Sql);
            }
            return result;
        }

    }
}
