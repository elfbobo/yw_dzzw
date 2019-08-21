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
    public enum ADS_USER_FLAG_ENUM
    {
        /// <summary>
        /// 登录脚本标志。
        /// </summary>
        ADS_UF_SCRIPT = 0X0001,

        /// <summary>
        /// 表示用户帐号禁用标志。
        /// </summary>
        ADS_UF_ACCOUNTDISABLE = 0X0002,

        /// <summary>
        /// 表示主文件夹标志。
        /// </summary>
        ADS_UF_HOMEDIR_REQUIRED = 0X0008,

        /// <summary>
        /// 表示过期标志。
        /// </summary>
        ADS_UF_LOCKOUT = 0X0010,

        /// <summary>
        /// 表示用户密码不是必须的。
        /// </summary>
        ADS_UF_PASSWD_NOTREQD = 0X0020,

        /// <summary>
        /// 表示密码不能更改标志。
        /// </summary>
        ADS_UF_PASSWD_CANT_CHANGE = 0X0040,

        /// <summary>
        /// 表示使用可逆的加密保存密码标志。
        /// </summary>
        ADS_UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0X0080,

        /// <summary>
        /// 表示本地帐号标志。
        /// </summary>
        ADS_UF_TEMP_DUPLICATE_ACCOUNT = 0X0100,

        /// <summary>
        /// 表示普通用户的默认帐号类型。
        /// </summary>
        ADS_UF_NORMAL_ACCOUNT = 0X0200,

        /// <summary>
        /// 表示跨域的信任帐号标志。
        /// </summary>
        ADS_UF_INTERDOMAIN_TRUST_ACCOUNT = 0X0800,

        /// <summary>
        /// 表示工作站信任帐号标志。
        /// </summary>
        ADS_UF_WORKSTATION_TRUST_ACCOUNT = 0x1000,

        /// <summary>
        /// 表示服务器信任帐号标志。
        /// </summary>
        ADS_UF_SERVER_TRUST_ACCOUNT = 0X2000,

        /// <summary>
        /// 表示密码永不过期标志。
        /// </summary>
        ADS_UF_DONT_EXPIRE_PASSWD = 0X10000,

        /// <summary>
        /// 表示MNS 帐号标志。
        /// </summary>
        ADS_UF_MNS_LOGON_ACCOUNT = 0X20000,

        /// <summary>
        /// 表示交互式登录必须使用智能卡。
        /// </summary>
        ADS_UF_SMARTCARD_REQUIRED = 0X40000,

        /// <summary>
        /// 表示服务帐号（用户或计算机帐号）将通过 Kerberos 委托信任。
        /// </summary>
        ADS_UF_TRUSTED_FOR_DELEGATION = 0X80000,

        /// <summary>
        /// 表示即使服务帐号是通过 Kerberos 委托信任的，敏感帐号不能被委托。
        /// </summary>
        ADS_UF_NOT_DELEGATED = 0X100000,

        /// <summary>
        /// 表示此帐号需要 DES 加密类型。
        /// </summary>
        ADS_UF_USE_DES_KEY_ONLY = 0X200000,

        /// <summary>
        /// 表示不需要进行 Kerberos 预身份验证。
        /// </summary>
        ADS_UF_DONT_REQUIRE_PREAUTH = 0X4000000,

        /// <summary>
        /// 表示用户密码过期标志。
        /// </summary>
        ADS_UF_PASSWORD_EXPIRED = 0X800000,

        /// <summary>
        /// 表示用户帐号可委托标志。
        /// </summary>
        ADS_UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0X1000000
    }
}
