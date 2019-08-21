//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// 域访问控制模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

namespace Yawei.Domain
{
    /// <summary>
    /// 表示对域节点或对象属性操作的结果。
    /// </summary>
    public enum PropertyResult
    {
        /// <summary>
        /// 操作成功。
        /// </summary>
        SUCCESS,

        /// <summary>
        /// 不可写。
        /// </summary>
        CAN_NOT_WRITE,

        /// <summary>
        /// 错误的属性值。
        /// </summary>
        ERROR_PROPERTY_VALUE,

        /// <summary>
        /// 没有属性值。
        /// </summary>
        NO_PROPERTY_VALUE,

        /// <summary>
        /// 找不到属性。
        /// </summary>
        PROPERTY_NOT_FOUND,

        /// <summary>
        /// 索引超出界限。
        /// </summary>
        INDEX_OUT_BROUNDS,

        /// <summary>
        /// 设置失败。
        /// </summary>
        FAILURE
    }
}
