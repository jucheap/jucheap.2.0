/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2015/8/7 11:12:12
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Configuration;

namespace JuCheap.Service.Config
{
    /// <summary>
    /// Email Config
    /// </summary>
    public class EmailConfiguration : ConfigurationSection
    {
        private static EmailConfiguration setting;

        /// <summary>
        /// EmailConfiguration
        /// </summary>
        public static EmailConfiguration Setting
        {
            get
            {
                if (setting == null)
                {
                    ExeConfigurationFileMap exeMap = new ExeConfigurationFileMap()
                    {
                        ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "/Config/Email.config"
                    };
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(exeMap,
                        ConfigurationUserLevel.None);

                    setting = (EmailConfiguration)config.GetSection("emailConfig");
                }

                return setting;
            }
        }

        /// <summary>
        /// SmtpServer
        /// </summary>
        [ConfigurationProperty("SmtpServer", IsRequired = true)]
        public string SmtpServer
        {
            get { return (string)this["SmtpServer"]; }
            set { this["SmtpServer"] = value; }
        }

        /// <summary>
        /// Port
        /// </summary>
        [ConfigurationProperty("Port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }

        /// <summary>
        /// Email
        /// </summary>
        [ConfigurationProperty("Email", IsRequired = true)]
        public string Email
        {
            get { return (string)this["Email"]; }
            set { this["Email"] = value; }
        }

        /// <summary>
        /// Password
        /// </summary>
        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }

        /// <summary>
        /// SSL
        /// </summary>
        [ConfigurationProperty("SSL", IsRequired = true)]
        public bool SSL
        {
            get { return (bool)this["SSL"]; }
            set { this["SSL"] = value; }
        }
    }
}
