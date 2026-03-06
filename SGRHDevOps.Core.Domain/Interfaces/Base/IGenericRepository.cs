using SGRHDevOps.Core.Domain.Common.Base;

namespace SGRHDevOps.Core.Domain.Interfaces.Base
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<OperationResult<Entity>> AddAsync(Entity entity);
        Task<OperationResult<Entity>> UpdateAsync(int id, Entity entity);
        Task<OperationResult<Entity>> DeleteAsync(int id);
        Task<OperationResult<List<Entity>>> GetAllListAsync();
        Task<OperationResult<Entity>> GetByIdAsync(int id);

        // Additional
        IQueryable<Entity> GetAllQuery();
        IQueryable<Entity> GetAllQueryWithInclude(List<string> properties);
        Task<OperationResult<List<Entity>>> AddRangeAsync(List<Entity> entities);
        Task<OperationResult<List<Entity>>> UpdateRange(List<Entity> entities);
        Task<OperationResult<bool>> SaveAsync();
    }
}
