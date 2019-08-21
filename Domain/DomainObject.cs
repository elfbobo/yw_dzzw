//===============================================================================
// Enterprise Technical Architecture 企业技术架构
// Library 软件开发功能组件库
// 域访问控制模块
//===============================================================================
// 著 作 人：张毅。 
// 版权所有：Copyright 2013 by 张毅 All Rights Reserved。
//===============================================================================

using System.Collections;
using System.DirectoryServices;
using System;

namespace Yawei.Domain
{
    /// <summary>
    /// 表示域节点或对象的实例。
    /// </summary>
    /// <see cref="Author">张毅</see>
    /// <see cref="Email">ZhangYi_Dev@Hotmail.com</see>
    /// <see cref="Data"></see>
    public abstract class DomainObject : DirectoryEntry
    {

        #region 属性

        /// <summary>
        /// 获取或设置只读属性名称的集合。
        /// </summary>
        public ArrayList ReadProperty
        {
            get;
            protected set;
        }

        /// <summary>
        /// 获取或设置读写属性名称的集合。
        /// </summary>
        public ArrayList WriteProperty
        {
            get;
            protected set;
        }

        /// <summary>
        /// 获取或设置单值属性名称的集合。
        /// </summary>
        public ArrayList SingleValueProperty
        {
            get;
            protected set;
        }

        /// <summary>
        /// 获取或设置多值属性名称的集合。
        /// </summary>
        public ArrayList MultipleValueProperty
        {
            get;
            protected set;
        }

        #endregion

        #region 构造器

        /// <summary>
        /// 初始化域节点或对象的实例。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        /// <param name="authenticationType">表示对客户端进行身份验证时使用的验证类型。</param>
        public DomainObject(string path, string username, string password, AuthenticationTypes authenticationType)
            : base(path, username, password, authenticationType)
        {
            ReadProperty = new ArrayList();
            WriteProperty = new ArrayList();
            SingleValueProperty = new ArrayList();
            MultipleValueProperty = new ArrayList();
        }

        /// <summary>
        /// 初始化域节点或对象的实例。
        /// </summary>
        /// <param name="path">表示访问路径。</param>
        /// <param name="username">表示对客户端进行身份验证时使用的用户名。</param>
        /// <param name="password">表示对客户端进行身份验证时使用的用户口令。</param>
        public DomainObject(string path, string username, string password)
            : base(path, username, password)
        {
            ReadProperty = new ArrayList();
            WriteProperty = new ArrayList();
            SingleValueProperty = new ArrayList();
            MultipleValueProperty = new ArrayList();
        }

        /// <summary>
        /// 初始化域节点或对象的实例。
        /// </summary>
        protected DomainObject()
            : base()
        {
            ReadProperty = new ArrayList();
            WriteProperty = new ArrayList();
            SingleValueProperty = new ArrayList();
            MultipleValueProperty = new ArrayList();
        }

        #endregion

        #region 属性方法

        /// <summary>
        /// 获取属性的值。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回原属性值。</returns>
        public object GetPropertyValue(string propertyName)
        {
            if (this.Properties.Contains(propertyName))
            {
                return this.Properties[propertyName].Value;
            }
            return null;
        }

        /// <summary>
        /// 获取属性值数组。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性值数组。</returns>
        public object[] GetPropertyValues(string propertyName)
        {
            if (this.Properties.Contains(propertyName))
            {
                if (this.Properties[propertyName].Value is Array)
                    return (object[])this.Properties[propertyName].Value;
                else
                    return new object[1] { this.Properties[propertyName].Value };

            }
            return null;
        }

