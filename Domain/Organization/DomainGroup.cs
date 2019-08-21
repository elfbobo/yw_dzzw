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
    /// <summary>
    /// 表示域工作组的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public class DomainGroup : DomainObject
    {
        #region 变量

        // 只读属性名称字符串。
        private const string _readPropertyName = "name;cn;distinguishedName;objectCategory;objectClass;objectGUID;memberOf;sAMAccountType;whenChanged;whenCreated";

        // 可写属性名称字符串。
        private const string _writePropertyName = "description;info;mail;managedBy;sAMAccountName;member;groupType";

        // 单值属性名称字符串。
        private const string _singleValuePropertyName = "description;info;mail;managedBy;sAMAccountName;name;cn;distinguishedName;objectCategory;objectGUID;sAMAccountType;whenChanged;whenCreated;groupType";

        // 多值属性名称字符串。
        private const string _multipleValuePropertyName = "member;objectClass;memberOf";

        #endregion

        #region 读写属性

        /// <summary>
        /// 获取或设置工作组的描述,对应属性名称：description。
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
        /// 获取或设置工作组的注释,对应属性名称：info。
        /// </summary>
        public string Info
        {
            get { return GetPropertyValue("info") != null ? GetPropertyValue("info").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("info", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置工作组的邮件,对应属性名称：mail。
        /// </summary>
        public string Mail
        {
            get { return GetPropertyValue("mail") != null ? GetPropertyValue("mail").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("mail", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置工作组的管理者,对应属性名称：managedBy。
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

        /// <summary>
        /// 获取或设置工作组的账户名称（Pre-Windows 2000）,对应属性名称：sAMAccountName。
        /// </summary>
        public string SAMAccountName
        {
            get { return GetPropertyValue("sAMAccountName") != null ? GetPropertyValue("sAMAccountName").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("sAMAccountName", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置工作组的成员,对应属性名称：member。
        /// </summary>
        public string[] Member
        {
            get { return GetPropertyValuesToStrings("member"); }
            set
            {
                PropertyResult result = SetPropertyValue("member", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置工作组类型标志。
        /// </summary>
        public string GroupType
        {
            get { return GetPropertyValue("groupType").ToString(); }
            set
            {
                PropertyResult result = SetPropertyValue("groupType", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        #endregion

        #region 只读属性

        /// <summary>
        /// 获取工作组的公共名称,对应属性名称：cn。
        /// </summary>
        public string CN
        {
            get { return GetPropertyValue("cn") != null ? GetPropertyValue("cn").ToString() : null; }
        }

        /// <summary>
        /// 获取工作组的名称,对应属性名称：name。
        /// </summary>
        public string FullName
        {
            get { return GetPropertyValue("name") != null ? GetPropertyValue("name").ToString() : null; }
        }

        /// <summary>
        /// 获取工作组的全名,对应属性名称：distinguishedName。
        /// </summary>
        public string DN
        {
            get { return GetPropertyValue("distinguishedName") != null ? GetPropertyValue("distinguishedName").ToString() : null; }
        }

        /// <summary>
        /// 获取工作组的对象类别,对应属性名称：objectCategory。
        /// </summary>
        public string ObjectCategory
        {
            get { return GetPropertyValue("objectCategory") != null ? GetPropertyValue("objectCategory").ToString() : null; }
        }

        /// <summary>
        /// 获取工作组的对象类别,对应属性名称：objectClass。
        /// </summary>
        public string[] ObjectClass
        {
            get { return GetPropertyValuesToStrings("objectClass"); }
        }

        /// <summary>
        /// 获取工作组的对象唯一标示,对应属性名称：objectGUID。
        /// </summary>
        public byte[] ObjectGUID
        {
            get { return GetPropertyValue("objectGUID") != null ? (byte[])GetPropertyValue("objectGUID") : null; }
        }

        /// <summary>
        /// 获取工作组的隶属于组DN,对应属性名称：memberOf。
        /// </summary>
        public string[] MemberOf
        {
            get { return GetPropertyValuesToStrings("memberOf"); }
        }

        /// <summary>
        /// 获取工作组的类型编号,对应属性名称：sAMAccountType。
        /// </summary>
        public string SAMAccountType
        {
            get { return GetPropertyValue("sAMAccountType") != null ? GetPropertyValue("sAMAccountType").ToString() : null; }
        }

        /// <summary>
        /// 获取工作组的最后一次更改时间,对应属性名称：whenChanged。
        /// </summary>
        public DateTime WhenChanged
        {
            get { return GetPropertyValue("whenChanged") != null ? DateTime.Parse(GetPropertyValue("whenChanged").ToString()) : DateTime.MinValue; }
        }

        /// <summary>
        /// 获取工作组的创建时间,对应属性名称：whenCreated。
        /// </summary>
        public DateTime WhenCreated
        {
            get { return GetPropertyValue("whenCreated") != null ? DateTime.Parse(GetPropertyValue("whenCreated").ToString()) : DateTime.MinValue; }
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化域用户的实例。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        /// <param name="authenticationType">表示对客户端进行身份验证时使用的验证类型。</param>
        public DomainGroup(string path, string username, string password, AuthenticationTypes authenticationType)
            : base(path, username, password, authenticationType)
        {
            if (this.SchemaClassName != "group")
                throw new Exception("不是一个域工作组对象");
            AddReadPropertyRange(_readPropertyName);
            AddWritePropertyRange(_writePropertyName);
            AddSingleValuePropertyRange(_singleValuePropertyName);
            AddMultipleValuePropertyRange(_multipleValuePropertyName);
        }

        /// <summary>
        /// 初始化域用户的实例。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        public DomainGroup(string path, string username, string password)
            : base(path, username, password)
        {
            if (this.SchemaClassName != "group")
                throw new Exception("不是一个域工作组对象");
            AddReadPropertyRange(_readPropertyName);
            AddWritePropertyRange(_writePropertyName);
            AddSingleValuePropertyRange(_singleValuePropertyName);
            AddMultipleValuePropertyRange(_multipleValuePropertyName);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 向工作组中增加一个用户成员。
        /// </summary>
        /// <param name="domainUser">表示要加入工作组的用户成员。</param>
        public void AddDomainUser(DomainUser domainUser)
        {
            Member = new string[1] { domainUser.DN };
        }

        /// <summary>
        /// 从工作组中移除一个用户成员。
        /// </summary>
        /// <param name="domainUser">表示要移除工作组的用户成员。</param>
        public void RemoveDomainUser(DomainUser domainUser)
        {
            RemovePropertyValue("member", domainUser.DN);
        }

        /// <summary>
        /// 向工作组中增加一个联系人成员。
        /// </summary>
        /// <param name="domainContact">表示要加入工作组的联系人成员。</param>
        public void AddDomainContact(DomainContact domainContact)
        {
            Member = new string[1] { domainContact.DN };
        }

        /// <summary>
        /// 从工作组中移除一个联系人成员。
        /// </summary>
        /// <param name="domainContact">表示要移除工作组的联系人成员。</param>
        public void RemoveDomainContact(DomainContact domainContact)
        {
            RemovePropertyValue("member", domainContact.DN);
        }

        /// <summary>
        /// 向工作组中增加一个组成员。
        /// </summary>
        /// <param name="domainGroup">表示要加入工作组的组成员。</param>
        public void AddDomainGroup(DomainGroup domainGroup)
        {
            if (GroupType == ((long)ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_UNIVERSAL_GROUP).ToString())
            {
                Member = new string[1] { domainGroup.DN };
            }
            else
            {
                throw new Exception("只有组类型为通讯组的工作组对象可以添加组成员。");
            }
        }

        /// <summary>
        /// 从工作组中移除一个组成员。
        /// </summary>
        /// <param name="domainGroup">表示要移除工作组的组成员。</param>
        public void RemoveDomainGroup(DomainGroup domainGroup)
        {
            RemovePropertyValue("member", domainGroup.DN);
        }

        /// <summary>
        /// 判断是一个通讯组（只有通讯组可以增加组成员）。
        /// </summary>
        /// <returns>返回是否是一个通讯组。</returns>
        public bool IsUniversalGroup()
        {
            return GroupType == ((long)ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_UNIVERSAL_GROUP).ToString();
        }

        #endregion
    }
}
