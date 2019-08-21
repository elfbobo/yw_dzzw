//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

namespace Yawei.DataAccess.Common
{
    /// <summary>
    /// 表示更新数据库时,提供数据适配器的更新命令时遇到一个错误时的处理方式的枚举。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public enum UpdateBehavior
    {
        /// <summary>
        /// 表示当数据适配器在执行更新命令时,如果遇到一个错误,更新停止,DataTable中的其他行无效。
        /// </summary>
        Standard,
        /// <summary>
        /// 表示如果数据适配器在执行更新命令时,如果遇到一个错误,更新将继续进行,更新命令将尝试更新剩余的行。
        /// </summary>
        Continue,
        /// <summary>
        /// 表示如果数据适配器在执行更新命令时,如果遇到一个错误,都将被回滚更新排。
        /// </summary>
        Transactional
    }
}
