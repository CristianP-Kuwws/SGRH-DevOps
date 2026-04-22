using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.UserManagement;
using SGRHDevOps.Core.Domain.Interfaces.UserManagement;
using SGRHDevOps.Infrastructure.Persistence.Contexts;
using SGRHDevOps.Infrastructure.Persistence.Repositories.Base;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.UserManagement
{
    public class PersonRepository : GenericRepository<User>, IPersonRepository
    {
        public PersonRepository(SGRHContext context) : base(context) { }
    }
}