        /// <summary>
        /// 获取属性值数组的字符串。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性值数组的字符串。</returns>
        public string[] GetPropertyValuesToStrings(string propertyName)
        {
            object[] objs = GetPropertyValues(propertyName);
            string[] ret = null;
            if (objs != null)
            {
                ret = new string[objs.Length];
                for (int i = 0; i < objs.Length; i++)
                {
                    ret[i] = objs[i].ToString();
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取属性值的合并字符串。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <param name="delimiter">表示属性值分隔符。</param>
        /// <returns>返回属性值字符串。</returns>
        public string GetPropertyValuesToString(string propertyName,char delimiter)
        {
            string values = string.Empty;
            if (this.Properties.Contains(propertyName))
            {
                PropertyValueCollection propertyValueCollection = Properties[propertyName];
                for (int i = 0; i < propertyValueCollection.Count; i++)
                {
                    values += propertyValueCollection[i].ToString();
                    if (i + 1 < propertyValueCollection.Count) values += delimiter;
                }
            }
            return values;
        }

        /// <summary>
        /// 获取属性值的合并字符串。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性值字符串。</returns>
        public string GetPropertyValuesToString(string propertyName)
        {
            return GetPropertyValuesToString(propertyName, ';');
        }

        /// <summary>
        /// 设置属性值。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <param name="value">表示新的属性值。</param>
        public PropertyResult SetPropertyValue(string propertyName, object value)
        {
            if (IsWriteProperty(propertyName))
            {
                if (IsSingleValueProperty(propertyName))
                {
                    if (value == null || string.IsNullOrEmpty(value.ToString()))
                        return PropertyResult.ERROR_PROPERTY_VALUE;
                    if (this.Properties.Contains(propertyName))
                        this.Properties[propertyName][0] = value;
                    else
                        this.Properties[propertyName].Add(value);
                    return PropertyResult.SUCCESS;
                }

                if (IsMultipleValueProperty(propertyName))
                {
                    if (value == null || (value is Array && ((object[])value).Length == 0))
                        return PropertyResult.ERROR_PROPERTY_VALUE;
                    object[] values = (object[])value;
                    if (this.Properties.Contains(propertyName))
                    {
                        foreach (object obj in values)
                        {
                            if (!this.Properties[propertyName].Contains(obj))
                            {
                                this.Properties[propertyName].Add(obj);
                            }
                        }
                    }
                    else
                    {
                        this.Properties[propertyName].AddRange(values);
                    }
                    return PropertyResult.SUCCESS;
                }

                return PropertyResult.FAILURE;
            }
            else
            {
                return PropertyResult.CAN_NOT_WRITE;
            }
        }

        /// <summary>
        /// 设置属性值。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <param name="oldValue">表示原属性值。</param>
        /// <param name="newValue">表示新属性值。</param>
        public PropertyResult SetPropertyValue(string propertyName, object oldValue, object newValue)
        {
            if (this.Properties.Contains(propertyName))
            {
                if (IsWriteProperty(propertyName))
                {
                    if (this.Properties[propertyName].Contains(oldValue))
                    {
                        for (int i = 0; i < this.Properties[propertyName].Count; i++)
                        {
                            if (this.Properties[propertyName][i] == oldValue)
                            {
                                this.Properties[propertyName][i] = newValue;
                                return PropertyResult.SUCCESS;
                            }
                        }
                    }
                    return PropertyResult.NO_PROPERTY_VALUE;
                }
                else
                {
                    return PropertyResult.CAN_NOT_WRITE;
                }
            }
            else
            {
                return SetPropertyValue(propertyName, newValue);
            }
        }

        /// <summary>
        /// 设置属性值。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <param name="index">表示属性值索引。</param>
        /// <param name="value">表示新属性值。</param>
        public PropertyResult SetPropertyValue(string propertyName, int index, object value)
        {
            if (IsWriteProperty(propertyName))
            {
                if (this.Properties.Contains(propertyName))
                {
                    if (index >= this.Properties[propertyName].Count)
                        return PropertyResult.INDEX_OUT_BROUNDS;
                    this.Properties[propertyName][index] = value;
                    return PropertyResult.SUCCESS;
                }
                else
                {
                    return PropertyResult.PROPERTY_NOT_FOUND;
                }
            }
            else
            {
                return PropertyResult.CAN_NOT_WRITE;
            }
        }

        /// <summary>
        /// 覆盖属性值。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <param name="value">表示新的属性值。</param>
        public PropertyResult CoverPropertyValue(string propertyName, object value)
        {
            if (this.Properties.Contains(propertyName))
            {
                this.Properties[propertyName].Value = value;
                return PropertyResult.SUCCESS;
            }
            else
            {
                return SetPropertyValue(propertyName, value);
            }
        }

        /// <summary>
        /// 清除属性。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        public PropertyResult ClearPropertyValue(string propertyName)
        {
            if (IsWriteProperty(propertyName))
            {
                if (this.Properties.Contains(propertyName))
                {
                    this.Properties[propertyName].Clear();
                }
                return PropertyResult.SUCCESS;
            }
            else
            {
                return PropertyResult.CAN_NOT_WRITE;
            }
        }

        /// <summary>
        /// 移除属性值。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <param name="value">表示准备移除的属性值。</param>
        public PropertyResult RemovePropertyValue(string propertyName, object value)
        {
            if (IsWriteProperty(propertyName))
            {
                if (this.Properties.Contains(propertyName))
                {
                    if (this.Properties[propertyName].Contains(value))
                    {
                        this.Properties[propertyName].Remove(value);
                        return PropertyResult.SUCCESS;
                    }
                    else
                    {
                        return PropertyResult.NO_PROPERTY_VALUE;
                    }
                }
                else
                {
                    return PropertyResult.PROPERTY_NOT_FOUND;
                }
            }
            else
            {
                return PropertyResult.CAN_NOT_WRITE;
            }
        }

        /// <summary>
        /// 移除属性值。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <param name="index">表示准备移除的属性值索引。</param>
        public PropertyResult RemovePropertyValue(string propertyName, int index)
        {
            if (IsWriteProperty(propertyName))
            {
                if (this.Properties.Contains(propertyName))
                {
                    if (index < this.Properties[propertyName].Count)
                    {
                        this.Properties[propertyName].RemoveAt(index);
                        return PropertyResult.SUCCESS;
                    }
                    else
                    {
                        return PropertyResult.INDEX_OUT_BROUNDS;
                    }
                }
                else
                {
                    return PropertyResult.PROPERTY_NOT_FOUND;
                }
            }
            else
            {
                return PropertyResult.CAN_NOT_WRITE;
            }
        }

        /// <summary>
        /// 获取属性值数量。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性值数量，-1表示找不到属性。</returns>
        public int GetPropertyValueCount(string propertyName)
        {
            if (this.Properties.Contains(propertyName))
            {
                return this.Properties[propertyName].Count;
            }
            return -1;
        }

        /// <summary>
        /// 判定属性是否是只读属性。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性是否是只读属性。</returns>
        public bool IsReadProperty(string propertyName)
        {
            if (ReadProperty.Contains(propertyName))
                return true;
            return false;
        }

        /// <summary>
        /// 判定属性是否是读写属性。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性是否是读写属性。</returns>
        public bool IsWriteProperty(string propertyName)
        {
            if (WriteProperty.Contains(propertyName))
                return true;
            return false;
        }

        /// <summary>
        /// 判定属性是否是单值属性。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性是否是单值属性。</returns>
        public bool IsSingleValueProperty(string propertyName)
        {
            if (SingleValueProperty.Contains(propertyName))
                return true;
            return false;
        }

        /// <summary>
        /// 判定属性是否是多值属性。
        /// </summary>
        /// <param name="propertyName">表示属性名称。</param>
        /// <returns>返回属性是否是多值属性。</returns>
        public bool IsMultipleValueProperty(string propertyName)
        {
            if (MultipleValueProperty.Contains(propertyName))
                return true;
            return false;
        }

        /// <summary>
        /// 将属性加入只读属性集合中。
        /// </summary>
        /// <param name="propertyNames">属性名称集合字符串，以';'号隔开。</param>
        protected void AddReadPropertyRange(string propertyNames)
        {
            string[] propertyNameArray = propertyNames.Split(';');
            foreach (string s in propertyNameArray)
            {
                ReadProperty.Add(s);
            }
        }

        /// <summary>
        /// 将属性加入读写属性集合中。
        /// </summary>
        /// <param name="propertyNames">属性名称集合字符串，以';'号隔开。</param>
        protected void AddWritePropertyRange(string propertyNames)
        {
            string[] propertyNameArray = propertyNames.Split(';');
            foreach (string s in propertyNameArray)
            {
                WriteProperty.Add(s);
            }
        }

        /// <summary>
        /// 将属性加入单值属性集合中。
        /// </summary>
        /// <param name="propertyNames">属性名称集合字符串，以';'号隔开。</param>
        protected void AddSingleValuePropertyRange(string propertyNames)
        {
            string[] propertyNameArray = propertyNames.Split(';');
            foreach (string s in propertyNameArray)
            {
                SingleValueProperty.Add(s);
            }
        }

        /// <summary>
        /// 将属性加入多值属性集合中。
        /// </summary>
        /// <param name="propertyNames">属性名称集合字符串，以';'号隔开。</param>
        protected void AddMultipleValuePropertyRange(string propertyNames)
        {
            string[] propertyNameArray = propertyNames.Split(';');
            foreach (string s in propertyNameArray)
            {
                MultipleValueProperty.Add(s);
            }
        }

        #endregion
    }
}
