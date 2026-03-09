using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Domain.Entities.UserManagement
{
    public class User : AuditEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
