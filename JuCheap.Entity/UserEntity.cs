/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 09/04/2015 11:47:14
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System.Collections.Generic;
using JuCheap.Entity.Base;
using Newtonsoft.Json;

namespace JuCheap.Entity
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserEntity : BaseEntity
    {

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public byte Status { get; set; }
        
        /// <summary>
        /// 用户拥有的角色
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; } 

    }
}
