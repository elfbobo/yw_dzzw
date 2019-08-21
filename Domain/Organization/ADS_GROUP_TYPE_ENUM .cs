//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// 域访问控制模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

namespace Yawei.Domain.Organization
{
    /// <summary>
    /// 表示用户属性定义标记。
    /// </summary>
    public enum ADS_GROUP_TYPE_ENUM:ulong
    {
        /// <summary>
        /// 表示全局组。
        /// </summary>
        ADS_GROUP_TYPE_GLOBAL_GROUP = 0x00000002,

        /// <summary>
        /// 表示域本地组。
        /// </summary>
        ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP = 0x00000004,

        /// <summary>
        /// 表示本地组。
        /// </summary>
        ADS_GROUP_TYPE_LOCAL_GROUP = 0x00000004,

        /// <summary>
        /// 表示通用组。
        /// </summary>
        ADS_GROUP_TYPE_UNIVERSAL_GROUP = 0x00000008,

        /// <summary>
        /// 表示安全组。
        /// </summary>
        ADS_GROUP_TYPE_SECURITY_ENABLED = 0x80000000
    }
}
