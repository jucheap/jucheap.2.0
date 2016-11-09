

namespace JuCheap.Service.Dto
{
    /// <summary>
    /// 用户角色关系DTO
    /// </summary>
    public class UserRoleDto : BaseDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
    }
}
