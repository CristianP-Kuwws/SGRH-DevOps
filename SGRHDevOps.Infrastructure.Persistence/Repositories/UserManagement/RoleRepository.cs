using SGRHDevOps.Core.Domain.Entities.UserManagement;
using SGRHDevOps.Core.Domain.Interfaces.UserManagement;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Base;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.UserManagement
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(SGRHContext context) : base(context) { }
    }
}
