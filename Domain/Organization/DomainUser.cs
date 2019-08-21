﻿//===============================================================================
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
    /// 表示域用户的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public class DomainUser : DomainObject
    {
        #region 变量

        // 只读属性名称字符串。
        private const string _readPropertyName = "name;cn;directReports;distinguishedName;objectCategory;objectClass;objectGUID;managedObjects;memberOf;sAMAccountType;whenChanged;whenCreated";

        // 可写属性名称字符串。
        private const string _writePropertyName = "sn;givenName;initials;displayName;description;physicalDeliveryOfficeName;telephoneNumber;otherTelephone;mail;wWWHomePage;url;c;co;countryCode;st;l;streetAddress;postOfficeBox;postalCode;sAMAccountName;userPrincipalName;logonHours;userWorkstations;homePhone;otherHomePhone;pager;otherPager;mobile;otherMobile;facsimileTelephoneNumber;otherFacsimileTelephoneNumber;ipPhone;otherIpPhone;info;title;department;company;manager";

        // 单值属性名称字符串。
        private const string _singleValuePropertyName = "name;sn;givenName;initials;displayName;description;physicalDeliveryOfficeName;telephoneNumber;mail;wWWHomePage;c;co;countryCode;st;l;streetAddress;postOfficeBox;postalCode;sAMAccountName;userPrincipalName;logonHours;userWorkstations;homePhone;pager;mobile;facsimileTelephoneNumber;ipPhone;info;title;department;company;manager;cn;distinguishedName;objectCategory;objectGUID;sAMAccountType;whenChanged;whenCreated";

        // 多值属性名称字符串。
        private const string _multipleValuePropertyName = "otherTelephone;url;otherHomePhone;otherPager;otherMobile;otherFacsimileTelephoneNumber;otherIpPhone;directReports;objectClass;managedObjects;memberOf";

        #endregion

        #region 读写属性

        /// <summary>
        /// 获取或设置用户的姓,对应属性名称：sn。
        /// </summary>
        public string LastName
        {
            get { return GetPropertyValue("sn") != null ? GetPropertyValue("sn").ToString() : null; }
            set 
            {
                PropertyResult result = SetPropertyValue("sn", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的名,对应属性名称：givenName。
        /// </summary>
        public string FirstName 
        {
            get { return GetPropertyValue("givenName") != null ? GetPropertyValue("givenName").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("givenName", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的英文缩写,对应属性名称：initials。
        /// </summary>
        public string Initials
        {
            get { return GetPropertyValue("initials") != null ? GetPropertyValue("initials").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("initials", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的显示名称,对应属性名称：displayName。
        /// </summary>
        public string DisplayName
        {
            get { return GetPropertyValue("displayName") != null ? GetPropertyValue("displayName").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("displayName", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的描述,对应属性名称：description。
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
        /// 获取或设置用户的办公室,对应属性名称：physicalDeliveryOfficeName。
        /// </summary>
        public string Office
        {
            get { return GetPropertyValue("physicalDeliveryOfficeName") != null ? GetPropertyValue("physicalDeliveryOfficeName").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("physicalDeliveryOfficeName", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的电话号码,对应属性名称：telephoneNumber。
        /// </summary>
        public string Telephone
        {
            get { return GetPropertyValue("telephoneNumber") != null ? GetPropertyValue("telephoneNumber").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("telephoneNumber", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的其他电话号码,对应属性名称：otherTelephone。
        /// </summary>
        public string[] OtherTelephone
        {
            get { return GetPropertyValuesToStrings("otherTelephone"); }
            set
            {
                PropertyResult result = SetPropertyValue("otherTelephone", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的电子邮件,对应属性名称：mail。
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
        /// 获取或设置用户的主页,对应属性名称：wWWHomePage。
        /// </summary>
        public string WebPage
        {
            get { return GetPropertyValue("wWWHomePage") != null ? GetPropertyValue("wWWHomePage").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("wWWHomePage", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的其他的网址,对应属性名称：url。
        /// </summary>
        public string[] OtherWebPage
        {
            get { return GetPropertyValuesToStrings("url"); }
            set
            {
                PropertyResult result = SetPropertyValue("url", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的国家名称,对应属性名称：co。
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
        /// 获取或设置用户的国家英文名称,对应属性名称：c。
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
        /// 获取或设置用户的国家编号,对应属性名称：countryCode。
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
        /// 获取或设置用户的省/自治区,对应属性名称：st。
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
        /// 获取或设置用户的市/县,对应属性名称：l。
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
        /// 获取或设置用户的街道,对应属性名称：streetAddress。
        /// </summary>
        public string Street
        {
            get { return GetPropertyValue("streetAddress") != null ? GetPropertyValue("streetAddress").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("streetAddress", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的邮政信箱,对应属性名称：postOfficeBox。
        /// </summary>
        public string PostOfficeBox
        {
            get { return GetPropertyValue("postOfficeBox") != null ? GetPropertyValue("postOfficeBox").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("postOfficeBox", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的邮政编码,对应属性名称：postalCode。
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
        /// 获取或设置用户的账户名称（Pre-Windows 2000）,对应属性名称：sAMAccountName。
        /// </summary>
        public string LoginName
        {
            get { return GetPropertyValue("sAMAccountName") != null ? GetPropertyValue("sAMAccountName").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("sAMAccountName", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的账户名称,对应属性名称：userPrincipalName。
        /// </summary>
        public string PrincipalName
        {
            get { return GetPropertyValue("userPrincipalName") != null ? GetPropertyValue("userPrincipalName").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("userPrincipalName", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的账户登录时间规则,对应属性名称：logonHours。
        /// </summary>
        public byte[] LogonHours
        {
            get { return GetPropertyValue("logonHours") != null ? (byte[])GetPropertyValue("logonHours") : null; }
            set
            {
                PropertyResult result = SetPropertyValue("logonHours", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的账户登录到计算机的名称,对应属性名称：userWorkstations。
        /// </summary>
        public string[] LogonOn
        {
            get { return GetPropertyValue("userWorkstations") != null ? GetPropertyValue("userWorkstations").ToString().Split(',') : null; }
            set
            {
                PropertyResult result = SetPropertyValue("userWorkstations", string.Join(",", value));
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的家庭电话,对应属性名称：homePhone。
        /// </summary>
        public string HomePhone
        {
            get { return GetPropertyValue("homePhone") != null ? GetPropertyValue("homePhone").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("homePhone", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的其他的家庭电话,对应属性名称：otherHomePhone。
        /// </summary>
        public string[] OtherHomePhone
        {
            get { return GetPropertyValuesToStrings("otherHomePhone"); }
            set
            {
                PropertyResult result = SetPropertyValue("otherHomePhone", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的寻呼机,对应属性名称：pager。
        /// </summary>
        public string Pager
        {
            get { return GetPropertyValue("pager") != null ? GetPropertyValue("pager").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("pager", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的其他的寻呼机,对应属性名称：otherPager。
        /// </summary>
        public string[] OtherPager
        {
            get { return GetPropertyValuesToStrings("otherPager"); }
            set
            {
                PropertyResult result = SetPropertyValue("otherPager", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的移动电话,对应属性名称：mobile。
        /// </summary>
        public string Mobile
        {
            get { return GetPropertyValue("mobile") != null ? GetPropertyValue("mobile").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("mobile", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的其他的移动电话,对应属性名称：otherMobile。
        /// </summary>
        public string[] OtherMobile
        {
            get { return GetPropertyValuesToStrings("otherMobile"); }
            set
            {
                PropertyResult result = SetPropertyValue("otherMobile", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的传真,对应属性名称：facsimileTelephoneNumber。
        /// </summary>
        public string Fax
        {
            get { return GetPropertyValue("facsimileTelephoneNumber") != null ? GetPropertyValue("facsimileTelephoneNumber").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("facsimileTelephoneNumber", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的其他的传真,对应属性名称：otherFacsimileTelephoneNumber。
        /// </summary>
        public string[] OtherFax
        {
            get { return GetPropertyValuesToStrings("otherFacsimileTelephoneNumber"); }
            set
            {
                PropertyResult result = SetPropertyValue("otherFacsimileTelephoneNumber", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的IP电话,对应属性名称：ipPhone。
        /// </summary>
        public string IpPhone
        {
            get { return GetPropertyValue("ipPhone") != null ? GetPropertyValue("ipPhone").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("ipPhone", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的其他的IP电话,对应属性名称：otherIpPhone。
        /// </summary>
        public string[] OtherIpPhone
        {
            get { return GetPropertyValuesToStrings("otherIpPhone"); }
            set
            {
                PropertyResult result = SetPropertyValue("otherIpPhone", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的电话注释,对应属性名称：info。
        /// </summary>
        public string Notes
        {
            get { return GetPropertyValue("info") != null ? GetPropertyValue("info").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("info", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的职务,对应属性名称：title。
        /// </summary>
        public string Title
        {
            get { return GetPropertyValue("title") != null ? GetPropertyValue("title").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("title", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的部门,对应属性名称：department。
        /// </summary>
        public string Department
        {
            get { return GetPropertyValue("department") != null ? GetPropertyValue("department").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("department", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的公司,对应属性名称：company。
        /// </summary>
        public string Company
        {
            get { return GetPropertyValue("company") != null ? GetPropertyValue("company").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("company", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        /// <summary>
        /// 获取或设置用户的经理人DN,对应属性名称：manager。
        /// </summary>
        public string Manager
        {
            get { return GetPropertyValue("manager") != null ? GetPropertyValue("manager").ToString() : null; }
            set
            {
                PropertyResult result = SetPropertyValue("manager", value);
                if (result != PropertyResult.SUCCESS) throw new Exception(result.ToString());
            }
        }

        #endregion

        #region 只读属性

        /// <summary>
        /// 获取用户的公共名称,对应属性名称：cn。
        /// </summary>
        public string CN
        {
            get { return GetPropertyValue("cn") != null ? GetPropertyValue("cn").ToString() : null; }
        }

        /// <summary>
        /// 获取用户的姓名,对应属性名称：name。
        /// </summary>
        public string FullName
        {
            get { return GetPropertyValue("name") != null ? GetPropertyValue("name").ToString() : null; }
        }

        /// <summary>
        /// 获取用户的直接下属,对应属性名称：directReports。
        /// </summary>
        public string[] DirectReports
        {
            get { return GetPropertyValuesToStrings("directReports"); }
        }

        /// <summary>
        /// 获取用户的全名,对应属性名称：distinguishedName。
        /// </summary>
        public string DN
        {
            get { return GetPropertyValue("distinguishedName") != null ? GetPropertyValue("distinguishedName").ToString() : null; }
        }

        /// <summary>
        /// 获取用户的对象类别,对应属性名称：objectCategory。
        /// </summary>
        public string ObjectCategory
        {
            get { return GetPropertyValue("objectCategory") != null ? GetPropertyValue("objectCategory").ToString() : null; }
        }

        /// <summary>
        /// 获取用户的对象类别,对应属性名称：objectClass。
        /// </summary>
        public string[] ObjectClass
        {
            get { return GetPropertyValuesToStrings("objectClass"); }
        }

        /// <summary>
        /// 获取用户的对象唯一标示,对应属性名称：objectGUID。
        /// </summary>
        public byte[] ObjectGUID
        {
            get { return GetPropertyValue("objectGUID") != null ? (byte[])GetPropertyValue("objectGUID") : null; }
        }

        /// <summary>
        /// 获取用户的管理对象DN,对应属性名称：managedObjects。
        /// </summary>
        public string[] ManagedObjects
        {
            get { return GetPropertyValuesToStrings("managedObjects"); }
        }

        /// <summary>
        /// 获取用户的隶属于组DN,对应属性名称：memberOf。
        /// </summary>
        public string[] MemberOf
        {
            get { return GetPropertyValuesToStrings("memberOf"); }
        }

        /// <summary>
        /// 获取用户的类型编号,对应属性名称：sAMAccountType。
        /// </summary>
        public string SAMAccountType
        {
            get { return GetPropertyValue("sAMAccountType") != null ? GetPropertyValue("sAMAccountType").ToString() : null; }
        }

        /// <summary>
        /// 获取用户的最后一次更改时间,对应属性名称：whenChanged。
        /// </summary>
        public DateTime WhenChanged
        {
            get { return GetPropertyValue("whenChanged") != null ? DateTime.Parse(GetPropertyValue("whenChanged").ToString()) : DateTime.MinValue; }
        }

        /// <summary>
        /// 获取用户的创建时间,对应属性名称：whenCreated。
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
        public DomainUser(string path, string username, string password, AuthenticationTypes authenticationType)
            : base(path, username, password, authenticationType)
        {
            if (this.SchemaClassName != "user")
                throw new Exception("不是一个域用户对象");
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
        public DomainUser(string path, string username, string password)
            : base(path, username, password)
        {
            if (this.SchemaClassName != "user")
                throw new Exception("不是一个域用户对象");
            AddReadPropertyRange(_readPropertyName);
            AddWritePropertyRange(_writePropertyName);
            AddSingleValuePropertyRange(_singleValuePropertyName);
            AddMultipleValuePropertyRange(_multipleValuePropertyName);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重置用户账户的登陆口令。
        /// </summary>
        /// <param name="password">表示用户新的账户口令。</param>
        /// <returns>重置成功返回True，失败返回False。</returns>
        public bool SetPassword(string password)
        {
            try
            {
                Invoke("SetPassword", new object[] { password });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改用户账户的登陆口令。
        /// </summary>
        /// <param name="oldPassword">表示用户原的账户口令。</param>
        /// <param name="newPassword">表示用户新的账户口令。</param>
        /// <returns>修改成功返回True，失败返回False。</returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                Invoke("ChangePassword", new object[] { oldPassword, newPassword });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断账户是否激活。
        /// </summary>
        /// <returns>如果账户激活返回True，反正返回False。</returns>
        public bool IsAccountActive()
        {
            int userAccountControl_Disabled = Convert.ToInt32(ADS_USER_FLAG_ENUM.ADS_UF_ACCOUNTDISABLE);
            int flagExists = int.Parse(this.Properties["userAccountControl"].Value.ToString()) & userAccountControl_Disabled;
            if (flagExists > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 启用用户。
        /// </summary>
        public void Enable()
        {
            this.Properties["userAccountControl"][0] = ADS_USER_FLAG_ENUM.ADS_UF_NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.ADS_UF_DONT_EXPIRE_PASSWD;
        }

        /// <summary>
        /// 禁用用户。
        /// </summary>
        public void Disable()
        {
            this.Properties["userAccountControl"][0] = ADS_USER_FLAG_ENUM.ADS_UF_NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.ADS_UF_DONT_EXPIRE_PASSWD | ADS_USER_FLAG_ENUM.ADS_UF_ACCOUNTDISABLE;
        }

        /// <summary>
        /// 设置用户控制标志。
        /// </summary>
        /// <param name="userAccountControl">表示用户控制标志，UserFlag位运算值。</param>
        public void SetAccountControl(object userAccountControl)
        {
            this.Properties["userAccountControl"][0] = userAccountControl;
        }

        #endregion
    }
}
