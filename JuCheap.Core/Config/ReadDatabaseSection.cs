/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2016/2/24 11:12:12
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Configuration;

namespace JuCheap.Core.Config
{
    /// <summary>
    /// 只读数据库配置
    /// </summary>
    public class ReadDatabaseSection : ConfigurationSection
    {
        /// <summary>
        /// 只读数据库配置集合
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public DatabaseCollection Databases
        {
            get
            {
                return (DatabaseCollection)base[""];
            }
        }
    }

    /// <summary>
    /// 只读数据库配置节点集合
    /// </summary>
    public class DatabaseCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseElement)element).Key;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "Database";
            }
        }

        public DatabaseElement this[int index]
        {
            get
            {
                return (DatabaseElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }

    /// <summary>
    /// 单个节点对象
    /// </summary>
    public class DatabaseElement : ConfigurationElement
    {
        /// <summary>
        /// 唯一标识Key
        /// </summary>
        [ConfigurationProperty("Key", IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)base["Key"];
            }
            set
            {
                base["Key"] = value;
            }
        }

        /// <summary>
        /// 数据库服务器
        /// </summary>
        [ConfigurationProperty("Datasource", IsRequired = true)]
        public string Datasource
        {
            get
            {
                return (string)base["Datasource"];
            }
            set
            {
                base["Datasource"] = value;
            }
        }

        /// <summary>
        /// 数据库服务器端口号
        /// </summary>
        [ConfigurationProperty("Port", IsRequired = true)]
        public int Port
        {
            get
            {
                return (int)base["Port"];
            }
            set
            {
                base["Port"] = value;
            }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [ConfigurationProperty("DatabaseName", IsRequired = true)]
        public string DatabaseName
        {
            get
            {
                return (string)base["DatabaseName"];
            }
            set
            {
                base["DatabaseName"] = value;
            }
        }

        /// <summary>
        /// 数据库帐号
        /// </summary>
        [ConfigurationProperty("User", IsRequired = true)]
        public string User
        {
            get
            {
                return (string)base["User"];
            }
            set
            {
                base["User"] = value;
            }
        }

        /// <summary>
        /// 数据库帐号登录密码
        /// </summary>
        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)base["Password"];
            }
            set
            {
                base["Password"] = value;
            }
        }
    }
}
