//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// 域访问控制模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System;
using System.DirectoryServices;

namespace Yawei.Domain.Organization
{
    public class DomainOU : DomainObject
    {
        #region 变量

        // 只读属性名称字符串。
        private const string _readPropertyName = "name;ou;distinguishedName;objectCategory;objectClass;objectGUID;whenChanged;whenCreated";

        // 可写属性名称字符串。
        private const string _writePropertyName = "description;co;c;countryCode;st;l;street;postalCode;managedBy";

        // 单值属性名称字符串。
        private const string _singleValuePropertyName = "description;co;c;countryCode;st;l;street;postalCode;managedBy;name;ou;distinguishedName;objectClass;objectGUID;whenChanged;whenCreated";

        // 多值属性名称字符串。
        private const string _multipleValuePropertyName = "objectClass";

        #endregion

        #region 读写属性

        /// <summary>
        /// 获取或设置组织单元的描述,对应属性名称：description。
        /// </summary>
        public string Description
        {
            get { return GetPropertyValue("description") != null ? GetPropertyValue("description").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("description", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的国家名称,对应属性名称：co。
        /// </summary>
        public string Country
        {
            get { return GetPropertyValue("co") != null ? GetPropertyValue("co").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("co", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的国家英文名称,对应属性名称：c。
        /// </summary>
        public string CountryInitials
        {
            get { return GetPropertyValue("c") != null ? GetPropertyValue("c").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("c", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的国家编号,对应属性名称：countryCode。
        /// </summary>
        public int CountryCode
        {
            get { return GetPropertyValue("countryCode") != null ? int.Parse(GetPropertyValue("countryCode").ToString()) : -1; }
            set
            {
                PropertyResult result = SetPropertyValue("countryCode", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的省/自治区,对应属性名称：st。
        /// </summary>
        public string Province
        {
            get { return GetPropertyValue("st") != null ? GetPropertyValue("st").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("st", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的市/县,对应属性名称：l。
        /// </summary>
        public string City
        {
            get { return GetPropertyValue("l") != null ? GetPropertyValue("l").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("l", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的街道,对应属性名称：street。
        /// </summary>
        public string Street
        {
            get { return GetPropertyValue("street") != null ? GetPropertyValue("street").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("street", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的邮政编码,对应属性名称：postalCode。
        /// </summary>
        public string PostalCode
        {
            get { return GetPropertyValue("postalCode") != null ? GetPropertyValue("postalCode").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("postalCode", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置组织单元的管理者,对应属性名称：managedBy。
        /// </summary>
        public string Manager
        {
            get { return GetPropertyValue("managedBy") != null ? GetPropertyValue("managedBy").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("managedBy", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        #endregion

        #region 只读属性

        /// <summary>
        /// 获取组织单元的公共名称,对应属性名称：ou。
        /// </summary>
        public string OU
        {
            get { return GetPropertyValue("ou") != null ? GetPropertyValue("ou").ToString() : null; }
        }

        /// <summary>
        /// 获取组织单元的姓名,对应属性名称：name。
        /// </summary>
        public string FullName
        {
            get { return GetPropertyValue("name") != null ? GetPropertyValue("name").ToString() : null; }
        }

        /// <summary>
        /// 获取组织单元的全名,对应属性名称：distinguishedName。
        /// </summary>
        public string DN
        {
            get { return GetPropertyValue("distinguishedName") != null ? GetPropertyValue("distinguishedName").ToString() : null; }
        }

        /// <summary>
        /// 获取组织单元的对象类别,对应属性名称：objectCategory。
        /// </summary>
        public string ObjectCategory
        {
            get { return GetPropertyValue("objectCategory") != null ? GetPropertyValue("objectCategory").ToString() : null; }
        }

        /// <summary>
        /// 获取组织单元的对象类别,对应属性名称：objectClass。
        /// </summary>
        public string[] ObjectClass
        {
            get { return GetPropertyValuesToStrings("objectClass"); }
        }

        /// <summary>
        /// 获取组织单元的对象唯一标示,对应属性名称：objectGUID。
        /// </summary>
        public byte[] ObjectGUID
        {
            get { return GetPropertyValue("objectGUID") != null ? (byte[])GetPropertyValue("objectGUID") : null; }
        }

        /// <summary>
        /// 获取组织单元的最后一次更改时间,对应属性名称：whenChanged。
        /// </summary>
        public DateTime WhenChanged
        {
            get { return GetPropertyValue("whenChanged") != null ? DateTime.Parse(GetPropertyValue("whenChanged").ToString()) : DateTime.MinValue; }
        }

        /// <summary>
        /// 获取组织单元的创建时间,对应属性名称：whenCreated。
        /// </summary>
        public DateTime WhenCreated
        {
            get { return GetPropertyValue("whenCreated") != null ? DateTime.Parse(GetPropertyValue("whenCreated").ToString()) : DateTime.MinValue; }
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化组织单元的实例。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        /// <param name="authenticationType">表示对客户端进行身份验证时使用的验证类型。</param>
        public DomainOU(string path, string username, string password, AuthenticationTypes authenticationType)
            : base(path, username, password, authenticationType)
        {
            if (this.SchemaClassName != "organizationalUnit")
                throw new Exception("不是一个组织单元对象");
            AddReadPropertyRange(_readPropertyName);
            AddWritePropertyRange(_writePropertyName);
            AddSingleValuePropertyRange(_singleValuePropertyName);
            AddMultipleValuePropertyRange(_multipleValuePropertyName);
        }

        /// <summary>
        /// 初始化组织单元的实例。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        public DomainOU(string path, string username, string password)
            : base(path, username, password)
        {
            if (this.SchemaClassName != "organizationalUnit")
                throw new Exception("不是一个组织单元对象");
            AddReadPropertyRange(_readPropertyName);
            AddWritePropertyRange(_writePropertyName);
            AddSingleValuePropertyRange(_singleValuePropertyName);
            AddMultipleValuePropertyRange(_multipleValuePropertyName);
        }

        #endregion
    }
}
