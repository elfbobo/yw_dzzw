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
    /// 预算明细
    /// </summary>
    public class BudgetDetailCore
    {
       
        Database db = DatabaseFactory.CreateDatabase();//获取数据库连接
      
       

        /// 根据项目Guid获取预算信息
        /// </summary>
        /// <param name="projGuid">项目主键</param>
        /// <returns></returns>
        public DataSet getData(String projGuid)
        {
            DataSet EDDS = db.ExecuteDataSet("select * from Busi_Con_BuggetDetails where ProjGuid = '" + projGuid + "'");//查询预算明细
            return EDDS;
        }
    }
}


