using Microsoft.EntityFrameworkCore;
using SGRHDevOps.Core.Domain.Common.Base;
using SGRHDevOps.Core.Domain.Interfaces.Base;
using SGRHDevOps.Infrastructure.Persistence.Contexts;

namespace SGRHDevOps.Infrastructure.Persistence.Repositories.Base
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        protected readonly SGRHContext _context;

        public GenericRepository(SGRHContext context)
        {
            _context = context;
        }

        public virtual async Task<OperationResult<Entity>> AddAsync(Entity entity)
        {
            try
            {
                if (entity == null)
                    return OperationResult<Entity>.Failure($"{typeof(Entity).Name} is null.");

                await _context.Set<Entity>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return OperationResult<Entity>.Success($"{typeof(Entity).Name} added successfully.", entity);
            }
            catch (Exception ex)
            {
                return OperationResult<Entity>.Failure($"An error occurred while trying to add the entity: {typeof(Entity).Name}. {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<Entity>> UpdateAsync(int id, Entity entity)
        {
            try
            {
                if (entity == null)
                    return OperationResult<Entity>.Failure($"{typeof(Entity).Name} is null.");

                var entry = await _context.Set<Entity>().FindAsync(id);

                if (entry == null)
                    return OperationResult<Entity>.Failure($"{typeof(Entity).Name} with id: {id} not found.");

                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return OperationResult<Entity>.Success($"{typeof(Entity).Name} updated successfully.", entry);
            }
            catch (Exception ex)
            {
                return OperationResult<Entity>.Failure($"An error occurred while trying to update the entity: {typeof(Entity).Name} with id: {id}. {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<Entity>> DeleteAsync(int id)
        {
            try
            {
                var entry = await _context.Set<Entity>().FindAsync(id);

                if (entry == null)
                    return OperationResult<Entity>.Failure($"{typeof(Entity).Name} with id: {id} not found.");

                _context.Set<Entity>().Remove(entry);
                await _context.SaveChangesAsync();

                return OperationResult<Entity>.Success($"{typeof(Entity).Name} deleted successfully.", entry);
            }
            catch (Exception ex)
            {
                return OperationResult<Entity>.Failure($"An error occurred while trying to delete the entity: {typeof(Entity).Name} with id: {id}. {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<List<Entity>>> GetAllListAsync()
        {
            try
            {
                var entities = await _context.Set<Entity>().ToListAsync();
                return OperationResult<List<Entity>>.Success($"{typeof(Entity).Name} list retrieved successfully.", entities);
            }
            catch (Exception ex)
            {
                return OperationResult<List<Entity>>.Failure($"An error occurred while trying to get all entities of type: {typeof(Entity).Name}. {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<Entity>> GetByIdAsync(int id)
        {
            try
            {
                var entry = await _context.Set<Entity>().FindAsync(id);

                if (entry == null)
                    return OperationResult<Entity>.Failure($"{typeof(Entity).Name} with id: {id} not found.");

                return OperationResult<Entity>.Success($"{typeof(Entity).Name} retrieved successfully.", entry);
            }
            catch (Exception ex)
            {
                return OperationResult<Entity>.Failure($"An error occurred while trying to get the entity: {typeof(Entity).Name} with id: {id}. {ex.Message}");
            }
        }

        // Additional - OperationResult doesn't apply here
        public virtual IQueryable<Entity> GetAllQuery()
        {
            try
            {
                return _context.Set<Entity>().AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while trying to get query for entities of type: {typeof(Entity).Name}.", ex);
            }
        }

        public virtual IQueryable<Entity> GetAllQueryWithInclude(List<string> properties)
        {
            try
            {
                var query = _context.Set<Entity>().AsQueryable();

                foreach (var property in properties)
                {
                    query = query.Include(property);
                }

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while trying to get query with includes for entities of type: {typeof(Entity).Name}.", ex);
            }
        }

        public virtual async Task<OperationResult<List<Entity>>> AddRangeAsync(List<Entity> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    return OperationResult<List<Entity>>.Failure("Entity list is null or empty.");

                await _context.Set<Entity>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return OperationResult<List<Entity>>.Success($"{typeof(Entity).Name} range added successfully.", entities);
            }
            catch (Exception ex)
            {
                return OperationResult<List<Entity>>.Failure($"An error occurred while trying to add range of entities of type: {typeof(Entity).Name}. {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<List<Entity>>> UpdateRange(List<Entity> entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    return OperationResult<List<Entity>>.Failure("Entity list is null or empty.");

                _context.Set<Entity>().UpdateRange(entities);
                await _context.SaveChangesAsync();

                return OperationResult<List<Entity>>.Success($"{typeof(Entity).Name} range updated successfully.", entities);
            }
            catch (Exception ex)
            {
                return OperationResult<List<Entity>>.Failure($"An error occurred while trying to update range of entities of type: {typeof(Entity).Name}. {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<bool>> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return OperationResult<bool>.Success("Changes saved successfully.", true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while trying to save changes: {ex.Message}");
            }
        }
    }
}
