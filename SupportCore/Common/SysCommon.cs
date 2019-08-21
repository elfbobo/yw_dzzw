using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yawei.DataAccess;
using Yawei.SSOLib.Cryptography;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Yawei.Common
{
    public class SysCommon
    {


        public static string ConvetUserToJSON(List<CurrentUser> cuList)
        {
            string json = "";
            for (int i = 0; i < cuList.Count; i++)
            {
                if (i > 0)
                    json += ",";
                TimeSpan span = DateTime.Now.Subtract(cuList[i].LoginDate);
                json += "{Guid:'" + cuList[i].UserGuid + "',CN:'" + cuList[i].UserCN + "',DN:'" + cuList[i].UserDN + "',Time:'" + span.Minutes + "分钟'}";

            }
            return json;
        }


        protected static void SetOnlinUser(string guid, string cn, string dn)
        {
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["onlineuser"].ToLower() == "true")
            {
                var currentUser = new CurrentUser();
                currentUser.UserCN = cn;
                currentUser.UserDN = dn;
                currentUser.UserGuid = guid;
                currentUser.LoginDate = DateTime.Now;
                if (System.Web.HttpContext.Current.Application["CurrentUser"] != null)
                {
                    List<CurrentUser> cuList = System.Web.HttpContext.Current.Application["CurrentUser"] as List<CurrentUser>;
                    if (cuList.Count(l => l.UserGuid == currentUser.UserGuid) <= 0)
                    {
                        cuList.Add(currentUser);
                        System.Web.HttpContext.Current.Application["CurrentUser"] = cuList;
                    }
                }
                else
                {
                    var cuList = new List<CurrentUser>();
                    cuList.Add(currentUser);
                    System.Web.HttpContext.Current.Application["CurrentUser"] = cuList;
                }
            }
        }

        public static void IsEnabled()
        {
            string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YAWEISysApi"].ConnectionString;
            string table = conn.Split(';')[1].Split('=')[1];
            Database database = DatabaseFactory.CreateDatabase(conn, "Yawei.DataAccess.SqlClient.SqlDatabase");
            object boj = database.ExecuteScalar("SELECT is_broker_enabled FROM sys.databases WHERE name = '" + table + "'");
            if (boj.ToString().ToLower() == "false")
            {
                database.ExecuteNonQuery(" ALTER DATABASE " + table + " SET NEW_BROKER WITH ROLLBACK IMMEDIATE");
            }
        }

        /// <summary>
        /// DB用户登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool DBLogin(string name, string password)
        {
            bool ret = false;

            string Key = ConfigurationManager.AppSettings["Key"];
            string IV = ConfigurationManager.AppSettings["IV"];
            string mailAddress = name + "@qingdao.gov.cn";

            Encrypter en = new Encrypter(Key, IV);

            string mailPass = en.EncryptString(password);

            string strSQL = "SELECT * FROM Users WHERE mailAddress=@mailAddress AND mailPass=@mailPass";

            Database db = DatabaseFactory.CreateDatabase("DBPerson");

            DbCommand cmd = db.CreateCommand(CommandType.Text, strSQL);
            cmd.Parameters.Add(new SqlParameter("@mailAddress", mailAddress));
            cmd.Parameters.Add(new SqlParameter("@mailPass", mailPass));
            DataSet ds = db.ExecuteDataSet(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ret = true;
                CurrentUser.SetCookieUserInfo(ds.Tables[0].Rows[0]["GUID"].ToString(), ds.Tables[0].Rows[0]["TITLE"].ToString(), ds.Tables[0].Rows[0]["USERPATH"].ToString(), mailAddress);
                SetOnlinUser(ds.Tables[0].Rows[0]["GUID"].ToString(), ds.Tables[0].Rows[0]["TITLE"].ToString(), ds.Tables[0].Rows[0]["USERPATH"].ToString());

            }

            return ret;
        }


        public static string GetXmlSql(string title, string path)
        {
            XmlDocument docment = new XmlDocument();

            docment.Load(path);

            switch (title.ToLower())
            {
                case "login":
                    return docment.SelectSingleNode("option/LoginTable").InnerText;
                case "error":
                    return docment.SelectSingleNode("option/ErrorTable").InnerText;
                case "operator":
                    return docment.SelectSingleNode("option/OperatorTable").InnerText;
                case "tabledetails":
                    return docment.SelectSingleNode("option/TableDetails").InnerText;
            }
            return "";
        }


        public static bool isHasTableDetails()
        {
            Database database = DatabaseFactory.CreateDatabase();
            object count = database.ExecuteScalar("select count(1) from dbo.sysobjects  where name='TableDetails'");
            if (count.ToString() != "0")
            {
                return true;
            }
            return false;
        }

        /// 合并数组
        /// </summary>
        /// <param name="First">第一个数组</param>
        /// <param name="Second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public static string[] MergerArray(string[] First, string[] Second)
        {
            if (Second != null && Second.Length > 0)
            {
                string[] result = new string[First.Length + Second.Length];
                First.CopyTo(result, 0);
                Second.CopyTo(result, First.Length);
                return result;
            }
            else
                return First;
        }
    }
}
