using SGHR_DevOps.Common.Base;

namespace SGHR_DevOps.Entities.UserManagement
{
    public class Role : AuditEntity
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
