using System.Net.Http.Json;
using System.Text.Json;
using SGRH.Web.Models;

namespace SGRH.Web.Services
{
    public class RoomCategoryApiService : IRoomCategoryApiService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;

        public RoomCategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoomCategoryViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/RoomCategories");
            if (!response.IsSuccessStatusCode)
            {
                return new List<RoomCategoryViewModel>();
            }

            var data = await response.Content.ReadFromJsonAsync<List<RoomCategoryViewModel>>(JsonOptions);
            return data ?? new List<RoomCategoryViewModel>();
        }

        public async Task<RoomCategoryViewModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/RoomCategories/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<RoomCategoryViewModel>(JsonOptions);
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateAsync(RoomCategoryViewModel roomCategory)
        {
            var response = await _httpClient.PostAsJsonAsync("api/RoomCategories", roomCategory);
            return await BuildResultAsync(response);
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateAsync(int id, RoomCategoryViewModel roomCategory)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/RoomCategories/{id}", roomCategory);
            return await BuildResultAsync(response);
        }

        public async Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/RoomCategories/{id}");
            return await BuildResultAsync(response);
        }

        private static async Task<(bool Success, string? ErrorMessage)> BuildResultAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }

            var error = await TryReadMessageAsync(response);
            return (false, error ?? "No se pudo completar la operacion con room categories.");
        }

        private static async Task<string?> TryReadMessageAsync(HttpResponseMessage response)
        {
            try
            {
                var payload = await response.Content.ReadFromJsonAsync<ApiMessageResponse>(JsonOptions);
                return payload?.Message;
            }
            catch
            {
                return response.ReasonPhrase;
            }
        }
    }
}
