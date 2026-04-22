using SGRHDevOps.Core.Domain.Entities.UserManagement;
using SGRHDevOps.Core.Domain.Interfaces.UserManagement;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Base;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.UserManagement
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SGRHContext context) : base(context) { }
    }
}
