using SGRH.Web.Models;

namespace SGRH.Web.Services
{
    public interface IFloorApiService
    {
        Task<List<FloorViewModel>> GetAllAsync();
        Task<FloorViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? ErrorMessage)> CreateAsync(FloorViewModel floor);
        Task<(bool Success, string? ErrorMessage)> UpdateAsync(int id, FloorViewModel floor);
        Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id);
    }
}
