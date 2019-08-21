//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// ADO.NET 数据访问模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System.Configuration;
using System.Xml;

namespace Yawei.DataAccess.Configuration
{
    /// <summary>
    /// 表示为定义默认数据库连接的配置文件自定义节点的实现类。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public class DefaultDatabaseConfiguration : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            return section.Attributes["defaultDatabase"].Value;
        }
    }
}
