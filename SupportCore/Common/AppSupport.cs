using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yawei.Common
{
    public class AppSupport
    {
        //系统名称
        public static string AppTitle = "政府投资项目监管平台";

        //虚拟路径
        public static string AppPath = "";


        public static string adminGuid = "ca8f23a0-6647-659e-a30d-e5d8728bc25a";  //管理员角色
        public static string IdyMageGuid = "f4e3ff87-c8b5-e75d-2917-9c642ba06f9c";//发改委
        public static string dzzwGuid = "e8e40fed-b138-5072-f838-1c8a7ec3aa78";//电政办
        public static string spvGuid = "FDB4DED5-B9D9-4043-9734-CEE4AF122857";//行政监察;
        public static string conStGuid = "AF328B39-375D-4843-B430-3DAA24EA52B1";//建设单位
        public static string zgbm = "7d655209-fe63-9d10-baec-d9ab1d3eef80";//主管部门
        public static string industryMage = "2ce5838b-77e0-35cd-96ea-637350ca4be7";//主管部门
        public static string[] commonRole = { conStGuid,zgbm,"7c67639a-265c-eb01-1316-47b4545cec19" }; //公共权限角色
        public static string viewdata = "5e022811-e156-02b0-7971-52d55727c60f";
        public static string[] editRole = SysCommon.MergerArray(commonRole, new string[] { adminGuid, IdyMageGuid, dzzwGuid, spvGuid });//编辑角色
        public static string[] deleteRole = SysCommon.MergerArray(commonRole, new string[] { adminGuid });//删除角色
        public static string[] saveRole = SysCommon.MergerArray(commonRole, new string[] { adminGuid });//保存角色
        public static string[] confrimRole = new string[] { adminGuid, zgbm };//审核角色
    }
}