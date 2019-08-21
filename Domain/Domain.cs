//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// 域访问控制模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System;
using System.Configuration;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using System.Xml;
using Yawei.Domain.Organization;

namespace Yawei.Domain
{
    /// <summary>
    /// 表示对域节点或对象访问的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public class Domain
    {
        #region 属性

        /// <summary>
        /// 获取域控服务器的名称。
        /// </summary>
        public string DomainName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对客户端进行身份验证时使用的用户名。
        /// </summary>
        public string Username
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对客户端进行身份验证时使用的用户口令。
        /// </summary>
        public string Password
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对客户端进行身份验证时使用的验证类型。
        /// </summary>
        public AuthenticationTypes AuthenticationType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取域控服务器的默认命名空间。
        /// </summary>
        public string DistinguishedName
        {
            get;
            private set;
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化对域节点或对象访问的新实例。
        /// </summary>
        /// <param name="domainName">表示域控服务器的名称。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        /// <param name="authenticationType">表示对客户端进行身份验证时使用的验证类型。</param>
        public Domain(string domainName,string username, string password, AuthenticationTypes authenticationType)
        {
            DomainName = domainName;
            Username = username;
            Password = password;
            AuthenticationType = authenticationType;

            using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + DomainName + "/rootDSE", Username, Password, AuthenticationType))
            {
                DistinguishedName = directoryEntry.Properties["defaultNamingContext"].Value.ToString();
            }
        }

