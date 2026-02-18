using SGHR_DevOps.Core.Domain.Common.Base;

namespace SGHR_DevOps.Core.Domain.Entities.UserManagement
{
    public class Role : AuditEntity
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
