using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Entities.UserManagement;
using SGRHDevOps.Core.Domain.Interfaces.Base;

namespace SGRHDevOps.Core.Domain.Interfaces.UserManagement
{
    public interface IPersonRepository : IGenericRepository<User>
    {

    }
}
