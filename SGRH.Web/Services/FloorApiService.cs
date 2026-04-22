using System.Net.Http.Json;
using System.Text.Json;
using SGRH.Web.Models;

namespace SGRH.Web.Services
{
    public class FloorApiService : IFloorApiService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;

        public FloorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FloorViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Floor");
            if (!response.IsSuccessStatusCode)
            {
                return new List<FloorViewModel>();
            }

            var data = await response.Content.ReadFromJsonAsync<List<FloorViewModel>>(JsonOptions);
            return data ?? new List<FloorViewModel>();
        }

        public async Task<FloorViewModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Floor/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<FloorViewModel>(JsonOptions);
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateAsync(FloorViewModel floor)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Floor", floor);
            return await BuildResultAsync(response);
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateAsync(int id, FloorViewModel floor)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Floor/{id}", floor);
            return await BuildResultAsync(response);
        }

        public async Task<(bool Success, string? ErrorMessage)> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Floor/{id}");
            return await BuildResultAsync(response);
        }

        private static async Task<(bool Success, string? ErrorMessage)> BuildResultAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }

            var error = await TryReadMessageAsync(response);
            return (false, error ?? "No se pudo completar la operacion con floors.");
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
