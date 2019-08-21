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
    /// 表示用户登录验证结果。
    /// </summary>
    public enum LoginResult
    {
        /// <summary>
        /// 成功登陆。
        /// </summary>
        SUCCESS = 0,

        /// <summary>
        /// 用户不存在。
        /// </summary>
        DOES_NOT_EXIST,

        /// <summary>
        /// 用户帐号被禁用。
        /// </summary>
        ACCOUNT_INACTIVE,

        /// <summary>
        /// 用户密码不正确。
        /// </summary>
        PASSWORD_INCORRECT

    }
}
