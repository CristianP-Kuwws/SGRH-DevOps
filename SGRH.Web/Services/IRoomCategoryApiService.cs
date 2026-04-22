using SGRH.Web.Models;

namespace SGRH.Web.Services
{
    public interface IRoomCategoryApiService
    {
        Task<List<RoomCategoryViewModel>> GetAllAsync();
        Task<RoomCategoryViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? ErrorMessage)> CreateAsync(RoomCategoryViewModel roomCategory);
        Task<(bool Success, string? ErrorMessage)> UpdateAsync(int id, RoomCategoryViewModel roomCategory);
        Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id);
    }
}
