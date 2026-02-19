using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Domain.Entities.UserManagement
{
    public class Role : AuditEntity
    {
        public int RoleId { get; set; }
        public required string Name { get; set; }
    }
}