        /// <summary>
        /// 初始化对域节点或对象访问的新实例。
        /// </summary>
        /// <param name="domainName">表示域控服务器的名称。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        public Domain(string domainName, string username, string password)
        {
            DomainName = domainName;
            Username = username;
            Password = password;
            AuthenticationType = AuthenticationTypes.Secure;

            using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + DomainName + "/rootDSE",Username,Password,AuthenticationType))
            {
                DistinguishedName = directoryEntry.Properties["defaultNamingContext"].Value.ToString();
            }
        }

        /// <summary>
        /// 从配置文件中读取 Yawei.Domain配置初始化对域节点或对象访问的实例。
        /// </summary>
        /// <param name="name">表示配置节点名称。</param>
        public Domain(string name)
        {
            if (ConfigurationManager.GetSection("Yawei.Domain") == null) throw new Exception("配置文件中找不到Yawei.Domain节点");
            XmlNode domainConfiguration = (XmlNode)ConfigurationManager.GetSection("Yawei.Domain");
            XmlNode defaultDomain = domainConfiguration.SelectSingleNode("add[@name='" + name + "']");
            if (defaultDomain == null) throw new Exception("配置文件中Yawei.Domain节点没有找到名为“" + name + "”的配置");
            if (defaultDomain.Attributes["domainName"] == null) throw new Exception("Yawei.Domain节点中找不到名为 domainName 的属性");
            DomainName = defaultDomain.Attributes["domainName"].Value;
            if (defaultDomain.Attributes["username"] == null) throw new Exception("Yawei.Domain节点中找不到名为 username 的属性");
            Username = defaultDomain.Attributes["username"].Value;
            if (defaultDomain.Attributes["password"] == null) throw new Exception("Yawei.Domain节点中找不到名为 password 的属性");
            Password = defaultDomain.Attributes["password"].Value;

            AuthenticationType = AuthenticationTypes.Secure;

            if (defaultDomain.Attributes["authenticationType"] != null)
            {
                AuthenticationType = (AuthenticationTypes)Enum.Parse(typeof(AuthenticationTypes), defaultDomain.Attributes["authenticationType"].Value, true);
            }

            using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + DomainName + "/rootDSE", Username, Password, AuthenticationType))
            {
                DistinguishedName = directoryEntry.Properties["defaultNamingContext"].Value.ToString();
            }
        }

        /// <summary>
        /// 从配置文件中读取 Yawei.Domain 配置初始化对域节点或对象访问的实例。
        /// </summary>
        public Domain()
        {
            if (ConfigurationManager.GetSection("Yawei.Domain") == null) throw new Exception("配置文件中找不到Yawei.Domain节点");
            XmlNode domainConfiguration = (XmlNode)ConfigurationManager.GetSection("Yawei.Domain");
            if (domainConfiguration.Attributes["defaultDomain"] == null) throw new Exception("配置文件中Yawei.Domain节点没有配置defaultDomain（默认域）的属性");
            XmlNode defaultDomain = domainConfiguration.SelectSingleNode("add");
            if (defaultDomain == null) throw new Exception("配置文件中Yawei.Domain节点没有找到defaultDomain（默认域）的配置");
            if (defaultDomain.Attributes["domainName"] == null) throw new Exception("Yawei.Domain节点中找不到名为 domainName 的属性");
            DomainName = defaultDomain.Attributes["domainName"].Value;
            if (defaultDomain.Attributes["username"] == null) throw new Exception("Yawei.Domain节点中找不到名为 username 的属性");
            Username = defaultDomain.Attributes["username"].Value;
            if (defaultDomain.Attributes["password"] == null) throw new Exception("Yawei.Domain节点中找不到名为 password 的属性");
            Password = defaultDomain.Attributes["password"].Value;

            AuthenticationType = AuthenticationTypes.Secure;

            if (defaultDomain.Attributes["authenticationType"] != null)
            {
                AuthenticationType = (AuthenticationTypes)Enum.Parse(typeof(AuthenticationTypes), defaultDomain.Attributes["authenticationType"].Value, true);
            }

            using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + DomainName + "/rootDSE", Username, Password, AuthenticationType))
            {
                DistinguishedName = directoryEntry.Properties["defaultNamingContext"].Value.ToString();
            }
        }

        #endregion

        #region 访问路径规范化

        /// <summary>
        /// 格式化域节点或对象的访问路径。
        /// </summary>
        /// <param name="path">表示原访问路径。</param>
        /// <returns>格式化访问路径。</returns>
        public string CreateDirectoryPath(string path)
        {
            path = path.Trim();
            if (Regex.IsMatch(path.ToUpper(), "^LDAP://" + DomainName.ToUpper() + "/"))
            {
                string p = path.Substring(7 + DomainName.Length + 1);
                return "LDAP://" + DomainName + "/" + p;
            }
            if (Regex.IsMatch(path.ToUpper(), "^LDAP://"))
            {
                string p = path.Substring(7);
                return "LDAP://" + DomainName + "/" + p;
            }
            if (Regex.IsMatch(path, "^[a-zA-Z0-9\\-]{36}$"))
            {
                return "LDAP://" + DomainName + "/" + "<GUID=" + path + ">";
            }
            if (Regex.IsMatch(path, "^.*$"))
            {
                return "LDAP://" + DomainName + "/" + path;
            }
            return path;
        }

        #endregion

        #region 域节点或对象方法

        /// <summary>
        /// 创建一个新的域节点或对象。
        /// </summary>
        /// <param name="containerDN">表示域节点或对象的容器DN。</param>
        /// <param name="schemaClassName">表示域节点或对象的类别。</param>
        /// <param name="commonName">表示域节点或对象的公共名称。</param>
        /// <returns>返回一个新的域节点或对象。</returns>
        public DirectoryEntry CreateNewDirectoryEntry(string containerDN, string schemaClassName, string commonName)
        {
            try
            {
                if (string.IsNullOrEmpty(containerDN.Trim())) throw new Exception("“containerDN”属性不能为空");
                if (string.IsNullOrEmpty(schemaClassName.Trim())) throw new Exception("“schemaClassName”属性不能为空");
                if (string.IsNullOrEmpty(commonName.Trim())) throw new Exception("“commonName”属性不能为空");
                string dn = string.Empty;
                string cn = string.Empty;
                if (schemaClassName == "organizationalUnit")
                {
                    dn = "OU=" + commonName + "," + containerDN;
                    cn = "OU=" + commonName;
                }
                else
                {
                    dn = "CN=" + commonName + "," + containerDN;
                    cn = "CN=" + commonName;
                }
                using (DirectoryEntry container = GetDirectoryEntry(containerDN))
                {
                    if (ExistDirectoryEntryByDN(dn))
                        throw new Exception("已存在公共名称为“" + commonName + "”的对象。");
                    DirectoryEntry directoryEntry = container.Children.Add(cn, schemaClassName);
                    directoryEntry.CommitChanges();
                    return directoryEntry;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建域节点或对象。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <returns>DirectoryEntry域节点或对象。</returns>
        public DirectoryEntry GetDirectoryEntry(string path)
        {
            DirectoryEntry directoryEntry = new DirectoryEntry(CreateDirectoryPath(path), Username, Password, AuthenticationType);
            return directoryEntry;
        }

        /// <summary>
        /// 根据域节点或对象的全名判断域节点或对象是否存在。
        /// </summary>
        /// <param name="dn">表示域节点或对象的全名。</param>
        /// <returns>返回域节点或对象是否存在。</returns>
        public bool ExistDirectoryEntryByDN(string dn)
        {
            SearchResultCollection results = ExecuteSearchResult("distinguishedName=" + dn + "");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据域节点或对象的主键判断工作组是否存在。
        /// </summary>
        /// <param name="guid">表示域节点或对象的唯一主键。</param>
        /// <returns>返回域节点或对象是否存在。</returns>
        public bool ExistDirectoryEntryByGuid(string guid)
        {

            byte[] b = Guid.Parse(guid).ToByteArray();
            string[] s = new string[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                s[i] = b[i].ToString("x2");
            }
            guid = "\\" + string.Join("\\", s);
            SearchResultCollection results = ExecuteSearchResult("objectGUID=" + guid + "");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断域节点或对象是否是一个指定的对象。
        /// </summary>
        /// <param name="directoryEntry">表示一个域节点或对象。</param>
        /// <param name="schemaClassName">表示一个域节点或对象的类型。</param>
        /// <returns>返回域节点或对象是否是一个联系人。</returns>
        public bool IsDirectoryEntry(DirectoryEntry directoryEntry, string schemaClassName)
        {
            try
            {
                if (directoryEntry.SchemaClassName == schemaClassName)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 域用户方法

        /// <summary>
        /// 创建一个新的用户对象。
        /// </summary>
        /// <param name="containerDN">表示用户的容器DN。</param>
        /// <param name="commonName">表示用户的公共名称。</param>
        /// <param name="sAMAccountName">表示用户的账户名称。</param>
        /// <param name="password">表示用户的账户密码。</param>
        /// <returns>返回一个新的用户对象。</returns>
        public DomainUser CreateNewUser(string containerDN, string commonName, string sAMAccountName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(containerDN.Trim())) throw new Exception("“containerDN”属性不能为空");
                if (string.IsNullOrEmpty(commonName.Trim())) throw new Exception("“commonName”属性不能为空");
                if (string.IsNullOrEmpty(sAMAccountName.Trim())) throw new Exception("“sAMAccountName”属性不能为空");
                string userDN = "CN=" + commonName + "," + containerDN;
                using (DirectoryEntry container = GetDirectoryEntry(containerDN))
                {
                    if (ExistDirectoryEntryByDN(userDN))
                        throw new Exception("已存在公共名称为“" + commonName + "”的对象。");
                    container.Children.Add("CN=" + commonName, "user").CommitChanges();
                    DomainUser domainUser = GetUser(userDN);
                    domainUser.LoginName = sAMAccountName;
                    domainUser.PrincipalName = sAMAccountName + "@" + DomainName;
                    domainUser.Enable();
                    domainUser.CommitChanges();
                    domainUser.SetPassword(password);
                    return domainUser;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 在Users容器下创建一个新的用户对象。
        /// </summary>
        /// <param name="commonName">表示用户的公共名称。</param>
        /// <param name="sAMAccountName">表示用户的账户名称。</param>
        /// <param name="password">表示用户的账户密码。</param>
        /// <returns>返回一个新的用户对象。</returns>
        public DomainUser CreateNewUser(string commonName, string sAMAccountName, string password)
        {
            return CreateNewUser("CN=Users", commonName, sAMAccountName, password);
        }

        /// <summary>
        /// 创建域用户对象。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <returns>域用户域节点或对象。</returns>
        public DomainUser GetUser(string path)
        {
            DomainUser domainUser = new DomainUser(CreateDirectoryPath(path), Username, Password, AuthenticationType);
            return domainUser;
        }

        /// <summary>
        /// 创建域用户对象。
        /// </summary>
        /// <param name="loginName">表示用户的登录名。</param>
        /// <returns>DomainUser域节点或对象。</returns>
        public DomainUser GetUserByLoginName(string loginName)
        {
            if (ExistUserByLoginName(loginName))
            {
                SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=person)(objectClass=user))(|(sAMAccountName=" + loginName + ")(userPrincipalName=" + loginName + ")))");
                DomainUser domainUser = new DomainUser(results[0].Path, Username, Password, AuthenticationType);
                return domainUser;
            }
            else
            {
                throw new Exception("没有找到登录名为“" + loginName + "”的用户");
            }
        }

        /// <summary>
        /// 根据用户的全名判断用户是否存在。
        /// </summary>
        /// <param name="dn">表示用户的全名。</param>
        /// <returns>返回用户是否存在。</returns>
        public bool ExistUserByDN(string dn)
        {
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=person)(objectClass=user))(distinguishedName=" + dn + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据用户的主键判断用户是否存在。
        /// </summary>
        /// <param name="guid">表示用户的唯一主键。</param>
        /// <returns>返回用户是否存在。</returns>
        public bool ExistUserByGuid(string guid)
        {

            byte[] b = Guid.Parse(guid).ToByteArray();
            string[] s = new string[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                s[i] = b[i].ToString("x2");
            }
            guid = "\\" + string.Join("\\", s);
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=person)(objectClass=user))(objectGUID=" + guid + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据用户登录名称判断用户是否存在。
        /// </summary>
        /// <param name="loginName">表示用户的登录名称。</param>
        /// <returns>返回用户是否存在。</returns>
        public bool ExistUserByLoginName(string loginName)
        {
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=person)(objectClass=user))(|(sAMAccountName=" + loginName + ")(userPrincipalName=" + loginName + ")))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断域节点或对象是否是一个用户。
        /// </summary>
        /// <param name="directoryEntry">表示一个域节点或对象。</param>
        /// <returns>返回域节点或对象是否是一个用户。</returns>
        public bool IsUser(DirectoryEntry directoryEntry)
        {
            try
            {
                if (directoryEntry.SchemaClassName == "user")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证用户登录。
        /// </summary>
        /// <param name="loginName">表示用户登录名。</param>
        /// <param name="password">表示用户登录口令。</param>
        /// <returns>返回用户登录结果。</returns>
        public LoginResult LoginUser(string loginName, string password)
        {
            if (!ExistUserByLoginName(loginName))
                return LoginResult.DOES_NOT_EXIST;
            using (DomainUser domainUser = GetUserByLoginName(loginName))
            {
                if (!domainUser.IsAccountActive())
                    return LoginResult.ACCOUNT_INACTIVE;
            }
            using (DirectoryEntry de = new DirectoryEntry(CreateDirectoryPath(DistinguishedName), DomainName + @"\" + loginName, password))
            {
                try
                {
                    object obj = de.NativeObject;
                    return LoginResult.SUCCESS;
                }
                catch
                {
                    return LoginResult.PASSWORD_INCORRECT;
                }
            }

        }

        #endregion

        #region 域工作组方法

        /// <summary>
        /// 创建一个新的工作组对象。
        /// </summary>
        /// <param name="containerDN">表示工作组的容器DN。</param>
        /// <param name="commonName">表示工作组的公共名称。</param>
        /// <param name="sAMAccountName">表示工作组的账户名称。</param>
        /// <param name="groupType">表示工作组的类型。</param>
        /// <returns>返回一个新的工作组对象。</returns>
        public DomainGroup CreateNewGroup(string containerDN, string commonName, string sAMAccountName,ADS_GROUP_TYPE_ENUM groupType)
        {
            try
            {
                if (string.IsNullOrEmpty(containerDN.Trim())) throw new Exception("“containerDN”属性不能为空");
                if (string.IsNullOrEmpty(commonName.Trim())) throw new Exception("“commonName”属性不能为空");
                if (string.IsNullOrEmpty(sAMAccountName.Trim())) throw new Exception("“sAMAccountName”属性不能为空");
                string groupDN = "CN=" + commonName + "," + containerDN;
                using (DirectoryEntry container = GetDirectoryEntry(containerDN))
                {
                    if (ExistDirectoryEntryByDN(groupDN))
                        throw new Exception("已存在公共名称为“" + commonName + "”的对象。");
                    container.Children.Add("CN=" + commonName, "group").CommitChanges();
                    DomainGroup Domaingroup = GetGroup(groupDN);
                    Domaingroup.SAMAccountName = sAMAccountName;
                    Domaingroup.GroupType = ((long)groupType).ToString();
                    Domaingroup.CommitChanges();
                    return Domaingroup;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建一个新的工作组对象。
        /// </summary>
        /// <param name="containerDN">表示工作组的容器DN。</param>
        /// <param name="commonName">表示工作组的公共名称。</param>
        /// <param name="sAMAccountName">表示工作组的账户名称。</param>
        /// <returns>返回一个新的工作组对象。</returns>
        public DomainGroup CreateNewGroup(string containerDN, string commonName, string sAMAccountName)
        {
            try
            {
                if (string.IsNullOrEmpty(containerDN.Trim())) throw new Exception("“containerDN”属性不能为空");
                if (string.IsNullOrEmpty(commonName.Trim())) throw new Exception("“commonName”属性不能为空");
                if (string.IsNullOrEmpty(sAMAccountName.Trim())) throw new Exception("“sAMAccountName”属性不能为空");
                string groupDN = "CN=" + commonName + "," + containerDN;
                using (DirectoryEntry container = GetDirectoryEntry(containerDN))
                {
                    if (ExistDirectoryEntryByDN(groupDN))
                        throw new Exception("已存在公共名称为“" + commonName + "”的对象。");
                    container.Children.Add("CN=" + commonName, "group").CommitChanges();
                    DomainGroup Domaingroup = GetGroup(groupDN);
                    Domaingroup.SAMAccountName = sAMAccountName;
                    Domaingroup.CommitChanges();
                    return Domaingroup;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 在Users容器下创建一个新的工作组对象。
        /// </summary>
        /// <param name="commonName">表示工作组的公共名称。</param>
        /// <param name="sAMAccountName">表示工作组的账户名称。</param>
        /// <param name="groupType">表示工作组的类型。</param>
        /// <returns>返回一个新的工作组对象。</returns>
        public DomainGroup CreateNewGroup(string commonName, string sAMAccountName, ADS_GROUP_TYPE_ENUM groupType)
        {
            return CreateNewGroup("CN=Users", commonName, sAMAccountName, groupType);
        }

        /// <summary>
        /// 在Users容器下创建一个新的工作组对象。
        /// </summary>
        /// <param name="commonName">表示工作组的公共名称。</param>
        /// <param name="sAMAccountName">表示工作组的账户名称。</param>
        /// <returns>返回一个新的工作组对象。</returns>
        public DomainGroup CreateNewGroup(string commonName, string sAMAccountName)
        {
            return CreateNewGroup("CN=Users", commonName, sAMAccountName);
        }

        /// <summary>
        /// 创建域工作组对象。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <returns>域工作组域节点或对象。</returns>
        public DomainGroup GetGroup(string path)
        {
            DomainGroup domainGroup = new DomainGroup(CreateDirectoryPath(path), Username, Password, AuthenticationType);
            return domainGroup;
        }

        /// <summary>
        /// 根据工作组的全名判断工作组是否存在。
        /// </summary>
        /// <param name="dn">表示工作组的全名。</param>
        /// <returns>返回工作组是否存在。</returns>
        public bool ExistGroupByDN(string dn)
        {
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=group)(objectClass=group))(distinguishedName=" + dn + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据工作组的主键判断工作组是否存在。
        /// </summary>
        /// <param name="guid">表示工作组的唯一主键。</param>
        /// <returns>返回工作组是否存在。</returns>
        public bool ExistGroupByGuid(string guid)
        {

            byte[] b = Guid.Parse(guid).ToByteArray();
            string[] s = new string[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                s[i] = b[i].ToString("x2");
            }
            guid = "\\" + string.Join("\\", s);
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=group)(objectClass=group))(objectGUID=" + guid + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据工作组的名称判断工作组是否存在。
        /// </summary>
        /// <param name="cn">表示工作组的名称。</param>
        /// <returns>返回工作组是否存在。</returns>
        public bool ExistGroupByCN(string cn)
        {
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=group)(objectClass=group))(cn=" + cn + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据工作组登录名称判断工作组是否存在。
        /// </summary>
        /// <param name="loginName">表示工作组的登录名称。</param>
        /// <returns>返回工作组是否存在。</returns>
        public bool ExistGroupByLoginName(string loginName)
        {
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=group)(objectClass=group))(sAMAccountName=" + loginName + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断域节点或对象是否是一个工作组。
        /// </summary>
        /// <param name="directoryEntry">表示一个域节点或对象。</param>
        /// <returns>返回域节点或对象是否是一个工作组。</returns>
        public bool IsGroup(DirectoryEntry directoryEntry)
        {
            try
            {
                if (directoryEntry.SchemaClassName == "group")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 组织单元方法

        /// <summary>
        /// 创建一个新的组织单元对象。
        /// </summary>
        /// <param name="containerDN">表示组织单元的容器DN。</param>
        /// <param name="commonName">表示组织单元的公共名称。</param>
        /// <returns>返回一个新的组织单元对象。</returns>
        public DomainOU CreateNewOU(string containerDN, string commonName)
        {
            try
            {
                if (string.IsNullOrEmpty(containerDN.Trim())) throw new Exception("“containerDN”属性不能为空");
                if (string.IsNullOrEmpty(commonName.Trim())) throw new Exception("“commonName”属性不能为空");
                string ouDN = "OU=" + commonName + "," + containerDN;
                using (DirectoryEntry container = GetDirectoryEntry(containerDN))
                {
                    if (ExistDirectoryEntryByDN(ouDN))
                        throw new Exception("已存在公共名称为“" + commonName + "”的对象。");
                    container.Children.Add("OU=" + commonName, "organizationalUnit").CommitChanges();
                    DomainOU domainOU = GetOU(ouDN);
                    return domainOU;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建组织单元对象。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <returns>组织单元域节点或对象。</returns>
        public DomainOU GetOU(string path)
        {
            DomainOU domainOU = new DomainOU(CreateDirectoryPath(path), Username, Password, AuthenticationType);
            return domainOU;
        }

        /// <summary>
        /// 根据组织单元的全名判断工作组是否存在。
        /// </summary>
        /// <param name="dn">表示组织单元的全名。</param>
        /// <returns>返回组织单元是否存在。</returns>
        public bool ExistOUByDN(string dn)
        {
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=organizationalUnit)(objectClass=organizationalUnit))(distinguishedName=" + dn + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据组织单元的主键判断工作组是否存在。
        /// </summary>
        /// <param name="guid">表示组织单元的唯一主键。</param>
        /// <returns>返回组织单元是否存在。</returns>
        public bool ExistOUByGuid(string guid)
        {

            byte[] b = Guid.Parse(guid).ToByteArray();
            string[] s = new string[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                s[i] = b[i].ToString("x2");
            }
            guid = "\\" + string.Join("\\", s);
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=organizationalUnit)(objectClass=organizationalUnit))(objectGUID=" + guid + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断域节点或对象是否是一个组织单元。
        /// </summary>
        /// <param name="directoryEntry">表示一个域节点或对象。</param>
        /// <returns>返回域节点或对象是否是一个组织单元。</returns>
        public bool IsOU(DirectoryEntry directoryEntry)
        {
            try
            {
                if (directoryEntry.SchemaClassName == "organizationalUnit")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 联系人方法

        /// <summary>
        /// 创建一个新的联系人对象。
        /// </summary>
        /// <param name="containerDN">表示联系人的容器DN。</param>
        /// <param name="commonName">表示联系人的公共名称。</param>
        /// <returns>返回一个新的联系人对象。</returns>
        public DomainContact CreateNewContact(string containerDN, string commonName)
        {
            try
            {
                if (string.IsNullOrEmpty(containerDN.Trim())) throw new Exception("“containerDN”属性不能为空");
                if (string.IsNullOrEmpty(commonName.Trim())) throw new Exception("“commonName”属性不能为空");
                string contactDN = "CN=" + commonName + "," + containerDN;
                using (DirectoryEntry container = GetDirectoryEntry(containerDN))
                {
                    if (ExistDirectoryEntryByDN(contactDN))
                        throw new Exception("已存在公共名称为“" + commonName + "”的对象。");
                    container.Children.Add("CN=" + commonName, "contact").CommitChanges();
                    DomainContact domainContact = GetContact(contactDN);
                    return domainContact;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建联系人对象。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <returns>联系人域节点或对象。</returns>
        public DomainContact GetContact(string path)
        {
            DomainContact domainContact = new DomainContact(CreateDirectoryPath(path), Username, Password, AuthenticationType);
            return domainContact;
        }

        /// <summary>
        /// 根据联系人的全名判断工作组是否存在。
        /// </summary>
        /// <param name="dn">表示联系人的全名。</param>
        /// <returns>返回联系人是否存在。</returns>
        public bool ExistContactByDN(string dn)
        {
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=person)(objectClass=contact))(distinguishedName=" + dn + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据联系人的主键判断工作组是否存在。
        /// </summary>
        /// <param name="guid">表示联系人的唯一主键。</param>
        /// <returns>返回联系人是否存在。</returns>
        public bool ExistContactByGuid(string guid)
        {

            byte[] b = Guid.Parse(guid).ToByteArray();
            string[] s = new string[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                s[i] = b[i].ToString("x2");
            }
            guid = "\\" + string.Join("\\", s);
            SearchResultCollection results = ExecuteSearchResult("(&(&(objectCategory=person)(objectClass=contact))(objectGUID=" + guid + "))");
            if (results.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断域节点或对象是否是一个联系人。
        /// </summary>
        /// <param name="directoryEntry">表示一个域节点或对象。</param>
        /// <returns>返回域节点或对象是否是一个联系人。</returns>
        public bool IsContact(DirectoryEntry directoryEntry)
        {
            try
            {
                if (directoryEntry.SchemaClassName == "contact")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 查询域节点或对象

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="directoryEntry">表示开始查询的节点。</param>
        /// <param name="filter">表示查询条件。</param>
        /// <param name="scope">表示查询方式。</param>
        /// <param name="properties">表示返回结果的属性数组。</param>
        /// <returns>查询结果集合。</returns>
        public static SearchResultCollection ExecuteSearchResult(DirectoryEntry directoryEntry, string filter, SearchScope scope, string[] properties)
        {
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry);
            SearchResultCollection results;
            searcher.Filter = filter;
            searcher.SearchScope = scope;
            if (properties != null && properties.Length > 0)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    searcher.PropertiesToLoad.Add(properties[i]);
                }
            }
            results = searcher.FindAll();
            return results;
        }

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="directoryEntry">表示开始查询的节点。</param>
        /// <param name="filter">表示查询条件。</param>
        /// <param name="scope">表示查询方式。</param>
        /// <returns>查询结果集合。</returns>
        public static SearchResultCollection ExecuteSearchResult(DirectoryEntry directoryEntry, string filter, SearchScope scope)
        {
            return ExecuteSearchResult(directoryEntry, filter, scope, null);
        }

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="directoryEntry">表示开始查询的节点。</param>
        /// <param name="filter">表示查询条件。</param>
        /// <param name="properties">表示返回结果的属性数组。</param>
        /// <returns>查询结果集合。</returns>
        public static SearchResultCollection ExecuteSearchResult(DirectoryEntry directoryEntry, string filter, string[] properties)
        {
            return ExecuteSearchResult(directoryEntry, filter, SearchScope.Subtree, properties);
        }

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="directoryEntry">表示开始查询的节点。</param>
        /// <param name="filter">表示查询条件。</param>
        /// <returns>查询结果集合。</returns>
        public static SearchResultCollection ExecuteSearchResult(DirectoryEntry directoryEntry, string filter)
        {
            return ExecuteSearchResult(directoryEntry, filter, SearchScope.Subtree);
        }

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="filter">表示查询条件。</param>
        /// <param name="scope">表示查询方式。</param>
        /// <param name="properties">表示返回结果的属性数组。</param>
        /// <returns>查询结果集合。</returns>
        public SearchResultCollection ExecuteSearchResult(string filter, SearchScope scope, string[] properties)
        {
            DirectoryEntry directoryEntry = GetDirectoryEntry(DistinguishedName);
            return ExecuteSearchResult(directoryEntry, filter, scope, properties);
        }

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="filter">表示查询条件。</param>
        /// <param name="scope">表示查询方式。</param>
        /// <returns>查询结果集合。</returns>
        public SearchResultCollection ExecuteSearchResult(string filter, SearchScope scope)
        {
            DirectoryEntry directoryEntry = GetDirectoryEntry(DistinguishedName);
            return ExecuteSearchResult(directoryEntry, filter, scope, null);
        }

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="filter">表示查询条件。</param>
        /// <param name="properties">表示返回结果的属性数组。</param>
        /// <returns>查询结果集合。</returns>
        public SearchResultCollection ExecuteSearchResult(string filter, string[] properties)
        {
            DirectoryEntry directoryEntry = GetDirectoryEntry(DistinguishedName);
            return ExecuteSearchResult(directoryEntry, filter, SearchScope.Subtree, properties);
        }

        /// <summary>
        /// 查询并返回一个结果集。
        /// </summary>
        /// <param name="filter">表示查询条件。</param>
        /// <returns>查询结果集合。</returns>
        public SearchResultCollection ExecuteSearchResult(string filter)
        {
            DirectoryEntry directoryEntry = GetDirectoryEntry(DistinguishedName);
            return ExecuteSearchResult(directoryEntry, filter);
        }

        #endregion
    }
}
